using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class ScrollNonUI : MonoBehaviour
    {
        public float tweenBackDuration = 0.3f;
        public Ease tweenBackEase;
        public bool freezeX, freezeY;
        public BoundedFloat xConstraints = new BoundedFloat();
        public BoundedFloat yConstraints = new BoundedFloat();
        private Vector2 offset;
        private Vector3 objectCenterToPointerClick;
        private float zDisplacement;
        private bool dragging;
        private Camera mainCamera;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            zDisplacement = -mainCamera.transform.position.z + transform.position.z;
        }

        public void OnMouseDown()
        {
            objectCenterToPointerClick = -transform.position + MouseInWorldCoords();
            transform.DOKill();
            dragging = true;
        }

        public void OnMouseUp()
        {
            dragging = false;
            TweenBack();
        }

        private void Update()
        {
            if (dragging)
            {
                Vector3 mousePos = MouseInWorldCoords();
                transform.position = new Vector3(
                    freezeX ? transform.position.x : mousePos.x - objectCenterToPointerClick.x,
                    freezeY ? transform.position.y : mousePos.y - objectCenterToPointerClick.y,
                    transform.position.z);
            }
        }

        private Vector3 MouseInWorldCoords()
        {
            Vector3 screenMousePos = Input.mousePosition;
            screenMousePos.z = zDisplacement;
            return mainCamera.ScreenToWorldPoint(screenMousePos);
        }

        private void TweenBack()
        {
            if (freezeY && transform.localPosition.x < xConstraints.min || xConstraints.max < transform.localPosition.x)
            {
                float targetX = (transform.localPosition.x < xConstraints.min) ? xConstraints.min : xConstraints.max;
                transform.DOLocalMoveX(targetX, tweenBackDuration).SetEase(tweenBackEase);
            }
            else if (freezeX && (transform.localPosition.y < yConstraints.min) || yConstraints.max < transform.localPosition.y)
            {
                float targetY = transform.localPosition.y < yConstraints.min ? yConstraints.min : yConstraints.max;
                transform.DOLocalMoveY(targetY, tweenBackDuration).SetEase(tweenBackEase);
            }
        }
    }
}
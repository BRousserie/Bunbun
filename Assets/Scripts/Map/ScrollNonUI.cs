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
                transform.DOLocalMoveX(BoundPosition(transform.localPosition.x), tweenBackDuration)
                    .SetEase(tweenBackEase);
            else if (freezeX && (transform.localPosition.y < yConstraints.min) || yConstraints.max < transform.localPosition.y)
                transform.DOLocalMoveY(BoundPosition(transform.localPosition.y), tweenBackDuration)
                    .SetEase(tweenBackEase);
        }

        public void FocusNode(MapNode currentNode)
        {
            if (!freezeX)
                transform.DOLocalMoveX(BoundPosition(-currentNode.transform.localPosition.y), tweenBackDuration)
                    .SetEase(tweenBackEase);
            if (!freezeY)
                transform.DOLocalMoveY(BoundPosition(-currentNode.transform.localPosition.y), tweenBackDuration)
                    .SetEase(tweenBackEase);
        }

        private float BoundPosition(float pos)
        {
            if (freezeY)
            {
                pos = Mathf.Max(pos, xConstraints.min);
                pos = Mathf.Min(pos, xConstraints.max);
            }
            else
            {
                pos = Mathf.Max(pos, yConstraints.min);
                pos = Mathf.Min(pos, yConstraints.max);
            }
            return pos;
        }
    }
}
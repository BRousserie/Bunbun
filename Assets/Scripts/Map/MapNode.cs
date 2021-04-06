using System;
using DG.Tweening;
using DOTween.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }
}

namespace Map
{
    public class MapNode : MonoBehaviour
    {
        private const float HoverScaleFactor = 1.2f;
        private const float MaxClickDuration = 0.5f;
        private float initialScale;
        private float mouseDownTime;
        
        public SpriteRenderer spriteRenderer;
        public Image visitedCircleImage;
        public Node Node { get; private set; }
        public RoomPattern RoomPattern { get; private set; }

        public void SetUp(Node node, RoomPattern roomPattern)
        {
            Node = node;
            RoomPattern = roomPattern;
            spriteRenderer.sprite = roomPattern.sprite;
            if (node.roomType == RoomType.Boss) transform.localScale *= 1.5f;
            initialScale = spriteRenderer.transform.localScale.x;
            visitedCircleImage.color = MapView.Instance.visitedColor;
            visitedCircleImage.gameObject.SetActive(false);
            SetState(NodeStates.Locked);
        }

        public void SetState(NodeStates state)
        {
            visitedCircleImage.gameObject.SetActive(false);
            spriteRenderer.DOKill();
            switch (state)
            {
                case NodeStates.Locked:
                    spriteRenderer.color = MapView.Instance.lockedColor;
                    break;
                case NodeStates.Visited:
                    spriteRenderer.color = MapView.Instance.visitedColor;
                    visitedCircleImage.gameObject.SetActive(true);
                    break;
                case NodeStates.Attainable: // start pulsating from visited to locked color:
                    spriteRenderer.color = MapView.Instance.lockedColor;
                    spriteRenderer.DOColor(MapView.Instance.visitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnMouseEnter()
        {
            spriteRenderer.transform.DOKill();
            spriteRenderer.transform.DOScale(initialScale * HoverScaleFactor, 0.3f);
        }

        private void OnMouseExit()
        {
            spriteRenderer.transform.DOKill();
            spriteRenderer.transform.DOScale(initialScale, 0.3f);
        }

        private void OnMouseDown()
        {
            mouseDownTime = Time.time;
        }

        private void OnMouseUp()
        {
            if (Time.time - mouseDownTime < MaxClickDuration)
                MapPlayerTracker.Instance.SelectNode(this);
        }

        public void ShowSwirlAnimation()
        {
            if (visitedCircleImage)
            {
                const float fillDuration = 0.3f;
                visitedCircleImage.fillAmount = 0;

                DG.Tweening.DOTween.To(() => visitedCircleImage.fillAmount, 
                    x => visitedCircleImage.fillAmount = x, 1f, fillDuration);
            }        
        }
    }
}
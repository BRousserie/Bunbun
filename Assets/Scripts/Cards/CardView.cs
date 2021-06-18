using System.Collections;
using Characters;
using Coffee.UIEffects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    public abstract class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Card Card;
        public float AnimationMovingSpeed = 1500.0f;

        [Header("UI Elements")]
        public Image Image;
        public Image Background;
        public Image NameBackground;
        public Text CardName;
        public Text CardDescription;
        public Text Cost;

        [Header("Dissolving")] 
        public UIDissolve CardViewDissolver;
        public UIDissolve DissolverOutline;
        public float DissolversDeltaTime;
        public float DissolversFadeTime = 1.0f;

        [HideInInspector] public Character owner;
        [HideInInspector] public Character target;

        protected const float HoverScaleFactor = 1.2f;
        protected float initialScale;
        protected bool dragging;
        protected Vector3 initialPosition;
        protected Vector3 originalMousePosition;
        protected bool returningToOrigin;

        private void Start()
        {
            Image.sprite = Card.Sprite;
            Background.sprite = Card.CardType.Background;
            NameBackground.color = Card.Rarity.Color;
            CardName.text = Card.Name;
            CardDescription.text = Card.Description;
            Cost.text = Card.EnergyCost.ToString();
        
            DissolverOutline.color = Card.Rarity.Color;
        
            initialScale = Image.transform.localScale.x;
            initialPosition = transform.localPosition;

            DissolveIn();
        }
        
        private void FixedUpdate()
        {
            if (dragging)
            {
                Drag();
            }
        }

        #region MouseEvents

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!returningToOrigin)
                transform.DOKill();
            transform.DOScale(initialScale * HoverScaleFactor, 0.3f).SetEase(Ease.OutExpo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!returningToOrigin)
                transform.DOKill();
            transform.DOScale(initialScale, 0.3f).SetEase(Ease.OutExpo);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            originalMousePosition = Input.mousePosition;
            dragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            MouseUp();
            dragging = false;
        }

        #endregion


        protected abstract void Drag();
        protected abstract void MouseUp();

        protected void ReturnToPosition()
        {
            returningToOrigin = true;
            transform.DOLocalMove(initialPosition, 
                    (transform.localPosition - initialPosition).magnitude / AnimationMovingSpeed)
                .SetEase(Ease.OutExpo)
                .onComplete += () => returningToOrigin = false;
        }
        
        #region Dissolve

        public void DissolveOut()
        {
            Dissolve(CardViewDissolver, false);
            StartCoroutine(DelayDissolve(DissolverOutline, false));
        }

        public void DissolveIn()
        {
            Dissolve(DissolverOutline, true);
            StartCoroutine(DelayDissolve(CardViewDissolver, true));
        }

        IEnumerator DelayDissolve(UIDissolve dissolver, bool _in)
        {
            yield return new WaitForSeconds(DissolversDeltaTime);
            Dissolve(dissolver, _in);
        }

        private void Dissolve(UIDissolve dissolver, bool _in)
        {
            dissolver.effectFactor = (_in) ? 1.0f : 0.0f;
            DG.Tweening.DOTween.To(() => dissolver.effectFactor, x => dissolver.effectFactor = x,
                    (_in) ? 0.0f : 1.0f, DissolversFadeTime)
                .SetEase(Ease.InOutCubic)
                .onComplete += () => { if (!_in) Destroy(gameObject); };
        }

        #endregion
    }
}

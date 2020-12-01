using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Map;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CardView : MonoBehaviour
{
    public Card Card;
    public SpriteRenderer SpriteRenderer;
    public Character owner;
    public Character target;

    private const float HoverScaleFactor = 1.2f;
    private const float MaxClickDuration = 0.5f;
    private float initialScale;
    private float mouseDownTime;
    
    private void Start()
    {
        SpriteRenderer.sprite = Card.Sprite;
        initialScale = SpriteRenderer.transform.localScale.x;
    }

    private void OnMouseEnter()
    {
        SpriteRenderer.transform.DOKill();
        SpriteRenderer.transform.DOScale(initialScale * HoverScaleFactor, 0.3f);
    }

    private void OnMouseExit()
    {
        SpriteRenderer.transform.DOKill();
        SpriteRenderer.transform.DOScale(initialScale, 0.3f);
    }
}

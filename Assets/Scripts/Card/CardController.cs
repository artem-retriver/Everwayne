using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    public SpriteRenderer[] _spriteRenderers;
    public Canvas _canvasPrice;
    
    private BoxCollider _boxCollider;
    private Vector3 _currentPos;

    public bool IsAttack, IsHeal, IsShield;
    private bool isPos;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void ActivateBoxCollider()
    {
        _boxCollider.enabled = true;
    }

    public void CheckCurrentPos()
    {
        _currentPos = transform.position;
    }
    
    public void OnMouseEnter()
    {
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f);
        transform.DOMoveY(-2.7f, 0.5f);

        foreach (var t in _spriteRenderers)
        {
            t.sortingOrder += 100;
        }

        _canvasPrice.sortingOrder += 100;
    }

    public void OnMouseExit()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        transform.DOMoveY(_currentPos.y, 0.5f);
            
        foreach (var t in _spriteRenderers)
        {
            t.sortingOrder -= 100;
        }
            
        _canvasPrice.sortingOrder -= 100;
    }
}

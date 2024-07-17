using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardPoolController _cardPoolController;
    [SerializeField] private CardManaController _cardManaController;
    [SerializeField] private DiscardController _discardController;
    [SerializeField] private SetcardController _setcardController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _outlineCard;
    
    public SpriteRenderer[] _spriteRenderers;
    public Canvas[] _canvasCard;
    
    private BoxCollider _boxCollider;
    private Vector3 _currentPosition;
    private Quaternion _currentRotation;

    public int PriceCard;
    public bool IsAttack, IsSuperAttack, IsHeal, IsLargeHeal, IsShield, IsSuperShield;
    private bool isPos, isUse;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void ActivateBoxCollider()
    {
        _boxCollider.enabled = true;
    }

    public void CheckCurrentPosition()
    {
        _currentPosition = transform.position;
    }

    public void CheckCurrentRotation()
    {
        _currentRotation = transform.rotation;
    }

    public void EndMoveRound()
    {
        if (isUse == false)
        {
            isUse = true;
            _boxCollider.enabled = false;
            _outlineCard.SetActive(false);
        
            transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.5f);
            transform.DOMove(_discardController.transform.position, 0.5f);
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        
            _discardController.IncreaseScore();
            _cardPoolController.DecreaseCountCurrentCardInPool();
        }
    }
    
    public void OnMouseEnter()
    {
        if (isUse != true)
        {
            _outlineCard.SetActive(true);
        
            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
            transform.DOMoveY(-2.3f, 0.5f);
            transform.DORotate(new Vector3(0,0,0), 0.5f);

            IncreaseSortingOrder();
        }
    }

    public void OnMouseDown()
    {
        if (_cardManaController.CurrentMana > 0 && _cardManaController.CurrentMana >= PriceCard )
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(EndMoveRound);
            sequence.AppendCallback(() => { _cardManaController.DecreaseScoreMana(PriceCard); });
            sequence.AppendCallback(DecreaseSortingOrder);

            sequence.AppendInterval(0.25f);

            sequence.AppendCallback(DecreaseSortingOrder);

            sequence.AppendCallback(() =>
            {
                if (IsAttack == true)
                {
                    _playerController.ActivateAttack();
                }
                else if (IsSuperAttack == true)
                {
                    _playerController.ActivateAttack();
                }
                else if (IsHeal == true)
                {
                    _playerController.ActivateIncreaseHp();
                }
                else if (IsLargeHeal == true)
                {
                    _playerController.ActivateLargeIncreaseHp();
                }
                else if (IsShield == true)
                {
                    _playerController.ActivateIncreaseShield();
                }
                else if (IsSuperShield == true)
                {
                    _playerController.ActivateLargeIncreaseShield();
                }
            });

            sequence.AppendInterval(0.1f);
        
            sequence.AppendCallback(() =>
            {
                if (IsAttack == true)
                {
                    _enemyController.ActivateDecreaseHp();
                }
                else if(IsSuperAttack == true)
                {
                    _enemyController.ActivateLargeDecreaseHp();
                }
            });

            sequence.AppendInterval(0.15f);
            
            sequence.AppendCallback(() => { transform.DOMove(_setcardController.transform.position, 0); });
        }
    }

    public void OnMouseExit()
    {
        if (isUse != true)
        {
            _outlineCard.SetActive(false);
        
            transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            transform.DOMoveY(_currentPosition.y, 0.5f);
            transform.DORotateQuaternion(_currentRotation, 0.5f);
            
            DecreaseSortingOrder();
        }
    }
    
    private void IncreaseSortingOrder()
    {
        foreach (var t in _spriteRenderers)
        {
            t.sortingOrder += 100;
        }
        
        foreach (var t in _canvasCard)
        {
            t.sortingOrder += 100;
        }
    }

    private void DecreaseSortingOrder()
    {
        foreach (var t in _spriteRenderers)
        {
            t.sortingOrder -= 100;
        }
        
        foreach (var t in _canvasCard)
        {
            t.sortingOrder -= 100;
        }
    }
}

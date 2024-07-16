using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardPoolController : MonoBehaviour
{
    [SerializeField] private SetcardController _setcardController;
    [SerializeField] private CardController[] _cardControllers;
    [SerializeField] private GameObject[] _positionRotationCards;

    public List<CardController> _listNewCard;
    private Sequence _sequence, _sequence1;
    
    private int countCurrentCardInPool, increaseOrderLayer = 5;

    public void InstantiateCard()
    {
        _sequence = DOTween.Sequence();

        _sequence.AppendCallback(InstantiateNewCard);
        _sequence.AppendCallback(ChangeOrderLayerNewCard);
        _sequence.AppendCallback(MoveNewCard);
        _sequence.AppendCallback(ScaleNewCard);
        _sequence.AppendCallback(RotateNewCard);
        _sequence.AppendCallback(() => { _setcardController.DeacreaseScore(); });

        _sequence.AppendInterval(0.5f);
        
        _sequence.AppendCallback(CheckForStopInstantiateNewCard);

        _sequence.SetLoops(-1);
    }

    private void CheckForStopInstantiateNewCard()
    {
        if (countCurrentCardInPool == 4)
        {
            _sequence.Kill();

            _sequence1 = DOTween.Sequence();
            
            _sequence1.AppendInterval(0.5f);
            
            _sequence1.AppendCallback(() =>
            {
                foreach (var t in _listNewCard)
                {
                    t.CheckCurrentPos();
                    t.ActivateBoxCollider();
                }
            });
        }
        else
        {
            increaseOrderLayer += 10;
            countCurrentCardInPool++;
        }
    }

    private void InstantiateNewCard()
    {
        int randomCard = Random.Range(0, 3);

        var newCard = Instantiate(_cardControllers[randomCard],
            _positionRotationCards[countCurrentCardInPool].transform, true);
        
        _listNewCard.Add(newCard);
    }

    private void MoveNewCard()
    {
        _listNewCard[^1].transform.DOMove(_positionRotationCards[countCurrentCardInPool].transform.position, 0.5f);
    }

    private void ScaleNewCard()
    {
        _listNewCard[^1].transform.DOScale(new Vector3(1,1,1), 0.5f);
    }

    private void RotateNewCard()
    {
        switch (countCurrentCardInPool)
        {
            case 0:
                _listNewCard[^1].transform.DORotate(new(0, 0, 25), 0.5f);
                break;
            case 1:
                _listNewCard[^1].transform.DORotate(new(0, 0, 10), 0.5f);
                break;
            case 2:
                _listNewCard[^1].transform.DORotate(new(0, 0, 0), 0.5f);
                break;
            case 3:
                _listNewCard[^1].transform.DORotate(new(0, 0, -10), 0.5f);
                break;
            case 4:
                _listNewCard[^1].transform.DORotate(new(0, 0, -25), 0.5f);
                break;
        }
    }

    private void ChangeOrderLayerNewCard()
    {
        _listNewCard[^1]._canvasPrice.gameObject.SetActive(true);
        _listNewCard[^1]._canvasPrice.sortingOrder += increaseOrderLayer;
        
        foreach (var t in _listNewCard[^1]._spriteRenderers)
        {
            t.sortingOrder += increaseOrderLayer;
        }
    }
}
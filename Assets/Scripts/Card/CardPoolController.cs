using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardPoolController : MonoBehaviour
{
    [SerializeField] private GameObject[] _positionRotationCards;
    [SerializeField] private List<CardController> _listNewCard;
    
    private DiscardController _discardController;
    private SetcardController _setcardController;
    private CardController[] _cardControllers;
    private Sequence _sequence, _sequence1;
    private Button _endTurnButton;
    
    private int countCurrentCardInPool, increaseOrderLayer = 5;

    private void Start()
    {
        _discardController = GetComponentInChildren<DiscardController>();
        _setcardController = GetComponentInChildren<SetcardController>();
        _cardControllers = GetComponentsInChildren<CardController>();
        _endTurnButton = GetComponentInChildren<Button>();
    }

    public void CheckForInstantiate()
    {
        if (countCurrentCardInPool == 0)
        {
            InstantiateCard();
        }
    }

    public void DecreaseCountCurrentCardInPool()
    {
        countCurrentCardInPool--;
    }

    public void EndMoveAllCard()
    {
        foreach (var t in _listNewCard)
        {
            t.EndMoveRound();
        }
    }
    
    private void InstantiateCard()
    {
        _sequence = DOTween.Sequence();

        _sequence.AppendCallback(CheckForChangeDeck);
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

    private void CheckForChangeDeck()
    {
        if (_setcardController.CountCurrentScore == 0)
        {
            _setcardController.ActivateChangeFVX();
            _discardController.ActivateChangeFVX();
        }
    }

    private void CheckForStopInstantiateNewCard()
    {
        countCurrentCardInPool++;
        increaseOrderLayer += 10;
        
        if (countCurrentCardInPool == 5)
        {
            _sequence.Kill();

            _sequence1 = DOTween.Sequence();
            
            _sequence1.AppendInterval(0.5f);

            _sequence1.AppendCallback(() => { _endTurnButton.interactable = true; });
            
            _sequence1.AppendCallback(() =>
            {
                foreach (var t in _listNewCard)
                {
                    t.CheckCurrentPosition();
                    t.CheckCurrentRotation();
                    t.ActivateBoxCollider();
                }
            });
        }
    }

    private void InstantiateNewCard()
    {
        int randomCard = Random.Range(0, 6);

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
                _listNewCard[^1].transform.DORotate(new Vector3(0, 0, 25), 0.5f);
                break;
            case 1:
                _listNewCard[^1].transform.DORotate(new Vector3(0, 0, 10), 0.5f);
                break;
            case 2:
                _listNewCard[^1].transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case 3:
                _listNewCard[^1].transform.DORotate(new Vector3(0, 0, -10), 0.5f);
                break;
            case 4:
                _listNewCard[^1].transform.DORotate(new Vector3(0, 0, -25), 0.5f);
                break;
        }
    }

    private void ChangeOrderLayerNewCard()
    {
        foreach (var t in _listNewCard[^1]._canvasCard)
        {
            t.gameObject.SetActive(true);
            t.sortingOrder += increaseOrderLayer;
        }
        
        foreach (var t in _listNewCard[^1]._spriteRenderers)
        {
            t.sortingOrder += increaseOrderLayer;
        }
    }
}
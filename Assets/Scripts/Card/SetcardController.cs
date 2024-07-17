using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SetcardController : MonoBehaviour
{
    [SerializeField] private GameObject _changeFVX;
    
    private Text _scoreCurrentSetcard;
    
    public int CountCurrentScore = 20;

    private void Start()
    {
        _scoreCurrentSetcard = GetComponentInChildren<Text>();
    }

    public void DeacreaseScore()
    {
        CountCurrentScore--;

        CheckToText();
    }

    public void ActivateChangeFVX()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => { _changeFVX.SetActive(true); });
        sequence.AppendCallback(() => { CountCurrentScore += 20; });
        sequence.AppendCallback(CheckToText);

        sequence.AppendInterval(2);
        
        sequence.AppendCallback(() => { _changeFVX.SetActive(false); });
    }

    private void CheckToText()
    {
        _scoreCurrentSetcard.text = CountCurrentScore.ToString();
    }
}

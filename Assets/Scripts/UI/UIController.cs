using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] _scenePanels;
    [SerializeField] private GameObject _showGame;
    [SerializeField] private GameObject _hideGame;

    private void Awake()
    {
        ShowGame();
    }

    private void ShowGame()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => { _showGame.SetActive(true); });

        sequence.AppendInterval(3);

        sequence.AppendCallback(() => { _showGame.SetActive(false); });
    }
}

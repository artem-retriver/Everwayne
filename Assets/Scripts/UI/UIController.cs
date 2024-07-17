using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private CardPoolController _cardPoolController;
    [SerializeField] private GameObject _showGame;
    [SerializeField] private GameObject _hideGame;

    private void Awake()
    {
        ShowGame();
    }

    private void ShowGame()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(3);

        sequence.AppendCallback(() => { _showGame.SetActive(false); });
    }

    public void HideGameWin()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => { _cardPoolController.gameObject.SetActive(false); });

        sequence.AppendInterval(1);

        sequence.AppendCallback(() => { _hideGame.SetActive(true); });
        
        sequence.AppendInterval(4);

        sequence.AppendCallback(() => { SceneManager.LoadScene("LevelTwo"); });
    }
    
    public void HideGameLose()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => { _cardPoolController.gameObject.SetActive(false); });

        sequence.AppendInterval(1);

        sequence.AppendCallback(() => { _hideGame.SetActive(true); });
        
        sequence.AppendInterval(4);

        sequence.AppendCallback(() => { SceneManager.LoadScene("LevelOne"); });
    }
}

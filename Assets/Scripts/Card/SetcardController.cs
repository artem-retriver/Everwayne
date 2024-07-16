using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetcardController : MonoBehaviour
{
    private Text _scoreCurrentSetcard;
    
    private int countCurrentScore = 10;

    private void Start()
    {
        _scoreCurrentSetcard = GetComponentInChildren<Text>();
    }

    public void DeacreaseScore()
    {
        countCurrentScore--;
        
        CheckToText();
    }

    private void CheckToText()
    {
        _scoreCurrentSetcard.text = countCurrentScore.ToString();
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DiscardController : MonoBehaviour
{
    [SerializeField] private GameObject _changeFVX;
    
    private Text _scoreCurrentDiscard;
    
    public int CountCurrentScore;

    private void Start()
    {
        _scoreCurrentDiscard = GetComponentInChildren<Text>();
    }

    public void IncreaseScore()
    {
        CountCurrentScore++;
        
        CheckToText();
    }
    
    public void ActivateChangeFVX()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => { _changeFVX.SetActive(true); });
        sequence.AppendCallback(() => { CountCurrentScore -= 20; });
        sequence.AppendCallback(CheckToText);

        sequence.AppendInterval(2);
        
        sequence.AppendCallback(() => { _changeFVX.SetActive(false); });
    }

    private void CheckToText()
    {
        _scoreCurrentDiscard.text = CountCurrentScore.ToString();
    }
}

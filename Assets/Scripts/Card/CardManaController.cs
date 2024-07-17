using UnityEngine;
using UnityEngine.UI;

public class CardManaController : MonoBehaviour
{
    private Text _currentScoreMana;

    public int CurrentMana = 4, MaxMana = 4;

    private void Start()
    {
        _currentScoreMana = GetComponentInChildren<Text>();
        
        CheckCurrentScoreMana();
    }

    public void RestoreManaCount()
    {
        CurrentMana = 0;
        CurrentMana += 4;
        
        CheckCurrentScoreMana();
    }

    public void DecreaseScoreMana(int priceMana)
    {
        CurrentMana -= priceMana;
        
        CheckCurrentScoreMana();
    }

    private void CheckCurrentScoreMana()
    {
        _currentScoreMana.text = CurrentMana.ToString("0") + "/" + MaxMana.ToString("0");
    }
}

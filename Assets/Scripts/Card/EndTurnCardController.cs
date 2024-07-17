using UnityEngine;
using UnityEngine.UI;

public class EndTurnCardController : MonoBehaviour
{
    [SerializeField] private CardManaController _cardManaController;
    [SerializeField] private CardPoolController _cardPoolController;
    [SerializeField] private EnemyController _enemyController;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void EndTurnButton()
    {
        _button.interactable = false;
        
        _cardManaController.RestoreManaCount();
        _cardPoolController.EndMoveAllCard();
        _enemyController.CheckForNextAction();
    }
}

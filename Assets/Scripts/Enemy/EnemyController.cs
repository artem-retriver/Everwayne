using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIController _uIController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CardPoolController _cardPoolController;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _shieldText;

    private CanvasDestroyController[] _canvasObjects;
    private Animator _animator;

    private float attack = 10, saveAttack = 10, 
        currentLargeAttack = 25, saveLargeAttack = 25, Shield;

    public float Hp, MaxHp;

    private void Start()
    {
        _canvasObjects = GetComponentsInChildren<CanvasDestroyController>();
        _animator = GetComponent<Animator>();

        OffAllCanvasObjects();
        CheckCurrentHp();
        CheckCurrentShield();
    }

    public void CheckForNextAction()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1);

        sequence.AppendCallback(() =>
        {
            int randomInt = Random.Range(0, 2);

            if (randomInt == 0)
            {
                ActivateAttack();
            }
            else
            {
                ActivateIncreaseShield();
            }
        });

        sequence.AppendInterval(1);

        sequence.AppendCallback(() => { _cardPoolController.CheckForInstantiate(); });
    }
    
    public void ActivateDecreaseHp()
    {
        if (Shield > 0)
        {
            saveAttack -= Shield;
            Shield -= attack;

            if (Shield <= 0 && saveAttack > 0)
            {
                Shield = 0;
                Hp -= saveAttack;
            }
        }
        else
        {
            Hp -= saveAttack;
        }

        saveAttack = 10;
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[0]);
        CheckCurrentHp();
        CheckCurrentShield();
    }
    
    public void ActivateLargeDecreaseHp()
    {
        if (Shield > 0)
        {
            saveLargeAttack -= Shield;
            Shield -= currentLargeAttack;

            if (Shield <= 0 && saveLargeAttack > 0)
            {
                Shield = 0;
                Hp -= saveLargeAttack;
            }
        }
        else
        {
            Hp -= saveLargeAttack;
        }

        saveLargeAttack = 25;
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[1]);
        CheckCurrentHp();
        CheckCurrentShield();
    }

    private void ActivateAttack()
    {
        _animator.SetTrigger("Attack");
        _playerController.ActivateDecreaseHp();
    }
    
    private void ActivateDeath()
    {
        _animator.SetTrigger("Death");
    }
    
    private void ActivateIncreaseShield()
    {
        Shield += 5;
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[2]);
        CheckCurrentShield();
    }

    private void CheckCurrentHp()
    {
        if (Hp <= 0)
        {
            Hp = 0;
            
            ActivateDeath();
            _uIController.HideGameWin();
        }
        
        _hpText.text = Hp.ToString("0") + "/" + MaxHp.ToString("0");
        _hpImage.fillAmount = Hp/MaxHp;
    }

    private void CheckCurrentShield()
    {
        _shieldText.text = Shield.ToString("0");
    }
    
    private void InstantiateCanvasBase(CanvasDestroyController canvasDestroyController)
    {
        var newCanvasIncreaseHp = Instantiate(canvasDestroyController, transform);
        newCanvasIncreaseHp.gameObject.SetActive(true);
        newCanvasIncreaseHp.DestroyCanvasIncreaseHp();
    }

    private void OffAllCanvasObjects()
    {
        foreach (var t in _canvasObjects)
        {
            t.gameObject.SetActive(false);
        }
    }
}

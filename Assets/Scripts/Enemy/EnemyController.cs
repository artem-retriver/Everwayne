using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UIController _uIController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CardPoolController _cardPoolController;
    [SerializeField] private GameObject _canvasDecreaseHp;
    [SerializeField] private GameObject _canvasLargeDecreaseHp;
    [SerializeField] private GameObject _canvasIncreaseShield;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _shieldText;
    
    private Animator _animator;

    private float currentAttack = 10, saveAttack = 10, currentLargeAttack = 25, saveLargeAttack = 25,
        Shield = 0;

    public float Hp, MaxHp;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
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
            Shield -= currentAttack;

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
        
        InstantiateCanvasBase(_canvasDecreaseHp);
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
        
        InstantiateCanvasBase(_canvasLargeDecreaseHp);
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
        
        InstantiateCanvasBase(_canvasIncreaseShield);
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
    
    private void InstantiateCanvasBase(GameObject canvasObject)
    {
        var newCanvasIncreaseHp = Instantiate(canvasObject, transform);
        newCanvasIncreaseHp.SetActive(true);
    }
}

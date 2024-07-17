using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIController _uIController;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _shieldText;
    
    private CanvasDestroyController[] _canvasObjects;
    private Animator _animator;

    private float currentAttack = 25, saveAttack = 25, Shield = 0, Hp = 64, MaxHp = 64;
    private int randomAttack;

    private void Start()
    {
        _canvasObjects = GetComponentsInChildren<CanvasDestroyController>();
        _animator = GetComponent<Animator>();
        
        OffAllCanvasObjects();
        CheckCurrentHp();
        CheckCurrentShield();
    }

    public void ActivateAttack()
    {
        randomAttack = Random.Range(1, 4);
        _animator.SetTrigger("Attack" + randomAttack);
    }
    
    public void ActivateIncreaseHp()
    {
        Hp += 5;
        
        if (Hp >= 64)
        {
            Hp = 64;
        }
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[0]);
        CheckCurrentHp();
    }
    
    public void ActivateLargeIncreaseHp()
    {
        Hp += 10;
        
        if (Hp >= 64)
        {
            Hp = 64;
        }
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[1]);
        CheckCurrentHp();
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
        
        saveAttack = 25;
        
        if (Hp <= 0)
        {
            Hp = 0;
            
            ActivateDeath();
            _uIController.HideGameLose();
        }
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasObjects[2]);
        CheckCurrentHp();
        CheckCurrentShield();
    }
    
    public void ActivateIncreaseShield()
    {
        Shield += 5;
        
        _animator.SetTrigger("Block");
        
        InstantiateCanvasBase(_canvasObjects[3]);
        CheckCurrentShield();
    }
    
    public void ActivateLargeIncreaseShield()
    {
        Shield += 10;
        
        _animator.SetTrigger("Block");
        
        InstantiateCanvasBase(_canvasObjects[4]);
        CheckCurrentShield();
    }

    private void InstantiateCanvasBase(CanvasDestroyController canvasDestroyController)
    {
        var newCanvasIncreaseHp = Instantiate(canvasDestroyController, transform);
        newCanvasIncreaseHp.gameObject.SetActive(true);
        newCanvasIncreaseHp.DestroyCanvasIncreaseHp();
    }

    private void CheckCurrentHp()
    {
        _hpText.text = Hp.ToString("0") + "/" + MaxHp.ToString("0");
        _hpImage.fillAmount = Hp/MaxHp;
    }
    
    private void ActivateDeath()
    {
        _animator.SetTrigger("Death");
    }

    private void CheckCurrentShield()
    {
        _shieldText.text = Shield.ToString("0");
    }
    
    private void OffAllCanvasObjects()
    {
        foreach (var t in _canvasObjects)
        {
            t.gameObject.SetActive(false);
        }
    }
}

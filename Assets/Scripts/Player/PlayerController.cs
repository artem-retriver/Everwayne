using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIController _uIController;
    [SerializeField] private GameObject _canvasIncreaseHp;
    [SerializeField] private GameObject _canvasLargeIncreaseHp;
    [SerializeField] private GameObject _canvasDecreaseHp;
    [SerializeField] private GameObject _canvasLargeIncreaseShield;
    [SerializeField] private GameObject _canvasIncreaseShield;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _shieldText;
    
    private Animator _animator;

    private float currentAttack = 25, saveAttack = 25, Shield = 0, Hp = 64, MaxHp = 64;
    private int randomAttack;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
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
        
        InstantiateCanvasBase(_canvasIncreaseHp);
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
        
        InstantiateCanvasBase(_canvasLargeIncreaseHp);
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
            Debug.Log("You Lose");
        }
        
        _animator.SetTrigger("Hurt");
        
        InstantiateCanvasBase(_canvasDecreaseHp);
        CheckCurrentHp();
        CheckCurrentShield();
    }
    
    public void ActivateIncreaseShield()
    {
        Shield += 5;
        
        _animator.SetTrigger("Block");
        
        InstantiateCanvasBase(_canvasIncreaseShield);
        CheckCurrentShield();
    }
    
    public void ActivateLargeIncreaseShield()
    {
        Shield += 10;
        
        _animator.SetTrigger("Block");
        
        InstantiateCanvasBase(_canvasLargeIncreaseShield);
        CheckCurrentShield();
    }

    private void InstantiateCanvasBase(GameObject canvasObject)
    {
        var newCanvasIncreaseHp = Instantiate(canvasObject, transform);
        newCanvasIncreaseHp.SetActive(true);
    }

    private void CheckCurrentHp()
    {
        if (Hp <= 0)
        {
            Hp = 0;
            
            _uIController.HideGameLose();
            ActivateDeath();
        }
        
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
}

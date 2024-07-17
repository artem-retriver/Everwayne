using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanvasDestroyController : MonoBehaviour
{
    private void Start()
    {
        DestroyCanvasIncreaseHp();
    }

    private void DestroyCanvasIncreaseHp()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.3f);

        sequence.AppendCallback(() => { Destroy(gameObject); });
    }
}

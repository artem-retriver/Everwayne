using DG.Tweening;
using UnityEngine;

public class CanvasDestroyController : MonoBehaviour
{
    public void DestroyCanvasIncreaseHp()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.3f);

        sequence.AppendCallback(() => { Destroy(gameObject); });
    }
}

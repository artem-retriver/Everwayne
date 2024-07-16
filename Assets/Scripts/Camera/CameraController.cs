using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CardPoolController _cardPoolController;
    
    private CinemachineVirtualCamera[] _virtualCameras;

    private void Awake()
    {
        _virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();
        
        StartMoveCamera();
    }

    private void StartMoveCamera()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(3);

        sequence.AppendCallback(() => { _virtualCameras[0].enabled = false; });
        sequence.AppendCallback(() => { _virtualCameras[1].enabled = true; });

        sequence.AppendInterval(5);

        sequence.AppendCallback(() => { _cardPoolController.InstantiateCard(); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public Camera CurrentCamera { get; private set; }

    private void Awake()
    {
        CurrentCamera = mainCamera;
    }
}

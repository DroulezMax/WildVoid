using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private OverlaysManager overlaysManager;
    private CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        if (!(overlaysManager = FindObjectOfType<OverlaysManager>()))
            Debug.LogError("PlayerController needs an OverlaysManager in scene");

        if (!(cameraController = FindObjectOfType<CameraController>()))
            Debug.LogError("PlayerController needs a CameraController in scene");
       
    }

    private void OnMove(InputValue value)
    {
        cameraController.OnMove(value);
    }

    private void OnRotate(InputValue value)
    {
        cameraController.OnRotate(value);
    }

    private void OnZoom(InputValue value)
    {
        cameraController.OnZoom(value);
    }

    private void OnOverlayBuilding()
    {
        overlaysManager.OnOverlayBuilding();
    }

    private void OnOverlayResourceStorage()
    {
        overlaysManager.OnOverlayResourceStorage();
    }

    private void OnOverlayLane()
    {
        overlaysManager.OnOverlayLane();
    }
}

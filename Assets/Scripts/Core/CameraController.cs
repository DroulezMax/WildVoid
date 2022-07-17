using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Parameters")]
    [Range(-90, 90)]
    [SerializeField] private float angle = 0;
    [SerializeField] private float distance = 20;
    [SerializeField] private float minDistance = 10;
    [SerializeField] private float maxDistance = 100;

    [Header("Controller")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float rotationSpeed = 90;
    [SerializeField] private float zoomSensibility = 0.02f;
    [SerializeField] private float zoomDuration = 1;
    [SerializeField] private AnimationCurve zoomCurve;

    [Header("References")]
    [SerializeField] new private Camera camera = null;

    private Vector2 moveInput = new Vector2(0, 0);
    private float rotateInput = 0;

    private Coroutine zoomCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void OnValidate()
    {
        PlaceAtAngle();
        PlaceAtDistance();
    }

    private void PlaceAtAngle()
    {
        Quaternion rot = transform.rotation;
        rot = Quaternion.Euler(angle, rot.eulerAngles.y, rot.eulerAngles.z);
        transform.rotation = rot;
    }

    private void PlaceAtDistance()
    {
        camera.transform.localPosition = new Vector3(0, 0, -distance);
    }

    private void Move()
    {
        transform.position += Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        transform.Rotate(0, rotateInput * rotationSpeed * Time.deltaTime, 0, Space.World);
    }

    private void StartNewZoom(float zoomAmount, float duration)
    {
        if (zoomCoroutine != null) 
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomCor(zoomAmount, duration));
    }

    private IEnumerator ZoomCor(float zoomAmount, float duration)
    {
        float counter = 0;
        float startDistance = distance;
        float endDistance = Mathf.Clamp(distance + zoomAmount, minDistance, maxDistance);

        while (counter < duration)
        {
            yield return null;
            counter += Time.deltaTime;
            distance = Mathf.Lerp(startDistance, endDistance, zoomCurve.Evaluate(counter / duration));
            PlaceAtDistance();
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        rotateInput = value.Get<float>();
    }

    public void OnZoom(InputValue value)
    {
        if(value.Get<float>() != 0)
        {
            StartNewZoom(-value.Get<float>() * zoomSensibility, zoomDuration);
        }
    }


}

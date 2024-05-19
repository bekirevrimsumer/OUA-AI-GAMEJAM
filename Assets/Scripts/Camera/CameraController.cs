using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CharacterTransform;
    public Transform CameraFollow;  // Karakter nesnesinin Transform bileşeni
    public Transform Camera;  // Kamera nesnesinin Transform bileşeni
    public Vector3 CameraOffset = new Vector3(0f, 2f, -5f);  // Kamera offset değeri
    public float CameraSpeed = 2f;  // Kamera hareket hızı
    
    private float _cameraRotationX = 0f;  // Kamera yatay açısı
    private float _cameraRotationY = 0f;  // Kamera dikey açısı
    public float _minYAngle = 0f;  // Kamera minimum dikey açısı
    public float _maxYAngle = 150f;  // Kamera maksimum dikey açısı

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _cameraRotationX += mouseX * CameraSpeed;
            _cameraRotationY -= mouseY * CameraSpeed;
            
            _cameraRotationY = Mathf.Clamp(_cameraRotationY, _minYAngle, _maxYAngle);
        }
        
        Quaternion cameraRotation = Quaternion.Euler(_cameraRotationY, _cameraRotationX, 0f);
        Vector3 cameraOffsetPosition = cameraRotation * CameraOffset;
        
        Camera.position = CameraFollow.position + cameraOffsetPosition + new Vector3(0f, 2f, 0f);
        Camera.LookAt(CameraFollow.position);
    }
}

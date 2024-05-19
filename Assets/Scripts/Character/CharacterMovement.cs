using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float MoveSpeed;

    private TimeRewinder _timeRewinder;
    private Animator _anim;
    private Camera _camera;

    void Start()
    {
        _timeRewinder = GetComponent<TimeRewinder>();
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var movement = GetMovement();
        movement = Vector3.ClampMagnitude(movement, 1);

        Quaternion cameraRotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
        movement = cameraRotation * movement;

        if(!_timeRewinder.IsRewinding)
            _anim.SetFloat("Speed", movement.magnitude);

        Vector3 newPosition = transform.position + movement * Time.deltaTime * MoveSpeed;

        transform.position = newPosition;
        Rotate(movement);
    }

    private void Rotate(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    private Vector3 GetMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0, vertical);

        return movement;
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TimeRewinder : MonoBehaviour
{
    public bool IsRewinding = false;
    public float RecordTime = 5f;

    public float recordInterval = 0.5f; // Kayıt aralığı
    public float lastRecordTime = 0; // Son kayıt zamanı
    Animator _anim;
    public List<Point> _points;
    Rigidbody _rb;

    void Start()
    {
        _points = new List<Point>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRewind();
        }

        if(_points.Count > 0 && IsRewinding)
        {
            Camera.main.transform.DOShakeRotation(0.2f, 0.3f, 10, 90, false);
        }
    }

    void FixedUpdate()
    {
        if (IsRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void StartRewind()
    {
        IsRewinding = true;
        _rb.isKinematic = true;
        _anim.SetBool("IsReverse", true);
    }

    public void StopRewind()
    {
        IsRewinding = false;
        _rb.isKinematic = false;
        _anim.SetBool("IsReverse", false);
    }

    void Rewind()
    {
        if (_points.Count > 0)
        {
            Point point = _points[0];
            transform.position = point.Position;
            transform.rotation = point.Rotation;
            Debug.Log(point.MoveSpeed);
            _anim.SetFloat("Speed", point.MoveSpeed);
            _points.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (_points.Count > Mathf.Round(RecordTime / Time.fixedDeltaTime))
        {
            _points.RemoveAt(_points.Count - 1);
        }
        var movement = GetMovement();
        movement = Vector3.ClampMagnitude(movement, 1);
        _points.Insert(0, new Point(transform.position, transform.rotation, movement.magnitude));
    }

    private Vector3 GetMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0, vertical);

        return movement;
    }

//     void Rewind()
// {
//     if (_points.Count > 1)
//     {
//         Point point1 = _points[0];
//         Point point2 = _points[1];
//         float duration = Vector3.Distance(point1.Position, point2.Position) / 0.1f; // rewindSpeed, rewind hızınız olmalıdır.
//         DOTween.To(() => transform.position, x => transform.position = x, point2.Position, duration);
//         _points.RemoveAt(0);
//     }
//     else
//     {
//         StopRewind();
//     }
// }

// void Record()
// {
//     if(_points.Count == 0)
//         _points.Insert(0, new Point(transform.position, transform.rotation));

//     if (Time.time - lastRecordTime > recordInterval)
//     {
//         if (_points.Count > 25)
//         {
//             _points.RemoveAt(_points.Count - 1);
//         }

//         _points.Insert(0, new Point(transform.position, transform.rotation));
//         lastRecordTime = Time.time;
//     }
// }
}

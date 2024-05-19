using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class TimeRewinder : MonoBehaviour
{
    public bool IsRewinding = false;
    public float RecordTime = 3f;
    public bool IsNpc = false;
    Animator _anim;
    public List<Point> _points;
    Rigidbody _rb;
    NavMeshAgent _navMeshAgent;

    void Start()
    {
        if(IsNpc)
            _navMeshAgent = GetComponent<NavMeshAgent>();
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
            Camera.main.transform.DOShakeRotation(0.1f, 0.3f, 1, 1, false);
        }

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
        Debug.Log(Time.timeScale);
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
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
            _anim.SetFloat("Speed", point.MoveSpeed);
            
            if(IsNpc && GetComponent<NPCState>().IsDead)
            {
                GetComponent<NPCState>().Rewind();
            }

            var rewindSound = GameObject.Find("RewindSound");
            if(rewindSound == null)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.RewindSound, "RewindSound", 0.1f);
            }

            UIManager.Instance.ClosePanel(UIManager.Instance.FailedMissionPanel);

            _points.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        var rewindSound = GameObject.Find("RewindSound");
        if(rewindSound != null)
        {
           Destroy(rewindSound);
        }

        if (_points.Count > Mathf.Round(RecordTime / Time.fixedDeltaTime))
        {
            _points.RemoveAt(_points.Count - 1);
        }
        
        
        if(IsNpc)
        {
            if(Time.timeScale < 0.11f)
            {
                return;
            }
            _points.Insert(0, new Point(transform.position, transform.rotation, _navMeshAgent.velocity.magnitude));
        }
        else
        {
            var movement = GetMovement();
            movement = Vector3.ClampMagnitude(movement, 1);
            _points.Insert(0, new Point(transform.position, transform.rotation, movement.magnitude));
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

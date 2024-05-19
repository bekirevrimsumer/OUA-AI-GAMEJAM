using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float MoveSpeed = 3f;
    
    private bool _isWandering = false;
    private Vector2 _xRange1 = new Vector2(183, 199);
    private Vector2 _zRange1 = new Vector2(215, 243);
    private Vector2 _xRange2 = new Vector2(207, 236);
    private Vector2 _zRange2 = new Vector2(208, 218);
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private TimeRewinder _timeRewinder;
    private Coroutine _wanderCoroutine;
    private NPCState _npcState;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _timeRewinder = GetComponent<TimeRewinder>();
        _wanderCoroutine = StartCoroutine(Wander());
        _npcState = GetComponent<NPCState>();
    }

    private void Update() 
    {
        HandleNPCState();
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);

        if (!_isWandering && !_timeRewinder.IsRewinding)
        {
            _wanderCoroutine = StartCoroutine(Wander());
        }

        if(_timeRewinder.IsRewinding && _wanderCoroutine != null)
        {
            _isWandering = false;
            StopCoroutine(_wanderCoroutine);
            _wanderCoroutine = null;
        }
    }

    private void HandleNPCState()
    {
        if(_npcState.IsDead)
        {
            _navMeshAgent.isStopped = true;
            StopCoroutine(_wanderCoroutine);
        }
        else
        {
            _navMeshAgent.isStopped = false;
        }
    }

    private Vector3 RandomPoint()
    {
        var random = Random.Range(0, 2);
        return random == 0 ? GenerateRandomPoint(_xRange1, _zRange1) : GenerateRandomPoint(_xRange2, _zRange2);
    }

    private Vector3 GenerateRandomPoint(Vector2 xRange, Vector2 zRange)
    {
        float x = Random.Range(xRange.x, xRange.y);
        float z = Random.Range(zRange.x, zRange.y);
        return new Vector3(x, 0, z);
    }

    IEnumerator Wander()
    {
        _isWandering = true;
        int walkWait = Random.Range(1, 5);
        int walkTime = Random.Range(1, 6);

        yield return new WaitForSeconds(walkWait);
        _navMeshAgent.SetDestination(RandomPoint());
        yield return new WaitForSeconds(walkTime);
        yield return new WaitForSeconds(walkWait);
        _isWandering = false;
    }
}
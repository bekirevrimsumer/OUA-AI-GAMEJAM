using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float MoveSpeed = 3f;
    
    private bool _isWandering = false;
    private float _minX = -33, _maxX = 11, _minZ = -27, _maxZ = 18;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    TimeRewinder _timeRewinder;
    Coroutine _wanderCoroutine;
    NPCState _npcState;

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
        if(_npcState.IsDead)
        {
            _navMeshAgent.isStopped = true;
            StopCoroutine(_wanderCoroutine);
            return;
        }
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

    private Vector3 RandomPoint()
    {
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        Vector3 randomPoint = new Vector3(x, 0, z);
        return randomPoint;
    }

    IEnumerator Wander()
    {
        if(_npcState.IsDead)
            yield break;

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

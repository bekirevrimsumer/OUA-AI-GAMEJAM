using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float MoveSpeed = 3f;
    
    private bool _isWandering = false;
    private float _minX = 183, _maxX = 199, _minZ = 215, _maxZ = 243;
    private float _minX2 = 207, _maxX2 = 236, _minZ2 = 208, _maxZ2 = 218;
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
        var random = Random.Range(0, 2);
        if(random == 0)
        {
            float x = Random.Range(_minX, _maxX);
            float z = Random.Range(_minZ, _maxZ);
            Vector3 randomPoint = new Vector3(x, 0, z);
            return randomPoint;
        }
        else
        {
            float x = Random.Range(_minX2, _maxX2);
            float z = Random.Range(_minZ2, _maxZ2);
            Vector3 randomPoint = new Vector3(x, 0, z);
            return randomPoint;
        }
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

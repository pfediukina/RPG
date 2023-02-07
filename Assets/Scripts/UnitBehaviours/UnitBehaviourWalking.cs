using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviourWalking : IUnitBehaviour
{
    private Unit _unit;
    private NavMeshAgent _agent;
    private Vector3 _movePoint;
    private Animator _animator;
    private Vector3 _endRotation;
    private bool _rotateAtEnd = false;

    public UnitBehaviourWalking(Unit unit) {
        _unit = unit;
        _agent = _unit.GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _animator = _unit.GetComponentInChildren<Animator>();        
    }

    public void Enter() 
    {
        _agent.stoppingDistance = 0;
//        Debug.Log(_movePoint);
        _agent.SetDestination(_movePoint);
        _agent.speed = _unit.UnitInfo.speed;
        PlayAnimation();
    }

    public void Exit() {
        _agent.velocity = Vector3.zero;
        if(_agent != null)
            _agent.SetDestination(_unit.transform.position);

        if(_rotateAtEnd)
        {
            _unit.transform.eulerAngles = _endRotation;
            _rotateAtEnd = false;
        }
    }

    public void Update() 
    {
        if(_agent.remainingDistance <= _agent.stoppingDistance) _unit.SetBehaviourIdle();

        _unit.transform.LookAt(new Vector3(_movePoint.x, _unit.transform.position.y, _movePoint.z));
    }

    public void SetMovePoint(Vector3 point) {
        _movePoint = point;
    }

    public void PlayAnimation()
    {
        if(_animator != null)
            _animator.Play("Walking");
    }

    public void SetEndRotation(Vector3 rotation)
    {
        _endRotation = rotation;
        _rotateAtEnd = true;
    }
}

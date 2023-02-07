using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviourFollow : IUnitBehaviour
{
    private Unit _unit;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private MonoBehaviour _followed;
    private float _stoppingDistance;
    private bool _attack;

    public UnitBehaviourFollow(Unit unit) {
        this._unit = unit;
        _agent = _unit.GetComponent<NavMeshAgent>();
        _animator = _unit.GetComponentInChildren<Animator>();        
    }

    public void Enter()
    {
        _agent.stoppingDistance = _stoppingDistance;
    }

    public void Exit() {}

    public void Update()
    {
        if(_animator != null)
            PlayAnimation();

        _agent.SetDestination(_followed.transform.position);
        _unit.transform.LookAt(new Vector3(_followed.transform.position.x, _unit.transform.position.y, _followed.transform.position.z));

        if(_agent.remainingDistance <= _agent.stoppingDistance && _attack)
        {
            _unit.SetBehaviourAttack(_followed.GetComponent<Unit>());
        }
    }

    public void PlayAnimation()
    {
        if(_animator == null) return;

        var currentAnimation = _animator.GetCurrentAnimatorClipInfo(0);
        if(currentAnimation == null || currentAnimation.Length == 0) return;

        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            if(currentAnimation[0].clip.name != "Idle")
                _animator.CrossFade("Idle", 0.3f);
        }
        else
        {
            if(currentAnimation[0].clip.name != "Walking")
                _animator.Play("Walking");
        }
    }

    public void SetFollowRange(float distance)
    {
        _stoppingDistance = distance >= 0 ? distance : 0f;
    }

    public void SetFollowObject(MonoBehaviour followed, bool attack = false)
    {
        _followed = followed;
        _attack = attack;
    }
}
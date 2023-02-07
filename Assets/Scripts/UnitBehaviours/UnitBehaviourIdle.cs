using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UnitBehaviourIdle : IUnitBehaviour
{
    private Unit _unit;
    private Animator _animator;
    
    public UnitBehaviourIdle(Unit unit) {
        this._unit = unit;
        _animator = _unit.GetComponentInChildren<Animator>();
    }

    public void Enter()
    {
        PlayAnimation();
    }

    public void Exit() {}

    public void PlayAnimation()
    {
        if(_animator != null)
            _animator.CrossFade("Idle", 0.2f);
    }

    public void Update() {}
}
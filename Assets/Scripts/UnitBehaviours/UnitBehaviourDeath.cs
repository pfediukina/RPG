using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UnitBehaviourDeath : IUnitBehaviour
{
    private Unit _unit;
    private Animator _animator;
    
    public UnitBehaviourDeath(Unit unit) 
    {
        _unit = unit;
        _animator = _unit.GetComponentInChildren<Animator>();
        SetDeathAnimationEvent();
    }

    public void Enter()
    {
        PlayAnimation();
    }

    public void Exit() {}
    
    public void Update() {}

    public void PlayAnimation()
    {
        if(_animator != null)
            _animator.Play("Death");
    }

    public void Death()
    {
        _unit.GetComponent<Collider>().enabled = false;
        if(_unit is not Player)
            DestroyAfterDeathAsync();
    }

    private void SetDeathAnimationEvent()
    {
        var events = _unit.GetComponentInChildren<UnitAnimationEvents>();
        if(events != null)
        {
            events.deathEvent = Death;
        }
    }

    private async void DestroyAfterDeathAsync()
    {
        await WaitTimeAfterDeath();
        if(_unit  != null)
            GameObject.Destroy(_unit.gameObject);
    }

    private async Task WaitTimeAfterDeath()
    {
        await Task.Delay(5000);  
    }

}
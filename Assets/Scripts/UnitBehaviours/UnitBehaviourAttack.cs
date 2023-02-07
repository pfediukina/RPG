using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class UnitBehaviourAttack : IUnitBehaviour
{
    private Unit _unit;
    private Unit _target;
    private Animator _animator;

    private float _attackAnimationSpeed = 0f;//2f;
    private string _attackAnimationName = "Attack";
    private bool _canAttack = true;
    private float _attackRange;

    public UnitBehaviourAttack(Unit unit) {
        this._unit = unit;
        _animator = _unit.GetComponentInChildren<Animator>();
        SetDamageAnimationEvent();

        var attackClip = _animator.runtimeAnimatorController.animationClips.Where(p => p.name == _attackAnimationName).FirstOrDefault();
        _attackAnimationSpeed = attackClip.length + 0.1f;
    }

    public void Enter() 
    {
        _attackRange = _unit.UnitInfo.attackRange;

        var collider = _target.GetComponent<CapsuleCollider>();
        if(collider != null)
            _attackRange += collider.radius;
        
        _animator.Play("Idle", 0);
        Attack();
    }

    public void Exit() { }

    public void Update()
    {
        if(_target == null || _target.behaviourCurrent is UnitBehaviourDeath)
        {
            _unit.SetBehaviourIdle();
            if(_unit is Player)
                (_unit as Player).SetCombat(false);

            if(_target is Enemy && MathExtension.IsBetweenRange(_unit.LevelSystem.currentLevel - _target.LevelSystem.currentLevel, -3, 3))
                _unit.LevelSystem.AddExperience((_target as Enemy).EnemyInfo.givingExp);
        }
        else
        {
            if(Vector3.Distance(_unit.transform.position, _target.transform.position) > _attackRange)
            {
                _unit.SetBehaviourFollow(_target, _unit.UnitInfo.attackRange, true);
            }
            _unit.transform.LookAt(new Vector3(_target.transform.position.x, _unit.transform.position.y, _target.transform.position.z));
            Attack();
        }
    }

    public void PlayAnimation()
    {
        if(_animator == null) return;
        
        _animator.Play(_attackAnimationName, 0);
        _animator.SetFloat("AttackSpeed", _attackAnimationSpeed / _unit.UnitInfo.attackSpeed);
    }

    public void SetAttackUnit(Unit target)
    {
        _canAttack = true;
        _target = target;
    }

    public void GiveDamage()
    {
        var resist = _target.LevelSystem.currentLevel - _unit.LevelSystem.currentLevel;
        _target.HealthSystem.GetDamageFromUnit(_unit.UnitInfo.damage - resist, _unit);
    }

//====================================

    private async void Attack()
    {
        if(_canAttack)
        {
            _canAttack = false;
            PlayAnimation();
            await ResetAttackAsync();
            _canAttack = true;
        }
    }

    private async Task ResetAttackAsync()
    {
        await Task.Delay((int)(_unit.UnitInfo.attackSpeed * 1000));  
    }

    private void SetDamageAnimationEvent()
    {
        var events = _unit.GetComponentInChildren<UnitAnimationEvents>();
        if(events != null)
        {
            events.damageEvent = GiveDamage;
        }
    }
}
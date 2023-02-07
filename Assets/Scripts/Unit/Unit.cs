using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public LevelSystem LevelSystem { get => _levelSystem; }
    public IUnitBehaviour behaviourCurrent { get => _behaviourCurrent;}
    public UnitInformation UnitInfo { get => _unitInfo; }
    public HealthSystem HealthSystem { get => _healthSystem; }
    public UnitAnimationSounds UnitSounds { get => _sounds; }

    [Header("UNIT")]
    [Header("Info")]
    [SerializeField] private UnitInformation _unitInfo;
    [SerializeField] protected Color _unitHealthColor;

    [Header("Components")]
    [SerializeField] protected HealthBar _healthBar;
    [SerializeField] protected UnitAnimationSounds _sounds;


    protected LevelSystem _levelSystem;
    protected Dictionary<Type, IUnitBehaviour> _behavioursMap;
    protected IUnitBehaviour _behaviourCurrent;
    protected HealthSystem _healthSystem;

// MONOBEHAVIOUR =============
    void Start()
    {
        InitUnit();
    }

    void Update()
    {
        UpdateBehaviour();
    }

//======================================

    public virtual void SetBehaviourIdle()
    {
        SetBehaviour(GetBehaviour<UnitBehaviourIdle>());
    }

    public virtual void SetBehaviourWalking(Vector3 position)
    {
        var behaviour = (UnitBehaviourWalking)GetBehaviour<UnitBehaviourWalking>();
        behaviour.SetMovePoint(position);
        SetBehaviour(behaviour);
    }

    public virtual void SetBehaviourWalking(Vector3 position, Vector3 rotation)
    {
        var behaviour = (UnitBehaviourWalking)GetBehaviour<UnitBehaviourWalking>();
        behaviour.SetMovePoint(position);
        behaviour.SetEndRotation(rotation);
        SetBehaviour(behaviour);
    }

    public virtual void SetBehaviourAttack(Unit target)
    {
        var behaviour = (UnitBehaviourAttack)GetBehaviour<UnitBehaviourAttack>();
        behaviour.SetAttackUnit(target);
        SetBehaviour(behaviour);
    }

    public virtual void SetBehaviourFollow(MonoBehaviour followed, float distance = 0f, bool attackObject = false)
    {
        float trueDistance = distance;
        var collider = followed.GetComponent<CapsuleCollider>();
        if(collider != null)
            trueDistance += collider.radius;

        var behaviour = (UnitBehaviourFollow)GetBehaviour<UnitBehaviourFollow>();
        behaviour.SetFollowRange(trueDistance);
        behaviour.SetFollowObject(followed, attackObject);
        SetBehaviour(behaviour);
    }

    public virtual void SetBehaviourDeath()
    {
        SetBehaviour(GetBehaviour<UnitBehaviourDeath>());
    }

//======================================
    protected virtual void InitUnit()
    {
        gameObject.tag = "Unit";
        
        InitLevelSystem();
        InitHealthSystem();
        InitBehaviours();
        SetBehaviourByDefault();
    }

    protected virtual void InitHealthSystem()
    {
        _healthSystem = new HealthSystem(this);
        _healthSystem.healthBar = _healthBar;
        _healthSystem.InitHealthBar(_unitHealthColor, _levelSystem.currentLevel);
    }

    protected virtual void InitLevelSystem()
    {
        _levelSystem = new LevelSystem(this);
        _levelSystem.canReceiveExp = false;
    }

    protected void UpdateBehaviour()
    {
        if(_behaviourCurrent != null)
            _behaviourCurrent.Update();
    }

//======================================

    private void InitBehaviours()
    {
        _behavioursMap = new Dictionary<Type, IUnitBehaviour>();

        _behavioursMap[typeof(UnitBehaviourIdle)] = new UnitBehaviourIdle(this);
        _behavioursMap[typeof(UnitBehaviourWalking)] = new UnitBehaviourWalking(this);
        _behavioursMap[typeof(UnitBehaviourFollow)] = new UnitBehaviourFollow(this);
        _behavioursMap[typeof(UnitBehaviourAttack)] = new UnitBehaviourAttack(this);
        _behavioursMap[typeof(UnitBehaviourDeath)] = new UnitBehaviourDeath(this);
    }

    private void SetBehaviour(IUnitBehaviour newBehaviour)
    {
        if(_behaviourCurrent != null)
            _behaviourCurrent.Exit();

        if(_behaviourCurrent is UnitBehaviourDeath) return;

        _behaviourCurrent = newBehaviour;
        _behaviourCurrent.Enter();
    }

    private void SetBehaviourByDefault()
    {
        var behaviourByDefault = GetBehaviour<UnitBehaviourIdle>();
        SetBehaviour(behaviourByDefault);
    }

    private IUnitBehaviour GetBehaviour<T>() where T : IUnitBehaviour
    {
        var type = typeof(T);
        return _behavioursMap[type];
    }
}

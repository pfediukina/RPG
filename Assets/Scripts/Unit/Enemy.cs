using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    public EnemyInformation EnemyInfo { get => _enemyInfo; }
    
    [Header("ENEMY")]
    [SerializeField] private EnemyInformation _enemyInfo;
    [SerializeField] private TriggerArea _triggerArea;
    [SerializeField] private float _pursuitRange;
    [SerializeField] private int _levelOverride = -1;

    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private Player _target;
    private bool _isFollowing = false;
    private bool _isReturning = false;

// UNIT ===================
    protected override void InitHealthSystem()
    {
        base.InitHealthSystem();
        HealthSystem.damageTrigger = CounterAttack;
    }

// MONOBEHAVIOUR =========================

    private void Start()
    {
        InitUnit();
    }

    void Update() 
    {
        if(behaviourCurrent is UnitBehaviourIdle && _isReturning)
        {
            _isReturning = false;
            HealthSystem.AddHealth(UnitInfo.maxHealth);
        }

        UpdateBehaviour();
        CheckEnemyReset();
    }

//===============================

    public void CounterAttack(Unit unit)
    {
        if(_isFollowing || _isReturning) return;
        if(unit.UnitInfo.threat != EThreat.PLAYER || unit == null) return;
        _isFollowing = true;
        SetBehaviourFollow(unit, UnitInfo.attackRange, true);
        
        _target = unit as Player;
        _target.SetCombat(true);
    }

    public void OnDetectionArea(Collider other)
    {
        if(_isFollowing || _isReturning) return;

        if(other.tag == "Unit")
        {
            var unit = other.GetComponent<Unit>();
            CounterAttack(unit);
        }
    }

    //===============================

    protected override void InitUnit()
    {
        if(_triggerArea != null)
            _triggerArea.CreateArea(EnemyInfo.detectionDistance, OnDetectionArea);

        _pursuitRange = EnemyInfo.pursuitRange;
        _startPosition = transform.position;
        _startRotation = transform.eulerAngles;
        base.InitUnit();

        if(_levelOverride != -1)
            LevelSystem.SetLevel(_levelOverride);
    }

    private void CheckEnemyReset()
    {
        if(!_isFollowing) return;
        if(_target.behaviourCurrent is UnitBehaviourDeath)
            ResetTarget();

        if(Vector3.Distance(_startPosition, transform.position) > _pursuitRange)
        {
            _target.SetCombat(false);
            ResetTarget();
        }
    }

    private void ResetTarget()
    {
        SetBehaviourWalking(_startPosition, _startRotation);
        HealthSystem.AddHealth(UnitInfo.maxHealth);
        _isFollowing = false;
        _isReturning = true;
    }
}
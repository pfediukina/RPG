using UnityEngine;
using System;

public class HealthSystem
{
    public float CurrentHealth { get => _healthCurrent ;}

    public Action<Unit> damageTrigger = null;

    public HealthBar healthBar;

    private Unit _unit;
    private Animator _animator;

    private float _healthCurrent;

//======================

    public HealthSystem(Unit owner)
    {
        _unit = owner;
        _healthCurrent = _unit.UnitInfo.maxHealth;
    }

    public void GetDamageFromUnit(float damage, Unit enemy)
    {
        _healthCurrent -= damage;
        CheckDeath();
        UpdateHealthBar();

        if(damageTrigger != null)
            damageTrigger.Invoke(enemy);

        if(_unit.UnitSounds != null)
            _unit.UnitSounds.PlayHurt();
    }

    public void AddHealth(float health)
    {
        _healthCurrent += health;
        _healthCurrent = Mathf.Clamp(_healthCurrent, 0, _unit.UnitInfo.maxHealth);
        UpdateHealthBar();
    }

    public void InitHealthBar(Color healthColor, int level)
    {
        if(healthBar == null) return;
        healthBar.ChangeColor(healthColor);
        healthBar.SetHealth(1f);
        healthBar.SetLevel(level);
    }

    public void SetLevel(int level)
    {
        if(healthBar == null) return;
        healthBar.SetLevel(level);
    }

// ===================================

    private void CheckDeath()
    {
        if(_healthCurrent <= 0)
        {
            if(healthBar != null)
                healthBar.gameObject.SetActive(false);
            _unit.SetBehaviourDeath();
        }
    }

    private void UpdateHealthBar()
    {
        if(healthBar == null) return;
        healthBar.SetHealth(_healthCurrent / _unit.UnitInfo.maxHealth);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private Unit _unit;

    public int currentLevel;
    public float currentExp = 0;
    public float expToNextLevel = 200;
    public bool canReceiveExp;

    private float _addedExpPerLevel = 40;

//=========================

    public LevelSystem(Unit unit)
    {
        _unit = unit;
        currentLevel = _unit.UnitInfo.startLevel;
    }

    public void AddExperience(float exp)
    {
        if(!canReceiveExp) return;
        if(currentLevel >= GameManager.GetInstance.maxLevel) return;

        currentExp += exp;
        if(currentExp >= expToNextLevel)
            LevelUp();

        if(_unit is Player)
            (_unit as Player).UpdateExperience();
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
        _unit.HealthSystem.SetLevel(currentLevel);
    }

    private void LevelUp()
    {
        currentExp %= expToNextLevel;
        currentLevel++;
        expToNextLevel = CalculateExperience();
    }

    private float CalculateExperience()
    {
        float exp;
        exp = _addedExpPerLevel * currentLevel + 200;
        return exp;
    }
}

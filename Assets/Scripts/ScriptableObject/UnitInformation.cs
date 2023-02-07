using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Informations/Unit Information", order = 0)]
public class UnitInformation : ScriptableObject 
{
    [Header("General")]
    public EThreat threat;    
    public int startLevel;

    [Header("Movement")]
    public float speed;

    [Header("Attack")]
    public float attackSpeed;
    public float attackRange;
    public float damage;

    [Header("Health")]
    public float maxHealth;
}
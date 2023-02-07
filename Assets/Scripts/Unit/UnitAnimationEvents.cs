using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationEvents : MonoBehaviour
{
    [HideInInspector] public Action damageEvent; 
    [HideInInspector] public Action deathEvent; 

    [HideInInspector] public Unit owner;

    public void GiveDamageEvent()
    {
        damageEvent.Invoke();
    }

    public void DeathEvent()
    {
        deathEvent.Invoke();
    }
}
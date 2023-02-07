using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Unit
{
    [Header("FRIEND")]
    public bool canTalk;

    void Start()
    {
        InitUnit();
    }

    protected override void InitUnit()
    {
        base.InitUnit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private CapsuleCollider _area;
    private Action<Collider> _exec;

    void Start()
    {
        _area = GetComponent<CapsuleCollider>();
        _area.isTrigger = true;
    }

    public void CreateArea(float radius, Action<Collider> areaTrigger)
    {
        _area.radius = radius;
        _exec = areaTrigger;
    }

    void OnTriggerEnter(Collider other)
    {
        _exec.Invoke(other);
    }
}

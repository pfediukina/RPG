using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private float _lifeTime;

    public void CreatePoint(Vector3 position, float lifeTime)
    {
        transform.position = position;
        _lifeTime = lifeTime;
    }

    private IEnumerator DestroyPointer()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);    
    }
}
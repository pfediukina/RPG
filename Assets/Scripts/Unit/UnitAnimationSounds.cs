using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationSounds : MonoBehaviour
{
    [SerializeField] public AudioSource _unitSounds;

    [SerializeField] public AudioClip _unitStep;
    [SerializeField] public AudioClip _unitAttack;
    [SerializeField] public AudioClip _unitDeath;
    [SerializeField] public AudioClip _unitHurt;

    public void PlayStep()
    {
        if(_unitStep != null)
        _unitSounds.Stop();
        _unitSounds.PlayOneShot(_unitStep);
    }

    public void PlayAttack()
    {
        if(_unitAttack != null)
        _unitSounds.Stop();
        _unitSounds.PlayOneShot(_unitAttack);
    }

    public void PlayDeath()
    {
        if(_unitDeath != null)
        _unitSounds.Stop();
        _unitSounds.PlayOneShot(_unitDeath);
    }

    public void PlayHurt()
    {
        if(_unitHurt != null)
        _unitSounds.PlayOneShot(_unitHurt);
    }
}

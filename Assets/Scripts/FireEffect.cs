using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem playerFirebulletEffect;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerFirebulletEffect.Play();
        }
    }
}

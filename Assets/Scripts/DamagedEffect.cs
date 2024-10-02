using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem playerDamgedEffect;

    private void Awake()
    {
        playerDamgedEffect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MonsterBullet"))
        {
            playerDamgedEffect.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenDamageEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem monsterDamagedEffect;
    [SerializeField] AudioSource takenDamageSound;

    private void OnCollisionEnter(Collision collision)
    {
        takenDamageSound.Play();
        monsterDamagedEffect.Play();
    }
}

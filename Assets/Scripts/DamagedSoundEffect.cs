using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedSoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource damagedSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterBullet"))
        {
            damagedSound.Play();
        }
    }
}

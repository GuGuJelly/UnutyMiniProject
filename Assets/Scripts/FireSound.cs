using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    [SerializeField] AudioSource fireSound;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireSound.Play(); 
        }
    }
}

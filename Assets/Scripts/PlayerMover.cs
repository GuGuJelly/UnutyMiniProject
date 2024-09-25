using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        Rotation();
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(0, 0, z);

        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Rotate(Vector3.up,(-1)* rotateSpeed * Time.deltaTime);
        }
        
    }
}

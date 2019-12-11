using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public float MoveSpeed = 1f;

    void UpdateMoveControl()
    {
        float xmove = Input.GetAxis("Horizontal");
        float ymove = Input.GetAxis("Vertical");

        Vector3 temppos = transform.position;
        temppos.x += xmove * MoveSpeed;
        temppos.z += ymove * MoveSpeed;

        transform.position = temppos;

    }


    void Update()
    {
        UpdateMoveControl();


    }
}

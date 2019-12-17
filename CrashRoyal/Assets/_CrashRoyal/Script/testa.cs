using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testa : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("테스트 A : " + other.name );
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

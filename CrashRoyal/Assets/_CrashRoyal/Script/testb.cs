using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("테스트 B : " + other.name);
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

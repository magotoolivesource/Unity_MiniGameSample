using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    bool m_ISInit = true;
    public Vector3 InitPos = new Vector3();
    public void SetInitPos()
    {
        InitPos = MapGenerator.GetI.GetInitPlayerPos();
        transform.position = InitPos;

        GetComponent<Rigidbody>().position = InitPos;

        //CharacterController colltrol = GetComponent<CharacterController>();
    }


    void Start()
    {
        SetInitPos();
    }

    void Update()
    {
        if(m_ISInit)
        {
            m_ISInit = false;
            SetInitPos();
        }
    }
}

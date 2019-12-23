using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    bool m_ISInit = true;
    public Vector3 InitPos = new Vector3();
    public void SetInitPos()
    {
        //CharacterController colltrol = GetComponent<CharacterController>();

        InitPos = MapGenerator.GetI.GetInitPlayerPos();
        transform.position = InitPos;

        GetComponent<Rigidbody>().position = InitPos;

        //
        //colltrol.attachedRigidbody.position = InitPos;
        CharacterController colltrol = GetComponent<CharacterController>();
        colltrol.enabled = true;
    }


    void Start()
    {
        SetInitPos();
    }

    void Update()
    {
        //CharacterController colltrol = GetComponent<CharacterController>();
        //if(m_ISInit  )
        //{
        //    m_ISInit = false;
        //    SetInitPos();
        //}
        

        //if(m_ISInit || true)
        //{
        //    m_ISInit = false;
        //    SetInitPos();
        //}
    }
}

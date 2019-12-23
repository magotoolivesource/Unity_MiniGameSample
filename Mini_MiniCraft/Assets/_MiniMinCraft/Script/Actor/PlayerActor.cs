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


    Camera m_Camera = null;
    void Start()
    {
        m_Camera = GetComponentInChildren<Camera>();
        SetInitPos();
    }

    void RightClick()
    {
        Vector3 forward = m_Camera.transform.forward;

        RaycastHit hitinfo;
        LayerMask mask = LayerMask.GetMask("Land", "Item", "Enemy");

        if( Physics.Raycast(m_Camera.transform.position
            , forward
            , out hitinfo
            , 3f
            , mask ) )
        {
            BaseBlock block = hitinfo.transform.GetComponent<BaseBlock>();

            if( block )
            {
                GameObject.Destroy(block.gameObject);
            }

        }
        

    }
    void LeftClick()
    {

    }
    void ClickEvent()
    {
        if( Input.GetMouseButtonDown(0))
        {
            RightClick();
        }

        if(Input.GetMouseButtonDown(1))
        {
            LeftClick();
        }

    }

    void Update()
    {
        ClickEvent();


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

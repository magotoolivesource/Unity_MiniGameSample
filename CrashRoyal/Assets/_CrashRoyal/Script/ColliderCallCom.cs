using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderCallCom : MonoBehaviour
{
    // 선언
    public delegate void ActionCall<in T>(T obj);
    // 함수 포인터 변수
    protected ActionCall<BaseActor> m_CallFN = null;

    protected float m_Range = 0.1f;


    //public Button.ButtonClickedEvent clickevent = null;

    public void SetInitColliderCallFN( ActionCall<BaseActor> p_callfn
        , float p_range )
    {
        m_CallFN = p_callfn;
        m_Range = p_range;
        GetComponent<SphereCollider>().radius = m_Range;
    }

    protected void OnTriggerEnter(Collider other)
    {
        BaseActor actor = other.GetComponent<BaseActor>();

        if( actor )
        {
            if(m_CallFN != null)
                m_CallFN(actor);
        }

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

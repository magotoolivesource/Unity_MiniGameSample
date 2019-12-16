using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public Transform TargetTrans = null;
    public float AttackSpeed = 1f;

    [Header("[확인용]")]
    [SerializeField]
    protected NavMeshAgent m_LinkgAgent = null;
    [SerializeField]
    protected Transform m_AttackTrans = null;
    [SerializeField]
    protected float m_NextAttackSec = 0f;

    private void Awake()
    {
        m_LinkgAgent = GetComponent<NavMeshAgent>();
        m_LinkgAgent.speed = MoveSpeed;

    }


    

    private void OnTriggerEnter(Collider other)
    {
        // 충돌된 내용들 확인하기
        Debug.LogFormat("충돌 : {0}, {1}", other.name, this.name);
        if( other.tag == "Enemy")
        {
            m_AttackTrans = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(m_AttackTrans
            && m_AttackTrans.gameObject == other.gameObject)
        {
            m_AttackTrans = null;
        }
    }


    void UpdateTargetMove()
    {
        m_LinkgAgent.SetDestination(TargetTrans.position);
    }

    void UpdateAttackTarget()
    {
        if(m_AttackTrans
            && m_NextAttackSec <= Time.time )
        {
            m_NextAttackSec = Time.time + AttackSpeed;
            Debug.LogFormat("공격함 : {0} -> {1}", name, m_AttackTrans.name);


        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateTargetMove();
        UpdateAttackTarget();
    }
}

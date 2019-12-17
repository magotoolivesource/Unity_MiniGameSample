using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//public enum E_TargetType
//{
//    Actor = 0x0001,
//    Tower = 0x0002,

//    Max
//}

public enum E_CampType
{
    MyCamp = 0,
    Enemy,

    Max
}

public class BaseActor : MonoBehaviour
{
    
    public E_CampType CampType = E_CampType.MyCamp;

    //public int TargetType = (int)E_TargetType.Actor; 
    //                        & (int)E_TargetType.Tower;

    public float MoveSpeed = 1f;
    public BaseActor TargetActor = null;
    public float AttackSpeed = 1f;
    public float AttackVal = 10f;
    public float AttackRange = 1f;

        
    [Header("[확인용]")]
    [SerializeField]
    protected NavMeshAgent m_LinkgAgent = null;
    [SerializeField]
    protected Transform m_AttackTrans = null;
    [SerializeField]
    protected float m_NextAttackSec = 0f;

    public LayerMask CollistionMask;


    protected virtual void TestFN()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        // 충돌된 내용들 확인하기
        Debug.LogFormat("충돌 : {0}, {1}", other.name, this.name);

        BaseActor otheractor = other.GetComponent<BaseActor>();
        if( otheractor
            && !InGameManager.ISSameCampType(otheractor, this) )
        {
            if(m_AttackTrans == null )
            {
                TargetActor = otheractor;
                m_AttackTrans = other.transform;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (m_AttackTrans
            && m_AttackTrans.gameObject == other.gameObject)
        {
            m_AttackTrans = null;
        }


        ReSearchingTarget();
    }

    public void ReSearchingTarget()
    {
        if( m_AttackTrans == null)
        {

        }

        if(TargetActor == null)
        {
            LayerMask maskval = LayerMask.GetMask("Player", "Enemy");
            Collider[] colarray = Physics.OverlapSphere(transform.position
                , AttackRange
                , maskval );


            BaseActor tempactor = null;
            //BaseActor minlengthcollider = null;
            float minlength = float.MaxValue;
            float templength = 0f;
            foreach (var item in colarray)
            {
                templength = (item.transform.position - transform.position).magnitude;
                tempactor = item.GetComponent<BaseActor>();

                if ( templength < minlength)
                {
                    if( !InGameManager.ISSameCampType(tempactor, this) )
                    {
                        minlength = templength;
                        //minlengthcollider = tempactor;
                        TargetActor = tempactor;
                    }
                }
            }

            if(TargetActor == null)
            {
                SetNearTowerTarget();
            }

        }


    }

    protected void SetNearTowerTarget()
    {
        TargetActor = InGameManager.GetI.GetEnemyCampTower(CampType
                        , this.transform.position.x);
    }

    protected void UpdateTargetMove()
    {
        m_LinkgAgent.SetDestination(TargetActor.transform.position);
    }

    protected void UpdateAttackTarget()
    {
        if (m_AttackTrans
            && m_NextAttackSec <= Time.time)
        {
            m_NextAttackSec = Time.time + AttackSpeed;
            Debug.LogFormat("공격함 : {0} -> {1}", name, m_AttackTrans.name);


            ActorStat statcom = m_AttackTrans.GetComponent<ActorStat>();
            statcom.SetDamage(AttackVal);
        }
    }

    void UpdateReserchingTarget()
    {
        if(TargetActor == null)
            ReSearchingTarget();
    }


    void Awake()
    {
        m_LinkgAgent = GetComponent<NavMeshAgent>();
        m_LinkgAgent.speed = MoveSpeed;

        if(CampType == E_CampType.MyCamp)
        {
            tag = "Player";
        }
        else
        {
            tag = "Enemy";
        }


    }


    //[SerializeField]
    //protected InGameManager m_Manager = null;
    protected virtual void Start()
    {
        //m_Manager = GameObject.FindObjectOfType<InGameManager>();
        //TargetActor = m_Manager.GetEnemyCampTower(CampType
        //                , this.transform.position.x);

        SetNearTowerTarget();

    }

    protected virtual void Update()
    {
        UpdateReserchingTarget();

        UpdateTargetMove();
        UpdateAttackTarget();
        



    }
}

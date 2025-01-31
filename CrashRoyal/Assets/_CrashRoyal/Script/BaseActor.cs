﻿using System.Collections;
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

public enum E_AttackType
{
    Meel = 0,
    Range,
}

public enum E_AttackRangeType
{
    One,
    Multi,
}

[System.Serializable]
public struct AttackCom
{
    public AttackCom(float p_attackval = 10f)
    {
        AttackType = E_AttackType.Meel;
        AttRangeType = E_AttackRangeType.One;
        AttackVal = p_attackval;
        RangeAttackSpeed = 10f;

        RangeSize= 2f;
        RangeAttackMulti = 0.5f;
    }
        
    public E_AttackType AttackType;// = E_AttackType.Meel;
    public E_AttackRangeType AttRangeType;// 
    public float AttackVal;// = 10f;
    public float RangeAttackSpeed;// = 10f;

    // rangetype = multi
    public float RangeSize; // 충돌되는 범위
    public float RangeAttackMulti; // 공격 값에 퍼센트

}

public enum E_AnimationType
{
    Idle = 0,
    Attack,
    GetHit,
    IsMoving,
    IsDead,

    Max
}


public class BaseActor : MonoBehaviour
{
    
    public E_CampType CampType = E_CampType.MyCamp;

    //public int TargetType = (int)E_TargetType.Actor; 
    //                        & (int)E_TargetType.Tower;

    public float MoveSpeed = 1f;
    public BaseActor TargetActor = null;
    public BaseActor SerchingActor = null;
    public float AttackSpeed = 1f; // 공격간의 속도
    public float AttackRange = 1f;
    public float SerchingRange = 3f;


    public AttackCom AttackData = new AttackCom();
    //public E_AttackType AttackType = E_AttackType.Meel;
    //public E_AttackRangeType AttRangeType = E_AttackRangeType.One;
    //public float AttackVal = 10f;
    //public float RangeAttackSpeed = 10f;



    [Header("[확인용]")]
    [SerializeField]
    protected NavMeshAgent m_LinkgAgent = null;
    [SerializeField]
    protected BaseActor m_TargetAttackActor = null;
    [SerializeField]
    protected float m_NextAttackSec = 0f;

    public LayerMask CollistionMask;


    [SerializeField]
    protected Animator m_Animator = null;


    public void SetAnimation( E_AnimationType p_animationtype )
    {
        if(m_Animator == null)
            return;


        if( p_animationtype == E_AnimationType.Idle )
        {
            m_Animator.SetBool("IsMoving", false);
        }
        else if(p_animationtype == E_AnimationType.IsMoving)
        {
            m_Animator.SetBool("IsMoving", true);
        }
        else
        {
            m_Animator.SetTrigger(p_animationtype.ToString() );
        }
        

    }




    protected virtual void TestFN()
    {

    }


    public void InitSettingBaseActor(ActorTableData p_actortable
        , E_CampType p_camptype )
    {
        CampType = p_camptype;
        AttackData = p_actortable.AttackComData;

        InitSettingBaseActor();
    }
    
    public void SerchingTriggerEnter(BaseActor p_actor)
    {
        Debug.LogFormat("범위 충돌 : {0}, {1}", p_actor.name, this.name);
        BaseActor otheractor = p_actor;// other.GetComponent<BaseActor>();

        if (otheractor
            && this != p_actor
            && !InGameManager.ISSameCampType(otheractor, this) )
        {
            if(SerchingActor == null)
            {
                SerchingActor = otheractor;
                TargetActor = SerchingActor;
            }
        }
    }

    public void SerchingTriggerExit(BaseActor p_actor)
    {
        if(p_actor.gameObject == this.gameObject  )
        {
            SerchingActor = null;
            SetNearTowerTarget();
        }
    }

    public void AttackTriggerEnter(BaseActor p_actor)
    {
        Debug.LogFormat("공격 충돌 : {0}, {1}", p_actor.name, this.name);
        BaseActor otheractor = p_actor;// other.GetComponent<BaseActor>();
        if (otheractor
            && this != p_actor
            && !InGameManager.ISSameCampType(otheractor, this))
        {
            if (m_TargetAttackActor == null)
            {
                SerchingActor = otheractor;
                TargetActor = otheractor;
                m_TargetAttackActor = otheractor;

            }
        }
    }

    public void AttackTriggerExit(BaseActor p_actor)
    {
        if (m_TargetAttackActor
            && m_TargetAttackActor.gameObject == p_actor.gameObject)
        {
            //SerchingActor = null;
            m_TargetAttackActor = null;
        }

        ReSearchingTarget();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // 충돌된 내용들 확인하기
    //    Debug.LogFormat("충돌 : {0}, {1}", other.name, this.name);

    //    BaseActor otheractor = other.GetComponent<BaseActor>();
    //    if( otheractor
    //        && !InGameManager.ISSameCampType(otheractor, this) )
    //    {
    //        if(m_AttackTrans == null )
    //        {
    //            TargetActor = otheractor;
    //            m_AttackTrans = otheractor.transform;
    //        }
    //    }

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (m_AttackTrans
    //        && m_AttackTrans.gameObject == other.gameObject)
    //    {
    //        m_AttackTrans = null;
    //    }


    //    ReSearchingTarget();
    //}

    public void ReSearchingTarget()
    {
        if( m_TargetAttackActor == null)
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
        if(m_TargetAttackActor)
        {
            m_LinkgAgent.isStopped = true;
            //m_LinkgAgent.SetDestination(TargetActor.transform.position);

            SetAnimation(E_AnimationType.Idle);
        }
        else
        {
            m_LinkgAgent.isStopped = false;
            m_LinkgAgent.SetDestination(TargetActor.transform.position);
            SetAnimation(E_AnimationType.IsMoving);
        }

        


    }

    
    public virtual void SetAttackDamage()
    {
        if (AttackData.AttackType == E_AttackType.Range)
        {
            Debug.LogErrorFormat("공격 값 확인하기 ?? ");
            //ThrowingObj obj = GameObject.Instantiate<ThrowingObj>(m_CloneThrowingObj);
            //obj.SetInitThworing(m_TargetAttackActor, this);
        }
        else
        {
            ActorStat statcom = m_TargetAttackActor.GetComponent<ActorStat>();
            statcom.SetDamage(AttackData);
        }
    }

    protected virtual void SetAttack()
    {
        Debug.LogFormat("공격함 : {0} -> {1}", name, m_TargetAttackActor.name);
        SetAnimation(E_AnimationType.Attack);


        //if (AttackData.AttackType == E_AttackType.Range)
        //{
        //    Debug.LogErrorFormat("공격 값 확인하기 ?? ");
        //    //ThrowingObj obj = GameObject.Instantiate<ThrowingObj>(m_CloneThrowingObj);
        //    //obj.SetInitThworing(m_TargetAttackActor, this);
        //}
        //else
        //{
        //    ActorStat statcom = m_TargetAttackActor.GetComponent<ActorStat>();
        //    statcom.SetDamage(AttackData);
        //}

    }
    protected void UpdateAttackTarget()
    {
        if (m_TargetAttackActor
            && m_NextAttackSec <= Time.time)
        {
            m_NextAttackSec = Time.time + AttackSpeed;
            SetAttack();
        }
        else
        {
        }
    }

    void UpdateReserchingTarget()
    {
        if(TargetActor == null)
            ReSearchingTarget();
    }


    public ColliderCallCom m_AttackColliderCom = null;
    public ColliderCallCom m_SerchingColliderCom = null;


    [ContextMenu("[충돌체사이즈확인용]")]
    protected void InitSettingBaseActor()
    {
        m_LinkgAgent = GetComponent<NavMeshAgent>();
        m_LinkgAgent.speed = MoveSpeed;

        if (CampType == E_CampType.MyCamp)
        {
            tag = "Player";
        }
        else
        {
            tag = "Enemy";
        }

        m_AttackColliderCom.SetInitColliderCallFN(AttackTriggerEnter
            , AttackTriggerExit
            , AttackRange);
        m_SerchingColliderCom.SetInitColliderCallFN(SerchingTriggerEnter
            , SerchingTriggerExit
            , SerchingRange);

        m_Animator = GetComponentInChildren<Animator>();
    }

    void Awake()
    {
        InitSettingBaseActor();
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

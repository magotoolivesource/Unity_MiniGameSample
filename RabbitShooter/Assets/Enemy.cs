using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float AttackRange = 2.5f;
    public float AttackVal = 10;
    public float AttackDelay = 2f;


    float NextAttackSec = 0f;
    public void Attack()
    {
        if(m_AttackPlayer)
        {
            if(NextAttackSec <= Time.time )
            {
                NextAttackSec = Time.time + AttackDelay;
                m_AttackPlayer.SetDamage(AttackVal);
            }

            
            //Invoke("SetAttack", AttackDelay);
        }
            


    }



    public int HP = 100;

    public float DefanceVal = 1f;
    public void SetDamage( float p_attackval )
    {
        HP = (int)((float)HP - p_attackval);

        if( HP <= 0 )
        {
            GameObject.Destroy(gameObject);
        }
    }


    [Header("[확인용]")]
    [SerializeField]
    protected PlayerController m_PlayerTarget = null;

    NavMeshAgent m_Agent = null;
    void UpdateTargetFollow()
    {
        m_Agent.SetDestination( m_PlayerTarget.transform.position );

    }

    private void Awake()
    {
        m_PlayerTarget = GameObject.FindObjectOfType<PlayerController>();
        m_Agent = GetComponent<NavMeshAgent>();


        GetComponent<SphereCollider>().radius = AttackRange;
        GetComponent<SphereCollider>().isTrigger = true;

    }


    [SerializeField]
    PlayerController m_AttackPlayer = null;
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("누가 공격 : " + this.name + " : 플레이어 : " +  other.name );
        Debug.LogFormat("공격 : {0}, {1} ", name, other.name);

        m_AttackPlayer = other.transform.GetComponent<PlayerController>();
        //if(m_AttackPlayer)
        //{
        //    Invoke("SetAttack", 0);
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        if(m_AttackPlayer.gameObject == other.gameObject)
        {
            m_AttackPlayer = null;
        }
        
        
    }





    public void Init()
    {

    }
    void Start()
    {
        
    }

    void Update()
    {
        UpdateTargetFollow();
        Attack();

    }
}

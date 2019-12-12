using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
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


    }
}

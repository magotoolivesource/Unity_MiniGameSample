using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObj : MonoBehaviour
{

    protected BaseActor m_TargetActor = null;
    protected Vector3 m_TargetPos = Vector3.zero;

    protected float m_RangeSpeed = 1f;
    protected float m_AttackVal = 10;


    public void SetInitThworing( BaseActor p_target, BaseActor p_attakcer )
    {
        m_RangeSpeed = p_attakcer.RangeAttackSpeed;
        m_AttackVal = p_attakcer.AttackVal;
        m_TargetActor = p_target;
        m_TargetPos = m_TargetActor.transform.position;

        this.transform.position = p_attakcer.transform.position;

    }

    void Start()
    {
        
    }

    void Update()
    {
        if(m_TargetActor )
        {
            m_TargetPos = m_TargetActor.transform.position;
        }


        Vector3 targetnormal = (m_TargetPos - transform.position);
        float targetlength = targetnormal.magnitude;
        float minlength = m_RangeSpeed * Time.deltaTime;


        if(targetlength <= minlength)
        {
            if (m_TargetActor)
            {
                ActorStat statcom = m_TargetActor.GetComponent<ActorStat>();
                statcom.SetDamage(m_AttackVal);
            }
            GameObject.Destroy(gameObject);
        }
        else
        {
            targetnormal.Normalize();
            transform.Translate(targetnormal * minlength);
        }

        


    }
}

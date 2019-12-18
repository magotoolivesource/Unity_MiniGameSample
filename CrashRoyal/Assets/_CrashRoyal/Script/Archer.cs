using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Archer : BaseActor
{
    [Header("[아처용값들]")]
    public ThrowingObj m_CloneThrowingObj = null;


    protected override void SetAttack()
    {
        if (AttackData.AttackType == E_AttackType.Range)
        {
            ThrowingObj obj = GameObject.Instantiate<ThrowingObj>(m_CloneThrowingObj);
            obj.SetInitThworing(m_TargetAttackActor, this);
        }
        else
        {
            ActorStat statcom = m_TargetAttackActor.GetComponent<ActorStat>();
            statcom.SetDamage(AttackData);
        }



    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

    }
}

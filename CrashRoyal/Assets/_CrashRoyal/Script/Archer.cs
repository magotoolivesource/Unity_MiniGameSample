using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Archer : BaseActor
{
    public ThrowingObj m_CloneThrowingObj = null;


    protected override void SetAttack()
    {
        //GameObject.Instantiate();
        //  m_TargetAttackActor ;

        ThrowingObj obj = GameObject.Instantiate<ThrowingObj>(m_CloneThrowingObj);

        obj.SetInitThworing(m_TargetAttackActor, this);

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

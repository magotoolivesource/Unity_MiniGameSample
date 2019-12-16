using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : BaseActor
{




    protected override void Start()
    {

        //m_LinkgAgent.enabled = false;
        //m_LinkgAgent.speed = 0f;
        base.Start();

    }

    protected override void Update()
    {
        //base.Update();
        UpdateAttackTarget();

    }
}

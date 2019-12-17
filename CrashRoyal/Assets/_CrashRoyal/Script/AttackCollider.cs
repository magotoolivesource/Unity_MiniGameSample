using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("공격 충돌 : {0}, {1}", other.name, this.name);

        BaseActor targetactor  = other.GetComponent<BaseActor>();
        BaseActor baseactor = GetComponentInParent<BaseActor>();
        baseactor.AttackTriggerEnter(targetactor);

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

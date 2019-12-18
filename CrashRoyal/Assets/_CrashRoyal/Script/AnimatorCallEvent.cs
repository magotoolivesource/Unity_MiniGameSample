using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCallEvent : MonoBehaviour
{

    public void SetAnimationCall(object p_calltype )
    {

    }

    BaseActor m_ParentBaseActor = null;

    public void DealDamage()
    {
        Debug.Log("애니메이션 콜");
        m_ParentBaseActor.SetAttackDamage();


    }

    private void Awake()
    {
        m_ParentBaseActor = GetComponentInParent<BaseActor>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

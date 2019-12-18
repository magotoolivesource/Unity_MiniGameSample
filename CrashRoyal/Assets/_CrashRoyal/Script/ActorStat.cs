using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStat : MonoBehaviour
{
    public int HP = 100;
    //public float MoveSpeed = 1f;
    //public float AttackVal = 10;


    public void SetDamage( AttackCom p_attdata )
    {
        
        if ( p_attdata.AttRangeType == E_AttackRangeType.Multi )
        {
            
        }


        HP = HP - (int)p_attdata.AttackVal;

        if( HP <= 0)
        {
            Debug.LogFormat("캐릭터 HP 제로 : ", this.name );
            GetComponent<BaseActor>().SetAnimation(E_AnimationType.IsDead);
            GameObject.Destroy(gameObject, 2f);
        }
        else
        {
            GetComponent<BaseActor>().SetAnimation(E_AnimationType.GetHit);
        }

        

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

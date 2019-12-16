using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStat : MonoBehaviour
{
    public int HP = 100;
    //public float MoveSpeed = 1f;
    //public float AttackVal = 10;


    public void SetDamage(float p_attackval)
    {
        HP = HP - (int)p_attackval;

        if( HP <= 0)
        {
            Debug.LogFormat("캐릭터 HP 제로 : ", this.name );
            GameObject.Destroy(gameObject);
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

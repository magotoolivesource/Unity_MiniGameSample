using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 100;

    public float DefanceVal = 1f;
    public void SetDamage( float p_attackval )
    {
        HP = (int)((float)HP - p_attackval);


    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float SpawnSec = 2f;
    public Enemy TargetEnemy = null;
    void CreateEnemy()
    {
        Enemy cloneenemy = GameObject.Instantiate<Enemy>(TargetEnemy);
        cloneenemy.gameObject.SetActive(true);
        cloneenemy.transform.position = this.transform.position;
        cloneenemy.Init();


        //Invoke("CreateEnemy", SpawnSec);
    }

    void Start()
    {
        //********************* 나중에 절대로 쓰지말것 절대로 *********************
        InvokeRepeating( "CreateEnemy", 0, SpawnSec);
        //Invoke("CreateEnemy", SpawnSec);
        // invoke 공부용임 쓰지말자


    }

    void Update()
    {
        
    }
}

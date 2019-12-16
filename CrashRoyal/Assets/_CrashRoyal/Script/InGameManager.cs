using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_TowerType
{
    Right = 0,
    Left,
    Center,

    Max
}

public class InGameManager : MonoBehaviour
{
    // 0= 오른쪽, 1은 왼쪽, 2 중앙
    public BaseActor[] m_EnemyTowerActor = new BaseActor[(int)E_TowerType.Max];
    public BaseActor[] m_MyTowerActor = new BaseActor[(int)E_TowerType.Max];


    public BaseActor GetEnemyCampTower( E_CampType p_camp, float p_x )
    {
        if( p_camp == E_CampType.MyCamp )
        {
            if( p_x >= 0)
            {
                //m_EnemyTowerActor[(int)E_TowerType.Right].GetComponent<ActorStat>().HP 
                return m_EnemyTowerActor[(int)E_TowerType.Right];
            }
            else
            {
                return m_EnemyTowerActor[(int)E_TowerType.Left];
            }
        }
        else
        {
            if (p_x >= 0)
            {
                return m_MyTowerActor[(int)E_TowerType.Left];
            }
            else
            {
                return m_MyTowerActor[(int)E_TowerType.Right];
            }
        }

    }




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

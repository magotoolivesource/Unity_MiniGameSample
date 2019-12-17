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

public class InGameManager : SingleTon<InGameManager>
{
    //private static InGameManager m_Instance = null;
    //public static InGameManager GetI
    //{
    //    get
    //    {
    //        if (m_Instance == null)
    //        {
    //            m_Instance = GameObject.FindObjectOfType<InGameManager>();
    //            if (m_Instance == null)
    //            {
    //                Debug.LogErrorFormat("매니저가 없음 확인 하세요 ");
    //            }
    //        }

    //        return m_Instance;
    //    }
    //    //set;
    //}

    //public static InGameManager GetInstace()
    //{
    //    if( m_Instance == null)
    //    {
    //        m_Instance = GameObject.FindObjectOfType<InGameManager>();
    //        if( m_Instance == null)
    //        {
    //            Debug.LogErrorFormat("매니저가 없음 확인 하세요 ");
    //        }
    //    }

    //    return m_Instance;
    //}



    // 0= 오른쪽, 1은 왼쪽, 2 중앙
    public BaseActor[] m_EnemyTowerActor = new BaseActor[(int)E_TowerType.Max];
    public BaseActor[] m_MyTowerActor = new BaseActor[(int)E_TowerType.Max];



    public static bool ISSameCampType( BaseActor p_srcactor, BaseActor p_destactor )
    {
        if( p_srcactor.CampType == E_CampType.Max
            || p_destactor.CampType == E_CampType.Max)
        {
            Debug.LogErrorFormat("현재값 이상함");
            return false;
        }

        if(p_srcactor.CampType == p_destactor.CampType)
        {
            return true;
        }

        return false;
    }

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

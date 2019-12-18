using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorTableData
{
    public ActorTableData( int p_id)
    {
        m_ID = p_id;
    }


    [SerializeField]
    private int m_ID = -1;
    public int ID
    {
        get => m_ID;
        protected set => m_ID = value;
    }

    // 데이터값
    public string Name = "";
    public int Cost = 1;

    public AttackCom AttackComData = new AttackCom();

    // UI 관련 데이터
    public Sprite UICardSprite = null;


    public Transform InGameModel = null;

    
}

public class ActorTableManager : SingleTon<ActorTableManager>
{
    [SerializeField]
    protected List<ActorTableData> m_ActorTableDataList = new List<ActorTableData>();

    public ActorTableData GetActorID( int p_id )
    {
        foreach (var item in m_ActorTableDataList)
        {
            if(item.ID == p_id)
            {
                return item;
            }
        }

        return null;
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

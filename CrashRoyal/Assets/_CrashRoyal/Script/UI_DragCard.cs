using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DragCard : MonoBehaviour
{
    public Image CardImage = null;

    [SerializeField]
    protected int m_CardID = -1;
    [SerializeField]
    ActorTableData m_LinkTableData = null;
    public void InitSettingDragCard( int p_id )
    {
        m_CardID = p_id;
        m_LinkTableData = ActorTableManager.GetI.GetActorID(m_CardID);
        CardImage.sprite = m_LinkTableData.UICardSprite;


    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

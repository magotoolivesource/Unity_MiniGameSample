using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class UICard : MonoBehaviour
{
    public int m_ActorID = -1;

    ActorTableData m_LinkTableData = null;

    // 실제 적용위한 값
    public Image CardImage = null;
    public Text CostLabel = null;
    public Text CardLabel = null;

    public void SetInitUICard(int p_id)
    {
        m_ActorID = p_id;

        m_LinkTableData = ActorTableManager.GetI.GetActorID(m_ActorID);

        CardImage.sprite = m_LinkTableData.UICardSprite;
        CardLabel.text = m_LinkTableData.Name;
        CostLabel.text = m_LinkTableData.Cost.ToString();
    }


    public void PointDownEvent( BaseEventData p_data )
    {
        PointerEventData eventdata = p_data as PointerEventData;
        Debug.Log("클릭 : " + eventdata.position );
    }
    public void PointUpEvent(BaseEventData p_data)
    {
        PointerEventData eventdata = p_data as PointerEventData;

        if( m_ISDrag )
        {
            Vector3 screenpos = eventdata.position;
            //Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenpos);
            LayerMask layermask = LayerMask.GetMask("Land");
            RaycastHit hitinfo;

            if(Physics.Raycast(ray, out hitinfo, 100f, layermask))
            {
                Vector3 worldpos = hitinfo.point;

                Debug.Log("위치값 : " + worldpos);

                //GameObject.Instantiate();
            }
            


        }

        m_ISDrag = false;
    }

    bool m_ISDrag = false;
    public void DragEvent(BaseEventData p_data)
    {
        PointerEventData eventdata = p_data as PointerEventData;
        //BaseEventData

        m_ISDrag = true;

        Debug.Log("드래그 : " + eventdata.position);
    }


    void Start()
    {
        // 테스트 코드
        SetInitUICard(m_ActorID);
    }

    void Update()
    {
        
    }
}

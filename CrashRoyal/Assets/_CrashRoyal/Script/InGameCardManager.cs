using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCardManager : SingleTon<InGameCardManager>
{

    public UI_DragCard DragCard = null;

    public RectTransform CardRectTransform = null;
    public UICard CloneUICard = null;

    public RectTransform[] CardPosition = new RectTransform[4];

    public UICard CreateUICard( int p_id
        , RectTransform p_recttrans
        , int p_posindex )
    {
        UICard outcard = GameObject.Instantiate<UICard>(CloneUICard);
        outcard.SetInitUICard(p_id);
        //outcard.transform.parent = p_recttrans;
        outcard.transform.SetParent(p_recttrans);
        outcard.transform.position = CardPosition[p_posindex].position;
        
        return outcard;
    }

    public void InitUICard()
    {
        DragCard.gameObject.SetActive(false);

        CreateUICard(0, CardRectTransform, 0);
        CreateUICard(1, CardRectTransform, 1);
        CreateUICard(1, CardRectTransform, 2);
        CreateUICard(0, CardRectTransform, 3);
    }



    void Start()
    {
        InitUICard();

    }

    void Update()
    {
        
    }
}

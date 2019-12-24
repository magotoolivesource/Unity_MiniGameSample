using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    bool m_ISInit = true;
    public Vector3 InitPos = new Vector3();
    public void SetInitPos()
    {
        //CharacterController colltrol = GetComponent<CharacterController>();

        InitPos = MapGenerator.GetI.GetInitPlayerPos();
        transform.position = InitPos;

        GetComponent<Rigidbody>().position = InitPos;

        //
        //colltrol.attachedRigidbody.position = InitPos;
        CharacterController colltrol = GetComponent<CharacterController>();
        colltrol.enabled = true;
    }


    Camera m_Camera = null;
    void Start()
    {
        m_Camera = GetComponentInChildren<Camera>();
        SetInitPos();
    }


    //private static Vector3 m_TempStaticVal = new Vector3();
    //public static Vector3 Zero()
    //{
    //    return m_TempStaticVal;
    //}

    //public static Vector3 GetVetor(float p_x, float p_y, float p_z)
    //{
    //    m_TempStaticVal.Set(p_x, p_y, p_z);
    //    return m_TempStaticVal;
    //}



    void DestroyBlock( BaseBlock p_block )
    {
        Vector3 worldpos = p_block.transform.position;
        GameObject.Destroy(p_block.gameObject);

        //Vector3 worldposa = new Vector3(10, 20, 30);


        
        // 6면에 블럭 생성
        Vector3Int worldposint = Vector3Int.CeilToInt(worldpos);
        // 위
        MapGenerator.GetI.CrateBlock(worldposint.x
            , worldposint.y + 1
            , worldposint.z
            , E_BlockType.Grass);

        MapGenerator.GetI.CrateBlock(worldposint.x
            , worldposint.y -1
            , worldposint.z
            , E_BlockType.Grass);

        MapGenerator.GetI.CrateBlock(worldposint.x + 1
            , worldposint.y
            , worldposint.z
            , E_BlockType.Grass);

        MapGenerator.GetI.CrateBlock(worldposint.x -1
            , worldposint.y
            , worldposint.z
            , E_BlockType.Grass);

        MapGenerator.GetI.CrateBlock(worldposint.x
            , worldposint.y
            , worldposint.z + 1
            , E_BlockType.Grass);

        MapGenerator.GetI.CrateBlock(worldposint.x
            , worldposint.y
            , worldposint.z - 1
            , E_BlockType.Grass);

    }

    void Digging()
    {
        Vector3 forward = m_Camera.transform.forward;

        RaycastHit hitinfo;
        LayerMask mask = LayerMask.GetMask("Land", "Item", "Enemy");

        if (Physics.Raycast(m_Camera.transform.position
            , forward
            , out hitinfo
            , 3f
            , mask))
        {
            BaseBlock block = hitinfo.transform.GetComponent<BaseBlock>();

            if (block)
            {
                //GameObject.Destroy(block.gameObject);

                DestroyBlock(block);
            }

        }
    }

    void RightClick()
    {
        Digging();

    }

    void LeftClick()
    {

    }
    void ClickEvent()
    {
        if( Input.GetMouseButtonDown(0))
        {
            RightClick();
        }

        if(Input.GetMouseButtonDown(1))
        {
            LeftClick();
        }

    }

    void Update()
    {
        ClickEvent();


        //CharacterController colltrol = GetComponent<CharacterController>();
        //if(m_ISInit  )
        //{
        //    m_ISInit = false;
        //    SetInitPos();
        //}
        

        //if(m_ISInit || true)
        //{
        //    m_ISInit = false;
        //    SetInitPos();
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_BlockType
{
    Grass,
    Mud,


    Max

}

public class BaseBlock : MonoBehaviour
{
    public E_BlockType BlockType = E_BlockType.Grass;
    [SerializeField]
    protected Vector3Int m_WorldPos;

    public void InitSettingBlock()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

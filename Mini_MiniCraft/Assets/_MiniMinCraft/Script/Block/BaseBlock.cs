using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_BlockType
{
    Grass,
    Mud,
    Soil,
    Ice,


    Max

}

public class BaseBlock : MonoBehaviour
{
    public E_BlockType BlockType = E_BlockType.Grass;
    [SerializeField]
    protected Vector3Int m_WorldPos;

    public void SetBlockType(E_BlockType p_type)
    {
        BlockType = p_type;
        UpdateDirectMaterial();
    }

    public void UpdateDirectMaterial()
    {
        GetComponent<MeshRenderer>().material = MapGenerator.GetI.GetBlockMaterial(BlockType);

    }

    public void InitSettingBlock()
    {
        Vector3 pos = transform.position;
        Material mat = MapGenerator.GetI.GetHeightRandMaterial((int)pos.y);
        GetComponent<MeshRenderer>().material = mat;

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

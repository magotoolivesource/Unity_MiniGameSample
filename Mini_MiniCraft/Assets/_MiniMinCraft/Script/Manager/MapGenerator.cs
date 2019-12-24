using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : SingleTon<MapGenerator>
{
    public bool ISRand = true;
    public int RandomSeed = 100;
    public Vector2Int MapSize = new Vector2Int(30, 30);

    //[SerializeField]
    protected int m_MapHeight = 100;
    public BaseBlock[,,] m_MapDataBlockArray = null;// new BaseBlock[2, 2];
    public bool[,,] m_ISCreateBlockArray = null;


    public float HeightMulti = 1f;
    public float Weight = 1000;
    public Vector2 MapOffset = new Vector2(0f, 0f);
    
    public BaseBlock CloneBlock = null;


    public int m_OffsetHeight = 30;



    public Vector3 GetInitPlayerPos()
    {
        int x = Random.Range(0, MapSize.x);
        int y = Random.Range(0, MapSize.y);
        float height = GetHeight(x, y);

        return new Vector3(x, height + 2f, y);
    }

    public int GetHeight(int p_x, int p_y)
    {
        float x = MapOffset.x + ((float)p_x / Weight);
        float y = MapOffset.y + ((float)p_y / Weight);

        float height = Mathf.PerlinNoise( x, y) * HeightMulti;
        //height *= HeightMulti;

        height = (int)height + m_OffsetHeight;

        return (int)height;
    }

    public Material[] GetHeightMaterial;
    public Material GetBlockMaterial(E_BlockType p_type)
    {
        return GetHeightMaterial[(int)p_type];
    }
    public Material GetHeightRandMaterial(int p_height)
    {
        int index = Random.Range(0, GetHeightMaterial.Length);

        int height = p_height - m_OffsetHeight;

        if( height < 5)
        {
            index = 0;
        }
        else if(height < 7)
        {
            index = Random.Range(0, 2);
        }
        else if(height < 9)
        {
            index = 1;
        }
        else if( height < 10)
        {
            index = Random.Range(1, 3);
        }
        else if (height < 12)
        {
            index = 2;
        }
        else if(height < 16)
        {
            index = Random.Range(2, 4);
        }
        else if (height < 20)
        {
            index = 3;
        }
        else if (height < 25)
        {
            index = 4;
        }

        return GetHeightMaterial[index];
    }
    public BaseBlock CrateBlock(int p_x, int p_y, int p_z, E_BlockType p_type)
    {
        BaseBlock block = GameObject.Instantiate<BaseBlock>(CloneBlock);
        block.gameObject.SetActive(true);

        Vector3 worldpos = new Vector3(p_x, p_y, p_z);
        block.transform.position = worldpos;

        m_MapDataBlockArray[p_z, p_y, p_x] = block;
        m_MapDataBlockArray[p_z, p_y, p_x].SetBlockType(p_type);
        m_ISCreateBlockArray[p_z, p_y, p_x] = true;
        return block;
    }

    public BaseBlock CrateBlock(Vector3Int p_worldpos, E_BlockType p_type )
    {
        BaseBlock block = GameObject.Instantiate<BaseBlock>(CloneBlock);
        block.gameObject.SetActive(true);


        Vector3 worldpos = new Vector3( p_worldpos.x, p_worldpos.y, p_worldpos.z );
        block.transform.position = worldpos;

        m_MapDataBlockArray[p_worldpos.y, p_worldpos.y, p_worldpos.x] = block;
        m_MapDataBlockArray[p_worldpos.y, p_worldpos.y, p_worldpos.x].SetBlockType(p_type);
        return block;
    }

    public BaseBlock CreateBlock(int p_x, int p_y)
    {
        BaseBlock block = GameObject.Instantiate<BaseBlock>(CloneBlock);
        block.gameObject.SetActive(true);

        int height = GetHeight(p_x, p_y);

        Vector3 worldpos = new Vector3(p_x, height, p_y);
        block.transform.position = worldpos;

        m_MapDataBlockArray[p_y, (int)height, p_x] = block;

        m_MapDataBlockArray[p_y, height, p_x].InitSettingBlock();
        return block;
    }

    public void CreateMapGenerator()
    {
        m_MapHeight = m_OffsetHeight + MapSize.x;
        m_MapDataBlockArray = new BaseBlock[MapSize.y, m_MapHeight, MapSize.x];
        m_ISCreateBlockArray = new bool[MapSize.y, m_MapHeight, MapSize.x];


        int xsize = MapSize.x;
        int ysize = MapSize.x;

        for (int y = 0; y < ysize; y++)
        {
            for (int x = 0; x < xsize; x++)
            {
                CreateBlock(x, y);
            }
        }

    }

    private void Awake()
    {
        System.Random rnd = new System.Random();
        if (ISRand)
        {
            RandomSeed = rnd.Next(int.MaxValue);
        }
        
        rnd = new System.Random(RandomSeed);
        MapOffset.x = (float)rnd.Next(10000) * 0.01f;
        MapOffset.y = (float)rnd.Next(10000) * 0.01f;


        CreateMapGenerator();
    }

    void Temp_UpdateLandReLocation()
    {
        int xsize = MapSize.x;
        int ysize = MapSize.x;
        BaseBlock block = null;
        int height = 0;
        for (int y = 0; y < ysize; y++)
        {
            for (int x = 0; x < xsize; x++)
            {
                height = GetHeight(x, y);

                block = m_MapDataBlockArray[y, height, x];
                if (block == null)
                    continue;

                Vector3 worldpos = new Vector3(x, height, y);
                block.transform.position = worldpos;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Temp_UpdateLandReLocation();



    }
}

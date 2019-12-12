using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public float MoveSpeed = 1f;

    void UpdateMoveControl()
    {
        float xmove = Input.GetAxis("Horizontal");
        float ymove = Input.GetAxis("Vertical");

        Vector3 temppos = transform.position;
        temppos.x += xmove * MoveSpeed;
        temppos.z += ymove * MoveSpeed;

        transform.position = temppos;

    }


    public float RotSpeed = 1f;

    [Header("[확인용]"), SerializeField]
    private float m_TempVal = 0f;
    [SerializeField]
    private Vector2 m_TempMouse = new Vector2();
    [SerializeField]
    private Vector3 m_MousePosition = new Vector3();
    [SerializeField]
    private Vector3 m_PlayerTOScreenPosition = new Vector3();


    private float m_RadianTODergger = (180 / Mathf.PI);


    void UpdateRotation()
    {
        m_MousePosition = Input.mousePosition;
        m_PlayerTOScreenPosition = 
            Camera.main.WorldToScreenPoint(transform.position);

        m_MousePosition = m_MousePosition - m_PlayerTOScreenPosition;

        //Debug.Log("위치값 : " + m_MousePosition.ToString());

        m_TempVal = Mathf.Atan2( -m_MousePosition.y, m_MousePosition.x );
        m_TempVal = m_TempVal * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, m_TempVal, 0);






        //if( Input.GetKey( KeyCode.Q ) )
        //{
        //    // 1도 회전
        //    Quaternion temprot = transform.rotation;

        //    Vector3 rot = temprot.eulerAngles;
        //    rot.y += RotSpeed;
        //    temprot = Quaternion.Euler(rot);

        //    transform.rotation = temprot;
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    // 1도 회전
        //    Quaternion temprot = transform.rotation;

        //    Vector3 rot = temprot.eulerAngles;
        //    rot.y -= RotSpeed;
        //    temprot = Quaternion.Euler(rot);

        //    transform.rotation = temprot;
        //}


    }


    public Transform Nozzle = null;

    void OnDrawGizmos()
    {
        Vector3 direction = transform.rotation * new Vector3(1, 0, 0) * 100f;
        Debug.DrawLine(Nozzle.position
            , Nozzle.position + direction
            , Color.blue);
    }


    public LayerMask EnemyLayer = new LayerMask();
    void UpdateFire()
    {

        if( Input.GetMouseButtonDown(0) )
        {
            //Nozzle.position;
            Vector3 direction = transform.rotation * new Vector3(1, 0, 0);

            RaycastHit hitinfo;
            if( Physics.Raycast(Nozzle.position
                , direction
                , out hitinfo
                , 100f
                , EnemyLayer ) )
            {
                //hitinfo.transform.gameObject.layer == ;


                //hitinfo.transform.gameObject
                Debug.Log("히트 : " + hitinfo.transform.name);


                Enemy hitenemy = hitinfo.transform.GetComponent<Enemy>();

                if(hitenemy )
                {
                    hitenemy.SetDamage(20);

                    //GameObject.Destroy(hitinfo.transform.gameObject);
                }

                //if (hitinfo.transform.tag == "Envement")
                //{
                //    return;
                //}
                

            }
            



        }


    }

    void Update()
    {
        UpdateMoveControl();
        UpdateRotation();
        UpdateFire();

    }
}

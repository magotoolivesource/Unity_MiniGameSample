using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFoller : MonoBehaviour
{
    public Transform TargetTrans = null;
    public float SmoothVal = 0.3f;

    Vector3 m_OffsetPos;// = new Vector3();
    void Awake()
    {
        m_OffsetPos = transform.position - TargetTrans.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 tempos = TargetTrans.position + m_OffsetPos;
        transform.position = Vector3.Lerp(transform.position
            , tempos
            , SmoothVal * Time.deltaTime);


    }
}

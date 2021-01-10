using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = targetObject.transform.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoRotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform centerTransformToRotate;

    void Update()
    {
        centerTransformToRotate.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
    } 
}

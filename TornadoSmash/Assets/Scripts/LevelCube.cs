using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelCube : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rbCube;
    private BoxCollider boxCol;
    private Transform centerTornado;
    [Header("General Variables")]
    public float shrinkSpeed; // 1
    public float risingSpeed;  // 5
    public float risingRotationSpeed;  // 180 
    public float vacuumSpeed; // 1750 
    public LevelCubeStates currentState;

    public enum LevelCubeStates
    {
        OnHold,
        InVacuum,
        OnRise
    }

    private void Start()
    {
        GameManager.instance.cubeList.Add(transform);
        rbCube = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        centerTornado = GameObject.FindGameObjectWithTag("TornadoInnerTrigger").transform;
    } 

    private void Update()
    {
        switch (currentState)
        {
            case LevelCubeStates.OnHold:
                break;
            case LevelCubeStates.InVacuum:
                HandleInVacuum();
                break;
            case LevelCubeStates.OnRise:
                HandleRising();
                break;
        }
    } 

    void HandleInVacuum()
    {
        transform.Rotate(new Vector3((risingRotationSpeed / 2f) * Time.deltaTime, (risingRotationSpeed / 2f) * Time.deltaTime, (risingRotationSpeed / 2f) * Time.deltaTime), Space.Self);
    } 

    void HandleRising()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (risingSpeed * Time.deltaTime), transform.position.z);
        transform.Rotate(new Vector3(risingRotationSpeed * Time.deltaTime, risingRotationSpeed * Time.deltaTime, risingRotationSpeed * Time.deltaTime), Space.Self);
        boxCol.enabled = false;
    } 

    void TriggerShrink()
    {
        transform.DOScale(0.05f, shrinkSpeed).OnComplete(() =>
        {
            UpdateCubeListFromGameManager();
            Destroy(gameObject);
        });
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TornadoInnerTrigger"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.OnRise;
                transform.parent = other.transform;
                rbCube.isKinematic = true;
                TriggerShrink();
            }
        }
    } 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TornadoVacuumCollider"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.InVacuum;

                Vector3 dir = (centerTornado.position - transform.position).normalized;
                rbCube.AddForce(dir * vacuumSpeed * Time.deltaTime);
            }
        }
    } 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TornadoVacuumCollider"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.OnHold;
            }
        }
    } 

    void UpdateCubeListFromGameManager()
    {
        GameManager.instance.cubeList.Remove(transform);
        GameManager.instance.CheckGameWinning();
    } 
} 
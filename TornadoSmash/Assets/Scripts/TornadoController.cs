using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    public static TornadoController instance;

    [Header("General Variables")]
    private Vector2 firstTouchPosition;
    private Vector2 curTouchPosition;   
    [SerializeField] private float sensitivityMultiplier; // 0.01f    
    [SerializeField] private float xBound;
    [SerializeField] private float minZBound;
    [SerializeField] private float maxZBound;
    private float finalTouchX, finalTouchZ;
    private bool canMove = true;
    [Header("References")]
    public CapsuleCollider centerCollider; 
    private Rigidbody rbTornado;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rbTornado = GetComponent<Rigidbody>();
    } 

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    } 

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            curTouchPosition = Input.mousePosition;
            Vector2 touchDelta = (curTouchPosition - firstTouchPosition);

            finalTouchX = (transform.position.x + (touchDelta.x * sensitivityMultiplier));
            finalTouchZ = (transform.position.z + (touchDelta.y * sensitivityMultiplier));

            rbTornado.position = new Vector3(finalTouchX, transform.position.y, finalTouchZ);           
            rbTornado.position = new Vector3(Mathf.Clamp(rbTornado.position.x, -xBound, xBound), rbTornado.position.y, Mathf.Clamp(rbTornado.position.z, minZBound, maxZBound));

            firstTouchPosition = Input.mousePosition; 
        }
    } 

    public void TriggerLevelFinished()
    {
        canMove = false;
        rbTornado.isKinematic = true;
    } 
} 

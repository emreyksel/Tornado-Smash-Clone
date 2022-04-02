using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorCube : MonoBehaviour
{
    [SerializeField] private float shrinkTime; //1
    private bool isRising = false;

    void TriggerRising()
    {
        transform.DOLocalMove(new Vector3(0f, transform.localPosition.y + 3f, 0f), shrinkTime);
        transform.DOScale(0.05f, shrinkTime).OnComplete(() => { Destroy(gameObject); });
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TornadoInnerTrigger") && GameManager.instance.isGameFinished && !isRising)
        {
            isRising = true;
            transform.parent = other.transform;
            TriggerRising();
        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            gameObject.SetActive(false);
    }
}

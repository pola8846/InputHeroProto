using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position+Vector3.up*Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, 0, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    private Vector3 originPos;


    private void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {
        transform.position = GameManager.Player.transform.position + originPos;
    }
}

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
        if (GameManager.Player!=null)
        {
            transform.position = GameManager.Player.transform.position + originPos;
        }
        else
        {
            transform.position = GameManager.Player2D.transform.position + originPos;
        }
    }
}

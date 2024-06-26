using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    private Vector3 originPos;
    public SmoothMoving smooth;
    public float trackingDistance;//추적 시작할 거리

    private void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {
        Vector3 targetPos = GameManager.Player2D.transform.position + originPos;
        if (Vector3.Distance(transform.position, targetPos) > trackingDistance)
        {
            if (GameManager.Player != null)
            {
                transform.position = targetPos;
            }
            else
            {
                //transform.position = GameManager.Player2D.transform.position + originPos;
                Vector3 dist = targetPos - transform.position;
                dist = dist.normalized;
                dist *= trackingDistance;
                smooth.directionPos = targetPos-dist;
            }
        }
    }
}

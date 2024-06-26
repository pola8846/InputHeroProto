using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoving : MonoBehaviour
{
    public Vector3 directionPos;

    public float minSpeed;
    public float minSpeedDistance;

    public float maxSpeed;
    public float maxSpeedDistance;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(directionPos, transform.position);

        if (distance < minSpeedDistance)
        {
            Move(minSpeed);
        }
        else if (distance > maxSpeedDistance)
        {
            Move(maxSpeed);
        }
        else
        {
            float dist = Vector3.Distance(transform.position, directionPos);
            float speedT = Mathf.InverseLerp(minSpeedDistance, maxSpeedDistance, dist);
            Move(Mathf.Lerp(minSpeed, maxSpeed, speedT));
        }
    }
    private void Update()
    {
        //float distance = Vector3.Distance(directionPos, transform.position);

        //if (distance < minSpeedDistance )
        //{
        //    Move(minSpeed);
        //}
        //else if (distance > maxSpeedDistance )
        //{
        //    Move(maxSpeed);
        //}
        //else
        //{
        //    float dist = Vector3.Distance(transform.position, directionPos);
        //    float speedT = Mathf.InverseLerp(minSpeedDistance, maxSpeedDistance, dist);
        //    Move(Mathf.Lerp(minSpeed, maxSpeed, speedT));
        //}
    }

    private void Move(float speed)
    {
        Vector3 temp = transform.position - directionPos;
        temp = temp.normalized;
        temp *= -speed * Time.fixedDeltaTime;
        transform.Translate(temp);
    }
}

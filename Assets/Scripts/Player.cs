using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 8f;

    private Rigidbody rb;

    private void Start()
    {
        GameManager.SetPlayer(this);
        rb = GetComponent<Rigidbody>();
    }

    public void DoAction(Action actionCode)
    {
        switch (actionCode)
        {
            case Action.MoveUp:
                break;
            case Action.MoveDown:
                break;
            case Action.MoveLeft:
                transform.position += speed * Time.deltaTime * Vector3.left;
                break;
            case Action.MoveRight:
                transform.position += speed * Time.deltaTime * Vector3.right;
                break;
            case Action.MoveStop:
                break;
            case Action.Jump:
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }
}

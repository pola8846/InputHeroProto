using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Unit
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 8f;


    private Rigidbody2D rb;

    private void Start()
    {
        GameManager.SetPlayer(this);
        rb = GetComponent<Rigidbody2D>();
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
            case Action.Attack:
                break;
            case Action.Jump:
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
                break;
            default:
                break;
        }
    }

    
}

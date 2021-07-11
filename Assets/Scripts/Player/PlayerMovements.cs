using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Rigidbody2D playerRigidBody2d;

    Vector2 movement;

    void Start()
    {
        playerRigidBody2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(-150, 0, 90);
        }
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(-150, 0, -90);
        }

        if (movement.y > 0)
        {
            transform.rotation = Quaternion.Euler(-150, 0, 0);
        }
        else if (movement.y < 0)
        {
            transform.rotation = Quaternion.Euler(-150, 0, 180);
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody2d.MovePosition(playerRigidBody2d.position + movement * moveSpeed * Time.deltaTime);
    }
}

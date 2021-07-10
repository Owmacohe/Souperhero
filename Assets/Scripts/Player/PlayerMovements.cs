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

    }

    private void FixedUpdate()
    {
        playerRigidBody2d.MovePosition(playerRigidBody2d.position + movement * moveSpeed * Time.deltaTime);
    }
}

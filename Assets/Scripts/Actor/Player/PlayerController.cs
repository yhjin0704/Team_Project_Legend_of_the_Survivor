using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rigidBody;
    private Animator animator;
    

    private Vector2 moveInput;

    void Awake()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        player.isMove = (moveInput.x != 0 || moveInput.y != 0);
        animator.SetBool("IsMove", player.isMove);
    }

    void FixedUpdate()
    {
        rigidBody.velocity = moveInput * player.speed;
    }
}

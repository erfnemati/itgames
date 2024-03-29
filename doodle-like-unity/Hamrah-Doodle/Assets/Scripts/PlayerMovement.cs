using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerRb;
    bool isCollidedWithPlatform = false;

    private Vector2 movement = Vector2.zero;
    [SerializeField] float playerVerticalSpeed = 50f;
    [SerializeField] float playerHorizontalSpeed = 5f;

    private void Awake()    
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.rotation = Quaternion.identity;
        movement.x = Input.GetAxis("Horizontal");
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        //Debug.Log("Velocity is " + playerRb.velocity);
        playerRb.velocity = new Vector2(movement.x * playerHorizontalSpeed,playerRb.velocity.y) ;
    }

    

    public void GoRight()
    {
        Debug.Log("going right");
        
    }

    public void GoLeft()
    {
        Debug.Log("going left");
        
    }



}

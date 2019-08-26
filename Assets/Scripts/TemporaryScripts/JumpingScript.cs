using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float maxMovementSpeed, acceleration, deceleration, jumpVelocity, fallMultiplier, lowJumpMultiplier;
    private float _movementSpeed;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        _movementSpeed = 0;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            _rb.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
        }
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") != 0 && _movementSpeed < maxMovementSpeed && _movementSpeed > -maxMovementSpeed)
        { //controls movement left and right
            _movementSpeed += Input.GetAxis("Horizontal") * Time.deltaTime * acceleration;
        }
        else if (Input.GetAxis("Horizontal") != 0 && _movementSpeed >= maxMovementSpeed)
        { //moving right
            _movementSpeed = maxMovementSpeed;
        }
        else if (Input.GetAxis("Horizontal") != 0 && _movementSpeed <= -maxMovementSpeed)
        { //moving left
            _movementSpeed = -maxMovementSpeed;
        }

        if (_movementSpeed > 0)
            //deceletating when moving right
            _movementSpeed += deceleration * Time.deltaTime;
        if (_movementSpeed < 0)
            //decelerating when moving left
            _movementSpeed -= deceleration * Time.deltaTime;
        if (_movementSpeed < 0.05 && _movementSpeed > -0.05)
            //making sure that all adequately small speeds go down to zero
            _movementSpeed = 0;

        _rb.velocity = new Vector2(_movementSpeed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
    }
}

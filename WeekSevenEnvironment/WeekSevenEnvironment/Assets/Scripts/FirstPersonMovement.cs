using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10.0f;

    private Rigidbody rb;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 localDirection = transform.TransformDirection(direction);
        rb.MovePosition(rb.position + (localDirection * speed * Time.deltaTime));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Water"))  
        {
            //rb.velocity = Vector3.zero;

            Vector3 localDirection = transform.TransformDirection(direction);
            rb.MovePosition(rb.position + -localDirection);

            // JUMP BACK
        }
    }

    public void OnPlayerMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        direction.x = inputVector.x;
        direction.z = inputVector.y;
    }
}

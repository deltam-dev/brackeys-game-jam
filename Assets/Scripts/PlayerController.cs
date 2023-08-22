using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float downSpeed = 2f;
    public float movementSpeed = 2f;

    private bool startedMoving = false;
    private bool isMovingDown = false;

    void Start() { }

    void Update() { }

    void FixedUpdate()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log(startedMoving);
        //     Debug.Log(isMovingDown);
        // }

        // Stop at the start
        if (startedMoving && rb.position.y >= 0f)
        {
            startedMoving = false;
            isMovingDown = false;

            rb.velocity = Vector2.zero;
        }

        // vertical movement
        if (Input.GetKeyDown(KeyCode.Space) && !startedMoving && !isMovingDown)
        {
            startedMoving = true;
            isMovingDown = true;

            rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && startedMoving && isMovingDown)
        {
            isMovingDown = false;

            rb.AddForce(Vector2.up * downSpeed * 2, ForceMode2D.Impulse);
        }

        // horizontal movement
        if (startedMoving)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            if (moveX > 0) {
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
            } else if (moveX < 0) {
                rb.AddForce(Vector2.left * movementSpeed, ForceMode2D.Impulse);
            }
        }

        // moves the camera
        Camera.main.transform.position = new Vector3(rb.position.x, rb.position.y, -10);
    }
}

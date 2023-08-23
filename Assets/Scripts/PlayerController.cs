using System;
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
    private bool isStatic = false;

    void Awake()
    {
        // limit framerate        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start() { }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log(startedMoving);
        //     Debug.Log(isMovingDown);
        // }

        GameState.Instance.updateDepth(rb.position.y);

        // Stop at the start
        if (startedMoving && rb.position.y >= 0f)
        {
            startedMoving = false;
            isMovingDown = false;

            rb.velocity = Vector2.zero;
            GameState.Instance.returnedToSurface();
        }

        if (Input.GetKeyDown(KeyCode.S) && startedMoving)
        {
            if (!isStatic)
            {
                isStatic = true;

                rb.velocity = Vector2.zero;
            }
            else
            {
                isStatic = false;

                if (isMovingDown)
                {
                    rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.up * downSpeed, ForceMode2D.Impulse);
                }
            }
        }

        // vertical movement
        if (Input.GetKeyDown(KeyCode.Space) && !startedMoving && !isMovingDown)
        {
            startedMoving = true;
            isMovingDown = true;

            rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
            GameState.Instance.startDiving();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && startedMoving && isMovingDown)
        {
            isMovingDown = false;

            if (isStatic)
            {
                isStatic = false;

                rb.AddForce(Vector2.up * downSpeed, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * downSpeed * 2, ForceMode2D.Impulse);
            }
        }

        // horizontal movement
        if (startedMoving && !isStatic)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            if (moveX > 0)
            {
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
            }
            else if (moveX < 0)
            {
                rb.AddForce(Vector2.left * movementSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        // moves the camera
        Camera.main.transform.position = new Vector3(0, rb.position.y, -10);
    }
}

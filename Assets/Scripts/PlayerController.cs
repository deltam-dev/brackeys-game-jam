using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float downSpeed = 2f;
    public float movementSpeed = 2f;
    public GameObject playerCamera;

    private bool startedMoving = false;
    private bool isMovingDown = false;
    private bool isStatic = false;

    Vector2 mousePosition;

    public AudioSource camaraFlash;
    public GameObject pez;

    void Awake()
    {
        // limit framerate        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        mousePosition = new Vector2(rb.position.x, rb.position.y - 1f);
    }

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

            // stop player
            rb.velocity = Vector2.zero;
            // return player to start position
            rb.position = new Vector2(0, 0);
            // stop playerCamera
            mousePosition = new Vector2(rb.position.x, rb.position.y - 1f);
            // restart O2 and activate shop
            GameState.Instance.returnedToSurface();
        }

        // stop movement once started
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

        if (startedMoving)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = -Camera.main.transform.position.z;
            mousePosition = Camera.main.ScreenToWorldPoint(screenPoint);

            if (Input.GetMouseButtonDown(0))
            {
                camaraFlash.Play();
                GameState.Instance.addPhoto(pez);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f);
                if (hit.collider != null)
                {
                    //Hit something, print the tag of the object
                    Debug.Log("Hitting: " + hit.collider.tag);
                    Debug.Log("Hitting: " + hit.collider.name);
                }

                // Method to draw the ray in scene for debug purpose
                Debug.DrawRay(transform.position, Vector2.right * 5F, Color.red);
            }
        }
    }

    void FixedUpdate()
    {
        // moves the camera
        Camera.main.transform.position = new Vector3(0, rb.position.y, -10);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion aimRotation = Quaternion.Euler(0f, 0f, aimAngle);
        playerCamera.transform.rotation = aimRotation;
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "photoBeforeSold")
        {
            Debug.Log("This!");
        }
    }
}

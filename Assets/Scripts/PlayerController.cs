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
    private Animator animator;

    Vector2 mousePosition;

    public AudioSource camaraFlash;
    public AudioSource moneyAudio;

    void Awake()
    {
        // limit framerate        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
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
            // play audio if anything to sell
            if (GameState.Instance.CurrentMoney > 0)
            {
                moneyAudio.Play();
            }
            // restart O2 and activate shop

            GameState.Instance.returnedToSurface();
        }

        // stop movement once started
        if (Input.GetKeyDown(KeyCode.S) && startedMoving)
        {
            animator.SetTrigger("finish");
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
                    animator.SetBool("down", true);
                    rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    animator.SetTrigger("return");
                    rb.AddForce(Vector2.up * downSpeed, ForceMode2D.Impulse);
                }
            }
        }

        // vertical movement
        if (Input.GetKeyDown(KeyCode.Space) && !startedMoving && !isMovingDown)
        {
            animator.SetBool("down", true);
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
                animator.SetBool("down", false);
            }
            else
            {
                animator.SetBool("down", true);
                rb.AddForce(Vector2.up * downSpeed * 2, ForceMode2D.Impulse);
            }
        }

        // horizontal movement
        if (startedMoving && !isStatic)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            if (moveX > 0)
            {
                animator.SetTrigger("right");
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
            }
            else if (moveX < 0)
            {
                animator.SetTrigger("left");
                rb.AddForce(Vector2.left * movementSpeed, ForceMode2D.Impulse);
            }
        }

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion aimRotation = Quaternion.Euler(0f, 0f, aimAngle);
        playerCamera.transform.rotation = aimRotation;

        if (startedMoving)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = -Camera.main.transform.position.z;
            mousePosition = Camera.main.ScreenToWorldPoint(screenPoint);

            if (Input.GetMouseButtonDown(0) && GameState.Instance.canTakePhoto())
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDirection, 5f, LayerMask.GetMask("Fish"));
                if (hit.collider != null)
                {
                    float depth = (rb.position.y * -1) * 0.5f;
                    float distance = hit.distance != 0 ? (1 / hit.distance) * 10 * 0.2f: 0f;
                    float rarity = hit.collider.gameObject.GetComponent<FishController>().fish.rare * 20 * 0.3f;
                    Debug.Log("" + depth + ", " + distance + ", " + rarity);

                    float moneyRaw = depth + distance + rarity;
                    float moneyNormalized = (moneyRaw / 1000) * 100;
                    Debug.Log("" + moneyRaw + ", " + moneyNormalized);

                    GameState.Instance.addPhoto(hit.collider.gameObject, moneyNormalized);
                    Debug.Log("Hitting: " + hit.collider.name);

                    camaraFlash.Play();
                }

                // Method to draw the ray in scene for debug purpose
                Debug.DrawRay(transform.position, aimDirection * 5f, Color.red);
            }


        }
    }

    void FixedUpdate()
    {
        // moves the camera
        Camera.main.transform.position = new Vector3(0, rb.position.y, -10);


    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "photoBeforeSold")
        {
            Debug.Log("This!");
        }
    }
}

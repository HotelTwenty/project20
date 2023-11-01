using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class p20MainControl : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public float xVal;
    int playerJumpPower = 25000;
    public float moveX;
    public static bool isGrounded = false;
    public static bool cMove;

    public GameObject leftDJ, rightDJ, healthImageFlash, healthBarFlash;
    Vector2 leftDJPos, rightDJPos;
    float nextDJ = 0.0f;
    float DJRate = 0.0005f;

    public int currentHealth;
    public static int p20Health;
    Vector3 spawnPos1;
    public static bool fRight = false;
    
    public AudioSource p20PlayerHit, p20MenBut;

    void Awake()
    {
        controls = new PlayerControls();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        cMove = true;
        p20Health = 300;
        spawnPos1 = new Vector3(0f, 100f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentHealth = p20Health;
        xVal = move.x;
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m * 0, Space.World);
        p20Go();

        if (moveX >= 0 && Time.time > nextDJ)
        {
            if(isGrounded == false)
            {
                nextDJ = Time.time + DJRate;
                djLeft();
            }
        }

         else if (moveX < 0 && Time.time > nextDJ)
        {
            if(isGrounded == false)
            {
                nextDJ = Time.time + DJRate;
                djRight();
            }
        }

        if(p20Health <= 0)
        {
            Invoke("reSpawn", .5f);
            Invoke("reHealth", 3.5f);
        }
    }

    void p20Go()
    {
        bool isLeftKeyHeld = controls.GamePlay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.GamePlay.MoveRight.ReadValue<float>() > 0.1f;
        
        if(isLeftKeyHeld || move.x < -0.1f)
        {
            transform.position = new Vector2(transform.position.x - 30 * Time.deltaTime, transform.position.y);
            if(fRight == true)
            {
                FlipPlayer();
                fRight = false;
            }
        }

        if(isRightKeyHeld || move.x > 0.1f)
        {
            transform.position = new Vector2(transform.position.x + 30 * Time.deltaTime, transform.position.y);
            if(fRight == false)
            {
                FlipPlayer();
                fRight = true;
            }
        }

        controls.GamePlay.JumpSkip.performed += ctx => Jump();
    }

    void Jump()
    {
        if(isGrounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
        }
        isGrounded = false;
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Ground":
            isGrounded = true;
            break;

            case "DBS":
            isGrounded = true;
            break;

            case "EnemLaser":
            p20Health -= 1;
            healthBarFlash.SetActive(true);
            healthImageFlash.SetActive(true);
            Invoke("offHit", .5f);
            p20PlayerHit.Play();
            break;

            case "Enem":
            p20Health -= 10;
            healthBarFlash.SetActive(true);
            healthImageFlash.SetActive(true);
            Invoke("offHit", .5f);
            p20PlayerHit.Play();
            break;
        }
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Enem") 
	    {

            p20Health -= 10;
            healthBarFlash.SetActive(true);
            healthImageFlash.SetActive(true);
            p20PlayerHit.Play();
            Invoke("offHit", .5f);
        }

        if (col.gameObject.tag == "EnemLaser") 
	    {
            p20Health -= 1;
            healthBarFlash.SetActive(true);
            healthImageFlash.SetActive(true);
            p20PlayerHit.Play();
            Invoke("offHit", .5f);
        }

        if (col.gameObject.tag == "HU") 
	    {
            p20Health = 300;
            healthBarFlash.SetActive(true);
            healthImageFlash.SetActive(true);
            p20MenBut.Play();
            Invoke("offHit", .5f);
        }
    }

    void djRight()
    {
        rightDJPos = transform.position;
        rightDJPos += new Vector2(.6f, .2f);
        Instantiate(rightDJ, rightDJPos, Quaternion.identity);
    }

    void djLeft()
    {
        leftDJPos = transform.position;
        leftDJPos += new Vector2(-.6f, .2f);
        Instantiate(leftDJ, leftDJPos, Quaternion.identity);
    }

    void reSpawn()
    {
        transform.position = spawnPos1;
    }

    void reHealth()
    {
        p20Health = 300;
    }

    void offHit()
    {
        healthBarFlash.SetActive(false);
        healthImageFlash.SetActive(false);
    }

    void FlipPlayer()
    {
        fRight = !fRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    
    void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}

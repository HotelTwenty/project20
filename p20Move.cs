using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class p20Move : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public float xVal;
    public float moveX;
    public static bool isOnGround;

    bool isFacingRight = false;

    float nextFlame = 0.0f;
    float flameRate = 0.005f;

    float nextFire = 0.0f;
    float fireRate = 0.1f;

    bool isFiringRight = false;
    bool isFiringLeft = false;
    bool FlameFireOn = false;

    public GameObject fRight, fRightUp, fRightdown, fLeft, fLeftUp, fLeftDown;
    public GameObject stdRight, stdRightUp, stdRightdown, stdLeft, stdLeftUp, stdLeftDown;
    Vector2 rightBull, rightBullDown, rightBullUp, leftBull, leftBullDown, leftBullUp;
    Vector2 STDrightBull, STDrightBullDown, STDrightBullUp, STDleftBull, STDleftBullDown, STDleftBullUp;

    public GameObject upJ;
    Vector2 upJPos;

    float nextUpJ = 0.0f;
    float upJRate = 0.001f;
    public AudioSource p20playerShoot;
    public static bool canMove;

    public bool fngRight, fngRightDown, fngRightUp, fngLeft, fngLeftDown, fngLeftUp;


    void Awake()
    {
        controls = new PlayerControls();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
        
        fngRight = true;
        fngRightDown = false;
        fngRightUp = false;
        fngLeft = false;
        fngLeftDown = false;
        fngLeftUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        FlameFireOn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isOnGround = p20MainControl.isGrounded;
        isFacingRight = p20MainControl.fRight;
        canMove = p20MainControl.cMove;
        xVal = move.x;
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m * 0, Space.World);
        KMove();

        if(fngRight == true)
        {
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngRightDown == true)
        {
            fngRight = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngRightUp == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngLeft == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngLeftDown == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftUp = false;
        }

        if(fngLeftUp == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
        }
    }

    void Fire()
    {
        if(fngRightDown == true)
        {
            STDFireRightDown();
        }

        else if(fngRightUp == true)
        {
            STDFireRightUp();
        }

        else if(fngLeft == true)
        {
            STDFireLeft();
        }

        else if(fngLeftDown == true)
        {
            STDFireLeftDown();
        }

        else if(fngLeftUp == true)
        {
            STDFireLeftUp();
        }

        else if(fngRight == true)
        {
            STDFireRight();
        }
    }

    void Fire2()
    {
        if(fngRightDown == true)
        {
            flameFireRightDown();
        }

        else if(fngRightUp == true)
        {
            flameFireRightUp();
        }

        else if(fngLeft == true)
        {
            flameFireLeft();
        }

        else if(fngLeftDown == true)
        {
            flameFireLeftDown();
        }

        else if(fngLeftUp == true)
        {
            flameFireLeftUp();
        }

        else if(fngRight == true)
        {
            flameFireRight();
        }
    }

    void KMove()
    {
        bool isLeftKeyHeld = controls.GamePlay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.GamePlay.MoveRight.ReadValue<float>() > 0.1f;
        bool isUpKeyHeld = controls.GamePlay.MoveUp.ReadValue<float>() > 0.1f;
        bool isDownKeyHeld = controls.GamePlay.MoveDown.ReadValue<float>() > 0.1f;
        bool isFire1KeyHeld = controls.GamePlay.Shot.ReadValue<float>() > 0.1f;
        bool isFire2KeyHeld = controls.GamePlay.ScndShot.ReadValue<float>() > 0.1f;

        if(isFire1KeyHeld == true)
        {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }

        if(FlameFireOn == true)
        {
            if(isFire2KeyHeld == true)
            {
                if(Time.time > nextFlame)
                {
                    nextFlame = Time.time + flameRate;
                    Fire2();
                }
            }
        }
        
        if(canMove == true)
        {
            if(isRightKeyHeld || move.x > 0.1f)
            {
                if(isUpKeyHeld || move.y > 0.1f)
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagUp", true);
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = true;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }

                else if(isDownKeyHeld || move.y < -0.1f)
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagDown", true);
                    fngRight = false;
                    fngRightDown = true;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagDown", false);
                    GetComponent<Animator> ().SetBool ("isRunningDiagUp", false);
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                    if(isFacingRight == true)
                    {
                        fngRight = true;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }
                        
                    else
                    {
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = true;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }
                }
            }

            else if(isLeftKeyHeld || move.x < -0.1f)
            {
                if(isUpKeyHeld || move.y > 0.1f)
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagUp", true);
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = true;
                }

                else if(isDownKeyHeld || move.y < -0.1f)
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagDown", true);
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = true;
                    fngLeftUp = false;
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isRunningDiagDown", false);
                    GetComponent<Animator> ().SetBool ("isRunningDiagUp", false);
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                    if(isFacingRight == true)
                    {
                        fngRight = true;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }
                        
                    else
                    {
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = true;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }
                }
            }

            else
            {
                GetComponent<Animator> ().SetBool ("isRunning", false);
                GetComponent<Animator> ().SetBool ("isRunningDiagDown", false);
                GetComponent<Animator> ().SetBool ("isRunningDiagUp", false);
                if(isFacingRight == true)
                {
                    fngRight = true;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }

                else
                {
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = true;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }
            }                                               
        }
    }

    //flame fire
    //==============================================================================================================================================================
    void flameFireRight()
    {
        rightBull = transform.position;
        rightBull += new Vector2(.6f, 0f);
        Instantiate(fRight, rightBull, Quaternion.identity);
    }

    void flameFireRightUp()
    {
        rightBullUp = transform.position;
        rightBullUp += new Vector2(.6f, .6f);
        Instantiate(fRightUp, rightBullUp, Quaternion.identity);
    }

    void flameFireRightDown()
    {
        rightBullDown = transform.position;
        rightBullDown += new Vector2(.6f, -.6f);
        Instantiate(fRightdown, rightBullDown, Quaternion.identity);
    }

    void flameFireLeft()
    {
        leftBull = transform.position;
        leftBull += new Vector2(-.6f, 0f);
        Instantiate(fLeft, leftBull, Quaternion.identity);
    }

    void flameFireLeftDown()
    {
        leftBullDown = transform.position;
        leftBullDown += new Vector2(-.6f, -.6f);
        Instantiate(fLeftDown, leftBullDown, Quaternion.identity);
    }

    void flameFireLeftUp()
    {
        leftBullUp = transform.position;
        leftBullUp += new Vector2(-.6f, .6f);
        Instantiate(fLeftUp, leftBullUp, Quaternion.identity);
    }

    //Std fire
    //========================================================================================================================================
    void STDFireRight()
    {
        p20playerShoot.Play();
        STDrightBull = transform.position;
        STDrightBull += new Vector2(.6f, 0f);
        Instantiate(stdRight, STDrightBull, Quaternion.identity);
    }

    void STDFireRightUp()
    {
        p20playerShoot.Play();
        STDrightBullUp = transform.position;
        STDrightBullUp += new Vector2(.6f, .6f);
        Instantiate(stdRightUp, STDrightBullUp, Quaternion.identity);
    }

    void STDFireRightDown()
    {
        p20playerShoot.Play();
        STDrightBullDown = transform.position;
        STDrightBullDown += new Vector2(.6f, -.6f);
        Instantiate(stdRightdown, STDrightBullDown, Quaternion.identity);
    }

    void STDFireLeft()
    {
        p20playerShoot.Play();
        STDleftBull = transform.position;
        STDleftBull += new Vector2(-.6f, 0f);
        Instantiate(stdLeft, STDleftBull, Quaternion.identity);
    }

    void STDFireLeftDown()
    {
        p20playerShoot.Play();
        STDleftBullDown = transform.position;
        STDleftBullDown += new Vector2(-.6f, -.6f);
        Instantiate(stdLeftDown, STDleftBullDown, Quaternion.identity);
    }

    void STDFireLeftUp()
    {
        p20playerShoot.Play();
        STDleftBullUp = transform.position;
        STDleftBullUp += new Vector2(-.6f, .6f);
        Instantiate(stdLeftUp, STDleftBullUp, Quaternion.identity);
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Enem") 
	    {
            isOnGround = true;
        }
    }
//jets
//=================================================================================
    void UpJet()
    {
        upJPos = transform.position;
        upJPos += new Vector2(0f, .6f);
        Instantiate(upJ, upJPos, Quaternion.identity);
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

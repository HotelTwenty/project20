using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Head : MonoBehaviour
{
    public GameObject Laser, MineSpawner, BDest1, BDest2, BDest3, BDest4, BDest5, DC, eye1, eye2, BossSM;
    Vector2 MineSpawnerPos, BDest1Pos, BDest2Pos, BDest3Pos, BDest4Pos, BDest5Pos, DCPos;
    float nextFire = 0.0f;
    float destRate = 3f;

    float nextMine = 0.0f;
    float mineRate = 10f;
    public static int B2Health;
    public int currHealth;
    public static int p20H;
    public AudioSource p20BossHit;

    // Start is called before the first frame update
    void Start()
    {
        B2Health = 33;    
        Laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        p20H = p20MainControl.p20Health;
        currHealth = B2Health;
        if(p20H <= 0)
        {
            B2Health = 33;
        }

        if(B2Health < 30 && B2Health > 0)
        {
            Laser.SetActive(true);
            if(B2Health < 100 && B2Health > 0)
            {
                if (Time.time > nextMine)
                {
                    nextMine = Time.time + mineRate;
                    MSpawner();
                }
            }
        }


        if(B2Health <= 0)
        {
            eye1.SetActive(false);
            eye2.SetActive(false);
            BossSM.SetActive(false);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + destRate;
                Invoke("D1", 1f);
                Invoke("D2", 2f);
                Invoke("D3", 3f);
                Invoke("D4", 4f);
                Invoke("D5", 5f);

                Invoke("D1", 6f);
                Invoke("D2", 7f);
                Invoke("D3", 8f);
                Invoke("D4", 9f);
                Invoke("D5", 10f);

                Invoke("D1", 10.5f);
                Invoke("D2", 11f);
                Invoke("D3", 11.5f);
                Invoke("D4", 12f);
                Invoke("D4", 12.5f);

                Invoke("D1", 13f);
                Invoke("D2", 13.5f);
                Invoke("D3", 14f);
                Invoke("D4", 14.5f);
                Invoke("D5", 15f);

                Invoke("D1", 15.5f);
                Invoke("D2", 16f);
                Invoke("D3", 16.5f);
                Invoke("D4", 17f);
                Invoke("D4", 17.5f);
            }
            Invoke("DeathCircle", 18f); 
            Destroy(gameObject, 18.2f);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "NormBull") 
	    {
            B2Health -= 3;
            GetComponent<Animator> ().SetBool ("isHit", true);
            Invoke("offHit", .5f);
            p20BossHit.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "FlameBull":
            B2Health -= 1;
            GetComponent<Animator> ().SetBool ("isHit", true);
            Invoke("offHit", .5f);
            p20BossHit.Play();
            break;
        }
    }

    void offHit()
    {
        GetComponent<Animator> ().SetBool ("isHit", false);
    }

    void DeathCircle()
    {
        DCPos = transform.position;
        DCPos += new Vector2(0f, 0f);
        Instantiate(DC, DCPos, Quaternion.identity);
    }

    void D1()
    {
        BDest1Pos = transform.position;
        BDest1Pos += new Vector2(0f, 0f);
        Instantiate(BDest1, BDest1Pos, Quaternion.identity);
    }

    void D2()
    {
        BDest2Pos = transform.position;
        BDest2Pos += new Vector2(-8f, -8f);
        Instantiate(BDest2, BDest2Pos, Quaternion.identity);
    }

    void D3()
    {
        BDest3Pos = transform.position;
        BDest3Pos += new Vector2(8f, 8f);
        Instantiate(BDest3, BDest3Pos, Quaternion.identity);
    }

    void D4()
    {
        BDest4Pos = transform.position;
        BDest4Pos += new Vector2(-7f, 8f);
        Instantiate(BDest4, BDest4Pos, Quaternion.identity);
    }

    void D5()
    {
        BDest5Pos = transform.position;
        BDest5Pos += new Vector2(8f, -7f);
        Instantiate(BDest5, BDest5Pos, Quaternion.identity);
    }

    void MSpawner()
    {
        MineSpawnerPos = transform.position;
        MineSpawnerPos += new Vector2(0f, -7f);
        Instantiate(MineSpawner, MineSpawnerPos, Quaternion.identity);
    }
}

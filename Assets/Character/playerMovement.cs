using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public static playerMovement instance
    {
        get;
        set;
    }
    public int noJumps;
    public int health = 5;
    public Rigidbody2D rbPlayer;
    BoxCollider2D bcPlayer;
    [SerializeField] float speed = 20.0f;
    [SerializeField] public float jumpForce = 10.0f;
    float timeTojump;
    public ParticleSystem dust;
    [SerializeField] float fallMultiplier = 150f;
    bool isRight = true;
    bool isJumping = false;
    public Animator anim;
    [SerializeField] LayerMask floor;
    public bool isAttacking = false;
    public ParticleSystem slash;
    bool z = false;
    public int combo = 0;
    public GameObject[] swordSwinging = new GameObject[3];
    public HealthBar hpPlayer;
    public Joystick controller;
    public GameObject deathMenu;
    float walkingSoundTime;
    public GameObject explode;
    public static bool winGame = false;
    void Start()
    {
        winGame = false;
        hpPlayer.setMaxValue(health);
        instance = this;
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bcPlayer = GetComponent<BoxCollider2D>();
        noJumps = 0;
        for (int i = 0; i < swordSwinging.Length; i++)
        {
            swordSwinging[i].SetActive(false);
        }
    }
    void FixedUpdate()
    {       
        if(health == 0 && !winGame)
        {
            isAttacking = true;
            Time.timeScale = 0.5f;
            Instantiate(explode, transform.position, Quaternion.identity);
            this.GetComponent<SpriteRenderer>().enabled = false;
            Invoke("deathGame", 1f);
            
        }
            move();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("floor") && !isAttacking)
        {
            createDust();
        }
    }
    void jumpAnimTrue()
    {
        anim.SetBool("Jump", true);
    }
    public void move()
        {
            if ((Input.GetButtonDown("Jump") || controller.Vertical >0.5f))
            {
            Invoke("jumpAnimTrue", 0.05f);
            anim.SetBool("run", false);
            anim.SetBool("idle", false);
            if (isGrounded() && noJumps == 0 && !isJumping)
                {
                
                    rbPlayer.velocity = new Vector2(rbPlayer.velocity.x/10f, jumpForce*1.2f * controller.Vertical);

                }
            else if(!isGrounded() && noJumps == 1 && !isJumping)
                {
                    rbPlayer.velocity = new Vector2(rbPlayer.velocity.x/10f, jumpForce*1.5f);
                    anim.SetBool("JumpSecond", true);
                    isJumping = true;
                }
                noJumps++;
            }
            if ((Input.GetKey(KeyCode.D) || controller.Horizontal >0.2f) && !isAttacking)
            {
            
                rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
                if (!isRight)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    isRight = true;
                }
                if (isGrounded())
                {
                if (walkingSoundTime < Time.time)
                {
                    FindObjectOfType<AudioManager>().PlaySoundOneShot("Walking");
                    walkingSoundTime = Time.time + 0.15f;
                }
                    anim.SetBool("run", true);
                    anim.SetBool("Jump", false);
                    anim.SetBool("JumpSecond", false);
                    anim.SetBool("idle", false);
                    if (isAttacking == false && z == false)
                    {
                        anim.SetBool("runUp", true);
                    }   
                }
            }
            else if ((Input.GetKey(KeyCode.A) || controller.Horizontal<-0.2f) && !isAttacking)
            {
                rbPlayer.velocity = new Vector2(-speed, rbPlayer.velocity.y);
                if (isRight)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    isRight = false;
                }
                if (isGrounded())
                {
                    if (walkingSoundTime < Time.time)
                    {
                        FindObjectOfType<AudioManager>().PlaySoundOneShot("Walking");
                        walkingSoundTime = Time.time + 0.15f;
                    }
                anim.SetBool("run", true);
                    anim.SetBool("Jump", false);
                    anim.SetBool("JumpSecond", false);
                    anim.SetBool("idle", false);
                    if (isAttacking == false && z == false)
                    {
                        anim.SetBool("runUp", true);
                    }
                }
            }
            else if (isGrounded() && !isAttacking)
            {
                anim.SetBool("run", false);
                anim.SetBool("Jump", false);
                anim.SetBool("JumpSecond", false);
                anim.SetBool("idle", true);
                if (isAttacking == false && z == false)
                {               
                    anim.SetBool("runUp", false);
                }
            }
            if (rbPlayer.velocity.y < .1 && isGrounded())
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fallMultiplier * Physics2D.gravity.y);
            }
            if (noJumps > 0 && isGrounded())
            {
                noJumps = 0;
            }
    }
    bool isGrounded()
    {
        RaycastHit2D rc = Physics2D.BoxCast(bcPlayer.bounds.center, bcPlayer.bounds.size, 0, Vector2.down, 0.05f, floor);
        if (rc.collider == null)
            return false;
        else
        {
            isJumping = false;
            return true;
        }
    }
    public void attackWithSlash()
    {
        if (!isAttacking)
        {
            FindObjectOfType<AudioManager>().PlaySoundOneShot("Attacking");
            z = true;
            isAttacking = true;
            anim.SetBool("dash", true);
            anim.SetBool("attack3", true);
            rbPlayer.velocity = new Vector2(transform.localScale.x * speed * 3.5f, rbPlayer.velocity.y);
            for (int i = 0; i < swordSwinging.Length; i++)
            {
                swordSwinging[i].SetActive(true);
            }
            Invoke("trailFalse", 0.35f);
            Invoke("Zfalse", 0.6f);
        }
    }
    public void normalAttack()
    {
        if (!isAttacking)
        {
            FindObjectOfType<AudioManager>().PlaySoundOneShot("Attacking");
            if (combo == 2)
            {
                combo = 0;
            }
            isAttacking = true;
            rbPlayer.velocity = Vector2.left * 0;
            anim.SetBool("dash", true);
            anim.SetBool("attack", true);
            anim.SetInteger("combo", combo++);
            rbPlayer.velocity = new Vector2(transform.localScale.x * speed * 3.5f, rbPlayer.velocity.y);
            for (int i = 0; i < swordSwinging.Length; i++)
            {
                swordSwinging[i].SetActive(true);
            }
            if (combo == 1)
            {
                Invoke("trailFalse", 0.25f);
            }
            if(combo == 2)
            {
                Invoke("trailFalse", 0.15f);
            }
        }
    }
    void trailFalse()
        {
        for (int i = 0; i < swordSwinging.Length; i++)
        {
            swordSwinging[i].SetActive(false);
        }
        anim.SetBool("dash", false);
        anim.SetBool("attack3", false);
        anim.SetBool("attack", false);
        isAttacking = false;
        }
    void comboInc()
        {
            combo++;
        }
    void comboDec()
        {
            combo--;
        }
    void Zfalse()
        {
            z = false;
        }
    void jumpingFalse()
        {
            isJumping = false;
        }
    void createDust()
        {
            dust.Play();
            if (transform.localScale.x < 0 && dust.transform.localScale.x > 0)
            {
                dust.transform.localScale = new Vector3(-dust.transform.localScale.x, dust.transform.localScale.y, dust.transform.localScale.z);
            }
            else if (transform.localScale.x > 0 && dust.transform.localScale.x < 0)
            {
                dust.transform.localScale = new Vector3(-dust.transform.localScale.x, dust.transform.localScale.y, dust.transform.localScale.z);
            }
        }
    void deathGame()
    {   
        Destroy(this.gameObject);
        deathMenu.SetActive(true);
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().pauseSounds();
    }
}

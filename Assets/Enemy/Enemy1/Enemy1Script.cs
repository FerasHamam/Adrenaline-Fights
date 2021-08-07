using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public static Enemy1Script instance
    {
        set;
        get;
    }
    public float speed;
    public float shootingRange;
    float nextTimeFire;
    public int healthE = 20;
    [SerializeField] GameObject startingPoint;
    public GameObject bullet;
    private Transform player;
    private Rigidbody2D enemy;
    public int combo = 0;
    float timeToAttack = Time.time;
    public EnemyHealthBar ehm;
    public Animator anim;
    bool enemyIsAttacking = false;
    public GameObject winMenu;
    public GameObject blood;
    public GameObject bloodEnd;
    void Start()
    {
        ehm.setMaxHealth(healthE);
        player = GameObject.FindGameObjectWithTag("player").transform;
        enemy = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if(transform.position.x< player.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if (Vector2.Distance(transform.position, player.position) > shootingRange && !enemyIsAttacking)
        {
            anim.Play("move");
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x,player.position.y+11f,player.position.z), speed * Time.deltaTime);
        }
        else if (nextTimeFire < Time.time && playerMovement.instance.health >0)
        {
            
            Invoke("fire", 0f);
            Invoke("fire", 0.5f);
            Invoke("fire", 1f);
            nextTimeFire = Time.time + 3f;
            combo = 0;
        }
        else if (!enemyIsAttacking)
        {
            anim.Play("Enemy1idle");
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sword") && playerMovement.instance.isAttacking && timeToAttack < Time.time)
        {
            FindObjectOfType<AudioManager>().playSound("takeDamage");
            healthE--;
            ehm.takeDamage();
            enemy.velocity = new Vector2(player.localScale.x * speed * 3f, enemy.velocity.y);
            Instantiate(blood, new Vector3(transform.position.x + (6f * player.localScale.x), transform.position.y, transform.position.z), Quaternion.identity);
            Invoke("hitSpeedFalse", 0.2f);
            cameraShake.instacne.shakeCamera(3.5f, 0.35f);
            if (healthE == 0)
            {
                playerMovement.winGame = true;
                Time.timeScale = 0.5f;
                Instantiate(bloodEnd, new Vector3(transform.position.x + (7f * player.localScale.x), transform.position.y-2f, transform.position.z), Quaternion.identity);
                this.GetComponent<SpriteRenderer>().enabled = false;
                nextTimeFire = 10000000f;
                Invoke("winGame", 1f);
            }
            timeToAttack = Time.time + 0.2f;
        }
    }
    void fire()
    {   
        {
            FindObjectOfType<AudioManager>().PlaySoundOneShot("EnemyAttack");
            enemyIsAttacking = true;
            Invoke("attackFalse",0.3f);
            anim.Play("Enemy1Attack");
            Instantiate(bullet, startingPoint.transform.position, Quaternion.identity);
        }
        combo++;
    }
    void attackFalse()
    {
        enemyIsAttacking = false;
    }
    void hitSpeedFalse()
    {
        enemy.velocity = Vector2.zero;
    }
    void winGame()
    {
        Time.timeScale = 1f;
        winMenu.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class BulletScript : MonoBehaviour
{   
    public float speed;
    public float timeToDestroy;
    GameObject player;
    Animator anim;
    Rigidbody2D bullet;
    Vector2 moveTo;
    public GameObject bloodPlayer;

    void Start()
    {   
        bullet = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");
        moveTo = (player.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(moveTo.x, moveTo.y);
        timeToDestroy = Time.time + timeToDestroy;
        
    }
     public void FixedUpdate()
     {
        if (timeToDestroy < Time.time)
        {
            Destroy(this.gameObject, 0f);
        }
     }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player"))
        {   if (!playerMovement.instance.isAttacking && playerMovement.instance.health>0)
            {
                
                FindObjectOfType<AudioManager>().playSound("takeDamage");
                playerMovement.instance.health--;
                cameraShake.instacne.shakeCamera(3.5f, .35f);
                playerMovement.instance.rbPlayer.velocity = new Vector2(moveTo.x * 5, playerMovement.instance.rbPlayer.velocity.y);
                Instantiate(bloodPlayer, new Vector3(transform.position.x ,transform.position.y,transform.position.z), Quaternion.identity);
            }
            Destroy(this.gameObject, 0f);
        }
        else if(collision.CompareTag("sword") && playerMovement.instance.isAttacking)
        {   
            Destroy(this.gameObject, 0f);
        }
    }
}


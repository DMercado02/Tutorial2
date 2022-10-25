using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    public Text winText;
    public GameObject winTextObject;
    public Text loseText;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool gameOver;
    Animator anim;
    private bool facingRight = true;
    private bool isJumping;


    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
       rd2d = GetComponent<Rigidbody2D>(); 
       score.text = scoreValue.ToString();
       lives.text = livesValue.ToString();
       winTextObject.SetActive(false);
       loseTextObject.SetActive(false);
       musicSource.clip = musicClipOne;
       musicSource.Play();
       musicSource.loop = true;
       gameOver = false;
    }

    // Update is called once per frame

void FixedUpdate()
    {
       float hozMovement = Input.GetAxis("Horizontal");
       float verMovement = Input.GetAxis("Vertical");

       rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
       if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
         else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
   if (isJumping == true && verMovement > 0)
   {
     anim.SetInteger("State",2);
   }
      else if (isJumping == true && verMovement == 0)
   {
     anim.SetInteger("State",0);
   }
    }
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.D))
       {
         anim.SetInteger("State",1);
         isJumping = false;
       }

       if(Input.GetKeyUp(KeyCode.D))
       {
         anim.SetInteger("State",0);
       }
       if(Input.GetKeyDown(KeyCode.A))
       {
         anim.SetInteger("State",1);
         isJumping = false;
       }

       if(Input.GetKeyUp(KeyCode.A))
       {
         anim.SetInteger("State",0);
       }

       if(Input.GetKeyDown(KeyCode.W))
       {
         isJumping = true;
       }
       if(Input.GetKeyUp(KeyCode.W))
       {
         isJumping = false; 
       }
       

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.tag == "Coin")
         {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
            
            if (scoreValue == 4)
            {
               gameObject.transform.position = new Vector2(45f,0f);
               livesValue = 3;
               lives.text = livesValue.ToString();
            }
            
            
            if (scoreValue == 8 & gameOver == false)
            {
               musicSource.clip = musicClipTwo;
               musicSource.Play();
               musicSource.loop = false;
               winTextObject.SetActive(true);
               gameOver = true;
            }
         }

         if (collision.collider.tag == "Enemy")
         {
            livesValue -=1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);

            if (livesValue == 0)
            {
               Destroy(gameObject);
               loseTextObject.SetActive(true);
            }
         }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
      if (collision.collider.tag == "Ground") 
      {
         
         if(Input.GetKey(KeyCode.W))
         {
            rd2d.AddForce(new Vector2(0, 2),ForceMode2D.Impulse);
         }
      }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}



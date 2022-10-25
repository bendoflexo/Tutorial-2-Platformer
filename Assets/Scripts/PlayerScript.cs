using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text livesCount;
    private int scoreValue;
    private int lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool facingRight = true;
    public Animator anim;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        

        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        scoreValue = 0;
        lives = 3;
        SetLifeText();

        musicSource.clip = musicClipOne;
        musicSource.Play();
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));




        

    }

    void Update()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical"); 

       


        if(hozMovement != 0 && verMovement == 0)
        {
            anim.SetInteger("State",1);
        }

        if(hozMovement == 0 && verMovement == 0)
        {
             anim.SetInteger("State",0);
        }

        if (facingRight == false && hozMovement > 0)
    {
        Flip();
    }
        else if (facingRight == true && hozMovement < 0)
    {
        Flip();
    }

        if(isGrounded == false && (verMovement * speed) > 0)
        {
            anim.SetInteger("State",2);
        }

        if(isGrounded == false && verMovement * speed< 0)
        {
            anim.SetInteger("State",2);
        }
        

        
    }

    
void SetLifeText()
    {
        livesCount.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {

            loseTextObject.SetActive(true);
            Destroy(rd2d);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
            lives = 3;
            livesCount.text = "Lives: " + lives.ToString();
            livesCount.text = livesCount.ToString();
            SetLifeText();

            transform.position = new Vector2(50.0f, 0.5f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            
            }
                
            

            else if (scoreValue == 8)
            {
                musicSource.clip = musicClipOne;
                musicSource.Stop();

                musicSource.clip = musicClipTwo;
                musicSource.Play();

                winTextObject.SetActive(true);
                Destroy(rd2d);
            }
           
        }
        if(collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesCount.text = livesCount.ToString();
            Destroy(collision.collider.gameObject);

            SetLifeText();
        }
    }

    

   
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
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

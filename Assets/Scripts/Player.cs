
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Text currentScore;
    public Text highScore;
    int Score = 0;
    public Animator anim;
    public float jumpPower = 1000f;
    Rigidbody2D myRigidbody;
    bool isGround = false;
    bool isGameOver = false;
    public float xPos = -8.98f;
    public float xPosition = 2.425821f;
    public float yPosition = 4.543404f;
    private AudioSource GameOverSound;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        GameOverSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {

        if (!isGameOver)
        {
            Score++;
            currentScore.text = Score.ToString();
        }

        if (Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }


        if (isGameOver) return;

        if (transform.position.x < xPos)
        {
            GameOver();
            GameOverSound.Play();
        }


        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            myRigidbody.AddForce(Vector2.up * jumpPower * Time.deltaTime * myRigidbody.mass * myRigidbody.gravityScale);
            anim.SetBool("Jump", true);


        }

        if (Input.GetKey(KeyCode.DownArrow) && isGround)
        {
            
            gameObject.GetComponent<BoxCollider2D>().size = new Vector3(xPosition, 2);
            myRigidbody.AddForce(Vector2.down * jumpPower * Time.deltaTime * myRigidbody.mass * myRigidbody.gravityScale);
            anim.SetBool("MoveDown", true);
        
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector3(xPosition,yPosition);
            anim.SetBool("MoveDown", false);
        }

    }
          private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground")
            {
                isGround = true;
            anim.SetBool("Jump", false);
            }
        }
          private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground")
            {
                isGround = false;
            anim.SetBool("Jump", true);
        }
        }
         private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground")
            {
                isGround = true;
            anim.SetBool("Jump", false);
        }
        }
       public void GameOver()
    {
        isGameOver = true;
        FindObjectOfType<ChallengeScroller>().GameOver();
        FindObjectOfType<Scroll>().xVel = 0f;
        FindObjectOfType<FlyScroll>().GameOver();
        anim.SetBool("GameOver", true);
        FindObjectOfType<Score>().GameOver();
    }
    }


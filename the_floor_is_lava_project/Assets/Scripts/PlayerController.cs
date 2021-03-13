using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    MoveGroup[] moveGroup;

    [Header("CHARACTER")]
   
    [SerializeField] public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public bool isground;
    bool isDead;

        

    [Header("SCORE")]
    public LayerMask objectLayer;
    public float distance;
    bool alreadyHit;

                          
    [Header("HUD")]
    public GameObject gameOver;
    public GameObject UI;
    public GameObject scoreInGame;
    public GameObject scoreGameOver;
    public GameObject bestScoreGameOver;
    public GameObject bestScoreMenu;
    public GameObject Lifess;
    public GameObject Coinss;
    [SerializeField] private Text scoreInGameText;
    public int scoreInGamee = 0;

    [SerializeField] private Text scoreGameOverText;
    public static int scoreGameOverr = 0;
  
    [SerializeField] private Text bestScoreGameOverText;
    public static int bestScoreGameOverr = 0;

    [SerializeField] private Text bestScoreMenuText;
    public static int bestScoreMenuu = 0;

    [SerializeField] private Text lifesText;
    [SerializeField] private Text coinsText;
    public static int coins = 0;
    public static int lifes = 0;
    public AudioClip getLife;
    public AudioClip loseLife;
    public AudioClip wiithoutLife;
    public AudioClip getCoin;


    [Header("JUMP")]
    public float speed = 0f;
    public float jumpspeed;
    

    [Header("TIMELAVA")]
    
    public float timeForLava;
    public float timeToLava;
    public GameObject lavaLogo;
    public GameObject partBackOne;
    public GameObject partBackOTwo;
    public AudioClip theFloorIsLavaSFX;

    public void StartGame()
    {
        for (int i = 0; i <moveGroup.Length; i++)
        {
            moveGroup[i].enabled = true;
        }
         StartCoroutine(isLava());

    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
       
        moveGroup = FindObjectsOfType<MoveGroup>();
        scoreInGamee = 0;
        lifes = 0;
        bestScoreGameOverText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        bestScoreMenuText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
             
                
    }

           
    void Update()
    {
        if(isDead)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Hit())
        {
            if (!alreadyHit)
            {
                Score();
                alreadyHit = true;
            }
        }
        else
            alreadyHit = false;
               
        lifesText.text = lifes.ToString();
        coinsText.text = coins.ToString();
            
    }

   
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
    void Jump()
    {
        if(isground)
        {
            myRigidbody.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            
        } 
        myAnimator.Play("jump");              
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            coins += 1;
            AudioManager.PlaySFX(getCoin, 1);
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Life"))
        {
            lifes +=1 ;
            AudioManager.PlaySFX(getLife, 1);
            collision.gameObject.SetActive(false);
        }
        
        if(collision.gameObject.CompareTag("CameraDeath"))
        {
            lifes = -1;
            AudioManager.PlaySFX(wiithoutLife, 1);
            Death();         
            
        }

        if(collision.gameObject.layer == 9)
        {
            lifes--;
            AudioManager.PlaySFX(loseLife, 1);
            
            if (lifes == -1)
            {
                Death();
            }     
        }   
    }

    public void SetOnGround(bool ground)
    {
        if(ground)
        {
            isground = true;
        }
        else
        {
            isground = false;
        }
    }

    bool Hit()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, distance, objectLayer);

        return hitInfo;
    }

    IEnumerator isLava()
    {
        yield return new WaitForSeconds(timeToLava);
        lavaLogo.SetActive(true);
        AudioManager.PlaySFX(theFloorIsLavaSFX, 1);
        partBackOne.SetActive(false);
        partBackOTwo.SetActive(true);

        StartCoroutine(isNoLava());
    }
    
    IEnumerator isNoLava()
    {
        yield return new WaitForSeconds(timeForLava);
        lavaLogo.SetActive(false);
        partBackOne.SetActive(true);
        partBackOTwo.SetActive(false);

        StartCoroutine(isLava());
    }

    void Score()
    {
        scoreInGamee += 1;
        scoreInGameText.text = scoreInGamee.ToString();
        scoreGameOverText.text = scoreInGameText.text;
       
        if(scoreInGamee > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", scoreInGamee);
            bestScoreGameOverText.text = scoreInGamee.ToString();
            bestScoreMenuText.text = scoreInGamee.ToString();

        }
    }
       
    
     public void Death()
    {
        isDead = true;
        gameOver.SetActive(true);
        MoveGroup[] moveGroups = FindObjectsOfType<MoveGroup>();
        
        for (int i = 0; i < moveGroups.Length; i++)
        {
            moveGroups[i].enabled = false;
        }
        StopAllCoroutines();
    
         
    }

}

  
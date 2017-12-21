using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    public float speed;

    private Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public GameObject resetBtn;

    private int score=0;

    public Text ScoreText,newHighScore;

    public Animator gameOverAnim;

    public Image background;

    public Text[] ScoreTexts;

    public LayerMask wathIsGround;

    private bool isPlaying=false;

    public Transform contactPoint;

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            
            return instance;
        }

    }


    // Use this for initialization
    void Start () {

        isDead = false;
        dir = Vector3.zero;

        
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsGrounded()&& isPlaying)
        {
            isDead = true;

            GameOver();


            resetBtn.SetActive(true);
            if (transform.childCount > 0)
            {
                transform.GetChild(0).transform.parent = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            isPlaying = true;

            score++;
            ScoreText.text = score.ToString();

            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }
        }



        //for Touch!

        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began && !isDead)
        //    {
        //        isPlaying = true;

        //        score++;
        //        ScoreText.text = score.ToString();

        //        if (dir == Vector3.forward)
        //        {
        //            dir = Vector3.left;
        //        }
        //        else
        //        {
        //            dir = Vector3.forward;
        //        }
        //    }
        //}



        float amountTomove = speed * Time.deltaTime;

        transform.Translate(dir * amountTomove);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score+=3;
            ScoreText.text = score.ToString();

        }
    }



    private void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");

        ScoreTexts[1].text = score.ToString();
        
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            newHighScore.gameObject.SetActive(true);
            background.color = new Color32(243, 65, 246, 255);

            foreach (Text txt in ScoreTexts)
            {
                txt.color=Color.white;
            }
        }
        
        ScoreTexts[3].text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        
    }

    private bool IsGrounded()
    {
        Collider[] Colliders = Physics.OverlapSphere(contactPoint.position, .5f, wathIsGround);

        for(int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void OnExit()
    {
            Application.Quit();
    }
    
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public Ball ball{get; private set;}
    public Paddle paddle{get; private set;}

    public Brick[] bricks { get; private set; }


    public int level = 1;
    public int score = 0;
    public int lives = 3; 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoad;
    }

    private void Start()
    {
        NewGame();
        this.LivesText.text = "Lives:" + this.lives.ToString();
    }


    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        if(level > 3)
        {
         SceneManager.LoadScene("WinScreen");
        }
        else { 
        SceneManager.LoadScene("level"+level);
        }
    }

    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();

    }

    private void RestartLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Miss()
    {
        this.lives--;
        this.LivesText.text = "Lives:" + this.lives.ToString();
        if (this.lives > 0)
        {
            RestartLevel();
        }
        else
        {
            GameOver();
        }
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;
        this.ScoreText.text = "Scores:" +this.score.ToString() ;
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length;i++)
          {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
          }
        return true;
    }
}

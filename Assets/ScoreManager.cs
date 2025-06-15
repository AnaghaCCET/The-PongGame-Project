using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI winText;

    public Transform ball;
    public Ball ballScript;

    private int leftScore = 0;
    private int rightScore = 0;

    public int winningScore = 10;

    public void GoalScored(int player)
    {
        if (player == 1)
            leftScore++;
        else if (player == 2)
            rightScore++;

        UpdateScore();

        if (leftScore >= winningScore)
        {
            winText.text = "Left Player Wins!";
            EndGame();
        }
        else if (rightScore >= winningScore)
        {
            winText.text = "Right Player Wins!";
            EndGame();
        }
        else
        {
            ResetBall();
        }
    }

    void UpdateScore()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    void ResetBall()
    {
        ball.position = Vector3.zero;
        ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Invoke(nameof(LaunchBallDelayed), 1f);
    }

    void LaunchBallDelayed()
    {
        ballScript.LaunchBall();
    }

    void EndGame()
    {
        ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        winText.gameObject.SetActive(true);
        ballScript.enabled = false;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
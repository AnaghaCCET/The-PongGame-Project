using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public int scoringPlayer; // 1 = Left, 2 = Right
    public ScoreManager scoreManager; // Set via Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            scoreManager.GoalScored(scoringPlayer);
        }
    }
}
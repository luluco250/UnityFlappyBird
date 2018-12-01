using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    private static ScoreSystem instance;
    public static ScoreSystem Get() {
        return instance;
    }
    
    private static int score = 0;
    private static int highscore = 0;

    public Text scoreText;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning(
                "There should be only ONE instance of " + 
                "ScoreSystem. Destroying this instance..."
            );
            DestroyImmediate(this);
            return;
        }

        instance = this;
    }

    void Start() {
        highscore = PlayerPrefs.GetInt("highscore");
        ResetScore();
    }

    public static void AddPoint() {
        ++score;
        highscore = Mathf.Max(score, highscore);
        Get().scoreText.text = $"Score: {score}\nHighscore: {highscore}";
    }

    public static int GetScore() {
        return score;
    }

    public static void ResetScore() {
        score = 0;
        Get().scoreText.text = $"Score: 0\nHighscore: {highscore}";
    }

    public static void SaveHighScore() {
        PlayerPrefs.SetInt("highscore", highscore);
    }
}

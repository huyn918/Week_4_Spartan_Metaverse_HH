using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flappy_GameManager : MonoBehaviour
{
    static Flappy_GameManager gameManager;
    Flappy_UIManager uiManager;
    public static Flappy_GameManager Instance { get { return gameManager; } }


    private const string BestScoreKey = "BestScore";
    

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<Flappy_UIManager>();
    }

    private int currentScore = 0;

    public void Start()
    {
        uiManager.UpdateScoreText(0);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.RestartText();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TestSceneLoad()
    {
        SceneManager.LoadScene("SampleScene");
    }

    
    public IEnumerator Load_Completely_Than_Unload(string loadingScene, string unLoadingScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive); // Additive로 로드

        // 로드가 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 이제 안전하게 기존 씬 언로드
        SceneManager.UnloadSceneAsync(unLoadingScene);
    }
    public void AddScore(int score)
    {
        currentScore += score;
        int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        if (bestScore < currentScore)
        {
            Debug.Log("최고 점수 갱신");
            bestScore = currentScore; // 최고 점수 갱신시 PlayerPrefs에 저장 : 바깥 신에서 확인 가능
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            Debug.Log(bestScore);
        }
        uiManager.UpdateScoreText(currentScore);



    }
}

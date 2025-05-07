using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flappy_UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;


    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null) Debug.Log("scoreText NOT FOUND");
        if (restartText == null) Debug.Log("restartText NOT FOUND");

        restartText.gameObject.SetActive(false);
    }

    public void RestartText()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString() + " / 최고 점수 : " + PlayerPrefs.GetInt("BestScore",0).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI userGold;
    [SerializeField] private GameObject Earned_Gold_UI;
    [SerializeField] private TextMeshProUGUI howMuch;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    private void OnEnable()
    {
        Earned_Gold_UI.SetActive(false);
        controller.currentState = BaseController.PlayerState.Normal;
        characterName.text = PlayerPrefs.GetString("CharacterName", "noName");
        StartCoroutine(Earned_Gold_By_Game());
        UpdateCurrentGoldText();
    }

    public void UpdateCharacterName(string userInput)
    {
        characterName.text = userInput.ToString();
        PlayerPrefs.SetString("CharacterName",userInput); // 이름 기억
    }

    public void UpdateCurrentGoldText()
    {
        userGold.text = PlayerPrefs.GetInt("CurrentGold", 0).ToString();
    }

    private IEnumerator Earned_Gold_By_Game()
    {
        if (PlayerPrefs.GetInt("BestScore") > 0) // 미니게임으로 돈 벌었으면
        {
            Earned_Gold_UI.SetActive(true);
            howMuch.text = $"최고 기록 : {PlayerPrefs.GetInt("BestScore")}\n만큼 골드 획득!";
            int gold_Earned = PlayerPrefs.GetInt("BestScore");
            PlayerPrefs.SetInt("BestScore", 0); // 골드 벌었으니 기록 초기화
            PlayerPrefs.SetInt("CurrentGold", PlayerPrefs.GetInt("CurrentGold") + gold_Earned); // 골드 획득
            yield return new WaitForSeconds(3);
            // 3초 후 골드 획득한 것처럼 시각화
            userGold.text = PlayerPrefs.GetInt("CurrentGold").ToString();
            Earned_Gold_UI.SetActive(false);
        }
        yield break;
    }

    protected override UIState GetUIState()
    {
        return UIState.Main;
    }
}

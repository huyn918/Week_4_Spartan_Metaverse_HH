using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkUI : BaseUI
{
    private ObjectController objectController;
    private CanvasGroup canvasGroup;
    private MainUI mainUI;
    [SerializeField] public TextMeshProUGUI mainText;
    [SerializeField] public TextMeshProUGUI choiceText;
    [SerializeField] public GameObject nameInputCanvas;
    [SerializeField] public TMP_InputField inputField;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    private void OnEnable()
    {
        mainUI = FindAnyObjectByType<MainUI>();
        NameInputSwitch(false); // 일단 이름 입력받는 곳 끄기

        controller.currentState = BaseController.PlayerState.Talking; // 일단 못 움직이게 플레이어 상태 변경
        for (int i = 0; i < TagList.Count; i++) // 이렇게 태그 리스트 일일이 붙이지 말고 나중에는 scriptableObject?를 써보자
        {
            GameObject temp_obj = GameObject.FindGameObjectWithTag(TagList[i]); // 일단 태그로 검색. 상자가 삭제되기도 하므로 널체크 필요

            if (temp_obj != null) // 널이 아니면, 이제 게임오브젝트 태그와 비교
            {
                objectController = temp_obj.GetComponent<ObjectController>();
                if (objectController.targetTagName == TagList[i])
                {
                    StartCoroutine(TagList[i]);
                    break;
                }
            }
            Debug.Log($"{i}번째 태그 없음. 다음 태그로"); // 널이면 다음.
        }
    }

    private void OnDisable()
    {
        controller.currentState = BaseController.PlayerState.Normal;
        objectController.targetTagName = null;
    }
    // 대화창 UI가 뜰 때 Talking으로 바꿔서 조작이 안 되도록 하고, 꺼질 때 다시 Normal로 변환.
    private void Update()
    {
        
    }

    private IEnumerator Crate()
    {
        
        mainText.text = "상자를 열까?";
        choiceText.text = "E : 상자를 연다\nQ : 돌아가기";
        
        while (true)
        {
            // Q나 E 중 하나가 입력될 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            if (Input.GetKeyDown(KeyCode.Q))
            {
                uiManager.SetBacktoMain();
                yield break; // 입력 받고 코루틴 종료
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenCrate();
                yield break; // 입력 받고 코루틴 종료
            }

            yield return null; // 혹시 아무 입력 없으면 루프 계속 돌게. 없어도 될 것 같긴 하지만.
        }
    }

    protected void OpenCrate()
    {
        // 아직 아이템 못 넣음. 일단 돈을 넣어 두자.
        Debug.Log("아이템 추가 예정. 50골드 보상!");
        int gold = PlayerPrefs.GetInt("CurrentGold") + 50;
        PlayerPrefs.SetInt("CurrentGold",gold);
        mainUI.UpdateCurrentGoldText();
        objectController.isAlive = false;
        uiManager.SetBacktoMain();
    }

    private IEnumerator NPC_Goblin()
    {
        mainText.text = "이름을 설정할래?";
        choiceText.text = "E : 이름을 바꾼다\nQ : 돌아가기";

        while (true)
        {
            // Q나 E 중 하나가 입력될 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            if (Input.GetKeyDown(KeyCode.Q))
            {
                uiManager.SetBacktoMain();
                yield break; // 입력 받고 코루틴 종료
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                NameInputSwitch(true); // 인풋필드 활성화
                bool isInputFinished = false;
                string result = "";

                inputField.onEndEdit.AddListener((text) =>
                {
                    result = text;
                    isInputFinished = true;
                });

                yield return new WaitUntil(() => isInputFinished);
                mainUI.UpdateCharacterName(result);
                uiManager.SetBacktoMain();
                yield break; // 입력 받고 코루틴 종료
            }

            yield return null; // 혹시 아무 입력 없으면 루프 계속 돌게. 없어도 될 것 같긴 하지만.
        }

    }

    private IEnumerator NPC_Wizard()
    {
        mainText.text = "미니게임 하러 갈래?";
        choiceText.text = "E : 게임하기\nQ : 돌아가기";

        while (true)
        {
            // Q나 E 중 하나가 입력될 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            if (Input.GetKeyDown(KeyCode.Q))
            {
                uiManager.SetBacktoMain();
                yield break; // 입력 받고 코루틴 종료
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Load_Completely_Than_Unload("FlappyBird", "SampleScene"));
                yield break; // 입력 받고 코루틴 종료
            }

            yield return null; // 혹시 아무 입력 없으면 루프 계속 돌게. 없어도 될 것 같긴 하지만.
        }
    }

    private IEnumerator ResetPotion()
    {
        mainText.text = "모든 기억을 잊을까?";
        choiceText.text = "E : 잊기\nQ : 돌아가기";

        while (true)
        {
            // Q나 E 중 하나가 입력될 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E));
            if (Input.GetKeyDown(KeyCode.Q))
            {
                uiManager.SetBacktoMain();
                yield break; // 입력 받고 코루틴 종료
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt("CurrentGold",0);
                PlayerPrefs.SetInt("BestScore",0);
                PlayerPrefs.SetString("CharacterName", "noName");
                mainText.text = "곧 모든 기억을 잊고\n다시 시작합니다.";
                choiceText.text = "";
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("SampleScene");
                yield break; // 입력 받고 코루틴 종료
            }

            yield return null; // 혹시 아무 입력 없으면 루프 계속 돌게. 없어도 될 것 같긴 하지만.
        }
    }
    private void NameInputSwitch(bool is_ON)
    {
        canvasGroup = nameInputCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = (is_ON) ? 1.0f : 0.0f;
        canvasGroup.blocksRaycasts = is_ON;
        canvasGroup.interactable = is_ON;
    }

    protected override UIState GetUIState()
    {
        return UIState.Talk;
    }
    private IEnumerator Load_Completely_Than_Unload(string loadingScene, string unLoadingScene)
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
}

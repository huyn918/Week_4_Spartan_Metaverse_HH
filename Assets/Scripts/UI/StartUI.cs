using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{
    [SerializeField] private Button startButton;
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    
    // Scene이 로드될 때 시작 UI가 뜰 때 Pause로 바꿔서 조작이 안 되도록 하고, 꺼질 때 다시 Normal로 변환.


    public void OnClickStartButton()
    {
        Debug.Log("시작 버튼 클릭함");
        uiManager.SetBacktoMain(); // 메인 UI로 이동
    }


    protected override UIState GetUIState()
    {
        return UIState.Start;
    }
}
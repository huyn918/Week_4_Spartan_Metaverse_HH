using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Main,
    Start,
    Talk,
}

public class UIManager : MonoBehaviour
{
    MainUI mainUI;
    StartUI startUI;
    TalkUI talkUI;
    private UIState currentState;

    private void Awake()
    {
        mainUI = GetComponentInChildren<MainUI>(true);
        mainUI.Init(this);
        startUI = GetComponentInChildren<StartUI>(true);
        startUI.Init(this);
        talkUI = GetComponentInChildren<TalkUI>(true);
        talkUI.Init(this);

        ChangeState(UIState.Start); // 스타트 UI만 키고 나머지 UI 끄기
    }

    public void SetBacktoMain() // 스타트 UI 끄고 
    {
        ChangeState(UIState.Main);
    }

    public void SetTalking()
    {
        ChangeState(UIState.Talk);
    }

  
    public void ChangeState(UIState state) // 입력받은 state가 아닌 건 다 끄고 일치하는 거만 On
    {
        currentState = state;
        startUI.ActiveUI(currentState);
        talkUI.ActiveUI(currentState);
        mainUI.ActiveUI(currentState);
    }
}

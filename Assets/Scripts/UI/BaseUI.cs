using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;
    protected PlayerController controller;
    protected List<string> TagList = new List<string> {"Crate","NPC_Goblin","NPC_Wizard", "ResetPotion" };
    // 오브젝트 및 NPC 상태값 받아오기 위함
    private void Awake()
    {
        if (controller == null)
        {
            controller = FindAnyObjectByType<PlayerController>();
        }
    }


    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState(); // 델리게이트
    public void ActiveUI(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state); // 입력값으로 받은 동일한 state인 경우만 SetActive함
    }
}


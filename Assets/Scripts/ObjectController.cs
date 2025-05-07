using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] public bool isTalkable = false;
    [SerializeField] public bool isDamage = false;
    [SerializeField] public bool isAlive = false;
    [SerializeField] public GameObject chatUI;

    // 위 public값 3개는 인스펙터에서 개체마다 설정할 것
    [SerializeField] private GameObject pressE;
    [SerializeField] private GameObject Player;

    private GameObject pressEprefab;
    private Transform userPosition;
    private Transform myPosition;
    [SerializeField] private bool isTargetInRange = false;
    [SerializeField] private float distance_btwn_User = 0f;
    public string targetTagName;
    private void Awake()
    {
        myPosition = this.transform;
        userPosition = Player.transform;
        
    }

    private void Update()
    {
        distance_btwn_User = DistanceToTarget(); // 거리 실시간 측정
        if (isTalkable) // 상호작용버튼 활성화 필요 시 호출.
        {
            DetectUserInRange();
        }
        if (!isAlive) KillTheObject();

    }
    private void DetectUserInRange()
    {
        if (distance_btwn_User < interactionRange && !isTargetInRange) // 처음 범위 안으로 들어오면
        {
            LocatePressE();
            pressEprefab = Instantiate(pressE);
            isTargetInRange = true;
            // 버튼 만들고 범위 안에 들어온 것 기록 : 다음 업데이트부터는 아래로
        }
        else if (distance_btwn_User > interactionRange && isTargetInRange) // 범위 안에 있으면 아무 일 없다가, 범위 밖으로 벗어나면
        {
            Destroy(pressEprefab);
            isTargetInRange = false;
        }
        else if (distance_btwn_User < interactionRange && Input.GetKeyDown(KeyCode.E))// 처음 범위 안으로 들어오고, 아직 안 나간 상태면 키 입력을 받음
        {
            Debug.Log("E누름");
            targetTagName = this.tag; // 대화 UI 진입 시 다른 Coroutine 실행을 위해 태그값 저장
            chatUI.SetActive(true);
            
        }

    }
    protected float DistanceToTarget() // 거리 계산. 2D라 2D로 해도 되긴 하는데 봐서
    {
        return Vector3.Distance(myPosition.position, userPosition.position);
    }

    protected Vector2 DirectionToTarget() // 방향 계산
    {
        return (userPosition.position - myPosition.position).normalized;
    }

    private void LocatePressE() // 상호작용 버튼 위치 설정
    {
        Vector2 temp = (Vector2)myPosition.position - Vector2.down;
        pressE.transform.position = temp;
        Debug.Log(pressE.transform.position);
    }
    private void KillTheObject() // 오브젝트 킬
    {
        Destroy(pressEprefab);
        Destroy(this.gameObject);
    }

    
}

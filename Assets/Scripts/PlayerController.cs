using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class PlayerController : BaseController // 베이스 컨트롤러 상속. 캐릭터에 붙은 카메라도 여기서. 분리하는 게 낫나?
{
    private Camera mainCamera; // 카메라 선언
    public Transform character_position { get; private set; }
    [SerializeField] private Tilemap tilemap_collider;
    private Bounds camera_Limit; // 카메라 벗어남 방지 위한 바운드박스.
    protected override void Start()
    {
        base.Start();
        mainCamera = GameObject.FindWithTag("SampleScene_Cam").GetComponent<Camera>(); // 여러 신을 오갈 경우 카메라를 태그로 지정
        character_position = GetComponent<Transform>();
        camera_Limit = tilemap_collider.localBounds;
    }

    protected override void HandleAction()
    // Update는 BaseController에서 진행. 유저 인풋값을 받아서 베이스에 전달
    // 인풋에 따른 실질적 움직임은 Movement, Rotate에서 진행
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        // WASD 이동 구현
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouse_localPosition = (Vector2)mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (mouse_localPosition - (Vector2)transform.position);
        lookDirection = lookDirection.normalized;
        // 보는 방향 벡터값으로 계산하고 노말라이즈
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        float vertExtent = mainCamera.orthographicSize; // 카메라 가로 절반값
        float horzExtent = vertExtent * mainCamera.aspect; // 카메라 세로 절반값
        float camera_Padding = 2f; // 여윳값 2칸

        float minX = camera_Limit.min.x + horzExtent - camera_Padding;
        float maxX = camera_Limit.max.x - horzExtent + camera_Padding;
        float minY = camera_Limit.min.y + vertExtent - camera_Padding;
        float maxY = camera_Limit.max.y - vertExtent + camera_Padding;
        // 벡터로 깔끔하게 어케 할 수 있을 것 같은데 머리아프니 좌표값 빼서 계산

        Vector3 targetPos = new Vector3(
            Mathf.Clamp(character_position.position.x, minX, maxX),
            Mathf.Clamp(character_position.position.y, minY, maxY),
            -10);
        // 여윳값 넣은 타겟포지션 설정
        mainCamera.transform.position = Vector3.Lerp(
        mainCamera.transform.position,
        targetPos,
        Time.deltaTime * 5f);
        // 숫자 크면 빨리 따라감. 카메라를 처음 시작 포지션에서 타겟포지션(유저 위치)으로 Lerp 이동
    }
}

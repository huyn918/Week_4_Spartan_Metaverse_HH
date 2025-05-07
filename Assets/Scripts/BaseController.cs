using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;
    // 아이템 스프라이트 설정 : TownScene에선 폭탄을 살 때까진 필요 없음 - CastleRunScene에서도 필요 없긴 한데?
    public enum PlayerState { Normal, Hit, Talking, Paused }
    public const string CurrentGoldKey = "CurrentGold";
    public const string CharacterNameKey = "CharacterName";
    [SerializeField]public PlayerState currentState = PlayerState.Paused;
    // 플레이어 상태를 지정해서 Normal일 때만 입력값을 받기
    

    protected Vector2 movementDirection = Vector2.zero;
    protected Vector2 lookDirection = Vector2.zero;
    // 상속받은 클래스 내부적으로 사용할 필드 선언
    public Vector2 MovementDirection { get { return movementDirection; } }
    public Vector2 LookDirection { get { return lookDirection; } }
    // 외부에서 벡터값 받아가기 위한 프로퍼티 선언


    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    // 넉벡 벡터 계산을 위한 필드 선언 및 초기화
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); // 충돌을 위한 리지드바디 불러옴

    
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update() // 자식클래스에 업데이트 없으면 여기서 업뎃.
    {
        if (currentState == PlayerState.Normal)
        {
            HandleAction();
            Rotate(lookDirection);
        }
    }

    protected virtual void FixedUpdate() // 물리 움직임은 fixedUpdate에서
    {
        Movement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction() // 자식 클래스에서 이용할 메서드 프리셋 (방어적 코드?)
    {

    }

    private void Movement(Vector2 direction) // 기본적인 캐릭터 이동을 위한 매소드(넉백 적용)
    {
        direction = direction * 5;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction; // 최종 속력 계산 : 기본 5, 넉백중일 시 1로 줄고 넉백 방향으로 합벡터 계산
    }

    private void Rotate(Vector2 direction) // 무기 회전 및 캐릭터 회전을 위한 메소드 (당장은 필요 없음 : 쓰게 되면 주석 달자)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration) // Move 메서드에 넣어줄 넉벡 벡터 계산
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    Flappy_GameManager gameManager;

    public float flapforce = 6.0f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool Godmode = false;


    void Start() // 오브젝트가 활성화되고 첫 프레임 전에 한 번 호출
    {
        gameManager = Flappy_GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        // getcomponent : 이 스크립트가 달려 있는 오브젝트에 Animator라는 컴포넌트가 달려 있으면 호출함.
        // 하지만 자식에 있는 것까지는 추적하지 못함. 이때 InChildren 붙이면 자식에 있는 걸 찾아 자동 호출함.

        if (animator == null) Debug.Log("animator NOT FOUND");
        if (_rigidbody == null) Debug.Log("_ridigbody NOT FOUND");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    gameManager.RestartGame();
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameManager.TestSceneLoad();
                    //StartCoroutine(gameManager.Load_Completely_Than_Unload("SampleScene","FlappyBird"));
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isDead) return; // 사망시 빠져나옴

        Vector3 velocity = _rigidbody.velocity; // 벡터값 만듦
        velocity.x = forwardSpeed; // x는 앞으로 가는 거

        if (isFlap) // 버튼 눌렀으면
        {
            velocity.y += flapforce; // flapforce만큼 위로 이동
            isFlap = false; // 다시 false로 반환
        }

        _rigidbody.velocity = velocity; // 반영된 x,y값 _ridigbody에 적용 (이전까진 아직 적용 안 됨! 값만 정한거)

        float angle = Mathf.Clamp((_rigidbody.velocity.y) * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Godmode) return;
        if (isDead) return;

        isDead = true;
        deathCooldown = 1.0f;

        animator.SetInteger("isDead", 1);
        gameManager.GameOver();
    }
}

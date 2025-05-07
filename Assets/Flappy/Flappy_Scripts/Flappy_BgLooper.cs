using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_BgLooper : MonoBehaviour
{
    public int numbgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;


    void Start()
    {
        Flappy_Obstacle[] obstacles = GameObject.FindObjectsOfType<Flappy_Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    // 트리거 충돌이라 실제 물리충돌은 없고 충돌에 대한 통보만 해 줌.
    // 그래서 나랑 부딫힌 충돌체에 대한 정보만 받음.
    {
        Debug.Log("Triggerd : " + collision.name);

        if (collision.CompareTag("Background"))
        {
            float widthOfbgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfbgObject * numbgCount;

            collision.transform.position = pos;
            return;
        }

        if (collision.TryGetComponent<Flappy_Obstacle>(out Flappy_Obstacle obstacle))
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}


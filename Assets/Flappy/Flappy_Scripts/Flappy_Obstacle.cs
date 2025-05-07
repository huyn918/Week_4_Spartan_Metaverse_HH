using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;
    Flappy_GameManager gameManager;
    private void Start()
    {
        gameManager = Flappy_GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastposition, int obstaclecount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastposition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Flappy_Player>(out Flappy_Player player))
        {
            gameManager.AddScore(1);
        }
    }
}


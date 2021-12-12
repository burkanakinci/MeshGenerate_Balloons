using UnityEngine;

public class BalloonLine : MonoBehaviour
{
    private LineRenderer line;
    private BalloonSpawner balloonSpawner;
    private Transform firstLinePos;
    void Start()
    {
        balloonSpawner = FindObjectOfType<BalloonSpawner>();
        firstLinePos = balloonSpawner.spawnPos;

        line = GetComponent<LineRenderer>();
        line.startWidth = 0.04f;
    }

    void Update()
    {
        line.SetPosition(0, firstLinePos.position);
        line.SetPosition(1, transform.position);
    }
}

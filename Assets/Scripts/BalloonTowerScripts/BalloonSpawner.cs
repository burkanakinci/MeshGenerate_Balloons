using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private List<Color> balloonColors;
    [SerializeField] private GameObject balloonPrefab;
    public Transform spawnPos;
    [SerializeField] private Transform balloonParent;
    [SerializeField] private Rigidbody connectBody;
    private GameObject tempBalloon;
    private BalloonController balloonController;

    void Start()
    {
        balloonController = FindObjectOfType<BalloonController>();
        InvokeRepeating("SpawnBalloon", 2f, 1f);
    }
    public void SpawnBalloon()
    {
        int matInd = Random.Range(0, balloonColors.Count);
        float balloonScale = Random.Range(0.3f, 0.6f);

        tempBalloon = Instantiate(balloonPrefab, spawnPos.position, Quaternion.identity, balloonParent);
        tempBalloon.GetComponent<MeshRenderer>().material.color = balloonColors[matInd];
        tempBalloon.transform.localScale = Vector3.one * balloonScale;
        tempBalloon.GetComponent<SpringJoint>().connectedBody = connectBody;
        tempBalloon.GetComponent<SpringJoint>().connectedAnchor += RandomVector(new Vector3(-2f, -0.4f, -0.4f),
                                                                                new Vector3(2f, 0.4f, 0.4f));
        balloonController.balloons.Add(tempBalloon);


    }
    private Vector3 RandomVector(Vector3 minVec, Vector3 maxVec)
    {

        Vector3 myVec = new Vector3(Random.Range(minVec.x, maxVec.x),
            Random.Range(minVec.y, maxVec.y),
            Random.Range(minVec.z, maxVec.z));

        return myVec;
    }
}

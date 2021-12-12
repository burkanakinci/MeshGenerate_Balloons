using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 distance;

    void Update()
    {

        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(transform.position.x, player.position.y - distance.y, player.position.z - distance.z),
                                          4f);
    }
}
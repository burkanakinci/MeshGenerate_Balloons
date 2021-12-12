using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public List<GameObject> balloons = new List<GameObject>();
    public float moveSpeed = 4f;
    public float xMoveLerp = 2f;
    private float difXPos, firstXPos, newXPos;
    public Transform moveParent;

    private void Update()
    {
        BalloonsScaling();

        Scrolling();
    }

    void FixedUpdate()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        moveParent.position = new Vector3(moveParent.position.x, moveParent.position.y + moveSpeed * Time.fixedDeltaTime, moveParent.position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition,
                                            new Vector3(Mathf.Clamp(newXPos, -5f, 5f),
                                                        transform.localPosition.y,
                                                        transform.localPosition.z),
                                                xMoveLerp);
    }

    private Vector3 EaseInExpo(Vector3 start, Vector3 end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }
    private void BalloonsScaling()
    {
        for (int i = balloons.Count - 1; i >= 0; i--)
        {
            if (balloons[i].transform.localScale != Vector3.one)
            {
                balloons[i].transform.localScale = EaseInExpo(balloons[i].transform.localScale,
                                                                Vector3.one,
                                                                0.01f);
            }
        }
    }

    private void Scrolling()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstXPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

        }
        if (Input.GetMouseButton(0))
        {
            difXPos = (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - firstXPos);
            newXPos += difXPos;
            difXPos = 0f;
            firstXPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        }
    }
}

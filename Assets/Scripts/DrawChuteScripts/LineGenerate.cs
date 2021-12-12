using UnityEngine;

public class LineGenerate : MonoBehaviour
{
    private LineRenderer line;
    private bool drawStarted;
    private Vector3 mousePos;
    [SerializeField] private Material lineMat;
    private int curInd;
    private Camera mainCam;
    private MeshGenerate meshGenerate;
    private Vector3[] linePoses;
    private bool firstTouch;
    private void Start()
    {
        firstTouch = false;

        meshGenerate = GetComponent<MeshGenerate>();
        line = GetComponent<LineRenderer>();
        mainCam = Camera.main;
    }
    public void TouchDown()
    {
        drawStarted = true;
        mousePos = Input.mousePosition;

        line.startWidth = 2f;
        line.material = lineMat;
    }
    public void TouchExit()
    {
        if (!firstTouch)
        {
            meshGenerate.parachute.AddComponent<MeshCollider>();

            firstTouch = true;
        }
        if (drawStarted)
        {
            linePoses = new Vector3[line.positionCount];
            for (int p = 0; p < line.positionCount; p++)
            {
                linePoses[p] = line.GetPosition(p);
            }

            drawStarted = false;
            curInd = 0;
            meshGenerate.CreateShape(linePoses);

            meshGenerate.parachute.GetComponent<MeshCollider>().convex = false;
            meshGenerate.parachute.GetComponent<MeshCollider>().convex = true;
        }

    }
    private void Update()
    {
        if (drawStarted)
        {
            Vector3 dist = mousePos - Input.mousePosition;

            float distSqrMag = dist.sqrMagnitude;
            if (distSqrMag > 100f)
            {
                line.SetPosition(curInd, mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 55f)));
                mousePos = Input.mousePosition;
                curInd++;
                line.positionCount = curInd + 1;
                line.SetPosition(curInd, mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 55f)));
            }
        }
    }
}



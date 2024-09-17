using UnityEngine;

public class LineRendererScript: MonoBehaviour
{
    public GameObject startObject;
    public GameObject endObject;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, startObject.transform.position);
        lineRenderer.SetPosition(1, endObject.transform.position);
    }
}

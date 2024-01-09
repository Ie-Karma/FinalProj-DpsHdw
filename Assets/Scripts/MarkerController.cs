using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{


	public LineRenderer line;
	private LineRenderer lineDraw;
    private List<Vector3> points = new List<Vector3>();
    public bool isDrawing = false;
	[Space,Header("BlackBoard")]
	public bool useSurface = false;
	public GameObject surface;
	public float offset = 0;
	public Transform lineContainer;
	private List<LineRenderer> lines = new List<LineRenderer>();
	private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
		line.positionCount = 2;
	}

	// Update is called once per frame
	void Update()
    {

		if (isDrawing)
		{
			if (useSurface && surface)
			{
				if (Vector3.Distance(transform.position, surface.transform.position) > 0.01f)
				{
					Vector3 newPos = transform.position;
					newPos.x = surface.transform.position.x + offset;
					line.SetPosition(0, newPos);
					line.SetPosition(1, points[points.Count - 1]);
				}
				else
				{
					return;
				}
			}
			else
			{
				line.SetPosition(0, transform.position);
				line.SetPosition(1, points[points.Count - 1]);
			}


			if (Vector3.Distance(transform.position, points[points.Count - 1]) > .01f)
			{
				Vector3 newPos = transform.position;
				if (useSurface && surface)
				{
					//place the new point on the surface
					newPos.x = surface.transform.position.x + offset;

				}
				points.Add(newPos);
			}


			lineDraw.positionCount = points.Count;
			printLine();
		}


	}

	public void GrabIt(bool grab)
	{
		isGrabbed = grab;
	}
	private void printLine()
  {

    for (int i = 0; i < points.Count; i++)
    {
			lineDraw.SetPosition(i, points[i]);
		}

	}

	public void DeleteLine()
	{

		if (!isGrabbed)
			return;
		if (lines.Count > 0)
		{
			// Get position of gameobject 
			Vector3 position = transform.position;

			// Find the closest line renderer based on the points of the line renderers
			float minDistance = Mathf.Infinity;
			LineRenderer closestLine = null;

			foreach (var lineRenderer in lines)
			{
				float distance = Mathf.Infinity;

				for (int i = 0; i < lineRenderer.positionCount - 1; i++)
				{
					Vector3 lineStart = lineRenderer.GetPosition(i);
					Vector3 lineEnd = lineRenderer.GetPosition(i + 1);

					float d = DistanceFromPointToLine(position, lineStart, lineEnd);

					if (d < distance)
					{
						distance = d;
					}
				}

				if (distance < minDistance)
				{
					minDistance = distance;
					closestLine = lineRenderer;
				}
			}
			if (closestLine != null)
			{
				// Remove the closest line renderer from list of lines and destroy it
				lines.Remove(closestLine);
				Destroy(closestLine.gameObject);
			}
		}
	}

	private float DistanceFromPointToLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		float lineLength = Vector3.Distance(lineStart, lineEnd);
		float t = Mathf.Clamp01(Vector3.Dot((point - lineStart), (lineEnd - lineStart).normalized) / lineLength);
		Vector3 projection = lineStart + t * (lineEnd - lineStart);
		return Vector3.Distance(projection, point);
	}


	public void startDrawing() {
    
		isDrawing = true;
		points = new List<Vector3>();
		points.Add(transform.position);
		lineDraw = Instantiate(line);
		lines.Add(lineDraw);
		lineDraw.transform.SetParent(lineContainer);	
		lineDraw.positionCount = 2;

	}

	public void endDrawing()
	{
		isDrawing = false;

	}


}

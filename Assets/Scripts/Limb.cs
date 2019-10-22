using UnityEngine;

public class Limb : MonoBehaviour
{
	public Limb child;
	public Vector3 jointLocation;
	public Vector3 jointOffset;
	public float lastAngle;
	public float angle;
	public Vector3[] limbVertexLocations;
	public Material material;
	public Mesh mesh;

	public Color color;

	void Start()
	{
		DrawLimb();
		if (child != null)
		{
			child.GetComponent<Limb>().MoveByOffset(jointOffset);
		}
	}

	public void DrawLimb()
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		mesh = GetComponent<MeshFilter>().mesh;

		GetComponent<MeshRenderer>().material = material;

		mesh.Clear();

		mesh.vertices = limbVertexLocations;

		mesh.colors = new Color[]
		{
							color,
							color,
							color,
							color
		};

		mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
	}

	public void MoveByOffset(Vector3 offset)
	{
		Matrix3x3 T = Matrix3x3.Translate(offset);
		Vector3[] vertices = 
		mesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] = T.MultiplyPoint(vertices[i]);
		}
		mesh.vertices = vertices;

		jointLocation = T.MultiplyPoint(jointLocation);

		if (child != null)
		{
			child.GetComponent<Limb>().MoveByOffset(offset);
		}
	}

	public void RotateAroundPoint(Vector3 point, float angle, float lastAngle)
	{
		Matrix3x3 T1 = Matrix3x3.Translate(-point);

		Matrix3x3 R1 = Matrix3x3.Rotate(-lastAngle);

		Matrix3x3 T2 = Matrix3x3.Translate(point);

		Matrix3x3 R2 = Matrix3x3.Rotate(angle);

		Matrix3x3 M = T2 * R2 * R1 * T1;

		Vector3[] vertices = mesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] = M.MultiplyPoint(vertices[i]);
		}
		mesh.vertices = vertices;

		jointLocation = M.MultiplyPoint(jointLocation);

		if (child != null)
		{
			child.GetComponent<Limb>().RotateAroundPoint(point, angle, lastAngle);
		}
	}
}

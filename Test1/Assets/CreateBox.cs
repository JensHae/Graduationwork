using UnityEngine;
using System.Collections;

public class CreateBox : MonoBehaviour {

    public float length = 1f;
    public float width = 1f;
    public float height = 1f;
    public Vector3 position;
    public int id = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Create()
	{
		// You can change that line to provide another MeshFilter
		var renderer = this.GetComponent<MeshRenderer>();
		var filter = this.GetComponent<MeshFilter>();
		
		GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
		Material diffuse = primitive.GetComponent<MeshRenderer>().sharedMaterial;
		DestroyImmediate(primitive);
		
		if (renderer != null) renderer.material = diffuse;
		Mesh mesh = new Mesh();
		mesh.Clear();
		
		#region Vertices
		Vector3 p0 = new Vector3(0, 0, width) + position;
		Vector3 p1 = new Vector3(length, 0, width) + position;
		Vector3 p2 = new Vector3(length, 0, 0) + position;
		Vector3 p3 = position;
		
		Vector3 p4 = new Vector3(0, height, width) + position;
		Vector3 p5 = new Vector3(length, height, width) + position;
		Vector3 p6 = new Vector3(length, height, 0) + position;
		Vector3 p7 = new Vector3(0, height, 0) + position;
		
		Vector3[] vertices;
		
		vertices = new Vector3[]
		{
			p0, p1, p2, p3, // Bottom
			p7, p4, p0, p3, // Left
			p4, p5, p1, p0, // Front
			p6, p7, p3, p2, // Back
			p5, p6, p2, p1, // Right
			p7, p6, p5, p4  // Top
		};
		
		#endregion
		
		#region Normales
		Vector3 up = Vector3.up;
		Vector3 down = Vector3.down;
		Vector3 front = Vector3.forward;
		Vector3 back = Vector3.back;
		Vector3 left = Vector3.left;
		Vector3 right = Vector3.right;
		
		Vector3[] normales;
		
		normales = new Vector3[]
		{
			down, down, down, down,     // Bottom
			left, left, left, left,     // Left
			front, front, front, front, // Front
			back, back, back, back,     // Back
			right, right, right, right, // Right
			up, up, up, up              // Top
		};
		
		#endregion
		
		#region UVs
		Vector2 _00 = new Vector2(0f, 0f);
		Vector2 _10 = new Vector2(1f, 0f);
		Vector2 _01 = new Vector2(0f, 1f);
		Vector2 _11 = new Vector2(1f, 1f);
		
		Vector2[] uvs;
		
		uvs = new Vector2[]
		{
			down, down, down, down,     // Bottom
			left, left, left, left,     // Left
			front, front, front, front, // Front
			back, back, back, back,     // Back
			right, right, right, right, // Right
			up, up, up, up              // Top
		};
		
		#endregion
		
		#region Triangles
		int[] triangles;    
		
		triangles = new int[]
		{
			3, 1, 0,        // Bottom
			3, 2, 1,			
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,    // Left
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,    // Front
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,    // Back
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,    // Right
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,    // Top
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
		};
		
		#endregion
		
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		filter.sharedMesh = mesh;
	}
}

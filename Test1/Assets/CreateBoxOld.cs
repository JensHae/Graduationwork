using UnityEngine;
using System.Collections;

public struct holeStruct
{
    public Vector3 pos;
    public float length;
    public float height;
    public bool isSet;
}

public class CreateBoxOld : MonoBehaviour {

    public float length = 1f;
    public float width = 1f;
    public float height = 1f;
    public Vector3 position;
    public holeStruct hole;

	// Use this for initialization
	void Start () {
        // You can change that line to provide another MeshFilter
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
        primitive.SetActive(false);
        Material diffuse = primitive.GetComponent<MeshRenderer>().sharedMaterial;
        DestroyImmediate(primitive);

        renderer.material = diffuse;
        Mesh mesh = filter.mesh;
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

        if (hole.isSet)
        {
            Vector3 p8 = new Vector3(0, 0, width) + position + hole.pos;
            Vector3 p9 = new Vector3(hole.length, 0, width) + position + hole.pos;
            Vector3 p10 = new Vector3(hole.length, 0, 0) + position + hole.pos;
            Vector3 p11 = position + hole.pos;

            Vector3 p12 = new Vector3(0, hole.height, width) + position + hole.pos;
            Vector3 p13 = new Vector3(hole.length, hole.height, width) + position + hole.pos;
            Vector3 p14 = new Vector3(hole.length, hole.height, 0) + position + hole.pos;
            Vector3 p15 = new Vector3(0, hole.height, 0) + position + hole.pos;

            vertices = new Vector3[]
            {
                p0, p1, p2, p3, // Bottom
                p7, p4, p0, p3, // Left
                p4, p12, p8, p0, // Front
                p8, p9, p1, p0,
                p13, p5, p1, p9,
                p4, p5, p13, p12,
                p6, p14, p10, p2, // Back
                p6, p7, p15, p14,
                p15, p7, p3, p11,
                p10, p11, p3, p2,
                p5, p6, p2, p1, // Right
                p7, p6, p5, p4,  // Top
                p8, p9, p10, p11, //Window
                p13, p14, p10, p9,
                p12, p15, p14, p13,
                p8, p11, p15, p12
            };
        }
        else
        {
            vertices = new Vector3[]
            {
                p0, p1, p2, p3, // Bottom
                p7, p4, p0, p3, // Left
                p4, p5, p1, p0, // Front
                p6, p7, p3, p2, // Back
                p5, p6, p2, p1, // Right
                p7, p6, p5, p4  // Top
            };
        }
        #endregion

        #region Normales
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales;
        if (hole.isSet)
        {
            normales = new Vector3[]
            {
                down, down, down, down, // Bottom
                left, left, left, left, // Left
                front, front, front, front, // Front
                front, front, front, front,
                front, front, front, front,
                front, front, front, front,
                back, back, back, back, // Back
                back, back, back, back,
                back, back, back, back,
                back, back, back, back,
                right, right, right, right, // Right
                up, up, up, up ,  // Top
                up, up, up, up , //Window
                left, left, left, left,
                down, down, down, down,
                right, right, right, right
            };
        }
        else
        {
            normales = new Vector3[]
            {
                down, down, down, down,     // Bottom
                left, left, left, left,     // Left
                front, front, front, front, // Front
                back, back, back, back,     // Back
                right, right, right, right, // Right
                up, up, up, up              // Top
            };
        }
        #endregion

        #region UVs
        Vector2 _00 = new Vector2(0f, 0f);
        Vector2 _10 = new Vector2(1f, 0f);
        Vector2 _01 = new Vector2(0f, 1f);
        Vector2 _11 = new Vector2(1f, 1f);

        Vector2[] uvs;
        if (hole.isSet)
        {
            uvs = new Vector2[]
            {
                _11, _01, _00, _10, // Bottom
                _11, _01, _00, _10, // Left
                _11, _01, _00, _10, // Front
                _11, _01, _00, _10,
                _11, _01, _00, _10,
                _11, _01, _00, _10,
                _11, _01, _00, _10, // Back
                _11, _01, _00, _10,
                _11, _01, _00, _10,
                _11, _01, _00, _10,
                _11, _01, _00, _10, // Right
                _11, _01, _00, _10,  // Top
                _11, _01, _00, _10, //Window
                _11, _01, _00, _10,
                _11, _01, _00, _10,
                _11, _01, _00, _10
            };
        }
        else
        {
            uvs = new Vector2[]
            {
                down, down, down, down,     // Bottom
                left, left, left, left,     // Left
                front, front, front, front, // Front
                back, back, back, back,     // Back
                right, right, right, right, // Right
                up, up, up, up              // Top
            };
        }
        #endregion

        #region Triangles
        int[] triangles;
        if (hole.isSet)
        {
            triangles = new int[]
            {
                3, 1, 0,            // Bottom
                3, 2, 1, 
                0, 4, 7,            // Left
                7, 3, 0, 
                0, 8, 12,           // Front    
                0, 1, 8,
                1, 9, 8,
                1, 5, 9,
                5, 13, 9,
                5, 4, 13,
                4, 12, 13,
                4, 0, 12,
                3, 11, 15,          // Back
                3, 2, 11,
                2, 10, 11,
                2, 6, 10,
                6, 14, 10,
                6, 7, 14,
                7, 15, 14,
                7, 3, 15,
                5, 2, 1,            // Right
                5, 6, 2,
                4, 7, 6,            // Top
                6, 5, 4, 
                8, 10, 9,   //Window
                8, 11, 10,
                9, 10, 13,
                10, 14, 13,
                14, 15, 12,
                14, 12, 13,
                15, 12, 11,
                12, 8, 11
            };
        }
        else
        {
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
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

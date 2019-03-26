using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class ExampleQuad : MonoBehaviour
{
    public float width;
    public float height;

    public MeshFilter meshFilter;

    [ContextMenu("CreateQuad")]
    public void CreateQuad()
    {
        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(width, 0, 0);
        verts[2] = new Vector3(0, height, 0);
        verts[3] = new Vector3(width, height, 0);

        mesh.vertices = verts;

        int[] tris = new int[6];

        tris[0] = 0;
        tris[1] = 2;
        tris[2] = 1;

        tris[3] = 1;
        tris[4] = 2;
        tris[5] = 3;

        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;

        mesh.normals = normals;

        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[0] = new Vector2(1, 0);
        uvs[0] = new Vector2(0, 1);
        uvs[0] = new Vector2(1, 1);

        mesh.uv = uvs;

        meshFilter.mesh = mesh;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class MeshDoor : MonoBehaviour
{
    [Tooltip("门的厚度")]
    public float doorThickness;

    public float bottomLength;
    public float bootomHeight;

    public float topRadio;
    public float details;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    List<Vector3> Verts;
    [ContextMenu("GenerateDoor")]
    public void GenerateDoor()
    {
        Mesh mesh = new Mesh();

        Verts = new List<Vector3>();
        List<int> tris = new List<int>();

        Vector3 originPos = transform.position;

        //左边
        List<Vector3> _verts = new List<Vector3>();
        List<int> _tris = new List<int>();
        GenerateBottom(0, originPos, bottomLength, doorThickness, bootomHeight, _verts, _tris);
        Verts.AddRange(_verts);
        tris.AddRange(_tris);
        _verts.Clear();
        _tris.Clear();

        //右边
        GenerateBottom(8,new Vector3(originPos.x + 2 * topRadio - bottomLength, originPos.y, originPos.z), bottomLength, doorThickness, bootomHeight, _verts, _tris);
        Verts.AddRange(_verts);
        tris.AddRange(_tris);
        _verts.Clear();
        _tris.Clear();

        //顶部
        GenerateTop(16,new Vector3(originPos.x+topRadio,transform.position.y + bootomHeight, originPos.z), doorThickness, topRadio,topRadio-bottomLength, details,_verts,_tris);
        Verts.AddRange(_verts);
        tris.AddRange(_tris);
        print(Verts.Count);


        mesh.vertices = Verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshFilter.mesh = mesh;

    }
    public void GenerateBottom(int leftPointIndex,Vector3 origin,float length,float width,float height,List<Vector3> verts,List<int> tris)
    {
        //vert
        //bottom
        verts.Add(origin);
        verts.Add(new Vector3(origin.x + length, origin.y, origin.z));
        verts.Add(new Vector3(origin.x , origin.y, origin.z + width));
        verts.Add(new Vector3(origin.x + length, origin.y, origin.z + width));
        //top
        verts.Add(new Vector3(origin.x , origin.y + height, origin.z));
        verts.Add(new Vector3(origin.x+length, origin.y + height, origin.z ));
        verts.Add(new Vector3(origin.x, origin.y + height, origin.z + width));
        verts.Add(new Vector3(origin.x+length, origin.y + height, origin.z + width));


        //tris

        //front 0 1 4 5
        tris.Add(leftPointIndex+0); tris.Add(leftPointIndex + 4); tris.Add(leftPointIndex + 1);
        tris.Add(leftPointIndex + 1); tris.Add(leftPointIndex + 4); tris.Add(leftPointIndex + 5);
        //behind 2 6 7 3
        tris.Add(leftPointIndex + 7); tris.Add(leftPointIndex + 6); tris.Add(leftPointIndex + 2);
        tris.Add(leftPointIndex + 7); tris.Add(leftPointIndex + 2); tris.Add(leftPointIndex + 3);
        //left 6 4 0 2
        tris.Add(leftPointIndex + 6); tris.Add(leftPointIndex + 4); tris.Add(leftPointIndex + 2);
        tris.Add(leftPointIndex + 4); tris.Add(leftPointIndex + 0); tris.Add(leftPointIndex + 2);
        //right 1 5 7 3
        tris.Add(leftPointIndex + 5); tris.Add(leftPointIndex + 3); tris.Add(leftPointIndex + 1);
        tris.Add(leftPointIndex + 5); tris.Add(leftPointIndex + 7); tris.Add(leftPointIndex + 3);
        ////up 6 7 4 5 (up面可以不加)
        //tris.Add(leftPointIndex + 6); tris.Add(leftPointIndex + 7); tris.Add(leftPointIndex + 4);
        //tris.Add(leftPointIndex + 7); tris.Add(leftPointIndex + 5); tris.Add(leftPointIndex + 4);
        //down 3 2 0 1
        tris.Add(leftPointIndex + 1); tris.Add(leftPointIndex + 3); tris.Add(leftPointIndex + 2);
        tris.Add(leftPointIndex + 1); tris.Add(leftPointIndex + 2); tris.Add(leftPointIndex + 0);





    }
    public void GenerateTop(int centerOfMind, Vector3 origin,float width, float outRadio,float inRadio,float details,List<Vector3> verts,List<int> tris)
    {



        float rad = Mathf.PI / details;

        float currentRad;
        //外圈
        for (currentRad=0;currentRad<=Mathf.PI;currentRad+=rad)
        {
            verts.Add(origin+new Vector3(Mathf.Cos(currentRad) *outRadio, Mathf.Sin(currentRad) * outRadio, 0));
            verts.Add(origin+new Vector3(Mathf.Cos(currentRad) * outRadio, Mathf.Sin(currentRad) * outRadio, width));

            
        }
        if(currentRad<Mathf.PI+0.001f)
        {
            verts.Add(origin+new Vector3(Mathf.Cos(Mathf.PI) * outRadio, Mathf.Sin(Mathf.PI) * outRadio, 0));
            verts.Add(origin+new Vector3(Mathf.Cos(Mathf.PI) * outRadio, Mathf.Sin(Mathf.PI) * outRadio, width));
        }
        //内圈节点起始索引(外圈从0开始到verts.Count-1)
        int inStartIndex = verts.Count ;
        //内圈
        for (currentRad = 0; currentRad <= Mathf.PI; currentRad += rad)
        {
            verts.Add(origin + new Vector3(Mathf.Cos(currentRad) * inRadio, Mathf.Sin(currentRad) * inRadio, 0));
            verts.Add(origin + new Vector3(Mathf.Cos(currentRad) * inRadio, Mathf.Sin(currentRad) * inRadio, width));
        }
        if (currentRad < Mathf.PI + 0.001f)
        {
            verts.Add(origin + new Vector3(Mathf.Cos(Mathf.PI) * inRadio, Mathf.Sin(Mathf.PI) * inRadio, 0));
            verts.Add(origin + new Vector3(Mathf.Cos(Mathf.PI) * inRadio, Mathf.Sin(Mathf.PI) * inRadio, width));
        }

        //out  inStartIndex-3 是因为避免首和尾相连（本应该可以要-2）
        for (int i = 0; i <= inStartIndex - 3; i += 2)
        {
            tris.Add(centerOfMind + i); tris.Add(centerOfMind + i + 2); tris.Add(centerOfMind + i + 1);
            tris.Add(centerOfMind + i + 1); tris.Add(centerOfMind + i + 2); tris.Add(centerOfMind + i + 3);
        }
        //in
        for (int i = inStartIndex; i <= verts.Count - 3; i += 2)
        {
            tris.Add(centerOfMind + i + 1); tris.Add(centerOfMind + i + 2); tris.Add(centerOfMind + i);
            tris.Add(centerOfMind + i + 3); tris.Add(centerOfMind + i + 2); tris.Add(centerOfMind + i + 1);
        }

        //侧面补全
        for (int i = 0; i <= inStartIndex-3; i += 2)
        {
            tris.Add(centerOfMind + i); tris.Add(centerOfMind + inStartIndex + i); tris.Add(centerOfMind + inStartIndex + i + 2);
            tris.Add(centerOfMind + i); tris.Add(centerOfMind + inStartIndex +i+ 2); tris.Add(centerOfMind + i + 2);
        }
        for (int i = 1; i <= inStartIndex-3; i += 2)
        {
            tris.Add(centerOfMind + i); tris.Add(centerOfMind + inStartIndex + i + 2); tris.Add(centerOfMind + inStartIndex + i);
            tris.Add(centerOfMind + i); tris.Add(centerOfMind + i + 2); tris.Add(centerOfMind + inStartIndex + i + 2);
        }

    }
    //public void OnDrawGizmos()
    //{
    //    if (Verts == null || Verts.Count<=0) return;
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(Verts[0], 0.5f);
    //    Gizmos.color = Color.gray;
    //    for (int i = 1; i < Verts.Count; i++)
    //    {
    //        Gizmos.DrawSphere(Verts[i], 0.5f);
    //    }
    //}
}

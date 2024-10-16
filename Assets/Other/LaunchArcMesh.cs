using System;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour
{

    Mesh mesh;
    public float meshWidth;
    public float velocity;
    public float angle;
    public int resolution = 10;

    float g;
    float radianAngle;
    private void Awake()
    {
       mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }
    private void Start()
    {
        MakeArcMesh(CalculationArcArray());
    }
    private void OnValidate()
    {
        if (mesh != null && Application.isEditor)
        {
            MakeArcMesh(CalculationArcArray());
        }
    }
    private void MakeArcMesh(Vector3[] arcVerts)
    {
       mesh.Clear();
       Vector3[] vertices = new Vector3[(resolution + 1)*2] ;
        int[] triangles = new int[resolution * 6*2];
        for (int i = 0; i <= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * 0.5F, arcVerts[i].y, arcVerts[i].x) ;
            vertices[i * 2+1] = new Vector3(meshWidth * -0.5F, arcVerts[i].y, arcVerts[i].x) ;

            if (i!= resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4]  = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 4] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 3] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }
          
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private Vector3[] CalculationArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        //Deg2Rad 度到弧度换算常量
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculationArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    private Vector3 CalculationArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * MathF.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * MathF.Cos(radianAngle) * MathF.Cos(radianAngle)));

        return new Vector2(x, y);
    }
}

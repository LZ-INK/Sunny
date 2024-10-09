using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{

    LineRenderer lr;
    public float velocity;
    public float angle;
    public int resolution = 10;

    float g;
    float radianAngle;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }
    private void Start()
    {
        RenderArc();
    }
    private void OnValidate()
    {
        if (lr != null && Application.isEditor)
        {
            RenderArc();
        }
    }
    private void RenderArc()
    {
        lr.SetVertexCount(resolution +1);
        lr.SetPositions(CalculationArcArray());
    }

    private Vector3[] CalculationArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution+1];
        //Deg2Rad 度到弧度换算常量
        radianAngle =  Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i /(float) resolution;
            arcArray[i] = CalculationArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    private Vector3 CalculationArcPoint(float t, float maxDistance)
    {
       float x = t * maxDistance;
       float y = x * MathF.Tan(radianAngle) - ((g*x*x)/(2 *velocity * velocity * MathF.Cos(radianAngle)* MathF.Cos(radianAngle)));
        
      return  new Vector2(x, y);
    }
}

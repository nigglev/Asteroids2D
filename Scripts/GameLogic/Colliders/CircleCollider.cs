using System;
using System.Collections.Generic;
using UnityEngine;


public class CCircleCollider : CCollider
{
    private float _radius; public float Radius => _radius;
    LineRenderer line;
    
    public CCircleCollider(CircleCollider2D inCircle2D) : base(inCircle2D.offset.x, inCircle2D.offset.y, EColliderTypes.CircleCollider)
    {
        _radius = inCircle2D.radius;
        line = new LineRenderer();
    }

    public CCircleCollider(float in_local_centerx, float in_local_centerY, float in_radius) : base(in_local_centerx, in_local_centerY, EColliderTypes.CircleCollider)
    {
        _radius = in_radius;
        line = new LineRenderer();
    }

    override public void Draw()
    {
        var segments = 360;
        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new List<Vector2>();

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points.Add(new Vector2(WorldCenter.x + Mathf.Cos(rad) * _radius, WorldCenter.y + Mathf.Sin(rad) * _radius));
            if(points.Count >= 2)
            {
                Debug.DrawLine(points[i - 1], points[i]);
            }
        }

        
    }

    override public void UpdateRotation(Matrix2x2 in_rotation)
    {

    }


}

using System;
using UnityEngine;

public abstract class CCollider
{
    
    private float _local_centerX; 
    public float LocalCenterX => _local_centerX;

    private float _local_centerY; 
    public float LocalCenterY => _local_centerY;
    private EColliderTypes _type; 
    public EColliderTypes ColliderType => _type;

    private Vector2 _local_center;
    public Vector2 LocalCenter => _local_center;

    private Vector2 _world_center;
    public Vector2 WorldCenter => _world_center;
    public CCollider(float in_local_centerX, float in_local_centerY, EColliderTypes in_type)
    {
        _local_centerX = in_local_centerX;
        _local_centerY = in_local_centerY;
        _local_center = new Vector2(_local_centerX, _local_centerY);
        _world_center = Vector2.zero;
        _type = in_type;
    }

    public void UpdatePosition(Vector2 in_obj_position)
    {
        _world_center = new Vector2(in_obj_position.x + _local_centerX, in_obj_position.y + _local_centerY);
    }

    abstract public void UpdateRotation(Matrix2x2 in_rotation);
    abstract public void Draw();
}



using UnityEditor;
using UnityEngine;


public class CBoxCollider : CCollider
{
    private float _width; public float Width => _width;
    private float _height; public float Height => _height;

    private float _rotation_angle;

    private Matrix2x2 _rotation_matrix;
    private Vector2 _left_bottom;
    private Vector2 _left_top;
    private Vector2 _right_top;
    private Vector2 _right_bottom;

    //Matrix4x4
    

    public CBoxCollider(BoxCollider2D inBox2D) : base(inBox2D.offset.x, inBox2D.offset.y, EColliderTypes.BoxCollider)
    {
        _width = inBox2D.size.x;
        _height = inBox2D.size.y;
        _rotation_matrix = new Matrix2x2(1,0,0,1);

        _left_bottom = new Vector2(LocalCenterX - _width / 2, LocalCenterY - _height / 2);
        _left_top = _left_bottom + new Vector2(0, _height);
        _right_top = _left_top + new Vector2(_width, 0);
        _right_bottom = _right_top - new Vector2(0, _height);
    }

    public CBoxCollider(float in_local_centerX, float in_local_centerY, float in_width, float in_height) : base(in_local_centerX, in_local_centerY, EColliderTypes.BoxCollider)
    {
        _width = in_width;
        _height = in_height;
        _rotation_matrix = _rotation_matrix = new Matrix2x2(1, 0, 0, 1);

        _left_bottom = new Vector2(LocalCenterX - _width / 2, LocalCenterY - _height / 2);
        _left_top = _left_bottom + new Vector2(0, _height);
        _right_top = _left_top + new Vector2(_width, 0);
        _right_bottom = _right_top - new Vector2(0, _height);
    }
    override public void UpdateRotation(Matrix2x2 in_rotation)
    {
        _rotation_matrix = in_rotation;
    }

    override public void Draw()
    {
        Vector2 lb = (WorldCenter - LocalCenter) + _left_bottom * _rotation_matrix;
        Vector2 lt = (WorldCenter - LocalCenter) + _left_top * _rotation_matrix;
        Vector2 rt = (WorldCenter - LocalCenter) + _right_top * _rotation_matrix;
        Vector2 rb = (WorldCenter - LocalCenter) + _right_bottom * _rotation_matrix;

        Debug.DrawLine(lb, lt);
        Debug.DrawLine(lt, rt);
        Debug.DrawLine(rt, rb);
        Debug.DrawLine(rb, lb);
    }
}
        

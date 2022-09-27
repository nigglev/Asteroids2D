using System.Drawing;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

public class AABB
{
	private Vector2 _center; public Vector2 Center => _center;
	private float _x; public float X => _x;
	private float _y; public float Y => _y;

	private float _width; public float Width => _width;
	private float _height; public float Height => _height;

	private Vector2 _left_bottom;
	private Vector2 _left_top;
	private Vector2 _right_top;
	private Vector2 _right_bottom;

	public AABB(float in_x, float in_y, float in_width, float in_height)
	{
		_x = in_x;
		_y = in_y;
		_center = new Vector2(in_x, in_y);

		_width = in_width;
		_height = in_height;

		CalculateEdgePoints();
	}

	public AABB(Vector2 in_center, float in_width, float in_height)
	{
		_center = in_center;
		_x = in_center.x;
		_y = in_center.y;

		_width = in_width;
		_height = in_height;

		CalculateEdgePoints();
	}

	private void CalculateEdgePoints()
	{
		_left_bottom = new Vector2(_center.x - _width / 2, _center.y - _height / 2);
		_left_top = _left_bottom + new Vector2(0, _height);
		_right_top = _left_top + new Vector2(_width, 0);
		_right_bottom = _right_top - new Vector2(0, _height);
	}

	public void Update(Vector2 in_new_center)
	{
		_center = in_new_center;
		CalculateEdgePoints();

		//Debug.DrawLine(_left_bottom, _left_top);
		//Debug.DrawLine(_left_top, _right_top);
		//Debug.DrawLine(_right_top, _right_bottom);
		//Debug.DrawLine(_right_bottom, _left_bottom);

	}

}

		
using System;
using UnityEngine;

public struct Matrix2x2
{
    private float _elem11; public float Elem11 => _elem11;
    private float _elem12; public float Elem12 => _elem12;
    private float _elem21; public float Elem21 => _elem21;
    private float _elem22; public float Elem22 => _elem22;

    private float _rotate_angle;

    public Matrix2x2(float in_elem11, float in_elem12, float in_elem21, float in_elem22)
    {
        _elem11 = in_elem11;
        _elem12 = in_elem12;
        _elem21 = in_elem21;
        _elem22 = in_elem22;
        _rotate_angle = 0f;
    }

    public Matrix2x2(Vector2 in_row1, Vector2 in_row2)
    {
        _elem11 = in_row1.x;
        _elem12 = in_row1.y;
        _elem21 = in_row2.x;
        _elem22 = in_row2.y;
        _rotate_angle = 0f;
    }

    public Matrix2x2(float[] in_elements)
    {
        if (in_elements.Length != 4)
        {
            throw new Exception("Wrong number of elements");
        }

        _elem11 = in_elements[0];
        _elem12 = in_elements[1];
        _elem21 = in_elements[2];
        _elem22 = in_elements[3];
        _rotate_angle = 0f;
    }

    public static Matrix2x2 operator *(Matrix2x2 M1, Matrix2x2 M2)
    {
        float e11 = M1.Elem11 * M2.Elem11 + M1.Elem12 * M2.Elem21;
        float e12 = M1.Elem11 * M2.Elem12 + M1.Elem12 * M2.Elem22;
        float e21 = M1.Elem21 * M2.Elem11 + M1.Elem22 * M2.Elem21;
        float e22 = M1.Elem21 * M2.Elem12 + M1.Elem22 * M2.Elem22;

        return new Matrix2x2(e11, e12, e21, e22);
    }


    public static Vector2 operator *(Vector2 in_vector, Matrix2x2 M)
    {
        return new Vector2(in_vector.x * M.Elem11 + in_vector.y * M.Elem12, in_vector.x * M.Elem21 + in_vector.y * M.Elem22);
    }

    public static Matrix2x2 operator *(float in_scalar, Matrix2x2 M)
    {
        return new Matrix2x2(in_scalar * M.Elem11, in_scalar * M.Elem12, in_scalar * M.Elem21, in_scalar * M.Elem22);
    }

    public float ToAngle()
    {
        float r = Mathf.Asin(_elem12) * 180 / Mathf.PI;
        if (Mathf.Abs(r) < Mathf.Abs(_rotate_angle))
        {
            _rotate_angle = Mathf.Sign(r) * 90 + r;
            return _rotate_angle;
        }
        _rotate_angle = r;
        return _rotate_angle;
    }
}

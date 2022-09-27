using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera
{
    private Camera _camera;
    float _half_width; public float CameraHalfWidth => _half_width;
    float _half_height; public float CameraHalfHeight => _half_height;
    public CCamera()
    {
        _camera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        
        
    }

    

}

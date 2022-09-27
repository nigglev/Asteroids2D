

using UnityEngine;

namespace GameView
{
    public interface IViewObject
    {
        void SetPosition(Vector2 inPosition);
        //void SetRotation(Quaternion inRotation);

        void SetRotation(Matrix2x2 in_rotation_mat);

    }
} 
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    public class CSpaceship : CEntity
    {
        private IWorld _world;
        private float _force = 3f;
        private float _rotation_speed = 30f;
        private float _thrust_input;
        

        public CSpaceship(IWorld in_world, EEntityTypes inType, ulong in_id, Vector2 in_position, List<CCollider> in_colliders) : base(inType, in_id, in_position, in_colliders)
        {
            _world = in_world;
        }

        public override void Update(float inTime)
        {
            ChangePositionIfOutOfBounds();

            base.Update(inTime);
            
            UpdateInputRotate(inTime);
            UpdateInputThrust(inTime);
        }
        
        private void UpdateInputRotate(float inTime)
        {
            float rotate_input = -_world.GetPlayerActions().Rotation.ReadValue<float>();
            if(rotate_input != 0)
            {
                float rotation = inTime * rotate_input * _rotation_speed;
                Matrix2x2 rotation_matrix = new Matrix2x2(Mathf.Cos(rotation * Mathf.PI / 180), Mathf.Sin(rotation * Mathf.PI / 180), -Mathf.Sin(rotation * Mathf.PI / 180), Mathf.Cos(rotation * Mathf.PI / 180));
                SetRotationMatrix(rotation_matrix);
                Debug.Log($"Turning By {rotation_matrix.ToAngle()}//{rotation} Degrees");
                
            }
        }

        private void ChangePositionIfOutOfBounds()
        {
            if (Position.x > _world.WorldRightBound || Position.x < _world.WorldLeftBound)
                UpdatePosition(new Vector2(_world.WorldCenter.x - Position.x, Position.y));

            if (Position.y > _world.WorldUpperBound || Position.y < _world.WorldLowerBound)
                UpdatePosition(new Vector2(Position.x, _world.WorldCenter.x - Position.y));
        }

        private void UpdateInputThrust(float inTime)
        {
            float thrust_input = _world.GetPlayerActions().Thrust.ReadValue<float>();
            if (!Mathf.Approximately(_thrust_input, thrust_input))
            {
                _thrust_input = thrust_input;
            }

            float add_vel = _force * _thrust_input * inTime;
            AddFrowardVelocity(add_vel);
        }
    }

}
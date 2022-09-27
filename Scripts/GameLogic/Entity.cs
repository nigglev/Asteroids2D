using GameView;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class CEntity
    {
        private ulong _entity_id; 
        private EEntityTypes _type;
        
        private Vector2 _position;
        private Quaternion _rotation;
        private Vector2 _velocity;
        private Matrix2x2 _rotation_matrix;

        //private AABB _aabb;
        private List<CCollider> _colliders;
        public List<CCollider> Colliders => _colliders;

        private IViewObject _view_obj;

        public ulong EntityID => _entity_id;
        public EEntityTypes EntityType => _type;
        public Vector2 Position => _position;
        public Quaternion Rotation => _rotation;
        public Vector2 Forward => _rotation * Vector2.up;

        public CEntity(EEntityTypes inType, ulong in_id, Vector2 in_position, List<CCollider> in_colliders)
        {
            _type = inType;
            _entity_id = in_id;
            _position = in_position;
            
            _rotation = Quaternion.identity;
            _rotation_matrix = new Matrix2x2(1, 0, 0, 1);

            _colliders = in_colliders;

            foreach (CCollider c in _colliders)
            {
                c.UpdateRotation(_rotation_matrix);
                c.UpdatePosition(_position);
            }
        }

        public virtual void Update(float inTime)
        {
            Vector2 v = _velocity * inTime;
            if (!Mathf.Approximately(v.sqrMagnitude, 0))
            {
                _position += v;
                foreach (CCollider c in _colliders)
                    c.UpdatePosition(_position);

                if (_view_obj != null)
                    _view_obj.SetPosition(_position);
            }
            //Debug.Log($"{_type} Position = [{_position.x}, {_position.y}]");

            foreach (CCollider c in _colliders)
                c.UpdateRotation(_rotation_matrix);


            //Debug.Log($"Current rotation is {_rotation_matrix.ToAngle()} Degrees");

            if (_view_obj != null)
                _view_obj.SetRotation(_rotation_matrix);

            foreach (CCollider c in _colliders)
            {
                c.Draw();
            }
        }

        protected void SetRotationMatrix(in Matrix2x2 in_rotation)
        {
            _rotation_matrix = _rotation_matrix * in_rotation;
        }

        protected void AddFrowardVelocity(float inVel)
        {
            _velocity += Forward * inVel;
        }

        public virtual void SetViewObject(IViewObject inViewObj)
        {
            _view_obj = inViewObj;
            Debug.Log("add ViewObj");
        }


        protected void UpdatePosition(Vector2 in_new_pos)
        {
            _position = in_new_pos;
            if (_view_obj != null)
                _view_obj.SetPosition(_position);
        }

    }
}
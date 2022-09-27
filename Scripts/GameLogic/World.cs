using GameView;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class CWorld : IWorld
    {
        private CSpaceship _spaceship;
        private CAsteroidBig _asteroid_big;
        private GameManager _gameManager;
        Dictionary<NamedId, CAdressablePrefabSpriteData> _sprites_descr = new Dictionary<NamedId, CAdressablePrefabSpriteData>(new NamedId.Comparer());
        Dictionary<NamedId, CAdressablePrefabColliderData> _colliders_descr = new Dictionary<NamedId, CAdressablePrefabColliderData>(new NamedId.Comparer());

        private List<CEntity> _entities = new List<CEntity>();

        private ulong _entity_id_counter;

        private float _world_width; public float WorldWidth => _world_width; 
        private float _world_height; public float WorldHeight => _world_height; 
        private float _world_upper_bound; public float WorldUpperBound => _world_upper_bound; 
        private float _world_lower_bound; public float WorldLowerBound => _world_lower_bound; 
        private float _world_left_bound; public float WorldLeftBound => _world_left_bound; 
        private float _world_right_bound; public float WorldRightBound => _world_right_bound; 
        private Vector2 _world_center; public Vector2 WorldCenter => _world_center;

        public ulong GetNewId()
        {
            return ++_entity_id_counter;
        }

        public CWorld(GameManager in_gameManager, float in_half_width, float in_half_height)
        {   
            _gameManager = in_gameManager;
            _world_width = in_half_width * 2;
            _world_height = in_half_height * 2;
            _world_center = Vector2.zero;
            _world_upper_bound = in_half_height;
            _world_lower_bound = -in_half_height;
            _world_right_bound = in_half_width;
            _world_left_bound = -in_half_width;
        }

        public void Init()
        {
            var obj_sprite_sizes = FileManager.ReadPrefabSpriteData();
            var obj_colliders = FileManager.ReadPrefabCollidersData();

            for (int i = 0; i < obj_sprite_sizes.Count; i++)
                _sprites_descr.Add(NamedId.GetNamedId(obj_sprite_sizes[i].Name), obj_sprite_sizes[i]);
            for (int i = 0; i < obj_colliders.Count; i++)
                _colliders_descr.Add(NamedId.GetNamedId(obj_colliders[i].AttachedObject), obj_colliders[i]);
            


            SpawnAsteroidBig();
            SpawnPlayerShip();
        }

        public void Update(float inTime)
        {
            
            for (int i = 0; i < _entities.Count; i++)
                _entities[i].Update(inTime);
        }
        
        public PlayerInput.PlayerActions GetPlayerActions()
        {
            return _gameManager.PlayerInput.Player;
        }

        public void SetViewObjectById(IViewObject in_view_object, ulong id)
        {
            var entity = _entities.Find(vo => vo.EntityID == id);
            entity.SetViewObject(in_view_object);
        }

        public void SpawnAsteroidBig()
        {
            Vector2 asteroid_big_size = GetSpriteSize(NamedId.GetNamedId(EEntityTypes.AsteroidBig.ToString()));
            var asteroid_big_colliders = GetColliders(NamedId.GetNamedId(EEntityTypes.AsteroidBig.ToString()));
            _asteroid_big = new CAsteroidBig(this, EEntityTypes.AsteroidBig, GetNewId(), new Vector2(0, _world_upper_bound - 5f), asteroid_big_colliders);
            _entities.Add(_asteroid_big);
            _gameManager.RequestViewObject(_asteroid_big.EntityType, _asteroid_big.EntityID, _asteroid_big.Position);
        }

        private void SpawnPlayerShip()
        {
            Vector2 spaceship_size = GetSpriteSize(NamedId.GetNamedId(EEntityTypes.Ship.ToString()));
            var spaceship_colliders = GetColliders(NamedId.GetNamedId(EEntityTypes.Ship.ToString()));
            _spaceship = new CSpaceship(this, EEntityTypes.Ship, GetNewId(), new Vector2(0, _world_lower_bound + 5f), spaceship_colliders);
            _entities.Add(_spaceship);
            _gameManager.RequestViewObject(_spaceship.EntityType, _spaceship.EntityID, _spaceship.Position);
        }

        private Vector2 GetSpriteSize(NamedId in_object_name)
        {
            if(_sprites_descr.TryGetValue(in_object_name, out var descr))
                return new Vector2(descr.SpriteWidth, descr.SpriteHeight);
            return Vector2.one;
        }


        private List<CCollider> GetColliders(NamedId in_object_name)
        {
            if (_colliders_descr.TryGetValue(in_object_name, out var descr))
                return descr.Colliders;
            
            return null;
        }
    }

}
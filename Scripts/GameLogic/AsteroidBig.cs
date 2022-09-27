using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    internal class CAsteroidBig : CEntity
    {
        private IWorld _world;

        public CAsteroidBig(IWorld in_world, EEntityTypes inType, ulong in_id, Vector2 in_position, List<CCollider> in_colliders) : base(inType, in_id, in_position, in_colliders)
        {
            _world = in_world;
        }
    }
}
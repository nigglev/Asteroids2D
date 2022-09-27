using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CAdressablePrefabColliderData
{
    public string AttachedObject { get; set; }

    [JsonConverter(typeof(ColliderListConverter))]
    public List<CCollider> Colliders { get; set; }

    public CAdressablePrefabColliderData()
    {
        Colliders = new List<CCollider>();
    }
}


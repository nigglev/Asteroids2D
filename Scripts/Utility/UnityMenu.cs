using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class UnityMenu : MonoBehaviour
{
    [MenuItem("Adressables menu/Read Adressables Sprite Data")]
    private static void CollectAdressablesSpriteData()
    {
        Debug.Log("Collecting Sprite Data");

        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        List<CAdressablePrefabSpriteData> lst_prefab_sprite_data = new List<CAdressablePrefabSpriteData>();

        for (int i = 0; i < settings.groups.Count; i++)
        {
            AddressableAssetGroup group = settings.groups[i];
            if (group.Name.Equals("GamePrefabs"))
            {
                foreach (AddressableAssetEntry entry in group.entries)
                {

                    GameObject go = entry.MainAsset as GameObject;
                    if (go == null)
                    {
                        Debug.LogError($"{entry.MainAsset.name} in group {group.Name} isn't GameObject!");
                        return;
                    }

                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    float sprite_width;
                    float sprite_height;

                    if (sr != null)
                    {
                        sprite_width = sr.bounds.size.x;
                        sprite_height = sr.bounds.size.y;

                    }
                    else
                    {
                        Debug.Log("Game Object does not have sprite");
                        return;
                    }

                    var prefab_data = new CAdressablePrefabSpriteData();
                    prefab_data.Name = entry.address;
                    prefab_data.SpriteWidth = sprite_width;
                    prefab_data.SpriteHeight = sprite_height;
                    lst_prefab_sprite_data.Add(prefab_data);
                    Debug.Log($"{prefab_data.Name} sprite added to adressable prefab data");

                }
            }
        }

        FileManager.WritePrefabSpriteData(lst_prefab_sprite_data);

    }


    [MenuItem("Adressables menu/Read Adressables Colliders Data")]
    private static void CollectAdressablesCollidersData()
    {
        Debug.Log("Collecting Colliders Data");
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        List<CAdressablePrefabColliderData> obj_colliders = new List<CAdressablePrefabColliderData>();
        //List<ObjColliderPair> obj_colliders = new List<ObjColliderPair>();

        for (int i = 0; i < settings.groups.Count; i++)
        {
            AddressableAssetGroup group = settings.groups[i];
            if (group.Name.Equals("GamePrefabs"))
            {
                foreach (AddressableAssetEntry entry in group.entries)
                {

                    GameObject go = entry.MainAsset as GameObject;
                    if (go == null)
                    {
                        Debug.LogError($"{entry.MainAsset.name} in group {group.Name} isn't GameObject!");
                        continue;
                    }

                    List<Collider2D> colliders = new List<Collider2D>();
                    go.GetComponents(colliders);
                    if(colliders.Count == 0)
                    {
                        Debug.Log($"Object {entry.address} has no colliders");
                        continue;
                    }

                    CAdressablePrefabColliderData data = new CAdressablePrefabColliderData();
                    data.AttachedObject = entry.address;

                    foreach (Collider2D c in colliders)
                    {
                        if(c is BoxCollider2D)
                        {
                            CBoxCollider bc = new CBoxCollider(c as BoxCollider2D);
                            data.Colliders.Add(bc);
                            //obj_colliders.Add(new ObjColliderPair
                            //{
                            //    attached_obj = entry.address,
                            //    collider = bc
                            //});
                        }

                        if(c is CircleCollider2D)
                        {   
                            CCircleCollider cc = new CCircleCollider(c as CircleCollider2D);
                            data.Colliders.Add(cc);
                            //obj_colliders.Add(new ObjColliderPair
                            //{
                            //    attached_obj = entry.address,
                            //    collider = cc
                            //});
                        }
                        
                    }

                    obj_colliders.Add(data);
                }
            }
        }

        FileManager.WritePrefabCollidersData(obj_colliders);
    }

}





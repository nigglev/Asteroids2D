using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class FileManager
{
    public static List<CAdressablePrefabSpriteData> ReadPrefabSpriteData()
    {
        List<CAdressablePrefabSpriteData> prefab_data = null;
        string fn = Path.Combine(Application.dataPath, "Prefabs Sprite data");
        if (File.Exists(fn))
        {
            using (StreamReader reader = File.OpenText(fn))
            {
                string txt = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(txt))
                {
                    try
                    {
                        prefab_data = JsonConvert.DeserializeObject<List<CAdressablePrefabSpriteData>>(txt);
                        
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }
        return prefab_data;
    }

    public static List<CAdressablePrefabColliderData> ReadPrefabCollidersData()
    {
        List<CAdressablePrefabColliderData> prefab_data = null;
        string fn = Path.Combine(Application.dataPath, "Prefabs Colliders data");
        if (File.Exists(fn))
        {
            using (StreamReader reader = File.OpenText(fn))
            {
                string txt = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(txt))
                {
                    try
                    {
                        prefab_data = JsonConvert.DeserializeObject<List<CAdressablePrefabColliderData>>(txt, new ColliderConverter());

                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }
        return prefab_data;
    }

    public static void WritePrefabSpriteData(List<CAdressablePrefabSpriteData> in_prefabs_data)
    {
        string txt = string.Empty;
        try
        {
            txt = JsonConvert.SerializeObject(in_prefabs_data);
            Debug.LogError(txt);
            string fn = Path.Combine(Application.dataPath, "Prefabs Sprite data");
            using (StreamWriter writer = File.CreateText(fn))
            {
                writer.Write(txt);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static void WritePrefabCollidersData(List<CAdressablePrefabColliderData> in_colliders_data)
    {
        string txt = string.Empty;
        try
        {
            txt = JsonConvert.SerializeObject(in_colliders_data, Formatting.Indented);
            Debug.LogError(txt);
            string fn = Path.Combine(Application.dataPath, "Prefabs Colliders data");
            using (StreamWriter writer = File.CreateText(fn))
            {
                writer.Write(txt);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}

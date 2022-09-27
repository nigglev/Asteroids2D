using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class ColliderConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(CCollider);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {

        writer.WriteStartArray();

        CCollider c = (CCollider)value;
        writer.WriteValue(c.ColliderType);
        writer.WriteValue(c.LocalCenterX);
        writer.WriteValue(c.LocalCenterY);
        if (c.ColliderType == EColliderTypes.BoxCollider)
        {
            writer.WriteValue((c as CBoxCollider).Width);
            writer.WriteValue((c as CBoxCollider).Height);

        }
        if (c.ColliderType == EColliderTypes.CircleCollider)
        {
            writer.WriteValue((c as CCircleCollider).Radius);
        }

        writer.WriteEndArray();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        int? e = reader.ReadAsInt32();
        if (!e.HasValue) return null;
        EColliderTypes ct = (EColliderTypes)e.Value;

        double? d = reader.ReadAsDouble();
        if (!d.HasValue) return null;
        float CenterX = (float)d.Value;

        d = reader.ReadAsDouble();
        if (!d.HasValue) return null;
        float CenterY = (float)d.Value;

        CCollider col = null;
        if (ct == EColliderTypes.BoxCollider)
        {
            d = reader.ReadAsDouble();
            if (!d.HasValue) return null;
            float Width = (float)d.Value;

            d = reader.ReadAsDouble();
            if (!d.HasValue) return null;
            float Height = (float)d.Value;

            CBoxCollider bc = new CBoxCollider(CenterX, CenterY, Width, Height);
            col = bc;
        }

        if (ct == EColliderTypes.CircleCollider)
        {
            d = reader.ReadAsDouble();
            if (!d.HasValue) return null;
            float Radius = (float)d.Value;

            CCircleCollider cc = new CCircleCollider(CenterX, CenterY, Radius);
            col = cc;
        }

        reader.ReadAsDouble();
        return col;
    }
}


public class ColliderListConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<CCollider>);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {

        List<CCollider> v = value as List<CCollider>;

        writer.WriteStartArray();

        foreach (CCollider c in v)
        {
            writer.WriteStartArray();

            writer.WriteValue(c.ColliderType);
            writer.WriteValue(c.LocalCenterX);
            writer.WriteValue(c.LocalCenterY);

            if (c.ColliderType == EColliderTypes.BoxCollider)
            {
                writer.WriteValue((c as CBoxCollider).Width);
                writer.WriteValue((c as CBoxCollider).Height);

            }
            if (c.ColliderType == EColliderTypes.CircleCollider)
            {
                writer.WriteValue((c as CCircleCollider).Radius);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        List<CCollider> res = new List<CCollider>();

        JToken jtoken = JToken.Load(reader);

        if (jtoken.Type == JTokenType.Array)
        {
            JArray jarr = jtoken as JArray;
            for (int i = 0; i < jarr.Count; i++)
            {
                JToken collider_info = jarr[i];
                var t = collider_info[0].Value<int>();
                if (t == (int)EColliderTypes.BoxCollider)
                {
                    float centerX = collider_info[1].Value<float>();
                    float centerY = collider_info[2].Value<float>();
                    float width = collider_info[3].Value<float>();
                    float height = collider_info[4].Value<float>();
                    CBoxCollider bc = new CBoxCollider(centerX, centerY, width, height);
                    res.Add(bc);
                }

                if (t == (int)EColliderTypes.CircleCollider)
                {
                    float centerX = collider_info[1].Value<float>();
                    float centerY = collider_info[2].Value<float>();
                    float radius = collider_info[3].Value<float>();
                    CCircleCollider cc = new CCircleCollider(centerX, centerY, radius);
                    res.Add(cc);
                }

            }

        }

        return res;
    }
}


using Newtonsoft.Json;

namespace TiersOfHeckCodes;

public class TierJson
{
    public static JsonTier Deserialize(string inp)
    {
        var ser = JsonConvert.DeserializeObject<JsonTier>(inp);
        if (ser == null)
        {
            throw new Exception("Error deserializing json!");
        }

        return ser;
    }

    public static string Serialize(List<TierJson.TierEnemy> enemies, List<TierJson.TierModifier> modifiers, List<TierJson.TierWeapon> weapons, TierJson.TierMap map, TierJson.TierDifficultyIncrease difficultyIncrease)
    {
        var e = new List<int>();
        var f = new List<int>();
        var g = new List<int>();
        
        var w = new List<int>();
        var v = new List<int>();
        
        var m = new List<int>();
        var n = new List<int>();

        int l;
        int i;

        foreach (var enemy in enemies)
        {
            e.Add((int)enemy.name);
            g.Add((int)enemy.cost);
            f.Add(enemy.minWave);
        }

        foreach (var modifier in modifiers)
        {
            m.Add((int)modifier.name);
            n.Add(modifier.level);
        }

        foreach (var weapon in weapons)
        {
            w.Add((int)weapon.name);
            v.Add(weapon.rarity);
        }

        l = (int)map.name;
        i = difficultyIncrease.threshold;

        var jsonTier = new JsonTier(l, w.ToArray(), v.ToArray(), e.ToArray(), f.ToArray(), g.ToArray(), m.ToArray(), n.ToArray(), i);

        return JsonConvert.SerializeObject(jsonTier);
    }

    public class TierEnemy
    {
        public static TierEnemy Get(JsonTier json, int index)
        {
            return new TierEnemy((SerializationEnemyName)json.e[index], json.g[index], json.f[index]);
        }

        public static List<TierEnemy> GetAll(JsonTier json)
        {
            var l = new List<TierEnemy>();
            for(var i = 0; i < json.e.Length; i++)
            {
                l.Add(Get(json, i));
            }

            return l;
        }

        public TierEnemy(SerializationEnemyName name, float cost, int minWave)
        {
            this.name = name;
            this.cost = cost;
            this.minWave = minWave;
        }

        public SerializationEnemyName name;
        public float cost;
        public int minWave;
    }
    public class TierModifier
    {
        public static TierModifier Get(JsonTier json, int index)
        {
            return new TierModifier((SerializationModifierName)json.m[index], json.n[index]);
        }

        public static List<TierModifier> GetAll(JsonTier json)
        {
            var l = new List<TierModifier>();
            for(var i = 0; i < json.m.Length; i++)
            {
                l.Add(Get(json, i));
            }

            return l;
        }

        public TierModifier(SerializationModifierName name, int level)
        {
            this.name = name;
            this.level = level;
        }

        public SerializationModifierName name;
        public int level;
    }
    public class TierWeapon
    {
        public static TierWeapon Get(JsonTier json, int index)
        {
            return new TierWeapon((SerializationWeaponName)json.w[index], json.v[index]);
        }

        public static List<TierWeapon> GetAll(JsonTier json)
        {
            var l = new List<TierWeapon>();
            for(var i = 0; i < json.w.Length; i++)
            {
                l.Add(Get(json, i));
            }

            return l;
        }

        public TierWeapon(SerializationWeaponName name, int rarity)
        {
            this.name = name;
            this.rarity = rarity;
        }

        public SerializationWeaponName name;
        public int rarity;
    }

    public class TierMap
    {
        public static TierMap Get(JsonTier json)
        {
            return new TierMap((SerializationMapMame)json.l);
        }

        public SerializationMapMame name;

        public TierMap(SerializationMapMame name)
        {
            this.name = name;
        }
    }
    public class TierDifficultyIncrease
    {
        public TierDifficultyIncrease(int threshold)
        {
            this.threshold = threshold;
        }

        public int threshold;

        public static TierDifficultyIncrease Get(JsonTier json)
        {
            return new TierDifficultyIncrease(json.i);
        }
    }
}
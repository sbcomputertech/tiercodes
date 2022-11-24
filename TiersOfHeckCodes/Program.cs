using System;

namespace TiersOfHeckCodes;

public class Program
{
    public static void Main()
    {
        Console.Write("Would you like to decode or encode? ");
        var dOrE = Console.ReadLine();
        if (dOrE == "decode")
        {
            Decode();
        }
        else
        {
            Encode();
        }
    }

    private static void Decode()
    {
        Console.Write("Enter tier code: ");
        var orig = Console.ReadLine();
        if (orig == null)
        {
            Console.WriteLine("Please provide a code");
        }
        else
        {
            var json = Decompressor.Decompress(orig);
            Console.WriteLine("JSON: " + json + "\n");
            var tier = TierJson.Deserialize(json);
            
            var enemies = TierJson.TierEnemy.GetAll(tier);
            var modifiers = TierJson.TierModifier.GetAll(tier);
            var weapons = TierJson.TierWeapon.GetAll(tier);
            var map = TierJson.TierMap.Get(tier);
            var difficultyIncrese = TierJson.TierDifficultyIncrease.Get(tier);

            Console.WriteLine("\nDone");
        }
        
    }

    private static void Encode()
    {
        var enemies = new List<TierJson.TierEnemy>();
        var modifiers = new List<TierJson.TierModifier>();
        var weapons = new List<TierJson.TierWeapon>();
        var map = new TierJson.TierMap(SerializationMapMame.Fungi);
        var difficultyIncrease = new TierJson.TierDifficultyIncrease(1);

        var inp1 = MultiPrompt("Enter: enemy name, space, enemy count, space, wave number");
        foreach (var input1 in inp1)
        {
            var split = input1.Split(" ");
            var enemyCost = int.Parse(split[1]);
            var enemyCount = int.Parse(split[2]);
            var enemyName = Enum.Parse<SerializationEnemyName>(split[0]);
            enemies.Add(new TierJson.TierEnemy(enemyName, enemyCost, enemyCount));
        }
        var inp2 = MultiPrompt("Enter: modifier name, space, modifier level");
        foreach (var input2 in inp2)
        {
            var split = input2.Split(" ");
            var modLevel = int.Parse(split[1]);
            var modName = Enum.Parse<SerializationModifierName>(split[0]);
            modifiers.Add(new TierJson.TierModifier(modName, modLevel));
        }
        var inp3 = MultiPrompt("Enter: weapon name, space, weapon rarity");
        foreach (var input3 in inp3)
        {
            var split = input3.Split(" ");
            var weaponRarity = int.Parse(split[1]);
            var weaponName = Enum.Parse<SerializationWeaponName>(split[0]);
            weapons.Add(new TierJson.TierWeapon(weaponName, weaponRarity));
        }
        
        Console.Write("Enter map name: ");
        var inp4 = Console.ReadLine() ?? "Launch";
        var mapName = Enum.Parse<SerializationMapMame>(inp4);
        map = new TierJson.TierMap(mapName);
        
        Console.Write("Enter difficulty increase threshold: ");
        var inp5 = Console.ReadLine() ?? "1";
        var threshold = int.Parse(inp5);
        difficultyIncrease = new TierJson.TierDifficultyIncrease(threshold);

        var json = TierJson.Serialize(enemies, modifiers, weapons, map, difficultyIncrease);
        var code = Decompressor.Compress(json);
        Console.WriteLine("Code: " + code);
    }

    private static List<string> MultiPrompt(string initPrompt)
    {
        var inputs = new List<string>();
        Console.WriteLine(initPrompt);
        Console.WriteLine("Press enter on a blank line to stop input");
        while (true)
        {
            Console.Write("> ");
            var inp = Console.ReadLine();
            if (string.IsNullOrEmpty(inp))
            {
                break;
            }
            inputs.Add(inp);
        }

        return inputs;
    }
}
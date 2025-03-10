using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static Dictionary<char, List<string>> letters;
    static List<string> spaces;

    static void Main()
    {
        IdentLetters();
        IdentSpaces();

        Console.WriteLine("Text:");
        string input = Console.ReadLine();

        Console.WriteLine("Chance (1-100):");
        int chance = int.Parse(Console.ReadLine());

        if (chance < 1 || chance > 100)
        {
            Console.WriteLine("Default: 33%");
            chance = 33;
        }

        Random rand = new Random();
        StringBuilder shitification = new StringBuilder();

        foreach (char c in input)
        {
            if (c == ' ' && rand.Next(100) < chance)
            {
                shitification.Append(spaces[rand.Next(spaces.Count)]);
            }
            else if (letters.ContainsKey(c) && rand.Next(100) < chance)
            {
                var vars = letters[c];
                shitification.Append(vars[rand.Next(vars.Count)]);
            }
            else
            {
                shitification.Append(c);
            }
        }

        string output = shitification.ToString();
        Console.WriteLine($"Result: {output}");

        string file = "brainrot.txt";

        File.WriteAllText(file, output);
        Console.WriteLine($"Saved: {file}");
    }

    static void IdentLetters()
    {
        string file = "fuel/letters.json";
        if (File.Exists(file))
        {
            var json = File.ReadAllText(file);
            letters = JsonConvert.DeserializeObject<Dictionary<char, List<string>>>(json);
        }
        else
        {
            Console.WriteLine("gone wrong.");
            Environment.Exit(1);
        }
    }

    static void IdentSpaces()
    {
        string file = "fuel/spaces.json";
        if (File.Exists(file))
        {
            var json = File.ReadAllText(file);
            spaces = JsonConvert.DeserializeObject<List<string>>(json);
        }
        else
        {
            Console.WriteLine("gone wrong.");
            Environment.Exit(1);
        }
    }
}

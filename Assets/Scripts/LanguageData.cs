using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageData
{
    private Language language;
    private string[] keys;
    private string[] values;

    public LanguageData(Language language)
    {
        this.language = language;
    }

    public void LoadFile(string file)
    {
        if (File.Exists(file))
        {
            string[] lines = File.ReadAllLines(file);

            List<string> keysNew = new List<string>();
            List<string> valuesNew = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Split('|').Length == 2)
                {
                    string left = lines[i].Split('|')[0];
                    string right = lines[i].Split('|')[1];

                    keysNew.Add(left);
                    valuesNew.Add(right);
                }
            }

            keys = keysNew.ToArray();
            values = valuesNew.ToArray();
        }
        else
        {
            keys = new string[0];
            values = new string[0];
        }
    }

    public string Translate(string english)
    {
        if (english.Split('\n').Length > 0 && english.Split('\n')[0].Length < english.Length)
        {
            string comb = Translate(english.Split('\n')[0]);
            for (int i = 1; i < english.Split('\n').Length; i++)
            {
                comb += "\n" + Translate(english.Split('\n')[i]);
            }

            return comb;
        }

        for (int i = 0; i < keys.Length; i++)
        {
            if (english == keys[i])
            {
                return values[i];
            }
        }

        return english;
    }

    public Language Language
    {
        get
        {
            return language;
        }
    }
}

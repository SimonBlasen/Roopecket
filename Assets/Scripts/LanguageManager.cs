using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Language
{
    GERMAN, ENGLISH, SPANISH
}

public class LanguageManager : MonoBehaviour
{
    private const string languageFileEnglish = "./languages/english.lan";
    private const string languageFileGerman = "./languages/german.lan";
    private const string languageFileSpanish = "./languages/spanish.lan";

    private static LanguageData[] languages = null;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro tmp = GetComponent<TextMeshPro>();
        if (tmp != null)
        {
            tmp.text = Translate(tmp.text);
        }

        TextMeshProUGUI tmpUGUI = GetComponent<TextMeshProUGUI>();
        if (tmpUGUI != null)
        {
            tmpUGUI.text = Translate(tmpUGUI.text);
        }

        Text textOldUI = GetComponent<Text>();
        if (textOldUI != null)
        {
            textOldUI.text = Translate(textOldUI.text);
        }
    }

    private static Language curLanguage = Language.ENGLISH;
    public static Language Language { get { return curLanguage; } set { curLanguage = value; } }

    public static string Translate(string text)
    {
        initLanguages();

        for (int i = 0; i < languages.Length; i++)
        {
            if (Language == languages[i].Language)
            {
                return languages[i].Translate(text);
            }
        }

        return text;
    }

    private static void initLanguages()
    {
        if (languages == null)
        {
            languages = new LanguageData[3];
            languages[0] = new LanguageData(Language.ENGLISH);
            languages[0].LoadFile(languageFileEnglish);
            languages[1] = new LanguageData(Language.GERMAN);
            languages[1].LoadFile(languageFileGerman);
            languages[2] = new LanguageData(Language.SPANISH);
            languages[2].LoadFile(languageFileSpanish);
        }
    }
}

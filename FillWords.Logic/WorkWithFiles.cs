using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FillWords.Console
{
    // Создание, открытие, изменение INI файла с настройками
    // Создание и обновление директории со словарем из гита, открытие файлов
    public class WorkWithFiles
    {
        private static string url = @"https://raw.githubusercontent.com/DemLog/Fillwords/master/dictionary/word_";

        private static bool CheckFilesDictionary(string path)
        {
            if (!Directory.Exists(path))
            {
                // Данные для логирования
                Directory.CreateDirectory(path);
                UpdateFilesDictionary(path);
                return false;
            }

            for (int i = 2; i <= 10; i++)
            {
                if (!File.Exists(path + $"\\word_{i}.txt"))
                {
                    UpdateFilesDictionary(path);
                    return false;
                }
            }
            return true;
        }

        private static void CreateFileSettings(string path)
        {
            INIManager manager = new INIManager(path);

            manager.WritePrivateString("SizeWindow", "width", "110");
            manager.WritePrivateString("SizeWindow", "height", "33");
            
            manager.WritePrivateString("Game", "SizeArea", "5");
            manager.WritePrivateString("Game", "ColorAreaBackground", "Black");
            manager.WritePrivateString("Game", "ColorAreaText", "White");
            manager.WritePrivateString("Game", "ColorHighlighting", "Gray");
            manager.WritePrivateString("Game", "ColorHighlightingWord", "Gray");
            manager.WritePrivateString("Game", "ColorGuessedWord", "Red");
            manager.WritePrivateString("Game", "ColorRandomGuessedWord", "True");
        }

        public static Dictionary<string, string> GetFileSettings(string path)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            if (!File.Exists(path))
            {
                CreateFileSettings(path);
            }
            INIManager manager = new INIManager(path);
            settings.Add("Width", manager.GetPrivateString("SizeWindow", "width"));
            settings.Add("Height", manager.GetPrivateString("SizeWindow", "height"));
            
            settings.Add("SizeArea", manager.GetPrivateString("Game", "SizeArea"));
            settings.Add("ColorAreaBackground", manager.GetPrivateString("Game", "ColorAreaBackground"));
            settings.Add("ColorAreaText", manager.GetPrivateString("Game", "ColorAreaText"));
            settings.Add("ColorHighlighting", manager.GetPrivateString("Game", "ColorHighlighting"));
            settings.Add("ColorHighlightingWord", manager.GetPrivateString("Game", "ColorHighlightingWord"));
            settings.Add("ColorGuessedWord", manager.GetPrivateString("Game", "ColorGuessedWord"));
            settings.Add("ColorRandomGuessedWord", manager.GetPrivateString("Game", "ColorRandomGuessedWord"));

            return settings;

        }

        public static void ChangeFileSettings(string path, string[] setting)
        {
            // setting[0] - Поле настройки
            // setting[1] - Имя настройки
            // setting[2] - Значение настройки
            if (!File.Exists(path))
            {
                CreateFileSettings(path);
            }
            INIManager manager = new INIManager(path);
            manager.WritePrivateString(setting[0], setting[1], setting[2]);
        }

        public static void UpdateFilesDictionary(string path)
        {
            WebClient wc = new WebClient();

            for (int i = 2; i <= 10; i++)
            {
                wc.DownloadFile(url+$"{i}.txt", path + $"\\word_{i}.txt");
            }
        }
        
        public static string GetWordInDictionary(string path, int num, Random rnd)
        {
            if (!File.Exists(path+$"\\word_{num}.txt"))
            {
                CheckFilesDictionary(path);
            }
            string[] inputText = File.ReadAllLines(path+$"\\word_{num}.txt", Encoding.Default);
            return inputText[rnd.Next(inputText.GetUpperBound(0))];
        }
    }
}
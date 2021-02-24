using System;
using System.Collections.Generic;
using System.Linq;

namespace FillWords.Console
{
    public class Fillwords
    {
        private static string PATH_FILE_DICT = @"dictionary";
        private Random rnd = new Random();
        protected int ROW;
        protected int COL;
        
        enum Path { TOP, RIGHT, BOTTOM, LEFT };

        public string[,] CreateField(string[] words)
        {
            string[,] field = new string[ROW, COL];
            bool[] position = new bool[ROW * COL];
            int value;
            for (int i = 0; i < position.Length; i++)
            {
                value = GetRandomPosition(position, rnd);
                if (RunFuckingAlgorithm(value / field.GetLength(0), value % field.GetLength(1), field, words))
                    return field;
            }
            return field;
        }

        private bool RunFuckingAlgorithm(int row, int col, string[,] field, string[] words, int lvl = 0)
        {
            if (!Contains(field)) 
                return true;
            field[row, col] = GetLetter(words, lvl);
            bool[] enumerationsFoo = new bool[4]; 
            for (int i = 0, val; i < enumerationsFoo.Length; i++)
            {
                val = GetRandomPosition(enumerationsFoo, rnd);
                switch ((Path)val)
                {
                    case Path.TOP:
                        if (CheckArray(field, row - 1, col))
                            RunFuckingAlgorithm(row - 1, col, field, words, lvl + 1);
                        break;
                    case Path.RIGHT:
                        if (CheckArray(field, row, col + 1))
                            RunFuckingAlgorithm(row, col + 1, field, words, lvl + 1);
                        break;
                    case Path.BOTTOM:
                        if (CheckArray(field, row + 1, col))
                            RunFuckingAlgorithm(row + 1, col, field, words, lvl + 1);
                        break;
                    case Path.LEFT:
                        if (CheckArray(field, row, col - 1))
                            RunFuckingAlgorithm(row, col - 1, field, words, lvl + 1);
                        break;
                }
            }
            if (!Contains(field)) 
                return true;

            field[row, col] = null;
            return false;
        }

        static string GetLetter(string[] words, int number)
        {
            char letter = words.SelectMany(x => x.ToCharArray()).ToArray()[number];
            int counter = 0;
            int pos = 0;
            for(int i = 0; i < words.Length; i++)
            {
                counter += words[i].Length;
                if(counter >= number+1)
                {
                    pos = i + 1;
                    break;
                }
            }
            return letter + pos.ToString();
        }

        static bool CheckArray(string[,] arr, int row, int col)
        {
            if (row < 0 || row == arr.GetLength(0) ||
                col < 0 || col == arr.GetLength(1) ||
                arr[row, col] != null)
                return false;
            return true;
        }

        private static int GetRandomPosition(bool[] pos, Random rnd)
        {
            int value;
            while (true)
            {
                value = rnd.Next(pos.Length);
                if (pos[value] == false)
                {
                    pos[value] = true;
                    return value;
                }

            }
        }

        static bool Contains(string[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == null)
                        return true;
                }
            }
            return false;
        }

        public string[] GetWords()
        {
            int size = ROW * COL;
            List<int> numberWords = GetNumberLetters(size, rnd);
            string[] str = new string[numberWords.Count];
            string temp;

            for (int i = 0; i < str.Length; i++)
            {
                temp = WorkWithFiles.GetWordInDictionary(PATH_FILE_DICT ,numberWords[i], rnd).ToUpper();
                if (str.Contains(temp))
                {
                    i--;
                    continue;
                }
                str[i] = temp;
            }
            return str;
        }

        private static List<int> GetNumberLetters(int size, Random rnd)
        {
            List<int> numberWords = new List<int>();
            int value;

            while (size != 0)
            {
                if (size == 2)
                {
                    numberWords.Add(2);
                    size -= 2;
                    continue;
                }
                if (size <= 10)
                {
                    value = rnd.Next(2, size);
                    if (size - value == 1)
                    {
                        numberWords.Add(size);
                        size = 0;
                        continue;
                    } 
                    numberWords.Add(value);
                    size -= value;
                    continue;
                }
                value = rnd.Next(2, 10);
                numberWords.Add(value);
                size -= value;
            }
            return numberWords;
        }
    }
}

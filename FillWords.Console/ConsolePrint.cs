using System;

namespace FillWords.Console
{
    class ConsolePrint
    {
		public static void PrintCenter(string data, int intent, ConsoleColor color = ConsoleColor.White) // вывод 1 строки по центру
		{
			int left = 0;
			int top = intent;
			int center = System.Console.WindowWidth / 2;

			System.Console.ForegroundColor = color;
			left = center - (data.Length / 2);
			System.Console.SetCursorPosition(left, top);
			System.Console.WriteLine(data);
			top = System.Console.CursorTop;
		}

		public static void PrintCenter(string[] data, int intent, ConsoleColor color = ConsoleColor.White) // вывод массива по центру
        {
			int left = 0;
			int top = intent;
			int center = System.Console.WindowWidth / 2;

			System.Console.ForegroundColor = color;
			for (int i = 0; i < data.Length; i++)
			{
				left = center - (data[i].Length / 2);
				System.Console.SetCursorPosition(left, top);
				System.Console.WriteLine(data[i]);
				top = System.Console.CursorTop;
			}
		}

		public static void PrintMenuCenter(string[][] data, int intentTop, int intentCenter, int choice, ConsoleColor colorChoice = ConsoleColor.Red, ConsoleColor color = ConsoleColor.White) // вывод массива массивов по центру
		{
			int left = 0;
			int top = intentTop;
			int center = System.Console.WindowWidth / 2;

			for (int i = 0; i < data.Length; i++)
			{
				if (i == choice)
					System.Console.ForegroundColor = colorChoice;
				else
					System.Console.ForegroundColor = color;
				for (int j = 0; j < data[i].Length; j++)
				{
					left = center - (data[i][j].Length / 2);
					System.Console.SetCursorPosition(left, top);
					System.Console.WriteLine(data[i][j]);
					top = System.Console.CursorTop;
				}
				top += intentCenter;
			}
		}

		public static void PrintFieldCenter(string[,] field)
        {
			int left = 0;
			int top = 2;
			int center = System.Console.WindowWidth / 2;
			left = center - (field.GetLength(0) + field.GetLength(0) + 1 / 2);

			for (int i = -1; i <= field.GetLength(0); i++)
            {
				System.Console.SetCursorPosition(left, top);
				if (i == -1)
                {
					PrintBorderField(field.GetLength(0), 0);
					top += 1;
					continue;
				}
				if (i == field.GetLength(0))
				{
					PrintBorderField(field.GetLength(0), 2);
					continue;
				}
				for (int j = 0; j <= field.GetLength(1); j++)
                {
					System.Console.Write("║");
					if (j == field.GetLength(1))
						break;
					System.Console.Write(field[i,j][0]);
                }
				top += 1;
				System.Console.SetCursorPosition(left, top);
				if (i == field.GetLength(0) - 1)
					continue;
				PrintBorderField(field.GetLength(0), 1);
				top += 1;

			}
		}
		private static void PrintBorderField(int len, int choice)
        {
			char cornerLeft;
			char cornerRight;
			char delimiter;
			char continuer = '═';

			switch (choice)
            {
				case 0:
					cornerLeft = '╔';
					cornerRight = '╗';
					delimiter = '╦';
					break;
				case 1:
					cornerLeft = '╠';
					cornerRight = '╣';
					delimiter = '╬';
					break;
				case 2:
					cornerLeft = '╚';
					cornerRight = '╝';
					delimiter = '╩';
					break;
				default:
					cornerLeft = '?';
					cornerRight = '?';
					delimiter = '?';
					break;
			}

			for (int i = 0; i <= len; i++)
            {
				if (i == 0)
                {
					System.Console.Write(cornerLeft);
					continue;
                }
				if (i == len)
                {
					System.Console.Write(continuer);
					System.Console.Write(cornerRight);
					continue;
				}
				System.Console.Write(continuer);
				System.Console.Write(delimiter);
			}
        } 
	}
}

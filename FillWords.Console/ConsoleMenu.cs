using System;

namespace FillWords.Console
{
    class ConsoleMenu
    {
        private const int WIDTH_DISPLAY = 110;
        private const int LENGTH_DISPLAY = 33;
		private string[][] ItemMenu = {ConsoleTextMenu.NEWGAME, ConsoleTextMenu.RESUME, ConsoleTextMenu.RATING, ConsoleTextMenu.EXIT };

		public void SelectMenu()
        {
			System.Console.SetWindowSize(WIDTH_DISPLAY, LENGTH_DISPLAY);
			System.Console.CursorVisible = false;

			int position = 1;
			while (true)
            {
				ShowMenu(position);
				switch (System.Console.ReadKey(true).Key)
                {
					case ConsoleKey.UpArrow:
						position = CheckUpButton(position);
						break;
					case ConsoleKey.DownArrow:
						position =  CheckDownButton(position);
						break;
					case ConsoleKey.W:
						position = CheckUpButton(position);
						break;
					case ConsoleKey.S:
						position = CheckDownButton(position);
						break;
					case ConsoleKey.Enter:
						ActionMenuItem(position);
						break;
					default:
						break;
				}
            }
        }

        public void ShowMenu(int choice)
        {
            System.Console.Clear();

			ConsolePrint.PrintCenter(ConsoleTextMenu.GAME, 2, ConsoleColor.Green);

			int cursorTop = System.Console.CursorTop;
			ConsolePrint.PrintMenuCenter(ItemMenu, cursorTop+5, 2, choice-1);
        }

		private int CheckUpButton(int position)
        {
			System.Console.Beep();
			if (position == 1)
			{
				return position = 4;
			}
			return --position;
        }

		private int CheckDownButton(int position)
		{
			System.Console.Beep();
			if (position == 4)
			{
				return position = 1;
			}
			return ++position;
		}

		private void ActionMenuItem(int position)
        {
            switch (position)
            {
				case 1:
					ConsoleNewGame NewGame = new ConsoleNewGame();
					NewGame.Main();
					break;
				case 2:
					PlugMenuItem("\"Продолжить\"");
					break;
				case 3:
					PlugMenuItem("\"Рейтинг\"");
					break;
				case 4:
					Environment.Exit(0);
					break;
            }
			System.Console.ReadKey();
        }

		private void PlugMenuItem(string item)
        {
			System.Console.Clear();
			string[] output = { $"Тут однажды будет {item}", "Для выхода в главное меню нажмите любую клавишу..." };

			int top = (System.Console.WindowHeight / 2) - (output.Length / 2) - 1;
			ConsolePrint.PrintCenter(output, top, ConsoleColor.Green);
		}
	}
}

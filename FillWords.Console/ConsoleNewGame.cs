using System;

namespace FillWords.Console
{
    class ConsoleNewGame
    {
        private static string[] TEXT_NEW_GAME = ConsoleTextMenu.NEWGAME;
        private const string TEXT_PLAYER_WELCOME = "Здравствуйте, уважаемый игрок. Введите свое имя ниже:";
        private string playerName;

        public void Main()
        {
            System.Console.Clear();

            ConsolePrint.PrintCenter(TEXT_NEW_GAME, 2, ConsoleColor.Yellow);

            int cursorTop = System.Console.CursorTop;
            ConsolePrint.PrintCenter(TEXT_PLAYER_WELCOME, cursorTop+5);

            int center = System.Console.WindowWidth / 2;
            System.Console.SetCursorPosition(center, cursorTop+6);
            playerName = System.Console.ReadLine();
            StartGame();
        }

        private void StartGame(int row = 4, int col = 4)
        {
            ConsoleUIGame game = new ConsoleUIGame(row, col);
            string[] arr = game.GetWords();
            string[,] field = game.CreateField(arr);
            System.Console.Clear();
            ConsolePrint.PrintFieldCenter(field);
        }
    }
}

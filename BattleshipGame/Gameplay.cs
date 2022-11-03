using BattleShipLibrary;
using BattleShipLibrary.Models;
using System;


namespace BattleshipGame
{
    public static class Gameplay
    {
        public static void DisplayShotGrid(PlayerModel activePlayer)
        {
            Console.WriteLine(" \t1\t2\t3\t4\t5");
            Console.WriteLine();
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;
            Console.Write($"{currentRow}\t");

            foreach (var spotGrid in activePlayer.ShotGrid)
            {
                if (spotGrid.SpotLetter != currentRow)
                {
                    currentRow = spotGrid.SpotLetter;
                    Console.WriteLine();
                    Console.Write($"{currentRow}\t");

                }

                if (spotGrid.Status == GridSpotStatus.Empty)
                {
                    Console.Write("*\t");
                }
                else if (spotGrid.Status == GridSpotStatus.Hit)
                {
                    Console.Write("x\t");
                }
                else if (spotGrid.Status == GridSpotStatus.Miss)
                {
                    Console.Write("o\t");
                }
                else
                {
                    Console.Write("?\t");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public static void RecordPlayerShot(PlayerModel activePlayer, PlayerModel opponent)
        {
            bool isValidShot = false;
            string row = string.Empty;
            int column = 0;

            Console.WriteLine();

            do
            {
                string shot = AskForShot(activePlayer);
                isValidShot = false;

                try
                {
                    (row, column) = GameLogic.SplitSpotIntoRowAndColumn(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row.ToUpper(), column);

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    isValidShot = false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("That is not a valid shot! Please try again!\n");
                }
            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            string result = GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static string AskForShot(PlayerModel player)
        {
            Console.Write($"{player.UsersName}, please enter your shot selection: ");
            string output = Console.ReadLine();

            return output;
        }
    }
}

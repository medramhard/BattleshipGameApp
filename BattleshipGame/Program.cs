using BattleShipLibrary;
using BattleShipLibrary.Models;
using System;



namespace BattleshipGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetUp.StartMessage();
            PlayerModel activePlayer = SetUp.CreatePlayer("Player1");
            PlayerModel opponent = SetUp.CreatePlayer("Player2");
            PlayerModel winner = null;

            do
            {
                Gameplay.DisplayShotGrid(activePlayer);
                Gameplay.RecordPlayerShot(activePlayer, opponent);

                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                if (doesGameContinue == false)
                {
                    winner = activePlayer;
                }
                else
                {
                    // using a tuple for swapping values of variables (modern approach)
                    (activePlayer, opponent) = (opponent, activePlayer);
                }

            } while (winner == null);

            SetUp.SummarizeGame(winner);

            Console.ReadLine();
        }
    }
}

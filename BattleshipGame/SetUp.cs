using BattleShipLibrary;
using BattleShipLibrary.Models;
using System;


namespace BattleshipGame
{
    public static class SetUp
    {
        public static void StartMessage()
        {
            // greet the user
            Console.WriteLine("Hello! Welcome to the Battleship Game");
            Console.WriteLine("created by Enda Dramheart");
            Console.WriteLine();

            // explain rules
            Console.WriteLine("Here are some rules for you to go:");
            Console.WriteLine("On a 25-spot grid each player must place 5 one-spot ships");
            Console.WriteLine("After that players will fire on each other in turns");
            Console.WriteLine("The first player to sink all the five ships of the opponnent is the winner");
            Console.WriteLine("Have a nice game!;)");
            Console.WriteLine();
        }

        public static PlayerModel CreatePlayer(string playerTitle)
        {
            Console.WriteLine($"Setting up information for {playerTitle}");
            PlayerModel output = new PlayerModel();
            // ask user for their name
            output.UsersName = GetUsersName();

            // load up the shot grid
            GameLogic.InitializeShotGrid(output);

            // ask user for their ships' placement
            PlaceShips(output);

            // clear the console
            Console.Clear();

            return output;
        }

        private static string GetUsersName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void PlaceShips(PlayerModel model)
        {
            do
            {
                Console.Write($"Where do you want to place your {model.ShipsLocations.Count + 1} ship: ");
                string location = Console.ReadLine();
                bool isValidLocation = false;

                try
                {
                    isValidLocation = GameLogic.PlaceShip(model, location);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValidLocation = false;
                }

                if (isValidLocation == false)
                {
                    Console.WriteLine("That is not a valid location! Please, try again.");
                }

            } while (model.ShipsLocations.Count < 5);

            Console.WriteLine("Great! All set!");
            Console.ReadLine();
        }

        public static void SummarizeGame(PlayerModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
            Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots");
        }

    }
}

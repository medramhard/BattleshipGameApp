using BattleShipLibrary.Models;
using System;
using System.Collections.Generic;


namespace BattleShipLibrary
{
    public static class GameLogic
    {
        public static void InitializeShotGrid(PlayerModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddShotGridSpot(model, letter, number);
                }
            }
        }

        private static void AddShotGridSpot(PlayerModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static bool PlayerStillActive(PlayerModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipsLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }

            return isActive;
        }

        public static bool PlaceShip(PlayerModel model, string location)
        {
            bool output = false;

            (string row, int column) = SplitSpotIntoRowAndColumn(location);
            bool isValidSpotLocation = ValidateSpotLocation(model, row.ToUpper(), column);
            bool isSpotOpen = ValidateSpotOpen(model, row.ToUpper(), column);

            if (isValidSpotLocation && isSpotOpen)
            {
                model.ShipsLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });

                output = true;
            }

            return output;
        }

        private static bool ValidateSpotOpen(PlayerModel model, string row, int column)
        {
            bool isSpotOpen = true;

            foreach (var ship in model.ShipsLocations)
            {
                if (ship.SpotLetter == row && ship.SpotNumber == column)
                {
                    isSpotOpen = false;
                }
            }

            return isSpotOpen;
        }

        private static bool ValidateSpotLocation(PlayerModel model, string row, int column)
        {
            bool isValidSpotLocation = false;

            foreach (var spot in model.ShotGrid)
            {
                if (spot.SpotLetter == row && spot.SpotNumber == column)
                {
                    isValidSpotLocation = true;
                }
            }

            return isValidSpotLocation;
        }

        public static int GetShotCount(PlayerModel player)
        {
            int output = player.ShotCount;

            return output;
        }

        public static (string row, int column) SplitSpotIntoRowAndColumn(string spot)
        {
            if (spot.Length < 2 || string.IsNullOrEmpty(spot))
            {
                throw new ArgumentException("Fail to cast invalid row value", "row");
            }
            string row = spot.Substring(0, 1);

            bool isValidNumber = int.TryParse(spot.Substring(1, spot.Length - 1), out int column);
            if (isValidNumber == false)
            {
                throw new ArgumentException("Fail to cast invalid column value", "column");
            }

            return (row, column);
        }

        public static bool ValidateShot(PlayerModel player, string row, int column)
        {
            bool output = false;

            bool isValidSpotLocation = ValidateSpotLocation(player, row, column);
            bool isEmpty = false;
            foreach (var shot in player.ShotGrid)
            {
                if (shot.SpotLetter == row && shot.SpotNumber == column && shot.Status == GridSpotStatus.Empty)
                {
                    isEmpty = true;
                }
            }

            if (isValidSpotLocation && isEmpty)
            {
                output = true;
            }

            return output;
        }

        public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
        {
            bool isAHit = false;

            foreach (var ship in opponent.ShipsLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    ship.Status = GridSpotStatus.Sunk;
                    isAHit = true;
                }
            }

            return isAHit;
        }

        public static string MarkShotResult(PlayerModel player, string row, int column, bool isAHit)
        {
            string output = string.Empty;

            player.ShotCount++;

            foreach (var shot in player.ShotGrid)
            {
                if (shot.SpotLetter == row.ToUpper() && shot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        shot.Status = GridSpotStatus.Hit;
                        output = "It is a hit!";
                    }
                    else
                    {
                        shot.Status = GridSpotStatus.Miss;
                        output = "It is a miss.";
                    }
                }
            }

            return output;
        }
    }
}

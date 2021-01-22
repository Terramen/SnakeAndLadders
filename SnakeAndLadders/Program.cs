using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadders
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            
            SnakesLadders snakesLadders = new SnakesLadders();
            Random random = new Random();
            snakesLadders.InitCells(100);
            Menu();

            void Menu()
            {
                Console.WriteLine("Game menu Snakes and Ladders");
                Console.WriteLine("1 - Start Game");
                Console.WriteLine("2 - Exit");
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        Console.WriteLine("Выберите кол-во игроков: ");
                        int count = Convert.ToInt32(Console.ReadLine());
                        snakesLadders.InitPlayers(count);
                        Console.WriteLine("Player 1 turn. ");
                        Gameplay();
                        break;
                    case 2: return;
                }
            }

            void Gameplay()
            {
                Console.WriteLine("1 - Roll the dice");
                Console.WriteLine("2 - Leave the game");
                int key2 = Convert.ToInt32(Console.ReadLine());
                switch (key2)
                {
                    case 1: snakesLadders.Play(random.Next(1,6), random.Next(1,6));
                        Gameplay();
                        break;
                    case 2: Menu();
                        break;
                }
            }
            /*snakesLadders.play(random.Next(1,6), random.Next(1,6));
            snakesLadders.play(random.Next(1,6), random.Next(1,6));
            snakesLadders.play(random.Next(1,6), random.Next(1,6));*/
            /*snakesLadders.play(1,1);
            snakesLadders.play(1,5);
            snakesLadders.play(6,2);
            snakesLadders.play(1,1);*/
            // GameCell gameCell1 = new GameCell(1);
        }
    }
    
    class SnakesLadders
    {
       // private int playerOneScore = 0;
       // private int playerTwoScore = 0;

        private int currentPlayer;
        
        
        List<GameCell> gameCells = new List<GameCell>();
        List<Player> playerList = new List<Player>();

        public int CurrentPlayer
        {
            get => currentPlayer;
            set => currentPlayer = value;
        }

        /*public int PlayerOneScore
        {
            get => playerOneScore;
            set => playerOneScore = value;
        }*/

        private int[] snakesStart = {16, 49, 46, 62, 64, 74, 89, 92, 95, 99}; // 
        private int[] snakesEnd = {6, 11, 25, 19, 60, 53, 68, 88, 75, 80};    // 2 massive numbers (start and end) connect game board snake
        private int[] laddersStart = {2, 7, 8, 15, 21, 28, 51, 71, 78, 87};   //
        private int[] laddersEnd = {38, 14, 31, 26, 42, 84, 67, 91, 98, 94};  // 2 massive numbers (start and end) connect game board ladder

        /*private int[,] allSnakesAndLadders = {{ 16, 49, 46, 62, 64, 74, 89, 92, 95, 99 },
                                                { 6, 11, 25, 19, 60, 53, 68, 88, 75, 80 },
                                                {2, 7, 8, 15, 21, 28, 51, 71, 78, 87 },
                                                { 38, 14, 31, 26, 42, 84, 67, 91, 98, 94}};*/

        public void InitCells(int cells)
        {
            for (int i = 0; i <= cells; i++)
            {
                gameCells.Add(new GameCell(i, 1));
            }

            for (int i = 0; i < snakesStart.Length; i++)
            {
                gameCells[snakesStart[i]].Points = snakesEnd[i] - snakesStart[i];
               // Console.WriteLine(gameCells[snakesStart[i]].Points);
            }
            
            for (int i = 0; i < laddersStart.Length; i++)
            {
                gameCells[laddersStart[i]].Points = laddersEnd[i] - laddersStart[i];
               // Console.WriteLine(gameCells[laddersStart[i]].Points);
            }
        }

        public void InitPlayers(int playerCount)
        {
            for (int i = 1; i <= playerCount; i++)
            {
                playerList.Add(new Player(i, 0));
            }
        }
        
        /*public string play (int die1, int die2)
        {
            Console.WriteLine($"Кубик 1 и Кубик 2: {die1}, {die2}");
            Console.WriteLine($"Position: {gameCells[playerOneScore].CellPostition}");
            for (int i = 0; i < die1 + die2; i++)
            {
                //Console.WriteLine(gameCells[2].Points);
                playerOneScore++;
                if (i.Equals(die1 + die2 - 1) && gameCells[playerOneScore].Points != 1) playerOneScore = playerOneScore + gameCells[playerOneScore].Points;
            }
            Console.WriteLine($"Player 1 is on square {playerOneScore}");
            return $"Player 1 is on square {playerOneScore}";
        }*/
        
        public string Play (int die1, int die2)
        {

            Console.WriteLine($"Кубик 1 и Кубик 2: {die1}, {die2}");
            
            //Console.WriteLine($"Position: {gameCells[playerList[currentPlayer].CurrentPoints].CellPostition}");
            for (int i = 0; i < die1 + die2; i++)
            {
                //Console.WriteLine(gameCells[2].Points);
                playerList[currentPlayer].CurrentPoints++;
                if (i.Equals(die1 + die2 - 1) && gameCells[playerList[currentPlayer].CurrentPoints].Points != 1)
                    playerList[currentPlayer].CurrentPoints = playerList[currentPlayer].CurrentPoints + gameCells[playerList[currentPlayer].CurrentPoints].Points;
            }
            Console.WriteLine($"Player {playerList[currentPlayer].Number} is on square {playerList[currentPlayer].CurrentPoints} \n ------");
            string answer =
                $"Player {playerList[currentPlayer].Number} is on square {playerList[currentPlayer].CurrentPoints}";
            if (currentPlayer == playerList.Count - 1) currentPlayer = 0;
            else if(die1 != die2) currentPlayer++;
            //TODO чей ход
            Console.WriteLine($"Player {currentPlayer + 1} turn.");
            if (playerList[currentPlayer].CurrentPoints == 100)
            {
                return $"Player {currentPlayer} Wins!";
            }
            else
            {
                return answer;
            }
        }
    }

    class GameCell
    {
        private int cellPostition;
        private int points;

        public int CellPostition
        {
            get => cellPostition;
            set => cellPostition = value;
        }
        public int Points
        {
            get => points;
            set => points = value;
        }
        public GameCell(int cellPostition, int points)
        {
            this.points = points;
            this.cellPostition = cellPostition;
        }
    }

    class Player
    {
        private int number;
        private int currentPoints;

        public Player(int number, int currentPoints)
        {
            this.number = number;
            this.currentPoints = currentPoints;
        }

        public int Number
        {
            get => number;
            set => number = value;
        }

        public int CurrentPoints
        {
            get => currentPoints;
            set => currentPoints = value;
        }
    }
}
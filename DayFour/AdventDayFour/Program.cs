using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventDayFour
{
    public class BingoBoard
    {
        public List<List<KeyVal<string, bool>>> Board { get; set; }
        public BingoBoard(List<List<KeyVal<string, bool>>> board)
        {
            Board = board;
        }
        public BingoBoard() { }

    }
    public class KeyVal<Key, Val>
    {
        public Key Id { get; set; }
        public Val Text { get; set; }

        public KeyVal() { }

        public KeyVal(Key key, Val val)
        {
            this.Id = key;
            this.Text = val;
        }
    }

    class Program
    {
        static BingoBoard BoardBuilder(List<string> boardList)
        {
            var fullBoard = new List<List<KeyVal<string, bool>>>();
            foreach (var line in boardList)
            {
                var boardRow = new List<KeyVal<string, bool>>();

                string[] subStr = line.Split().Where(S => !string.IsNullOrEmpty(S)).ToArray();
                foreach (var sub in subStr)
                {
                    boardRow.Add(new KeyVal<string, bool>(sub, false));
                }
                fullBoard.Add(boardRow);
            }

            return new BingoBoard(fullBoard);
        }

        static List<BingoBoard> MarkBoard(string numberCalled, List<BingoBoard> boardList)
        {
            foreach (BingoBoard board in boardList)
            {
                foreach (var row in board.Board)
                {
                    for (int ndx = 0; ndx < row.Count; ndx++)
                    {
                        if (row[ndx].Id == numberCalled)
                        {
                            row[ndx].Text = true;
                        }
                    }
                }
            }
            return boardList;
        }

        static bool CheckForWinner(BingoBoard myBoard)
        {
            if (myBoard.Board[0][0].Text && myBoard.Board[0][1].Text && myBoard.Board[0][2].Text && myBoard.Board[0][3].Text && myBoard.Board[0][4].Text ||
                myBoard.Board[1][0].Text && myBoard.Board[1][1].Text && myBoard.Board[1][2].Text && myBoard.Board[1][3].Text && myBoard.Board[1][4].Text ||
                myBoard.Board[2][0].Text && myBoard.Board[2][1].Text && myBoard.Board[2][2].Text && myBoard.Board[2][3].Text && myBoard.Board[2][4].Text ||
                myBoard.Board[3][0].Text && myBoard.Board[3][1].Text && myBoard.Board[3][2].Text && myBoard.Board[3][3].Text && myBoard.Board[3][4].Text ||
                myBoard.Board[4][0].Text && myBoard.Board[4][1].Text && myBoard.Board[4][2].Text && myBoard.Board[4][3].Text && myBoard.Board[4][4].Text ||
                myBoard.Board[0][0].Text && myBoard.Board[1][0].Text && myBoard.Board[2][0].Text && myBoard.Board[3][0].Text && myBoard.Board[4][0].Text ||
                myBoard.Board[0][1].Text && myBoard.Board[1][1].Text && myBoard.Board[2][1].Text && myBoard.Board[3][1].Text && myBoard.Board[4][1].Text ||
                myBoard.Board[0][2].Text && myBoard.Board[1][2].Text && myBoard.Board[2][2].Text && myBoard.Board[3][2].Text && myBoard.Board[4][2].Text ||
                myBoard.Board[0][3].Text && myBoard.Board[1][3].Text && myBoard.Board[2][3].Text && myBoard.Board[3][3].Text && myBoard.Board[4][3].Text ||
                myBoard.Board[0][4].Text && myBoard.Board[1][4].Text && myBoard.Board[2][4].Text && myBoard.Board[3][4].Text && myBoard.Board[4][4].Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int DetermineWinningScore(string numberCalled, BingoBoard winningBoard)
        {
            int score = 0;
            foreach (var row in winningBoard.Board)
            {
                foreach (var element in row)
                {
                    if (!element.Text)
                    {
                        score += int.Parse(element.Id);
                    }
                }
            }
            return score * int.Parse(numberCalled);
        }

        static int PartOne(string[] file)
        {
            var winningScore = 0;
            var input = new List<string>(file);
            var bingoCalls = input[0].Split(',');

            var builderList = new List<string>();

            var playingBoards = new List<BingoBoard>();

            for (int ndx = 2; ndx < input.Count; ndx++)
            {
                if (input[ndx] == String.Empty)
                {
                    playingBoards.Add(BoardBuilder(builderList));
                    builderList.Clear();
                }
                else
                {
                    builderList.Add(input[ndx]);
                }
            }

            var winner = false;
            while (!winner)
            {
                foreach (var call in bingoCalls)
                {
                    playingBoards = MarkBoard(call, playingBoards);
                    foreach (var board in playingBoards)
                    {
                        if (CheckForWinner(board))
                        {
                            Console.WriteLine("We have a winner! Winning Call: {0}",call);
                            winner = true;
                            winningScore = DetermineWinningScore(call, board);
                            break;
                        }
                        else
                        {
                            //Console.WriteLine("No Winners Found yet");
                        }
                    }
                    if (winner)
                    {
                        break;
                    }
                }
            }


            return winningScore;
        }

        static int PartTwo(string[] file)
        {
            var notQuiteWinningScore = 0;

            var winningScore = 0;
            var winningCall = "";
            var input = new List<string>(file);
            var bingoCalls = input[0].Split(',');

            var builderList = new List<string>();

            var playingBoards = new List<BingoBoard>();

            for (int ndx = 2; ndx < input.Count; ndx++)
            {
                if (input[ndx] == String.Empty)
                {
                    playingBoards.Add(BoardBuilder(builderList));
                    builderList.Clear();
                }
                else
                {
                    builderList.Add(input[ndx]);
                }
            }

            var removeList = new List<BingoBoard>();
            BingoBoard winningBoard = new BingoBoard();

            int callCounter = 0;
            foreach (var call in bingoCalls)
            {
                callCounter++;
                playingBoards = MarkBoard(call, playingBoards);
                foreach (var board in playingBoards)
                {
                    if (CheckForWinner(board))
                    {
                        Console.WriteLine("Winning Call: {0}", call);
                        removeList.Add(board);
                        winningBoard = board;
                        winningCall = call;
                    }
                }

                playingBoards.RemoveAll(x => removeList.Contains(x));
            }

            winningScore = DetermineWinningScore(winningCall, winningBoard);

            return notQuiteWinningScore;
        }


        static void Main(string[] args)
        {
            string path = @"C:\Projects\AdventCode2021\DayFour\input.txt";
            var file = File.ReadAllLines(path);

            var resultOne = PartOne(file);
            var resultTwo = PartTwo(file);

            Console.WriteLine("Winning Score: {0}", resultOne);

            Console.ReadLine();
        }
    }
}

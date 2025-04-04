using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Connect_Four_Final_Project_OOP_2025

{

    abstract class Player
    {
        public char Disc { get; set; }
        public string Name { get; set; }
        static List<ConnectFour> pieceLocations;

        public Player(char disc, string name)
        {
            Disc = disc;
            Name = name;
        }

        public abstract int GetMove();


    }

    class HumanPlayer : Player
    {
        public override int GetMove()
        {

            int column;
        }
    }

    class ComputerPlayer : Player
    {
        private Random random;//How the computer will place pieces by random

        public override int GetMove()
        {
            int column;
        }
    }


    class ConnectFour
    {
        public bool gameOver = true;

        public ConnectFour()
        {


        }

        public IsItConnectFour()
        {

        }

        public PlayATurn()
        {

            while (!gameOver)
            {
                .GetMove
            }
        }


    }

    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
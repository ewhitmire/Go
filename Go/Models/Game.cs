﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    class Game
    {
        public Board GameBoard { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public List<Move> History { get; private set; }
        public float Komi { get; private set; }
        public int WhiteCaptured { get; private set; }
        public int BlackCaptured { get; private set; }

        private Player WhoseTurn;
        private GameController Controller;

        public Game(int boardSize, GoGrid grid)
        {
            this.GameBoard = new Board(boardSize);
            this.Player1 = new Player(Color.BLACK);
            this.Player2 = new Player(Color.BLACK);
            this.History = new List<Move>();
            this.WhoseTurn = Player1;
            //this.Controller = new GameController(grid);
        }

        internal void MakeMove(int x, int y)
        {
            GameBoard.PlacePiece(WhoseTurn,x,y);

            // Switch turns
            if(WhoseTurn == Player1)
            {
                WhoseTurn = Player2;
            }
            else
            {
                WhoseTurn = Player1;
            }
        }


    }

}

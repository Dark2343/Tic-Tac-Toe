namespace XO 
{
    internal abstract class Board {
        private readonly int _n;
        protected readonly char[,] Grid;

        protected Board(int n)
        {
            _n = n;
            Grid = new char[_n, _n];
        }

        public abstract bool UpdateBoard(int x, int y, char symbol);
        public abstract bool IsWinner(char symbol);
        public abstract bool IsDraw();
        public void ResetBoard()
        {
            for (var i = 0; i < _n; i++) 
            {
                for (var j = 0; j < _n; j++) 
                {
                    this.Grid[i,j] = ' ';
                }
            }
        }

        public void DisplayBoard() 
        {
            for (var i = 0; i < _n; i++) 
            {
                for (var j = 0; j < _n; j++) 
                {
                    Console.Write("| " + Grid[i, j] + " |");
                }
                Console.WriteLine();
            }
        }
    }

    internal class Xo : Board
    {
        public Xo() : base(3) {}
        public override bool UpdateBoard(int x, int y, char symbol)
        {
            if (x - 1 < 3 && y - 1 < 3 && this.Grid[x,y] != 'X' && this.Grid[x,y] != 'O')
            {
                this.Grid[x,y] = symbol;
                return true;
            }
            else {return false;}
        }
        public override bool IsWinner(char symbol)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if((this.Grid[i,0] == symbol && this.Grid[i,1] == symbol && this.Grid[i,2] == symbol) ||
                    (this.Grid[0,j] == symbol && this.Grid[1,j] == symbol && this.Grid[2,j] == symbol) ||
                    (this.Grid[0,0] == symbol && this.Grid[1,1] == symbol && this.Grid[2,2] == symbol) ||
                    (this.Grid[0,2] == symbol && this.Grid[1,1] == symbol && this.Grid[2,0] == symbol)) 
                    {  
                        return true;
                    }
                }
            }
            return false;
        }
        public override bool IsDraw()
        {
            if(this.IsWinner('X') == false && this.IsWinner('O') == false){
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (this.Grid[i,j] != 'X' && this.Grid[i,j] != 'O')
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else {return false;}
        }
    }

    class Player
    {
        public string GetName { get; }
        public char GetSymbol { get; }

        public Player(string name, char symbol)
        {
            GetName = name;
            GetSymbol = symbol;
        }
    }

    class Game
    {
        private int _turn;
        private readonly Xo _board;
        private readonly Player[] _players;
        public Game(Xo board, Player[] p)
        {
            _board = board;
            _players = p;
        }
        public void PlayGame()
        {
            _board.ResetBoard();
            int x = 0, y = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_players[0].GetName + ": " + _players[0].GetSymbol);
                Console.WriteLine(_players[1].GetName + ": " + _players[1].GetSymbol);
                _board.DisplayBoard();

                Console.WriteLine(_players[_turn].GetName + ", please choose where you want to play:");

                var input = Console.ReadLine();      // This is so the user can enter the 2 inputs on the same line,
                if (input != null)
                {
                    var data = input.Split(' ');            // We should prob also make a case for if he enters 1 input then presses enter, aka defensive programming
                    x = Convert.ToInt32(data[0]);
                    y = Convert.ToInt32(data[1]);
                }

                if(_board.UpdateBoard(x, y, _players[_turn].GetSymbol))
                {
                    if(_board.IsWinner(_players[_turn].GetSymbol) == false)
                    {
                        if(_board.IsDraw() == false)
                        {
                            switch(_turn)
                            {
                                case 0:
                                    _turn = 1;
                                    break;
                                case 1:
                                    _turn = 0;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("The game ends with a tie.");
                            _board.DisplayBoard();
                            break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Congrats, " + _players[_turn].GetName + " won.");
                        _board.DisplayBoard();
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry the index you chose is invalid,");
                    Console.WriteLine("Press any button to try again.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
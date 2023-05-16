namespace XO
{
    public abstract class Launcher
    {
        private static void Main()
        {
            var board = new Xo();
            var players = new Player[2];
            char symbol;

            Console.WriteLine("Welcome to XO");
            Console.WriteLine("To start, Player 1 please write your name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Now, please choose your symbol: (X) or (O)");
            while (true)
            {
                symbol = char.ToUpper(Convert.ToChar(Console.ReadLine()!)); 
                if (symbol != 'X' && symbol != 'O')
                {
                    Console.WriteLine("Please choose either (X) or (O)");
                }
                else {break;}
            }
            players[0] = new Player(name, symbol);
            
            Console.WriteLine("Player 2 please write your name: ");
            name = Console.ReadLine();
            
            symbol = symbol == 'X' ? 'O' : 'X';
            players[1] = new Player(name, symbol);

            var launcher = new Game(board, players);
            
            while (true)
            {
                launcher.PlayGame();
                Console.WriteLine("Would you like to play again ? (Y/N)");
                while (true)
                {
                    symbol = char.ToUpper(Convert.ToChar(Console.ReadLine()!)); 
                    if (symbol is not 'Y' and not 'N')
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    else {break;}
                }

                if (symbol == 'Y') continue;
                Console.WriteLine("Thanks for playing XO....");
                break;
            }
        }
    }
}
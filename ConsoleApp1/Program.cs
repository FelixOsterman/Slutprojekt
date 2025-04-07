// Starta ett spelet.
//
// Sätt parametrar.
//
// skapa mapen för spelet.
//
// Läss keys för rörelse av spelare.
//
// Gör så att spelarna kan inte gå genom andra spelare eller vägar.
//
// Gör så att poäng kan tas upp.
//
// Gör så att spelet lopas så efter spelaren har rört sig rörelsen skrivs ut.
//
// Gör så att spelet slutar efter en av spelarna har tagit upp mer en hälften av poängen.

var game = new Game();
game.CreateMap();
game.IsRunning();
Console.ReadKey();

public class Game
{
    public void IsRunning()
    {
        while (running)
        {
            Render();
            PlayerMovment();
        }
    }

    public bool running = true;

    char[,] map;
    int width = 80;
    int height = 40;

    const char wall = '#';
    const char blank = ' ';
    const char pointMark = '.';

    List<Point> points = new List<Point>();
    public int numberOfPoints = 10;
    public int valueOfAllPoints;

    Player player1;
    Player player2;
    const char player1Mark = 'X';
    const char player2Mark = 'O';

    public int steg1;
    public int steg2;

    public void CreateMap()
    {
        player1 = new Player("Robert", 5, 20);
        player2 = new Player("Alexander", width - 5, 20);

        map = new char[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == 0 || y == height || x == 0 || x == width)
                {
                    map[x, y] = wall;
                }
                else
                {
                    map[x, y] = blank;
                }
            }
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points.Add(new Point());
            valueOfAllPoints += points[i].value;

            while (true)
            {
                points[i].X = Random.Shared.Next(1, width - 1);
                points[i].Y = Random.Shared.Next(1, height - 1);
                if (map[points[i].X, points[i].Y] != wall && map[points[i].X, points[i].Y] != map[player1.X, player1.Y] && map[points[i].X, points[i].Y] != map[player2.X, player2.Y])
                {
                    break;
                }
            }

            map[points[i].X, points[i].Y] = pointMark;
        }

        Console.Clear();
    }

    public void Render()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Player: " + player1.name);
        Console.SetCursorPosition(0, 1);
        Console.WriteLine("Steps: " + steg1);
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("Points: " + player1.Score + "/" + valueOfAllPoints);
        Console.SetCursorPosition(20, 0);
        Console.WriteLine("Player: " + player2.name);
        Console.SetCursorPosition(20, 1);
        Console.WriteLine("Steps: " + steg2);
        Console.SetCursorPosition(20, 2);
        Console.WriteLine("Points: " + player2.Score + "/" + valueOfAllPoints);

        map[player1.X, player1.Y] = player1Mark;
        map[player2.X, player2.X] = player2Mark;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                WriteOut(x, y, map[x, y]);
            }
        }

        map[player1.X, player1.Y] = blank;
        map[player2.X, player2.X] = blank;

        if (player1.Score > valueOfAllPoints / 2)
        {
            Console.WriteLine(player1.name + " win!");
            running = false;
        }
        if (player2.Score > valueOfAllPoints / 2)
        {
            Console.WriteLine(player2.name + " win!");
            running = false;
        }
        if (player1.Score == valueOfAllPoints / 2 && player2.Score == valueOfAllPoints / 2)
        {
            Console.WriteLine("Draw!");
            running = false;
        }
    }

    public void WriteOut(int x, int y, char map)
    {
        switch (map)
        {
            case wall:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case pointMark:
                Console.BackgroundColor = ConsoleColor.Yellow;
                break;
            case player1Mark:
                Console.BackgroundColor = ConsoleColor.Blue;
                break;
            case player2Mark:
                Console.BackgroundColor = ConsoleColor.Red;
                break;
            default:
                break;
        }
        Console.SetCursorPosition(x, y + 3);
        Console.Write(map);
    }

    public void PlayerMovment()
    {
        ConsoleKeyInfo movment = Console.ReadKey(true);
        switch (movment.Key)
        {
            case ConsoleKey.W:
                if (IsWallSlashIsPlayer(player1.X, player1.Y - 1) == false)
                {
                    player1.Y--;
                    steg1++;
                }
                break;
            case ConsoleKey.A:
                if (IsWallSlashIsPlayer(player1.X - 1, player1.Y) == false)
                {
                    player1.X--;
                    steg1++;
                }
                break;
            case ConsoleKey.D:
                if (IsWallSlashIsPlayer(player1.X + 1, player1.Y) == false)
                {
                    player1.X++;
                    steg1++;
                }
                break;
            case ConsoleKey.S:
                if (IsWallSlashIsPlayer(player1.X, player1.Y + 1) == false)
                {
                    player1.Y++;
                    steg1++;
                }
                break;
            case ConsoleKey.UpArrow:
                if (IsWallSlashIsPlayer(player2.X, player2.Y - 1) == false)
                {
                    player2.Y--;
                    steg1++;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (IsWallSlashIsPlayer(player2.X - 1, player2.Y) == false)
                {
                    player2.X--;
                    steg1++;
                }
                break;
            case ConsoleKey.RightArrow:
                if (IsWallSlashIsPlayer(player2.X + 1, player2.Y) == false)
                {
                    player2.X++;
                    steg1++;
                }
                break;
            case ConsoleKey.DownArrow:
                if (IsWallSlashIsPlayer(player2.X, player2.Y + 1) == false)
                {
                    player2.Y++;
                    steg1++;
                }
                break;
        }

        if (map[player1.X, player1.Y] == pointMark)
        {
            Point point = PickPoint(player1.X, player1.Y);
            player1.Points.Add(point);
        }
        else if (map[player2.X, player2.Y] == pointMark)
        {
            Point point = PickPoint(player2.X, player2.Y);
            player2.Points.Add(point);
        }


    }

    public bool IsWallSlashIsPlayer(int x, int y)
    {
        switch (map[x, y])
        {
            case '#':
            case 'X':
            case 'O':
                return true;
            default:
                return false;
        }
    }

    public Point PickPoint(int x, int y)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (x == points[i].X && y == points[i].Y)
            {
                Point stone = points[i];
                points.RemoveAt(i);
                return stone;
            }
        }
        return null;
    }
}

public class Point
{
    public int value = Random.Shared.Next(1, 11);
    public int X;
    public int Y;
}

public class Player
{
    public Player(string name, int x, int y)
    {
        this.name = name;
        int X = x;
        int Y = y;

    }
    public string name;
    public int X;
    public int Y;

    public List<Point> Points = new List<Point>();
    public int Score
    {
        get
        {
            int score = 0;
            
            foreach (var point in Points)
            {
                score += point.value;
            }        
            return score;
        }
    }
}

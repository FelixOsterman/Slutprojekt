var game = new Game();
game.CreateMap();
game.IsRunning();

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
    const char point_mark = '.';

    List<Point> points = new List<Point>();
    const int number_of_points = 10;
    public int value_of_all_points;

    Player player1;
    Player player2;
    const char player1_mark = 'X';
    const char player2_mark = 'O';

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

        for (int i = 0; i < number_of_points; i++)
        {
            points.Add(new Point());
            value_of_all_points += points[i].Value;

            while (true)
            {
                points[i].X = Random.Shared.Next(1, width - 1);
                points[i].Y = Random.Shared.Next(1, height - 1);
                if (map[points[i].X, points[i].Y] != wall && map[points[i].X, points[i].Y] != map[player1.X, player1.Y] && map[points[i].X, points[i].Y] != map[player2.X, player2.Y])
                {
                    break;
                }
            }

            map[points[i].X, points[i].Y] = point_mark;
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
        Console.WriteLine("Points: " + player1.Score + "/" + value_of_all_points);
        Console.SetCursorPosition(20, 0);
        Console.WriteLine("Player: " + player2.name);
        Console.SetCursorPosition(20, 1);
        Console.WriteLine("Steps: " + steg2);
        Console.SetCursorPosition(20, 2);
        Console.WriteLine("Points: " + player2.Score + "/" + value_of_all_points);

        map[player1.X, player1.Y] = player1_mark;
        map[player2.X, player2.X] = player2_mark;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                WriteOut(x, y, map[x, y]);
            }
        }

        map[player1.X, player1.Y] = blank;
        map[player2.X, player2.X] = blank;

        if (player1.Score > value_of_all_points / 2)
        {
            Console.WriteLine(player1.name + " win!");
            running = false;
        }
        if (player2.Score > value_of_all_points / 2)
        {
            Console.WriteLine(player2.name + " win!");
            running = false;
        }
        if (player1.Score == value_of_all_points / 2 && player2.Score == value_of_all_points / 2)
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
            case point_mark:
                Console.BackgroundColor = ConsoleColor.Yellow;
                break;
            case player1_mark:
                Console.BackgroundColor = ConsoleColor.Blue;
                break;
            case player2_mark:
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
                if (IsWall_or_IsPlayer(player1.X, player1.Y - 1) == false)
                {
                    player1.Y--;
                    steg1++;
                }
                break;
            case ConsoleKey.A:
                if (IsWall_or_IsPlayer(player1.X - 1, player1.Y) == false)
                {
                    player1.X--;
                    steg1++;
                }
                break;
            case ConsoleKey.D:
                if (IsWall_or_IsPlayer(player1.X + 1, player1.Y) == false)
                {
                    player1.X++;
                    steg1++;
                }
                break;
            case ConsoleKey.S:
                if (IsWall_or_IsPlayer(player1.X, player1.Y + 1) == false)
                {
                    player1.Y++;
                    steg1++;
                }
                break;
            case ConsoleKey.UpArrow:
                if (IsWall_or_IsPlayer(player2.X, player2.Y - 1) == false)
                {
                    player2.Y--;
                    steg1++;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (IsWall_or_IsPlayer(player2.X - 1, player2.Y) == false)
                {
                    player2.X--;
                    steg1++;
                }
                break;
            case ConsoleKey.RightArrow:
                if (IsWall_or_IsPlayer(player2.X + 1, player2.Y) == false)
                {
                    player2.X++;
                    steg1++;
                }
                break;
            case ConsoleKey.DownArrow:
                if (IsWall_or_IsPlayer(player2.X, player2.Y + 1) == false)
                {
                    player2.Y++;
                    steg1++;
                }
                break;
        }

        if (map[player1.X, player1.Y] == point_mark)
        {
            Point stone = PickPoint(player1.X, player1.Y);
            player1.Points.Add(stone);
        }
        else if (map[player2.X, player2.Y] == point_mark)
        {
            Point stone = PickPoint(player2.X, player2.Y);
            player2.Points.Add(stone);
        }


    }

    public bool IsWall_or_IsPlayer(int x, int y)
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
    public int Value = Random.Shared.Next(1, 11);
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
            int score;

            foreach (var point in Points)
            {
                score += point.Value;
            }
            return score;
        }
    }
}

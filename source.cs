using System;
using System.Threading;
using System.Collections.Generic;

class Player
{
    public char sprite {get; set;}
    public int _x {get; set;}
    public int _y {get; set;}

    public Player(char _0, int _1, int _2)
    {
        sprite = _0;
        _x = _1;
        _y = _2;
    }
}

public class App
{
    private List<string> map = new List<string>();
    private bool drawState = false;
    private bool end = false;
    private char dir = '►';
    private char _dir;
    private int score = 6;

    public App(){
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.WindowHeight = 40;
        Console.BufferHeight = 40;
        Console.WindowWidth = 112;
        Console.BufferWidth = 112;
        Console.Title = "Snake";

        //Console.BackgroundColor = ConsoleColor.Blue;
        //Console.ForegroundColor = ConsoleColor.Red;
    }

    public void setMap(int x){
        map.Clear();

        switch (x)
        {
            case 0:
            {
                map.Add("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▌                                    ▌");
                map.Add("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄");
                break;
            }
            
            case 1:
            {
                map.Add(" ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ");
                map.Add("▐                                ▌");
                map.Add("▐                     ------     ▌");
                map.Add("▐               ▌                ▌");
                map.Add("▐               ▌                ▌");
                map.Add("▐                                ▌");
                map.Add("▐   -------     ▌                ▌");
                map.Add("▐               ▌                ▌");
                map.Add("▐               ▌            .   ▌");
                map.Add("▐                                ▌");
                map.Add(" ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                break;
            }
        }
        
        foreach (string data in map)
        {
            Console.WriteLine(data);
        }
    }

    public void controls(){
        while (!end)
        {
            if (drawState)
            {    
                ConsoleKey key = Console.ReadKey(false).Key;

                if (key == ConsoleKey.UpArrow && _dir != '▼')
                    dir = '▲';

                if (key == ConsoleKey.DownArrow && _dir != '▲')
                    dir = '▼';

                if (key == ConsoleKey.RightArrow && _dir != '◄')
                    dir = '►';

                if (key == ConsoleKey.LeftArrow && _dir != '►')
                    dir = '◄';
            }
        }
    }

    public void game(){
        int x = 10;
        int y = 5;

        var snake = new List<Player>();
        snake.Add(new Player(dir, x, y));
        snake.Add(new Player('O', x-1, y));
        snake.Add(new Player('O', x-2, y));
        snake.Add(new Player('O', x-3, y));
        snake.Add(new Player('O', x-4, y));
        snake.Add(new Player('O', x-5, y));

        char[] rules = {
            '▌',
            '▐',
            '▀',
            '▄',
            '-'
        };

        while (!end)
        {
            drawState = true;
            
            foreach (Player data in snake)
            {
                Console.SetCursorPosition(data._x,data._y);
                Console.Write(map[y][x]);
            }

            switch (dir)
            {
                case '▲':
                {
                    y--;
                    break;
                }
                
                case '▼':
                {
                    y++;
                    break;
                }

                case '►':
                {
                    x++;
                    break;
                }

                case '◄':
                {
                    x--;
                    break;
                }
            }
                
            if (map[y][x] == '.')
            {    
                snake.Add(new Player('O', x-score, y));
                score++;
            }

            for (int k = (snake.Count-1); k > 0; k--)
            {
                snake[k]._x = snake[k-1]._x;
                snake[k]._y = snake[k-1]._y;
            }
            
            snake[0]._x = x;
            snake[0]._y = y;
            snake[0].sprite = dir;

            for (int k = (snake.Count-1); k >= 0; k--)
            {
                Console.SetCursorPosition(snake[k]._x,snake[k]._y);
                Console.Write(snake[k].sprite);

                if (snake[k].sprite != dir && x == snake[k]._x && y == snake[k]._y)
                    end = true;
            }

            foreach (char rule in rules)
            {
                if (map[y][x] == rule)
                    end = true;
            }

            Console.SetCursorPosition(0,30);
            
            drawState = false;
            _dir = dir;
            Thread.Sleep(200);
        }
    
        Console.SetCursorPosition(30,12);
        Console.Write("Game Over.");
    }
}

public class exe{
    static void Main()
    {
        while (0 < 1)
        {
            Console.Clear();

            App app = new App();
            var driver = new Thread(app.controls);

            app.setMap(1);

            driver.Start();
            app.game();

            Console.ReadLine();
        }
    }
}

//Thread.Sleep(100);







public class Player 
{
    public string? Name { get; set; }
    public Dungeon CurrentDungeon { get; set; }
    public TurnTimer CurrentTurnTimer { get; set; }
    public int X { get; set; }
    public int Y { get; set ;}

    public Player()
    {
        X = 0;
        Y = 0;
    }

    public Player(int x, int y)
    {
        Name = "Player";
        X = x;
        Y = y;
    }

    public Player(string name)
    {
        Name = name;
        X = 0;
        Y = 0;
    }

    public Player(int x, int y, string name)
    {
        Name = name;
        X = x;
        Y = y;

    }

    public void SetTurnTimer(TurnTimer turnTimer)
    {
        CurrentTurnTimer = turnTimer;
    }

    public void SetDungeon(Dungeon dungeon)
    {
        CurrentDungeon = dungeon;
    }
    
    public void GetUserName()
    {
        string? userInput;

        while(true)
        {
            Console.Write("What is your name? ");
            Name = Console.ReadLine();
            if(Text.IsAlpha(Name!))
            {
                if(Name == "guid")
                {
                    Name = Guid.NewGuid().ToString();
                }
                Console.WriteLine($"Are you sure you want to choose {Name}? Type \'y\' or \'n\' to confirm: ");
                userInput = Console.ReadLine();
                
                if(userInput?.ToLower() == "y")
                {
                    break;
                }
                else if(userInput?.ToLower() == "n")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid Response \n");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid Response.");
                continue;
            }
        }
    }

    public void GetMove()
    {
        string? userInput;
        while(true)
        {   
            Console.WriteLine($"What will you do?");
            userInput = Console.ReadLine();
            switch(userInput!.ToLower())
            {
                case null:
                    continue;
                case "move north":
                    MoveNorth();
                    break;
                case "move south":
                    MoveSouth();
                    break;
                case "move east":
                    MoveEast();
                    break;
                case "move west":
                    MoveWest();
                    break;
                case "check surroundings":
                    if(CurrentDungeon.PlayerInFountainRoom())
                    {
                        Console.WriteLine($"Player is in Fountain Room!");
                        break;
                    }
                    else if(CurrentDungeon.PlayerInSpawnRoom())
                    {
                        Console.WriteLine($"Player is in Spawn Room!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Player is not in Fountain Room.");
                        break;
                    }
                case "leave":
                    if(CurrentDungeon.PlayerInSpawnRoom() && CurrentDungeon.FountainActive == true)
                    {
                        Console.WriteLine("You beat the game! Good job.");
                        CurrentDungeon.IsBeaten = true;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You can't leave just yet!");
                        continue;
                    }
                case "activate":    
                case "activate fountain":
                    if(CurrentDungeon.PlayerInFountainRoom() && !CurrentDungeon.FountainActive)
                    {
                        CurrentDungeon.FountainActive = true;
                        Console.WriteLine($"Player activates the fountain!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You cannot do that right now!");
                        continue;
                    }
                case "deactivate":
                case "deactivate fountain":
                    if(CurrentDungeon.PlayerInFountainRoom() && CurrentDungeon.FountainActive)
                    {
                        CurrentDungeon.FountainActive = false;
                        Console.WriteLine($"You deactivate the fountain, immediately feeling a cosmic sense of confusion and dissapointment.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You can't do that.");
                        continue;
                    }
                default:
                    Console.WriteLine("Not a valid choice. Try again.");
                    continue;
            }
            break;
        }
    }
    public void MoveNorth()
    {
        Y++;
        if(IsOutOfBounds())
        {
            Y--;
            CurrentTurnTimer.CurrentTurn--;
            Console.WriteLine($"There appears to be a stone wall here");
        } 
    }

    public void MoveSouth() 
    {
        Y--;
        if(IsOutOfBounds())
        {
            Y++;
            CurrentTurnTimer.CurrentTurn--;
            Console.WriteLine($"There appears to be a stone wall here");
        } 
    } 
    public void MoveEast() 
    {
         X++;
        if(IsOutOfBounds())
        {
            X--;
            CurrentTurnTimer.CurrentTurn--;
            Console.WriteLine($"There appears to be a stone wall here");
        } 
    }

    public void MoveWest()
    {
        X--;
        if(IsOutOfBounds())
        {
            X++;
            CurrentTurnTimer.CurrentTurn--;
            Console.WriteLine($"There appears to be a stone wall here");
        } 
    } 

    public bool IsOutOfBounds()
    {
        return X > 3 || X < 0 || Y > 3 || Y < 0;
    }
}


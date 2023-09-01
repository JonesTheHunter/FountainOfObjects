


Game game = new Game();
game.Begin();

public class Game
{
    Player CurrentPlayer { get; set; }
    Dungeon CurrentDungeon { get; set; }
    TurnTimer CurrentTurn { get; set; }

    public Game()
    {
        CurrentPlayer = new Player();
        CurrentDungeon = new Dungeon(Room.CreateFourByFourDungeonv2(), CurrentPlayer);
        CurrentTurn = new TurnTimer();
        CurrentPlayer.SetDungeon(CurrentDungeon);
        CurrentPlayer.SetTurnTimer(CurrentTurn);
    }
    public void Begin()
    {
        Console.WriteLine($"Welcome to the Dungeon!");
        CurrentPlayer.GetUserName();
        Console.Clear();
        Console.WriteLine($"Welcome to the dungeon, {CurrentPlayer.Name}!");
        Console.WriteLine("Use the following commands to find the fountain, activate it, then leave: \n\n" +
                          "- \'move\' (north, south, east, west)\n" +
                          "- \'activate fountain\'\n" +
                          "- \'leave\'\n\n" +
                          "Good luck! \n\n");
        while(true)
        {
            Console.WriteLine($"You are in the room at ({CurrentPlayer.X},{CurrentPlayer.Y})");
            Console.WriteLine($"Current Turn: {CurrentTurn.CurrentTurn}\n");
            CurrentPlayer.GetMove();
            if(CurrentDungeon.IsBeaten)
            {
                Console.WriteLine($"You beat the dungeon in {CurrentTurn.CurrentTurn} turns!");
                break;
            }
            CurrentTurn.AdvanceTurn();
            continue;
        }
    }

    public void End()
    {
        return;
    }
}

public class Text 
{
    public static bool IsAlpha(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        foreach (char c in str)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }
        return true;
    }
}

public class TurnTimer
{
    public int CurrentTurn { get; set; }

    public TurnTimer()
    {
        CurrentTurn = 0;
    }

    public void PlayTurn()
    {

    }
    public void AdvanceTurn()
    {
        CurrentTurn++;
    }
}

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

public class Dungeon
{
    public Room[,] RoomMap { get; set; }
    public Player? Player { get; set; }
    public (int x, int y) FountainRoomCoords { get; init; }
    public bool FountainActive { get; set; } = false;
    public bool IsBeaten { get; set; } = false;

    public Dungeon(Room[,] roomMap, Player player)
    {
        RoomMap = roomMap;
        Player = player;
        FountainRoomCoords = GetFountainRoom();
        SetPlayerSpawn();
    }

    public Dungeon(Room[,] roomMap)
    {
        Player = null;
        RoomMap = roomMap;
        FountainRoomCoords = GetFountainRoom();
        SetPlayerSpawn();
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public void SetPlayerSpawn()
    {
        for(int x = 0; x < RoomMap.GetLength(0); x++)
        {
            for(int y = 0; y < RoomMap.GetLength(1); y++)
            {
                if(RoomMap[x,y].RoomType == Room.Type.SpawnRoom)
                {
                    Player.X = RoomMap[x,y].X;
                    Player.Y = RoomMap[x,y].Y;
                }
            }
        }
    }

    public bool PlayerInSpawnRoom()
    {
        (int x, int y) spawnCoords = GetSpawnRoom();
        (int x, int y) playerCoords = (Player.X, Player.Y);

        if(spawnCoords == playerCoords)
        {
            return true;
        }
        return false;
    }

    public bool PlayerInFountainRoom()
    {
        (int x, int y) fountainCoords = GetFountainRoom();
        (int x, int y) playerCoords = (Player.X, Player.Y);

        if(fountainCoords == playerCoords)
        {
            return true;
        }
        return false;
    }

    public (int x, int y) GetFountainRoom()
    {
        for(int x = 0; x < RoomMap.GetLength(0); x++)
        {
            for(int y = 0; y < RoomMap.GetLength(1); y++)
            {
                if(RoomMap[x,y].RoomType == Room.Type.FountainRoom)
                {
                    return (RoomMap[x,y].X,RoomMap[x,y].Y);
                }
                    
            }
        }
        return (-1,-1);
    }

    public (int x, int y) GetSpawnRoom()
    {
        for(int x = 0; x < RoomMap.GetLength(0); x++)
        {
            for(int y = 0; y < RoomMap.GetLength(1); y++)
            {
                if(RoomMap[x,y].RoomType == Room.Type.SpawnRoom)
                {
                    return (RoomMap[x,y].X,RoomMap[x,y].Y);
                }
                    
            }
        }
        return (-1,-1);
    }
}

public class Room 
{
    public int X { get; init; }
    public int Y { get; set; }
    public string Description { get; init; } = "";
    public Type RoomType { get; set; } = Type.EmptyRoom;

    public Room(int x, int y, Type roomType)
    {
        X = x;
        Y = y;
        RoomType = roomType;
        
    }

    public Room(int x, int y, string description, Type roomType)
    {
        X = x;
        Y = y;
        Description = description;
        RoomType = roomType;
    }

    public static Room CreateEmptyRoom(int x, int y) => new Room(x, y, Type.EmptyRoom);
    public static Room CreateFountainRoom(int x, int y) => new Room(x, y, "You are in the Fountain Room!", Type.FountainRoom);
    public static Room CreateSpawnRoom(int x, int y) => new Room(x, y, "You are in the spawn room.", Type.SpawnRoom);

    public static Room[,] CreateFourByFourDungeonv1() => new Room[4,4] {
                                          { CreateEmptyRoom(0,3), CreateEmptyRoom(1,3), CreateEmptyRoom(2,3), CreateEmptyRoom(3,3) },
                                          { CreateEmptyRoom(0,2), CreateEmptyRoom(1,2), CreateFountainRoom(2,2), CreateEmptyRoom(3,2) },
                                          { CreateEmptyRoom(0,1), CreateEmptyRoom(1,1), CreateEmptyRoom(2,1), CreateEmptyRoom(3,1) },
                                          { CreateSpawnRoom(0,0), CreateEmptyRoom(1,0), CreateEmptyRoom(2,0), CreateEmptyRoom(3,0) }
                                                                    };

        public static Room[,] CreateFourByFourDungeonv2() => new Room[4,4] {
                                          { CreateEmptyRoom(0,3), CreateEmptyRoom(1,3), CreateEmptyRoom(2,3), CreateEmptyRoom(3,3) },
                                          { CreateEmptyRoom(0,2), CreateEmptyRoom(1,2), CreateEmptyRoom(2,2), CreateEmptyRoom(3,2) },
                                          { CreateEmptyRoom(0,1), CreateEmptyRoom(1,1), CreateEmptyRoom(2,1), CreateEmptyRoom(3,1) },
                                          { CreateSpawnRoom(0,0), CreateEmptyRoom(1,0), CreateEmptyRoom(2,0), CreateFountainRoom(3,0) }
                                                                    };
    public enum Type
    {
        
        EmptyRoom,
        SpawnRoom,
        FountainRoom,
        PitRoom
    }
}


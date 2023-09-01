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









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


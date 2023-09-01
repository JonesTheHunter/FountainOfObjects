






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


using apbd_5.Models;

namespace apbd_5.Database;

public class DataStore
{
    public static List<Room> Rooms { get; } = new()
    {
        new Room { Id = 1, Name = "Lecture Hall 101", BuildingCode = "A", Floor = 1, Capacity = 120, HasProjector = true,  IsActive = true },
        new Room { Id = 2, Name = "Lab 202",          BuildingCode = "A", Floor = 2, Capacity = 30,  HasProjector = true,  IsActive = true },
        new Room { Id = 3, Name = "Seminar Room 305", BuildingCode = "B", Floor = 3, Capacity = 20,  HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "Conference Room 1", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = true,  IsActive = false },
        new Room { Id = 5, Name = "Workshop Room 110", BuildingCode = "C", Floor = 1, Capacity = 40, HasProjector = true,  IsActive = true }
    };
    
    public static int NextRoomId => Rooms.Count > 0 ? Rooms.Max(r => r.Id) + 1 : 1;
}
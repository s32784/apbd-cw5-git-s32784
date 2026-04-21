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
    
    public static List<Reservation> Reservations { get; } = new()
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Anna", Topic = "C# Dev", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Piotr", Topic = "Java Intro", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Max", Topic = "Web API", Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "confirmed" }
    };
}
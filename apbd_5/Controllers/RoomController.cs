using apbd_5.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbd_5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly
    )
    {
        var rooms = Database.DataStore.Rooms.AsEnumerable();
        
        if (minCapacity.HasValue) 
            rooms = rooms.Where(r => r.Capacity >= minCapacity);
        
        if (hasProjector.HasValue) 
            rooms = rooms.Where(r => r.HasProjector == hasProjector);
        
        if (activeOnly == true) 
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound($"Room with id {id} not found");
        }
        return Ok(room);
    }

    [HttpPost]
    public ActionResult<Room> CreateRoom(Room room)
    {
        room.Id = Database.DataStore.NextRoomId;
        Database.DataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }
    
    [HttpGet("building/{buildingCode}")]
    public ActionResult<List<Room>> GetByBuilding(string buildingCode)
    {
        var rooms = Database.DataStore.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        return Ok(rooms);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> UpdateRoom(int id, Room updatedRoom)
    {
        var existingRoom = Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (existingRoom == null)
        {
            return NotFound($"Room with id {id} not found");
        }
        
        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.Capacity = updatedRoom.Capacity;
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;

        return Ok(existingRoom);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound($"Room with id {id} not found");
        }
        
        if (Database.DataStore.Reservations.Any(res => res.RoomId == id))
        {
            return Conflict("Cannot delete room with existing reservations.");
        }

        Database.DataStore.Rooms.Remove(room);
        return NoContent(); 
    }
}
using apbd_5.Models;
using apbd_5.Database;
using Microsoft.AspNetCore.Mvc;

namespace apbd_5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] DateOnly? date, [FromQuery] int? roomId)
    {
        var query = DataStore.Reservations.AsQueryable();
        if (date.HasValue) query = query.Where(r => r.Date == date);
        if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId);
        return Ok(query.ToList());
    }

    [HttpPost]
    public IActionResult Create(Reservation res)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == res.RoomId);
        if (room == null) return NotFound("Room not found.");
        if (!room.IsActive) return BadRequest("Room is inactive.");
        if (res.EndTime <= res.StartTime) return BadRequest("End time must be after start time.");
        
        bool overlap = DataStore.Reservations.Any(r => 
            r.RoomId == res.RoomId && 
            r.Date == res.Date && 
            res.StartTime < r.EndTime && r.StartTime < res.EndTime &&
            r.Status != "cancelled");

        if (overlap) return Conflict("Room is already booked for this time.");

        res.Id = DataStore.NextReservationId;
        DataStore.Reservations.Add(res);
        return CreatedAtAction(nameof(GetAll), new { id = res.Id }, res);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();
        DataStore.Reservations.Remove(res);
        return NoContent();
    }
}
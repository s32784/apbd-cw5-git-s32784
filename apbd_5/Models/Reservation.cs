using System.ComponentModel.DataAnnotations;

namespace apbd_5.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    [MinLength(1)]
    public string OrganizerName { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Topic { get; set; } = string.Empty;

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public string Status { get; set; } = "planned"; // planned, confirmed, cancelled
}
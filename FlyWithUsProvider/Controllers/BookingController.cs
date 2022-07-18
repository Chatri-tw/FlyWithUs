using System.Net;
using FlyWithUsProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithUsProvider.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly List<Booking> _bookings;

    public BookingController(ILogger<BookingController> logger)
    {
        _logger = logger;
        _bookings = new List<Booking>
        {
            new Booking{PNR = "BNK48", Firstname = "Alice"},
            new Booking{PNR = "AKB48", Firstname = "Bob"},
            new Booking{PNR = "RGB72", Firstname = "Takachi"}
        };
    }

    [HttpGet("{pnr}")]
    public IActionResult Get(string pnr, string? firstname)
    {
        var booking = _bookings.Find(booking => booking.PNR.Equals(pnr) && booking.Firstname.Equals(firstname));
        if (booking != null)
        {
            return Ok(booking);
        }

        return NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Booking))]
    public IActionResult Post()
    {
        return Created(nameof(Get), new Booking{PNR="BNK48"});
    }

    [Route("checkin")]
    [HttpPost]
    public IActionResult CheckIn()
    {
        return Ok();
    }
}
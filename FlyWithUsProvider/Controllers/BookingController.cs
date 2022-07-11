using System.Net;
using FlyWithUsProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlyWithUsProvider.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;

    public BookingController(ILogger<BookingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Booking Get()
    {
        return new Booking
        {
            PNR = "BNK48"
        };
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
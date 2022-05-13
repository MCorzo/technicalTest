using System.Text.RegularExpressions;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace technicalTest.Controllers;

[ApiController]
[Route("[controller]")]
public class GeolocationController : ControllerBase
{
    private IGeollocationRepository _repoGeollocation;
    private readonly ILogger<GeolocationController> _logger;
    private string _regexValidator = "^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)(\\.(?!$)|$)){4}$";

    public GeolocationController(ILogger<GeolocationController> logger, IGeollocationRepository repository)
    {
        _logger = logger;
        _repoGeollocation = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IpGeolocation))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public IActionResult geolocation()
    {
        var ipAddress = getClientIP();
        var regexValidator = new Regex(_regexValidator);

        if (!regexValidator.Match(ipAddress).Success)
            return BadRequest("The given value is not a valid ip address" + ipAddress);

        var result = _repoGeollocation.GetIpGeolocation(ipAddress);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IpGeolocation>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public IActionResult geolocation(IEnumerable<string> ipAddresses)
    {
        var regexValidator = new Regex(_regexValidator);
        var anyInvalidIp = ipAddresses.Any(ip => !regexValidator.Match(ip).Success);

        if (anyInvalidIp)
            return BadRequest("A value in the array is not a valid ip address");

        var result = _repoGeollocation.GetIpsGeolocation(ipAddresses);

        return Ok(result);
    }

    private string getClientIP()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress;

        return ipAddress.MapToIPv4().ToString();
    }
}

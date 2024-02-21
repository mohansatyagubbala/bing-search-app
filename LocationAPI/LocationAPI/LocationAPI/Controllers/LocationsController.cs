using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LocationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly List<Location> locations = new List<Location>
        {
            new Location { Name = "Pharmacy", Availability = "10 am to 1 pm" },
            new Location { Name = "Bakery", Availability = "8 am to 2 pm" },
            new Location { Name = "Barber Shop", Availability = "9 am to 5 pm" },
            new Location { Name = "Supermarket", Availability = "1 am to 12 pm" },
            new Location { Name = "Candy Store", Availability = "10 am to 8 pm" },
            new Location { Name = "Cinema Complex", Availability = "12 pm to 10 pm" },
            new Location { Name = "Gym", Availability = "5 pm to 10 pm" },
            new Location { Name = "Restaurant", Availability = "11 am to 1 pm" },
            new Location { Name = "Library", Availability = "2 pm to 6 pm" },
            new Location { Name = "Coffee Shop", Availability = "7 am to 7 pm" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var availableLocations = locations
                .Where(l => IsLocationAvailableBetween10amAnd1pm(l.Availability))
                .ToList();

            if (!availableLocations.Any())
            {
                return NotFound();
            }

            return Ok(availableLocations);
        }

        private bool IsLocationAvailableBetween10amAnd1pm(string availability)
        {
            string[] parts = availability.Split(new char[] { ' ', 't', 'o' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4)
            {
                return false;
            }

            string start = parts[0] + " " + parts[1];
            string end = parts[2] + " " + parts[3];

            DateTime startTime, endTime;
            if (!DateTime.TryParseExact(start, "h tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime) ||
                !DateTime.TryParseExact(end, "h tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
            {
                return false;
            }

            DateTime tenAM = DateTime.Today.AddHours(10);
            DateTime onePM = DateTime.Today.AddHours(13);

            return startTime <= onePM && endTime >= tenAM;
        }
    }

    public class Location
    {
        public string Name { get; set; } = ""; 
        public string Availability { get; set; } = "";
    }
}

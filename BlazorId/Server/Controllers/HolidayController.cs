using BlazorId.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorId.Server.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        [HttpGet("{year}")]
        public List<Holiday> Get(int year)
        {
            DateTime paques = CalculatePaques(year);
            holidays.Add(new Holiday(paques.Day, paques.Month));
            DateTime ascension = paques.AddDays(39);
            holidays.Add(new Holiday(ascension.Day, ascension.Month));
            DateTime pentecote = paques.AddDays(50);
            holidays.Add(new Holiday(pentecote.Day, pentecote.Month));
            return holidays;
        }

        private List<Holiday> holidays = new()
        {
            new(1, 1),//(day,month)
            new(1, 5),
            new(8, 5),
            new(14, 7),
            new(15, 8),
            new(1, 11),
            new(11, 11),
            new(25, 12),
        };
        private static DateTime CalculatePaques(int year)
        {
            // (from Computus)
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int mois = (h + l - 7 * m + 114) / 31;
            int jour = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(year, mois, jour);
        }
    }
}

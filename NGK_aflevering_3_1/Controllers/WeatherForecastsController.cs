using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGK_aflevering_3_1;

namespace NGK_aflevering_3_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHubContext<WeatherForecastHub> hub;

        public WeatherForecastsController(DBContext context, IHubContext<WeatherForecastHub> hub)
        {
            _context = context;
            this.hub = hub;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            return await _context.Set<WeatherForecast>().Include(x => x.Location).ToListAsync();
        }




        // GET: api/WeatherForecasts/ID/5
        [HttpGet("ID/{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }
            return weatherForecast;
        }

        // PUT: api/WeatherForecasts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.ID)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);

            await hub.Clients.All.SendAsync("NewMeasurements",
               weatherForecast.Date,
               weatherForecast.Location.Name,
               weatherForecast.Location.Latitude,
               weatherForecast.Location.Longitude,
               weatherForecast.Temperature,
               weatherForecast.Humidity,
               weatherForecast.AirPressure);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.ID }, weatherForecast);
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherForecast>> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return weatherForecast;
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecasts.Any(e => e.ID == id);
        }

        // GET: api/WeatherForecasts/ThreeNewest
        [HttpGet("ThreeNewest")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetThreeNewest()
        {
            return await _context.WeatherForecasts.Include(x => x.Location).OrderByDescending(x => x.Date).Take(3).ToListAsync();
        }

        // GET: api/WeatherForecasts/*DATE*
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetDate(DateTime date)
        {
            return await _context.WeatherForecasts.Include(x => x.Location).Where(x => x.Date.Date == date.Date).ToListAsync();
        }

        // GET: api/WeatherForecasts/*DATE*
        [HttpGet("{start}/{end}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetDate(DateTime start, DateTime end)
        {
            return await _context.WeatherForecasts.Where(x => x.Date >= start && x.Date <= end).Include(x => x.Location).ToListAsync();
        }
    }
}

using DiplomenProekt.Data.Models;
using DiplomenProekt.Data;
using Microsoft.AspNetCore.Mvc;
using DiplomenProekt.Models;

namespace DiplomenProekt.Controllers
{
    public class ArduinoController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ArduinoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Create()
        {
            var model = new TempReadingViewModel();
            return Ok();
        }

        [HttpGet]
        public IActionResult Create(double arduino)
        {

            var tmpRng = new TempReading
            {
                RecTime = DateTime.UtcNow,
                ReadedTemp = arduino,
            };
            db.Add(tmpRng);
            db.SaveChanges();
            return Ok();
        }
        public IActionResult Read()
        {
            var arduino = db.TempReadings.OrderByDescending(x => x.Id).Take(7).ToList();
            var model = arduino.Select(a => new TempReadingViewModel
            {
                Id = a.Id,
                ReadedTemper = a.ReadedTemp,
                RecordTime = a.RecTime
            }).ToList();
            return View(model);
        }


        public IActionResult TempDateChart()
        {
            return View();
        }
        public List<object> GetChartData()
        {
            List<object> data = new List<object>();
            List<DateTime> label = db.TempReadings.Select(r => r.RecTime).Take(7).ToList();
            List<double> Temp = db.TempReadings.Select(t => t.ReadedTemp).Take(7).ToList();
            data.Add(label);
            data.Add(Temp);
            return data;
        }
        //setting the microcontroller turn the cool/heat relays
        public IActionResult TempSeting()
        {
            var tmpset = db.TempReadings.OrderByDescending(x => x.Id).Take(1);
            var model = tmpset.Select(t => new TempReadingViewModel
            {
                ReadedTemper = t.ReadedTemp,
                RecordTime = t.RecTime
            });
            return View(model);

        }
    }
}

using DiplomenProekt.Data.Models;
using DiplomenProekt.Data;
using Microsoft.AspNetCore.Mvc;
using DiplomenProekt.Models;
using Newtonsoft.Json;

namespace DiplomenProekt.Controllers
{
    public class ArduinoController : Controller
    {
        private readonly IArduinoService arduinoService;
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ArduinoController(IArduinoService arduinoService, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.arduinoService = arduinoService;
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }
        //Arduino sends the data via GET request
        //Then we save the records in the DB
        [HttpGet]
        public IActionResult Create(double arduino)
        {

            var tmpRng = new TempReading
            {
                RecTime = DateTime.UtcNow,
                ReadedTemp = arduino
            };
            db.Add(tmpRng);
            db.SaveChanges();
            return Ok();
        }
        //After we save the records, we use mainly the DB
        //to make different CRUD operations
        public IActionResult Read()
        {
            var arduino = arduinoService.GetTemperaturesOfLastNReadings(7);

            var model = arduino.Select(a => new TempReadingViewModel
            {
                Id = a.Id,
                ReadedTemper = a.ReadedTemp,
                RecordTime = a.RecTime
            }).ToList();
            return View(model);
        }
        //Thats the action for the view file
        //Where we see the bar chart
        public IActionResult TempDateChart()
        {
            return View();
        }
        //By this action we send "adress data" to the arduino
        //and then arduino decides to turn heat or cool realy
        public IActionResult HeatButton(string data)
        {
            data="Heat";
            return Ok(data);
        }
        public IActionResult CoolButton(string data)
        {
            data = "Cool";
            return Ok(data);
        }
        //With this action we set on the screen only the last record of the DB
        public IActionResult TempSeting()
        {
            var arduino = arduinoService.GetTemperaturesOfLastNReadings(1);

            var model = arduino.Select(a => new TempReadingViewModel
            {
                //Id = a.Id,
                ReadedTemper = a.ReadedTemp,
                RecordTime = a.RecTime
            }).FirstOrDefault();
            return View(model);
        }
        //That's method for the chart view where we make the data to JSON 
        public string GetChartData()
        {
            
            List<string> label = db.TempReadings.OrderByDescending(x => x.RecTime).Take(7).Select(r => r.RecTime.ToString("dd.MM.yyyy [HH:mm]")).ToList();
            List<double> temp = db.TempReadings.OrderByDescending(x => x.RecTime).Take(7).Select(r => r.ReadedTemp).ToList();
             return JsonConvert.SerializeObject(new { label, temp });
        }
    }

}



        

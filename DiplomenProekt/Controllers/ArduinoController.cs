﻿using DiplomenProekt.Data.Models;
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
        public IActionResult TempDateChart()
        {
            return View();
        }

        public IActionResult HeatButton(int data)
        {
            data=1;
            return Ok(data);
        }
        public IActionResult CoolButton(int data)
        {
            data = 0;
            return Ok();
        }
        public string GetChartData()
        {
            
            List<string> label = db.TempReadings.OrderByDescending(x => x.RecTime).Take(7).Select(r => r.RecTime.ToString("dd.MM.yyyy [HH:mm]")).ToList();
            List<double> temp = db.TempReadings.OrderByDescending(x => x.RecTime).Take(7).Select(r => r.ReadedTemp).ToList();
             return JsonConvert.SerializeObject(new { label, temp });
        }
        public IActionResult TempSeting()
        {
            var tmpset = db.TempReadings.OrderByDescending(x => x.Id);
            var model = tmpset.Select(t => new TempReadingViewModel
            {
                ReadedTemper = t.ReadedTemp,
                RecordTime = t.RecTime
            }).FirstOrDefault();    
            return View(model);

        }

    }

}



        

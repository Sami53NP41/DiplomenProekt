using DiplomenProekt.Data;
using DiplomenProekt.Data.Models;
using DiplomenProekt.Models;

namespace DiplomenProekt
{
    public class Arduino_Service : IArduinoService
    {
        private readonly ApplicationDbContext db;

        public Arduino_Service(ApplicationDbContext db)
        {
            this.db = db;

        }

        public List<TempReadingViewModel> GetLastNReadingsAsVies(int count)
        {
            //var result = db.TempReadings.OrderByDescending(x => x.Id).Select(a => new TempReadingViewModel
            //{
            //    Id = a.Id,
            //    ReadedTemper = a.ReadedTemp,
            //    RecordTime = a.RecTime
            //}).Take(count).ToList();

            return GetTemperaturesOfLastNReadings(count).Select(a => new TempReadingViewModel
            {
                Id = a.Id,
                ReadedTemper = a.ReadedTemp,
                RecordTime = a.RecTime
            }).ToList();
        }

        public List<TempReading> GetTemperaturesOfLastNReadings(int count)
        {
            return db.TempReadings.OrderByDescending(x => x.Id).Take(count).ToList();
        }

        public void RecordNewTemperature(TempReading tmpRng)
        {
            throw new NotImplementedException();
        }
    }
}

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

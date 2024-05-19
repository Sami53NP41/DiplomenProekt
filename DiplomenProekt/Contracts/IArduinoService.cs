using DiplomenProekt.Data.Models;
using DiplomenProekt.Models;

namespace DiplomenProekt
{
    public interface IArduinoService
    {
        List<TempReading> GetTemperaturesOfLastNReadings(int v);

        void DeleteRecordsAfterMonth(DateTime date);
    }
}

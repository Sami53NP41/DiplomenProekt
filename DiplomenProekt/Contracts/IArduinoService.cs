using DiplomenProekt.Data.Models;
using DiplomenProekt.Models;

namespace DiplomenProekt
{
    public interface IArduinoService
    {
        List<TempReadingViewModel> GetLastNReadingsAsVies(int v);
        List<TempReading> GetTemperaturesOfLastNReadings(int v);
        void RecordNewTemperature(TempReading tmpRng);
    }
}

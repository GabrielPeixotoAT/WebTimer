using System.Security.Claims;
using WebTimer.Models;

namespace WebTimer.Services.Records.Interfaces
{
    public interface IRecordService
    {
        Task<DateTime> InsertRecord(int statusId, ClaimsPrincipal user);
        Record? GetOpen(ClaimsPrincipal user);
        Task<Dictionary<int, TimeSpan>> GetRecordsAndTimes(ClaimsPrincipal user);
        Task<Dictionary<int, TimeSpan>> GetRecordsAndTimesToday(ClaimsPrincipal user);
    }
}

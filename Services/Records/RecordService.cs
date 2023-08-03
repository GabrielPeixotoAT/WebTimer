using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using WebTimer.Data;
using WebTimer.Models;
using WebTimer.Services.Auth.Interfaces;
using WebTimer.Services.Records.Interfaces;

namespace WebTimer.Services.Records
{
    public class RecordService : IRecordService
    {
        ApplicationDbContext context;

        IIdentityService identityService;

        public RecordService(ApplicationDbContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<DateTime> InsertRecord(int statusId, ClaimsPrincipal user)
        {
            Record? record = GetOpen(user);

            if (record != null)
            {
                if (record.StatusId == statusId)
                {
                    record.EndTime = DateTime.Now;
                }
                else
                {
                    throw new ApplicationException("Registro de atividade diferente em aberto!");
                }
            }
            else
            {
                record = new Record
                {
                    StartTime = DateTime.Now,
                    Status = context.Status.First(s => s.StatusId == statusId),
                    User = await identityService.GetCurrentUserAsync(user),
                };

                context.Records.Add(record);
            }

            await context.SaveChangesAsync();

            return DateTime.Now;
        }

        private Record? GetOpen(ClaimsPrincipal user)
        {
            return context.Records.FirstOrDefault(r => r.User.Id == identityService.GetUserId() && r.EndTime == null);
        }

        public IEnumerable<Record> GetRecords(ClaimsPrincipal user)
        {
            string userId = identityService.GetUserId();

            return context.Records.Where(r => r.User.Id == userId);
        }

        public async Task<Dictionary<int, TimeSpan>> GetRecordsAndTimes(ClaimsPrincipal user)
        {
            var records = GetRecords(user).ToList();

            records = records.Where(rec => rec.EndTime != null).ToList();

            var result2 = records.GroupBy(rec => rec.StatusId).ToDictionary(
                rec => rec.Key,
                rec => TimeSpan.FromTicks(rec.Sum(r => (r.EndTime - r.StartTime).Value.Ticks))
                );

            Dictionary<int, TimeSpan> result = new Dictionary<int, TimeSpan>();

            TimeSpan time;

            if(result2.TryGetValue(1, out time))
                result.Add(1, time);
            else
                result.Add(1, TimeSpan.Zero);
            if (result2.TryGetValue(2, out time))
                result.Add(2, time);
            else
                result.Add(2, TimeSpan.Zero);
            if (result2.TryGetValue(3, out time))
                result.Add(3, time);
            else
                result.Add(3, TimeSpan.Zero);
            if (result2.TryGetValue(4, out time))
                result.Add(4, time);
            else
                result.Add(4, TimeSpan.Zero);
            if (result2.TryGetValue(5, out time))
                result.Add(5, time);
            else
                result.Add(5, TimeSpan.Zero);

            return result;
        }

        public async Task<Dictionary<int, TimeSpan>> GetRecordsAndTimesToday(ClaimsPrincipal user)
        {
            var records = GetRecords(user).ToList();

            records = records.Where(rec => rec.EndTime != null)
                .Where(rec => rec.EndTime > DateTime.Today).ToList();

            var result2 = records.GroupBy(rec => rec.StatusId).ToDictionary(
                rec => rec.Key,
                rec => TimeSpan.FromTicks(rec.Sum(r => (r.EndTime - r.StartTime).Value.Ticks))
                );

            Dictionary<int, TimeSpan> result = new Dictionary<int, TimeSpan>();

            TimeSpan time;

            if (result2.TryGetValue(1, out time))
                result.Add(1, time);
            else
                result.Add(1, TimeSpan.Zero);
            if (result2.TryGetValue(2, out time))
                result.Add(2, time);
            else
                result.Add(2, TimeSpan.Zero);
            if (result2.TryGetValue(3, out time))
                result.Add(3, time);
            else
                result.Add(3, TimeSpan.Zero);
            if (result2.TryGetValue(4, out time))
                result.Add(4, time);
            else
                result.Add(4, TimeSpan.Zero);
            if (result2.TryGetValue(5, out time))
                result.Add(5, time);
            else
                result.Add(5, TimeSpan.Zero);

            return result;
        }
    }
}

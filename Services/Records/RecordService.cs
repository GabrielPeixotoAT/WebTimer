using Microsoft.AspNetCore.Http;
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
    }
}

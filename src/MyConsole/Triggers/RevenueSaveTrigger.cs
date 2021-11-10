using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using MyConsole.Entities;

namespace MyConsole.Triggers
{
    public class RevenueSaveTrigger : IBeforeSaveTrigger<Revenue> // Any changes made in IBeforeSaveTrigger<TEntity> will be included within the transaction
    {
        readonly MyContext _myContext;
        public RevenueSaveTrigger(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task BeforeSave(ITriggerContext<Revenue> context, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ChangeType: {context.ChangeType}");
            
            if (context.ChangeType == ChangeType.Added)
            {
                var date = context.Entity.CreateDate.Date;
                var userId = context.Entity.UserId;
                var utcNow = DateTime.UtcNow;

                var revenueDaily = await _myContext.RevenueDailies.FirstOrDefaultAsync(i => i.Date == date && i.UserId == userId);
                if (revenueDaily == null)
                {
                    revenueDaily = new RevenueDaily
                    {
                        Date = date,
                        UserId = userId,
                        CreateDate = utcNow
                    };
                    _myContext.RevenueDailies.Add(revenueDaily);
                }

                revenueDaily.TotalAmount += context.Entity.Amount;
                revenueDaily.ModifyDate = utcNow;
                await _myContext.SaveChangesAsync();
            }
        }
    }
}
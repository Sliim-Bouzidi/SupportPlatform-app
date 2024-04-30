using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using System.Net.NetworkInformation;

namespace Support_Ticket_System.Services.status_services
{
    public class statusservices : IStatusservice
    {
        private readonly Datacontext  _context ;

        public statusservices(Datacontext context)
        {
            _context = context;
        }
        public IEnumerable<Status> GetAllStatuses()
        {
            return _context.statuses.ToList();
        }

        public async Task<IEnumerable<StatusHistory>> GetStatusHistoryOfTicket(Guid TicketID)
        {
            var statushistory = await _context.statushistory.Where(s=>s.Ticket.TicketID == TicketID)
                .Select(s=> new StatusHistory
                {
                    StatusValue = s.StatusValue,
                    TimeStamp = s.TimeStamp.Date
                })
                .ToListAsync();
            return statushistory;
        }

        public IEnumerable<string> GetStatusNames()
        {
            var statusNames = _context.statuses
                                    .Select(s => s.StatusName)
                                    .ToList(); 
            return statusNames;
        }

        public StatusHistory SetStatus(Guid TicketID, string statusName = null )
        {
            var statuss = _context.statuses.Where(s=>s.StatusName == statusName).FirstOrDefault();
            if (statuss != null)
            {
                var StatusID = statuss.StatusID;
                var StatusHistoryadded = new StatusHistory()
                {
                    StatusHistoryID = Guid.NewGuid(),
                    StatusValue = statusName,
                    StatusID = StatusID,
                    TimeStamp = DateTime.Now,
                    TicketID = TicketID
                    

                };
                _context.statushistory.Add(StatusHistoryadded);
                _context.SaveChanges();
                
                return StatusHistoryadded;

            }
            else
                return null;
        }

      
    }
}

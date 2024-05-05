using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.PriorityServices;
using Support_Ticket_System.Services.severity_services;
using Support_Ticket_System.Services.status_services;
using Support_Ticket_System.Services.Tagservices;

namespace Support_Ticket_System.Services.ticketservices
{
    public class ticketservices : ITicketService
    {

        private readonly Datacontext _context;
        private readonly IStatusservice _statusservice;
        private readonly IPriorityServices _priorityServices;
        private readonly IseverityServices _severityServices;
        private readonly ITagServices _tagServices;


        public ticketservices(Datacontext context, IStatusservice statusservices, IPriorityServices priorityservices, IseverityServices severityservices, ITagServices tagservices)
        {
            _context = context;
            _statusservice = statusservices;
            _priorityServices = priorityservices;
            _tagServices = tagservices;
            _severityServices = severityservices;

        }


        public async Task<Ticket> CreateTicket(string title, string description, string assignTo, string statusName, string processFlowname, string username, string tenantname, string priorityname, string severityname, List<string> tag)
        {
            var processFlow = await _context.processFlows.FirstOrDefaultAsync(p => p.ProcessFlowName == processFlowname);
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username);
            var tenant = await _context.tenants.FirstOrDefaultAsync(t => t.Name == tenantname);
            var priority = _priorityServices.SetPriority(priorityname);
            var severity = await _context.severities.FirstOrDefaultAsync(s => s.SeverityName == severityname);

            var ticket = new Ticket
            {
                TicketID = Guid.NewGuid(),
                Title = title,
                Description = description,
                AssignTo = assignTo,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                processFlow = processFlow,
                severity = severity,
                user = user,
                priority = priority,
                tenant = tenant
            };

            _context.tickets.Add(ticket);
            await _context.SaveChangesAsync();

            await StoreInTicketHistory(ticket.TicketID, ticket.UserID, "ticket Creation", null, null);

            _statusservice.SetStatus(ticket.TicketID, statusName);
            var statusvalue = await _context.statushistory
                .Where(p => p.TicketID == ticket.TicketID)
                .OrderByDescending(p => p.TimeStamp)
                .Select(p => p.StatusValue)
                .FirstOrDefaultAsync();

            ticket.Status = statusvalue;

            var tagadded = _tagServices.AddTagtoticket(ticket.TicketID, tag);
            foreach (var tag1 in tagadded)
            {
                ticket.tags.Add(tag1);
            }

            await _context.SaveChangesAsync();

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(string TenantName)
        {
            return await _context.tickets
                .Where(t => t.tenant.Name == TenantName)
                .Include(t => t.tags)
                    .ThenInclude(ti => ti.tag)
                .Include(t => t.user)
                .Include(t => t.processFlow)
                .Include(t => t.priority)
                .Select(t => new Ticket
                {
                    TicketID = t.TicketID,
                    AssignTo = t.AssignTo,
                    Title = t.Title,
                    Status = t.Status,
                    Description = t.Description,
                    CreatedDate = t.CreatedDate.Date,
                    user = t.user != null ? new User
                    {
                        UserID = t.user.UserID,
                        Username = t.user.Username,
                    } : null,
                    processFlow = new ProcessFlow
                    {
                        ProcessFlowName = t.processFlow.ProcessFlowName
                    },
                    priority = t.priority != null ? new Priority
                    {
                        PriorityID = t.priority.PriorityID,
                        PriorityName = t.priority.PriorityName
                    } : null,
                    tags = t.tags
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TicketHistory>> GetTicketHistory(Guid TicketID)
        {
            var tickethistory = await _context.ticketHistories.Where(t => t.TicketID == TicketID)
                .Include(t => t.user.Username)
                .ToListAsync();
            return tickethistory;
        }

        public async Task<bool> StoreInTicketHistory(Guid ticketID, Guid? userID, string changetype, string oldvalue = null, string newvalue = null)
        {
            var tickethistoryline = new TicketHistory
            {
                TicketHistoryID = Guid.NewGuid(),
                changeType = changetype,
                OldValue = oldvalue,
                NewValue = newvalue,
                TimeStamp = DateTime.UtcNow,
                TicketID = ticketID,
                UserID = userID
            };


            _context.ticketHistories.Add(tickethistoryline);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Ticket> ticketDetails(Guid TicketID)
        {

            var ticket = await _context.tickets
                                .Where(t => t.TicketID == TicketID)
                                .Include(t => t.tags)
                                    .ThenInclude(tags => tags.tag)
                                .Select(t => new Ticket
                                {
                                    TicketID = t.TicketID,
                                    Title = t.Title,
                                    Description = t.Description,
                                    CreatedDate = t.CreatedDate,
                                    UpdatedDate = t.UpdatedDate.Date,
                                    AssignTo = t.AssignTo,


                                    processFlow = new ProcessFlow
                                    {
                                        ProcessFlowName = t.processFlow.ProcessFlowName
                                    },



                                    priority = new Priority
                                    {
                                        PriorityName = t.priority.PriorityName
                                    },

                                    severity = new Severity
                                    {
                                        SeverityName = t.severity.SeverityName
                                    },
                                    Status = t.Status,
                                    tags = t.tags,


                                })
                                .FirstOrDefaultAsync();


            return ticket;
        }


        public async Task<Ticket> UpdateTicket(Guid TicketID, Guid UserID, string title = null, string description = null, string assignTo = null, string statusName = null, List<string> tag = null)
        {
            var ticket = await _context.tickets.Where(t => t.TicketID == TicketID).FirstOrDefaultAsync();
            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            var oldTitle = ticket.Title;
            var oldDescription = ticket.Description;
            var oldAssignTo = ticket.AssignTo;
            var oldStatus = ticket.Status;


            if (title != null && ticket.Title != title)
            {
                ticket.Title = title;
                var changeType = "Update title";
                await StoreInTicketHistory(ticket.TicketID, UserID, changeType, oldTitle, title);
            }


            if (description != null && ticket.Description != description)
            {
                ticket.Description = description;
                var changeType = "Update Description";
                await StoreInTicketHistory(ticket.TicketID, UserID, changeType, oldDescription, description);
            }


            if (assignTo != null && ticket.AssignTo != assignTo)
            {
                ticket.AssignTo = assignTo;
                var changeType = "Update Assignee";
                await StoreInTicketHistory(ticket.TicketID, UserID, changeType, oldAssignTo, assignTo);
            }


            if (statusName != null && ticket.Status != statusName)
            {
                _statusservice.SetStatus(ticket.TicketID, statusName);
                var statusValue = await _context.statushistory
                    .Where(p => p.TicketID == ticket.TicketID)
                    .OrderByDescending(p => p.TimeStamp)
                    .Select(p => p.StatusValue)
                    .FirstOrDefaultAsync();
                ticket.Status = statusValue;
                var changeType = "Update Status";
                await StoreInTicketHistory(ticket.TicketID, UserID, changeType, oldStatus, statusValue);
            }


            if (tag != null)
            {
                var tagAdded = _tagServices.AddTagtoticket(ticket.TicketID, tag);
                foreach (var tag1 in tagAdded)
                {
                    ticket.tags.Add(tag1);
                }
                var changeType = "Update Tags";
                await StoreInTicketHistory(ticket.TicketID, UserID, changeType, null, string.Join(",", tag));
            }

            await _context.SaveChangesAsync();
            return ticket;
        }


    }
}

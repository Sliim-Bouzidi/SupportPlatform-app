using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.CategoryServices;
using Support_Ticket_System.Services.PriorityServices;
using Support_Ticket_System.Services.severity_services;
using Support_Ticket_System.Services.status_services;
using Support_Ticket_System.Services.Tagservices;
using Support_Ticket_System.Services.TicketTypeServices;

namespace Support_Ticket_System.Services.ticketservices
{
    public class ticketservices : ITicketService
    {

        private readonly Datacontext _context;
        private readonly IStatusservice _statusservice;
        private readonly IPriorityServices _priorityServices;
        private readonly IseverityServices _severityServices;
        private readonly ITagServices _tagServices;
        private readonly ICategoryServices _categoryservices;
        private readonly ITicketTypeServices _ticketTypeServices;
        

        public ticketservices(Datacontext context, IStatusservice statusservices, IPriorityServices priorityservices, IseverityServices severityservices, ITagServices tagservices , ICategoryServices categoryservices , ITicketTypeServices ticketTypeServices)
        {
            _context = context;
            _statusservice = statusservices;
            _priorityServices = priorityservices;
            _tagServices = tagservices;
            _severityServices = severityservices;
            _categoryservices = categoryservices;
            _ticketTypeServices = ticketTypeServices;
            
        }


        public async Task<Ticket> CreateTicket(string title, string description, string assignTo, string processFlowname, string username, string tenantname, string priorityname, string severityname, List<string> tag, string? DossierNumber, string? SalesOrderNumber, string? WorkingOrderNumber, string? AssistancePlanNumber, IFormFile? file)
        {
            var processFlow = await _context.processFlows.FirstOrDefaultAsync(p => p.ProcessFlowName == processFlowname);
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username);
            var tenant = await _context.tenants.FirstOrDefaultAsync(t => t.Name == tenantname);
            var priority = _priorityServices.SetPriority(priorityname);
            var severity = await _context.severities.FirstOrDefaultAsync(s => s.SeverityName == severityname);
            var statusNmae = "New";
            var dossiernumberatt = await _context.attributs.FirstOrDefaultAsync(a => a.Name == "DossierNumber");
            var SalesOrderNumberatt = await _context.attributs.FirstOrDefaultAsync(a => a.Name == "SalesOrderNumber");
            var WorkingOrderatt = await _context.attributs.FirstOrDefaultAsync(a => a.Name == "WorkingOrderNumber");
            var assistanceplanatt = await _context.attributs.FirstOrDefaultAsync(a => a.Name == "AssistancePlanNumber");

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
            switch (processFlowname)
            {
                case "Sales Invoice Creation":
                     var pfattribut1 = new ProcessFlowAttrribut
                    {
                        PFAttributID = Guid.NewGuid(),
                        processflow = processFlow,
                        attribut = dossiernumberatt,
                        Value = DossierNumber,
                        ticket = ticket
                    };
                    var pfattribut2 = new ProcessFlowAttrribut
                    {
                        PFAttributID = Guid.NewGuid(),
                        processflow = processFlow,
                        attribut = SalesOrderNumberatt,
                        Value = SalesOrderNumber,
                        ticket = ticket
                    };
                    _context.Add(pfattribut1);
                    _context.Add(pfattribut2);
                    break;

                case "Purchase Invoice Creation":
                    var pfattribut3 = new ProcessFlowAttrribut
                    {
                        PFAttributID = Guid.NewGuid(),
                        processflow = processFlow,
                        attribut = dossiernumberatt,
                        Value = DossierNumber,
                        ticket = ticket
                    };
                    _context.Add(pfattribut3);
                    var pfattribut4 = new ProcessFlowAttrribut
                    {
                        PFAttributID = Guid.NewGuid(),
                        processflow = processFlow,
                        attribut = WorkingOrderatt,
                        Value = WorkingOrderNumber,
                        ticket = ticket
                    };
                    _context.Add(pfattribut4);
                    var pfattribut5 = new ProcessFlowAttrribut
                    {
                        PFAttributID = Guid.NewGuid(),
                        processflow = processFlow,
                        attribut = assistanceplanatt,
                        Value = AssistancePlanNumber,
                        ticket = ticket
                    };
                    _context.Add(pfattribut5); 
                   
                    break;
                case "Mail Reader":
                    var attachment = new Attachment
                    {
                        Id = Guid.NewGuid(),
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        ticket = ticket
                    };
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);


                        var fileData = ms.ToArray();


                    }
                    _context.Add(attachment);
                    break;


                default:
                    break;
            }

            _context.tickets.Add(ticket);
            await _context.SaveChangesAsync();
            await StoreInTicketHistory(ticket.TicketID, ticket.UserID, "ticket Creation", null, null);

            _statusservice.SetStatus(ticket.TicketID, statusNmae);
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

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(Guid UserID, string TenantName)
        {
            var user = await _context.users.Where(u => u.UserID == UserID).FirstOrDefaultAsync();
            var roles = _context.UserRoles.Where(r => r.User == user).Select(r => r.RoleValue).ToList();
            if (roles.Contains("Admin"))
            {
                return await _context.tickets
                    .Where(t => t.tenant.Name == TenantName)
                    .Include(t => t.tags)
                        .ThenInclude(ti => ti.tag)
                    .Include(t => t.categories)
                        .ThenInclude(c => c.category)
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
                        CreatedDate = t.CreatedDate,
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
                        tags = t.tags,
                        categories = t.categories,

                    })
                     .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();
            }
            if (roles.Contains("AppUser"))
            {
                return await _context.tickets
                    .Where(t => t.Status == "new" && t.user.UserID == UserID)
                    .Where(t => t.tenant.Name == TenantName)
                    .Include(t => t.tags)
                        .ThenInclude(ti => ti.tag)
                    .Include(t => t.categories)
                        .ThenInclude(c => c.category)
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
                        tags = t.tags,
                        categories = t.categories,
                    })
                     .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();
            }
            if (roles.Contains("AppSupportL1"))
            {
                return await _context.tickets
                    .Where(t => t.Status == "new")
                    .Where(t => t.tenant.Name == TenantName)
                    .Include(t => t.tags)
                        .ThenInclude(ti => ti.tag)
                    .Include(t => t.categories)
                        .ThenInclude(c => c.category)
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
                        tags = t.tags,
                        categories = t.categories,
                    })
                     .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();
            }
            if (roles.Contains("AppSupportL2"))
            {
                return await _context.tickets
                    .Where(t => t.Status == "Transferred to Level 2")
                    .Where(t => t.tenant.Name == TenantName)
                    .Include(t => t.tags)
                        .ThenInclude(ti => ti.tag)
                    .Include(t => t.categories)
                        .ThenInclude(c => c.category)
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
                        tags = t.tags,
                        categories = t.categories,
                    })
                     .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();
            }
            if (roles.Contains("AppSupportL3"))
            {
                return await _context.tickets
                    .Where(t => t.Status == "Transferred to Level 3")
                    .Where(t => t.tenant.Name == TenantName)
                    .Include(t => t.tags)
                        .ThenInclude(ti => ti.tag)
                    .Include(t => t.categories)
                        .ThenInclude(c => c.category)
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
                        tags = t.tags,
                        categories = t.categories,
                    })
                    .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();

            }
            return null;

        }


        


        //public async Task<IEnumerable<TicketHistory>> GetTicketHistory(Guid TicketID)
        //{
        //    var tickethistory = await _context.ticketHistories.Where(t=>t.TicketID == TicketID)
        //        .Include(t=>t.user.Username)
        //        .ToListAsync();
        //    return tickethistory;
        //}
        public async Task<IEnumerable<string>> GetTicketHistoryMessages(Guid TicketID)
        {
            var ticketHistoryMessages = await _context.ticketHistories
                .Where(t => t.TicketID == TicketID)
                .Include(t => t.user)
                .OrderByDescending(th => th.TimeStamp)
                .Select(th => th.changeType == "update status" ?
                    $"{th.user.Username} has changed the status from {th.OldValue} to {th.NewValue}" :
                    th.changeType == "comment added" ?
                    $" {th.user.Username} has added a comment" :
                    th.changeType == "ticket Creation" ?
                    $"The ticket was created at {th.TimeStamp}" :
                    th.changeType == "Update title" ?
                    $" {th.user.Username} has changed the title from {th.OldValue} to {th.NewValue}" :
                    th.changeType == "Update Description" ?
                    $" {th.user.Username} has changed the description at {th.TimeStamp}" :
                    th.changeType == "Update Assignee" ?
                    $"{th.user.Username} has assinged the ticket to {th.NewValue} at  {th.TimeStamp}":
                    $"Unhandled change type: {th.changeType}"



                )
                .ToListAsync();

            return ticketHistoryMessages;
        }


        public async Task<bool> StoreInTicketHistory(Guid ticketID, Guid? userID , string changetype , string oldvalue = null , string newvalue = null)
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
                                     .Include(t => t.processFlow)
                                      .ThenInclude(pf => pf.PFAttributs)
                                        .ThenInclude(pfa => pfa.attribut)
                                .Include(t => t.categories)
                                    .ThenInclude(cat => cat.category)

                                .Select(t => new Ticket
                                {
                                    TicketID = t.TicketID,
                                    Title = t.Title,
                                    Description = t.Description,
                                    CreatedDate = t.CreatedDate,
                                    UpdatedDate = t.UpdatedDate,
                                    AssignTo = t.AssignTo,


                                    processFlow = new ProcessFlow
                                    {
                                        ProcessFlowName = t.processFlow.ProcessFlowName,
                                        PFAttributs = t.processFlow.PFAttributs.Where(t => t.ticket.TicketID == TicketID).ToList(),
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

                                    tags = t.tags.Select(tag => new taggableitem
                                    {
                                        tagName = tag.tagName
                                    }).ToList(),

                                    categories = t.categories,
                                    TicketType = t.TicketType


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
                ticket.UpdatedDate = DateTime.UtcNow;

            
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



        public async Task<bool> RemoveTicket(Guid ticketID)
        {
            var ticket = await _context.tickets.FindAsync(ticketID);
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;
using System.Threading;

namespace CoreBotCLU.Utility
{
    public interface IUtilityServices
    {
       
        Task GetCustomQAResponseAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken);
        //Task<bool> CreateTicketAsync(ticket ticketDetails);
        //Task<bool> CreateTicket(string title, string description, string assignto, string statusName, string priorityName, string SeverityName, string tenantName, string ProcessFlowName);


    }
}

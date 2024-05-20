using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Builder.AI.QnA.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreBotCLU.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace CoreBotCLU.Utility
{
	public class UtilityService : IUtilityServices
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<UtilityService> _logger;
         

        //public SupportTicketSystemContext Context { get { return _context; } }


        public UtilityService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<UtilityService> logger)
        {
            _configuration = configuration;
			_httpClientFactory = httpClientFactory;
			_logger = logger;
			
           
            //_apiBaseUrl = apiBaseUrl;
        }

		public async Task GetCustomQAResponseAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
		{
			using var httpClient = _httpClientFactory.CreateClient();

			var customQuestionAnswering = CreateCustomQuestionAnsweringClient(httpClient);

			// Call Custom Question Answering service to get a response.
			_logger.LogInformation("Calling Custom Question Answering");
			var options = new QnAMakerOptions { Top = 1, EnablePreciseAnswer = bool.Parse(_configuration["CustomQnA:EnablePreciseAnswer"]) };
			var response = await customQuestionAnswering.GetAnswersAsync(stepContext.Context, options);

			if (response.Length > 0)
			{
				var activities = new List<IActivity>();

				// Create answer activity.
				var answerText = response[0].Answer;
				var answer = MessageFactory.Text(answerText, answerText);

				// Answer span text has precise answer.
				var preciseAnswerText = response[0].AnswerSpan?.Text;
				if (string.IsNullOrEmpty(preciseAnswerText))
				{
					activities.Add(answer);
				}
				else
				{
					// Create precise answer activity.
					var preciseAnswer = MessageFactory.Text(preciseAnswerText, preciseAnswerText);
					activities.Add(preciseAnswer);

					if (!bool.Parse(_configuration["CustomQnA:DisplayPreciseAnswerOnly"]))
					{
						// Add answer to the reply when it is configured.
						activities.Add(answer);
					}
				}

				await stepContext.Context.SendActivitiesAsync(activities.ToArray(), cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await stepContext.Context.SendActivityAsync(MessageFactory.Text("No answers were found.", "No answers were found."), cancellationToken);
			}
		}

		private CustomQuestionAnswering CreateCustomQuestionAnsweringClient(HttpClient httpClient)
		{
			// Create a new Custom Question Answering instance initialized with QnAMakerEndpoint.
			return new CustomQuestionAnswering(new QnAMakerEndpoint
			{
				KnowledgeBaseId = _configuration["CustomQnA:ProjectName"],
				EndpointKey = _configuration["CustomQnA:LanguageEndpointKey"],
				Host = _configuration["CustomQnA:LanguageEndpointHostName"],
				QnAServiceType = ServiceType.Language
			},
		   null,
		   httpClient);
		}
        //public async Task<bool> CreateTicketAsync(ticket ticketDetails)
        //{
        //    var apiBaseUrl = _configuration["ApiBaseUrl/ticket"]; ;
        //    var json = JsonConvert.SerializeObject(ticketDetails);
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await httpClient.PostAsync(apiBaseUrl, content);

        //    return response.IsSuccessStatusCode;
        //}
		//public async Task<bool> CreateTicket(string title, string description, string assignto, string statusName, string priorityName, string SeverityName, string tenantName, string ProcessFlowName)
		//{
			
           
  //          var processflow = _context.ProcessFlows.Where(p => p.ProcessFlowName == ProcessFlowName).FirstOrDefault();
           
  //          var severity = _context.Severities.Where(p => p.SeverityName == SeverityName).FirstOrDefault();
  //          var Status = _context.Statuses.Where(p => p.StatusName ==statusName).FirstOrDefault();
  //          var priority = _context.Priorities.Where(p => p.PriorityName == priorityName).FirstOrDefault();
  //          var tenant = _context.Tenants.Where(p => p.Name == tenantName).FirstOrDefault();
  //          var user = _context.Users.Where(u => u.Username == "Bot").FirstOrDefault();

  //          Tickets ticket = new Tickets
  //          {
  //              Title = title,
  //              Description = description,
  //              Status = statusName,
  //              ProcessFlow = processflow,
  //              Severity = severity,
  //              User = user,
  //              Priority = priority,
  //              Tenant = tenant,
  //              AssignTo = assignto,
  //          };
  //          _context.Tickets.Add(ticket);
  //          _context.SaveChanges();
		//	return true;
  //      }

    }
}

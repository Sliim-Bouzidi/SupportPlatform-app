

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema.Teams;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.BotBuilderSamples.Dialogs;
using Microsoft.Bot.Schema;
using System.Net.Sockets;
using Microsoft.BotBuilderSamples;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using AdaptiveCards;

using System.Linq;

using QnABotWithMSI.Models;



namespace QnABotWithMSI.Dialogs

{
    public class TicketCreation : CancelAndHelpDialog
    {
        
        public TicketCreation() : base(nameof(TicketCreation))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new DateResolverDialog());
            AddDialog(new AttachmentPrompt(nameof(AttachmentPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
           {    TitleStepAsync,
                DescriptionStepAsync,
                AssignToStepAsync,
                StatusStepAsync,
                ProcessFlowStepAsync,
                TenantNameStepAsync,
                PriorityNameStepAsync,
                SeverityNameStepAsync,
                TagsStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,

            }));
            



            InitialDialogId = nameof(WaterfallDialog);
        }









        private async Task<DialogTurnResult> TitleStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;

            if (TicketDetails.Title == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the title of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.Title, cancellationToken);
        }

        private async Task<DialogTurnResult> DescriptionStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.Title = (string)stepContext.Result;
            if (TicketDetails.Description == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the description of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.Description, cancellationToken);
        }
        private async Task<DialogTurnResult> AssignToStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.Description = (string)stepContext.Result;

            if (TicketDetails.AssignTo == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the name of the assignee ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.AssignTo, cancellationToken);
        }
        private async Task<DialogTurnResult> StatusStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.AssignTo = (string)stepContext.Result;

            if (TicketDetails.StatusName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the status of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.StatusName, cancellationToken);
        }

        private async Task<DialogTurnResult> ProcessFlowStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.StatusName = (string)stepContext.Result;

            if (TicketDetails.ProcessFlowName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the ProcessFlow of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.ProcessFlowName, cancellationToken);
        }


        private async Task<DialogTurnResult> TenantNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.ProcessFlowName = (string)stepContext.Result;
            if (TicketDetails.TenantName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the tenant of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.TenantName, cancellationToken);
        }
        private async Task<DialogTurnResult> PriorityNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.TenantName = (string)stepContext.Result;
            if (TicketDetails.PriorityName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the priority of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.PriorityName, cancellationToken);
        }

        private async Task<DialogTurnResult> SeverityNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.PriorityName = (string)stepContext.Result;
            if (TicketDetails.SeverityName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
                {
                    Prompt = MessageFactory.Text("please enter the severity of the ticket that you want to submit ")
                }, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.SeverityName, cancellationToken);
        }
        private async Task<DialogTurnResult> TagsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.SeverityName = (string)stepContext.Result;
            if (TicketDetails.CreationDate == null)
            {
                return await stepContext.BeginDialogAsync(nameof(DateResolverDialog), TicketDetails.CreationDate, cancellationToken);
            }
            return await stepContext.NextAsync(TicketDetails.CreationDate, cancellationToken);
        }
        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var TicketDetails = (Ticket)stepContext.Options;
            TicketDetails.CreationDate = (string)stepContext.Result;
           
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Text = "TICKET DETAILS ",
                        Weight = AdaptiveTextWeight.Bolder,
                        Size = AdaptiveTextSize.Medium,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"Title : {TicketDetails.Title}",
                        Size = AdaptiveTextSize.Medium,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"Description : {TicketDetails.Description}",
                        Size = AdaptiveTextSize.Medium,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = $"Priority : {TicketDetails.PriorityName}",
                        Size = AdaptiveTextSize.Medium,
                    },
                     new AdaptiveTextBlock
                    {
                        Text = $"Tenant : {TicketDetails.TenantName}",
                        Size = AdaptiveTextSize.Medium,
                    },
                      new AdaptiveTextBlock
                    {
                        Text = $"Severity : {TicketDetails.SeverityName}",
                        Size = AdaptiveTextSize.Medium,
                    },
                       new AdaptiveTextBlock
                    {
                        Text = $"Date : {TicketDetails.CreationDate}",
                        Size = AdaptiveTextSize.Medium,
                    },

                },
                BackgroundImage = new AdaptiveBackgroundImage
                {
                    Url = new Uri("https://th.bing.com/th/id/R.7903d6fd0397080d0a98a32f807ae615?rik=HKwL0PdyG1lyRA&riu=http%3a%2f%2fwww.solidbackgrounds.com%2fimages%2f2880x1800%2f2880x1800-light-sky-blue-solid-color-background.jpg&ehk=Gm141oxIDKzlHNSvQvPwdQZUoD8UZBc7%2fXMYhC4TmRs%3d&risl=&pid=ImgRaw&r=0"), // Replace with your image URL
                    FillMode = AdaptiveImageFillMode.Cover,
                },
            };

            var attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = JObject.FromObject(card),
            };
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            return await stepContext.NextAsync(null,cancellationToken);
        }

        //    return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        //}
        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            //if ((bool)stepContext.Result)
            //{
            //    var ticketDetails = (ticket)stepContext.Options;

            //    return await stepContext.EndDialogAsync(ticketDetails, cancellationToken);
            //}
            var ticketDetails = (Ticket)stepContext.Options;
            return await stepContext.EndDialogAsync(null, cancellationToken);

        }
    }
}


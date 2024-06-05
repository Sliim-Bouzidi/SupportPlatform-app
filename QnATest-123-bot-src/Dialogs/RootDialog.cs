// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.BotBuilderSamples.Clu;
using Microsoft.Extensions.Configuration;
using QnABotWithMSI.Dialogs;
using QnABotWithMSI.Models;

namespace Microsoft.BotBuilderSamples.Dialogs
{
    /// <summary>
    /// This is an example root dialog. Replace this with your applications.
    /// </summary>
    public class RootDialog : ComponentDialog
    {
        /// <summary>
        /// QnA Maker initial dialog
        /// </summary>
        private const string InitialDialog = "initial-dialog";
        private FlightBookingRecognizer _cluRecognizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootDialog"/> class.
        /// </summary>
        /// <param name="services">Bot Services.</param>
        public RootDialog(IBotServices services, IConfiguration configuration, FlightBookingRecognizer cluRecognizer)
            : base("root")
        {
            AddDialog(new QnAMakerBaseDialog(services, configuration));
            AddDialog(new TicketCreation());

            AddDialog(new WaterfallDialog(InitialDialog)
               .AddStep(InitialStepAsync));
            // The initial child Dialog to run.
            InitialDialogId = InitialDialog;
            _cluRecognizer = cluRecognizer;

        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Call CLU and gather any potential booking details. (Note the TurnContext has the response to the prompt.)
            var cluResult = await _cluRecognizer.RecognizeAsync<FlightBooking>(stepContext.Context, cancellationToken);

            if (cluResult.GetTopIntent().score > 0.8)
            {
                switch (cluResult.GetTopIntent().intent)
                {
                    case FlightBooking.Intent.CreateTicket:
                        var TicketDetails = new Ticket();
                        return await stepContext.BeginDialogAsync(nameof(TicketCreation), TicketDetails, cancellationToken);

                    case FlightBooking.Intent.DeleteTicket:
                        var deleteTicketMessage = "Ticket has been deleted";
                        var deleteTicketResponse = MessageFactory.Text(deleteTicketMessage, deleteTicketMessage, InputHints.IgnoringInput);
                        await stepContext.Context.SendActivityAsync(deleteTicketResponse, cancellationToken);
                        // Set a flag indicating CLU responded
                        stepContext.Values["CLUResponded"] = true;
                        // End the dialog since CLU responded
                        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
                }
            }

            // No high-confidence response from CLU, proceed with QnA Maker dialog
            return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
        }

    }
}
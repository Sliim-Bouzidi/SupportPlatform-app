// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder;
using Microsoft.BotBuilderSamples.Clu;
using Newtonsoft.Json;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// An <see cref="IRecognizerConvert"/> implementation that provides helper methods and properties to interact with
    /// the CLU recognizer results.
    /// </summary>
    public class CluTicketBot : IRecognizerConvert
    {
        public enum Intent
        {
            CreateTicket,
            DeleteTicket,
            None
        }

        public string Text { get; set; }

        public string AlteredText { get; set; }

        public Dictionary<Intent, IntentScore> Intents { get; set; }

        public CluEntities Entities { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public void Convert(dynamic result)
        {
            var jsonResult = JsonConvert.SerializeObject(result, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            var app = JsonConvert.DeserializeObject<CluTicketBot>(jsonResult);

            Text = app.Text;
            AlteredText = app.AlteredText;
            Intents = app.Intents;
            Entities = app.Entities;
            Properties = app.Properties;
        }

        public (Intent intent, double score) GetTopIntent()
        {
            var maxIntent = Intent.None;
            var max = 0.0;
            foreach (var entry in Intents)
            {
                if (entry.Value.Score > max)
                {
                    maxIntent = entry.Key;
                    max = entry.Value.Score.Value;
                }
            }

            return (maxIntent, max);
        }

        public class CluEntities
        {
            public CluEntity[] Entities;

            public CluEntity[] GetTitleList() => Entities.Where(e => e.Category == "Title").ToArray();

            public CluEntity[] GetDescriptionList() => Entities.Where(e => e.Category == "Description").ToArray();

            public CluEntity[] GetPriorityList() => Entities.Where(e => e.Category == "Priority").ToArray();

            public string GetTitle() => GetTitleList().FirstOrDefault()?.Text;

            public string GetDescription() => GetDescriptionList().FirstOrDefault()?.Text;

            public string GetPriority() => GetPriorityList().FirstOrDefault()?.Text;
        }
    }
}

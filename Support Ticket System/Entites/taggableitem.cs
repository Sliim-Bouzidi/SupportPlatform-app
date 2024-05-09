using System.Text.Json.Serialization;

namespace Support_Ticket_System.Entites
{
    public class taggableitem
    {

        [JsonIgnore]
        public Guid TicketID { get; set; }

        [JsonIgnore]
        public Guid TagID { get; set; }


        [JsonIgnore]
        public Ticket ticket { get; set; }


        [JsonIgnore]
        public Tag tag { get; set; }

        public string tagName { get; set; }
    }
}

namespace Support_Ticket_System.Entites
{
    public class Attachment
    {

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public Ticket ticket { get; set; }
    }   
}

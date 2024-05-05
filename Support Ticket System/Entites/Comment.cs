namespace Support_Ticket_System.Entites
{
    public class Comment
    {
        public Guid CommentID { get; set; }
        public string text { get; set; }
        public DateTime Date { get; set; }
        public User user { get; set; }
        public Ticket ticket { get; set; }

    

    }
}

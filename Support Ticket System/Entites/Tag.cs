namespace Support_Ticket_System.Entites
{
    public class Tag
    {
        public Guid TagID { get; set; }
        public string TagName { get; set; }
        public ICollection<taggableitem> taggableitems { get; set; }
    }
}

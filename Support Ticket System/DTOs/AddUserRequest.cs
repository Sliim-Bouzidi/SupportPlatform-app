namespace Support_Ticket_System.DTOs
{
    public class AddUserRequest
    {
        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}

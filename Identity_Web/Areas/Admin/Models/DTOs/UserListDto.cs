namespace Identity_Web.Areas.Admin.Models.DTOs
{
    public class UserListDto
    {
        public string Id { get; set; }=string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public int AccessFaildCount { get; set; }
    }
}

namespace DotnetApi {

    public partial class Users {

        public int UserId { get; set; }
        public String? FirstName { get; set; } = "";
        public String? LastName { get; set; } = "";
        public String? Email { get; set; } = "";
        public String? Gender { get; set; } = "";
        public bool Active { get; set; }

    }
}
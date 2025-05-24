namespace CRMSystem.Web.ViewModels
{
    public class UserDto
    {
        public class CreateUserDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class UpdateUserDto
        {
            public int Id { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}

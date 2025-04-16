namespace FitnessApp.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Profile? Profile { get; set; } // Relacionamento 1:1 com Profile
    }
}

namespace FitnessApp.Server.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string activityLevel { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;

        public int UserId { get; set; } // Chave estrangeira
        public User User { get; set; } = null!; // Relacionamento 1:1 com User
    }
}

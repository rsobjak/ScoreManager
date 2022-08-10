namespace ScoreManager.Entities
{
    public class Candidate : EntityBase
    {

        public string Name { get; set; }
        public string Document { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public virtual User? User { get; set; }
    }
}
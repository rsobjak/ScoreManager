namespace ScoreManager.Entities
{
    public class Candidate : Entity
    {
        public Candidate(string name, string document)
        {
            this.Name = name;
            this.Document = document;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
    }
}
namespace ScoreManager.Entities
{
    public class Perform : Entity
    {
        public Category? Category { get; set; }
        public Candidate? PrimaryCandidate { get; set; }
        public Candidate? SecondaryCandidate { get; set; }
        public decimal? Score { get; set; }
    }
}
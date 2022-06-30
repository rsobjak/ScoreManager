namespace ScoreManager.Entities
{
    public class Perform : Entity
    {
        public virtual Category? Category { get; set; }
        public virtual Candidate? PrimaryCandidate { get; set; }
        public virtual Candidate? SecondaryCandidate { get; set; }
        public decimal? Score { get; set; }
    }
}
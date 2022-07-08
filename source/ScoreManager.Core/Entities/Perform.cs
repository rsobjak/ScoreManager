namespace ScoreManager.Entities
{
    public class Perform : EntityBase
    {
        public virtual Category? Category { get; set; }
        public virtual Candidate? PrimaryCandidate { get; set; }
        public virtual Candidate? SecondaryCandidate { get; set; }
        public decimal? Score { get; set; }
        public int? Order { get; set; }
        public string SongTitle { get; set; }
        public string? SongLyrics { get; set; }
        public string? SongInterpreter { get; set; }
        public virtual IEnumerable<Rating> Ratings { get; set; }
        public PerformStatus Status { get; set; }
    }

    public enum PerformStatus
    {
        NotConfirmed = 0,
        Confirmed = 1,
        Performing = 2,
        Performed = 3,
        Canceled = 4
    }
}
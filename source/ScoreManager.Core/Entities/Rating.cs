namespace ScoreManager.Entities
{
    public class Rating : EntityBase
    {
        public User User { get; set; }
        public Perform Perform { get; set; }
        public decimal? Rate { get; set; }
    }
}
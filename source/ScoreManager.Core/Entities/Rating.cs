namespace ScoreManager.Entities
{
    public class Rating : Entity
    {
        public User User { get; set; }
        public Perform Subscription { get; set; }
        public int? Rate { get; set; }
    }
}
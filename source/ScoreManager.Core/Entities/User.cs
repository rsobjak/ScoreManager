namespace ScoreManager.Entities
{
    public class User : EntityBase
    {
        public string Login { get; set; }
        public bool IsRater { get; set; }
    }
}
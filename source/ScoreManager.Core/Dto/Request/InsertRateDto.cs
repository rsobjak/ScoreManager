namespace ScoreManager.Dto.Request
{
    public class InsertRateDto
    {
        public int UserId { get; set; }
        public int PerformId { get; set; }
        public decimal? Rate { get; set; }
    }
}
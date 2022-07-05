namespace ScoreManager.Dto.Request
{
    public class InsertPerformDto
    {
        public int InsertUserId { get; set; }
        public int CategoryId { get; set; }
        public int PrimaryCandidateId { get; set; }
        public int SecondaryCandidateId { get; set; }
    }
}
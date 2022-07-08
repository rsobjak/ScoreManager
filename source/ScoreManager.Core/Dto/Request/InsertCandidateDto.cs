namespace ScoreManager.Dto.Request
{
    public class InsertCandidateDto
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
    }
}
namespace ScoreManager.Dto.Request
{
    public class InsertPerformDto
    {
        public int CategoryId { get; set; }
        public int PrimaryCandidateId { get; set; }
        public int SecondaryCandidateId { get; set; }

        public string SongTitle { get; set; }
        public string? SongLyrics { get; set; }
        public string? SongInterpreter { get; set; }
    }
}
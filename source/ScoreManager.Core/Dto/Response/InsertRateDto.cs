using ScoreManager.Entities;

namespace ScoreManager.Dto.Response
{
    public class PerformDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string PrimaryCandidateName { get; set; }
        public string SecondaryCandidateName { get; set; }
        public string City { get; set; }
        public string SongTitle { get; set; }
        public string? SongLyrics { get; set; }
        public string? SongInterpreter { get; set; }
        public PerformStatus Status { get; set; }
    }
}
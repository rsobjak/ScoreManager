namespace ScoreManager.Dto.Request
{
    public class InsertCategoryDto
    {
        public InsertCategoryDto(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
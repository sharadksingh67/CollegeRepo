namespace SmartCollege.Services.College.Models.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}

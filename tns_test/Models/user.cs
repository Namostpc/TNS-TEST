namespace tns_test.Models
{
    public class User
    {
        public required int userId { get; set; }
        public required string fristName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public string create_at { get; set; }
        public string update_at { get; set; }
        public required int departmentId { get; set; }
        public required Department Department { get; set; }
    }
}


namespace tns_test.Models
{
    public class Department
    {
        public required int departmentId { get; set; }
        public required string departmentName { get; set; }
        public string create_at {get; set;}
        public string update_at {get; set;}
        public ICollection<User> Users { get; set; }
    }
}



using System.Text.Json.Serialization;

namespace tns_test.Models
{
    public class Department
    {
        public  int departmentid { get; set; }
        public required string departmentname { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }


    public class CreateDepartmentRequest
    {
        public string department {get; set;}
    }
    
    public class UpdateDepartmentRequest
    {
        public string department {get; set;}
        public string newdepartmentname {get; set;}
    }
}



using System.Text.Json.Serialization;

namespace tns_test.Models
{
    public class User
    {
        public  int userid { get; set; }
        public required string firstname { get; set; }
        public required string lastname { get; set; }
        public required string email { get; set; }
        public required int departmentid { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }


    public class CreateUserRequest
    {
        public string firstname {get; set;}
        public string lastname {get; set;}
        public string email {get; set;}
        public string department {get; set;}
    }
}


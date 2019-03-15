using SQLite;

namespace VPIIntercom.Models
{
    public class SaveLoginResponse
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string server_address { get; set; }
        public string building_name { get; set; }
    }
}

using Food_Users_Api.Models;
using System.Text.Json.Serialization;

namespace Api_Food.Models
{
    public class FoodUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        [JsonIgnore]
        public Food Food { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}

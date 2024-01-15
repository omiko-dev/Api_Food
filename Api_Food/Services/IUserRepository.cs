using Food_Users_Api.Models;

namespace Api_Food.Services
{
    public interface IUserRepository
    {

        public User GetByEmail(string Email);

        public Task<Food> AddFood(Food food, string Email);

        public Task<Food> AddFood(string Email, Food food);

        public Task<List<Food>> GetFood(string Email);

        public Task<List<Food>> DeleteFood(string Email, Food food);

    }
}

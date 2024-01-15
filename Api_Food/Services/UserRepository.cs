using Api_Food.Models;
using Food_Users_Api;
using Food_Users_Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api_Food.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext userDB;

        public UserRepository(UserDBContext userDB)
        {
            this.userDB = userDB;
        }

        public async Task<Food> AddFood(Food food, string Email)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
                return null!;

            
            await userDB.Foods.AddAsync(
                food
            );
            await userDB.SaveChangesAsync();

            return food;

        }

        public User GetByEmail(string Email)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
                return null!;

            return user;
        }


        public User SetFood(string Email, Food food)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
                return null!;


            return user;
        }


        public async Task<Food> AddFood(string Email, Food food)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
                return null!;

            var userFood = new FoodUser()
            {
                Food = food,
                User = user
            };

            await userDB.AddAsync(userFood);
            await userDB.SaveChangesAsync();
            return food;
        }

        public async Task<List<Food>> GetFood(string Email)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
                return null!;

            var foods = userDB.FoodUsers.Where(u => u.UserId == user.Id).Select(c => c.Food).ToList();

            return foods;
        }

        public async Task<List<Food>> DeleteFood(string Email, Food food)
        {
            var user = userDB.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
                return null!;


            userDB.Remove(food);
            await userDB.SaveChangesAsync();
            var foods = userDB.FoodUsers.Where(u => u.UserId == user.Id).Select(c => c.Food).ToList();
            return foods;
        }

    }
}

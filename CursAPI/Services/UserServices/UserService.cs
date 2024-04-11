using CursAPI.Enities;
using CursAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CursAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        public async Task<bool> AddUsers()
        {
            using var context = new Context();
            for (int i = 0; i < 1000; i++)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = GetRandomStr(),
                    Password = GetRandomStr(),
                    Email = GetRandomStr(),
                    Sex = GetRandomInt(0, 1),
                    BirthDate = GetRandomDate(),
                };

                var client = new Client
                {
                    ClientId = Guid.NewGuid(),
                    CountSell = GetRandomInt(0, 500),
                    CountBuy = GetRandomInt(0, 500),
                    UserId = user.Id,
                    User = user
                };
                context.Users.Add(user);
                context.Clients.Add(client);
            }
            
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddUser(UserModel userModel)
        {
            using var context = new Context();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userModel.Name,
                Password = userModel.Password,
                Email = userModel.Email,
                Sex = userModel.Sex,
                BirthDate = userModel.BirthDate
            };

            context.Users.Add(user);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> GetAllUsers()
        {
            using var context = new Context();

            return await context.Users
                .Include(u => u.Clients)
                .ToListAsync();
        }

        #region Вспомогательные методы

        private static string GetRandomStr()
        {
            int x = GetRandomInt(4, 10);

            string str = "";
            var r = new Random();
            while (str.Length < x)
            {
                char c = (char)r.Next(33, 125);
                if (char.IsLetterOrDigit(c))
                    str += c;
            }
            return str;
        }

        private static int GetRandomInt(int from, int to)
        {
            var dig = new Random();
            int x = dig.Next(from, to);
            return x;
        }

        private static DateTime GetRandomDate()
        {
            var gen = new Random();
            var start = new DateTime(1995, 1, 1);
            int range = (new DateTime(2006, 1, 1) - start).Days;
            return start.AddDays(gen.Next(range));
        }

        #endregion
    }
}

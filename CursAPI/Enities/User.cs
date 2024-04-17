using Microsoft.AspNetCore.Identity;

namespace CursAPI.Enities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Clients = [];
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }

        //Поля для токена
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}

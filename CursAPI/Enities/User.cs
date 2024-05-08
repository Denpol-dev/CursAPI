using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }

        //Поля для токена
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}

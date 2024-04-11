namespace CursAPI.Enities
{
    public class User
    {
        public User()
        {
            Clients = [];
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}

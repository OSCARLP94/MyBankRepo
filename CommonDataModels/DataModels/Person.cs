
namespace CommonDataModels.DataModels
{
    public class Person
    {
        public string Id { get; set; }

        /// <summary>
        /// # documento unico
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string Names { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        public string Surnames { get; set; }

        /// <summary>
        /// Fecha nacimiento
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sex { get; set; }
    }
}

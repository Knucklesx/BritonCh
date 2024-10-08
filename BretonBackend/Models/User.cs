using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace BretonBackend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Senha { get; set; }
        private string _role = "user";
        public required string Role
        {
            get => _role;
            set => _role = string.IsNullOrWhiteSpace(value) ? "user" : value;
        }

        public User() { }
    }
}

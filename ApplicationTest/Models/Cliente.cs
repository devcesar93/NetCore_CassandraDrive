using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationTest.Models
{
    public class Cliente
    {
        [Key]
        public string Id { get; set; }

        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }
    }
}
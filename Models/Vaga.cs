using System;
using System.ComponentModel.DataAnnotations;

namespace Empregare.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string Localizacao { get; set; }
        public string Salario { get; set; }

        [DisplayFormat(DataFormatString = "{hh:mm dd/MM/yyyy}")]
        public DateTime Data { get; set; }

    }
}

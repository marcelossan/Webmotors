using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_Webmotors.Models
{
    [Table("tb_AnuncioWebmotors")]
    public class AnuncioWebmotors
    {
        [Column("ID")] [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("marca")]
        public string Marca { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("modelo")]
        public string Modelo { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("versao")]
        public string versao { get; set; }

        [Required]
        [Column("ano")]
        public int Ano { get; set; }

        [Required]
        [Column("quilometragem")]
        public int Quilometragem { get; set; }

        [Required]
        [Column("observacao")]
        public string Observacao { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Files.EntityFrameworkCore.Extensions;

namespace Imagem.Models
{
    public class Img 
    {
        [Column("ImgId")]
        [Display(Name = "Cód. Imagem")]
        public int ImgId { get; set; }

        [Column("DescricaoImg")]
        [Display(Name = "Descrição Imagem")]
        public string DescricaoImg { get; set; } = string.Empty;

        [Column("ImagemTeste")]
        [Display(Name = "Img")]
        public byte[]? ImagemTeste { get; set; }

        [NotMapped]
        public string testeExibicao { get; set; } = string.Empty;
    }
}

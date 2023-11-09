using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Imagem.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Img> Img { get; set; }
    }
}

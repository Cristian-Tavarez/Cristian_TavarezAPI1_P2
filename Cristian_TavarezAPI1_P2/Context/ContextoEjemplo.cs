using Cristian_TavarezAPI1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cristian_TavarezAPI1_P2.Context;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<ViajesEspaciales> ViajesEspaciales { get; set; }
}

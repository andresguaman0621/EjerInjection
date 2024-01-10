using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EjerInjection.Models;

namespace EjerInjection.Data
{
    public class EjerInjectionContext : DbContext
    {
        public EjerInjectionContext (DbContextOptions<EjerInjectionContext> options)
            : base(options)
        {
        }

        public DbSet<EjerInjection.Models.RegistroDatos> RegistroDatos { get; set; } = default!;
    }
}

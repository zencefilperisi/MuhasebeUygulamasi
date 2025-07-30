using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FormGiris.cs
{
    public class MuhasebeModel : DbContext
    {
        public MuhasebeModel()
        : base("name=MuhasebeDBEntities") // App.config içindeki connection string adı
        {
        }

        public DbSet<StokKart> StokKart { get; set; }
    }
}

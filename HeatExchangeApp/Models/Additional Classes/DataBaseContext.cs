using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace HeatExchangeApp.Models.Additional_Classes
{
    public class DataBaseContext: DbContext
    {
        public DbSet<InitData> InitData { get; set; }
        public DbSet<User> User { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
    }
}

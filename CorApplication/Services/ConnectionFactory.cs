using CorApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorApplication.Services
{
    public class ConnectionFactory
    {
        public static ApplicationContext CreateContext(IConfiguration configuration)
        {
            IConfiguration _configuration = configuration;
            var strConn = _configuration["connectionString:BlogApplicationDB"].ToString();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(strConn);
            return new ApplicationContext(optionsBuilder.Options);            
        }
    }
}

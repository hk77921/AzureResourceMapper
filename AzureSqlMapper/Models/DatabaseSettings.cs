using System;
using System.Collections.Generic;
using System.Text;

namespace AzureSqlMapper.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

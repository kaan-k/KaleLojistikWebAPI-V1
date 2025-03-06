using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public class MongoDbSettings
    {
        public string BusinessUsersCollectionName { get; set; } = "Users";
        public string OrdersCollectionName { get; set; }
        public string ProductsCollectionName { get; set; }
    }
}

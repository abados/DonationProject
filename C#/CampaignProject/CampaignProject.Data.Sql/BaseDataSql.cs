using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class BaseDataSql
    {
        public BaseDataSql(Logger log) { Logger = log; }
        public Logger Logger;
    }
}

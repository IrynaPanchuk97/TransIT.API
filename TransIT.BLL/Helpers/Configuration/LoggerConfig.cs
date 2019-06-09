using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Helpers.Configuration
{
    enum Logger
    {
        Azure,
        FileSystem
    }
    static class LoggerConfig
    {
        public static readonly Logger logger = Logger.Azure;
    }
}

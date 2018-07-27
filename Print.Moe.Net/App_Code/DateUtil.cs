using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code
{
    public class DateUtil
    {
        public static long zeroTime(int day)
        {
                 
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
        public static long getTime
        {
            get
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds);
            }
            
        }
        public static long zeroTimes
        {
            get
            {
                DateTime time = DateTime.UtcNow;
                DateTime dt = new DateTime(time.Year, time.Month, time.Day,0,0,0);
                TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds);
            }

        }
    }
}
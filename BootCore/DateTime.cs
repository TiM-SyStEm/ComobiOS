using System;
using Date = Cosmos.HAL.RTC;

namespace ComobiOS.BootCore
{
    class DateTime
    {
        public static string STDview()
        {
            return Date.DayOfTheMonth + "." + Date.Month + ".20" + Date.Year + " " + Date.Hour + ":" + Date.Minute;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComobiOS.BootCore
{
    public enum CompUnits
    {
        BITE, KB, MB, GB
    }
    class Units
    {
        public static string Convert(int num, CompUnits units)
        {
            if (units == CompUnits.BITE)
            {
                if (num < 1024)
                    return num.ToString() + "B";
                else if (num < 1048576)
                    return (num / 1024).ToString() + "KB";
                else if (num >= 1048576)
                    return (num / 1024).ToString() + "MB";
                else if (num < 1073741824)
                    return (num / 1048576).ToString() + "GB";
            }
            else if (units == CompUnits.KB)
            {
                if (num < 1048576)
                    return (num / 1024).ToString() + "KB";
                else if (num >= 1048576)
                    return (num / 1024).ToString() + "MB";
                else if (num < 1073741824)
                    return (num / 1048576).ToString() + "GB";
            }
            else if (units == CompUnits.MB)
            {
                if (num >= 1048576)
                    return (num / 1024).ToString() + "MB";
                else if (num < 1073741824)
                    return (num / 1048576).ToString() + "GB";
            }
            else if (units == CompUnits.GB)
            {
                return num.ToString() + "GB";
            }
            return null;
        }
    }
}

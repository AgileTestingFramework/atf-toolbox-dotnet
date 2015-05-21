using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atf.toolbox
{
    public enum BrowserType
    {
        FIREFOX, IE, CHROME, SAFARI, OPERA, ANDROID, IPHONE
    }

    public static class AtfConstant
    {
        // Wait Times
        public static int WAIT_TIME_TINY = 5;
        public static int WAIT_TIME_EXTRA_SMALL = 10;
        public static int WAIT_TIME_SMALL = 15;
        public static int WAIT_TIME_MEDIUM = 20;
        public static int WAIT_TIME_LARGE = 30;
        public static int WAIT_TIME_EXTRA_LARGE = 60;
    }
}

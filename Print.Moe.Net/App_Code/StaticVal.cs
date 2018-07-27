using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code
{
    public class StaticVal
    {
        public const int DEFAULT_LIMIT = 1;
        public const int DEFAULT_DAYS = 300;
        public const String DEFAULT_AUTHOR="";
        public static ArrayList DEFAULT_TAGS= new ArrayList();
        public const int DEFAULT_MIN_WIDTH = 0;
        public const int DEFAULT_MAX_WIDTH = 99999;
        public const int DEFAULT_MAX_HEIGHT = 99999;
        public const int DEFAULT_MIN_HEIGHT = 0;
        public const int DEFAULT_PAGE = 1;
        public const int DEFAULT_STEP = 2000;



        public static class Order
        {
            public const int ORDER_BY_LIKES = 5;
            public const int ORDER_BY_RANDOM = 4;
            public const int ORDER_BY_CREATETIME = 3;
            public const int ORDER_BY_HEIGHT = 2;
            public const int ORDER_BY_WIDTH = 1;
            public const int ORDER_BY_score = 0;
        }
        public static class Sort
        {
            public const int desc = 1;
            public const int asc = 0;
        }

    }
}
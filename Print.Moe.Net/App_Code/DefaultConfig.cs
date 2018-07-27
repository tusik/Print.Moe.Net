using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code
{
    public class DefaultConfig
    {

            public int limit =StaticVal.DEFAULT_LIMIT;
            public int days = StaticVal.DEFAULT_DAYS;
            public String author = StaticVal.DEFAULT_AUTHOR;
            public ArrayList tags = StaticVal.DEFAULT_TAGS;
            public int w_min = StaticVal.DEFAULT_MIN_WIDTH;
            public int w_max = StaticVal.DEFAULT_MAX_WIDTH;
            public int h_max = StaticVal.DEFAULT_MAX_HEIGHT;
            public int h_min = StaticVal.DEFAULT_MIN_HEIGHT;
            public int order = StaticVal.Order.ORDER_BY_RANDOM;
            public int sort = StaticVal.Sort.desc;
            public long fileSize = 0;
            public long sTime = 0L;
            public long eTime = 0L;
            public int page = StaticVal.DEFAULT_PAGE;
            public string ext = "all";
            public int uid = 0;
        public string username = "";

        public DefaultConfig()
        {
        }

        public DefaultConfig(int limit, int days, string author, ArrayList tags, int w_min, int w_max, int h_max, int h_min, int order, int sort, long fileSize, long sTime, long eTime, int page, string ext, int uid, string username)
        {
            this.limit = limit;
            this.days = days;
            this.author = author;
            this.tags = tags;
            this.w_min = w_min;
            this.w_max = w_max;
            this.h_max = h_max;
            this.h_min = h_min;
            this.order = order;
            this.sort = sort;
            this.fileSize = fileSize;
            this.sTime = sTime;
            this.eTime = eTime;
            this.page = page;
            this.ext = ext;
            this.uid = uid;
            this.username = username;
        }


        public DefaultConfig Clone()
        {
            DefaultConfig c = new DefaultConfig(this.limit, this.days, this.author, this.tags, this.w_min, this.w_max, this.h_max, this.h_min, this.order, this.sort, this.fileSize, this.sTime, this.eTime, this.page, this.ext, this.uid, this.username);
            return c;
        }
    }
}
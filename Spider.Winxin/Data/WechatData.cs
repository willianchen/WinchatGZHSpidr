using Spider.Data;
using Spider.Weixin.Data.Model;
using Spider.Weixin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin.Data
{
    public class WechatData
    {
        public IDataBase database = new MySqlDataBaseFactory().Create();

        public int InsertProfile(List<ProfileListInfo> list, int accountId)
        {
            string sql = "";
            return 1;
        }

        public IEnumerable<WeChatAccountModel> GetAllAccount()
        {
            string sql = "select * from t_WechatAccount";
            SqlQuery sqlquery = new SqlQuery(sql);
            return database.QueryList<WeChatAccountModel>(sqlquery);
        }
    }
}

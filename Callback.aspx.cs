using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GooLuck_LineNotify.classData;
using isRock.LineNotify;

namespace GooLuck_LineNotify
{
    public partial class Callback : System.Web.UI.Page
    {
        private clsData clDB = new clsData();
        private string strConnString = ConfigurationManager.ConnectionStrings["sqlConnn"].ToString();
        private string callbackUrl = ConfigurationManager.AppSettings["callbackUrl"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //取得返回的code
            var code = Request.QueryString["code"];
            if (code == null)
            {
                Response.Write("沒有正確回應code");
                Response.End();
            }

            //測試
            //Response.Write("<br/> code : " + code);

            //從Code取回toke
            var token = Utility.GetToeknFromCode(code,
                "client_id",  //TODO:自己的 client_id
                "client_secret", //TODO:自己的 client_secret
                callbackUrl);
            //測試
            //Response.Write("<br/> token : " + token.access_token);
            //利用token發測試訊息
            Utility.SendNotify(token.access_token, "Thank you for joining us - " + System.DateTime.Now.ToString());

            //導入首頁，儲存token
            string query = "insert into dbo.tbToken (_token,_privilege,_createDate)values ('" + token.access_token + "','" + Session["privilege"] + "',CONVerT(FLOAT,getdate()+2))";
            string strError = "";
            int result;
            result = clDB.zd_IsertQuery(strConnString, query, CommandType.Text, ref strError);
            if (result == 1)
            {
                Session["userTokenID"] = token.access_token;
                Response.Redirect("frmLogin.aspx");
            }
            else
            {
                Response.Write("沒有正確儲存access_token");
                Response.End();
            }
        }
    }
}

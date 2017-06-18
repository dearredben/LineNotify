using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GooLuck_LineNotify.classData;
using isRock.LineNotify;

namespace GooLuck_LineNotify
{
    public partial class frmLogin : System.Web.UI.Page
    {
        private clsData clDB = new clsData();
        private string strConnString = ConfigurationManager.ConnectionStrings["sqlConnn"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["userTokenID"] != null)
                {
                    string token = Session["userTokenID"].ToString();
                    //string strHalf = token.Substring(token.Length / 2);
                    this.txb_token.Value = token.Replace(token.Substring(token.Length / 2), "***********");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string client_id = "";
            client_id = ConfigurationManager.AppSettings["Client_ID"].ToString();
            string callbackUrl = ConfigurationManager.AppSettings["callbackUrl"].ToString();

            var URL = "https://notify-bot.line.me/oauth/authorize?";
            URL += "response_type=code";
            URL += "&client_id=" + client_id;  //TODO:你的client_id
            URL += "&redirect_uri=" + callbackUrl;   //TODO:將此edirect url 填回 LineNotify後台設定
            URL += "&scope=notify";
            URL += "&state=testabcde";
            Response.Redirect(URL);
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            var ret = Utility.SendNotify(Session["userTokenID"].ToString(), this.txt_msg.Value);
            msg.InnerText = "send '{" + this.txt_msg.Value + "}'..." + ret.message;
        }

        protected void brnAll_Click(object sender, EventArgs e)
        {
            string query = "select _tosn,_token FROM dbo.tbToken ";
            string strError = "";
            DataSet ds;
            ds = clDB.zd_SelectQuery(strConnString, query, "tbToken", ref strError);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int i;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    var ret = Utility.SendNotify(ds.Tables[0].Rows[i]["_token"].ToString(), this.txt_Allmsg.Value);
                    msgAll.InnerText = "send '{" + this.txt_Allmsg.Value + "}'..." + ret.message + "[" + ds.Tables[0].Rows.Count.ToString() + " ]";
                }
            }
        }
    }
}
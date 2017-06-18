<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="GooLuck_LineNotify.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>GooLuck-LineNotify</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row" style="margin: 12px">
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Line Notify Tester
                        </div>
                        <div class="panel-body">
                            使用:

                            <ol>
                                <li>請點選[用戶註冊]，取得該登入用戶的發送訊息token</li>
                                <li>接著輸入訊息後，按下發送鈕即可測試訊息發送</li>
                                <li>beta功能測試中:全體發送訊息給註冊的用戶, </li>
                            </ol>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            測試
                        </div>
                        <div class="panel-body">
                            <asp:Button CssClass="btn btn-primary" ID="btnSubmit" Text="用戶註冊" runat="server" OnClick="btnSubmit_Click" />
                            <%--<button class="btn btn-primary" id="btnSubmit" type="button">用戶註冊</button>--%>
                            <br />
                            <br />
                            <div class="form-group">
                                <label>取回的token:</label>
                                <input runat="server" id="txb_token" class="form-control" />
                                <label>訊息:</label>
                                <input runat="server" id="txt_msg" class="form-control" placeholder="請填寫要發送的訊息" />
                                <label runat="server" id="msg"></label>
                                <br />
                                <asp:Button CssClass="btn btn-primary" OnClick="ButtonSend_Click" ID="ButtonSend" runat="server" Text="自我發送測試" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            測試2(beta)
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label>全體訊息:</label>
                                <input runat="server" id="txt_Allmsg" class="form-control" placeholder="請填寫要發送的訊息" />
                                <label runat="server" id="msgAll"></label>
                                <br />
                                <asp:Button CssClass="btn btn-primary" ID="brnAll" runat="server" Text="全體發送測試" OnClick="brnAll_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
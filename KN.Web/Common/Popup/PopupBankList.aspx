<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupBankList.aspx.cs" Inherits="KN.Web.Common.Popup.PopupBankList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>	
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <meta http-equiv="ImageToolBar" content="no" />
    <meta name="Keywords" content="사이트내용" />
    <meta name="Description" content="사이트소개" />
    <meta name="Copyright" content="저작권정보" />
    <script type="text/javascript" src="/Common/Javascript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/Common/Javascript/Global.js"></script>
    <script type="text/javascript">
        opener.window.fnHoldItem();

        function fnInsertDatum(strBankNm, strAccountNo, strAccCd)
        {
            var strhfParams1 = document.getElementById("<%=hfParams1.ClientID%>").value;

            opener.document.getElementById(strhfParams1 + "txtBankNm").value = strBankNm;
            opener.document.getElementById(strhfParams1 + "txtAccountNo").value = strAccountNo;
            opener.document.getElementById(strhfParams1 + "txtAccCd").value = strAccCd;

            window.close();
        }
    </script>
    <link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css" />
</head>
<body>
    <form id="frmPopupMemInfo" runat="server">
        <div>
            <table cellspacing="0" class="TbCel-Type-Pup4">
	            <colgroup>
                    <col width="300px"/>
                    <col width="200px"/>
                    <col width="100px"/>
	            </colgroup>
                <tr>
                    <th class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltTopBankNm" runat="server"></asp:Literal></th>                    
                    <th class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltTopAccountNo" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltTopAccCd" runat="server"></asp:Literal></th>
                </tr>
            </table>
            <div style="overflow-y:scroll;height:350px;width:600px;">
                <asp:ListView ID="lvBankList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnItemDataBound="lvBankList_ItemDataBound">
                    <LayoutTemplate>
                        <table class="table.TbCel-Type-Pup4-1">
                            <colgroup>
                                <col width="290px"/>
                                <col width="200px"/>
                                <col width="90px"/>
                                <tr id="iphItemlPlaceholderId" runat="server"></tr>
                            </colgroup>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnInsertDatum("<%#Eval("BankNm")%>", "<%#Eval("AccountNo")%>", "<%#Eval("AccCd")%>");'>
                            <td><asp:Literal ID="ltBankNm" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltAccountNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltAccCd" runat="server"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="table.TbCel-Type-Pup4-1">
                            <colgroup>
                                <col width="290px"/>
                                <col width="200px"/>
                                <col width="90px"/>
                            </colgroup>
                            <tr>
                                <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>        
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <asp:HiddenField ID="hfParams1" runat="server" />
            <asp:TextBox ID="txtParams" runat="server" Visible="false"></asp:TextBox>
        </div>
    </form>
</body>
</html>
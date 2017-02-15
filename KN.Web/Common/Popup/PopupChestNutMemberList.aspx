<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupChestNutMemberList.aspx.cs" Inherits="KN.Web.Common.Popup.PopupChestNutMemberList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>	
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Style-Type" content="text/css"/>
    <meta http-equiv="Content-Script-Type" content="text/javascript"/>
    <meta http-equiv="ImageToolBar" content="no"/>
    <meta name="Keywords" content="사이트내용"/>
    <meta name="Description" content="사이트소개"/>
    <meta name="Copyright" content="저작권정보"/>
    <script type="text/javascript" src="/Common/Javascript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/Common/Javascript/Global.js"></script>
    <script type="text/javascript">
        opener.window.fnHoldItem();
        
        function fnInsertDatum(strMemNo, strMemEngNm, strUserId, strTelFrontNo, strTelMidNo, strTelRearNo, strCellFrontNo, strCellMidNo, strCellRearNo, strEntDt, strAddr, strDetAddr, strEmailID, strEmailServer)
        {
            var strhfParams1 = document.getElementById("<%=hfParams1.ClientID%>").value;

            opener.document.getElementById(strhfParams1 + "hfMemNo").value = strMemNo;
            opener.document.getElementById(strhfParams1 + "hfMemEngNm").value = strMemEngNm;
            opener.document.getElementById(strhfParams1 + "hfUserID").value = strUserId;
            opener.document.getElementById(strhfParams1 + "hfTelFrontNo").value = strTelFrontNo;
            opener.document.getElementById(strhfParams1 + "hfTelMidNo").value = strTelMidNo;
            opener.document.getElementById(strhfParams1 + "hfTelRearNo").value = strTelRearNo;
            opener.document.getElementById(strhfParams1 + "hfCellFrontNo").value = strCellFrontNo;
            opener.document.getElementById(strhfParams1 + "hfCellMidNo").value = strCellMidNo;
            opener.document.getElementById(strhfParams1 + "hfCellRearNo").value = strCellRearNo;
            opener.document.getElementById(strhfParams1 + "hfEnterDt").value = strEntDt;
            opener.document.getElementById(strhfParams1 + "hfAddr").value = strAddr;
            opener.document.getElementById(strhfParams1 + "hfDetAddr").value = strDetAddr;
            opener.document.getElementById(strhfParams1 + "hfEmailID").value = strEmailID;
            opener.document.getElementById(strhfParams1 + "hfEmailServer").value = strEmailServer;
            opener.document.getElementById(strhfParams1 + "txtName").value = strMemEngNm;
            opener.document.getElementById(strhfParams1 + "txtUserID").value = strUserId;
            opener.document.getElementById(strhfParams1 + "txtTelTy").value = strTelFrontNo;
            opener.document.getElementById(strhfParams1 + "txtTelFrontNo").value = strTelMidNo;
            opener.document.getElementById(strhfParams1 + "txtTelRearNo").value = strTelRearNo;
            opener.document.getElementById(strhfParams1 + "txtMobileTy").value = strCellFrontNo;
            opener.document.getElementById(strhfParams1 + "txtMobileFrontNo").value = strCellMidNo;
            opener.document.getElementById(strhfParams1 + "txtMobileRearNo").value = strCellRearNo;
            opener.document.getElementById(strhfParams1 + "txtEnterDt").value = strEntDt;
            opener.document.getElementById(strhfParams1 + "txtAddr").value = strAddr;
            opener.document.getElementById(strhfParams1 + "txtDetAddr").value = strDetAddr;
            opener.document.getElementById(strhfParams1 + "txtEmailID").value = strEmailID;
            opener.document.getElementById(strhfParams1 + "txtEmailServer").value = strEmailServer;

            window.close();
        }

        function fnCheckType()
        {
            if (event.keyCode == 13)
            {
                document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
                return false;
            }
        }

        function fnSearch()
        {
            var strSearch = document.getElementById("<%=txtSearch.ClientID%>").value;

            strSearch = trim(strSearch);

            if (strSearch.length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }
    </script>
    <link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
</head>
<body>
    <form id="frmPopupMemInfo" runat="server">
        <div>
            <fieldset class="sh-field1 MrgB10">
                <legend><asp:Literal ID="ltSearch" runat="server"></asp:Literal></legend>
                <ul class="sf1-ag MrgL100">
                    <li><asp:DropDownList ID="ddlSearch" runat="server" OnKeyPress="javascript:return fnCheckType();"></asp:DropDownList></li>
                    <li><asp:TextBox ID="txtSearch" runat="server" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClientClick="javascript:return fnSearch();" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
            <table cellspacing="0" class="TbCel-Type4-A iw600">
	            <colgroup>
                    <col width="30px"/>
                    <col width="150px"/>
                    <col width="150px"/>
                    <col width="90px"/>
                    <col width="90px"/>
                    <col width="80px"/>
	            </colgroup>
                <tr>
                    <th ><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></th>                    
                    <th ><asp:Literal ID="ltTopMemNm" runat="server"></asp:Literal></th>
                    <th ><asp:Literal ID="ltTopUserId" runat="server"></asp:Literal></th>
                    <th ><asp:Literal ID="ltTopTelNo" runat="server"></asp:Literal></th>
                    <th ><asp:Literal ID="ltTopCellNo" runat="server"></asp:Literal></th>
		            <th ><asp:Literal ID="ltTopEnterDt" runat="server"></asp:Literal></th>
                </tr>
            </table>
            <div style="overflow-y:scroll;height:290px;width:622px;">
                <asp:ListView ID="lvMemList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMemList_ItemCreated" OnItemDataBound="lvMemList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellspacing="0" class="TbCel-Type4-A iw600">
	                        <colgroup>
                                <col width="30px"/>
                                <col width="150px"/>
                                <col width="150px"/>
                                <col width="90px"/>
                                <col width="90px"/>
                                <col width="80px"/>
	                        </colgroup>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnInsertDatum("<%#Eval("MemNo")%>", "<%#Eval("MemEngNm")%>", "<%#Eval("UserId")%>", "<%#Eval("TelFrontNo")%>", "<%#Eval("TelMidNo")%>", "<%#Eval("TelRearNo")%>", "<%#Eval("CellFrontNo")%>", "<%#Eval("CellMidNo")%>", "<%#Eval("CellRearNo")%>", "<%#Eval("EntDate")%>", "<%#Eval("Addr")%>", "<%#Eval("DetAddr")%>", "<%#Eval("EmailID")%>", "<%#Eval("EmailServer")%>");'>
                            <td class="TbTxtCenter P0">
                                <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfMemNo" runat="server" />
                                <asp:HiddenField ID="hfUserId" runat="server" />
                                <asp:HiddenField ID="hfMemEngNm" runat="server" />
                                <asp:HiddenField ID="hfEntDt" runat="server" />
                                <asp:HiddenField ID="hfTelFrontNo" runat="server" />
                                <asp:HiddenField ID="hfTelMidNo" runat="server" />
                                <asp:HiddenField ID="hfTelRearNo" runat="server" />
                                <asp:HiddenField ID="hfCellFrontNo" runat="server" />
                                <asp:HiddenField ID="hfCellMidNo" runat="server" />
                                <asp:HiddenField ID="hfCellRearNo" runat="server" />
                                <asp:HiddenField ID="hfAddr" runat="server" />
                                <asp:HiddenField ID="hfDetAddr" runat="server" />
                                <asp:HiddenField ID="hfEmailID" runat="server" />
                                <asp:HiddenField ID="hfEmailServer" runat="server" />
                            </td>
                            <td class="TbTxtCenter P0"><asp:Literal ID="ltMemNm" runat="server"></asp:Literal></td>
			                <td class="TbTxtCenter P0"><asp:Literal ID="ltUserId" runat="server"></asp:Literal></td>
                            <td class="TbTxtCenter P0"><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></td>
                            <td class="TbTxtCenter P0"><asp:Literal ID="ltCellNo" runat="server"></asp:Literal></td>
                            <td class="TbTxtCenter"><asp:Literal ID="ltEnterDt" runat="server"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellspacing="0" class="TbCel-Type4-A iw600">
	                        <colgroup>
                                <col width="30px"/>
                                <col width="150px"/>
                                <col width="150px"/>
                                <col width="90px"/>
                                <col width="90px"/>
                                <col width="80px"/>
	                        </colgroup>
                            <tr>
                                <td colspan="6" class="TbTxtCenter"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <asp:HiddenField ID="hfExgistMsg" runat="server" />
            <asp:HiddenField ID="hfParams1" runat="server" />
        </div>
    </form>
</body>
</html>
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AccessList.aspx.cs" Inherits="KN.Web.Config.Log.AccessList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    function fnMovePage(intPageNo) 
    {
        if (intPageNo == null) 
        {
            intPageNo = 1;
        }
        
        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
        <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
    }
    
    function fnAccessList()
    {
        // Datum0 : 시작일
        // Datum1 : 종료일
        var strStartDt = document.getElementById("<%=hfStartDt.ClientID%>").value;
        var strEndDt = document.getElementById("<%=hfEndDt.ClientID%>").value;
        
        if (strStartDt != "")
        {
            strStartDt = strStartDt.replace(/\-/gi,"");
        }
        
        if (strEndDt != "")
        {
            strEndDt = strEndDt.replace(/\-/gi,"");
        }
        
        window.open("/Common/RdPopup/RDPopupAccessList.aspx?Datum0=" + strStartDt + "&Datum1=" + strEndDt, "AccessList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        return false;
    }
    //-->
    </script>
    <fieldset class="sh-field2">
        <ul class="sf2-ag MrgL10 ">
            <li><asp:Literal ID="ltTerm" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtStartDt" runat="server" Width="70px"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
                <asp:HiddenField ID="hfStartDt" runat="server"/>
            </li>
            <li><span>~</span></li>
            <li>
	            <asp:TextBox ID="txtEndDt" runat="server" Width="70px"></asp:TextBox>
	            <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndDt.ClientID%>', '<%=hfEndDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
	            <asp:HiddenField ID="hfEndDt" runat="server"/>
            </li>
            <li>
	            <div class="Btn-Type4-wp">
		            <div class="Btn-Tp4-L">
			            <div class="Btn-Tp4-R">
				            <div class="Btn-Tp4-M">
					            <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
				            </div>
			            </div>
		            </div>
	            </div>			
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upLogList" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnDel" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvLogList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvLogList_ItemDataBound" 
                OnLayoutCreated="lvLogList_LayoutCreated" OnItemCreated="lvLogList_ItemCreated">
                <LayoutTemplate>
                    <table class="TbCel-Type5-C" cellpadding="0">
                        <col width="5%"/>
                        <col width="5%"/>
                        <col width="25%"/>
                        <col width="25%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                        <tr>
                            <th><asp:CheckBox ID="chkAll" style="text-align:center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged"/></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleAccessSeq" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleDate" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleMemNo" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleUserId" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleInsIP" runat="server"></asp:Literal></th>
                        </tr>
                        <tr runat="server" id="iphItemPlaceHolderId"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:CheckBox ID="chkboxList" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"></asp:CheckBox>
                            <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>                            
                        </td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltAccessSeq" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltDate" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltMemNo" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltUserId" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt PL10"><asp:Literal ID="ltInsIP" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table cellpadding="0" class="TbCel-Type1">
                        <col width="5%"/>   
                        <col width="5%"/>
                        <col width="25%"/>
                        <col width="25%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                        <tr>
                            <th>&nbsp;</th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleAccessSeq" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleDate" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleMemNo" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleUserId" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltTitleInsIP" runat="server"></asp:Literal></th>
                        </tr>
                        <tr>
                            <td colspan="6" class="TbTxtCenter"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div class="Clear">
                 <span id="spanPageNavi" runat="server" style="width:100%"></span>
            </div>
		    <div class="Btwps Clear">
			    <div class="Btn-Type2-wp FloatR">
				    <div class="Btn-Tp2-L">
					    <div class="Btn-Tp2-R">
						    <div class="Btn-Tp2-M">
							    <span><asp:LinkButton ID="lnkbtnDel" runat="server" onclick="lnkbtnDel_Click"></asp:LinkButton></span>
						    </div>
					    </div>
				    </div>
			    </div>
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
	                        <div class="Btn-Tp2-M">
		                        <span><asp:LinkButton ID="lnkbtnPrint" runat="server" OnClientClick="javascript:return fnAccessList();"></asp:LinkButton></span>
	                        </div>
                        </div>
                    </div>
                </div>
		    </div>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
            <asp:HiddenField ID="hfCurrentPage" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
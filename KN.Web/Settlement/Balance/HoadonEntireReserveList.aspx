<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonEntireReserveList.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonEntireReserveList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strTxt)
        {
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strUserTaxCd = document.getElementById("<%=txtUserTaxCd.ClientID%>").value;
            var strRssNo = document.getElementById("<%=txtRssNo.ClientID%>").value;

            if (trim(strSearchRoom.value) == "" && trim(strUserTaxCd) == "" && trim(strRssNo) == "")
            {
                strSearchRoom.focus();
                alert(strTxt);

                return false;
            }

            return true;
        }
        
        function fnChestNutPreview(strUserSeq, strPaymentDt, strPaymentSeq)
        {
            <%=Page.GetPostBackEventReference(imgbtnHoadonPreView)%>;        
            window.open("/Common/RdPopup/RDPopupHoadonCNPreview.aspx?UserSeq=" + strUserSeq + "&PaymentDt=" + strPaymentDt + "&PaymentSeq=" + strPaymentSeq + "&InsPaymentDt=" + strPaymentDt + "&InvoiceYn=&InsTaxCd=", "HoaDon", "status=no, resizable=no, width=730, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }
        
        function fnIssuingCheck(strTxt)
        {
            if (confirm(strTxt))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    //-->
    </script>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
	        <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
	        <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	        <li><asp:Literal ID="ltSearchYear" runat="server"></asp:Literal></li>
	        <li><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></li>
	        <li><asp:Literal ID="ltSearchMonth" runat="server"></asp:Literal></li>
	        <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>
	        <li><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></li>
	        <li>
	            <asp:TextBox ID="txtStartDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
	            <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
	            <asp:HiddenField ID="hfStartDt" runat="server"/>
	        </li>
	        <li><asp:Literal ID="ltEndDt" runat="server"></asp:Literal></li>
	        <li>
	            <asp:TextBox ID="txtEndDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
	            <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndDt.ClientID%>', '<%=hfEndDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
	            <asp:HiddenField ID="hfEndDt" runat="server"/>
	        </li>
        </ul>
    </fieldset>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
	        <li><asp:DropDownList ID="ddlItemCd" runat="server"></asp:DropDownList></li>
	        <li><asp:Literal ID="ltTxtCd" runat="server"></asp:Literal></li>
	        <li><asp:TextBox ID="txtUserTaxCd" runat="server"></asp:TextBox></li>
	        <li><asp:Literal ID="ltRssNo" runat="server"></asp:Literal></li>
	        <li><asp:TextBox ID="txtRssNo" runat="server"></asp:TextBox></li>
	        <li>
		        <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>		        
	        </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="120px" />
                    <col width="150px" />
                    <col width="120px" />
                    <col width="450px" />
                    <tr>
                        <th><asp:Literal ID="ltRange" runat="server"></asp:Literal></th>
                        <td class="Bd-Lt TbTxtLeft" colspan="3"><asp:DropDownList ID="ddlRange" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltType" runat="server"></asp:Literal></th>
                        <td class="Bd-Lt TbTxtLeft"><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
                        <th class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
                        <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtTitle" runat="server" MaxLength="255" Width="440px"></asp:TextBox></td>
                    </tr>
                </colgroup>
            </table>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="50px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="290px" />
                    <col width="80px" />
                    <col width="130px" />
                    <col width="50px" />
                    <tr>
                        <th class="Fr-line"><asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" style="text-align:center" /></th>
                        <th><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltDescription" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                        <th class="Ls-line"></th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y:scroll;height:250px;width:840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="50px"/>
                            <col width="80px"/>
                            <col width="80px"/>
                            <col width="80px"/>
                            <col width="290px"/>
                            <col width="80px"/>
                            <col width="130px"/>
                            <col width="30px"/>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkboxList" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"></asp:CheckBox>
                                <asp:TextBox ID="txtHfDebitCreditCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsYear" runat="server"></asp:Literal>&nbsp;/&nbsp;<asp:Literal ID="ltInsMonth" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsItemNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfItemCd" runat="server" Visible="false"></asp:TextBox>                                
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfBillNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtLeft">
                                <asp:Literal ID="ltInsRemarkEn" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsTotSellingPrice" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRegDt" runat="server"></asp:Literal></td>
                            <td>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="50px"/>
                            <col width="80px"/>
                            <col width="80px"/>
                            <col width="80px"/>
                            <col width="290px"/>
                            <col width="80px"/>
                            <col width="130px"/>
                            <col width="30px"/>
                            <tr>
                                <td colspan="8" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div class="Btwps FloatR">
	            <div class="Btn-Type2-wp FloatL">
		            <div class="Btn-Tp2-L">
			            <div class="Btn-Tp2-R">
				            <div class="Btn-Tp2-M">
					            <span><asp:LinkButton ID="lnkbtnIssuing" runat="server" onclick="lnkbtnIssuing_Click"></asp:LinkButton></span>
				            </div>
			            </div>
		            </div>
	            </div>
            </div>
            <!--//<asp:HiddenField ID="hfDisplayUserSeq" runat="server"></asp:HiddenField>//-->
            <!--//<asp:HiddenField ID="hfDisplayPrintSeq" runat="server"></asp:HiddenField>//-->
            <!--//<asp:HiddenField ID="hfDisplayPrintDetSeq" runat="server"></asp:HiddenField>//-->
        </ContentTemplate>    
    </asp:UpdatePanel>
    <asp:ImageButton ID="imgbtnHoadonPreView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnHoadonPreView_Click"/>    
    <asp:HiddenField ID="hfRentCd" runat="server" />
</asp:Content>
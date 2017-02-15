<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonReserveList.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonReserveList" ValidateRequest="false"%>
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
    
    function fnCheckValidate(strTxt)
    {
        var strName = document.getElementById("<%=txtSearchNm.ClientID%>");
        var strRoomNo = document.getElementById("<%=txtSearchRoom.ClientID%>").value;
        var strPaymentKind = document.getElementById("<%=ddlPayment.ClientID%>").value;
        
//        if (trim(strName.value) == "" && trim(strRoomNo) == "" && trim(strPaymentKind) == "")
//        {
//            alert(strTxt);
//            strName.focus();
//            
//            return false;
//        }
//        else
//        {
//            return true;
//        }
        
        return true;
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
    
    function fnEntireIssuingCheck(strTxt)
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
    
    function fnEditor(strPaymentDt, strPaymentSeq, strPaymentDetSeq)
    {
        document.getElementById("<%=hfPaymentDt.ClientID%>").value = strPaymentDt;
        document.getElementById("<%=hfPaymentSeq.ClientID%>").value = strPaymentSeq;
        document.getElementById("<%=hfPaymentDetSeq.ClientID%>").value = strPaymentDetSeq;
        
        <%=Page.GetPostBackEventReference(imgbtnHoadonEdit)%>;
        
        return false;
    }
    
    function fnChestNutPreview(strUserSeq, strPaymentDt, strPaymentSeq, strInsTaxCdClientID, strInsPaymentDt, strInvoiceYn)
    {
        var strInsTaxCd = document.getElementById(strInsTaxCdClientID).value;
        
        <%=Page.GetPostBackEventReference(imgbtnHoadonPreView)%>;        
        window.open("/Common/RdPopup/RDPopupHoadonCNPreview.aspx?UserSeq=" + strUserSeq + "&PaymentDt=" + strPaymentDt + "&PaymentSeq=" + strPaymentSeq + "&InsPaymentDt=" + strInsPaymentDt + "&InvoiceYn=" + strInvoiceYn + "&InsTaxCd=" + strInsTaxCd, "HoaDon", "status=no, resizable=no, width=730, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        
        return false;
    }
    
    function fnLoadData()
    {
        document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
    }  
    //-->
    </script>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:DropDownList ID="ddlCompNo" runat="server" Visible="false"></asp:DropDownList></li>
            <li><asp:Literal ID="ltSearchName" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>            
	        <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
	        <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	        <li>
	            <div class="C235-st FloatL">
	                <asp:DropDownList ID="ddlPayment" runat="server"></asp:DropDownList>	               
                </div>
	        </li>
	        <li>
	            <asp:TextBox ID="txtStartDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
	            <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
	            <asp:HiddenField ID="hfStartDt" runat="server"/>
	        </li>
	        <li>
	            <asp:TextBox ID="txtEndDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
	            <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndDt.ClientID%>', '<%=hfEndDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
	            <asp:HiddenField ID="hfEndDt" runat="server"/>
	        </li>	
	        <li>
		        <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>		        
	        </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnEntireIssuing" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnIssuing" EventName="Click"/>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvInvoiceList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvInvoiceList_LayoutCreated" 
                OnItemCreated="lvInvoiceList_ItemCreated" OnItemDataBound="lvInvoiceList_ItemDataBound">
                <LayoutTemplate>
                    <table class="TbCel-Type6-A" cellpadding="0">
		                <col width="50px"/>
		                <col width="80px"/>
		                <col width="150px"/>
		                <col width="80px"/>
		                <col width="170px"/>
		                <col width="80px"/>
		                <col width="80px"/>
		                <col width="100px"/>
		                <col width="50px"/>
                        <tr>
                            <th class="Fr-line"><asp:CheckBox ID="chkAll" style="text-align:center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged"/></th>
                            <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltTaxCd" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPaymentKind" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPayMethod" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                            <th class="Ls-line"></th>
                        </tr>
                        <tr runat="server" id="iphItemPlaceHolderId"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:CheckBox ID="chkboxList" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"></asp:CheckBox>
                            <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsNm" runat="server"></asp:Literal></td>
                        <!--<td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsTaxCd" runat="server"></asp:Literal></td>-->
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:TextBox ID="txtInsTaxCd" Width="70px" MaxLength="16" runat="server" AutoPostBack="true" OnTextChanged="txtInsTaxCd_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtHfInsTaxCd" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td class="Bd-Lt TbTxtLeft"><asp:Literal ID="ltInsPaymentKind" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltInsPaymentDt" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfInsPaymentDt" runat="server"/>
                        </td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsPayMethod" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsAmount" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:ImageButton ID="imgbtnEdit" runat="server" AlternateText="Edit" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Icon/edit.gif"/>
                            <asp:ImageButton ID="imgbtnExample" runat="server" AlternateText="Preview" ImageAlign="AbsMiddle" Visible="false" ImageUrl="~/Common/Images/Icon/Magnifier.gif"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TbCel-Type6-A" cellpadding="0">
		                <col width="50px"/>
		                <col width="80px"/>
		                <col width="150px"/>
		                <col width="80px"/>
		                <col width="170px"/>
		                <col width="80px"/>
		                <col width="80px"/>
		                <col width="100px"/>
		                <col width="50px"/>
                        <tr>
                            <th class="Fr-line"><asp:CheckBox ID="chkAll" style="text-align:center" runat="server" Enabled="false"/></th>
                            <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltTaxCd" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPaymentKind" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltPayMethod" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                            <th class="Ls-line"></th>
                        </tr>
                        <tr>
                            <td colspan="9" class="TbTxtCenter"><asp:Literal ID="lblINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width:100%"></span>
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
            <div class="Btwps FloatR">
	            <div class="Btn-Type2-wp FloatL">
		            <div class="Btn-Tp2-L">
			            <div class="Btn-Tp2-R">
				            <div class="Btn-Tp2-M">
					            <span><asp:LinkButton ID="lnkbtnEntireIssuing" runat="server" onclick="lnkbtnEntireIssuing_Click"></asp:LinkButton></span>
				            </div>
			            </div>
		            </div>
	            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnHoadonEdit" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnHoadonEdit_Click"/>
    <asp:ImageButton ID="imgbtnHoadonPreView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnHoadonPreView_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfPaymentDt" runat="server" />
    <asp:HiddenField ID="hfPaymentSeq" runat="server" />
    <asp:HiddenField ID="hfPaymentDetSeq" runat="server" />
</asp:Content>
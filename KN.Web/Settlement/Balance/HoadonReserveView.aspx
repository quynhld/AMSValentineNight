<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonReserveView.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonReserveView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/jscript">
    function fnDetailView(strPaymentDt, strPaymentSeq, strPaymentDetSeq)
    {
        document.getElementById("<%=hfPaymentDt.ClientID%>").value = strPaymentDt;
        document.getElementById("<%=hfPaymentSeq.ClientID%>").value = strPaymentSeq;
        document.getElementById("<%=hfPaymentDetSeq.ClientID%>").value = strPaymentDetSeq;
        
        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
        
        return false;
    }
</script>
<asp:UpdatePanel ID="upLedgerMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ddlInvoiceCont" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="chkPayDt" EventName="CheckedChanged"/>
    </Triggers>
    <ContentTemplate>
        <table class="TbCel-Type2-A">
	        <colgroup>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
                <tr>
                    <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                    <th class="Bd-Lt"><asp:Literal ID="ltNm" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtInsNm" runat="server" CssClass="bgType2" MaxLength="255" Width="150px"></asp:TextBox>
                        <asp:TextBox ID="txtHfNm" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltPaymentMethod" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltInsPaymentMethod" runat="server"></asp:Literal></td>
                    <th><asp:Literal ID="ltPayDt" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtInsPayDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="80px"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtInsPayDt.ClientID%>', '<%=hfInsPayDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value="" />
                        <asp:HiddenField ID="hfInsPayDt" runat="server"/>
                        <asp:TextBox ID="txtHfPayDt" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:CheckBox ID="chkPayDt" runat="server" AutoPostBack="true" OnCheckedChanged="chkPayDt_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltItemViAmt" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltInsItemViAmt" runat="server"></asp:Literal></td>
                    <th><asp:Literal ID="ltItemTotViAmt" runat="server"></asp:Literal></th>
                    <td>
                        <asp:Literal ID="ltInsItemTotViAmt" runat="server"></asp:Literal>
                        (
                        <asp:Literal ID="ltInsPaymentNm" runat="server"></asp:Literal>
                        )
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltInvoiceCont" runat="server"></asp:Literal></th>
                    <td>
                        <asp:DropDownList ID="ddlInvoiceCont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceCont_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th><asp:Literal ID="ltTaxCd" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtInsTaxCd" runat="server" MaxLength="16" Width="150px"></asp:TextBox>
                        <asp:TextBox ID="txtHfTaxCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="Bd-bt" rowspan="2" style="height:52px">
                        <asp:Literal ID="ltTaxAddr" runat="server"></asp:Literal>&nbsp;(For Tax)
                    </th>
                    <td colspan="3" style="height:26px">
                        <asp:TextBox ID="txtInsTaxAddr" runat="server" MaxLength="255" Width="620px"></asp:TextBox>
                        <asp:TextBox ID="txtHfTaxAddr" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Bd-bt" colspan="3" style="height:26px">
                        <asp:TextBox ID="txtInsTaxDetAddr" runat="server" MaxLength="255" Width="620px"></asp:TextBox>
                        <asp:TextBox ID="txtHfTaxDetAddr" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </colgroup>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="Btwps FloatL">
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
<div class="Btwps FloatL">
    <div class="Btn-Type2-wp FloatL">
        <div class="Btn-Tp2-L">
            <div class="Btn-Tp2-R">
	            <div class="Btn-Tp2-M">
		            <span><asp:LinkButton ID="lnkbtnReIssuing" runat="server" onclick="lnkbtnReIssuing_Click"></asp:LinkButton></span>
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
		            <span><asp:LinkButton ID="lnkbtnApply" runat="server" onclick="lnkbtnApply_Click"></asp:LinkButton></span>
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
		            <span><asp:LinkButton ID="lnkbtnEntireApply" runat="server" onclick="lnkbtnEntireApply_Click"></asp:LinkButton></span>
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
		            <span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton></span>
	            </div>
            </div>
        </div>
    </div>
</div>
<asp:UpdatePanel ID="upLedgerDetail" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvInvoiceList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvInvoiceList_LayoutCreated" 
            OnItemCreated="lvInvoiceList_ItemCreated" OnItemDataBound="lvInvoiceList_ItemDataBound">
            <LayoutTemplate>
                <table class="TbCel-Type6-A" cellpadding="0">
                    <col width="50px"/>
                    <col width="80px"/>
                    <col width="200px"/>
                    <col width="100px"/>
                    <col width="120px"/>
	                <col width="50px"/>
	                <col width="110px"/>
	                <col width="130px"/>
                    <tr>
                        <th class="Fr-line"></th>
                        <th><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPaymentKind" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPayMethod" runat="server"></asp:Literal></th>                        
                        <th><asp:Literal ID="ltUnitPrimeCost" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltVATAmount" runat="server"></asp:Literal></th>
                        <th class="Ls-line"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    </tr>
                    <tr runat="server" id="iphItemPlaceHolderId"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:return fnDetailView('<%#Eval("PaymentDt")%>','<%#Eval("PaymentSeq")%>','<%#Eval("PaymentDetSeq")%>')">
                    <td class="Bd-Lt TbTxtCenter"><asp:Image ID="imgCheck" runat="server" ImageAlign="AbsMiddle"/></td>
                    <td class="Bd-Lt TbTxtCenter">
                        <asp:Literal ID="ltInsPaymentDt" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsPaymentKind" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsPayMethod" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsUnitPrimeCost" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsQty" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsVATAmount" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsAmount" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type6-A" cellpadding="0">
                    <col width="50px"/>
                    <col width="80px"/>
                    <col width="200px"/>
                    <col width="100px"/>
                    <col width="120px"/>
	                <col width="50px"/>
	                <col width="110px"/>
	                <col width="130px"/>
                    <tr>
                        <th class="Fr-line"></th>
                        <th><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPaymentKind" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltPayMethod" runat="server"></asp:Literal></th>                        
                        <th><asp:Literal ID="ltUnitPrimeCost" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltVATAmount" runat="server"></asp:Literal></th>
                        <th class="Ls-line"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    </tr>
                    <tr>
                        <td colspan="8" class="TbTxtCenter"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailView_Click"/>
<asp:HiddenField ID="hfRentCd" runat="server" />
<asp:HiddenField ID="hfPaymentDt" runat="server" />
<asp:HiddenField ID="hfPaymentSeq" runat="server" />
<asp:HiddenField ID="hfPaymentDetSeq" runat="server" />
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
</asp:Content>
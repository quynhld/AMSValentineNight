<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AccountlogList.aspx.cs" Inherits="KN.Web.Config.Log.AccountlogList" ValidateRequest="false"%>
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
    //-->
    </script>    
    <asp:ListView ID="lvLogList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvLogList_ItemDataBound" 
        OnLayoutCreated="lvLogList_LayoutCreated" OnItemCreated="lvLogList_ItemCreated">
        <LayoutTemplate>
            <table class="TbCel-Type5-C" cellpadding="0">
                <col width="5%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="15%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDebitNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltRentNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDirectNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltPaymentNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                </tr>
                <tr runat="server" id="iphItemPlaceHolderId"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'">               
                <td class="Bd-Lt TbTxtCenter">
                    <asp:Literal ID="ltInsSeq" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfFeeVat" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsDebitNm" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsDirectNm" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsItemNm" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsMngFeeNET" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsMngFeeVAT" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsAmount" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsPaymentCd" runat="server"></asp:Literal></td>
                <td class="Bd-Lt PL10"><asp:Literal ID="ltInsDtList" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellpadding="0" class="TbCel-Type1">
                <col width="5%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="15%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <col width="10%"/>
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDebitNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltRentNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDirectNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltPaymentNm" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <td colspan="10" class="TbTxtCenter"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>    
    <div class="Clear">
         <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>    
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfVatRation" runat="server"/>    
</asp:Content>
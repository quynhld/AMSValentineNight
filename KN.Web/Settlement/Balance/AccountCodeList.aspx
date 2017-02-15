<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AccountCodeList.aspx.cs" Inherits="KN.Web.Settlement.Balance.AccountCodeList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnHoldItem()
    {
    }
    function SaveSuccess() {
        alert('Save Successful !');
    }

    function fnDeleted() {
        alert('Delete Successful !');
    }
    function fnCheckValidate(strText) {
        var ddlRentNm = $('#<%=ddlRentNm.ClientID %>').val();
        if (ddlRentNm == "") {
            alert(strText);
            $('#<%=ddlRentNm.ClientID %>').focus();
            return false;
        }        
        var ddlPaymentType = $('#<%=ddlPaymentType.ClientID %>').val();
        if (ddlPaymentType == "") {
            alert(strText);
            $('#<%=ddlPaymentType.ClientID %>').focus();
            return false;
        }
        var ddlFeeType = $('#<%=ddlFeeType.ClientID %>').val();
        if (ddlFeeType == "") {
            alert(strText);
            $('#<%=ddlFeeType.ClientID %>').focus();
            return false;
        }        
        var txtBankName = $('#<%=txtBankName.ClientID %>').val();
        if (txtBankName == "") {
            alert(strText);
            $('#<%=txtBankName.ClientID %>').focus();
            return false;
        }
        var txtBankAccount = $('#<%=txtBankAccount.ClientID %>').val();
        if (txtBankAccount == "") {
            alert(strText);
            $('#<%=txtBankAccount.ClientID %>').focus();
            return false;
        }

        return true;
    }
</script>
    <table cellspacing="0" class="TbCel-Type4-A">
	    <colgroup>
            <col width="125px"/>
            <col width="80px"/>
            <col width="125px"/>
            <col width="100px"/>
            <col width="125px"/>
            <col width="125px"/>
            <col width="70px"/>
            <col width="90px"/>
	    </colgroup>
        <tr>
            <th><asp:Literal ID="ltTopCompCd" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltTopRent" runat="server"></asp:Literal></th>
            <th class="Bd-Lt">Fee Type</th>
            <th class="Bd-Lt">Pay Type</th>
            <th class="Bd-Lt">Bank Name</th>
            <th class="Bd-Lt">Bank Account</th>
            <th class="Bd-Lt">Active</th>
            <th class="Bd-Lt">Action</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnWrite" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlCompNm" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div style="overflow-y:scroll;height:370px;width:840px;">
                <asp:ListView ID="lvAccountCdList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnitemCreated="lvAccountCdList_ItemCreated"
                    OnItemUpdating="lvAccountCdList_ItemUpdating" OnItemDeleting="lvAccountCdList_ItemDeleting" OnItemDataBound="lvAccountCdList_ItemDataBound" OnItemCommand="lvAccountCdList_ItemCommand">
                    <LayoutTemplate>
                        <table class="TypeA iw820">
                            <colgroup>
                                <col width="125px"/>
                                <col width="80px"/>
                                <col width="125px"/>
                                <col width="100px"/>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="70px"/>
                                <col width="70px"/>
                            </colgroup>
                            <tr id="iphItemlPlaceholderId" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>            
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltCompNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfCompCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltRentNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltFeeName" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfFeeCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPayMentType" runat="server"></asp:Literal>                               
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtBankName" runat="server" Width="115px" MaxLength="300"></asp:TextBox>
                                <asp:TextBox ID="txtHfBankSeq" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtBankCode" runat="server" Width="115px" MaxLength="300"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkUsedYN" runat="server"></asp:CheckBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <span>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Common/Images/Icon/edit.gif" AlternateText="Update"/>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Common/Images/Icon/Trash.gif" AlternateText="Delete this row "/>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA iw820">
                            <colgroup>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="125px"/>
                                <col width="70px"/>
                            </colgroup>
                            <tr>
                                <td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>        
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <table cellspacing="0" class="TbCel-Type4-A">
	            <colgroup>
                    <col width="125px"/>
                    <col width="125px"/>
                    <col width="125px"/>
                    <col width="125px"/>
<%--                    <col width="125px"/>
                    <col width="90px"/>--%>
	            </colgroup>
                <tr id="trInsertAuthGrp" runat="server" >
                    <td style="background-color:rgb(236, 236, 236)">
                        <b>Company Name</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft" >
                        <asp:DropDownList ID="ddlCompNm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompNm_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="background-color:rgb(236, 236, 236)">
                       <b> Rent Name</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft">
                        <asp:DropDownList ID="ddlRentNm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRentNm_SelectedIndexChanged"></asp:DropDownList>
                    </td>  
                </tr>
                <tr>
                    <td style="background-color:rgb(236, 236, 236)">
                        <b>Payment Type</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft">
                        <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="background-color:rgb(236, 236, 236)">
                       <b> Fee Type</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft">
                        <asp:DropDownList ID="ddlFeeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeType_SelectedIndexChanged"></asp:DropDownList>
                    </td>                    
                </tr>
                <tr>
                    <td style="background-color:rgb(236, 236, 236)">
                        <b>Bank Name</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft">
                        <asp:TextBox ID="txtBankName" runat="server" Width="270px" MaxLength="300"></asp:TextBox>
                    </td>
                    <td style="background-color:rgb(236, 236, 236)">
                       <b> Bank Account</b>
                    </td>
                    <td class="Bd-Lt TbTxtLeft">
                        <asp:TextBox ID="txtBankAccount" runat="server" Width="270px" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnWrite" runat="server" Text="Save" 
                                    onclick="lnkbtnWrite_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>                
            </div>
            <asp:HiddenField ID="hfParamData" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
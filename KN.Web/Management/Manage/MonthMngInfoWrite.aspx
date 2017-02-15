<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthMngInfoWrite.aspx.cs" Inherits="KN.Web.Management.Manage.MonthMngInfoWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div class="TpAtit1">
        <span class="shf-sel FloatR2">
            <b><asp:Literal ID="ltChargeMonth" runat="server"></asp:Literal></b>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
            <asp:TextBox ID="txtHfYear" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfMonth" runat="server" Visible="false"></asp:TextBox>            
        </span>
    </div>
    <table class="TypeA">
        <col width="10%"/>
        <col width="20%"/>
        <col width="25%"/>
        <col width="15%"/>
        <col width="15%"/>
        <col width="15%"/>
        <thead>
        <tr>
            <th><asp:Literal ID="ltUseYn" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltMngFeeCd" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltMngFeeNm" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltMngFee" runat="server"></asp:Literal></th>
        </tr>                       
        </thead>
    </table>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Always">
        <Triggers></Triggers>
        <ContentTemplate>
            <div style="overflow-y:scroll;height:400px;" class="iw840">      	
                <asp:ListView ID="lvMonthMngInfoWrite" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMonthMngInfoWrite_ItemDataBound">
                    <LayoutTemplate>
                        <table class="TypeA iw800 bdFFF">
                            <col width="10%"/>
                            <col width="20%"/>
                            <col width="25%"/>
                            <col width="15%"/>
                            <col width="15%"/>
                            <col width="15%"/>
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server"></tr>
                            </tbody>                
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center"><asp:CheckBox ID="chkInsUseYn" runat="server" OnCheckedChanged="chkInsUseYn_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                            <td align="center"><asp:Literal ID="ltInsMngFeeCd" runat="server"></asp:Literal></td>
                            <td align="center"><asp:Literal ID="ltInsMngFeeNm" runat="server"></asp:Literal></td>
                            <td align="center"><asp:TextBox ID="txtInsMngFeeNet" runat="server" CssClass="bgType2" MaxLength="21" Enabled="false" OnTextChanged="txtInsMngFeeNet_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            <td align="center"><asp:Literal ID="ltInsMngFeeVat" runat="server"></asp:Literal></td>
                            <td align="center"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA iw820">
                            <tbody>
                                <tr>
                                    <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>        
                </asp:ListView>    
            </div>
            <div class="Btwps FloatR">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span><asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span> <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton> </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngFeeCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngMM" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngFee" runat="server" Visible="false"></asp:TextBox>    
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server"/>
    <asp:HiddenField ID="hfMngYear" runat="server"/>
    <asp:HiddenField ID="hfMngMM" runat="server"/>
    <asp:HiddenField ID="hfVatRation" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "") 
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>
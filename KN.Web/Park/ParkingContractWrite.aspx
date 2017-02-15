<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ParkingContractWrite.aspx.cs" Inherits="KN.Web.Park.ParkingContractWrite"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--    //
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            loadCalendar();
            loadCompanyName();
        }
    }    
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {                        
            return false;
        }
    }

    function fnCheckValidate(strText)
    {
        var roomNo = $('#<%=txtRoomNo.ClientID %>').val();
        if (roomNo =="") {
            alert(strText);
            $('#<%=ddlCompanyName.ClientID %>').focus();
            return false;
        }
        var txtSUsingDt = $('#<%=txtTenantName.ClientID %>').val();
        if (txtSUsingDt =="") {
            alert(strText);
            $('#<%=txtTenantName.ClientID %>').focus();
            return false;
        }
        var txtAddress = $('#<%=txtAddress.ClientID %>').val();
        if (txtAddress =="") {
            alert(strText);
            $('#<%=txtAddress.ClientID %>').focus();
            return false;
        }           
       
        return true;
    }


    function loadCalendar() {
    }

    $(document).ready(function () {
        loadCalendar();
        loadCompanyName();
    });

    function SaveSuccess() {
        alert('Save Successful !');
         <%=Page.GetPostBackEventReference(lnkbtnCancel)%>;
    }

    function addDueDt() {

    }

    function DeleteSuccess() {        
        alert('Delete Successful !'); 
         <%=Page.GetPostBackEventReference(lnkbtnCancel)%>;
        return false;
    }

    function fnDeleteData() {       
        if (confirm('Are you sure ?')) {
            <%=Page.GetPostBackEventReference(lnkbtnDelete)%>;
            return false;
        }  
        return false;
    }
    function loadCompanyName() {
        $('#<%=txtRoomNo.ClientID %>').live('change',function () {
            $('#<%=ddlCompanyName.ClientID %>').val($('#<%=txtRoomNo.ClientID %>').val());
        }); 
    }

    function countIndex() {

    }
//-->
    </script>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlInsRentCd" EventName="SelectedIndexChanged"/>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRetalFee" runat="server" Text="Basic Information"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                Rental
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlInsRentCd" runat="server" AutoPostBack="True"
                                    onselectedindexchanged="ddlInsRentCd_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <th>
                                Room No
                            </th>
                            <td>
                                <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="10" Width="100" CssClass="bgType2 "
                                    Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Company Name
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlCompanyName" runat="server" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Tenant Name
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtTenantName" runat="server" MaxLength="300" Width="527" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                Company Address
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="300" Width="527" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="3">
                                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="300" Width="527"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Company Tax Code
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtTaxCd" runat="server" MaxLength="300" Width="527" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>                        
                    </tbody>
                </colgroup>
            </table>                                                
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnWrite" runat="server" Text="Save" OnClick="lnkbtnWrite_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" OnClick="lnkbtnDelete_Click"
                                        Visible="False"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lnkReload" runat="server" OnClick="lnkReload_Click" Visible="False"></asp:LinkButton>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txthfContractNo" runat="server" Visible="false" Text="0"></asp:TextBox>
</asp:Content>

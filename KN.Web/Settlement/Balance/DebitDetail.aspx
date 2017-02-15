<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="DebitDetail.aspx.cs" Inherits="KN.Web.Settlement.Balance.DebitDetail" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/jscript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            loadCalendar();           
        }
    }  
    
    function loadCalendar() {
        $(".Csscalender").datepicker();
    }
    $(document).ready(function () {
        loadCalendar();
    });    
    function SaveSuccess() {
        alert('Save Successful !');
        if (confirm('Do you want continue ?')) {
              location.reload();  
        } else {
            <%=Page.GetPostBackEventReference(lnkbtnCancel)%>;
        }
    }    
</script>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
    </Triggers>
    <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRetalFee" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                Company Name
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="ltCompNm" runat="server" Text="Start Using Date"></asp:Literal>   
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Room No
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="ltRoomNo" runat="server" Text="Start Using Date"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Start Using Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtSUsingDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 Csscalender" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSUsingDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;"  />
                            </td>
                            <th>
                                <asp:Literal ID="Literal2" runat="server" Text="End Using Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtEUsingDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 Csscalender" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtEUsingDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            </td>
                            
                        </tr>
                        
                        <tr>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Pay Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtPayDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            </td>
                            <th>
                                <asp:Literal ID="Literal6" runat="server" Text="Issuing Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtIssueDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtIssueDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal8" runat="server" Text="Exchange Rate"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtExchangeRate" runat="server" MaxLength="20" Width="100" 
                                    CssClass="bgType2" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                                     
                            </td>
                            <th>
                                 <asp:Literal ID="Literal5" runat="server" Text="Fee Amount"></asp:Literal>
                            </th>
                            <td>
                                 <asp:TextBox ID="txtFeeAmount" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType2"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>                                     
                            </td>                            
                        </tr> 
                        <tr>
                            <th>
                                <asp:Literal ID="Literal7" runat="server" Text="Discount"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtDiscount" runat="server" MaxLength="20" Width="100" 
                                    CssClass="bgType2" Text="0"></asp:TextBox>                                     
                            </td>                               
                            <th>
                                <asp:Literal ID="Literal4" runat="server" Text="Total Amount"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtTotal" runat="server" MaxLength="20" Width="100" 
                                    CssClass="bgType2" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                                     
                            </td>                            
                        </tr>   
                         <tr>
                            <th>
                                <asp:Literal ID="Literal9" runat="server" Text="Sub Description"></asp:Literal>
                            </th>
                            <td colspan="3" >
                                <asp:TextBox ID="txtSubDes" runat="server" MaxLength="22" Width="400" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
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
                                    <asp:LinkButton ID="lnkbtnWrite" runat="server" Text="Save" 
                                    onclick="lnkbtnWrite_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" 
                                    onclick="lnkbtnCancel_Click" ></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hfSeq" runat="server" />
<asp:HiddenField ID="hfRentCd" runat="server" />
<asp:HiddenField ID="hfItemRentCd" runat="server" />
<asp:HiddenField ID="hfFeeTy" runat="server" />
<asp:HiddenField ID="hfPaymentDt" runat="server" />
<asp:HiddenField ID="hfPaymentSeq" runat="server" />
<asp:HiddenField ID="hfPaymentDetSeq" runat="server" />
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="UtilFeeWriteOverTime.aspx.cs" Inherits="KN.Web.Management.Remote.UtilFeeWriteOverTime" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
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

    function fnSelectCheckValidate(strText)
    {


        return true;
    }

    function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd,strSquare)
    {
         strCompNmS = $('#<%=txtTitle.ClientID %>').val();
        window.open("/Common/Popup/PopupCompAirConFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd +"&SquareS=" +strSquare , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
        
        return false;
    }


    function loadCalendar() {
        $(".Csscalender").monthpicker();
        $('#<%=txtDueDt.ClientID%>').datepicker();
        $("#<%=txtRequestDt.ClientID%>").datepicker({
            onSelect: function (dateText, inst) {
                var date2 = $('#<%=txtRequestDt.ClientID%>').datepicker('getDate');
                date2.setDate(date2.getDate() + 7);                 
                $('#<%=txtDueDt.ClientID%>').datepicker('setDate', date2);
            }
        });
        
        $('#<%=txtTitle.ClientID %>').keydown(function(e) {          
           var code = e.keyCode || e.which;
           if (code == '9') {             
               $('#<%=imgbtnSearchCompNm.ClientID %>').click();
               
           return false;
           }            
        });
        
        $("#<%=lnkbtnDelete.ClientID %>").bind("click", function () {
            ShowLoading("Deleting data ......");
        });  
    }

    $(document).ready(function () {
        loadCalendar();
        loadCompanyName();
    });

    function SaveSuccess() {
        CloseLoading();
        alert('Save Successful !');
        if (confirm('Do you want continue ?')) {
              location.reload();  
        } else {
            <%=Page.GetPostBackEventReference(lnkbtnCancel)%>;
        }
    }

    function addDueDt() {
        var date2 = $('#<%=txtRequestDt.ClientID%>').datepicker('getDate');
        if (date2==null||date2=="") {
            return;
        }
        date2.setDate(date2.getDate() + 7);
        $('#<%=txtDueDt.ClientID%>').datepicker('setDate', date2);
    }

    function DeleteSuccess() {   
        CloseLoading();
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

    function fnValidate(strText) {
        
        var hfReturnUserSeqId = $('#<%=HfReturnUserSeqId.ClientID %>').val();
        if (hfReturnUserSeqId =="") {
            alert(strText);
            $('#<%=txtTitle.ClientID %>').focus();
            return false;
        }          
        var roomNo = $('#<%=txtRoomNo.ClientID %>').val();
        if (roomNo =="") {
            alert(strText);
            $('#<%=txtTitle.ClientID %>').focus();
            return false;
        }
        var unitPrice = $('#<%=txtUnitPrice.ClientID %>').val();
        if (unitPrice =="") {
            alert(strText);
            $('#<%=txtUnitPrice.ClientID %>').focus();
            return false;
        }    
        var txtPeriod = $('#<%=txtPeriod.ClientID %>').val();
        if (txtPeriod =="") {
            alert(strText);
            $('#<%=txtPeriod.ClientID %>').focus();
            return false;
        }       
        var txtRequestDt = $('#<%=txtRequestDt.ClientID %>').val();
        if (txtRequestDt =="") {
            alert(strText);
            $('#<%=txtRequestDt.ClientID %>').focus();
            return false;
        }     
        var txtDueDt = $('#<%=txtDueDt.ClientID %>').val();
        if (txtDueDt =="") {
            alert(strText);
            $('#<%=txtDueDt.ClientID %>').focus();
            return false;
        }           
        ShowLoading("Saving data ......");        
        return true;        
    }

    function loadCompanyName() {
        $('#<%=txtRoomNo.ClientID %>').live('change',function () {
        });  
    }
    function callBack(compNm,rentCd,roomNo,userSeq,square) {                    
    }       
//-->
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
                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="1000" Width="505"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Room No
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 " Text=""></asp:TextBox> 
                            </td>

                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal2" runat="server" Text="Unit Price"></asp:Literal>
                            </th>
                            <td >
                                 <asp:TextBox ID="txtUnitPrice" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType2"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>                                                                 
                            </td> 
                            <th>
                                <asp:Literal ID="Literal5" runat="server" Text="Square"></asp:Literal>
                            </th>
                            <td >
                                 <asp:TextBox ID="txtSquare" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType3"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>&nbsp;m2                                                                 
                            </td>                                                         
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Period"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtPeriod" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 Csscalender" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPeriod.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;"  />
                            </td>
                            <th>
                                <asp:Literal ID="Literal4" runat="server" Text="Hours Over Time"></asp:Literal>
                            </th>
                            <td >
                                 <asp:TextBox ID="txtHoursOver" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType3"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>                                                                 
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Request Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtRequestDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRequestDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            </td>
                            <th>
                                <asp:Literal ID="Literal6" runat="server" Text="Due Date"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtDueDt" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtDueDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal7" runat="server" Text="Payment Type"></asp:Literal>
                            </th>
                            <td >
                                <asp:DropDownList ID="ddlPaymentType" runat="server" >
                                    <asp:ListItem Value="USD" Selected="True">USD</asp:ListItem>
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                </asp:DropDownList>                                 
                            </td>
                            <th>
                                <asp:Literal ID="Literal8" runat="server" Text="Exchange Rate"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtExchangeRate" runat="server" MaxLength="20" Width="100" 
                                    CssClass="bgType2" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                                     
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal9" runat="server" Text="Discount"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtDisCount" runat="server" MaxLength="20" Width="100" 
                                    CssClass="bgType3" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                          
                            </td>
                            <th>
                                <asp:Literal ID="Literal10" runat="server" Text="Include VAT"></asp:Literal>
                            </th>
                            <td>
                                <asp:CheckBox ID="chkIncludeVat" runat="server" />
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal11" runat="server" Text="Sub Description"></asp:Literal>
                            </th>
                            <td colspan="3" >
                                <asp:TextBox ID="txtSubDes" runat="server" MaxLength="22" Width="400" 
                                    CssClass="bgType2" ></asp:TextBox>                                     
                            </td>           
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
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" 
                                    onclick="lnkbtnDelete_Click" Visible="False"></asp:LinkButton></span>
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
<asp:LinkButton ID="lnkReload" runat="server"  onclick="lnkReload_Click" Visible="False"></asp:LinkButton> 
<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txthfChargeSeq" runat="server" Visible="false" Text=""></asp:TextBox>
<asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>

</asp:Content>
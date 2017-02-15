<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="UtilFeeWrite.aspx.cs" Inherits="KN.Web.Management.Remote.UtilFeeWrite" ValidateRequest="false"%>
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
    
    function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd)
    {
         strCompNmS = $('#<%=txtTitle.ClientID %>').val();
        window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
        
        return false;
    }
    

    function fnCheckValidate(strText)
    {       
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
        var txtSUsingDt = $('#<%=txtSUsingDt.ClientID %>').val();
        if (txtSUsingDt =="") {
            alert(strText);
            $('#<%=txtSUsingDt.ClientID %>').focus();
            return false;
        }
        var txtEUsingDt = $('#<%=txtEUsingDt.ClientID %>').val();
        if (txtEUsingDt =="") {
            alert(strText);
            $('#<%=txtEUsingDt.ClientID %>').focus();
            return false;
        }                
        var txtFistIndex = $('#<%=txtFistIndex.ClientID %>').val();
        if (txtFistIndex =="") {
            alert(strText);
            $('#<%=txtFistIndex.ClientID %>').focus();
            return false;
        }  
        
        var txtLastIndex = $('#<%=txtLastIndex.ClientID %>').val();
        if (txtLastIndex =="") {
            alert(strText);
            $('#<%=txtLastIndex.ClientID %>').focus();
            return false;
        }         
        var txtNormalHours = $('#<%=txtNormalHours.ClientID %>').val();
        if (txtNormalHours =="") {
            alert(strText);
            $('#<%=txtNormalHours.ClientID %>').focus();
            return false;
        } 
        var txtRequestDt = $('#<%=txtRequestDt.ClientID %>').val();
        if (txtRequestDt =="") {
            alert(strText);
            $('#<%=txtRequestDt.ClientID %>').focus();
            return false;
        }      
        ShowLoading("Saving data ......");
        return true;
    }


    function loadCalendar() {
        $(".Csscalender").datepicker();
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
            return true;
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
    function loadCompanyName() {
        $('#<%=txtRoomNo.ClientID %>').live('blur', function() {
             <%=Page.GetPostBackEventReference(lnkLoadOldRoom)%>;
        });
    }

    function countIndex() {
        var fistIndex = $('#<%=txtFistIndex.ClientID %>').val().trim();
        var endIndex = $('#<%=txtLastIndex.ClientID %>').val().trim();
        if (fistIndex!=""&& endIndex!="") {
           if(parseFloat(endIndex) < parseFloat(fistIndex)) {
               alert('End Index must be bigger than fist index !');
               //$('#<%=txtLastIndex.ClientID %>').focus();
           } else {
               $('#<%=txtNormalHours.ClientID %>').val((parseFloat(endIndex) - parseFloat(fistIndex)).toFixed(2));
           }
        }
    }
    function callBack(compNm,rentCd,roomNo,userSeq) {
        $('#<%=txthfUserSeq.ClientID %>').val(userSeq);
       <%=Page.GetPostBackEventReference(lnkLoadOldRoom)%>;
        return false;
    }    
//-->
</script>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkLoadOldRoom" EventName="Click" />       
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
                                <asp:Literal ID="Literal4" runat="server" Text="Fist Index"></asp:Literal>
                            </th>
                            <td >
                                 <asp:TextBox ID="txtFistIndex" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType2"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>                                                                 
                            </td>
                            <th>
                                 <asp:Literal ID="Literal5" runat="server" Text="End Index"></asp:Literal>
                            </th>
                            <td>

                                 <asp:TextBox ID="txtLastIndex" runat="server" maxlength="18" Width="100" 
                                     CssClass="bgType2"  onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" ></asp:TextBox>                                     
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
                                    <asp:ListItem Value="USD" >USD</asp:ListItem>
                                    <asp:ListItem Value="VND" Selected="True">VND</asp:ListItem>
                                </asp:DropDownList>                                 
                            </td>
                            <th>
                                <asp:Literal ID="Literal8" runat="server" Text="Exchange Rate"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtExchangeRate" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                                     
                            </td>
                            
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal9" runat="server" Text="Discount"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtDisCount" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2" onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                          
                            </td>
                            <th>
                                <asp:Literal ID="Literal10" runat="server" Text="Sub Description"></asp:Literal>
                            </th>
                            <td >
                                <asp:TextBox ID="txtSubDes" runat="server" MaxLength="22" Width="300" 
                                    CssClass="bgType2"></asp:TextBox>                                     
                            </td>
                            
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table class="TbCel-Type4-A">
                <colgroup>
                    <col width="185px" />
                    <col width="185px" />
                    <col width="185px" />
                    <col width="185px" />
                    <col width="80px" />
                    <tbody>
                        <tr>
                            <th align="center" class="P0">
                                <asp:Literal ID="ltTopRentalFeeStartDt" runat="server" Text="Normal Hours"></asp:Literal>
                            </th>
                            <th align="center" class="P0">
                                <asp:Literal ID="ltTopRentalFeeEndDt" runat="server" Text="Rush Hours"></asp:Literal>
                            </th>
                            <th align="center" class="P0">
                                <asp:Literal ID="ltTopRentalFeeRate" runat="server" Text="Low Hours"></asp:Literal>
                            </th>
                            <th align="center" class="P0">
                                <asp:Literal ID="ltTopRentalFeeAmt" runat="server" Text="Other"></asp:Literal>
                            </th>
                            <th align="center" class="P0">
                            </th>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table class="TbCel-Type4-A" id="tblListRentFee">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
       
                <tbody>                                                            
                </tbody>
            </colgroup>
            </table>                    
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="185px" />
                    <col width="185px" />
                    <col width="185px" />
                    <col width="185px" />
                    <col width="80px" />
                    <tbody>
                        <tr>
                            <input type="hidden" id="rentfeeID" value=""/>
                            <td align="center" class="P0">                                
                                <asp:TextBox ID="txtNormalHours" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 " onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>  
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtHightUsing" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 " onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtLowUsing" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 " onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>
                                
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtNormalOtherUsing" runat="server" MaxLength="10" Width="100" 
                                    CssClass="bgType2 " onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" Text="0"></asp:TextBox>                                       
                            </td>
                        <td align="center" class="P0">

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
            <asp:ImageButton ID="lnkLoadOldRoom" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="lnkLoadOldRoom_Click" />
            <asp:TextBox ID="txthfUserSeq" runat="server" Visible="false" Text=""></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:LinkButton ID="lnkReload" runat="server"  onclick="lnkReload_Click" Visible="False"></asp:LinkButton> 

<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txthfChargeSeq" runat="server" Visible="false" Text="0"></asp:TextBox>

<asp:TextBox ID="txthfUSeq" runat="server" Visible="false" Text=""></asp:TextBox>
<asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>

</asp:Content>
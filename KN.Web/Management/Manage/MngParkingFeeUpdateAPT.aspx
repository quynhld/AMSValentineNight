<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngParkingFeeUpdateAPT.aspx.cs" Inherits="KN.Web.Management.Manage.MngParkingFeeUpdateAPT" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
            onLoadinit();
        }
    }       
    <!--//
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            return false;
        }
    }

    function fnSearchCheckValidate(strText) {
            var strYear = document.getElementById("<%=ddlSearchYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlSearchMonth.ClientID%>");
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strCarNo = document.getElementById("<%=txtSearchCarNo.ClientID%>");
            var strCardNo = document.getElementById("<%=txtSearchCardNo.ClientID%>");

            if (trim(strYear.value) == "" && trim(strMonth.value) == "" && trim(strSearchRoom.value) == "" && trim(strCarNo.value) == "" && trim(strCardNo.value) == "") {
                alert(strText);
                return false;
            }
            ShowLoading("Loading data....!");
            return true;
        }


    function fnRentChange()
    {


        return false;
    }

    function fnCardNoChange()
    {

        return false;
    }

    function fnCalendarChange()
    {
        return false;
    }

    function fnOpenCalendar(obj)
    {

        return false;
    }

    function fnCheckValidate(strTxt)
    {
   
        var txtAmount = $('#<%=txtAmount.ClientID %>').val();
        if (txtAmount =="") {
            alert(strTxt);
            $('#<%=txtAmount.ClientID %>').focus();
            return false;
        }    
        var txtPayDay = $('#<%=txtPayDay.ClientID %>').val();
        if (txtPayDay =="") {
            alert(strTxt);
            $('#<%=txtPayDay.ClientID %>').focus();
            return false;
        }   
        var ddlPaymentTy = $('#<%=ddlPaymentTy.ClientID %>').val();
        if (ddlPaymentTy =="0003") {
             var ddlTransfer = $('#<%=ddlTransfer.ClientID %>').val();
            if (ddlTransfer =="") {
                alert(strTxt);
                $('#<%=ddlTransfer.ClientID %>').focus();
                return false;
            }              
        }
        ShowLoading("Saving payment .....! ");
        return true;
    }

    function fnMonthlyParkingList() {
            var strData1 = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
            var strData2 = document.getElementById("<%=ddlSearchMonth.ClientID%>").value;
            var strData3 = document.getElementById("<%=ddlRentCd.ClientID%>").value;
            var strData4 = document.getElementById("<%=ddlCarTy.ClientID%>").value;

            window.open("/Common/RdPopup/RDPopupAcntMMParkingFeeList.aspx?Datum0=" + strData1 + "&Datum1=" + strData2 + "&Datum2=" + strData3 + "&Datum3=" + strData4, "MonthlyParkingList", "status=no, resizable=no, width=740, height=700, left=100, top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

            return false;
        }

   
    
    $(document).ready(function () {
        callCalendar();
        onLoadinit();        
    });
    

    function onLoadinit() {
        $('#<%=ddlPaymentTy.ClientID %>').change(function() {
         // alert($(this).val());
        });   
        $('#<%=rbMoneyCd.ClientID %>').change(function() {
         var moneyCd = $('#<%=rbMoneyCd.ClientID %> input:radio:checked').val();
            if (moneyCd=="VND") {
                $('#<%=txtExRate.ClientID %>').attr('readonly','readonly');
                $('#<%=txtExRate.ClientID %>').val("");
            } else {
                $('#<%=txtExRate.ClientID %>').removeAttr('readonly');
            }
        });
        formatMoney();
    }

    function formatMoney() {

        $('#<%=txtAmount.ClientID %>,#<%=txtExRate.ClientID %>').blur(function() {
            var inputAmt = $('#<%=txtAmount.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
            var exchangeRt = $('#<%=txtExRate.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
            var moneyCd = $('#<%=rbMoneyCd.ClientID %> input:radio:checked').val();
            if (moneyCd == "VND") {
                $('#<%=txtRealAmount.ClientID %>').val($('#<%=txtAmount.ClientID %>').val());
            } else {
                if ($('#<%=txtExRate.ClientID %>').val() == "") {
                    $('#<%=txtRealAmount.ClientID %>').val($('#<%=txtAmount.ClientID %>').val());
                } else {

                    var amount = (parseFloat(inputAmt)) * parseFloat(exchangeRt);
                    $('#<%=txtRealAmount.ClientID %>').val(amount);
                    //alert(amount);
                }
            }
            $('#<%=txtAmount.ClientID %>').formatCurrency();
            $('#<%=txtRealAmount.ClientID %>').formatCurrency();
            $('#<%=txtExRate.ClientID %>').formatCurrency();
        });

        $('#<%=txtAmount.ClientID %>,#<%=txtExRate.ClientID %>').keypress(function() {

        });
    }

    function SaveSuccess() {
        alert('Save Successful !');         
    }   
    function fnDetailView(trow,strSeq,roomNo,userSeq,refSeq,reAmount,feeTy,feeTyDt,pInvoice,period)
    {
        resetTable();
        $(trow).addClass('rowSelected');
        document.getElementById("<%=hfSeq.ClientID%>").value = strSeq;
        document.getElementById("<%=hfRoomNo.ClientID%>").value = roomNo;
        document.getElementById("<%=hfUserSeq.ClientID%>").value = userSeq;
        document.getElementById("<%=hfRef_Seq.ClientID%>").value = refSeq;
        document.getElementById("<%=txtReceiveAmount.ClientID%>").value = reAmount;
        document.getElementById("<%=hfFeeTy.ClientID%>").value = feeTy;
        document.getElementById("<%=hfBillCdDt.ClientID%>").value = feeTyDt;
        document.getElementById("<%=hfPInvoice.ClientID%>").value = pInvoice;
        document.getElementById("<%=hfPeriod.ClientID%>").value = period;
        $('#<%=txtReceiveAmount.ClientID %>').formatCurrency();
        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
    }

    function resetTable() {
        $("div#<%=upSearch.ClientID %> tbody").find("tr").each(function() {
            //get all rows in table
            $(this).removeClass('rowSelected');
        });  
    }

    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

    function ReLoadData(invoiceNo) {
        if(invoiceNo==null || invoiceNo =="") {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        } else {
           window.open("/Common/RdPopup/RDPopupAdjustmentA0000InvoiceAPT.aspx?Datum0=" + invoiceNo, "Reciept", "status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
        }
    }

    function SendFunction(invoiceNo,amountSend) {
        if(invoiceNo==null || invoiceNo =="") {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        } else {
           window.open("/Common/RdPopup/RDPopupAdjustmentA0000InvoiceAPT.aspx?Datum0=" + invoiceNo + "&Datum1=" + amountSend, "Reciept", "status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
        }
    }

    function fnLoadData(invoiceNo) 
    {
        document.getElementById("<%=hfInvoice.ClientID%>").value = invoiceNo;
        <%=Page.GetPostBackEventReference(imgUpdateInvoice)%>;
    }    
//-->
   
</script>
<style type="text/css">
    .rowSelected 
    {
        background-color: #E4EEF5
    }
    .rowHover 
    {
        background-color: #E4EEF5
    }    
</style>
<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
        <ul class="sf5-ag MrgL30 ">
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlRentCd" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCarTy" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSearchYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSearchMonth" runat="server">
                    </asp:DropDownList>
                </div>
            </li>
            <li>
                <div class="Btn-Type1-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnReport" runat="server" OnClientClick="javascript:return fnMonthlyParkingList();"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="Btn-Type1-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>            
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li>
                <asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltSearchCarNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCarNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltSearchCardNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCardNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>

             <li>
                <div class="TpAtit1">
                    <div class="FloatR">
                        (<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal
                            ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate"
                                runat="server" Visible="false"></asp:TextBox></div>
                </div>
            </li>
        </ul>

</fieldset>
<table  class="TbCel-Type6" style="margin-bottom: 0px;">
                    <col width="10% "/>
                    <col width="35%"/>
                    <col width="15%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
    <thead>
        <tr>
            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal></th>
            <th><asp:Literal ID="ltTopName" runat="server" Text="Tenant Name"></asp:Literal></th>
            <th><asp:Literal ID="ltTopFeeTy" runat="server" Text="Fee"></asp:Literal></th>
            <th><asp:Literal ID="ltPayLimitDay" runat="server" Text="Period"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPay" runat="server" Text="Charge"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPaid" runat="server" Text="Balance"></asp:Literal></th>
            <th><asp:Literal ID="ltPayNoPaid" runat="server" Text="Is Paid"></asp:Literal></th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:210px;width:840px;border-bottom: solid 2px rgb(182, 182, 182)">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgUpdateInvoice" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
       
        <asp:PostBackTrigger ControlID="lnkExportExcel"/>       
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvPaymentList_ItemCreated" OnItemDataBound="lvPaymentList_ItemDataBound"
                OnItemUpdating="lvPaymentDetails_ItemUpdating" OnItemDeleting="lvPaymentDetails_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="10% "/>
                    <col width="35%"/>
                    <col width="15%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="cursor:pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)" onclick="javascript:return fnDetailView(this,'<%#Eval("Seq")%>','<%#Eval("RoomNo")%>','<%#Eval("UserSeq")%>','<%#Eval("Ref_Seq")%>','<%#Eval("ReAmount")%>','<%#Eval("Fee_Type")%>','<%#Eval("Fee_TypeDetail")%>','<%#Eval("P_InvoiceNo")%>','<%#Eval("StartDt")%>')">
                    <td align="center">
                        <asp:Literal ID="ltRoom" runat="server"></asp:Literal>
                        <asp:TextBox ID="txthfSeq" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txthfRentCd" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txthfPInvoice" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsName" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltFeeTy" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPayDay" runat="server"></asp:Literal>
                    </td>                                                                                            
                    <td align="center">
                        <asp:Literal ID="ltInsTotalPay" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsPaidAmt" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:CheckBox ID="chkReceitCd" runat="server" class="bd0"></asp:CheckBox>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <asp:HiddenField ID="hfSelectedLine" runat="server" />
        <asp:HiddenField ID="hfInvoice" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="ltCoInfo" runat="server" Text="Details Payment"></asp:Literal></div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="Literal8" runat="server" Text="Receivable Amount"></asp:Literal></div>
</div>

<table  class="TbCel-Type6" style="margin-bottom: 0px;width: 50%;float: left">
                   
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="15%"/>
    <thead>
        <tr>
          
            <th>
                <asp:Literal ID="Literal9" runat="server" Text="Pay Day"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="Literal11" runat="server" Text="Paid Type"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="Literal3" runat="server" Text="Pay Amount"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="Literal13" runat="server" Text="Revert"></asp:Literal>
            </th>  
            <th>
               &nbsp;
            </th>
        </tr>
    </thead>
</table>
<table  class="TbCel-Type6" style="margin-bottom: 0px;width: 49.5%;float: left">
   <col width="25%"/>
   <col width="25%"/>
   <col width="25%"/>
   <col width="25%"/>
    <thead>
        <tr>
            <th>
                <asp:Literal ID="lt1" runat="server" Text="Fee"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="lt2" runat="server" Text="Period"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="Literal4" runat="server" Text="Balance"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="Literal10" runat="server" Text="Pay Day"></asp:Literal>
            </th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:80px;width:50%;float: left">
<asp:UpdatePanel ID="upListPayMent" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>
        
         <asp:PostBackTrigger ControlID="lnkExportExcel"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvPaymentDetails" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvPaymentDetails_ItemCreated" OnItemDataBound="lvPaymentDetails_ItemDataBound"
                OnItemUpdating="lvPaymentDetails_ItemUpdating" OnItemDeleting="lvPaymentDetails_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA" style="width: 100%">
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="15%"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>                   
                    <td align="center">
                        <asp:Literal ID="ltPayDay" runat="server"></asp:Literal>
                        <asp:TextBox ID="txthfSeq" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtPSeq" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPaidTy" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPayAmount" runat="server"></asp:Literal>
                    </td>   
                    <td align="center">
                        <asp:Literal ID="ltRevertAmt" runat="server"></asp:Literal>
                    </td>                                                                                        
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton AlternateText="Revert Payment" CommandName="Update" ID="imgbtnPrint" ImageUrl="~/Common/Images/Icon/Revert.png" runat="server" ToolTip="Revert Payment" />                            
                            <asp:ImageButton AlternateText="Delete this row " CommandName="Delete" ID="imgbtnDelete" ImageUrl="~/Common/Images/Icon/Trash.gif" runat="server" ToolTip="Delete Payment" />
                        </span>                      
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA" style="width: 100%">
                    <tbody>
                    <tr>
                        <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<div style="overflow-y:scroll;overflow-x:hidden;height:80px;width:49.5%; float: left">
<asp:UpdatePanel ID="upReceiveable" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>        
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>
        <asp:PostBackTrigger ControlID="lnkExportExcel"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvReceivable" runat="server" 
            ItemPlaceholderID="iphItemPlaceHolderID" 
            onitemcreated="lvReceivable_ItemCreated" 
            onitemdatabound="lvReceivable_ItemDataBound">
            <LayoutTemplate>
                <table class="TypeA" style="width: 100%">
                       <col width="25%"/>
                       <col width="25%"/>
                       <col width="25%"/>
                       <col width="25%"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:Literal ID="ltFeeNm" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPayday" runat="server"></asp:Literal>
                    </td>                    
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA" style="width: 100%">
                    <tbody>
                    <tr>
                        <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</div>
        <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
                
                 <asp:PostBackTrigger ControlID="lnkExportExcel"/>
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0"  class="TbCel-Type2" style="margin-bottom: 10px;">
                    <tr>
                        <th align="center"><asp:Literal ID="ltRegCarTy" runat="server" Text="Payment Type"></asp:Literal></th>
                        <td><asp:DropDownList ID="ddlPaymentTy" runat="server" AutoPostBack="True"
                                onselectedindexchanged="ddlPaymentTy_SelectedIndexChanged"></asp:DropDownList></td>
	                    <th align="center"><asp:Literal ID="ltBankAcc" runat="server" Text="Bank Account"></asp:Literal></th>
                        <td colspan="5">
                             <asp:DropDownList ID="ddlTransfer" runat="server" Enabled="False"></asp:DropDownList> 
                        </td>
	                </tr>
                    <tr>
	                    <th align="center">
	                        <asp:Literal ID="ltRegCardFee" runat="server" Text="Unit"></asp:Literal>
	                    </th>
                        <td >
                            <asp:RadioButtonList ID="rbMoneyCd" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="VND" Selected="True">VND</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                            </asp:RadioButtonList>                             
                        </td>                        
	                    <th align="center">
	                        <asp:Literal ID="ltRecei" runat="server" Text="Receivable Amount"></asp:Literal>
	                    </th>
                        <td>
                            <asp:TextBox ID="txtReceiveAmount" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltPaydt" runat="server" Text="Pay Date"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtPayDay" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDay.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                        </td>
                    </tr>
                    <tr>
           	            <th align="center">
           	                <asp:Literal ID="ltAmount" runat="server" Text="Amount"></asp:Literal>
           	            </th>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                        </td>
           	            <th align="center">
           	                <asp:Literal ID="Literal6" runat="server" Text="Exchange Rate"></asp:Literal>
           	            </th>
                        <td>
                            <asp:TextBox ID="txtExRate" runat="server" CssClass="bgType2" MaxLength="18" Width="100" ReadOnly="True"></asp:TextBox>
                        </td>
           	            <th align="center">
           	                <asp:Literal ID="Literal7" runat="server" Text="Real Amount"></asp:Literal>
           	            </th>
                        <td>
                            <asp:TextBox ID="txtRealAmount" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="Btwps FloatR2">
                    <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Save"></asp:LinkButton></span>
	                            </div>
                            </div>
                        </div>
                    </div>
                    <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkAdjustment" runat="server" OnClick="lnkAdjustment_Click" Text="Adjustment"></asp:LinkButton></span>
	                            </div>
                            </div>
                        </div>
                    </div>
                      <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkAdjBalance" runat="server" 
                                        OnClick="lnkAdjBalance_Click" Text="Adj Balance"></asp:LinkButton></span>
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
                    <div class="Btn-Type3-wp ">            
                        <div class="Btn-Tp2-L">
                            <div class="Btn-Tp2-R">
                                <div class="Btn-Tp2-M">
                                    <span>
                                    <asp:LinkButton ID="lnkExportExcel" runat="server" 
                                        Text="Export Excel" onclick="lnkExportExcel_Click"></asp:LinkButton></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfUserSeq" runat="server" />
                <asp:HiddenField ID="hfSeq" runat="server" Value=""/>
                 <asp:HiddenField ID="hfFeeTy" runat="server" Value=""/>
                <asp:HiddenField ID="hfRef_Seq" runat="server" Value=""/>
                <asp:HiddenField ID="hfRoomNo" runat="server" Value=""/>
                 <asp:HiddenField ID="hfSeqDt" runat="server" Value="0"/>
                 <asp:HiddenField ID="hfBillCdDt" runat="server" Value=""/>
                 <asp:HiddenField ID="hfRentCd" runat="server" Value=""/>
                 <asp:HiddenField ID="hfPInvoice" runat="server" Value=""/>
                 <asp:HiddenField ID="hfPeriod" runat="server" Value=""/>

            </ContentTemplate>
        </asp:UpdatePanel>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>      
<asp:ImageButton ID="imgbtnDetailPayment" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailPayment_Click"/>   
<asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgUpdateInvoice_Click"/>       
</asp:Content>

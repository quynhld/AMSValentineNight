<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngPaymentRenovationAPT.aspx.cs" Inherits="KN.Web.Management.Manage.MngPaymentRenovationAPT" %>
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

    function fnSelectCheckValidate(strText)
    {
      var roomNo = $('#<%=txtRoomNo.ClientID %>').val();
        if (roomNo =="") {
            alert(strText);
            $('#<%=txtRoomNo.ClientID %>').focus();
            return false;
        }        
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
        var ddlFeeType = $('#<%=ddlFeeTypeR.ClientID %>').val();
        if (ddlFeeType =="") {
                alert(strTxt);
                $('#<%=ddlFeeTypeR.ClientID %>').focus();
                return false;                         
        }   

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
        return true;
    }
    
    $(document).ready(function () {
        callCalendar();
        onLoadinit();
        
    });

    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").datepicker({  
        });  
        $("#<%=txtESearchDt.ClientID %>").datepicker({  
        });          
        $("#<%=txtPayDay.ClientID %>").datepicker({  
        });    
        $("#<%=txtPayDay.ClientID %>").datepicker("setDate", new Date());        
    }

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

    function ReLoadData() {
        document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
    }

    function fnConfirmR(strText) {
        if(confirm(strText)) {
            return true;
        } else {
            return false;
        }

    }

    function reLoadData(refSeq) {            
        window.open("/Common/RdPopup/RDPopupReciptRenovationAPT.aspx?Datum0=" + refSeq + "&Datum1=2", "KeangNamReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        ReLoadData();
        return false;        
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
    <ul class="sf5-ag MrgL30">
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlFeeType" runat="server"></asp:DropDownList></li>   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Customer Name :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px" runat="server"></asp:TextBox></li>        
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Period :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li><b><asp:Literal ID="Literal12" runat="server" Text="~"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtESearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>                                
        <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>		        
        </li>         
     </ul>

</fieldset>
<table  class="TbCel-Type6" style="margin-bottom: 0px;">
                    <col width="10% "/>
                    <col width="30%"/>
                    <col width="20%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
    <thead>
        <tr>
            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal></th>
            <th><asp:Literal ID="ltTopName" runat="server" Text="Tenant Name"></asp:Literal></th>
            <th><asp:Literal ID="ltTopFeeTy" runat="server" Text="Fee"></asp:Literal></th>
            <th><asp:Literal ID="ltPayLimitDay" runat="server" Text="Receipt Date"></asp:Literal></th>
            <th><asp:Literal ID="ltReturnDt" runat="server" Text="Return Date"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPaid" runat="server" Text="Amount"></asp:Literal></th>
            <th><asp:Literal ID="ltPayNoPaid" runat="server" Text=""></asp:Literal></th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:210px;width:840px;border-bottom: solid 2px rgb(182, 182, 182)">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>             
        <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>  
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvPaymentList_ItemCreated" OnItemDataBound="lvPaymentList_ItemDataBound"
                OnItemUpdating="lvPaymentDetails_ItemUpdating" OnItemDeleting="lvPaymentDetails_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="10% "/>
                    <col width="30%"/>
                    <col width="20%"/>
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
                <tr style="cursor:pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)">
                    <td align="center">
                        <asp:Literal ID="ltRoom" runat="server"></asp:Literal>
                        <asp:TextBox ID="txthfSeq" runat="server" Visible="False"></asp:TextBox>
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
                        <asp:Literal ID="ltReturnDate" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsTotalAmt" runat="server"></asp:Literal>
                    </td>
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton AlternateText="Reprint Receive" CommandName="Update" ID="imgbtnPrint" ImageUrl="~/Common/Images/Icon/print.gif" runat="server" />                            
                            <asp:ImageButton AlternateText="Delete this row " CommandName="Delete" ID="imgbtnDelete" ImageUrl="~/Common/Images/Icon/Trash.gif" runat="server" />
                        </span>                      
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
        <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="ltCoInfo" runat="server" Text="Details Payment"></asp:Literal></div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="Literal8" runat="server" Text="Receivable Amount"></asp:Literal></div>
</div>

        <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>                
                <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0"  class="TbCel-Type2" style="margin-bottom: 10px;">
                    <tr>
                        <th align="center"><asp:Literal ID="Literal3" runat="server" Text="Payment Type"></asp:Literal></th>
                        <td><asp:DropDownList ID="ddlFeeTypeR" runat="server"></asp:DropDownList></td>
	                    <th align="center"><asp:Literal ID="Literal4" runat="server" Text="Card No"></asp:Literal></th>
                        <td colspan="5">
                             <asp:TextBox ID="txtCarcardNo" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                        </td>
	                </tr>                    
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
	                        <asp:Literal ID="ltRecei" runat="server" Text="Room :"></asp:Literal>
	                    </th>
                        <td>
                            <asp:TextBox ID="txtRomNoR" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                        </td>                        
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
                                    <span>
                                        <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
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
                  <asp:HiddenField ID="hfPaymentCd" runat="server" Value=""/>
                  

            </ContentTemplate>
        </asp:UpdatePanel>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>      
          
</asp:Content>

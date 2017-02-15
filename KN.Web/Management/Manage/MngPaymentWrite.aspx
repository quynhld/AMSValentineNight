<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngPaymentWrite.aspx.cs" Inherits="KN.Web.Management.Manage.MngPaymentWrite" %>
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
        $('#<%=lnkbtnRegist.ClientID %>').css('display','none');
        $('#<%=lnkAdjustment.ClientID %>').css('display','none');
        ShowLoading("Loading data ....!");
        
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

        ShowLoading("Saving data ....!");
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
        $('#<%=txtCompanyNm.ClientID %>').keydown(function(e) {          
           var code = e.keyCode || e.which;
           if (code == '9') {             
               $('#<%=imgbtnSearchCompNm.ClientID %>').click();
               
           return false;
           }
            return true;
        });         

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
                }
            }
            $('#<%=txtAmount.ClientID %>').formatCurrency();
            $('#<%=txtRealAmount.ClientID %>').formatCurrency();
            $('#<%=txtExRate.ClientID %>').formatCurrency();
        });       
    }
    function SaveSuccess() {
        alert('Save Successful !');         
    }   
    function fnDetailView(trow,strSeq,roomNo,userSeq,refSeq,reAmount,feeTy,rentCd)
    {
        resetTable();
        $('#<%=lnkbtnRegist.ClientID %>').css('display','');
        $('#<%=lnkAdjustment.ClientID %>').css('display','');   
        $(trow).addClass('rowSelected');
        document.getElementById("<%=hfSeq.ClientID%>").value = strSeq;
        document.getElementById("<%=hfRoomNo.ClientID%>").value = roomNo;
        document.getElementById("<%=hfUserSeq.ClientID%>").value = userSeq;
        document.getElementById("<%=hfRef_Seq.ClientID%>").value = refSeq;
        document.getElementById("<%=txtReceiveAmount.ClientID%>").value = reAmount;
        document.getElementById("<%=hfFeeTy.ClientID%>").value = feeTy;
        document.getElementById("<%=hfRentCd.ClientID%>").value = rentCd;
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

    function ReLoadData() {
        document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
    }
    
    function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd)
    {
         strCompNmS = $('#<%=txtCompanyNm.ClientID %>').val();
        strRentCd = document.getElementById("<%=ddlRentCd.ClientID%>").value;
        window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');        
        return false;
    }

    function callBack(compNm,rentCd,roomNo,userSeq) {
        if (rentCd == '0007' ||rentCd == '0008' ) {
            rentCd = '9900';    
        }
        if (rentCd == '0003') {
            rentCd = '0002';    
        }                
        //document.getElementById("<%=ddlRentCd.ClientID%>").value = rentCd;
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
    .hide { display:none; }      
</style>
<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">   
        <li><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></li>       
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Fee Type :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlFeeType" runat="server"></asp:DropDownList></li>   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="250px" runat="server"></asp:TextBox></li>
        <asp:ImageButton ID="imgLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgLoadData_Click"/>  
        <li><asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/></li>             
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
        <li><b><asp:Literal ID="ltInsPaidCd" runat="server" Text="Payment :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlPaidCd" runat="server"></asp:DropDownList></li>                                     
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
            <th><asp:Literal ID="ltPayLimitDay" runat="server" Text="Issue Date"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPay" runat="server" Text="Charge"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPaid" runat="server" Text="Balance"></asp:Literal></th>
            <th><asp:Literal ID="ltPayNoPaid" runat="server" Text="Is Paid"></asp:Literal></th>
        </tr>
    </thead>
</table>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlPaidCd" EventName="SelectedIndexChanged"/> 
        <asp:AsyncPostBackTrigger ControlID="imgLoadData" EventName="Click"/>       
    </Triggers>
    <ContentTemplate>
<div style="overflow-y:scroll;overflow-x:hidden;height:205px;width:840px;">

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
                <tr style="cursor:pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)" onclick="javascript:return fnDetailView(this,'<%#Eval("REF_SERIAL_NO")%>','<%#Eval("RoomNo")%>','<%#Eval("UserSeq")%>','<%#Eval("REF_SERIAL_NO")%>','<%#Eval("ReAmount")%>','<%#Eval("BillCd")%>','<%#Eval("RentCd")%>')" title="<%#Eval("TenantNm")%>">
                    <td align="center">
                        <asp:Literal ID="ltRoom" runat="server"></asp:Literal>
                        <asp:TextBox ID="txthfSeq" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txthfRentCd" runat="server" Visible="False"></asp:TextBox>
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
                        <asp:CheckBox ID="chkReceitCd" runat="server" class="bd0" Enabled="False"></asp:CheckBox>
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
        <asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>

</div>
    <table class="TypeA">
        <tr style="background-color: #E4EEF5">
            <td width="12%" align="center">
                <asp:Literal ID="Literal19" runat="server" Text="Mng Fee :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal21" runat="server" Text="Rent Fee :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal23" runat="server" Text="Utility :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal13" runat="server" Text="Parking :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal25" runat="server" Text="All :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal27" runat="server" Text="Total Paid :"></asp:Literal>
            </td>
            <td width="12%" align="center">
                <asp:Literal ID="Literal29" runat="server" Text="Balance :"></asp:Literal>
            </td>
        </tr>
    </table>
    <table class="TypeA">
        <tr style="background-color: #E4EEF5">
            <td width="12%" align="center">
                <b><asp:Literal ID="ltMFAll" runat="server" Text="0"></asp:Literal></b>
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltRFAll" runat="server" Text="0"></asp:Literal></b>
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltUAll" runat="server" Text="0"></asp:Literal></b>
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltParking" runat="server" Text="0"></asp:Literal></b> 
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltTotalAmtAll" runat="server" Text="0"></asp:Literal></b> 
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltPaidAll" runat="server" Text="0"></asp:Literal></b>
            </td>
            <td width="12%" align="center">
                <b><asp:Literal ID="ltBalanceAll" runat="server" Text="0"></asp:Literal></b>
            </td>
        </tr>
    </table>

    </ContentTemplate>
</asp:UpdatePanel>
<div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left; margin: 0px 0px "><asp:Literal ID="ltCoInfo" runat="server" Text="Details Payment"></asp:Literal></div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left; margin: 0px 0px "><asp:Literal ID="Literal8" runat="server" Text="Receivable Amount"></asp:Literal></div>
</div>

<table  class="TbCel-Type6" style="margin-bottom: 0px;width: 50%;float: left">
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="40%"/>
                    <col width="20%"/>
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
<div style="overflow-y:scroll;overflow-x:hidden;height:70px;width:50%;float: left">
<asp:UpdatePanel ID="upListPayMent" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>
         <asp:AsyncPostBackTrigger ControlID="ddlPaidCd" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvPaymentDetails" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvPaymentDetails_ItemCreated" OnItemDataBound="lvPaymentDetails_ItemDataBound"
                OnItemUpdating="lvPaymentDetails_ItemUpdating" OnItemDeleting="lvPaymentDetails_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA" style="width: 100%">
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="40%"/>
                    <col width="20%"/>
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
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton AlternateText="Reprint Receive" CommandName="Update" ID="imgbtnPrint" ImageUrl="~/Common/Images/Icon/print.gif" runat="server" />                            
                            <asp:ImageButton AlternateText="Delete this row " CommandName="Delete" ID="imgbtnDelete" ImageUrl="~/Common/Images/Icon/Trash.gif" runat="server" />
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
<div style="overflow-y:scroll;overflow-x:hidden;height:70px;width:49.5%; float: left">
<asp:UpdatePanel ID="upReceiveable" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>
         <asp:AsyncPostBackTrigger ControlID="ddlPaidCd" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>
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
                       <col width="20%"/>
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
               <%-- <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>--%>
               <%-- <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>--%>
                <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="ddlPaidCd" EventName="SelectedIndexChanged"/>
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0"  class="TbCel-Type2" style="margin-bottom: -4px;">
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
                            <asp:TextBox ID="txtExRate" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
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
		                            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Save" CssClass="hide"></asp:LinkButton></span>
	                            </div>
                            </div>
                        </div>
                    </div>
                    <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkAdjustment" runat="server" OnClick="lnkAdjustment_Click" Text="Adjustment" CssClass="hide"></asp:LinkButton></span>
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

            </ContentTemplate>
        </asp:UpdatePanel>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>      
<asp:ImageButton ID="imgbtnDetailPayment" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailPayment_Click"/>      
<div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
                                <div class="Btn-Tp3-M">
                                    <span>
                                        <asp:LinkButton ID="lnkExport" runat="server" Text="Export Excel" 
                                        onclick="lnkExport_Click"></asp:LinkButton></span>
                                </div>
                            </div>
                        </div>
                    </div>    
</asp:Content>

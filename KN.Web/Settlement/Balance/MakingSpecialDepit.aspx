<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="MakingSpecialDepit.aspx.cs" Inherits="KN.Web.Settlement.Balance.MakingSpecialDepit" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender(); 
            loadCompanyName();
            formatMoney();            
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

        return true;
    }
    
    function fnCheckValidate(strText)
    {
        var txtInputRoom = document.getElementById("<%= txtInputRoom.ClientID %>");
        if (trim(txtInputRoom.value) == "") {
            txtInputRoom.focus();
            alert(strText);
            return false;
        }          
        var ddlInputItem = document.getElementById("<%= ddlInputItem.ClientID %>");
        if (trim(ddlInputItem.value) == "") {
            ddlInputItem.focus();
            alert(strText);
            return false;
        }    
        var txtDescVi = document.getElementById("<%= txtDescVi.ClientID %>");
        if (trim(txtDescVi.value) == "") {
            txtDescVi.focus();
            alert(strText);
            return false;
        }     
        var txtInputIssDt = document.getElementById("<%= txtInputIssDt.ClientID %>");
        if (trim(txtInputIssDt.value) == "") {
            txtInputIssDt.focus();
            alert(strText);
            return false;
        }    
        var txtInputRealAmt = document.getElementById("<%= txtInputRealAmt.ClientID %>");
        if (trim(txtInputRealAmt.value) == "") {
            txtInputRealAmt.focus();
            alert(strText);
            return false;
        } 
        var txtInputAmt = document.getElementById("<%= txtInputAmt.ClientID %>");
        if (trim(txtInputAmt.value) == "") {
            txtInputAmt.focus();
            alert(strText);
            return false;
        }     
        var txtInputVat = document.getElementById("<%= txtInputVat.ClientID %>");
        if (trim(txtInputVat.value) == "") {
            txtInputVat.focus();
            alert(strText);
            return false;
        }          
        var txtInputQty = document.getElementById("<%= txtInputQty.ClientID %>");
        if (trim(txtInputQty.value) == "") {
            txtInputQty.focus();
            alert(strText);
            return false;
        }          
        var txtInputUnitPrice = document.getElementById("<%= txtInputUnitPrice.ClientID %>");
        if (trim(txtInputUnitPrice.value) == "") {
            txtInputUnitPrice.focus();
            alert(strText);
            return false;
        } 

        return true;
    }

 
    function SaveSuccess() {
        alert('Save Successful !');         
    }   
   

    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

     function LoadCalender() {
            datePicker();
         
        $('#<%=txtTitle.ClientID %>').keydown(function(e) {          
           var code = e.keyCode || e.which;
           if (code == '9') {             
               $('#<%=imgbtnSearchCompNm.ClientID %>').click();               
           return false;
           }
            return true;
        });

        }

    function formatMoney() {
        
        $('#<%=txtInputAmt.ClientID %>,#<%=txtInputRealAmt.ClientID %>,#<%=txtInputEx.ClientID %>').blur(function() {                    
            var inputRealAmt = $('#<%=txtInputRealAmt.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
            var exchangeRt = $('#<%=txtInputEx.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
           
            var itemType = $('#<%=ddlInputItem.ClientID %>').val();   

            if(exchangeRt=="") {
                exchangeRt = "1";
            }
            if(inputRealAmt=="") {
                inputRealAmt = "0";
            }
            if(itemType =="0009"){
                var amountVat = (parseFloat(inputRealAmt))* parseFloat(exchangeRt) * 0.05;
            }else{
                var amountVat = (parseFloat(inputRealAmt))* parseFloat(exchangeRt) * 0.1;
            }
            var amount = (parseFloat(inputRealAmt)) * parseFloat(exchangeRt) + amountVat;                                  
            $('#<%=txtInputVat.ClientID %>').val(amountVat); 
            $('#<%=txtInputAmt.ClientID %>').val(amount);                  
            
            $('#<%=txtInputRealAmt.ClientID %>').formatCurrency();
            $('#<%=txtInputEx.ClientID %>').formatCurrency();
            $('#<%=txtInputVat.ClientID %>').formatCurrency();
            $('#<%=txtInputAmt.ClientID %>').formatCurrency();
        });
        
        $('#<%=rbMoneyCd.ClientID %>').change(function() {
         var moneyCd = $('#<%=rbMoneyCd.ClientID %> input:radio:checked').val();
            if (moneyCd=="VND") {
                $('#<%=txtInputEx.ClientID %>').val("1");
            }
            makeMoney();
        });
    }
    function makeMoney() {
        var inputRealAmt = $('#<%=txtInputRealAmt.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
        var exchangeRt = $('#<%=txtInputEx.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
        var itemType = $('#<%=ddlInputItem.ClientID %>').val();   
        if(exchangeRt=="") {
            exchangeRt = "1";
        }
        if(inputRealAmt=="") {
            inputRealAmt = "0";
        }
        if(itemType =="0009"){
                var amountVat = (parseFloat(inputRealAmt))* parseFloat(exchangeRt) * 0.05;
            }else{
                var amountVat = (parseFloat(inputRealAmt))* parseFloat(exchangeRt) * 0.1;
            }
        
        var amount = (parseFloat(inputRealAmt)) * parseFloat(exchangeRt) + amountVat;
        $('#<%=txtInputVat.ClientID %>').val(amountVat); 
        $('#<%=txtInputAmt.ClientID %>').val(amount);                  
            
        $('#<%=txtInputRealAmt.ClientID %>').formatCurrency();
        $('#<%=txtInputEx.ClientID %>').formatCurrency();
        $('#<%=txtInputVat.ClientID %>').formatCurrency();
        $('#<%=txtInputAmt.ClientID %>').formatCurrency();    
    }
   

    function datePicker() {
        $("#<%=txtSearchDt.ClientID %>").datepicker();
        $("#<%=txtESearchDt.ClientID %>").datepicker();   
        $("#<%=txtInputSearchDt.ClientID %>").datepicker();
        $("#<%=txtInputESearchDt.ClientID %>").datepicker(); 
        $("#<%=txtInputPayDt.ClientID %>").datepicker();
        $("#<%=txtInputIssDt.ClientID %>").datepicker();
        $("#<%=txtReqDt.ClientID %>").datepicker();                    
    }
    function loadCompanyName() {
        $('#<%=txtInputRoom.ClientID %>').live('change',function () {            
        });  
    }
    
    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

    function formatStringToDate(strdate) {
        var year        = strdate.substring(0,4);
        var month       = strdate.substring(4,6);
        var day         = strdate.substring(6,8);
        var newdate = year + "-" + month + "-" + day;
        return newdate;
    }



     function fnDetailView(trow,TenantNm,RoomNo,FeeTy,StartDt,EndDt,PayDt,PrintYN, RefSeq,IssuingDt,MonthViAmtNo,RealMonthViAmtNo,DongToDollar, DescVn, DescEng, VatAmt,billTy,requestDt,qty,UnitPrice)
    {
        resetTable();
        $(trow).addClass('rowSelected');   
        document.getElementById("<%=txtTitle.ClientID%>").value = TenantNm;
        document.getElementById("<%=txtInputRoom.ClientID%>").value = RoomNo;
        document.getElementById("<%=ddlInputItem.ClientID%>").value = FeeTy;
        document.getElementById("<%=txtInputSearchDt.ClientID%>").value = formatStringToDate(StartDt);
        document.getElementById("<%=txtInputESearchDt.ClientID%>").value = formatStringToDate(EndDt);
        document.getElementById("<%=txtInputPayDt.ClientID%>").value = formatStringToDate(PayDt);
        document.getElementById("<%=txtInputIssDt.ClientID%>").value = formatStringToDate(IssuingDt);
        document.getElementById("<%=txtInputAmt.ClientID%>").value = RealMonthViAmtNo;
        document.getElementById("<%=txtDescVi.ClientID%>").value = DescVn;
        document.getElementById("<%=txtDescEng.ClientID%>").value = DescEng;
        document.getElementById("<%=txtInputRealAmt.ClientID%>").value = MonthViAmtNo;
        document.getElementById("<%=txtInputEx.ClientID%>").value = DongToDollar ;
        document.getElementById("<%=txtInputVat.ClientID%>").value = VatAmt ;
         document.getElementById("<%=hfbillTy.ClientID%>").value = billTy ;
        document.getElementById("<%=hfRefSeq.ClientID%>").value = RefSeq ;
        document.getElementById("<%=txtTitle.ClientID%>").setAttribute('disabled', true);
        document.getElementById("<%=txtInputRoom.ClientID%>").setAttribute('disabled', true);
        document.getElementById("<%=txtReqDt.ClientID%>").value = formatStringToDate(requestDt);  
              
        document.getElementById("<%=txtInputQty.ClientID%>").value = qty;        
        document.getElementById("<%=txtInputUnitPrice.ClientID%>").value = UnitPrice;        
        

        $('#<%=txtInputAmt.ClientID %>').formatCurrency();
        $('#<%=txtInputRealAmt.ClientID %>').formatCurrency();
        $('#<%=txtInputEx.ClientID %>').formatCurrency();
        $('#<%=txtInputUnitPrice.ClientID %>').formatCurrency();
         

        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;

    }
     
    function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd)
    {
        strCompNmS = $('#<%=txtTitle.ClientID %>').val();
        window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
        
        return false;
    }

     function resetTable() {
        $("div#<%=upSettlement.ClientID %> tbody").find("tr").each(function() {
            //get all rows in table
            $(this).removeClass('rowSelected');
        });  
    }

      function fnOccupantList(strSeq,strFeeTy) {     
            var strReqDt = document.getElementById("<%=txtReqDt.ClientID%>").value;
            
           $.initWindowMsg();
   
            window.open("/Common/RdPopup/RDPopupSpecialDebitList.aspx?Datum0=" + strSeq + "&Datum1=" + strReqDt + "&Datum2=" + strFeeTy, "PrintSpecialDebit", "status=no, resizable=yes, width=900, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            return false;
        }

     $(document).ready(function () {
             LoadCalender();
             loadCompanyName();
             formatMoney();
             createDebit(false);
        });

         $(function () {
            $.windowMsg("childMsg1", function (message) {
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = message;                                     
                <%=Page.GetPostBackEventReference(imgbtnPrint)%>;
                return false;
            });
        });  
    function callBack(compNm,rentCd,roomNo,userSeq) {                    
    }  
    
    function createDebit(isTrue) {
        if(isTrue) {
            $("#tblRegDebit").show('slow');   
        } else {
            $("#tblRegDebit").hide();    
        }
    }
//-->
   
    </script>
    <style type="text/css">
        .rowSelected
        {
            background-color: #E4EEF5;
        }
        .rowHover
        {
            background-color: #E4EEF5;
        }
    </style>
    <fieldset class="sh-field4 MrgB10">
        <legend>검색</legend>
        <ul class="sf4-ag MrgL30">
            <li><b>
                <asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="200" Width="260px"
                    runat="server"></asp:TextBox></li>
            <li>
                <asp:DropDownList ID="ddlItems" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <b><asp:Literal ID="ltPrintedYN" runat="server" Text="Printed"></asp:Literal></b>
            </li>
            <li>
                <asp:DropDownList ID="ddlPrintYN" runat="server">
                </asp:DropDownList>
            </li>
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
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN" style="display: none">
            <li><b>
                <asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox></li>
            <li><b>
                <asp:Literal ID="Literal2" runat="server" Text="Period :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
            <li><b>
                <asp:Literal ID="Literal12" runat="server" Text="~"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtESearchDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgLoadData" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="120px" />
                    <col width="70px" />
                    <col width="210px" />
                    <col width="85px" />
                    <col width="80px" />
                    <col width="80px" />
                    <tr>
                        <th>
                            Fee Type
                        </th>
                        <th>
                            Room No
                        </th>
                        <th>
                            Tenant Nm
                        </th>
                        <th>
                            Using Period
                        </th>
                        <th>
                            Fee Amount
                        </th>
                        <th>
                            Issuing Date
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 235px; width: 840px; overflow-x: no-display">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound"
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" OnSelectedIndexChanged="lvPrintoutList_SelectedIndexChanged">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter">
                            <col width="165px" />
                            <col width="80px" />
                            <col width="260px" />
                            <col width="160px" />
                            <col width="100px" />
                            <col width="90px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="cursor: pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)"
                            onclick="javascript:return fnDetailView(this,'<%#Eval("TenantNm")%>','<%#Eval("RoomNo")%>','<%#Eval("FeeTy")%>','<%#Eval("StartDt")%>','<%#Eval("EndDt")%>','<%#Eval("PaymentDt")%>','<%#Eval("PrintedYN")%>','<%#Eval("REF_SEQ")%>','<%#Eval("IssuingDt")%>','<%#Eval("MonthViAmtNo")%>','<%#Eval("RealMonthViAmtNo")%>','<%#Eval("DongToDollar")%>','<%#Eval("Desciption")%>','<%#Eval("DesciptionEng")%>','<%#Eval("VATAmt")%>','<%#Eval("BillTy")%>','<%#Eval("RequestDt")%>','<%#Eval("Qty")%>','<%#Eval("UnitPrice")%>')">
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txthfFeeTypeCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:Literal ID="ltFeeType" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymetDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfContractNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtDebitCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfFloor" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfMonthViAmtNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRealMonthViAmtNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfUnPaidAmount" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfDongtoDollar" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfFeeCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfBundleSeqNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfStartUsingDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfEndUsingDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfIssDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfReqDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfQty" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfUnitPrice" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltUsingPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight2">
                                <asp:Literal ID="ltFee" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltIssDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820px">
                            <col width="165px" />
                            <col width="80px" />
                            <col width="260px" />
                            <col width="160px" />
                            <col width="100px" />
                            <col width="90px" />
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>                                 
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtInputPayDt" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnPrint" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCreate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnBack" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtReqDt" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="rbMoneyCd" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div id="tblRegDebit">
                <table cellspacing="0" class="TbCel-Type2" style="margin-bottom: 10px;">
                    <tr>
                        <th>
                            <asp:Literal ID="ltBankAcc" runat="server" Text="Company Name"></asp:Literal>
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="1000" Width="455"></asp:TextBox>
                            <asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif"
                                ImageAlign="AbsMiddle" Width="17px" Height="15px" />
                        </td>
                        <th align="center">
                            <asp:Literal ID="Literal6" runat="server" Text="Debit Type"></asp:Literal>
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rbIsDebit" runat="server">
                                <asp:ListItem Value="NM" Selected="True">Normal</asp:ListItem>
                                <asp:ListItem Value="AD">Adjustment</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltDesVi" runat="server" Text="Description Vn"></asp:Literal>
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescVi" CssClass="bgType2" runat="server" Width="450px"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="Literal4" runat="server" Text="Request Date"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtReqDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                                AutoPostBack="true" OnTextChanged="txtReqDt_TextChanged"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtReqDt.ClientID %>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltDesEng" runat="server" Text="Description Eng"></asp:Literal>
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescEng" CssClass="bgType2" runat="server" Width="450px"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltIssDt" runat="server" Text="Issuing Date"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputIssDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                                runat="server"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInputIssDt.ClientID %>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputRoom" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltInputPaymentKind" runat="server" Text="Fee Type"></asp:Literal>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlInputItem" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th align="center">
                            <asp:Literal ID="Literal5" runat="server" Text="Unit"></asp:Literal>
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rbMoneyCd" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbMoneyCd_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="USD" Selected="True">USD</asp:ListItem>
                                <asp:ListItem Value="VND">VND</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltPeriod" runat="server" Text="Period"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                                runat="server"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInputSearchDt.ClientID %>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                            <b>
                                <asp:Literal ID="Literal3" runat="server" Text="~"></asp:Literal>
                                <asp:TextBox ID="txtInputESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                                    runat="server"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInputESearchDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltPayDt" runat="server" Text="Pay Date"></asp:Literal>
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtInputPayDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                                runat="server" AutoPostBack="true" OnTextChanged="txtInputPayDt_TextChanged"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInputPayDt.ClientID %>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>                        
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltInputQty" runat="server" Text="Quantity"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputQty" runat="server" CssClass="bgType2" MaxLength="20"
                                Width="100"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltInputUnitPrice" runat="server" Text="Unit Price"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputUnitPrice" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                        </td>  
                        <th align="center">
                            <asp:Literal ID="ltVat" runat="server" Text="Vat"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputVat" runat="server" Width="100"></asp:TextBox>
                        </td>                      
                    </tr>
                    <tr>
                        <th align="center">
                            <asp:Literal ID="ltAmount" runat="server" Text="Net Amount"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputRealAmt" runat="server" CssClass="bgType2" MaxLength="18"
                                Width="100"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltExchangeRate" runat="server" Text="Ex.Rate"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputEx" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                        </td>
                        <th align="center">
                            <asp:Literal ID="ltRealAmt" runat="server" Text="Amount"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtInputAmt" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" Text=" Print " OnClick="lnkbtnPrint_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCreate" runat="server" Text="Create" OnClick="lnkbtnCreate_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Save"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" Text="Update" OnClick="lnkbtnUpdate_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Delete" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnBack" runat="server" Text="Back" OnClick="lnkbtnBack_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfUserSeq" runat="server" />
            <asp:HiddenField ID="hfSeq" runat="server" Value="" />
            <asp:HiddenField ID="hfFeeTy" runat="server" Value="" />
            <asp:HiddenField ID="hfRef_Seq" runat="server" Value="" />
            <asp:HiddenField ID="hfRoomNo" runat="server" Value="" />
            <asp:HiddenField ID="hfSeqDt" runat="server" Value="0" />
            <asp:HiddenField ID="hfBillCdDt" runat="server" Value="" />
            <asp:HiddenField ID="hfRentCd" runat="server" Value="" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfRoomNo" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txthfChargeSeq" runat="server" Visible="false" Text="0"></asp:TextBox>
            <asp:HiddenField ID="HfReturnUserSeqId" runat="server" />
            <asp:HiddenField ID="hfbillTy" runat="server" Value="NM" />
            </b>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfReqDt" runat="server" Value="" />
    <asp:HiddenField ID="hfRefSeq" runat="server" Value="" />
    <asp:HiddenField ID="hfPrintBundleNo" runat="server" Value="" />
    <asp:HiddenField ID="hfsendParam" runat="server" />
    <asp:ImageButton ID="imgLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgLoadData_Click" />
    <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailView_Click" />
    <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnPrint_Click" />
</asp:Content>

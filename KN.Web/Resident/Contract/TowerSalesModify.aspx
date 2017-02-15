<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="TowerSalesModify.aspx.cs" Inherits="KN.Web.Resident.Contract.TowerSalesModify"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--//
    
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            loadCalendar(); 
        }
    }
    function fnCheckDeposit(strText)
    {
        var strDepositExpDt = document.getElementById("<%=txtDepositExpDt.ClientID %>");
        var strHfDepositExpDt = document.getElementById("<%=hfDepositExpDt.ClientID %>");
        var strDepositExpAmt = document.getElementById("<%=txtDepositExpAmt.ClientID %>");
        var strDepositPayDt = document.getElementById("<%=txtDepositPayDt.ClientID%>");
        var strHfDepositPayDt = document.getElementById("<%=hfDepositPayDt.ClientID%>");
        var strDepositPayAmt = document.getElementById("<%=txtDepositPayAmt.ClientID%>");
        
        if (trim(strHfDepositExpDt.value) == "")
        {
            alert(strText);
            strDepositExpDt.focus();
            
            return false;
        }
        
        if (trim(strDepositExpAmt.value) == "")
        {
            alert(strText);
            strDepositExpAmt.focus();
            
            return false;        
        }
        else
        {
            if (trim(strDepositPayAmt.value) != "")
            {
                if (Number(trim(strDepositPayAmt.value)) > 0)
                {
                    if (trim(strDepositExpAmt.value) != trim(strDepositPayAmt.value))
                    {
                        document.getElementById("<%=txtDepositPayAmt.ClientID%>").value = "";
                        alert(strText);
                        strDepositPayAmt.focus();
                        
                        return false;
                    }
                }
            }
        }
       
        if (trim(strDepositPayAmt.value) != "")
        {
            if (strHfDepositPayDt.value == "")
            {
                alert(strText);
                strDepositPayDt.focus();
                
                return false;
            }
        }

        return true;
    }
    
    function fnCheckValidate(strAlertText, strTanVal, strLongVal, strShortVal)
    {
        // 기본정보
        var strInCharge = document.getElementById("<%=txtInchage.ClientID %>");
        var strLandloadNm = document.getElementById("<%=txtLandloadNm.ClientID %>");
        var strLandloadAddr = document.getElementById("<%=txtLandloadAddr.ClientID %>");
        var strLandloadCorpCert = document.getElementById("<%=txtLandloadCorpCert.ClientID %>");
        var strIssueDt = document.getElementById("<%=txtIssueDt.ClientID %>");
        var strLandloadTelFrontNo = document.getElementById("<%=txtLandloadTelFrontNo.ClientID %>");
        var strLandloadTelMidNo = document.getElementById("<%=txtLandloadTelMidNo.ClientID %>");
        var strLandloadTelRearNo = document.getElementById("<%=txtLandloadTelRearNo.ClientID %>");
        var strLandloadRepNm = document.getElementById("<%=txtLandloadRepNm.ClientID %>");
        var strLandloadTaxCd = document.getElementById("<%=txtLandloadTaxCd.ClientID %>");
        
        var strTerm = document.getElementById("<%=ddlTerm.ClientID %>");
        var strPersonal = document.getElementById("<%=ddlPersonal.ClientID %>");
        
        // 기간
        var strRentStartDt = document.getElementById("<%=txtRentStartDt.ClientID %>");
        var strRentEndDt = document.getElementById("<%=txtRentEndDt.ClientID %>");
        var strTermMonth = document.getElementById("<%=txtTermMonth.ClientID %>");
        
        // 방정보
        var strFloor = document.getElementById("<%=txtFloor.ClientID %>");

        var strRentLeasingArea = document.getElementById("<%=txtRentLeasingArea.ClientID %>");
        
        // 임대비용
        var strExchangeRate = document.getElementById("<%=txtExchangeRate.ClientID %>");

        var strPayTermMonth = document.getElementById("<%=txtPayTermMonth.ClientID %>");
        var strPayDay = document.getElementById("<%=txtPayDay.ClientID %>");

        var strSumDepositVNDNo = document.getElementById("<%=txtSumDepositVNDNo.ClientID %>");
        var strSumDepositUSDNo = document.getElementById("<%=txtSumDepositUSDNo.ClientID %>");
        
        // 관리비용

        if (trim(strInCharge.value) == "") 
        {
            alert(strAlertText);
            strInCharge.focus();
            return false;
        }           
             
        if (trim(strLandloadNm.value) == "") 
        {
            alert(strAlertText);
            strLandloadNm.focus();
            return false;
        }           
        
        if (trim(strLandloadAddr.value) == "") 
        {
            alert(strAlertText);
            strLandloadAddr.focus();
            return false;
        } 

        if (trim(strLandloadCorpCert.value) == "") 
        {
            alert(strAlertText);
            strLandloadCorpCert.focus();
            return false;
        } 
        
        if (trim(strIssueDt.value) == "") 
        {
            alert(strAlertText);
            strIssueDt.focus();
            return false;
        }        
        
        if (trim(strLandloadTelFrontNo.value) == "") 
        {
            alert(strAlertText);
            strLandloadTelFrontNo.focus();
            return false;
        }                         
        
        if (trim(strLandloadTelMidNo.value) == "") 
        {
            alert(strAlertText);
            strLandloadTelMidNo.focus();
            return false;
        } 
        
        if (trim(strLandloadTelRearNo.value) == "") 
        {
            alert(strAlertText);
            strLandloadTelRearNo.focus();
            return false;
        }   

        if (strPersonal.value == "0002")
        {
//            if (trim(strLandloadRepNm.value) == "") 
//            {
//                alert(strAlertText);
//                strLandloadRepNm.focus();
//                return false;
//            }
            
            if (trim(strLandloadTaxCd.value) == "") 
            {
                alert(strAlertText);
                strLandloadTaxCd.focus();
                return false;
            }
        }

        if (trim(strRentStartDt.value) == "") 
        {
            alert(strAlertText);
            strRentStartDt.focus();
            return false;
        }
        
        if (trim(strRentEndDt.value) == "") 
        {
            alert(strAlertText);
            strRentEndDt.focus();
            return false;
        }
        
        if (trim(strTermMonth.value) == "") 
        {
            alert(strAlertText);
            strTermMonth.focus();
            return false;
        }

        if (trim(strFloor.value) == "") 
        {
            alert(strAlertText);
            strFloor.focus();
            return false;
        }       
        
        if (trim(strRentLeasingArea.value) == "") 
        {
            alert(strAlertText);
            strRentLeasingArea.focus();
            return false;
        }
        
        if (trim(strExchangeRate.value) == "") 
        {
            alert(strAlertText);
            strExchangeRate.focus();
            return false;
        }
        
//        if (trim(strPayStartYYYYMM.value) == "")
//        {
//            alert(strAlertText);
//            strPayStartYYYYMM.focus();
//            return false;
//        }
        
        if (trim(strPayTermMonth.value) == "")
        {
            alert(strAlertText);
            strPayTermMonth.focus();
            return false;
        }
        
        if (trim(strPayDay.value) == "")
        {
            alert(strAlertText);
            strPayDay.focus();
            return false;
        }
        
//        if (strTerm.value == "0001")
//        {        
//            if (trim(strSumRentVNDNo.value) == "") 
//            {
//                alert(strAlertText);
//                strSumRentVNDNo.focus();
//                return false;
//            }
//            
//            if (trim(strSumRentUSDNo.value) == "") 
//            {
//                alert(strAlertText);
//                strSumRentUSDNo.focus();
//                return false;
//            }
//        }
        
        
        if (trim(strSumDepositVNDNo.value) == "") 
        {
            alert(strAlertText);
            strSumDepositVNDNo.focus();
            return false;
        }
        
        if (trim(strSumDepositUSDNo.value) == "") 
        {
            alert(strAlertText);
            strSumDepositUSDNo.focus();
            return false;
        }
        getListFeeMng();
        getListFitFeeMng();
        getListRentFeeMng();
 
        return true;
    }
    
        function getListFeeMng() {
        var strListFee = "";
        $('#tblListFee tbody tr input').each(function (){                    
            if($(this).val()!="|") {
                strListFee +=$(this).val()+',';                    
            } else {
                strListFee +=$(this).val();
            }              
        });  
        $('#<%=hfListFeeMng.ClientID %>').val("");
        $('#<%=hfListFeeMng.ClientID %>').val(strListFee);         
    }
    
    function getListRentFeeMng() {
        var strListRentFee = "";
        $('#tblListRentFee tbody tr input').each(function (){                    
            if($(this).val()!="|") {
                strListRentFee +=$(this).val()+',';                    
            } else {
                strListRentFee +=$(this).val();
            }              
        });  
        $('#<%=hfListRentFee.ClientID %>').val("");
        $('#<%=hfListRentFee.ClientID %>').val(strListRentFee);         
    }

    function getListFitFeeMng() {
        if($('#<%=hfApplyFeeMn.ClientID %>').val()=="Y") {
            var strListFitFee = "";
            $('#tblListFitFee tbody tr input').each(function (){                    
                if($(this).val()!="|") {
                    strListFitFee +=$(this).val()+',';                    
                } else {
                    strListFitFee +=$(this).val();
                }              
            });
            $('#<%=hfListFitFeeMng.ClientID %>').val("");
            $('#<%=hfListFitFeeMng.ClientID %>').val(strListFitFee);            
        }        
    }
    
    function fnChangePopup(strNowRate, strReturnBox1, strReturnBox2)
    {
        window.open("<%=Master.PAGE_POPUP2%>?NowRate=" + strNowRate + "&ReturnBox1=" + strReturnBox1 + "&ReturnBox2=" + strReturnBox2, 'TmpExchange', 'status=no, resizable=no, width=320, height=80, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');
        return false;
    }
    
    function fnCheckRentalFee(strData1, strData2, strData3, strData4, strData5, strText)
    {
        var strRentalFeeStartDt = document.getElementById(strData1);
        var strHfRentalFeeStartDt = document.getElementById(strData2);
        var strRentalFeeEndDt = document.getElementById(strData3);
        var strHfRentalFeeEndDt = document.getElementById(strData4);
        var strRentalFeeExpAmt = document.getElementById(strData5);

        if (trim(strHfRentalFeeStartDt.value) == "")
        {
            alert(strText);
            strRentalFeeStartDt.focus();
            
            return false;
        }
        
        if (trim(strHfRentalFeeEndDt.value) == "")
        {
            alert(strText);
            strRentalFeeEndDt.focus();
            
            return false;
        }
        
        if (trim(strRentalFeeExpAmt.value) == "")
        {
            alert(strText);
            strRentalFeeExpAmt.focus();
            
            return false;
        }
            
        return true;
    }
    
    function fnCheckItems(strData1, strData2, strData3, strData4, strData5, strData6, strText)
    {
        var strDepositExpDt = document.getElementById(strData1);
        var strHfDepositExpDt = document.getElementById(strData2);
        var strDepositExpAmt = document.getElementById(strData3);
        var strDepositPayDt = document.getElementById(strData4);
        var strHfDepositPayDt = document.getElementById(strData5);
        var strDepositPayAmt = document.getElementById(strData6);

        if (trim(strHfDepositExpDt.value) == "")
        {
            alert(strText);
            strDepositExpDt.focus();
            
            return false;
        }
        
        if (trim(strDepositExpAmt.value) == "")
        {
            alert(strText);
            strDepositExpAmt.focus();
            
            return false;
        }
        else
        {
            if (trim(strDepositPayAmt.value) != "")
            {
                if (Number(trim(strDepositPayAmt.value)) > 0)
                {
                    if (trim(strDepositExpAmt.value) != trim(strDepositPayAmt.value))
                    {
                        document.getElementById(strData6).value = "";
                        alert(strText);
                        strDepositPayAmt.focus();
                        
                        return false;
                    }
                }
            }
        }
        
        if (strDepositPayAmt.value != "")
        {
            if (strHfDepositPayDt.value == "")
            {
                alert(strText);
                strDepositPayDt.focus();
                
                return false;
            }
        }
            
        return true;
    }
    
    function fnResetCurrency()
    {
        <%=Page.GetPostBackEventReference(imgbtnResetCurrency)%>;
        return false;
    }
    

    function chkCCChange(a) {
       
        var chkCc = document.getElementById("<%= chkCC.ClientID %>");
        var cc = true;
        if (chkCc.checked) {
            document.getElementById("<%= hfCCtype.ClientID %>").value = 'CC';
            document.getElementById("<%= txtExchangeRate.ClientID %>").value = document.getElementById("<%= hfExchangeRate.ClientID %>").value;
            $('#<%=txtExchangeRate.ClientID %>').attr('readonly', 'readonly');
            cc = true;
        } else {
            document.getElementById("<%= hfCCtype.ClientID %>").value = 'FC';
            $('#<%=txtExchangeRate.ClientID %>').focus();
            $('#<%=txtExchangeRate.ClientID %>').removeAttr("readonly");
            cc = false;            
        }
        
//        $('#<%=txtExchangeRate.ClientID%>').attr('readonly', cc);        
//        $.ajax({
//          type: "POST",
//          url: "TowerSalesModify.aspx/GetCustomers",
//          data: {name:'baokk'},
//          contentType: "application/json; charset=utf-8",
//          dataType: "json",
//          success: function(msg) {
//              alert(msg);
//          }
//        });
    }
    
        function fnApplyFee() {
            if($("#<%=isApplyFeeMn.ClientID %>").is(':checked')) {
                $("#<%=listFitOutFee.ClientID %>").show("slow"); // checked0
                $("#<%=hfApplyFeeMn.ClientID %>").val("Y");
                $('#<%=lineRow.ClientID %>').attr("style", "display: none");           
            } else {
                $("#<%=listFitOutFee.ClientID %>").hide("slow");
                $("#<%=hfApplyFeeMn.ClientID %>").val("N");
                $('#<%=lineRow.ClientID %>').attr("style", "");            
            }
        } 
    

 function loadCalendar() {
  

    $( "#txtFeeStartDt" ).datepicker();
    $( "#txtFeeEndDt" ).datepicker();
     
    $("#txtFitFeeStartDt").datepicker();
    $("#txtFitFeeEndDt" ).datepicker();
    $("#<%=txtRSPayDate.ClientID %>").datepicker();
    $("#<%=txtMSPayDate.ClientID %>").datepicker();         
    $("#<%=txtRSUsingDt.ClientID %>").datepicker();
    $("#<%=txtMSUsingDt.ClientID %>").datepicker();
      
    $("#<%=txtIssueDt.ClientID %>").datepicker({
        altField: "#<%=hfIssueDt.ClientID %>"
        });
    $("#<%=txtOTLAgreeDt.ClientID %>").datepicker({
        altField: "#<%=hfOTLAgreeDt.ClientID %>"
        });
    $("#<%=txtRentAgreeDt.ClientID %>").datepicker({
        altField: "#<%=hfRentAgreeDt.ClientID %>"
        });
    $("#<%=txtRentStartDt.ClientID %>").datepicker({
        altField: "#<%=hfRentStartDt.ClientID %>"
        });
    $("#<%=txtRentEndDt.ClientID %>").datepicker({
        altField: "#<%=hfRentEndDt.ClientID %>"
        });
    $("#<%=txtHandOverDt.ClientID %>").datepicker({
        altField: "#<%=hfHandOverDt.ClientID %>"
        });  
    $("#txtRentalFeeStartDt").datepicker({
        altField: "#<%=hfHandOverDt.ClientID %>"
        });  
    $("#txtRentalFeeEndDt").datepicker({
        altField: "#<%=hfRentalFeeEndDt.ClientID %>"
        });  
    $("#<%=txtDepositExpDt.ClientID %>").datepicker({
        altField: "#<%=hfDepositExpDt.ClientID %>"
        });  
    $("#<%=txtDepositPayDt.ClientID %>").datepicker({
        altField: "#<%=hfDepositPayDt.ClientID %>"
        });  
    $("#<%=txtInteriorStartDt.ClientID %>").datepicker({
        altField: "#<%=hfInteriorStartDt.ClientID %>"
        }); 
    $("#<%=txtInteriorStartDt.ClientID %>").datepicker({
        altField: "#<%=hfInteriorStartDt.ClientID %>"
        }); 
    $("#<%=txtInteriorEndDt.ClientID %>").datepicker({
        altField: "#<%=hfInteriorEndDt.ClientID %>"
        }); 
    $("#<%=txtConsDepositDt.ClientID %>").datepicker({
        altField: "#<%=hfConsDepositDt.ClientID %>"
        }); 
    $("#<%=txtConsDepositDt.ClientID %>").datepicker({
        altField: "#<%=hfConsDepositDt.ClientID %>"
        }); 
    $("#<%=txtConsRefundDt.ClientID %>").datepicker({
        altField: "#<%=hfConsRefundDt.ClientID %>"
        });      
     
     $("#<%=txtSMEndDate.ClientID %>").datepicker();
     $("#<%=txtSREndDate.ClientID %>").datepicker();

     $("#<%=txtRenewDt.ClientID %>").datepicker();

     var ddlMIsueDateType = $("#<%=ddlMIsueDateType.ClientID %>").val();
     var ddlRIsueDateType = $("#<%=ddlRIsueDateType.ClientID %>").val();
         if (ddlRIsueDateType == "A") {
             $("#<%=txtRAdjustDate.ClientID %>").show();            
         } else {
            $("#<%=txtRAdjustDate.ClientID %>").hide(); 
         }  
         if (ddlMIsueDateType == "A") {
             $("#<%=txtMAdjustDate.ClientID %>").show();            
         } else {
            $("#<%=txtMAdjustDate.ClientID %>").hide(); 
         }       

     $("#<%=ddlMIsueDateType.ClientID %>").change(function() {        
         if ($(this).val() == "A") {
             $("#<%=txtMAdjustDate.ClientID %>").show();            
         } else {
            $("#<%=txtMAdjustDate.ClientID %>").hide(); 
         }
         $("#<%=txtMAdjustDate.ClientID %>").val("0");
     });
     $("#<%=ddlRIsueDateType.ClientID %>").change(function() {        
         if ($(this).val() == "A") {
             $("#<%=txtRAdjustDate.ClientID %>").show();            
         } else {
            $("#<%=txtRAdjustDate.ClientID %>").hide(); 
         }
         $("#<%=txtRAdjustDate.ClientID %>").val("0");
     });

 }
 
    var feeNum = 0;
    function AddMngFee(){
        
        if(!isValidateFee('#txtFeeStartDt','#txtFeeEndDt','#txtFeeExcRate','#txtFeeExpAmt')) return; 
        var stSDate = $('#txtFeeStartDt').val();
        var stEDate = $('#txtFeeEndDt').val();
        var vndFee = $('#txtFeeExcRate').val();
        var usdFee = $('#txtFeeExpAmt').val();
        var feeNo = $('#feeID').val();
        if (feeNo!="") {
            $("#feeNum" + feeNo + " :text").eq(0).val(stSDate);
            $("#feeNum" + feeNo + " :text").eq(1).val(stEDate);
            $("#feeNum" + feeNo + " :text").eq(2).val(vndFee);
            $("#feeNum" + feeNo + " :text").eq(3).val(usdFee);
            $("#feeNum" + feeNo + " :hidden").eq(0).val("Y");
            $('#feeID').val("");
            
        } else {

            $('#tblListFee tbody').append(''+
                '<tr id="feeNum'+feeNum+'" >' +
                '<input type="hidden" id="isNew" value="Y"/>'+
                '<input type="hidden" id="feeSeq" value=""/>'+
                '<td align="left" class="P0"><input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stSDate+'">' +
                '</td>'+
                '<td align="left" class="P0">'+
                ' <input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stEDate+'">'+               
                '</td>'+
                '<td align="center" class="P0">'+
                '<input name="" type="text"  maxlength="18" id="" style="width:70px;" value="'+vndFee+'" readonly="readonly">&nbsp;VND</td>'+
                '<td align="center" class="P0"><input name="" type="text" maxlength="18" id="" style="width:70px;" value="'+usdFee+'" readonly="readonly">&nbsp;$</td>'+
                '<td align="center" class="P0">'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/edit.gif"  style="border-width:0px;" onclick="editFee('+feeNum+')"></span>'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/Trash.gif" style="border-width:0px;" onclick="deleteFee('+feeNum+')"></span>'+
                '</td><input type="hidden" id="" value="|"/></tr>'        
            );
            feeNum++;              
        }
       
        $('#txtFeeStartDt').val("");
        $('#txtFeeEndDt').val("");
        $('#txtFeeExcRate').val("");
        $('#txtFeeExpAmt').val("");
    }
    
    var fitFeeNum = 0;
    function AddFitOutFee(){
        if(!isValidateFee('#txtFitFeeStartDt','#txtFitFeeEndDt','#txtFitFeeExcRate','#txtFitFeeExpAmt')) return; 
        var stSDate = $('#txtFitFeeStartDt').val();
        var stEDate = $('#txtFitFeeEndDt').val();
        var vndFee = $('#txtFitFeeExcRate').val();
        var usdFee = $('#txtFitFeeExpAmt').val();
        var feeNo = $('#fitfeeID').val();
        if (feeNo!="") {
            $("#fitfeeNum" + feeNo + " :text").eq(0).val(stSDate);
            $("#fitfeeNum" + feeNo + " :text").eq(1).val(stEDate);
            $("#fitfeeNum" + feeNo + " :text").eq(2).val(vndFee);
            $("#fitfeeNum" + feeNo + " :text").eq(3).val(usdFee);
            $("#fitfeeNum" + feeNo + " :hidden").eq(0).val("Y");
            $('#fitfeeID').val("");
            
        } else {

            $('#tblListFitFee tbody').append(''+
                '<tr id="fitfeeNum'+fitFeeNum+'" >' +
                '<input type="hidden" id="isNew" value="Y"/>'+
                '<input type="hidden" id="feeSeq" value=""/>'+
                '<td align="left" class="P0"><input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stSDate+'">' +                
                '</td>'+
                '<td align="left" class="P0">'+
                ' <input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stEDate+'">'+                
                '</td>'+
                '<td align="center" class="P0">'+
                '<input name="" type="text"  maxlength="18" id="" style="width:70px;" value="'+vndFee+'" readonly="readonly">&nbsp;VND</td>'+
                '<td align="center" class="P0"><input name="" type="text" maxlength="18" id="" style="width:70px;" value="'+usdFee+'" readonly="readonly">&nbsp;$</td>'+
                '<td align="center" class="P0">'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/edit.gif"  style="border-width:0px;" onclick="editFitFee('+fitFeeNum+')"></span>'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/Trash.gif" style="border-width:0px;" onclick="deleteFitFee('+fitFeeNum+')"></span>'+
                '</td><input type="hidden" id="" value="|"/></tr>'        
            );
            fitFeeNum++;              
        }
       
        $('#txtFitFeeStartDt').val("");
        $('#txtFitFeeEndDt').val("");
        $('#txtFitFeeExcRate').val("");
        $('#txtFitFeeExpAmt').val("");
    }
    
    var rentFeeNum = 0;
    function AddRentFee(){
        if(!isValidateFee('#txtRentalFeeStartDt','#txtRentalFeeEndDt','#txtRentalFeeExcRate','#txtRentalFeeExpAmt')) return; 
        var stSDate = $('#txtRentalFeeStartDt').val();
        var stEDate = $('#txtRentalFeeEndDt').val();
        var vndFee = $('#txtRentalFeeExcRate').val();
        var usdFee = $('#txtRentalFeeExpAmt').val();
        var feeNo = $('#rentfeeID').val();
        if (feeNo!="") {
            $("#rentfeeNum" + feeNo + " :text").eq(0).val(stSDate);
            $("#rentfeeNum" + feeNo + " :text").eq(1).val(stEDate);
            $("#rentfeeNum" + feeNo + " :text").eq(2).val(vndFee);
            $("#rentfeeNum" + feeNo + " :text").eq(3).val(usdFee);
            $("#rentfeeNum" + feeNo + " :hidden").eq(0).val("Y");
            $('#rentfeeID').val("");
            
        } else {

            $('#tblListRentFee tbody').append(''+
                '<tr id="rentfeeNum'+rentFeeNum+'" >' +
                '<input type="hidden" id="isNew" value="Y"/>'+
                '<input type="hidden" id="feeSeq" value=""/>'+
                '<td align="left" class="P0"><input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stSDate+'">' +
                '</td>'+
                '<td align="left" class="P0">'+
                ' <input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stEDate+'">'+
                '</td>'+
                '<td align="center" class="P0">'+
                '<input name="" type="text"  maxlength="18" id="" style="width:70px;" value="'+vndFee+'" readonly="readonly">&nbsp;VND</td>'+
                '<td align="center" class="P0"><input name="" type="text" maxlength="18" id="" style="width:70px;" value="'+usdFee+'" readonly="readonly">&nbsp;$</td>'+
                '<td align="center" class="P0">'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/edit.gif"  style="border-width:0px;" onclick="editRentFee('+rentFeeNum+')"></span>'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/Trash.gif" style="border-width:0px;" onclick="deleteRentFee('+rentFeeNum+')"></span>'+
                '</td><input type="hidden" id="" value="|"/></tr>'        
            );
            rentFeeNum++;              
        }
       
        $('#txtRentalFeeStartDt').val("");
        $('#txtRentalFeeEndDt').val("");
        $('#txtRentalFeeExcRate').val("");
        $('#txtRentalFeeExpAmt').val("");
    }
    
    

    function editMngFee(feeNum) {    
        $('#txtFeeStartDt').val($("#feeNum" + feeNum + " :text").eq(0).val());
        $('#txtFeeEndDt').val($("#feeNum" + feeNum + " :text").eq(1).val());
        $('#txtFeeExcRate').val($("#feeNum" + feeNum + " :text").eq(2).val());
        $('#txtFeeExpAmt').val($("#feeNum" + feeNum + " :text").eq(3).val());
        $('#feeID').val(feeNum);
        
    }
    function editFitFee(feeNum) {    
        $('#txtFitFeeStartDt').val($("#fitfeeNum" + feeNum + " :text").eq(0).val());
        $('#txtFitFeeEndDt').val($("#fitfeeNum" + feeNum + " :text").eq(1).val());
        $('#txtFitFeeExcRate').val($("#fitfeeNum" + feeNum + " :text").eq(2).val());
        $('#txtFitFeeExpAmt').val($("#fitfeeNum" + feeNum + " :text").eq(3).val());
        $('#fitfeeID').val(feeNum);
        
    }
    function editRentFee(feeNum) {    
        $('#txtRentalFeeStartDt').val($("#rentfeeNum" + feeNum + " :text").eq(0).val());
        $('#txtRentalFeeEndDt').val($("#rentfeeNum" + feeNum + " :text").eq(1).val());
        $('#txtRentalFeeExcRate').val($("#rentfeeNum" + feeNum + " :text").eq(2).val());
        $('#txtRentalFeeExpAmt').val($("#rentfeeNum" + feeNum + " :text").eq(3).val());
        $('#rentfeeID').val(feeNum);
        
    }    
    
    function deleteFitFee(thisFee) {
           if ($("#fitfeeNum" + thisFee + " :hidden").eq(1).val()!='') {
                $('#<%=hfFeeSeqDel.ClientID %>').val($("#fitfeeNum" + thisFee + " :hidden").eq(1).val());
                <%=Page.GetPostBackEventReference(imgDeleteFee)%>;
                $('#<%=hfFeeSeqDel.ClientID %>').val("");
            } 
         $('tr#fitfeeNum' + thisFee).remove();
        $('#fitfeeID').val("");
        $('#txtFitFeeStartDt').val("");
        $('#txtFitFeeEndDt').val("");
        $('#txtFitFeeExcRate').val("");
        $('#txtFitFeeExpAmt').val("");        
    }

    function deleteRentFee(thisFee) {
           if ($("#rentfeeNum" + thisFee + " :hidden").eq(1).val()!='') {
                $('#<%=hfRentFeeSeq.ClientID %>').val($("#rentfeeNum" + thisFee + " :hidden").eq(1).val());
                <%=Page.GetPostBackEventReference(imgDeleteRentFee)%>;
                $('#<%=hfRentFeeSeq.ClientID %>').val("");
            } 
         $('tr#rentfeeNum' + thisFee).remove();
        $('#rentfeeID').val("");
        $('#txtRentalFeeStartDt').val("");
        $('#txtRentalFeeEndDt').val("");
        $('#txtRentalFeeExcRate').val("");
        $('#txtRentalFeeExpAmt').val("");  
        return false;
    }    
    function deleteMngFee(thisFee) {
           if ($("#feeNum" + thisFee + " :hidden").eq(1).val()!='') {
                $('#<%=hfFeeSeqDel.ClientID %>').val($("#feeNum" + thisFee + " :hidden").eq(1).val());
                <%=Page.GetPostBackEventReference(imgDeleteFee)%>;
                $('#<%=hfFeeSeqDel.ClientID %>').val("");
            } 
         $('tr#feeNum' + thisFee).remove();
        $('#feeID').val("");
        $('#txtFeeStartDt').val("");
        $('#txtFeeEndDt').val("");
        $('#txtFeeExcRate').val("");
        $('#txtFeeExpAmt').val("");        
    }    

    
 function isValidateFee(sdate,edate,vnd,usd) {


            if ($(sdate).val()=="") {
                $(sdate).focus();
                return false;
            }
            if ($(edate).val()=="") {
                $(edate).focus();
                return false;
            }
            var stSDate = $(sdate).val().replace("-","");
            var stEDate = $(edate).val().replace("-","");
            if(parseInt(stSDate.replace("-",""))>parseInt(stEDate.replace("-",""))) {
                $(edate).focus();
                return false;
            }     
            if ($(vnd).val()=="") {
                $(+vnd).focus();
                return false;
            }   
            if ($(usd).val()=="") {
                $(usd).focus();
                return false;
            }
            return true;
 } 
 
 function isValidateFitFee() {


     if ($('#txtFitFeeStartDt').val()=="") {
         $('#txtFitFeeStartDt').focus();
         return false;
     }
     if ($('#txtFitFeeEndDt').val()=="") {
         $('#txtFitFeeEndDt').focus();
          return false;
     }
     var stSDate = $('#txtFitFeeStartDt').val().replace("-","");
     var stEDate = $('#txtFitFeeEndDt').val().replace("-","");
     if(parseInt(stSDate.replace("-",""))>parseInt(stEDate.replace("-",""))) {
         $('#txtFitFeeEndDt').focus();
         return false;
     }     
     if ($('#txtFitFeeExcRate').val()=="") {
         $('#txtFitFeeExcRate').focus();
          return false;
     }   
     if ($('#txtFitFeeExpAmt').val()=="") {
         $('#txtFitFeeExpAmt').focus();
          return false;
     }
     return true;
 }
 

 $(document).ready(function() {
     loadCalendar();
     fnApplyFee();
 });
//-->  

 function VNDtoUSD() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtFeeExcRate").val();
           if (num2=="") {
               return; 
           }   
       var result = parseFloat(num2, 10) / parseFloat(currentFc, 10);
        
        $('#txtFeeExpAmt').val(result.toFixed(2)); 
        $("#txtFeeExcRate").val(parseFloat(num2).toFixed(2));
 }

 function USDtoVND() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtFeeExpAmt").val();
           if (num2=="") {
               return; 
           }
       var result = parseFloat(num2, 10) * parseFloat(currentFc, 10);
        
        $('#txtFeeExcRate').val(result.toFixed(2)); 
        $("#txtFeeExpAmt").val(parseFloat(num2).toFixed(2));
 }


 function fitVNDtoUSD() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtFitFeeExcRate").val();
           if (num2=="") {
               return; 
           }   
       var result = parseFloat(num2, 10) / parseFloat(currentFc, 10);
        
        $('#txtFitFeeExpAmt').val(result.toFixed(2)); 
     $("#txtFitFeeExcRate").val(parseFloat(num2).toFixed(2));
 }

 function fitUSDtoVND() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtFitFeeExpAmt").val();
           if (num2=="") {
               return; 
           }
       var result = parseFloat(num2, 10) * parseFloat(currentFc, 10);
        
        $('#txtFitFeeExcRate').val(result.toFixed(2));  
     $("#txtFitFeeExpAmt").val(parseFloat(num2).toFixed(2));
 }
 
 function rentVNDtoUSD() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtRentalFeeExcRate").val();
           if (num2=="") {
               return; 
           }   
       var result = parseFloat(num2, 10) / parseFloat(currentFc, 10);
        
        $('#txtRentalFeeExpAmt').val(result.toFixed(2)); 
     $("#txtRentalFeeExcRate").val(parseFloat(num2).toFixed(2));
 }

 function rentUSDtoVND() {
           var currentFc = $('#<%= txtExchangeRate.ClientID %>').val();
           
            var num2 = $("#txtRentalFeeExpAmt").val();
           if (num2=="") {
               return; 
           }
       var result = parseFloat(num2, 10) * parseFloat(currentFc, 10);
        
        $('#txtRentalFeeExcRate').val(result.toFixed(2));  
     $("#txtRentalFeeExpAmt").val(parseFloat(num2).toFixed(2));
 }
//-->        
    </script>
    <style>
        .cont-Mid .cont-wp
        {
            width: 855px;
        }
    </style>
    <asp:UpdatePanel ID="upBasicInfo" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltBasicInfo" runat="server"></asp:Literal>
                (<asp:Literal ID="ltIncharge" runat="server"></asp:Literal>
                :
                <asp:TextBox ID="txtInchage" runat="server" MaxLength="20" Width="100px" CssClass="bgType3"></asp:TextBox>)
            </div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                Contract Type
                            </th>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbContractType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD" Selected="True">USD</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSpecialContract" runat="server" Text="Is Special Contract" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPodium" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlPodium" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPodium_SelectedIndexChanged">
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtHfPodium" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadNm" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlPersonal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonal_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtLandloadNm" runat="server" MaxLength="245" Width="480" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltContNo" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlContStep" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtContNo" runat="server" CssClass="grBg bgType1" MaxLength="20"
                                    ReadOnly="true" Width="150"></asp:TextBox>
                                <asp:HiddenField ID="hfContNo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal11" runat="server" Text="Industry"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlIndustry" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Literal ID="Literal13" runat="server" Text="Nationality"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlNat" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltLandloadAddr" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtLandloadAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:TextBox ID="txtLandloadDetAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadCorpCert" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadCorpCert" runat="server" CssClass="grBg bgType2" MaxLength="15"
                                    Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltIssueDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtIssueDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtIssueDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfIssueDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadTelNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadTelFrontNo" runat="server" CssClass="grBg bgType2" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadTelMidNo" runat="server" CssClass="grBg bgType2"
                                    MaxLength="4" Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadTelRearNo" runat="server" CssClass="grBg bgType2"
                                    MaxLength="4" Width="35"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadMobileNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadMobileFrontNo" runat="server" CssClass="grBg bgType3"
                                    MaxLength="4" Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadMobileMidNo" runat="server" CssClass="grBg bgType3"
                                    MaxLength="4" Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadMobileRearNo" runat="server" CssClass="grBg bgType3"
                                    MaxLength="4" Width="35"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadFAX" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadFAXFrontNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadFAXMidNo" runat="server" CssClass="grBg bgType3"
                                    MaxLength="4" Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtLandloadFAXRearNo" runat="server" CssClass="grBg bgType3"
                                    MaxLength="4" Width="35"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadEmail" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadEmailID" runat="server" CssClass="grBg bgType3" MaxLength="50"
                                    Width="80"></asp:TextBox>
                                @&nbsp;<asp:TextBox ID="txtLandloadEmailServer" runat="server" CssClass="grBg bgType3"
                                    MaxLength="50" Width="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadRepNm" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadRepNm" runat="server" CssClass="grBg bgType3" MaxLength="255"
                                    Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadTaxCd" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtLandloadTaxCd" runat="server" CssClass="grBg bgType3" MaxLength="20"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltRentAddr" runat="server"></asp:Literal>&nbsp;(For Tax)
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtRentAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:TextBox ID="txtRentDetAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRentTerm" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltOTLAgreeDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtOTLAgreeDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtOTLAgreeDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfOTLAgreeDt" runat="server" />
                            </td>
                            <th>
                                <asp:Literal ID="ltRentAgreeDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRentAgreeDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRentAgreeDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfRentAgreeDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRentStartDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRentStartDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRentStartDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfRentStartDt" runat="server" />
                                <asp:ImageButton ID="imgbtnStartCheck" runat="server" ImageUrl="~/Common/Images/Icon/check.gif"
                                    OnClick="imgbtnStartCheck_Click" />
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltRentEndDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRentEndDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRentEndDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfRentEndDt" runat="server" />
                                <asp:ImageButton ID="imgbtnEndCheck" runat="server" ImageUrl="~/Common/Images/Icon/check.gif"
                                    OnClick="imgbtnEndCheck_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltFreeRentMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFreeRentMonth" runat="server" CssClass="grBg bgType2" MaxLength="2"
                                    Width="70"></asp:TextBox>
                                <asp:Literal ID="ltMonthUnit" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltTermMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtTermMonth" runat="server" CssClass="grBg bgType2" MaxLength="3"
                                    Width="70"></asp:TextBox>
                                <asp:Literal ID="ltMonth" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal14" runat="server" Text="Renewal Time"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRenewDt" runat="server" MaxLength="2" Width="100" CssClass="bgType2"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRenewDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th class="lebd">
                                <asp:Literal ID="ltHandOverDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtHandOverDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtHandOverDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfHandOverDt" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRoomInfo" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="80px" />
                    <col width="435px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltFloor" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFloor" runat="server" MaxLength="3" AutoPostBack="true" CssClass="bgType2"
                                    OnTextChanged="txtFloor_TextChanged"></asp:TextBox>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="10" AutoPostBack="true" CssClass="bgType2"
                                    OnTextChanged="txtRoomNo_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtExtRoomNo" runat="server" MaxLength="255" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRentLeasingArea" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtRentLeasingArea" runat="server" MaxLength="10" CssClass="bgType2"></asp:TextBox>&nbsp;㎡
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                Exchange Rate</asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltExchangeRate" runat="server"></asp:Literal>
                            </th>
                            <td colspan="2" style="width: 157px">
                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="bgType2" MaxLength="10"></asp:TextBox>
                                <asp:CheckBox ID="chkCC" runat="server" Text="Current Currency" Checked="True" AutoPostBack="False" />
                                <asp:HiddenField ID="hfCCtype" runat="server" Value="CC" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                            <th>
                                <asp:Label ID="lblFloat" runat="server" Text="Inflation(%)"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFloation" runat="server" AutoPostBack="true" MaxLength="3" Width="67px">0.1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Payment Type
                            </th>
                            <td colspan="2" style="width: 157px">
                                <asp:DropDownList ID="ddlPaymentType" runat="server">
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                CPI(%)
                            </th>
                            <td>
                                <asp:TextBox ID="txtCPI" runat="server" AutoPostBack="true" MaxLength="3" Width="67px">0.1</asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRetalFee" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr style="display: none">
                            <th>
                            </th>
                            <td colspan="2">
                                1 Dollar :
                                <asp:Literal ID="ltExchangeUnit" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfExchangeRate" runat="server" />
                            </td>
                            <td>
                                <div class="Btn-Type1-wp ">
                                    <div class="Btn-Tp-L">
                                        <div class="Btn-Tp-R">
                                            <div class="Btn-Tp-M">
                                                <span>
                                                    <asp:LinkButton ID="lnkbtnChange" runat="server"></asp:LinkButton></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPayStartYYYYMM" runat="server" Text="Current Using Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRSUsingDt" runat="server" MaxLength="6" Width="100" CssClass="bgType2"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRSUsingDt.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:TextBox ID="txtPayStartYYYYMM" runat="server" MaxLength="6" Width="100" CssClass="bgType2"
                                    Visible="False"></asp:TextBox>
                                <asp:Literal ID="ltPayStartYYYYMMUnit" runat="server" Visible="False"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltPayTermMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPayTermMonth" runat="server" MaxLength="3" Width="100" CssClass="bgType2"></asp:TextBox>
                                <asp:Literal ID="ltPayTermMonthUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Current Pay Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRSPayDate" runat="server" MaxLength="2" Width="100" CssClass="bgType2"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRSPayDate.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                            </td>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Pay Cycle Type"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlRPaymentCycle" runat="server">
                                    <asp:ListItem Text="B" Value="M">By Monthly</asp:ListItem>
                                    <asp:ListItem Text="O" Value="Q">Make Round Monthy</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal4" runat="server" Text="Isue Date Type"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlRIsueDateType" runat="server">
                                    <asp:ListItem Value="E">End Of month</asp:ListItem>
                                    <asp:ListItem Value="A">By Perior Date</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Literal ID="Literal5" runat="server" Text="Isue Date Adjust"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRAdjustDate" runat="server" MaxLength="3" Width="100" CssClass="bgType2"></asp:TextBox>
                                &nbsp;Day(s)
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>
                                <asp:Literal ID="ltPayDay" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtPayDay" runat="server" MaxLength="2" Width="100" CssClass="bgType2"
                                    AutoPostBack="true" OnTextChanged="txtSumRentVNDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltPayDayUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltSREndDate" runat="server" Text="Special End Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSREndDate" runat="server" MaxLength="2" Width="100" CssClass="bgType2"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSREndDate.ClientID %>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                            </td>
                            <td colspan="2">
                                <asp:Literal ID="ltExplain" runat="server" Text="If you apply this date period using will be Current Using Date ~ Special End Date. After make debit this date will auto reset"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--// 임대료 (월간 임대료) //-->
    <asp:UpdatePanel ID="upRentalFee" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="mvRentFee" runat="server" ActiveViewIndex="0">
                <asp:View runat="server" ID="vGeneal">
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
                                        <asp:Literal ID="ltTopRentalFeeStartDt" runat="server" Text="Apply Start Date"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeEndDt" runat="server"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeRate" runat="server" Visible="False"></asp:Literal>
                                        <asp:Literal ID="Literal6" runat="server" Text="VND"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeAmt" runat="server"></asp:Literal>
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
                                <div id="listRentFee" runat="server">
                                </div>
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
                                    <input type="hidden" id="rentfeeID" value="" />
                                    <td align="center" class="P0">
                                        <input id="txtRentalFeeStartDt" class="grBg bgType2" maxlength="10" style="width: 70px;"></input>
                                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtRentalFeeStartDt')"
                                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                        <asp:HiddenField ID="hfRentalFeeStartDt" runat="server" />
                                    </td>
                                    <td align="center" class="P0">
                                        <input id="txtRentalFeeEndDt" class="grBg bgType2" maxlength="10" style="width: 70px;"></input>
                                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtRentalFeeEndDt')"
                                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                                        <asp:HiddenField ID="hfRentalFeeEndDt" runat="server" />
                                    </td>
                                    <td align="center" class="P0">
                                        <input id="txtRentalFeeExcRate" maxlength="18" style="width: 70px;" onblur="rentVNDtoUSD()"
                                            onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');"></input>
                                        <asp:Literal ID="ltRentalFeeExcRateUnit" runat="server"></asp:Literal>
                                    </td>
                                    <td align="center" class="P0">
                                        <input id="txtRentalFeeExpAmt" maxlength="18" style="width: 70px;" onblur="rentUSDtoVND()"
                                            onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');"></input>
                                        <asp:Literal ID="ltRentalFeeAmtUnit" runat="server"></asp:Literal>
                                    </td>
                                    <td align="center" class="P0">
                                        <span>
                                            <%--<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"/></span>--%>
                                            <img style="border-width: 0px;" id="btnAddRentFee" src="../../Common/Images/Icon/plus.gif"
                                                type="image" onclick="AddRentFee()" alt="Add Fit Out Fee"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </colgroup>
                    </table>
                </asp:View>
                <asp:View runat="server" ID="vPodium">
                    <table cellspacing="0" class="TbCel-Type2-A">
                        <colgroup>
                            <col width="147px" />
                            <col width="178px" />
                            <col width="147px" />
                            <col width="178px" />
                            <tbody>
                                <tr>
                                    <th>
                                        <asp:Literal ID="ltMinimumIncome" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtMinimumIncome" runat="server"></asp:TextBox>
                                    </td>
                                    <th>
                                        <asp:Literal ID="ltApplyRate" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtApplyRate" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </colgroup>
                    </table>
                </asp:View>
            </asp:MultiView>
            <asp:TextBox ID="txtHfRentFeeTmpSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfRentFeeSeq" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Tb-Tp-tit">
        Fit Out Fee Management
        <div style="float: right" id="chkUsingMnFee">
            <asp:CheckBox ID="isApplyFeeMn" runat="server" Checked="True" />Apply Fit Out Management
        </div>
        <input type="hidden" id="hfIsApplyFeeMn" value="N" />
    </div>
    <div id="listFitOutFee" runat="server">
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
                            Apply Start Date
                        </th>
                        <th align="center" class="P0">
                            Appl End Date
                        </th>
                        <th align="center" class="P0">
                            VND
                        </th>
                        <th align="center" class="P0">
                            USD
                        </th>
                        <th align="center" class="P0">
                        </th>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <table class="TbCel-Type4-A" id="tblListFitFee">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
                <tbody>
                    <div id="displayFitOutFee" runat="server">
                    </div>
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
                        <td align="center" class="P0">
                            <input type="hidden" id="fitfeeID" value="" />
                            <input type="text" id="txtFitFeeStartDt" name="txtFitFeeStartDt" class="grBg bgType2"
                                maxlength="10" style="width: 70px" />
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtFitFeeStartDt')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFitFeeEndDt" class="grBg bgType2" maxlength="10" style="width: 70px"
                                readonly="true" />
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtFitFeeEndDt')" src="/Common/Images/Common/calendar.gif"
                                style="cursor: pointer; align: absmiddle;" />
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFitFeeExcRate" maxlength="18" style="width: 70px" onblur="fitVNDtoUSD()"
                                onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" />
                            VND
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFitFeeExpAmt" maxlength="18" style="width: 70px" onblur="fitUSDtoVND();"
                                onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" />
                            $
                        </td>
                        <td align="center" class="P0">
                            <span>
                                <img style="border-width: 0px;" id="btnAddFitFee" src="../../Common/Images/Icon/plus.gif"
                                    type="image" onclick="AddFitOutFee()" alt="Add Fit Out Fee"></span>
                        </td>
                    </tr>
                </tbody>
            </colgroup>
        </table>
    </div>
    <div id="lineRow" runat="server" class="lineRow">
    </div>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltMngFee" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="178px" />
            <col width="147px" />
            <col width="178px" />
            <tbody>
                <tr>
                    <th>
                        Current Using Date
                    </th>
                    <td>
                        <asp:TextBox ID="txtMSUsingDt" runat="server" MaxLength="18" Width="100" CssClass="bgType3"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtMSUsingDt.ClientID %>')"
                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                    </td>
                    <th>
                        Payment Cycle
                    </th>
                    <td>
                        <asp:TextBox ID="txtMPayCycle" runat="server" MaxLength="18" Width="100" CssClass="bgType3"></asp:TextBox>
                        <asp:Literal ID="Literal8" runat="server" Text="Month(s)"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        Payment Cycle Type
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMPaymentCycle" runat="server">
                            <asp:ListItem Text="B" Value="M">By Monthly</asp:ListItem>
                            <asp:ListItem Text="O" Value="Q">Make Round Monthy</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="Literal2" runat="server" Text="Current Pay Date"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtMSPayDate" runat="server" MaxLength="2" Width="100" CssClass="bgType2"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtMSPayDate.ClientID %>')"
                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                    </td>
                </tr>
                <tr>
                    <th>
                        Isue Date Type
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMIsueDateType" runat="server" Width="110px">
                            <asp:ListItem Value="E">End Of month</asp:ListItem>
                            <asp:ListItem Value="A">By Perior Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        Isue Date Adjust
                    </th>
                    <td>
                        <asp:TextBox ID="txtMAdjustDate" runat="server" MaxLength="18" Width="100" CssClass="bgType3"></asp:TextBox>&nbsp;Day(s)
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal9" runat="server" Text="Special End Date"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtSMEndDate" runat="server" MaxLength="2" Width="100" CssClass="bgType2"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSMEndDate.ClientID %>')"
                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                    </td>
                    <td colspan="2">
                        <asp:Literal ID="Literal10" runat="server" Text="If you apply this date period using will be Current Using Date ~ Special End Date. After make debit this date will auto reset"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <div id="ListMngFee" runat="server">
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
                            Apply Start Date
                        </th>
                        <th align="center" class="P0">
                            Appl End Date
                        </th>
                        <th align="center" class="P0">
                            VND
                        </th>
                        <th align="center" class="P0">
                            USD
                        </th>
                        <th align="center" class="P0">
                        </th>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <table class="TbCel-Type4-A" id="tblListFee">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
            </colgroup>
            <tbody>
                <div id="diplayMngFee" runat="server">
                </div>
            </tbody>
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
                        <td align="center" class="P0">
                            <input type="hidden" id="Hidden1" value="" />
                            <input type="text" id="txtFeeStartDt" name="txtFeeStartDt" class="grBg bgType2" maxlength="10"
                                style="width: 70px" />
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtFeeStartDt')" src="/Common/Images/Common/calendar.gif"
                                style="cursor: pointer; align: absmiddle;" />
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFeeEndDt" class="grBg bgType2" maxlength="10" style="width: 70px"
                                readonly="true" />
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtFeeEndDt')" src="/Common/Images/Common/calendar.gif"
                                style="cursor: pointer; align: absmiddle;" />
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFeeExcRate" maxlength="18" style="width: 70px" onblur="VNDtoUSD()"
                                onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" />
                            VND
                        </td>
                        <td align="center" class="P0">
                            <input type="text" id="txtFeeExpAmt" maxlength="18" style="width: 70px" onblur="USDtoVND();"
                                onkeypress="javascript:IsNumericOrDot(this, 'Please enter only numbers.');" />
                            $
                        </td>
                        <td align="center" class="P0">
                            <span>
                                <%--<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"/></span>--%>
                                <img style="border-width: 0px;" id="btnAddFee" src="../../Common/Images/Icon/plus.gif"
                                    type="image" onclick="AddMngFee()" alt="Add Fee"></span>
                            <input type="hidden" id="feeID" value="" />
                        </td>
                    </tr>
                </tbody>
            </colgroup>
        </table>
    </div>
    <asp:UpdatePanel ID="upDeposit" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltDeposit" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltSumDepositVNDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSumDepositVNDNo" runat="server" MaxLength="18" Width="100" CssClass="bgType3"
                                    AutoPostBack="true" OnTextChanged="txtSumDepositVNDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltDepositSumVNDNoUnit" runat="server"></asp:Literal>
                                <asp:Literal ID="ltSumDepositVNDEn" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfSumDepositVNDEn" runat="server" />
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltSumDepositUSDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSumDepositUSDNo" runat="server" MaxLength="18" Width="100" CssClass="bgType3"
                                    AutoPostBack="true" OnTextChanged="txtSumDepositUSDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltDepositSumUSDNoUnit" runat="server"></asp:Literal>
                                <asp:Literal ID="ltSumDepositUSDEn" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfSumDepositUSDEn" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table class="TbCel-Type4-A">
                <colgroup>
                    <col width="164px" />
                    <col width="164px" />
                    <col width="104px" />
                    <col width="164px" />
                    <col width="164px" />
                    <col width="60px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltDepositExpDt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositExpAmt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositExcRate" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositPayDt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositPayAmt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                &nbsp;
                            </th>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvDepositList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemDataBound="lvDepositList_ItemDataBound" OnItemUpdating="lvRentalFeeList_ItemUpdating"
                OnItemDeleting="lvDepositList_ItemDeleting">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="160px" />
                        <col width="160px" />
                        <col width="100px" />
                        <col width="160px" />
                        <col width="160px" />
                        <col width="80px" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExpDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                Width="70" ReadOnly="true"></asp:TextBox>
                            <asp:Literal ID="ltExpCalendar" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfDepositExpDt" runat="server" />
                            <asp:TextBox ID="txtDepositTmpSeq" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtDepositSeq" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExpAmt" runat="server" MaxLength="18" Width="90"></asp:TextBox>
                            <asp:Literal ID="ltDepositExpAmtUnit" runat="server"></asp:Literal>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExcRate" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                            <asp:Literal ID="ltDepositExcRateUnit" runat="server"></asp:Literal>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositPayDt" runat="server" CssClass="grBg bgType1" MaxLength="18"
                                Width="70" ReadOnly="true"></asp:TextBox>
                            <asp:Literal ID="ltPayCalendar" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfDepositPayDt" runat="server" />
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositPayAmt" runat="server" MaxLength="18" Width="90"></asp:TextBox>
                            <asp:Literal ID="ltDepositPayAmtUnit" runat="server"></asp:Literal>
                        </td>
                        <td align="center" class="P0">
                            <span>
                                <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                            <span>
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <table>
                <colgroup>
                    <col width="164px" />
                    <col width="164px" />
                    <col width="104px" />
                    <col width="164px" />
                    <col width="164px" />
                    <col width="60px" />
                    <tbody>
                        <tr>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtDepositExpDt" runat="server" CssClass="grBg bgType2" MaxLength="8"
                                    ReadOnly="true" Width="70"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtDepositExpDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfDepositExpDt" runat="server" />
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtDepositExpAmt" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                                <asp:Literal ID="ltDepositExpAmtUnit" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtDepositExcRate" runat="server" MaxLength="8" Width="70"></asp:TextBox>
                                <asp:Literal ID="ltDepositExcRateUnit" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtDepositPayDt" runat="server" CssClass="grBg bgType2" MaxLength="8"
                                    ReadOnly="true" Width="70"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtDepositPayDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfDepositPayDt" runat="server" />
                            </td>
                            <td align="center" class="P0">
                                <asp:TextBox ID="txtDepositPayAmt" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                                <asp:Literal ID="ltDepositPayAmtUnit" runat="server"></asp:Literal>
                            </td>
                            <td align="center" class="P0">
                                <span>
                                    <asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"
                                        OnClick="imgbtnRegist_Click" /></span>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <asp:TextBox ID="txtHfDepositTmpSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfDepositSeq" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upInterior" runat="server">
        <Triggers>
        <asp:PostBackTrigger ControlID="lnkbtnModify" />
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltInterior" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltInteriorStartDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtInteriorStartDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInteriorStartDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfInteriorStartDt" runat="server" />
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltInteriorEndDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtInteriorEndDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtInteriorEndDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfInteriorEndDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltConsDeposit" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtConsDeposit" runat="server" CssClass="grBg bgType2" MaxLength="18"
                                    Width="90"></asp:TextBox>
                                <asp:Literal ID="ltConsDepositUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltConsDepositDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtConsDepositDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtConsDepositDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfConsDepositDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltConsRefund" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtConsRefund" runat="server" CssClass="grBg bgType2" MaxLength="18"
                                    Width="90"></asp:TextBox>
                                <asp:Literal ID="ltConsRefundUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltConsRefundDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtConsRefundDt" runat="server" CssClass="grBg bgType1" MaxLength="18"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtConsRefundDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfConsRefundDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltDifferenceReason" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtDifferenceReason" runat="server" Columns="70" TextMode="MultiLine"
                                    Rows="10" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltOther" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltMemo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtMemo" runat="server" Columns="70" TextMode="MultiLine" Rows="10"
                                    CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal12" runat="server" Text="Remark"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" Columns="70" TextMode="MultiLine" Rows="10"
                                    CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal7" runat="server" Text="Remark"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtContRemark" runat="server" Columns="70" TextMode="MultiLine"
                                    Rows="10" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal15" runat="server" Text="Import Contract Files"></asp:Literal>
                            </th>
                            <td>
                                <asp:FileUpload ID="fileUpl" runat="server" 
                                     CssClass="bgType3"  />
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
                                    <asp:LinkButton ID="lnkbtnModify" runat="server" OnClick="lnkbtnModify_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:TextBox ID="txtHfExchangeRate" runat="server" Visible="false"></asp:TextBox>
            <asp:ImageButton ID="imgbtnResetCurrency" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnResetCurrency_Click" />
            <asp:ImageButton ID="imgDeleteFee" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgDeleteFee_Click" />
            <asp:ImageButton ID="imgDeleteRentFee" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgDeleteRentFee_Click" />
            <asp:HiddenField ID="hfFeeSeqDel" runat="server" />
            <asp:HiddenField ID="hfRentFeeSeq" runat="server" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfParamRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfParamRentSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="hfListFeeMng" runat="server" />
            <asp:HiddenField ID="hfListFitFeeMng" runat="server" />
            <asp:HiddenField ID="hfListRentFee" runat="server" />
            <asp:HiddenField ID="hfApplyFeeMn" runat="server" Value="N" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

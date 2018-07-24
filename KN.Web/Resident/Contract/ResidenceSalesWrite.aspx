<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ResidenceSalesWrite.aspx.cs" Inherits="KN.Web.Resident.Contract.ResidenceSalesWrite" %>

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
    function fnCheckValidate(strAlertText, strTanVal, strLongVal, strShortVal)
    {
        var strInCharge = document.getElementById("<%= txtInchage.ClientID %>");
        var strTenantNm = document.getElementById("<%= txtTenantNm.ClientID %>");
        var strTenantCd = document.getElementById("<%= ddlPersonal.ClientID %>");
        var strTermCd = document.getElementById("<%= ddlTerm.ClientID %>"); 
        var strAddr = document.getElementById("<%= txtAddr.ClientID %>");
        //var strDetAddr = document.getElementById("<%= txtDetAddr.ClientID %>");
        var strICPN = document.getElementById("<%= txtICPN.ClientID %>");
        var strIssueDt = document.getElementById("<%= txtIssueDt.ClientID %>");
        var strIssuePlace = document.getElementById("<%= txtIssuePlace.ClientID %>");
        var strTelFrontNo = document.getElementById("<%= txtTelFrontNo.ClientID %>");
        var strTelMidNo = document.getElementById("<%= txtTelMidNo.ClientID %>");
        var strTelRearNo = document.getElementById("<%= txtTelRearNo.ClientID %>");
        //var strMobileFrontNo = document.getElementById("<%= txtMobileFrontNo.ClientID %>");
        //var strMobileMidNo = document.getElementById("<%= txtMobileMidNo.ClientID %>");
        //var strMobileRearNo = document.getElementById("<%= txtMobileRearNo.ClientID %>");
        //var strFAXFrontNo = document.getElementById("<%= txtFAXFrontNo.ClientID %>");
        //var strFAXMidNo = document.getElementById("<%= txtFAXMidNo.ClientID %>");
        //var strFAXRearNo = document.getElementById("<%= txtFAXRearNo.ClientID %>");
        //var strEmailID = document.getElementById("<%= txtEmailID.ClientID %>");
        //var strEmailServer = document.getElementById("<%= txtEmailServer.ClientID %>");
        var strRepresent = document.getElementById("<%= txtRepresent.ClientID %>");
        var strPosition = document.getElementById("<%= txtPosition.ClientID %>");
        //var strBank = document.getElementById("<%= txtBank.ClientID %>");
        var strTaxCd = document.getElementById("<%= txtTaxCd.ClientID %>");
        //var strCoOwnerNm = document.getElementById("<%= txtCoOwnerNm.ClientID %>");
        //var strRelationShip = document.getElementById("<%= txtRelationShip.ClientID %>");
        //var strCoRss = document.getElementById("<%= txtCoRss.ClientID %>");
        //var strCoIssueDt = document.getElementById("<%= txtCoIssueDt.ClientID %>");
        //var strCoIssuePlace = document.getElementById("<%= txtCoIssuePlace.ClientID %>");
        //var strCoAddr = document.getElementById("<%= txtCoAddr.ClientID %>");
        //var strCoDetAddr = document.getElementById("<%= txtCoDetAddr.ClientID %>");
        var strCommencingDt = document.getElementById("<%= txtCommencingDt.ClientID %>");
        //var strExpiringDt = document.getElementById("<%= txtExpiringDt.ClientID %>");
        var strUnitNo = document.getElementById("<%= txtUnitNo.ClientID %>");
        var strFloor = document.getElementById("<%= txtFloor.ClientID %>");
        var strLeasingArea = document.getElementById("<%= txtLeasingArea.ClientID %>");

        var strSumRentVNDNo = document.getElementById("<%= txtSumRentVNDNo.ClientID %>");
        var strSumRentUSDNo = document.getElementById("<%= txtSumRentUSDNo.ClientID %>");
        var strPerMMRentVND = document.getElementById("<%= txtPerMMRentVND.ClientID %>");
        var strPerMMRentUSD = document.getElementById("<%= txtPerMMRentUSD.ClientID %>");
        var strSumDepositVNDNo = document.getElementById("<%= txtSumDepositVNDNo.ClientID %>");
        var strSumDepositUSDNo = document.getElementById("<%= txtSumDepositUSDNo.ClientID %>");

        //var strTradeNm = document.getElementById("<%= txtTradeNm.ClientID %>");
        //var strPurpose = document.getElementById("<%= txtPurpose.ClientID %>");
        //var strPlusCondDt = document.getElementById("<%= txtPlusCondDt.ClientID %>");
        //var strPlusCond = document.getElementById("<%= txtPlusCond.ClientID %>");
        //var strMemo = document.getElementById("<%= txtMemo.ClientID %>");            

        if (trim(strInCharge.value) == "") 
        {
            alert(strAlertText);
            strInCharge.focus();
            return false;
        }           
             
        if (trim(strTenantNm.value) == "") 
        {
            alert(strAlertText);
            strTenantNm.focus();
            return false;
        }           
        
        if (trim(strAddr.value) == "") 
        {
            alert(strAlertText);
            strAddr.focus();
            return false;
        } 
        /*
        if (trim(strDetAddr.value) == "") 
        {
            alert(strAlertText);
            strDetAddr.focus();
            return false;
        }
        */
        if (trim(strICPN.value) == "") 
        {
            alert(strAlertText);
            strICPN.focus();
            return false;
        } 
        
        if (trim(strIssueDt.value) == "") 
        {
            alert(strAlertText);
            strIssueDt.focus();
            return false;
        }        
        
        if (trim(strIssuePlace.value) == "") 
        {
            alert(strAlertText);
            strIssuePlace.focus();
            return false;
        } 
        
        if (trim(strTelFrontNo.value) == "") 
        {
            alert(strAlertText);
            strTelFrontNo.focus();
            return false;
        }                         
        
        if (trim(strTelMidNo.value) == "") 
        {
            alert(strAlertText);
            strTelMidNo.focus();
            return false;
        } 
        
        if (trim(strTelRearNo.value) == "") 
        {
            alert(strAlertText);
            strTelRearNo.focus();
            return false;
        }   
        
        /*
        if (trim(strMobileFrontNo.value) == "") 
        {
            alert(strAlertText);
            strMobileFrontNo.focus();
            return false;
        } 
        
        if (trim(strMobileMidNo.value) == "") 
        {
            alert(strAlertText);
            strMobileMidNo.focus();
            return false;
        } 
        
        if (trim(strMobileRearNo.value) == "") 
        {
            alert(strAlertText);
            strMobileRearNo.focus();
            return false;
        } 
        
        if (trim(strFAXFrontNo.value) == "") 
        {
            alert(strAlertText);
            strFAXFrontNo.focus();
            return false;
        } 
          
        if (trim(strFAXMidNo.value) == "") 
        {
            alert(strAlertText);
            strFAXMidNo.focus();
            return false;
        }
        
        if (trim(strFAXRearNo.value) == "") 
        {
            alert(strAlertText);
            strFAXRearNo.focus();
            return false;
        }
        
        if (trim(strEmailID.value) == "") 
        {
            alert(strAlertText);
            strEmailID.focus();
            return false;
        }
        
        if (trim(strEmailServer.value) == "") 
        {
            alert(strAlertText);
            strEmailServer.focus();
            return false;
        }          
        */
        
        if (strTenantCd.value == strTanVal)
        {
            if (trim(strRepresent.value) == "")
            {
                alert(strAlertText);
                strRepresent.focus();
                return false;
            }
            
            if (trim(strPosition.value) == "") 
            {
                alert(strAlertText);
                strPosition.focus();
                return false;
            }
            
            if (trim(strTaxCd.value) == "") 
            {
                alert(strAlertText);
                strTaxCd.focus();
                return false;
            }            
        }
        /*
        if (trim(strBank.value) == "") 
        {
            alert(strAlertText);
            strBank.focus();
            return false;
        }                        
        
        if (trim(strCoOwnerNm.value) == "") 
        {
            alert(strAlertText);
            strCoOwnerNm.focus();
            return false;
        }
        
        if (trim(strRelationShip.value) == "") 
        {
            alert(strAlertText);
            strRelationShip.focus();
            return false;
        }
        
        if (trim(strCoRss.value) == "") 
        {
            alert(strAlertText);
            strCoRss.focus();
            return false;
        }
        
        if (trim(strCoIssueDt.value) == "") 
        {
            alert(strAlertText);
            strCoIssueDt.focus();
            return false;
        }
        
        if (trim(strCoIssuePlace.value) == "") 
        {
            alert(strAlertText);
            strCoIssuePlace.focus();
            return false;
        }
        
        if (trim(strCoAddr.value) == "") 
        {
            alert(strAlertText);
            strCoAddr.focus();
            return false;
        }
        
        if (trim(strCoDetAddr.value) == "") 
        {
            alert(strAlertText);
            strCoDetAddr.focus();
            return false;
        }
        */
        if (trim(strCommencingDt.value) == "") 
        {
            alert(strAlertText);
            strCommencingDt.focus();
            return false;
        }
        
//        if (trim(strExpiringDt.value) == "") 
//        {
//            alert(strAlertText);
//            strExpiringDt.focus();
//            return false;
//        }
        
        if (trim(strUnitNo.value) == "") 
        {
            alert(strAlertText);
            strUnitNo.focus();
            return false;
        }
        
        if (trim(strFloor.value) == "") 
        {
            alert(strAlertText);
            strFloor.focus();
            return false;
        }
        
        if (trim(strLeasingArea.value) == "") 
        {
            alert(strAlertText);
            strLeasingArea.focus();
            return false;
        }
        
//        if (trim(strExchangeRate.value) == "") 
//        {
//            alert(strAlertText);
//            strExchangeRate.focus();
//            return false;
//        }
        
        if (strTermCd.value == strLongVal)
        {
            if (trim(strSumRentVNDNo.value) == "") 
            {
                alert(strAlertText);
                strSumRentVNDNo.focus();
                return false;
            }
            
            if (trim(strSumRentUSDNo.value) == "") 
            {
                alert(strAlertText);
                strSumRentUSDNo.focus();
                return false;
            }                                
        }
        
        if (strTermCd.value == strShortVal)
        {
            if (trim(strPerMMRentVND.value) == "") 
            {
                alert(strAlertText);
                strPerMMRentVND.focus();
                return false;
            }
            
            if (trim(strPerMMRentUSD.value) == "") 
            {
                alert(strAlertText);
                strPerMMRentUSD.focus();
                return false;
            }
            
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
        }                                                
        
//        if (trim(strPerMMMngVND.value) == "") 
//        {
//            alert(strAlertText);
//            strPerMMMngVND.focus();
//            return false;
//        }
//        
//        if (trim(strPerMMMngUSD.value) == "") 
//        {
//            alert(strAlertText);
//            strPerMMMngUSD.focus();
//            return false;
//        }
        /*
        if (trim(strTradeNm.value) == "") 
        {
            alert(strAlertText);
            strTradeNm.focus();
            return false;
        }
        
        if (trim(strPurpose.value) == "") 
        {
            alert(strAlertText);
            strPurpose.focus();
            return false;
        }
        
        if (trim(strPlusCondDt.value) == "") 
        {
            alert(strAlertText);
            strPlusCondDt.focus();
            return false;
        }
        
        if (trim(strPlusCond.value) == "") 
        {
            alert(strAlertText);
            strPlusCond.focus();
            return false;
        }
        
        if (trim(strMemo.value) == "") 
        {
            alert(strAlertText);
            strMemo.focus();
            return false;
        }    
        */
        getListFeeMng();
        getListFitFeeMng();
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

    function getListFitFeeMng() {
        if($('#<%=hfIsApplyFeeMn.ClientID%>').val()=="Y") {
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
    
    function fnResetCurrency()
    {
            <%=Page.GetPostBackEventReference(imgbtnResetCurrency)%>;
        return false;
    }

    function checkRoom() {
        <%=Page.GetPostBackEventReference(imgbtnCheckRoom)%>;
        return false;
    }

    function chkCCChange(a) {
        var chkCc = document.getElementById("<%= chkCC.ClientID %>");
        var cc = true;
        if (chkCc.checked) {
            document.getElementById("<%= hfCCtype.ClientID %>").value = 'CC';
            document.getElementById("<%= txtFC.ClientID %>").value = document.getElementById("<%= hfCCValue.ClientID %>").value;
            cc = true;
        } else {
            document.getElementById("<%= hfCCtype.ClientID %>").value = 'FC';
            $('#<%=txtFC.ClientID %>').focus();
            cc = false;            
        }
        
         $('#<%=txtFC.ClientID%>').attr('readonly', cc);        
    }

    function addFCValue() {
        var chkCc = document.getElementById("<%= chkCC.ClientID %>");
        if(!chkCc.checked) {
            $('#<%=hfFCValue.ClientID %>').val($('#<%=txtFC.ClientID %>').val());
        }
    }

    function fnApplyFee() {
        if(!$("#isApplyFeeMn").is(':checked')) {
            $("#listFee").hide("slow"); // checked0
            $('#<%=hfIsApplyFeeMn.ClientID%>').val("N");
            $('#lineRow').attr("style", "");           
        } else {
            $("#listFee").show("slow");
             $('#<%=hfIsApplyFeeMn.ClientID%>').val("Y"); 
            $('#lineRow').attr("style", "display: none");            
        }
    }    

    function editFitFee(feeNum) {    
        $('#txtFitFeeStartDt').val($("#fitfeeNum" + feeNum + " :text").eq(0).val());
        $('#txtFitFeeEndDt').val($("#fitfeeNum" + feeNum + " :text").eq(1).val());
        $('#txtFitFeeExcRate').val($("#fitfeeNum" + feeNum + " :text").eq(2).val());
        $('#txtFitFeeExpAmt').val($("#fitfeeNum" + feeNum + " :text").eq(3).val());
        $('#fitfeeID').val(feeNum);
    }

    var fitFeeNum = 0;
    function AddFitFee(){
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
            $('#fitfeeID').val("");
            
        } else {

            $('#tblListFitFee tbody').append(''+
                '<tr id="fitfeeNum'+fitFeeNum+'" ><td align="left" class="P0"><input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stSDate+'">' +                
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
            $('#feeID').val("");
            
        } else {

            $('#tblListFee tbody').append(''+
                '<tr id="feeNum'+feeNum+'" ><td align="left" class="P0"><input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stSDate+'">' +
                '</td>'+
                '<td align="left" class="P0">'+
                ' <input name="" type="text" maxlength="10" readonly="readonly" id="" class="grBg bgType1" style="width:70px;margin-left: 48px;" value="'+stEDate+'">'+               
                '</td>'+
                '<td align="center" class="P0">'+
                '<input name="" type="text"  maxlength="18" id="" style="width:70px;" value="'+vndFee+'" readonly="readonly">&nbsp;VND</td>'+
                '<td align="center" class="P0"><input name="" type="text" maxlength="18" id="" style="width:70px;" value="'+usdFee+'" readonly="readonly">&nbsp;$</td>'+
                '<td align="center" class="P0">'+
                '<span><image type="image" name="" id="" src="../../Common/Images/Icon/edit.gif"  style="border-width:0px;" onclick="editMngFee('+feeNum+')"></span>'+
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
    
    function editMngFee(feeNum) {    
        $('#txtFeeStartDt').val($("#feeNum" + feeNum + " :text").eq(0).val());
        $('#txtFeeEndDt').val($("#feeNum" + feeNum + " :text").eq(1).val());
        $('#txtFeeExcRate').val($("#feeNum" + feeNum + " :text").eq(2).val());
        $('#txtFeeExpAmt').val($("#feeNum" + feeNum + " :text").eq(3).val());
        $('#feeID').val(feeNum);
        
    }
    function deleteFee(thisFee) {
       // thisFee.parent().parent().delete();
         $('tr#feeNum' + thisFee).remove();
        $('#feeID').val("");
    }     
    function deleteFitFee(thisFee) {
       // thisFee.parent().parent().delete();
         $('tr#fitfeeNum' + thisFee).remove();
        $('#fitfeeID').val("");
    }     
    
      function success(response)
        {
           alert(response.d);
       }
       
       function fail(response)
        {
            alert("An error occurred.");
        }

 $(document).ready(function() {
     loadCalendar();
 });
//-->  

 function VNDtoUSD() {
           var currentFc = $('#<%=txtFC.ClientID %>').val();
           
            var num2 = $("#txtFeeExcRate").val();
           if (num2=="") {
               return; 
           }   
       var result = parseFloat(num2, 10) / parseFloat(currentFc, 10);
        
        $('#txtFeeExpAmt').val(result.toFixed(2));  
       $("#txtFeeExcRate").val(parseFloat(num2).toFixed(2));
 }

 function USDtoVND() {
           var currentFc = $('#<%= txtFC.ClientID %>').val();
           
            var num2 = $("#txtFeeExpAmt").val();
           if (num2=="") {
               return; 
           }
       var result = parseFloat(num2, 10) * parseFloat(currentFc, 10);
        
        $('#txtFeeExcRate').val(result.toFixed(2));  
        $("#txtFeeExpAmt").val(parseFloat(num2).toFixed(2));
 }
 function fitVNDtoUSD() {
           var currentFc = $('#<%=txtFC.ClientID %>').val();
           
            var num2 = $("#txtFitFeeExcRate").val();
           if (num2=="") {
               return; 
           }   
       var result = parseFloat(num2, 10) / parseFloat(currentFc, 10);
        
        $('#txtFitFeeExpAmt').val(result.toFixed(2)); 
     $("#txtFitFeeExcRate").val(parseFloat(num2).toFixed(2));
 }

 function fitUSDtoVND() {
           var currentFc = $('#<%=txtFC.ClientID %>').val();
           
            var num2 = $("#txtFitFeeExpAmt").val();
           if (num2=="") {
               return; 
           }
       var result = parseFloat(num2, 10) * parseFloat(currentFc, 10);
        
        $('#txtFitFeeExcRate').val(result.toFixed(2));  
     $("#txtFitFeeExpAmt").val(parseFloat(num2).toFixed(2));
 } 



 function loadCalendar() {
  

    $( "#txtFeeStartDt").datepicker();
    $( "#txtFeeEndDt").datepicker();
    $( "#txtFitFeeStartDt").datepicker();
    $( "#txtFitFeeEndDt").datepicker();  
     $( "#<%=txtMSPayDate.ClientID %>").datepicker();
      $( "#<%=txtMSUsingDt.ClientID %>").datepicker();
      
    $("#<%=txtIssueDt.ClientID %>").datepicker({
        altField: "#<%=hfIssueDt.ClientID %>"
        });
    $("#<%=txtCoIssueDt.ClientID %>").datepicker({
        altField: "#<%=hfCoIssueDt.ClientID %>"
        });
    $("#<%=txtCommencingDt.ClientID %>").datepicker({
        altField: "#<%=hfCommencingDt.ClientID %>"
        });
    $("#<%=txtExpiringDt.ClientID %>").datepicker({
        altField: "#<%=hfExpiringDt.ClientID %>"
        });
    $("#<%=txtLastKeyDt.ClientID %>").datepicker({
        altField: "#<%=hfLastKeyDt.ClientID %>"
        });
    $("#<%=txtPlusCondDt.ClientID %>").datepicker({
        altField: "#<%=hfPlusCondDt.ClientID %>"
        });
     initControl();
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

 function initControl() {
    $("#<%=ddlLessor.ClientID %>").change( function() {
      // check input ($(this).val()) for validity here
        $('#<%=txtLessorRoomNo.ClientID %>').val($(this).val());
    });  
     
//	$(window).bind('beforeunload', function(){
//		return 'Do you want to cancel this contract ?';
//	});     
 }
       
    </script>
    <asp:UpdatePanel ID="upBasicInfo" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlContTy" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTerm" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPersonal" EventName="SelectedIndexChanged" />
            <%--        <asp:AsyncPostBackTrigger ControlID="txtSumRentVNDNo" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtSumRentUSDNo" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtPerMMRentVND" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtPerMMRentUSD" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtSumDepositVNDNo" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtSumDepositUSDNo" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtUnitNo" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtFloor" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="imgbtnResetCurrency" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnWrite" />
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltBasicInfo" runat="server"></asp:Literal>
                (<asp:Literal ID="ltIncharge" runat="server"></asp:Literal>
                :
                <asp:TextBox ID="txtInchage" runat="server" MaxLength="20" Width="100px" CssClass="bgType2"></asp:TextBox>)
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
                                    <asp:ListItem Value="VND" Selected="True">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSpecialContract" runat="server" Text="Is Special Contract" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Sub Lessor Of"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlLessor" runat="server" Width="600">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Room No"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtLessorRoomNo" runat="server" MaxLength="30" Width="150" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlPersonal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonal_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtTenantNm" runat="server" MaxLength="245" Width="480" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltContNo" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlContTy" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTerm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:TextBox ID="txtContNo" runat="server" CssClass="grBg bgType2" MaxLength="20"
                                    ReadOnly="true" Width="150"></asp:TextBox>
                                <asp:HiddenField ID="hfContNo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltAddr" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:TextBox ID="txtDetAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltICPN" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtICPN" runat="server" CssClass="grBg bgType2" MaxLength="30" Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltIssueDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtIssueDt" runat="server" CssClass="grBg bgType2" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtIssueDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfIssueDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltIssuePlace" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtIssuePlace" runat="server" CssClass="grBg bgType2" MaxLength="255"
                                    Width="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltTel" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtTelFrontNo" runat="server" CssClass="grBg bgType2" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtTelMidNo" runat="server" CssClass="grBg bgType2" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtTelRearNo" runat="server" CssClass="grBg bgType2" MaxLength="4"
                                    Width="35"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltMobileNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtMobileFrontNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtMobileMidNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtMobileRearNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltFAX" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtFAXFrontNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtFAXMidNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                                -&nbsp;<asp:TextBox ID="txtFAXRearNo" runat="server" CssClass="grBg bgType3" MaxLength="4"
                                    Width="35"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="grBg bgType3" MaxLength="50"
                                    Width="80"></asp:TextBox>
                                @&nbsp;<asp:TextBox ID="txtEmailServer" runat="server" CssClass="grBg bgType3" MaxLength="50"
                                    Width="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRepresent" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRepresent" runat="server" CssClass="grBg bgType3" MaxLength="255"
                                    Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltPosition" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPosition" runat="server" CssClass="grBg bgType3" MaxLength="50"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltBank" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtBank" runat="server" CssClass="grBg bgType3" MaxLength="50" Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltTaxCd" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtTaxCd" runat="server" CssClass="grBg bgType3" MaxLength="20"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltConcYn" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlConcYn" runat="server">
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltCoInfo" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltCoOwnerNm" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCoOwnerNm" runat="server" MaxLength="50" Width="150" CssClass="bgType3"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltRelationShip" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRelationShip" runat="server" CssClass="grBg bgType3" MaxLength="20"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltCoRss" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCoRss" runat="server" CssClass="grBg bgType3" MaxLength="30"
                                    Width="150"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Literal ID="ltCoIssueDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCoIssueDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtCoIssueDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfCoIssueDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltCoIssuePlace" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtCoIssuePlace" runat="server" CssClass="grBg bgType3" MaxLength="255"
                                    Width="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltCoAddr" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtCoAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:TextBox ID="txtCoDetAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType3"></asp:TextBox>
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
                                <asp:Literal ID="ltCommencingDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCommencingDt" runat="server" CssClass="grBg bgType2" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtCommencingDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfCommencingDt" runat="server" />
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltExpiringDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtExpiringDt" runat="server" CssClass="grBg bgType3" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtExpiringDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfExpiringDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLastKeyDt" runat="server"></asp:Literal>&nbsp;(Last Key)
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtLastKeyDt" runat="server" CssClass="grBg bgType3" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtLastKeyDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfLastKeyDt" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRoomInfo" runat="server"></asp:Literal></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgbtnCheckRoom" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" class="TbCel-Type2-A">
                        <colgroup>
                            <col width="147px" />
                            <col width="178px" />
                            <col width="147px" />
                            <col width="178px" />
                            <tbody>
                                <tr>
                                    <th>
                                        <asp:Literal ID="ltUnitNo" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtUnitNo" runat="server" MaxLength="10" CssClass="bgType2"></asp:TextBox>
                                    </td>
                                    <th class="lebd">
                                        <asp:Literal ID="ltRoomNoExt" Text="Room No Extend" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtRoomNoExt" runat="server" MaxLength="10" CssClass="bgType2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:Literal ID="ltLeasingArea" runat="server"></asp:Literal>
                                    </th>
                                    <td >
                                        <asp:TextBox ID="txtLeasingArea" runat="server" MaxLength="10" CssClass="bgType2"></asp:TextBox>&nbsp;㎡
                                    </td>
                                    <th class="lebd">
                                        <asp:Literal ID="ltFloor" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtFloor" runat="server" MaxLength="3" CssClass="bgType2"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                            </tbody>
                        </colgroup>
                    </table>
                    <asp:ImageButton ID="imgbtnCheckRoom" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                        OnClick="imgbtnCheckRoom_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="Tb-Tp-tit">
                Exchange Rate</div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                </colgroup>
                <tbody>
                    <tr>
                        <th>
                            <asp:Literal ID="ltExchangeRate" runat="server"></asp:Literal>
                        </th>
                        <td colspan="2">
                            <asp:TextBox ID="txtFC" runat="server" AutoPostBack="False" CssClass="bgType2" MaxLength="10"
                                ReadOnly="True"></asp:TextBox>
                            <asp:CheckBox ID="chkCC" runat="server" Text="Current Currency" Checked="True" AutoPostBack="False" />
                            <asp:HiddenField ID="hfCCtype" runat="server" Value="CC" />
                            <asp:HiddenField ID="hfCCValue" runat="server" />
                            <asp:HiddenField ID="hfFCValue" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblFloat" runat="server" Text="Inflation(%)"></asp:Label>
                            <asp:TextBox ID="txtFloation" runat="server" AutoPostBack="true" CssClass="bgType2"
                                MaxLength="3" Width="67px">0.1</asp:TextBox>
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
                        <!--// 매매가(보증금)관련가격 //-->
                        <tr>
                            <th>
                                <asp:Literal ID="ltSumRentVNDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSumRentVNDNo" runat="server" MaxLength="18" Width="100" CssClass="bgType2"
                                    AutoPostBack="False" OnTextChanged="txtSumRentVNDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltSumRentVNDNoUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltSumRentUSDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSumRentUSDNo" runat="server" MaxLength="18" Width="100" CssClass="bgType2"
                                    AutoPostBack="False" OnTextChanged="txtSumRentUSDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltSumRentUSDNoUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <!--// 임대관련 가격 //-->
                        <tr>
                            <th>
                                <asp:Literal ID="ltPerMMRentVND" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPerMMRentVND" runat="server" MaxLength="18" Width="100" CssClass="bgType3"
                                    AutoPostBack="true" OnTextChanged="txtPerMMRentVND_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltPerMMRentVNDUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltPerMMRentUSD" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPerMMRentUSD" runat="server" MaxLength="18" Width="100" CssClass="bgType3"
                                    AutoPostBack="true" OnTextChanged="txtPerMMRentUSD_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltPerMMRentUSDUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
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
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltSumDepositUSDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtSumDepositUSDNo" runat="server" MaxLength="18" Width="100" CssClass="bgType3"
                                    AutoPostBack="true" OnTextChanged="txtSumDepositUSDNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltDepositSumUSDNoUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                Fit Out Management Fee
                <div style="float: right" id="chkUsingMnFee">
                    <input type="checkbox" onclick="fnApplyFee()" id="isApplyFeeMn" checked="checked" />Apply
                    Fit Out Management
                </div>
                <asp:HiddenField ID="hfIsApplyFeeMn" runat="server" Value="Y" />
            </div>
            <div id="lineRow" style="display: none">
            </div>
            <div id="listFee">
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
                <table cellspacing="0" class="TbCel-Type2-A" id="tblListFitFee">
                    <colgroup>
                        <col width="185px">
                        <col width="185px">
                        <col width="185px">
                        <col width="185px">
                        <col width="80px">
                        <tbody>
                        </tbody>
                    </colgroup>
                    <asp:HiddenField ID="hfListFeeMng" runat="server" />
                    <asp:HiddenField ID="hfListFitFeeMng" runat="server" />
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
                                    <asp:HiddenField ID="hfRentalFeeStartDt" runat="server" />
                                </td>
                                <td align="center" class="P0">
                                    <input type="text" id="txtFitFeeEndDt" class="grBg bgType2" maxlength="10" style="width: 70px"
                                        readonly="true" />
                                    <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#txtFitFeeEndDt')" src="/Common/Images/Common/calendar.gif"
                                        style="cursor: pointer; align: absmiddle;" />
                                    <asp:HiddenField ID="hfRentalFeeEndDt" runat="server" />
                                </td>
                                <td align="center" class="P0">
                                    <input type="text" id="txtFitFeeExcRate" maxlength="10" style="width: 70px" onblur="fitVNDtoUSD()"
                                        onkeypress="javascript:IsNumeric(this, 'Please enter only numbers.');" />
                                    <asp:Literal ID="ltFeeExcRateUnit" runat="server"></asp:Literal>
                                    &nbsp;VND
                                </td>
                                <td align="center" class="P0">
                                    <input type="text" id="txtFitFeeExpAmt" maxlength="18" style="width: 70px" onblur="fitUSDtoVND();"
                                        onkeypress="javascript:IsNumeric(this, 'Please enter only numbers.');" />
                                    <asp:Literal ID="ltFeeAmtUnit" runat="server"></asp:Literal>&nbsp;$
                                </td>
                                <td align="center" class="P0">
                                    <span>
                                        <%--<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif"/></span>--%>
                                        <img style="border-width: 0px;" id="btnAddFee" src="../../Common/Images/Icon/plus.gif"
                                            type="image" onclick="AddFitFee()" alt="Add Fit Fee"></span>
                                </td>
                            </tr>
                        </tbody>
                    </colgroup>
                </table>
            </div>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltMngFee" runat="server" Text="Managerment Fee"></asp:Literal></div>
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
                                    <asp:ListItem Text="B" Value="M">By monthly</asp:ListItem>
                                    <asp:ListItem Text="O" Value="Q">By round monthly</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Literal ID="Literal2" runat="server" Text="Current Pay Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtMSPayDate" runat="server" MaxLength="10" Width="100" CssClass="bgType2"></asp:TextBox>
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
                                <input type="hidden" id="feeID" value="" />
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
                                <input type="text" id="txtFeeExcRate" maxlength="10" style="width: 70px" onblur="VNDtoUSD()"
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
                                    <img style="border-width: 0px;" id="Img1" src="../../Common/Images/Icon/plus.gif"
                                        type="image" onclick="AddMngFee()" alt="Add Fee"></span>
                                <input type="hidden" id="Hidden1" value="" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltUse" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltTradeNm" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtTradeNm" runat="server" MaxLength="255" Width="100" CssClass="bgType3"></asp:TextBox>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltPurpose" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPurpose" runat="server" MaxLength="255" Width="100" CssClass="bgType3"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltOtherCondition" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPlusCondDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPlusCondDt" runat="server" CssClass="grBg bgType3" MaxLength="10"
                                    Width="70" ReadOnly="true"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPlusCondDt.ClientID%>')"
                                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfPlusCondDt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPlusCond" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPlusCond" runat="server" Columns="70" TextMode="MultiLine" Rows="10"
                                    CssClass="bgType3"></asp:TextBox>
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
                                <asp:Literal ID="Literal4" runat="server" Text="Remark"></asp:Literal>
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
                                <asp:Literal ID="Literal15" runat="server" Text="Select contract files"></asp:Literal>
                            </th>
                            <td>
                                <asp:FileUpload ID="ContractFileUpload" runat="server" CssClass="bgType3" />
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
                                    <asp:LinkButton ID="lnkbtnWrite" runat="server" OnClick="lnkbtnWrite_Click"></asp:LinkButton></span>
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
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="PrintHoadonMerge.aspx.cs" Inherits="KN.Web.Settlement.Balance.PrintHoadonMerge" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
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
       
        return true;
    }
    
    $(document).ready(function () {
        callCalendar();  
        formatMoney();    
        
    });

    function callCalendar() {   
        $("#<%=txtSearchDt.ClientID %>").monthpicker({  
        });  
       
        $("#<%=txtPayDay.ClientID %>").datepicker({  
        });    
        $("#<%=txtPayDay.ClientID %>").datepicker("setDate", new Date());        
    }

    
    function formatMoney() {

        $('#<%=txtAmount.ClientID %>,#<%=txtExRate.ClientID %>').blur(function() {
            var inputAmt = $('#<%=txtAmount.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");
            var exchangeRt = $('#<%=txtExRate.ClientID %>').val().trim().replace(new RegExp(",", "g"), "");

            

            if ($('#<%=txtExRate.ClientID %>').val() == "") {
                    $('#<%=txtRealAmount.ClientID %>').val($('#<%=txtAmount.ClientID %>').val());
                } else {

                    var amount = (parseFloat(inputAmt)) - parseFloat(exchangeRt);
                    $('#<%=txtRealAmount.ClientID %>').val(amount);
                    //alert(amount);
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

     function fnOccupantList(strPayDt, strPrintDt, strRefPrint) {

      document.getElementById("<%=hfRefPrint.ClientID%>").value = strRefPrint;       

            window.open("/Common/RdPopup/RDPopupHoaDonParkingMergeAPTPreview.aspx?Datum0=" + strPayDt + "&Datum1=" + strPrintDt + "&Datum2=" + strRefPrint ,"MergeInvoice","status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            
            return false;
        }

     function Func1(printSeq) 
        {
            //alert(printSeq);
            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
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
    
     <ul class="sf5-ag MrgL30 bgimgN">
            <li><b><asp:Literal ID="Literal2" runat="server" Text="Pay Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="60px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>                                
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

<div style="overflow-x:hidden;height:100px;width:840px;border-bottom: solid 2px rgb(182, 182, 182)">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>

       
    </Triggers>
    <ContentTemplate>
            <p style="text-align:right"><b><asp:Literal ID="ltMaxNo" runat="server" Text = "Max Number"></asp:Literal></b>&nbsp;:&nbsp;<asp:Literal ID="ltInsMaxNo" runat="server"></asp:Literal></p>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="40px" />
                    <col width="60px" />
                    <col width="70px" />
                    <col width="80px" />
                    <col width="280px" />
                    <col width="120px" />
                    <col width="130px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack = "true"
                                 Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Tenant Name"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </th>
                        <th class="Ls-line">
                            <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 100px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" >
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="40px" />                           
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
                            <col width="240px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter" runat="server" id="tdChk">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>                                

                            </td>
                          
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAmtViNo" runat="server" Width="110" CssClass="TbTxtRight"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtInsRegDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
                                <asp:Literal ID="ltCalendarImg" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfInsRegDt" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="30px" />                            
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
                            <col width="280px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr>
                                <td colspan="8" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
</asp:UpdatePanel>
</div>
<div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="ltCoInfo" runat="server" Text="Total Payment"></asp:Literal></div>
    <div class="Tb-Tp-tit" style="width: 50%;float: left"><asp:Literal ID="Literal8" runat="server" Text="Invoice Amount"></asp:Literal></div>
</div>

        <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
                
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0"  class="TbCel-Type2" style="margin-bottom: 10px;">
                    <tr>
	                    <th align="center">
           	                <asp:Literal ID="ltAmount" runat="server" Text="Total Amount"></asp:Literal>
           	            </th>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                        </td> 
                              
                        <th align="center">
                            <asp:Literal ID="ltPaydt" runat="server" Text="Print Date"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtPayDay" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDay.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                        </td>
                    </tr>
                    <tr>
           	            
           	            <th align="center">
           	                <asp:Literal ID="Literal6" runat="server" Text="Issuing Amount"></asp:Literal>
           	            </th>
                        <td>
                            <asp:TextBox ID="txtExRate" runat="server" CssClass="bgType2" MaxLength="18" Width="100" ReadOnly="True"></asp:TextBox>
                        </td>
           	            <th align="center">
           	                <asp:Literal ID="Literal7" runat="server" Text="Invoice Amount"></asp:Literal>
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
		                            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" 
                                        OnClick="lnkbtnRegist_Click" Text="Print" Width="30px"></asp:LinkButton></span>
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
                   <asp:HiddenField ID="hfRefPrint" runat="server" Value=""/>
                  
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>             
          
</asp:Content>

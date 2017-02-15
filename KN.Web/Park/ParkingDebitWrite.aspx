<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ParkingDebitWrite.aspx.cs" Inherits="KN.Web.Park.ParkingDebitWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
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
        var carTy = $('#<%=ddlCarTy.ClientID %>').val();
        if (carTy=="0000") {
            alert(strTxt);
            $('#<%=ddlCarTy.ClientID %>').focus();
            return false;
        }
        var txtDesReg = $('#<%=txtDesReg.ClientID %>').val();
        if (txtDesReg=="") {
            alert(strTxt);
            $('#<%=txtDesReg.ClientID %>').focus();
            return false;
        }
        var txtUnitReg = $('#<%=txtUnitReg.ClientID %>').val();
        if (txtUnitReg=="") {
            alert(strTxt);
            $('#<%=txtUnitReg.ClientID %>').focus();
            return false;
        }
        var txtQuantityReg = $('#<%=txtQuantityReg.ClientID %>').val();
        if (txtQuantityReg=="") {
            alert(strTxt);
            $('#<%=txtQuantityReg.ClientID %>').focus();
            return false;
        }
        var txtUnitPriceReg = $('#<%=txtUnitPriceReg.ClientID %>').val();
        if (txtUnitPriceReg=="") {
            alert(strTxt);
            $('#<%=txtUnitPriceReg.ClientID %>').focus();
            return false;
        } 
        
        var txtAmountReg = $('#<%=txtAmountReg.ClientID %>').val();
        if (txtAmountReg=="") {
            alert(strTxt);
            $('#<%=txtAmountReg.ClientID %>').focus();
            return false;
        }      
//        var txtRemarkReg = $('#<%=txtRemarkReg.ClientID %>').val();
//        if (txtRemarkReg=="") {
//            alert(strTxt);
//            $('#<%=txtRemarkReg.ClientID %>').focus();
//            return false;
//        }         
        return true;
    }
    
    $(document).ready(function () {
        callCalendar();
        var now = new Date();
        $("#<%=txtSearchDt.ClientID %>").val(now.format("yyyy-MM"));
    });

    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").monthpicker({  
        });
        
        $('#<%=ddlCarTy.ClientID %>').live('change',function () {
            var carTy = $('#<%=ddlCarTy.ClientID %>').val();
            var dateTime = $('#<%=txtSearchDt.ClientID %>').val();
            if (carTy=='0001') {
                $('#<%=txtDesReg.ClientID %>').val('Ô tô (Car) ' + dateTime);
            }
            if (carTy=='0002') {
                $('#<%=txtDesReg.ClientID %>').val('Xe máy (Motobike) ' + dateTime);
            }
            if (carTy=='0003') {
                $('#<%=txtDesReg.ClientID %>').val('Fee Exemption' + dateTime);
            }              
            if (carTy=='0004') {
                $('#<%=txtDesReg.ClientID %>').val('Phí thẻ từ Ô tô (Car card fee) ' + dateTime);
            }        
        });
        formatMoney();
    }    
    function SaveSuccess() {
        alert('Save Successful !');         
    }   
    
    function formatMoney() {
        
        $('#<%=txtQuantityReg.ClientID %>,#<%=txtUnitPriceReg.ClientID %>').blur(function() {
            var inputAmt = $('#<%=txtUnitPriceReg.ClientID %>').val().trim();
            var exchangeRt = $('#<%=txtQuantityReg.ClientID %>').val().trim();
            if(inputAmt=='' || exchangeRt=='')
                return;
            var amount = (parseFloat(inputAmt)) * parseFloat(exchangeRt);
            $('#<%=txtAmountReg.ClientID %>').val(amount);

        });       
    }
//-->
</script>
<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
        <li><asp:Literal ID="ltComPanyName" runat="server"></asp:Literal></li>
        <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></li>  
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Month :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
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
<table class="TypeA" width="840px">
    <col width="40"/>
    <col width="80"/>
    <col width="200"/>
    <col width="40"/>
    <col width="40"/>
    <col width="100"/>
    <col width="100"/>
    <col width="200"/>
    <col width="110"/>
    <thead>
        <tr>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopSeq" runat="server" Text="Seq"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopDebitDt" runat="server" Text="Period"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="lttopDes" runat="server" Text="Description"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopUnit" runat="server" Text="Unit"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopQuantity" runat="server" Text="Quantity"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopUnitPrice" runat="server" Text="Unit Price"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopAmout" runat="server" Text="Amount"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopMark" runat="server" Text="Remark"></asp:Literal></th>
            <th class="TbTxtCenter" >&nbsp;</th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:315px;width:840px;">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound"
                OnItemUpdating="lvActMonthParkingCardList_ItemUpdating" OnItemDeleting="lvActMonthParkingCardList_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="40"/>
                    <col width="80"/>
                    <col width="200"/>
                    <col width="40"/>
                    <col width="40"/>
                    <col width="100"/>
                    <col width="100"/>
                    <col width="200"/>
                    <col width="90"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfSeq" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltDebitDt" runat="server"></asp:Literal>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:TextBox ID="txtDes" runat="server" MaxLength="16" Width="150"></asp:TextBox>                       
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltUnit" runat="server"></asp:Literal>                        
                    </td>
		            <td class="TbTxtCenter">
		                 <asp:TextBox ID="txtQuantity" runat="server" MaxLength="16" Width="40"></asp:TextBox>
		            </td>
		            <td class="TbTxtCenter">
                         <asp:TextBox ID="txtUnitPrice" runat="server" MaxLength="16" Width="70"></asp:TextBox>
		            </td>
		            <td class="TbTxtCenter">
		               <asp:TextBox ID="txtAmount" runat="server" MaxLength="16" Width="70"></asp:TextBox>
		            </td>
                    <td class="TbTxtCenter">
                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="16" Width="150"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/>
                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/>
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
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0"  class="TbCel-Type1 iw840">
            <tr>
                <th align="center"><asp:Literal ID="ltRegCarTy" runat="server" Text="Car Type"></asp:Literal></th>
                <td><asp:DropDownList ID="ddlCarTy" runat="server"></asp:DropDownList></td>
	            <th align="center"><asp:Literal ID="ltRegParkingCardNo" runat="server" Text="Desciption"></asp:Literal></th>
                <td>
                    <asp:TextBox ID="txtDesReg" runat="server" CssClass="bgType2" MaxLength="300" Width="250"></asp:TextBox>
                </td>
	            <th align="center">
	                <asp:Literal ID="ltRegCardFee" runat="server" Text="Unit"></asp:Literal>
	            </th>
                <td>
                    <asp:TextBox ID="txtUnitReg" runat="server" CssClass="bgType2" MaxLength="18" Width="100" Text="EA"></asp:TextBox>
                </td>
	        </tr>
            <tr>
	            <th align="center">
	                <asp:Literal ID="ltRegParkingFee" runat="server" Text="Quantity"></asp:Literal>
	            </th>
                <td>
                    <asp:TextBox ID="txtQuantityReg" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                </td>
                <th align="center">
                    <asp:Literal ID="ltRegTotalFee" runat="server" Text="Unit Price"></asp:Literal>
                </th>
                <td>
                    <asp:TextBox ID="txtUnitPriceReg" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                </td>
	            <th align="center">
	                <asp:Literal ID="Literal3" runat="server" Text="Amount"></asp:Literal>
	            </th>
                <td>
                    <asp:TextBox ID="txtAmountReg" runat="server" CssClass="bgType2" MaxLength="18" Width="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
           	    <th align="center"><asp:Literal ID="ltRegPaymentCd" runat="server" Text="Remark"></asp:Literal></th>
                <td colspan="5">
                    <asp:TextBox ID="txtRemarkReg" runat="server" CssClass="bgType2" MaxLength="500" Width="714"></asp:TextBox>
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
        <asp:HiddenField ID="hfSeq" runat="server" Value="0"/>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
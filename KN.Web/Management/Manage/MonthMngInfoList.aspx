<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthMngInfoList.aspx.cs" Inherits="KN.Web.Management.Manage.MonthMngInfoList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            $( "#<%=txtFeeAppDt.ClientID %>" ).monthpicker();
        }
    }         
        function fnMovePage(intPageNo) 
        {
            if (intPageNo == null) 
            {
                intPageNo = 1;
            }
            
            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }

        function fnDetailView(strMngYear, strMngMM)
        {


            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
        }
        $(document).ready(function() {
            $( "#<%=txtFeeAppDt.ClientID %>" ).monthpicker();
        });

        var isClose = true;
        function openFrmAddFee() {
            if (isClose) {
                $('#frmAddFeeMn').show("slow");         
            } else {
                $('#frmAddFeeMn').hide("slow");        
            }
            isClose = !isClose;   
        }

        function closeFrmAddFee() {
            // $('#frmAddFeeMn').hide("slow");
            isClose = true;
        }


        function formatText(textControl) {
            $(textControl).val(parseFloat($(textControl).val()).toFixed(2));
        }

        function fnConfirmSave() {
            var dateTime = $('#<%=txtFeeAppDt.ClientID %>').val();
            var feeNet = $('#<%=txtFeeNet.ClientID %>').val();
            var vatFee = $('#<%=txtVAT.ClientID %>').val();
            if (dateTime=="") {
                alert('Please input all infomation !');
                $('#<%=txtFeeAppDt.ClientID %>').focus();
                return false;
            }
            if (feeNet=="") {
                alert('Please input all infomation !');
                $('#<%=txtFeeNet.ClientID %>').focus();
                return false;
            }
            if (vatFee=="") {
                alert('Please input all infomation !');
                $('#<%=txtVAT.ClientID %>').focus();
                return false;
            }     
            return true;
        }
    </script>
    <style type="text/css">
        #frmAddFeeMn {
          height: 180px;
          padding: 10px 5px;
          border: 1px solid rgb(192, 192, 192);            
        }
        .btnFix {
            position: fixed;margin-top: 245px;margin-left: 600px
        }
    </style>
	<div class="TpAtit1">
	    <div style="float: left">
        <span class="shf-sel FloatR2">
           Applied Fee by Date : <%--<td align="center"><asp:TextBox ID="txtSearchDt" runat="server" CssClass="grBg bgType2" MaxLength="10" Width="70"></asp:TextBox>
                                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  />--%>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                 </span> </div>
                <div class="Btn-Type4-wp">
		            <div class="Btn-Tp4-L">
			            <div class="Btn-Tp4-R">
				            <div class="Btn-Tp4-M">
					            <span><asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" Text="Search"></asp:LinkButton></span>
				            </div>
			            </div>
		            </div>
	            </div>
       
	</div>
        <table class="TypeA">
                <colgroup>
                         <col width="15%">
                            <col width="25%">
                            <col width="20%">
                            <col width="10%">
                            <col width="20%"> 
                            <col width="15%">                   
	            </colgroup>
                <thead>
                    <tr>
                        <th>Applied Date</th>
                        <th>Fee Type</th>
                        <th>Management Fee(Net)</th>
                        <th>VAT(%)</th>
                        <th>Unit Price</th>
                        <th>&nbsp;</th>
                    </tr>
	            </thead>
            </table>
            <div id="ctl00_cphContent_upListPanel">
            <asp:ListView ID="lvMngFeeList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" 
                    OnItemCreated="lvMngFeeList_ItemCreated" 
                    OnItemDataBound="lvMngFeeList_ItemDataBound" 
                    onitemdeleting="lvMngFeeList_ItemDeleting" 
                    onitemupdating="lvMngFeeList_ItemUpdating">
                <LayoutTemplate>
                    <table class="TypeA">
                            <col width="15%">
                            <col width="25%">
                            <col width="20%">
                            <col width="10%">
                            <col width="20%"> 
                            <col width="15%">   
                        <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter"><asp:Literal ID="ltAppliedDt" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfFeeType" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfMngFeeCode" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfMngYear" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfMngMM" runat="server" Visible="false"></asp:TextBox>                        
                        </td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltFeeName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:TextBox ID="txtMngFee" runat="server" CssClass="grBg bgType2" MaxLength="10" Width="70"></asp:TextBox></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltVAT" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltUnitPrice" runat="server"></asp:Literal></td>
                        <td align="center" class="P0">                          
                            <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" />                           
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                        <tr>
                            <td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>        
            </div>
            <div ><span id="spanPageNavi" runat="server" style="width:100%"></span></div>
            <div id="frmAddFeeMn">
                <div>
                    <div style="width: 100%;float: left;margin-bottom: 10px">
                       Unit Price :<b> Management Fee </b>
                    </div>
                    <div style="margin-bottom: 10px;">
                    <table class="TypeA" style="width: 830px">
                            <colgroup>
                                <col width="30%">
                                <col width="30%">
                                <col width="30%">
                                                
	                        </colgroup><thead>
                                <tr>
                                    <th>Applied Date</th>
                                    <th>Management Fee(Net)</th>
                                    <th>VAT(%)</th>
                                   
                                </tr>
	                        </thead>
                        </table>                        
                    </div>
                    <div>
                    <table class="TypeA" >
                            <colgroup>
                                <col width="30%">
                                <col width="30%">
                                <col width="30%">                                              
	                        </colgroup><thead>
                                <tr>
                                  <td align="center"><asp:TextBox ID="txtFeeAppDt" runat="server" CssClass="grBg bgType2" MaxLength="10" Width="70"></asp:TextBox>&nbsp;<img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtFeeAppDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></td>                                   
                                   <td align="center"><asp:TextBox ID="txtFeeNet" runat="server" CssClass="grBg bgType2" MaxLength="10" Width="70"></asp:TextBox></td>
                                   <td align="center"><asp:TextBox ID="txtVAT" runat="server" CssClass="grBg bgType2" MaxLength="10" Width="70" ReadOnly="True"></asp:TextBox></td>
                                </tr>
	                        </thead>
                        </table>                          
                    </div>
                    <div class="Btwps FloatR2" style="padding-right: 0px">
	                        <div class="Btn-Type3-wp FloatL">
		                        <div class="Btn-Tp3-L">
			                        <div class="Btn-Tp3-R">
				                        <div class="Btn-Tp3-M">
					                        <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Save"></asp:LinkButton></span>
				                        </div>
			                        </div>
		                        </div>
	                        </div>
	                        <div class="Btn-Type3-wp FloatL" onclick="closeFrmAddFee()">
		                        <div class="Btn-Tp3-L">
			                        <div class="Btn-Tp3-R">
				                        <div class="Btn-Tp3-M">
					                        <span><a  id="A3" href="#">Cancel</a></span>
				                        </div>
			                        </div>
		                        </div>
	                        </div>
                        </div> 
                </div>
            </div>          
            <asp:HiddenField ID="hfAlertText" runat="server"/>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
            <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
            <asp:HiddenField ID="hfCurrentPage" runat="server"/>
            <asp:HiddenField ID="txtHfRentCd" runat="server"/>
            <asp:HiddenField ID="txtHfFeeTy" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>    
</asp:Content>
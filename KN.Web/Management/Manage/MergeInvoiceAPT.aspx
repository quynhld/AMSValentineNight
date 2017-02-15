<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="MergeInvoiceAPT.aspx.cs" Inherits="KN.Web.Management.Manage.MergeInvoiceAPT" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
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
   
   
        return true;
    }
    
    $(document).ready(function () {
        callCalendar();
        onLoadinit();
    });

    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").monthpicker({  
        });     
        $("#<%=txtPaidDt.ClientID %>").datepicker({  
        });          
        
			$("#<%=lnkbtnRegist.ClientID %>").bind("click", function () {
			    ShowLoading("Making invoice ......");
			});   
			$("#<%=lnkbtnSearch.ClientID %>").bind("click", function () {
			    ShowLoading("Loading data ......");			    
			});   
    }

    function onLoadinit() {
            $('#<%=chkReceitCdAll.ClientID %>').change(function () {
                var $this = $(this);
                var checkboxes = $('div#<%=upSearch.ClientID %>').find(':checkbox');
                if ($this.is(':checked')) {
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });        
    }


    function SaveSuccess() {
        alert('Save Successful !');         
    }   
    function resetTable() {
        $("div#<%=upSearch.ClientID %> tbody").find("tr").each(function() {
            $(this).removeClass('rowSelected');
        });  
    }

    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

    function fnLoadData() 
    {
        <%=Page.GetPostBackEventReference(imgUpdateInvoice)%>;
    } 
    
    function LoadPopupDebit(printNo) {
        document.getElementById("<%=txtHfRefPrintNo.ClientID%>").value = printNo;
        document.getElementById("<%=hfRef_Seq.ClientID%>").value = printNo;
        CloseLoading();
        window.open("/Common/RdPopup/RDPopupMergeInvoiceAPT.aspx?Datum0=" + printNo, "ManageFee", "status=yes, resizable=yes, width=840, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
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
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li><b>
                <asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
            <li>
                <asp:DropDownList ID="ddlFeeType" runat="server">
                </asp:DropDownList>
            </li>
            <li><b>
                <asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px"
                    runat="server"></asp:TextBox></li>
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li><b>
                <asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox></li>
            <li><b>
                <asp:Literal ID="Literal2" runat="server" Text="Month :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
            <li>
            <li><b>
                <asp:Literal ID="Literal3" runat="server" Text="Paid date to(for Excel) :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtPaidDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPaidDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
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
    </fieldset>
    <table class="TbCel-Type6" style="margin-bottom: 0px;">
        <col width="10%" />
        <col width="10% " />
        <col width="35%" />
        <col width="15%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />

        <thead>
            <tr>
                <th>
                    <asp:CheckBox ID="chkReceitCdAll" runat="server" class="bd0"></asp:CheckBox>
                </th>                              
                <th class="Fr-line">
                    <asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltTopName" runat="server" Text="Tenant Name"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltTopFeeTy" runat="server" Text="Fee"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltPayLimitDay" runat="server" Text="Pay Day"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltTotalPay" runat="server" Text="Charge"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltTotalPaid" runat="server" Text="Paid Amount"></asp:Literal>
                </th>

            </tr>
        </thead>
    </table>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 410px; width: 840px;
        border-bottom: solid 2px rgb(182, 182, 182)">
        <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="lnkExportExcel" />
            </Triggers>
            <ContentTemplate>
                <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemCreated="lvPaymentList_ItemCreated" 
                    OnItemDataBound="lvPaymentList_ItemDataBound" >
                    <LayoutTemplate>
                        <table class="TypeA">
                            <col width="10%" />                            
                            <col width="10%" />
                            <col width="35%" />
                            <col width="15%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="cursor: pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)">
                            <td align="center">
                                <asp:CheckBox ID="chkReceitCd" runat="server" class="bd0"></asp:CheckBox>
                            </td>                            
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
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA" id="tblListDebit">
                            <tbody>
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:HiddenField ID="hfSelectedLine" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="Btwps FloatR2">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
	                <div class="Btn-Tp3-M">
		                <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click" Text="Merge Invoice"></asp:LinkButton></span>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <div class="Btwps FloatR2">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
	                <div class="Btn-Tp3-M">
		                <span><asp:LinkButton ID="lnkExportExcel" runat="server" OnClick="lnkExportExcel_Click" Text="Export Excel"></asp:LinkButton></span>
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
    <asp:HiddenField ID="txtHfRefPrintNo" runat="server" Value="" />
    <asp:ImageButton ID="imgbtnLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnLoadData_Click" />
    <asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgUpdateInvoice_Click"/>
</asp:Content>

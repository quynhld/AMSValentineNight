<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="UtilityBilling.aspx.cs" Inherits="KN.Web.Settlement.Receipt.UtilityBilling" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
        }
    }         
    function fnMovePage(intPageNo)
    {
    }
    
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            
            return false;
        }
    }

    function fnDetailView(strUserSeq)
    {
        var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
        // Datum0 : 입주자번호
        // Datum1 : 섹션코드
        // Datum2 : 관리비/주차비/임대료
        // Datum3 : 거주년
        // Datum4 : 거주월


        document.getElementById("<%=hfUserSeq.ClientID%>").value = strUserSeq;

        window.open("/Common/Popup/PopupStoreSettleDetail.aspx?Datum0=" + strUserSeq + "&Datum1=&Datum2=&Datum3=&Datum4=", "DetailStoreVeiw", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

        return false;
    }

    function fnDetailViewJs(strTxt)
    {

        var strData1 = document.getElementById("<%=ddlPayment.ClientID%>");

        if (trim(strData1.value) == "")
        {
            strData1.focus();
            alert(strTxt);
            return false;
        }

        var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
        var strChargeType = document.getElementById("<%=ddlPayment.ClientID%>").value;
        var strDates = document.getElementById("<%=txtSearchDt.ClientID%>").value.replace('-','');
        var strRequestDate = document.getElementById("<%=txtRequestDate.ClientID%>").value.replace('-','').replace('-','');   
        var isPrinted = $('#<%=rbIsDebit.ClientID %> input:radio:checked').val();
        $.initWindowMsg();
        
        var childWin;        

           childWin = window.open("/Common/RdPopup/RDPopupUltilDebitDetails.aspx?Datum0="+strRentCd+"&Datum1=" + strChargeType + "&Datum2=" + strDates + "&Datum3=" + strRequestDate+ "&Datum4=" + isPrinted, "UtilityDebit", "status=no, resizable=yes, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");         
          if (window.focus) {
              childWin.focus();
          }                   
        return false;
    }

    function fnCheckValidate(strText)
    {
        return true;
    }
        $(document).ready(function () {
            callCalendar();            
        }); 
        
        function callCalendar() {
              $("#<%=txtSearchDt.ClientID %>").monthpicker();
             $("#<%=txtRequestDate.ClientID %>").datepicker();
            
            var options = {
                rowsPerPage: 15
            };
            $('#tblListDebit').tablePagination(options);


            $("#<%=lnkbtnSearch.ClientID %>").bind("click", function () {
                ShowLoading("Loading data ......");
            }); 
        }

        $(function() {
            $.windowMsg("childMsg1", function(message) {
                var button = document.getElementById("<%=lnkbtnSearch.ClientID%>"); //document.getElementByID("Image_Print");
                button.click();
            });          
        });        
    //-->
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport"/>
        </Triggers>
        <ContentTemplate>
            <fieldset class="sh-field2 MrgB10">
                <legend>출력물</legend>
                <ul class="sf2-ag MrgL10">
                    <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
                    <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
                    <li><asp:DropDownList ID="ddlPayment" runat="server" ></asp:DropDownList></li>
                    <li><asp:Literal ID="ltFloor" runat="server" Text="Request Date:"></asp:Literal></li>
                    <li><asp:TextBox ID="txtRequestDate" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
                    <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtRequestDate.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
                    <li>
                        <asp:Literal ID="Literal3" runat="server" Text="Is Debit"></asp:Literal></li>
                    <li>
                        <asp:RadioButtonList ID="rbIsDebit" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li>
		            <div class="Btn-Type4-wp">
                        <div class="Btn-Tp4-L">
                            <div class="Btn-Tp4-R">
                                <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                            </div>
                        </div>
                    </div>
	                </li>
                    <li>
                        <div class="Btn-Type4-wp">
			                <div class="Btn-Tp4-L">
				                <div class="Btn-Tp4-R">
					                <div class="Btn-Tp4-M">
						                <span><asp:LinkButton ID="lnkbtnPrint" runat="server"   ></asp:LinkButton></span>
					                </div>
				                </div>
			                </div>
		                </div>
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPaymentList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport"/>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA" >
                <colgroup>
                        <col width="35%"/>
                        <col width="15%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                Company
                            </th>
                            <th>
                                Room
                            </th>
                            <th class="iw60">
                                Fee Type
                            </th>
                            <th style="width: 241px">
                                Period
                            </th>
                            <th>
                               Money Fee
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvPaymentList_ItemCreated" OnItemDataBound="lvPaymentList_ItemDataBound">
                <LayoutTemplate>
                    <table class="TypeA" id="tblListDebit">
                        <col width="35%"/>
                        <col width="15%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                        <col width="20%"/>
                        <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#ffffff'" title="<%#Eval("UserNm")%>" style="cursor:pointer">
                        <td class="TbTxtCenter"><asp:Literal ID="ltUserNm" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltFeeName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltAmountMoney" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA" >
                        <tbody>
                        <tr>
                            <td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width:100%"></span>
            </div>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="imgbtnMakeExcel_Click" Text="Export Excel"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfUserSeq" runat="server"/>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfRentCd" runat="server" />
     <asp:HiddenField ID="hfChargeType" runat="server" />
</asp:Content>
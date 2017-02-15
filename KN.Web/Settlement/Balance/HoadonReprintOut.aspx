<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonReprintOut.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonReprintOut"  ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
//        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//        function EndRequestHandler(sender, args) {
//            if (args.get_error() == undefined) {
//                callCalendar();
//            }
//        }          
        function fnCheckValidate(strTxt)
        {
//            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
//            var strUserTaxCd = document.getElementById("<%=txtUserTaxCd.ClientID%>").value;
//            var strRssNo = document.getElementById("<%=txtRssNo.ClientID%>").value;

//            if (trim(strSearchRoom.value) == "" && trim(strUserTaxCd) == "" && trim(strRssNo) == "")
//            {
//                strSearchRoom.focus();
//                alert(strTxt);

//                return false;
//            }

            return true;
        }

        function fnChestNutPreview(strUserSeq, strPrintSeq, strPrintDetSeq)
        {
            //document.getElementById("<%=hfDisplayUserSeq.ClientID%>") = strUserSeq;
            //document.getElementById("<%=hfDisplayPrintSeq.ClientID%>") = strPrintSeq;
            //document.getElementById("<%=hfDisplayPrintDetSeq.ClientID%>") = strPrintDetSeq;

            window.open("/Common/RdPopup/RDPopupReciptDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "KeangNamReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }

        function fnKeangNamPreview(strUserSeq, strPrintSeq, strPrintDetSeq)
        {
            //document.getElementById("<%=hfDisplayUserSeq.ClientID%>") = strUserSeq;
            //document.getElementById("<%=hfDisplayPrintSeq.ClientID%>") = strPrintSeq;
            //document.getElementById("<%=hfDisplayPrintDetSeq.ClientID%>") = strPrintDetSeq;

            window.open("/Common/RdPopup/RDPopupReciptKNDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "ChestnutReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }

        function fnIssuingCheck(strTxt)
        {
            if (confirm(strTxt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        function fnLoadData()
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        }
        $(document).ready(function () {
            callCalendar();
        });

        function callCalendar() {
            $('.calendartxt').datepicker();
        }
        //-->
    </script>

    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li><asp:Literal ID="ltSearchYear" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltSearchMonth" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtStartDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true" CssClass="calendartxt"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                <asp:HiddenField ID="hfStartDt" runat="server" />
            </li>
            <li><asp:Literal ID="ltEndDt" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtEndDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true" CssClass="calendartxt"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtEndDt.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                <asp:HiddenField ID="hfEndDt" runat="server" />
            </li>
        </ul>
    </fieldset>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:DropDownList ID="ddlItemCd" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltTxtCd" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtUserTaxCd" runat="server"></asp:TextBox></li>
            <li><asp:Literal ID="ltRssNo" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtRssNo" runat="server"></asp:TextBox></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnExcel"/>
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <col width="60px" />
                <col width="80px" />
                <col width="60px" />
                <col width="70px" />
                <col width="260px" />
                <col width="80px" />
                <col width="80px" />
                <col width="60px" />
                <col width="110px" />
                <tr>
                    <th class="Fr-line"></th>
                    <th><asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltDescription" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltNewInvoiceNo" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCount" runat="server"></asp:Literal></th>
                    <th class="Ls-line"><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
                </tr>
            </table>
            <div style="overflow-y: scroll; height: 250px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated"
                    OnItemEditing="lvPrintoutList_ItemEditing" OnItemUpdating="lvPrintoutList_ItemUpdating" OnItemDeleting="lvPrintoutList_ItemDeleting" 
                    OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="60px" />
                            <col width="80px" />
                            <col width="60px" />
                            <col width="70px" />
                            <col width="260px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="60px" />
                            <col width="90px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter">
                                <span>
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" CommandName="Update" ImageUrl="~/Common/Images/Icon/print.gif"/>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Common/Images/Icon/Trash.gif"/>
                                </span>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfPrintSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsYear" runat="server"></asp:Literal>&nbsp;/&nbsp;<asp:Literal ID="ltInsMonth" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtLeft"><asp:Literal ID="ltInsDescription" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtNewInvoiceNo" runat="server" Width="50" MaxLength="7" AutoPostBack="true" OnTextChanged="txtNewInvoiceNo_TextChanged"></asp:TextBox>
                                <asp:Literal ID="ltNewInvoiceNo" runat="server"></asp:Literal>
                                <asp:ImageButton ID="imgbtnConfirm" runat="server" CommandName="Edit" ImageUrl="~/Common/Images/Icon/check.gif"/>
                            </td>
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsAmtViNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsCount" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRegDt" runat="server"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="60px" />
                            <col width="80px" />
                            <col width="60px" />
                            <col width="70px" />
                            <col width="260px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="60px" />
                            <col width="90px" />
                            <tr><td colspan="9" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnExcel" runat="server" Text="Export Excel" OnClick="lnkbtnExcel_Click"></asp:LinkButton>
                                    </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--//<asp:HiddenField ID="hfDisplayUserSeq" runat="server"></asp:HiddenField>//-->
            <!--//<asp:HiddenField ID="hfDisplayPrintSeq" runat="server"></asp:HiddenField>//-->
            <!--//<asp:HiddenField ID="hfDisplayPrintDetSeq" runat="server"></asp:HiddenField>//-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfRentCd" runat="server" />
</asp:Content>
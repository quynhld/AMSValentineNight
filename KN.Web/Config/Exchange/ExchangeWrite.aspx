<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ExchangeWrite.aspx.cs" Inherits="KN.Web.Config.Exchange.ExchangeWrite"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--        //

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                $("#<%=txtDate.ClientID %>").datepicker({
                    altField: "#<%=hfDate.ClientID %>"
                });
            }
        }
        function fnCheckDate(strText) {
            var strDate = document.getElementById("<%=txtDate.ClientID%>");

            if (trim(strDate.value) == "") {
                alert(strText);
                document.getElementById("<%=txtBaseRate.ClientID%>").value = "";
                return false;
            }
            else {
                return true;
            }
        }

        function fnCheckValidate(strtext, strAlert) {
            if (confirm(strtext)) {
                var strDate = document.getElementById("<%=txtDate.ClientID%>");
                var strBaseRate = document.getElementById("<%=txtBaseRate.ClientID%>");
                var strBuying = document.getElementById("<%= txtBuying.ClientID %>");
                var strSelling = document.getElementById("<%= txtSelling.ClientID %>");
                var strSending = document.getElementById("<%= txtSending.ClientID %>");
                var strReceiving = document.getElementById("<%= txtReceiving.ClientID %>");

                if (trim(strDate.value) == "") {
                    alert(strAlert);
                    return false;
                }

                if (trim(strBaseRate.value) == "") {
                    alert(strAlert);
                    strBaseRate.focus();
                    return false;
                }

                if (trim(strBuying.value) == "") {
                    alert(strAlert);
                    strBuying.focus();
                    return false;
                }

                if (trim(strSelling.value) == "") {
                    alert(strAlert);
                    strSelling.focus();
                    return false;
                }

                if (trim(strSending.value) == "") {
                    alert(strAlert);
                    strSending.focus();
                    return false;
                }

                if (trim(strReceiving.value) == "") {
                    alert(strAlert);
                    strReceiving.focus();
                    return false;
                }

                return true;

            }
            else {
                return false;
            }
        }
        //-->

        $(document).ready(function () {

        });

        $(function () {
            $("#<%=txtDate.ClientID %>").datepicker({
                altField: "#<%=hfDate.ClientID %>"
            });
            // $("#txtFeeEndDt").datepicker();
        });

        function fnWooriBank() {
            window.open("http://vn.wooribank.com/wgcontrol.jsp?appid=WN005_1CNL", "WooriBank", "status=no, resizable=yes, width=620, height=630, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=top", false);
            return false;
        }
    </script>
    <style type="text/css">  
    #weather-share99{
        color :#000;
        cursor: pointer;
        background:#7fccff;
        background: -moz-linear-gradient(top, #4fbfdc 0%, #f4f4f4 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#4fbfdc),color-top(100%,#f4f4f4));
        background: -webkit-linear-gradient(top,#4fbfdc 0%,#f4f4f4 100%);
        background: -o-linear-gradient(top, #4fbfdc 0%,#f4f4f4 100%);
        background: -ms-linear-gradient(top, #4fbfdc 0%,#f4f4f4 100%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#FBFBFB', endColorstr='#F4F4F4',GradientType=0 );
        background: linear-gradient(top, #4fbfdc 0%,#f4f4f4 100%);
    }
    #weather-share99 a{ text-decoration:none; color: #000};
    </style>
    <asp:UpdatePanel ID="upExchange" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtBaseRate" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRealRate" runat="server"></asp:Literal></div>
            <table class="TbCel-Type6-A">
                <tr>
                    <th>
                        <asp:Literal ID="ltTopDate" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltTopBuying" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltTopSelling" runat="server"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltRealDate" runat="server"></asp:Literal>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:Literal ID="ltRealBuying" runat="server"></asp:Literal>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:Literal ID="ltRealSelling" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltAppliedRate" runat="server"></asp:Literal></div>
            <table class="TbCel-Type6-A">
                <tr>
                    <th rowspan="2">
                        <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb" rowspan="2">
                        <asp:Literal ID="ltBaseRate" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb" rowspan="2">
                        <asp:Literal ID="ltFluctAmt" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb" rowspan="2">
                        <asp:Literal ID="ltFluctRatio" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb" colspan="2">
                        <asp:Literal ID="ltCash" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb" colspan="2">
                        <asp:Literal ID="ltWireTrans" runat="server"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltBuying" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltSelling" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltSending" runat="server"></asp:Literal>
                    </th>
                    <th class="Bd-Lt Tb">
                        <asp:Literal ID="ltReceiving" runat="server"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td class="Tb TbTxtCenter Hg20">
                        <span>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="bgType2" Width="80px" MaxLength="8"
                                ReadOnly="true"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtDate.ClientID%>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                            <asp:HiddenField ID="hfDate" runat="server" />
                        </span>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtBaseRate" runat="server" CssClass="bgType2" Width="60px" OnTextChanged="txtBaseRate_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtFluctAmt" runat="server" CssClass="bgType2" Width="60px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtFluctRatio" runat="server" CssClass="bgType2" Width="60px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtBuying" runat="server" CssClass="bgType2" Width="60px"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtSelling" runat="server" CssClass="bgType2" Width="60px"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtSending" runat="server" CssClass="bgType2" Width="60px"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Tb TbTxtCenter">
                        <asp:TextBox ID="txtReceiving" runat="server" CssClass="bgType2" Width="60px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="Btwps FloatR2">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp FloatL">
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
            <asp:TextBox ID="txtHfExchangeDate" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfExchangeSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="Literal1" runat="server" Text="Woori Bank"></asp:Literal></div>
            <div style="width: 100%; float: right; margin-left: 100px">
                <a href="#" onclick="fnWooriBank();">
                    <img src="http://vn.wooribank.com/docs/img/nonbanking/main/tit_fxrate_en.gif" border="0" />
                </a>
            </div>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="Literal2" runat="server" Text="VCB Bank"></asp:Literal></div>
            <div >
                <div style="width: 300px">
                    <div id="weather-share99" class="weather-share99">
                        <div class="box">
                            <td colspan="2">
                                <img src="http://nhavietnam.com.vn/hinhanh/circle-chart.png" style="vertical-align: middle"
                                    border="0" title="Gia Vang" alt="Tỷ giá" /><b><a href="#">ExChange Rate</a></b>
                            </td>
                            <div id="weather-share99" class="weather-share99">
                                <script type="text/javascript" language="JavaScript" src="http://vnexpress.net/Service/Forex_Content.js"></script>
                                <script type="text/javascript" language="JavaScript" src="http://bachkhoamedia.googlecode.com/files/money.js"></script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

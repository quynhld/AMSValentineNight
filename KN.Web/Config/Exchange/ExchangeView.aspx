<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ExchangeView.aspx.cs" Inherits="KN.Web.Config.Exchange.ExchangeView"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script>
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
    <table cellpadding="0" class="TbCel-Type3-D MrgT30 Clear">
        <tr>                
            <th rowspan="2"><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
            <th rowspan="2"><asp:Literal ID="ltBaseRate" runat="server"></asp:Literal></th>
            <th rowspan="2"><asp:Literal ID="ltFluctAmt" runat="server"></asp:Literal></th>
            <th rowspan="2"><asp:Literal ID="ltFluctRatio" runat="server"></asp:Literal></th>
            <th colspan="2"><asp:Literal ID="ltCash" runat="server"></asp:Literal></th>
            <th colspan="2"><asp:Literal ID="ltWireTrans" runat="server"></asp:Literal></th>
        </tr>
        <tr>
            <th><asp:Literal ID="ltBuying" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSelling" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSending" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltReceiving" runat="server"></asp:Literal></th>
        </tr>
        <tr>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsDate" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsBaseRate" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFluctAmt" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFluctRatio" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsBuying" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsSelling" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsSending" runat="server"></asp:Literal></td>
            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsReceiving" runat="server"></asp:Literal></td>
        </tr>
    </table>
    		<div class="Btwps FloatR">
			<div class="Btn-Type3-wp ">
				<div class="Btn-Tp3-L">
					<div class="Btn-Tp3-R">
						<div class="Btn-Tp3-M">
							<span>  <asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton></span>
						</div>
					</div>
				</div>
			</div>
			<div class="Btn-Type3-wp ">
				<div class="Btn-Tp3-L">
					<div class="Btn-Tp3-R">
						<div class="Btn-Tp3-M">
							<span><asp:LinkButton ID="lnkbtnMod" runat="server" OnClick="lnkbtnMod_Click"></asp:LinkButton></span>
						</div>
					</div>
				</div>
			</div>
			<div class="Btn-Type3-wp ">
				<div class="Btn-Tp3-L">
					<div class="Btn-Tp3-R">
						<div class="Btn-Tp3-M">
							<span><asp:LinkButton ID="lnkbtnDel" runat="server" OnClick="lnkbtnDel_Click"></asp:LinkButton></span>
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
</asp:Content>
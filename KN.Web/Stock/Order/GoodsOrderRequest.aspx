<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="GoodsOrderRequest.aspx.cs" Inherits="KN.Web.Stock.Order.GoodsOrderRequest" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <table class="TbCel-Type2-D">
	    <col width="20%"/>
	    <col width="30%"/>
	    <col width="20%"/>
	    <col width="30%"/>
	    <tr>
		    <th>수신</th>
		    <td>
			    <select class="FloatL">
					    <option>옵션옵션옵션</option>
				    </select>
			    <div class="Btn-Type1-wp FloatL Mrg0 MrgL10">
				    <div class="Btn-Tp-L">
					    <div class="Btn-Tp-R">
						    <div class="Btn-Tp-M">
							    <span>검색</span>
						    </div>
					    </div>
				    </div>
			    </div>
		    </td>
		    <th class="Bd-Lt">발주처</th>
		    <td>
				    &nbsp;</td>
	    </tr>
	    <tr>
		    <th>납품기일</th>
		    <td>
			    <input type="text" name="" class="input-St"/>
			    <span class="Cal">
				    <img src="/Common/Images/Icon/calendar.gif"/>
			    </span>
		    </td>
		    <th class="Bd-Lt">참조</th>
		    <td>
			    &nbsp;</td>
	    </tr>
    </table>
    <table class="TbCel-Type5-A">
	    <tr>
		    <th>품명</th>
		    <th class="Bd-Lt">수량(EA)</th>
		    <th class="Bd-Lt">단가(원)</th>
		    <th class="Bd-Lt">금액(원)</th>
		    <th class="Bd-Lt">원</th>
	    </tr>
	    <tr >
		    <td>21321</td>
		    <td class="Bd-Lt">소형형</td>
		    <td class="Bd-Lt">소형</td>
		    <td class="Bd-Lt">소형</td>
		    <td class="Bd-Lt">소형</td>
	    </tr>
	    <tr>
		    <td colspan="5">&nbsp;</td>
	    </tr>
	    <tr >
		    <th class="bgFFF">소계</th>
		    <td colspan="3" class="Bd-Lt TbTxtRight2">100</td>
		    <td class="Bd-Lt TbTxtRight2">100</td>
	    </tr>
	    <tr >
		    <th class="bgFFF">부가세</th>
		    <td colspan="4" class="Bd-Lt TbTxtRight2">포함</td>
		    <td class="Bd-Lt">&nbsp;</td>
	    </tr>
	    <tr >
		    <th class="bgFFF">총계</th>
		    <td colspan="4" class="Bd-Lt TbTxtRight2">100</td>
		    <td class="Bd-Lt TbTxtRight2">100</td>
	    </tr>
	    <tr class="bgC1">
		    <td colspan="5" class="TbTxtRight2 bdFFF TxtBold "><span class="Txbg1">결제조건</span></td>
		    <td class="TbTxtRight2 bdFFF">현금</td>
	    </tr>
	    <tr class="bgC1">
		    <td colspan="5" class="TbTxtRight2 TxtBold"><span class="Txbg1">결제일자</span></td>
		    <td class="TbTxtRight2">2010-01-05</td>
	    </tr>
    </table>
    <!-- bottom-Fix -->
    <div class="bottom-fix">
	    <div class="Btwps FloatR">
		    <div class="Btn-Type3-wp ">
			    <div class="Btn-Tp3-L">
				    <div class="Btn-Tp3-R">
					    <div class="Btn-Tp3-M">
						    <span>등록</span>
					    </div>
				    </div>
			    </div>
		    </div>
			    <div class="Btn-Type3-wp ">
				    <div class="Btn-Tp3-L">
					    <div class="Btn-Tp3-R">
						    <div class="Btn-Tp3-M">
							    <span>취소</span>
						    </div>
					    </div>
				    </div>
			    </div>
	    </div>
    </div>
</asp:Content>
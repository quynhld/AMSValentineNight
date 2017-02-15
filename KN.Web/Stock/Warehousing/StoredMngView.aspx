<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="StoredMngView.aspx.cs" Inherits="KN.Web.Stock.Warehousing.StoredMngView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

    <script language="javascript" type="text/javascript">
    <!--        //
        function fnCheckValidate(strTxt1, strTxt2) {
            if (confirm(strTxt1)) {
                var strAmount = document.getElementById("<%=txtStoredAmount.ClientID%>");
                var strReceitDt = document.getElementById("<%=hfInsDt.ClientID%>");


                if (trim(strAmount.value) == "") {
                    strAmount.focus();
                    alert(strTxt2);

                    return false;
                }

                else if (trim(strReceitDt.value) == "") {
                    strReceitDt.focus();
                    alert(strTxt2);

                    return false;
                }

                return true;
            }
            else {
                return false;
            }
        }

        function fnPayCheckValidate(strTxt1, strTxt2) {
            if (confirm(strTxt1)) {
                var strPayAmt = document.getElementById("<%=txtPayAmt.ClientID%>");
                var strPaidDt = document.getElementById("<%=hfPaidDt.ClientID%>");


                if (trim(strPayAmt.value) == "") {
                    strAmount.focus();
                    alert(strTxt2);

                    return false;
                }

                else if (trim(strPaidDt.value) == "") {
                    strReceitDt.focus();
                    alert(strTxt2);

                    return false;
                }

                return true;
            }
            else {
                return false;
            }
        }
    //-->
    </script>
    <div class="TpAtit1">
	    <div class="FloatR">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate" runat="server" Visible="false"></asp:TextBox></div>
	</div>
    <table class="TbCel-Type2-C">    
	    <tr>
		    <th width="150"><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
		    <td>
		        <asp:Literal ID="ltIncComp" runat="server"></asp:Literal>
		        <asp:TextBox ID="txtHfWarehouseSeq" runat="server" Visible="false"></asp:TextBox>
		    </td>
	    </tr>
    </table>
    
    <asp:ListView ID="lvStoredGoodList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
        OnLayoutCreated="lvStoredGoodList_LayoutCreated" OnItemDataBound="lvStoredGoodList_ItemDataBound" OnItemCreated="lvStoredGoodList_ItemCreated">
        <LayoutTemplate>
            <table class="TbCel-Type5-B">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltStoredAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltUnit" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltLastReceitDt" runat="server"></asp:Literal></th>				
                    <th class="Bd-Lt"><asp:Literal ID="ltReceitDt" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                    <th>&nbsp;</th>
                </tr>
                <tr runat="server" id="iphItemplaceHolderId"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><asp:Literal ID="ltSeqList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltItemList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltDeptList" runat="server"></asp:Literal></td>            
                <td class="Bd-Lt"><asp:Literal ID="ltTotalAmountList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltStoredAmount" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltUnitList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltLastReceitDtList" runat="server"></asp:Literal></td>                       
                <td class="Bd-Lt"><asp:Literal ID="ltReceitDtList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltRemarkList" runat="server"></asp:Literal></td>
                <td>&nbsp;</td>
            </tr>        
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TbCel-Type6-C">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltStoredAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltUnit" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltLastReceitDt" runat="server"></asp:Literal></th>				
                    <th class="Bd-Lt"><asp:Literal ID="ltReceitDt" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                    <th>&nbsp;</th>
                </tr>
                <tr>
                    <td colspan="10" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>       
	   
    <asp:UpdatePanel ID="upGoodsInfo" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Btwps FloatR" runat="server" id="divConfirm">
                <div class="Btn-Type1-wp Mrg0 MrgL10 FloatR">
				    <div class="Btn-Tp-L">
					    <div class="Btn-Tp-R">
						    <div class="Btn-Tp-M">
							    <span><asp:LinkButton ID="lnkbtnStoredConfirm" runat="server"></asp:LinkButton></span>
						    </div>
					    </div>
				    </div>				
			    </div>
			    <div class="Btn-Type1-wp Mrg0 MrgL10 FloatR">
			        <div class="Btn-Tp-L">
					    <div class="Btn-Tp-R">
						    <div class="Btn-Tp-M">
							    <span><asp:LinkButton ID="lnkbtnPaymentConfirm" runat="server"></asp:LinkButton></span>
						    </div>
					    </div>
				    </div>
			    </div>
			</div>
            <table class="TbCel-Type2-C">
                <tr>
                    <th style="text-align:center"><asp:Literal ID="ltItem" runat="server"></asp:Literal></td>                    
                    <th style="text-align:center"><asp:Literal ID="ltInsDept" runat="server"></asp:Literal></td>                    
                    <th width="10%" style="text-align:center"><asp:Literal ID="ltInsRestAmount" runat="server"></asp:Literal></td>                    
                    <th width="10%" style="text-align:center"><asp:Literal ID="ltInsStoredAmount" runat="server"></asp:Literal></td>                    
                    <th width="20%" style="text-align:center"><asp:Literal ID="ltInsLastReceitDt" runat="server"></asp:Literal></td>                    
                    <th width="20%" style="text-align:center"><asp:Literal ID="ltIntReceitDt" runat="server"></asp:Literal></td>                    
                    <th width="30%" style="text-align:center"><asp:Literal ID="ltInsRemark" runat="server"></asp:Literal></td>                            
                    <th width="5%">&nbsp;</td>
                </tr> 
                <tr>
                    <td><asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList></td>
                    <td class="Bd-Lt" style="text-align:center"><asp:Literal ID="ltDept" runat="server"></asp:Literal></td>  
                    <td class="Bd-Lt" style="text-align:center"><asp:Literal ID="ltRestAmount" runat="server"></asp:Literal></td>       
                    <td class="Bd-Lt" style="text-align:center"><asp:TextBox ID="txtStoredAmount" runat="server" Width="50" MaxLength="10" ></asp:TextBox></td>   
                    <td class="Bd-Lt" style="text-align:center"><asp:Literal ID="ltLastReceitDt" runat="server"></asp:Literal></td>   
                    <td class="Bd-Lt" style="text-align:center" runat="server" id="divCal">
                        <asp:TextBox ID="txtReceitDt" runat="server" ReadOnly="true" Width="70"></asp:TextBox>
                        <a href="#"><img alt="Calendar" onclick="Calendar(this, '<%=txtReceitDt.ClientID%>', '<%=hfInsDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                        <asp:HiddenField ID="hfInsDt" runat="server"/>
                        <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                    </td>     
                    <td class="Bd-Lt" style="text-align:center"><asp:TextBox ID="txtRemark" runat="server" width="100"></asp:TextBox></td>
                    <td class="Bd-Lt" style="text-align:center">
                        <span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span>
                        <asp:TextBox ID="txtHfWarehouseDetSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfReceitMemNo" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfTotalAmount" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfRestQty" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfOrderSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfOrderDetSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfSvczoneCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfGroupCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfMainCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                    </td>               
                </tr>
            </table>    
        </ContentTemplate>
    </asp:UpdatePanel>
        	
    <asp:ListView ID="lvStoredPayList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
        OnLayoutCreated="lvStoredPayList_LayoutCreated" OnItemDataBound="lvStoredPayList_ItemDataBound" OnItemCreated="lvStoredPayList_ItemCreated">
        <LayoutTemplate>
            <table class="TbCel-Type5-B">
                <tr>
		            <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPayedMount" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPayDt" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></th>				
		            <th class="Bd-Lt"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
		            <th>&nbsp;</th>
	            </tr>
                <tr runat="server" id="iphItemplaceHolderId"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><asp:Literal ID="ltSeqList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltItemList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltDeptList" runat="server"></asp:Literal></td>            
                <td class="Bd-Lt"><asp:Literal ID="ltTotalPriceList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltPayedMountList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltPayDtList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltPaymentCdList" runat="server"></asp:Literal></td>                                   
                <td class="Bd-Lt"><asp:Literal ID="ltRemarkList" runat="server"></asp:Literal></td>
                <td>&nbsp;</td>
            </tr>        
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TbCel-Type6-C">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPayedMount" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPayDt" runat="server"></asp:Literal></th>
		            <th class="Bd-Lt"><asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></th>				
		            <th class="Bd-Lt"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
		            <th>&nbsp;</th>
	            </tr>
                <tr>
                    <td colspan="9" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    
            <table class="TbCel-Type2-C">
                <tr>
                    <th style="text-align:center"><asp:Literal ID="ltItemPay" runat="server"></asp:Literal></td>                    
                    <th style="text-align:center"><asp:Literal ID="ltInsDeptPay" runat="server"></asp:Literal></td>                    
                    <th width="10%" style="text-align:center"><asp:Literal ID="ltInsRestPrice" runat="server"></asp:Literal></td>                    
                    <th width="10%" style="text-align:center"><asp:Literal ID="ltInsPayAmt" runat="server"></asp:Literal></td>                    
                    <th width="20%" style="text-align:center"><asp:Literal ID="ltInsPaidDt" runat="server"></asp:Literal></td>                    
                    <th width="20%" style="text-align:center"><asp:Literal ID="ltInsPaymentCd" runat="server"></asp:Literal></td>                    
                    <th width="30%" style="text-align:center"><asp:Literal ID="ltInsRemarkPay" runat="server"></asp:Literal></td>                            
                    <th width="5%">&nbsp;</td>
                </tr>
                <tr>                 
                    <td><asp:DropDownList ID="ddlItemPay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemPay_SelectedIndexChanged"></asp:DropDownList></td>
                    <td width="15%" align="center"><asp:Literal ID="ltDeptPay" runat="server"></asp:Literal></td>            
                    <td width="5%" align="center"><asp:Literal ID="ltRestPrice" runat="server"></asp:Literal></td>
                    <td width="5%" align="center"><asp:TextBox ID="txtPayAmt" runat="server" MaxLength="21" ></asp:TextBox></td>                   
                    <td width="15%" align="center" runat="server" id="divCal1">
                        <asp:TextBox ID="txtPaidDt" runat="server" ReadOnly="true" Width="70"></asp:TextBox>
                        <a href="#"><img alt="Calendar" onclick="Calendar(this, '<%=txtPaidDt.ClientID%>', '<%=hfPaidDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                        <asp:HiddenField ID="hfPaidDt" runat="server"/>
                        <asp:TextBox ID="txtHfPaidDt" runat="server" Visible="false"></asp:TextBox>
                    </td>        
                    <td width="10%" align="center"><asp:DropDownList ID="ddlPaymentCd" runat="server" width="100"></asp:DropDownList></td>
                    <td width="10%" align="center"><asp:TextBox ID="txtRemarkPay" runat="server" width="100"></asp:TextBox></td>
                    <td width="5%" align="center">
                        <span><asp:ImageButton ID="imgbtnRegistPay" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegistPay_Click"/></span>
                        <asp:TextBox ID="txtHfWarehouseDetSeqPay" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfReceitMemNoPay" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfTotalPrice" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfRestPrice" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>  
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

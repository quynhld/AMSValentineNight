<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="GoodsOrderDetailModify.aspx.cs" Inherits="KN.Web.Stock.Order.GoodsOrderDetailModify" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
        <!--//
        function fnCheckTmpRegist(strText)
        {
            var strQty = document.getElementById("<%=txtQty.ClientID%>");
            var strGoodsInfo = document.getElementById("<%=hfGoodsInfo.ClientID%>");
            
            if (trim(strQty.value) == "")
            {
                alert(strText);
                return false;
            }
            
            if (trim(strGoodsInfo.value) == "")
            {
                alert(strText);
                return false;
            }
            
            return true;
        }

        function fnCheckValidate(strText, strDateTxt)
        {
            var strProcessDt = document.getElementById("<%=txtProcessDt.ClientID%>");

            if (trim(strProcessDt.value) == "")
            {
                alert(strText);
                return false;
            }
            
            var strNowDate = document.getElementById("<%=hfToday.ClientID%>");
            var strRequestDate = document.getElementById("<%=hfProcessDt.ClientID%>");
            var intNowDate = Number(strNowDate.value);
            var intReqDate = Number(strRequestDate.value.replace(/\-/gi,""));
            
            if (intNowDate > intReqDate)
            {
                alert(strDateTxt);
                return false;
            }

            return true;
        }
        
        function fnQtyCheckValidate(strControlID, strText1, strText2)
        {
            var strControl = document.getElementById(strControlID);
            
            if (trim(strControl.value) == "")
            {
                alert(strText1);
                strControl.focus();
                return false;
            }
            else
            {
                alert(strText2);
                return true;
            }
        }

        function fnChangePopup(strReturnValue)
        {
            <%=Page.GetPostBackEventReference(imgbtnCharge)%>
            window.open("<%=Master.PAGE_POPUP1%>?" + "<%=Master.PARAM_DATA1%>=" + strReturnValue, 'TmpExchange', 'status=no, resizable=no, width=840, height=450, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');
            return false;
        }
        
        function fnOriginData()
        {
            <%=Page.GetPostBackEventReference(imgbtnOriginData)%>
        }
        
        function fnSearchPopup(strReturnValue, strReturnQtyValue, strReturnValueInfo)
        {
            <%=Page.GetPostBackEventReference(lnkbtnSearchItem)%>
            window.open("<%=Master.PAGE_POPUP2%>?" + "<%=Master.PARAM_DATA1%>=" + strReturnValue + "&<%=Master.PARAM_DATA2%>=" + strReturnQtyValue + "&<%=Master.PARAM_DATA3%>=" + strReturnValueInfo, 'TmpExchange', 'status=no, resizable=no, width=840, height=450, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');
            return false;
        }
        
        function fnCheckData()
        {
            <%=Page.GetPostBackEventReference(imgbtnCheckSum)%>
        }
        //-->
    </script>
    <div class="FloatR2">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)</div>
    <asp:UpdatePanel ID="upBasicInfo" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlDept" EventName="SelectedIndexChanged"/>
        </Triggers>
        <ContentTemplate>
	        <table class="TbCel-Type2-D">
	            <colgroup>
	                <col width="140px" />
	                <col width="300px" />
	                <col width="140px" />
	                <col width="" />
	            </colgroup>
		        <tr>
		            <th><asp:Literal ID="ltDpt" runat="server"></asp:Literal></th>
		            <td><asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList></td>
		            <th class="Bd-Lt"><asp:Literal ID="ltProcessMemNo" runat="server"></asp:Literal></th>
		            <td><asp:DropDownList ID="ddlProcessMemNo" runat="server"></asp:DropDownList></td>
	            </tr>
	            <tr>
		            <th><asp:Literal ID="ltProcessDt" runat="server"></asp:Literal></th>
		            <td>
			            <span class="Cal">
				            <asp:TextBox ID="txtProcessDt" runat="server" ReadOnly="true" Width="80" MaxLength="10"></asp:TextBox>
				            <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtProcessDt.ClientID%>', '<%=hfProcessDt.ClientID%>', true);" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                            <asp:HiddenField ID="hfProcessDt" runat="server"/>
			            </span>
		            </td>
		            <th class="Bd-Lt"><asp:Literal ID="ltApprovalYn" runat="server"></asp:Literal></th>
		            <td><asp:CheckBox ID="chkApprovalYn" runat="server"/></td>
		        </tr>
	        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
	<asp:UpdatePanel ID="upRequestList" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnGoodsOrder" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnCheckSum" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
            <asp:ListView ID="lvRequestList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
                OnLayoutCreated="lvRequestList_LayoutCreated" OnItemDataBound="lvRequestList_ItemDataBound" OnItemCreated="lvRequestList_ItemCreated"
                OnItemDeleting="lvRequestList_ItemDeleting" OnItemUpdating="lvRequestList_ItemUpdating">
                <LayoutTemplate>
	                <table class="TbCel-Type5-C">
	                    <colgroup>
	                        <col width="220"/>
	                        <col width="200"/>
	                        <col width="100"/>
	                        <col width="120"/>
	                        <col width="120"/>
	                        <col width="60"/>
	                    </colgroup>
		                <tr>
			                <th><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
			                <th class="Bd-Lt"><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
			                <th class="Bd-Lt"><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			                <th class="Bd-Lt"><asp:Literal ID="ltSellingPrice" runat="server"></asp:Literal></th>
			                <th class="Bd-Lt"><asp:Literal ID="ltTotPrice" runat="server"></asp:Literal></th>
			                <th class="Bd-Lt"></th>
		                </tr>
		                <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
	                <tr>
		                <td class="TbTxtCenter">
		                    <asp:Literal ID="ltInsItem" runat="server"></asp:Literal>
		                    <asp:TextBox ID="txtHfItemCd" runat="server" Visible="false"></asp:TextBox>
		                    <asp:TextBox ID="txtHfTmpOrderDetSeq" runat="server" Visible="false"></asp:TextBox>
		                    <asp:TextBox ID="txtHfApprovalYn" runat="server" Visible="false"></asp:TextBox>
		                </td>
		                <td class="Bd-Lt">
		                    <asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal>
		                    <asp:TextBox ID="txtHfCompCd" runat="server" Visible="false"></asp:TextBox>
		                </td>
		                <td class="Bd-Lt">
		                    <asp:TextBox ID="txtInsQty" runat="server" Width="80" MaxLength="7"></asp:TextBox>
		                    <asp:TextBox ID="txtInsHaveQty" runat="server" Visible="false"></asp:TextBox>
		                </td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsSellingPrice" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsTotPrice" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt TbTxtCenter">
		                    <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
		                    <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
		                </td>
	                </tr>
                </ItemTemplate>
            </asp:ListView>
            <table class="TbCel-Type5-C">
                <colgroup>
                    <col width="640"/>
                    <col width="120"/>
                    <col width="60"/>
                </colgroup>
                <tr>
                    <td></td>
                    <td><asp:Literal ID="ltSumPrice" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
            </table>
	    </ContentTemplate>
    </asp:UpdatePanel>
	<asp:UpdatePanel ID="upGoodsInfo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnGoodsOrder" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnOriginData" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearchItem" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="txtQty" EventName="TextChanged"/>
	    </Triggers>
	    <ContentTemplate>
	        <table class="TbCel-Type2-E">
	            <colgroup>
	                <col width="140px" />
	                <col width="300px" />
	                <col width="140px" />
	                <col width="" />
	            </colgroup>
		         <tr>
			        <th><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
			        <td>
			            <span class="FloatL" >
			                <asp:TextBox ID="txtItemNm" runat="server" MaxLength="255" Width="180px" CssClass="input-St" ReadOnly="true"></asp:TextBox>
			                <asp:HiddenField ID="hfGoodsInfo" runat="server" />
			                <asp:TextBox ID="txtItemCd" runat="server" Visible="false"></asp:TextBox>
			                <asp:TextBox ID="txtCompNm" runat="server" Visible="false"></asp:TextBox>
			            </span>
			            <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL">
							<div class="Btn-Tp-L">
								<div class="Btn-Tp-R">
									<div class="Btn-Tp-M">
										<span><asp:LinkButton ID="lnkbtnSearchItem" runat="server" OnClick="lnkbtnSearchItem_Click"></asp:LinkButton></span>
									</div>
								</div>
							</div>
						</div>
			        </td>
		        </tr>
		        <tr>
			        <th>
			            <asp:Literal ID="ltHaveQty" runat="server"></asp:Literal>&nbsp;/&nbsp;
			            <asp:Literal ID="ltQty" runat="server"></asp:Literal>
			        </th>
			        <td>
			            <span class="FloatL" >
			                <asp:TextBox ID="txtInsHaveQty" runat="server" ReadOnly="true"></asp:TextBox>&nbsp;/
			                <asp:TextBox ID="txtQty" runat="server" MaxLength="10" Width="100px" CssClass="input-St" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
			            </span>
			            <span class="FloatL MrgL10" ><asp:Literal ID="ltInsScale" runat="server"></asp:Literal></span>
			            <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL">
							<div class="Btn-Tp-L">
								<div class="Btn-Tp-R">
									<div class="Btn-Tp-M">
										<span><asp:LinkButton ID="lnkbtnGoodsOrder" runat="server" OnClick="lnkbtnGoodsOrder_Click"></asp:LinkButton></span>
									</div>
								</div>
							</div>
						</div>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltTotPrice" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsTotPrice" runat="server"></asp:Literal><asp:Literal ID="ltTotPriceDong" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
			        <td class="P0 PL10"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" Width="500" Height="45"></asp:TextBox></td>
		        </tr>
	        </table>
            <asp:TextBox ID="txtHfGoodsOrderSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfTmpOrderSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfGoodsOrderDetSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfApprovalYn" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfHaveQty" runat="server" Visible="false"></asp:TextBox>
	    </ContentTemplate>
	</asp:UpdatePanel>
	<asp:UpdatePanel ID="upChargePanel" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnCharge" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnOriginData" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
	        <div class="Tb-Tp-tit FloatL"><asp:Literal ID="ltChargeList" runat="server"></asp:Literal></div>
	        <div class="Btn-Type1-wp FloatL MrgL10">
	            <div class="Btn-Tp-L">
		            <div class="Btn-Tp-R">
			            <div class="Btn-Tp-M">
				            <span><asp:LinkButton ID="lnkbtnTmpExchage" runat="server"></asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
            <table class="TbCel-Type2-D Clear">
                <tr>
                    <td><asp:Literal ID="ltReleaseChargeList" runat="server"></asp:Literal></td>
                </tr>
            </table>
            <asp:ImageButton ID="imgbtnCharge" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnCharge_Click"/>
            <asp:ImageButton ID="imgbtnOriginData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnOriginData_Click"/>
            <asp:ImageButton ID="imgbtnCheckSum" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnCheckSum_Click"/>
            <asp:HiddenField ID="hfTmpSeq" runat="server" />            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btwps FloatR">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                        <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                        <span><asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfToday" runat="server" />
    <asp:HiddenField ID="hfReturnValue" runat="server" />
</asp:Content>
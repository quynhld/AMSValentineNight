<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MaterialMngWrite.aspx.cs" Inherits="KN.Web.Stock.Material.MaterialMngWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strTxt, strBlankTxt)
        {
            if (confirm(strTxt))
            {
                var strChkAuto = document.getElementById("<%=chkAuto.ClientID%>");
                var strSubCd = document.getElementById("<%=txtSubCd.ClientID%>");
                var strCdNm = document.getElementById("<%=txtCdNm.ClientID%>");
                
                if (strChkAuto.checked == false)
                {
                    if (trim(strSubCd.value) == "")
                    {
                        strSubCd.focus();
                        alert(strBlankTxt);
                        
                        return false;                                            
                    }
                }
                
                if (trim(strCdNm.value) == "")
                {
                    strCdNm.focus();
                    alert(strBlankTxt);
                    
                    return false;
                }
                
                return true;
            }
            else
            {
                return false;            
            }
        }

        function fnCompSearch(strText1, strData1, strText2, strData2)
        {
            <%=Page.GetPostBackEventReference(imgbtnComp)%>
            window.open("<%=Master.PAGE_POPUP1%>?" + strText1 + "=" + strData1 + "&" + strText2 + "=" + strData2, 'SearchCompany', 'status=no, resizable=no, width=570, height=280, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
            
            return false;
        }
    //-->
    </script>
    <div class="FloatR2">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)</div>
	<asp:UpdatePanel ID="upGoodsInfo" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="ddlGrpCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="chkAuto" EventName="CheckedChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnComp" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
	        <table class="TbCel-Type2-E">
		        <tr>
			        <th><asp:Literal ID="ltRentCd" runat="server"></asp:Literal></th>
			        <td><asp:DropDownList ID="ddlRentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRentCd_SelectedIndexChanged"></asp:DropDownList></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSvcZoneCd" runat="server"></asp:Literal></th>
			        <td><asp:DropDownList ID="ddlSvcZoneCd" runat="server"></asp:DropDownList></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltGrpCd" runat="server"></asp:Literal></th>
			        <td><asp:DropDownList ID="ddlGrpCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpCd_SelectedIndexChanged"></asp:DropDownList></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltMainCd" runat="server"></asp:Literal></th>
			        <td><asp:DropDownList ID="ddlMainCd" runat="server"></asp:DropDownList></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSubCd" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtSubCd" runat="server" MaxLength="4" Width="100px" CssClass="input-St"></asp:TextBox>&nbsp;
			            <asp:CheckBox ID="chkAuto" runat="server" OnCheckedChanged="chkAuto_CheckedChanged" AutoPostBack="true"/>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltCdNm" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtCdNm" runat="server" MaxLength="255" Width="210px" CssClass="input-St"></asp:TextBox>
			            <asp:CheckBox ID="chkAutoApproval" runat="server"/>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtCompNm" runat="server" MaxLength="255" Width="210px" CssClass="input-St FloatL"></asp:TextBox>
				        <div class="Btn-Type1-wp FloatL Mrg0 MrgL10">
					        <div class="Btn-Tp-L">
						        <div class="Btn-Tp-R">
							        <div class="Btn-Tp-M">
								        <span><asp:LinkButton ID="lnkbtnCdsearch" runat="server"></asp:LinkButton></span>
							        </div>
						        </div>
					        </div>
				        </div>
				        <asp:HiddenField ID="hfCompCd" runat="server"/>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtQty" runat="server" MaxLength="10" Width="100px" CssClass="input-St"></asp:TextBox>
			            <asp:DropDownList ID="ddlScale" runat="server"></asp:DropDownList>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltPrimeCost" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtPrimeCost" runat="server" MaxLength="20" Width="210px" CssClass="input-St"></asp:TextBox>
			            <asp:Literal ID="ltPrimeDong" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSellingCost" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtSellingCost" runat="server" MaxLength="20" Width="210px" CssClass="input-St"></asp:TextBox>
			            <asp:Literal ID="ltSellingDong" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltVat" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtVatRatio" runat="server" MaxLength="5" Width="100px" CssClass="input-St"></asp:TextBox> %
			            <asp:DropDownList ID="ddlVatYn" runat="server"></asp:DropDownList>&nbsp;
			            <asp:Literal ID="ltIncludedVat" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
			        <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="5" Width="500" Height="100"></asp:TextBox></td>
		        </tr>
	        </table>
	    </ContentTemplate>
	</asp:UpdatePanel>
	<asp:ImageButton ID="imgbtnComp" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnComp_Click"/>
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
</asp:Content>
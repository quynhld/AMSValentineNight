<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MaterialClassificationList.aspx.cs" Inherits="KN.Web.Stock.Material.MaterialClassificationList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--    //
    function fnRegCheck(strEmpty, strDuplicate, strConfirmTxt)
    {
        if (confirm(strConfirmTxt))
        {
            var strTxtCodeGrp = document.getElementById("<%=txtCodeGrpCd.ClientID%>");
            var strHfCodeGrp = document.getElementById("<%=hfCodeGrpCd.ClientID%>");
            var strTxtCodeMain = document.getElementById("<%=txtCodeMainCd.ClientID%>");
            var strHfCodeMain = document.getElementById("<%=hfCodeMainCd.ClientID%>");
            var strTxtCodeNmKr = document.getElementById("<%=txtCodeNmKr.ClientID%>");
            var strHfCodeNmKr = document.getElementById("<%=hfCodeNmKr.ClientID%>");
            var strTxtCodeNmVi = document.getElementById("<%=txtCodeNmVi.ClientID%>");
            var strHfCodeNmVi = document.getElementById("<%=hfCodeNmVi.ClientID%>");
            var strTxtCodeNmEn = document.getElementById("<%=txtCodeNmEn.ClientID%>");
            var strHfCodeNmEn = document.getElementById("<%=hfCodeNmEn.ClientID%>");

            if (trim(strTxtCodeGrp.value) == "")
            {
                alert(strEmpty);
                strTxtCodeGrp.focus();
                return false;
            }

            if (trim(strTxtCodeMain.value) == "")
            {
                alert(strEmpty);
                strTxtCodeMain.focus();
                return false;
            }

            if (trim(strTxtCodeNmEn.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmEn.focus();
                return false;
            }

            if (trim(strTxtCodeNmVi.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmVi.focus();
                return false;
            }

            if (trim(strTxtCodeGrp.value) == trim(strHfCodeGrp.value) &&
            trim(strTxtCodeMain.value) == trim(strHfCodeMain.value) &&
            trim(strTxtCodeNmKr.value) == trim(strHfCodeNmKr.value) &&
            trim(strTxtCodeNmVi.value) == trim(strHfCodeNmVi.value) &&
            trim(strTxtCodeNmEn.value) == trim(strHfCodeNmEn.value))
            {
                alert(strDuplicate);
                return false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    function fnModCheck(strEmpty, strDuplicate, strConfirmTxt)
    {
        if (confirm(strConfirmTxt))
        {
            var strTxtCodeGrp = document.getElementById("<%=txtCodeGrpCd.ClientID%>");
            var strHfCodeGrp = document.getElementById("<%=hfCodeGrpCd.ClientID%>");
            var strTxtCodeMain = document.getElementById("<%=txtCodeMainCd.ClientID%>");
            var strHfCodeMain = document.getElementById("<%=hfCodeMainCd.ClientID%>");
            var strTxtCodeNmKr = document.getElementById("<%=txtCodeNmKr.ClientID%>");
            var strHfCodeNmKr = document.getElementById("<%=hfCodeNmKr.ClientID%>");
            var strTxtCodeNmVi = document.getElementById("<%=txtCodeNmVi.ClientID%>");
            var strHfCodeNmVi = document.getElementById("<%=hfCodeNmVi.ClientID%>");
            var strTxtCodeNmEn = document.getElementById("<%=txtCodeNmEn.ClientID%>");
            var strHfCodeNmEn = document.getElementById("<%=hfCodeNmEn.ClientID%>");

            if (trim(strTxtCodeGrp.value) == "")
            {
                alert(strEmpty);
                strTxtCodeGrp.focus();
                return false;
            }

            if (trim(strTxtCodeMain.value) == "")
            {
                alert(strEmpty);
                strTxtCodeMain.focus();
                return false;
            }

            if (trim(strTxtCodeNmEn.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmEn.focus();
                return false;
            }

            if (trim(strTxtCodeNmVi.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmVi.focus();
                return false;
            }

            if (trim(strTxtCodeGrp.value) == trim(strHfCodeGrp.value) &&
            trim(strTxtCodeMain.value) == trim(strHfCodeMain.value) &&
            trim(strTxtCodeNmKr.value) == trim(strHfCodeNmKr.value) &&
            trim(strTxtCodeNmVi.value) == trim(strHfCodeNmVi.value) &&
            trim(strTxtCodeNmEn.value) == trim(strHfCodeNmEn.value))
            {
                alert(strDuplicate);
                return false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    function fnDelCheck(strEmpty, strConfirmTxt)
    {
        if (confirm(strConfirmTxt))
        {
            var strTxtCodeGrp = document.getElementById("<%=txtCodeGrpCd.ClientID%>");
            var strHfCodeGrp = document.getElementById("<%=hfCodeGrpCd.ClientID%>");
            var strTxtCodeMain = document.getElementById("<%=txtCodeMainCd.ClientID%>");
            var strHfCodeMain = document.getElementById("<%=hfCodeMainCd.ClientID%>");
            var strTxtCodeNmKr = document.getElementById("<%=txtCodeNmKr.ClientID%>");
            var strHfCodeNmKr = document.getElementById("<%=hfCodeNmKr.ClientID%>");
            var strTxtCodeNmVi = document.getElementById("<%=txtCodeNmVi.ClientID%>");
            var strHfCodeNmVi = document.getElementById("<%=hfCodeNmVi.ClientID%>");
            var strTxtCodeNmEn = document.getElementById("<%=txtCodeNmEn.ClientID%>");
            var strHfCodeNmEn = document.getElementById("<%=hfCodeNmEn.ClientID%>");

            if (trim(strTxtCodeGrp.value) == "")
            {
                alert(strEmpty);
                strTxtCodeGrp.focus();
                return false;
            }

            if (trim(strTxtCodeMain.value) == "")
            {
                alert(strEmpty);
                strTxtCodeMain.focus();
                return false;
            }

            if (trim(strTxtCodeNmEn.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmEn.focus();
                return false;
            }

            if (trim(strTxtCodeNmVi.value) == "")
            {
                alert(strEmpty);
                strTxtCodeNmVi.focus();
                return false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
//-->
</script>
 
	<div class="sType1">
		<dl class="dp1">
			<dt><asp:Literal ID="ltGrpCdClassi" runat="server"></asp:Literal></dt>
			<dd class="setw">
	            <asp:UpdatePanel ID="upGrpPanel" runat="server" UpdateMode="Conditional">
	                <Triggers>
	                    <asp:AsyncPostBackTrigger ControlID="lbGrpClass" EventName="SelectedIndexChanged"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnIns" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnMod" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnDel" EventName="Click"/>
	                </Triggers>
	                <ContentTemplate>
			            <asp:ListBox ID="lbGrpClass" runat="server" AutoPostBack="true" Rows="15" Width="240px" Height="300px" OnSelectedIndexChanged="lbGrpClass_SelectedIndexChanged"></asp:ListBox>
	                </ContentTemplate>
	            </asp:UpdatePanel>
			</dd>
		</dl>
        <dl class="dp1">
            <dt><asp:Literal ID="ltMainCdClassi" runat="server"></asp:Literal></dt>
            <dd class="setw">
	            <asp:UpdatePanel ID="upMainPanel" runat="server" UpdateMode="Conditional">
	                <Triggers>
	                    <asp:AsyncPostBackTrigger ControlID="lbGrpClass" EventName="SelectedIndexChanged"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnIns" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnMod" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnDel" EventName="Click"/>
	                </Triggers>
	                <ContentTemplate>
	                    <asp:ListBox ID="lbMainClass" runat="server" AutoPostBack="true" Rows="15" Width="240px" Height="300px" OnSelectedIndexChanged="lbMainClass_SelectedIndexChanged"></asp:ListBox>
	                </ContentTemplate>
	            </asp:UpdatePanel>
            </dd>
         </dl>
		<dl class="dp2">
			<dt><asp:Literal ID="ltAuth" runat="server"></asp:Literal></dt>
			<dd class="setw">
				<div>
			    <asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
			        <Triggers>
			            <asp:AsyncPostBackTrigger ControlID="lbGrpClass" EventName="SelectedIndexChanged"/>
			            <asp:AsyncPostBackTrigger ControlID="lbMainClass" EventName="SelectedIndexChanged"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnIns" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnMod" EventName="Click"/>
			            <asp:AsyncPostBackTrigger ControlID="lnkbtnDel" EventName="Click"/>
			        </Triggers>
			        <ContentTemplate>
					    <dl class="InData">
						    <dt><asp:Literal ID="ltCodeGrpCd" runat="server"></asp:Literal></dt>
						    <dd>
						        <asp:TextBox ID="txtCodeGrpCd" CssClass="iw130" runat="server" MaxLength="4"></asp:TextBox>
						        <asp:HiddenField ID="hfCodeGrpCd" runat="server"/>
						    </dd>
					    </dl>
					    <dl class="InData">
						    <dt><asp:Literal ID="ltCodeMainCd" runat="server"></asp:Literal></dt>
						    <dd>
						        <asp:TextBox ID="txtCodeMainCd" CssClass="iw130" runat="server" MaxLength="4"></asp:TextBox>
						        <asp:HiddenField ID="hfCodeMainCd" runat="server"/>
						    </dd>
					    </dl>
					    <dl class="InData">
						    <dt><asp:Literal ID="ltCodeNmVi" runat="server"></asp:Literal></dt>
						    <dd>
						        <asp:TextBox ID="txtCodeNmVi" CssClass="iw130" runat="server"></asp:TextBox>
						        <asp:HiddenField ID="hfCodeNmVi" runat="server"/>
						    </dd>
					    </dl>
					    <dl class="InData">
						    <dt><asp:Literal ID="ltCodeNmEn" runat="server"></asp:Literal></dt>
						    <dd>
						        <asp:TextBox ID="txtCodeNmEn" CssClass="iw130" runat="server"></asp:TextBox>
						        <asp:HiddenField ID="hfCodeNmEn" runat="server"/>
						    </dd>
					    </dl>
					    <dl class="InData">
						    <dt><asp:Literal ID="ltCodeNmKr" runat="server"></asp:Literal></dt>
						    <dd>
						        <asp:TextBox ID="txtCodeNmKr" CssClass="iw130" runat="server"></asp:TextBox>
						        <asp:HiddenField ID="hfCodeNmKr" runat="server"/>
						    </dd>
					    </dl>
			        </ContentTemplate>
			    </asp:UpdatePanel>
				</div>
			</dd>
			<dd class="sBtn">
				<div class="codeBtn">
					<div class="Txt-Ftype1-wp FloatL">
						<div class="Txt-Ftype1-L">
							<div class="Txt-Ftype1-R">
								<div class="Txt-Ftype1-M">
									<span>
									    <asp:Image ID="imgIns" runat="server" ImageUrl="~/Common/Images/Icon/Code-Wt.gif" ImageAlign="AbsMiddle"/>
									    <asp:LinkButton ID="lnkbtnIns" runat="server" OnClick="lnkbtnIns_Click"></asp:LinkButton>
									</span>
								</div>
							</div>
						</div>
					</div>
				</div>
			</dd>
			<dd class="sBtn">
				<div class="codeBtn">
					<div class="Txt-Ftype1-wp FloatL">
						<div class="Txt-Ftype1-L">
							<div class="Txt-Ftype1-R">
								<div class="Txt-Ftype1-M">
									<span>
									    <asp:Image ID="imgMod" runat="server" ImageUrl="~/Common/Images/Icon/Code-Wt.gif" ImageAlign="AbsMiddle"/>
									    <asp:LinkButton ID="lnkbtnMod" runat="server" OnClick="lnkbtnMod_Click"></asp:LinkButton>
									</span>
								</div>
							</div>
						</div>
					</div>
				</div>
			</dd>
			<dd class="sBtn">
				<div class="codeBtn">
					<div class="Txt-Ftype1-wp FloatL">
						<div class="Txt-Ftype1-L">
							<div class="Txt-Ftype1-R">
								<div class="Txt-Ftype1-M">
									<span>
									    <asp:Image ID="imgDel" runat="server" ImageUrl="~/Common/Images/Icon/Code-tc.gif" ImageAlign="AbsMiddle"/>
									    <asp:LinkButton ID="lnkbtnDel" runat="server" OnClick="lnkbtnDel_Click"></asp:LinkButton>
									</span>
								</div>
							</div>
						</div>
					</div>
				</div>
			</dd>
		</dl>
	</div>
	<asp:UpdatePanel ID="upTxt" runat="server">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnIns" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnMod" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnDel" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
            <asp:HiddenField ID="hfAlertTxt" runat="server"/>
	    </ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
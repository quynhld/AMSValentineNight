<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardModify.aspx.cs" Inherits="KN.Web.Board.Board.BoardModify"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnValidateData(strAlertTitle, strAlertContext)
    {
        var strTitle = document.getElementById("<%=txtTitle.ClientID%>");

        if (trim(strTitle.value) == "")
        {
            strTitle.focus();
            alert(strAlertTitle);
            return false;
        }

        var strContent = document.getElementById("<%=txtContext.ClientID%>");

        if (trim(strContent.value) == "")
        {
            strContent.focus();
            alert(strAlertContext);
            return false;
        }

        return true;
    }
</script>
<div class="Tb-Tp-tit">&bull;Board / Download</div>
<div class="Tbtop-tit"><asp:Literal ID="ltModify" runat="server"></asp:Literal></div>
<asp:UpdatePanel ID="upAuthPanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chkAuth" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="TbCel-Type2-E">
            <col width="147px"/>
            <col width="503px"/>
            <tbody>
                <tr>
                    <th rowspan="2" valign="top"><asp:Literal ID="ltAuthority" runat="server"></asp:Literal></th>
                    <td class="bwe1">
                        <asp:DropDownList ID="ddlAuth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuth_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="bwe1">
                        <asp:CheckBoxList ID="chkAuth" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="4" OnSelectedIndexChanged="chkAuth_SelectedIndexChanged"></asp:CheckBoxList>
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<table cellspacing="0" class="TbCel-Type1">
    <col width="147px"/>
    <col width="503px"/>
    <tbody>
        <tr>
            <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
            <td><asp:TextBox ID="txtTitle" runat="server" Width="450px" MaxLength="255" CssClass="bgType2"></asp:TextBox></td>
        </tr>
        <tr>
            <th valign="top"><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
            <td><asp:TextBox runat="server" ID="txtContext" Rows="5" Columns="60" TextMode="MultiLine" CssClass="bgType2"></asp:TextBox></td>
        </tr>
        <tr runat="server" id="trFileAddon1">
            <th><asp:Literal ID="ltFileAddon1" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon1" runat="server" Width="450px"/><br />
                <asp:Literal ID="ltFileNm1" runat="server"></asp:Literal>
                <asp:CheckBox ID="chkFileAddon1" runat="server"/> 
            </td>
        </tr>
        <tr runat="server" id="trFileAddon2">
            <th><asp:Literal ID="ltFileAddon2" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon2" runat="server" Width="450px"/><br />
                <asp:Literal ID="ltFileNm2" runat="server"></asp:Literal>
                <asp:CheckBox ID="chkFileAddon2" runat="server"/> 
            </td>
        </tr>
        <tr runat="server" id="trFileAddon3">
            <th><asp:Literal ID="ltFileAddon3" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon3" runat="server" Width="450px"/><br />
                <asp:Literal ID="ltFileNm3" runat="server"></asp:Literal>
                <asp:CheckBox ID="chkFileAddon3" runat="server"/> 
            </td>
        </tr>
    </tbody>
</table>
<div class="Btwps FloatR">
    <div class="Btn-Type3-wp ">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				 <span> <asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
	<div class="Btn-Type3-wp ">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span> <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp ">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					 <span> <asp:LinkButton ID="lnkbtnList" runat="server"></asp:LinkButton>  </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:UpdatePanel ID="upBtnPanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chkAuth" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
        <asp:TextBox ID="txtHfAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfTotalGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfBoardTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfBoardCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfBoardSeq" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFilePath1" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFilePath2" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFilePath3" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFileCnt" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtReplyYn" runat="server" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
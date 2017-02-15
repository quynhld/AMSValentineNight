<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemberMngWrite.aspx.cs" Inherits="KN.Web.Config.Authority.MemberMngWrite"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnOpenFindId()
    {
        var strParams1 = document.getElementById("<%=hfParamData.ClientID%>").value;
        var strParams2 = document.getElementById("<%=ddlAuth.ClientID%>").value;

        if (strParams2 == "11111111")
        {
            window.open("/Common/Popup/PopupChestNutMemberList.aspx?Params1=" + strParams1, "MemberList", "status=no, resizable=no, width=623, height=400, left=100, top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }
        else if (strParams2 == "11111112")
        {
            window.open("/Common/Popup/PopupKeangNamMemberList.aspx?Params1=" + strParams1, "MemberList", "status=no, resizable=no, width=623, height=400, left=100, top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }
        
        return false;
    }

    function fnHoldItem()
    {
        <%=Page.GetPostBackEventReference(imgbtnHoldItem)%>;
    }
    
    function fnAlertTempID(strText1, strText2, strText3)
    {
        if (confirm(strText1 + "\n" + strText2 + "\n" + strText3))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    function fnValidateData(strAlertText, strAlertDiff)
    {
        var strUserID = document.getElementById("<%=txtUserID.ClientID%>");
        var strName = document.getElementById("<%=txtName.ClientID%>");
        var strPWD = document.getElementById("<%=txtPwd.ClientID%>");
        var strPWDConfirm = document.getElementById("<%=txtPwdConfirm.ClientID%>");
        var strTelTy = document.getElementById("<%=txtTelTy.ClientID%>");
        var strTelFrontNo = document.getElementById("<%=txtTelFrontNo.ClientID%>");
        var strTelRearNo = document.getElementById("<%=txtTelRearNo.ClientID%>");
        var strMobileTy = document.getElementById("<%=txtMobileTy.ClientID%>");
        var strMobileFrontNo = document.getElementById("<%=txtMobileFrontNo.ClientID%>");
        var strMobileRearNo = document.getElementById("<%=txtMobileRearNo.ClientID%>");
        var strEmailID = document.getElementById("<%=txtEmailID.ClientID%>");
        var strEmailServer = document.getElementById("<%=txtEmailServer.ClientID%>");

        if (trim(strUserID.value) == "")
        {
            strUserID.focus();
            alert(strAlertText);
            return false;
        }

        if (trim(strName.value) == "")
        {
            strName.focus();
            alert(strAlertText);
            return false;
        }

        if (trim(strPWD.value) == "" || trim(strPWDConfirm.value) == "")
        {
            strPWD.focus();
            alert(strAlertText);
            return false;
        }

        if (trim(strPWD.value) != trim(strPWDConfirm.value))
        {
            strPWDConfirm.focus();
            alert(strAlertDiff);
            return false;
        }

        if (trim(strTelTy.value) == "" || trim(strTelFrontNo.value) == "" || trim(strTelRearNo.value) == "")
        {
            strTelTy.focus();
            alert(strAlertText);
            return false;
        }

        if (trim(strMobileTy.value) == "" || trim(strMobileFrontNo.value) == "" || trim(strMobileRearNo.value) == "")
        {
            strMobileTy.focus();
            alert(strAlertText);
            return false;
        }

        if (trim(strEmailID.value) == "" || trim(strEmailServer.value) == "")
        {
            strEmailID.focus();
            alert(strAlertText);
            return false;
        }

        return true;
    }
</script>
<asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnList" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnTemporary" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnHoldItem" EventName="Click"/>        
        <asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlAuth" EventName="SelectedIndexChanged"/>        
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="TbCel-Type2-F">
            <colgroup>
                <col width="125px" />
                <col width="200px" />
                <col width="125px" />
                <col width="200px" />
                <tr>
                    <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAuth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuth_SelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox ID="txtHfAuth" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" Width="180px"></asp:TextBox>
                        <asp:HiddenField ID="hfMemEngNm" runat="server" />
                        <asp:HiddenField ID="hfMemNo" runat="server" />
                        &nbsp;
                        <asp:HiddenField ID="hfKNNo" runat="server" />
                    </td>
                    <td colspan="2">
                        <div class="Btwps FloatL">
                            <div class="Btn-Type3-wp">
                                <div class="Btn-Tp-L">
                                    <div class="Btn-Tp-R">
                                        <div class="Btn-Tp-M">
                                            <span>
                                                <asp:LinkButton ID="lnkbtnMemInfo" runat="server" OnClientClick="javascript:return fnOpenFindId();"></asp:LinkButton>
                                                <asp:HiddenField ID="hfParamData" runat="server" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="Btn-Tp-L">
                                    <div class="Btn-Tp-R">
                                        <div class="Btn-Tp-M">
                                            <span>
                                                <asp:LinkButton ID="lnkbtnTemporary" runat="server" onclick="lnkbtnTemporary_Click"></asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltUserID" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server" AutoPostBack="true" MaxLength="9" OnTextChanged="txtUserID_TextChanged" Width="180px"></asp:TextBox>
                        <asp:HiddenField ID="hfUserID" runat="server" />
                    </td>
                    <th><asp:Literal ID="ltEnterDt" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtEnterDt" runat="server" ReadOnly="true" Width="70" MaxLength="10"></asp:TextBox>
                        <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEnterDt.ClientID%>', '<%=hfEnterDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                        <asp:HiddenField ID="hfEnterDt" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltAuthGrp" runat="server"></asp:Literal></th>
                    <td>
                        <asp:DropDownList ID="ddlAuthGrp" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtHfAuthGrp" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <th class="lebd"><asp:Literal ID="ltFMS" runat="server"></asp:Literal></th>
                    <td>
                        <asp:DropDownList ID="ddlFMSAuthYn" runat="server">
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltPwd" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" MaxLength="20" TextMode="Password" Width="180px"></asp:TextBox>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltPwdConfirm" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtPwdConfirm" runat="server" MaxLength="20" TextMode="Password" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTelNo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtTelTy" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfTelFrontNo" runat="server" />
                        &nbsp;)
                        <asp:TextBox ID="txtTelFrontNo" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfTelMidNo" runat="server" />
                        &nbsp;-
                        <asp:TextBox ID="txtTelRearNo" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfTelRearNo" runat="server" />
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltMobileNo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtMobileTy" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfCellFrontNo" runat="server" />
                        &nbsp;)
                        <asp:TextBox ID="txtMobileFrontNo" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfCellMidNo" runat="server" />
                        &nbsp;-
                        <asp:TextBox ID="txtMobileRearNo" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:HiddenField ID="hfCellRearNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmailID" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                        <asp:HiddenField ID="hfEmailID" runat="server" />
                        &nbsp;@
                        <asp:TextBox ID="txtEmailServer" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                        <asp:HiddenField ID="hfEmailServer" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th rowspan="2" valign="top">
                        <asp:Literal ID="ltAddr" runat="server"></asp:Literal>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtAddr" runat="server" MaxLength="500" Width="500px"></asp:TextBox>
                        <asp:HiddenField ID="hfAddr" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDetAddr" runat="server" MaxLength="500" Width="500px"></asp:TextBox>
                        <asp:HiddenField ID="hfDetAddr" runat="server" />
                    </td>
                </tr>
                <!--//<tr ID="trFileAddon" runat="server">
                    <th><asp:Literal ID="ltFileAddon" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:FileUpload ID="fileAddon" runat="server" Width="450px" /></td>
                </tr>
                <tr>
                    <th valign="top"><asp:Literal ID="ltAuthority" runat="server"></asp:Literal></th>
                    <td colspan="3">
                        <asp:UpdatePanel ID="upAuthPanel" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkAuth" EventName="SelectedIndexChanged"/>
                            </Triggers>
                            <ContentTemplate>
                                <asp:CheckBoxList ID="chkAuth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkAuth_SelectedIndexChanged" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table"></asp:CheckBoxList>
                                <asp:TextBox ID="txtHfAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfTotalGrpTy" runat="server" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>//-->
            </colgroup>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="Btwps FloatR">
    <div class="Btn-Type3-wp ">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				 <span><asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
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
					 <span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:ImageButton ID="imgbtnHoldItem" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnHoldItem_Click"/>
</asp:Content>
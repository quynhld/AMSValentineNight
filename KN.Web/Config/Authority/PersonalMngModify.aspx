<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="PersonalMngModify.aspx.cs" Inherits="KN.Web.Config.Authority.PersonalMngModify" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnValidateData(strAlertText, strAlertDiff)
    {
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
<div class="Tbtop-tit">
    <span class="bt2"><asp:Literal ID="ltWrite" runat="server"></asp:Literal></span>
</div>
<table cellspacing="0" class="TbCel-Type2-A">
    <col width="125px"/>
    <col width="200px"/>
    <col width="125px"/>
    <col width="200px"/>
    <tr>
        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
        <td colspan="3">
            <asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal>
            <asp:TextBox ID="txtHfCompNo" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltUserID" runat="server"></asp:Literal></th>
        <td>
            <asp:Label ID="lblUserID" runat="server"></asp:Label>&nbsp;
            <asp:HiddenField ID="hfKNNo" runat="server"/>
            <asp:TextBox ID="txtHfMemNo" runat="server" Visible="false"></asp:TextBox>
        </td>
        <th class="lebd"><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
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
            <asp:TextBox ID="txtHfFMSAuthYn" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltEnterDt" runat="server"></asp:Literal></th>
        <td>
            <asp:Label ID="lblEnterDt" runat="server"></asp:Label>
            <asp:HiddenField ID="hfEnterDt" runat="server" />
        </td>
        <th class="lebd"><asp:Literal ID="ltRetireDt" runat="server"></asp:Literal></th>
        <td>
            <asp:Label ID="lblRetireDt" runat="server"></asp:Label>
            <asp:HiddenField ID="hfRetireDt" runat="server" />
        </td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltPwd" runat="server"></asp:Literal></th>
        <td><asp:TextBox ID="txtPwd" runat="server" Width="180px" MaxLength="20" TextMode="Password"></asp:TextBox></td>
        <th class="lebd"><asp:Literal ID="ltPwdConfirm" runat="server"></asp:Literal></th>
        <td><asp:TextBox ID="txtPwdConfirm" runat="server" Width="180px" MaxLength="20" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></th>
        <td>
            <asp:TextBox ID="txtTelTy" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;)
            <asp:TextBox ID="txtTelFrontNo" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;-
            <asp:TextBox ID="txtTelRearNo" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
            <asp:TextBox ID="txtHfTelTy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfTelFrontNo" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfTelRearNo" runat="server" Visible="false"></asp:TextBox>
        </td>
        <th class="lebd"><asp:Literal ID="ltMobileNo" runat="server"></asp:Literal></th>
        <td>
            <asp:TextBox ID="txtMobileTy" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;)
            <asp:TextBox ID="txtMobileFrontNo" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;-
            <asp:TextBox ID="txtMobileRearNo" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
            <asp:TextBox ID="txtHfMobileTy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfMobileFrontNo" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfMobileRearNo" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltEmail" runat="server"></asp:Literal></th>
        <td colspan="3">
            <asp:TextBox ID="txtEmailID" runat="server" Width="180px" MaxLength="50"></asp:TextBox>&nbsp;@
            <asp:TextBox ID="txtEmailServer" runat="server" Width="180px" MaxLength="50"></asp:TextBox>
            <asp:TextBox ID="txtHfEmailID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfEmailServer" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th rowspan="2" valign="top"><asp:Literal ID="ltAddr" runat="server"></asp:Literal></th>
        <td colspan="3">
            <asp:TextBox ID="txtAddr" runat="server" Width="500px" MaxLength="500"></asp:TextBox>
            <asp:TextBox ID="txtHfAddr" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtDetAddr" runat="server" Width="500px" MaxLength="500"></asp:TextBox>
            <asp:TextBox ID="txtHfDetAddr" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="Btwps FloatR2">
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
				    <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
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
<asp:TextBox ID="txtHfMemSeq" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
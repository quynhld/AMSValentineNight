<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="KN.Web.Index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Style-Type" content="text/css"/>
    <meta http-equiv="Content-Script-Type" content="text/javascript"/>
    <meta http-equiv="ImageToolBar" content="no"/>
    <meta name="Keywords" content="사이트내용"/>
    <meta name="Description" content="사이트소개"/>
    <meta name="Copyright" content="저작권정보"/>
    <meta name="Author" content="Shim"/>
    <meta name="Date" content="2010.08"/>
    <script type="text/javascript" src="/Common/Javascript/jquery-1.8.3.min.js"></script>
    <link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
</head>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--//----dfgkdjfgdfgdlfg
$(function(){
	// body 스크롤 없애기
	var html_dom = document.getElementsByTagName("html")[0]; 
	var overflow = ""; 
	
	if(html_dom.style.overflow == "")
	{
	    overflow = "hidden"; 
	}
	else
	{
	    overflow = ""; 
	}
		    
	html_dom.style.overflow = overflow;
});
function fnLoginCheck()
{
    var strLoginId  = document.getElementById("<%=txtID.ClientID%>").value;
    var strLoginPw  = document.getElementById("<%=txtPWD.ClientID%>").value;
    
    document.getElementById("<%=txtID.ClientID%>").value    = trim(strLoginId);
    document.getElementById("<%=txtPWD.ClientID%>").value   = trim(strLoginPw);
    
    if (trim(strLoginId) == "")
    {
        document.getElementById("<%=txtID.ClientID%>").value = "";
        alert("Please enter your ID.");
        return false;
    }

    if (trim(strLoginPw) == "")
    {
        document.getElementById("<%=txtPWD.ClientID%>").value = "";
        alert("Please enter your Password.");
                
        return false;
    }
    
    return true;
}

// 공통성이 짙은 함수이나 페이지마다 알맞게 변형해서 사용할 것.
function fnKeyEnter() 
{  	 
	if(event.keyCode == 13)   
	{
		event.keyCode = "";
		
		if(fnLoginCheck())
		{
            document.getElementById("<%=imgbtnEnter.ClientID%>").click();
		}
		else
		{
		    return false;
	    }
	    
	    return true;
	}
}
//-->
</script>
<body id="mainw">
    <form id="frmlogin" runat="server">
		<div id="idw">
			<div class="Cont">
				<h1><img src="/Common/Images/Common/Index-kn-logo.png" alt="Keangnam"/></h1>
				<h2><img src="/Common/Images/Common/Index-kn-Title.png" alt="Hanoi Landmark Tower"/></h2>
				<div class="Lbox">
					<ul class="Lgbt">
						<li>
							<span class="ty2"><img src="/Common/Images/Common/Index-kn-Select.png" alt="Language"/></span>
							<asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="true" CssClass="lge-st" onselectedindexchanged="ddlLang_SelectedIndexChanged"></asp:DropDownList>
						</li>
						<li>
						    <span class="ty2"><img src="/Common/Images/Common/Index-kn-Company.png" alt="Language"/></span>
						    <asp:DropDownList ID="ddlComp" runat="server">
						        <asp:ListItem Text="ChestNut" Value="11111111" Selected="True"></asp:ListItem>
						        <asp:ListItem Text="KeangNam" Value="11111112"></asp:ListItem>
						    </asp:DropDownList>
						</li>
						<li>
							<span class="ty1"><img src="/Common/Images/Common/Index-kn-Id.png" alt="ID"/></span>
							<asp:TextBox ID="txtID" runat="server" onKeyPress="javascript:fnKeyEnter();" Width="140"></asp:TextBox>
						</li>
						<li>
							<span class="ty1"><img src="/Common/Images/Common/Index-kn-PW.png" alt="PW"/></span>
							<asp:TextBox ID="txtPWD" runat="server" TextMode="Password" onKeyPress="javascript:fnKeyEnter();" Width="140"></asp:TextBox>
						</li>
					</ul>
					<div class="Rgbt">
					    <asp:ImageButton ID="imgbtnEnter" runat="server" AlternateText="Enter" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/Index-kn-login.png" OnClick="imgbtnEnter_Click"  OnClientClick="javascript:return fnLoginCheck();"/>
					</div>
				</div><!-- //Lbox -->
				<div class="Rchk">
				    <asp:CheckBox ID="chkRememberID" runat="server"/>
				</div>
				<div class="find">
					<span><asp:ImageButton ID="imgbtnFindID" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/Index-kn-FindId.png" onclick="imgbtnFindID_Click"/></span>
					<span><asp:ImageButton ID="imgbtnFindPW" runat="server" AlternateText="find pw" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/Index-kn-FindPW.png" onclick="imgbtnFindPW_Click"/></span>
				</div>
			</div>
		</div>
    </form>
</body>
</html>
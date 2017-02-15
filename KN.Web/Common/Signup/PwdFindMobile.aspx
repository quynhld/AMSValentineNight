<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PwdFindMobile.aspx.cs" Inherits="KN.Web.Common.Signup.PwdFindMobile" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="ko">
<head>
<title>제목</title>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="Content-Style-Type" content="text/css"/>
<meta http-equiv="Content-Script-Type" content="text/javascript"/>
<meta http-equiv="ImageToolBar" content="no"/>
<meta name="Keywords" content="사이트내용"/>
<meta name="Description" content="사이트소개"/>
<meta name="Copyright" content="저작권정보"/>
<meta name="Author" content="Shim"/>
<meta name="Date" content="2010.08"/>
<script type="text/javascript" src="/Common/Javascript/jquery-1.4.2.min.js"></script>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script type="text/javascript">
	$(function(){
		//body 스크롤 없애기
		var html_dom = document.getElementsByTagName('html')[0]; 
		var overflow = ''; 
			if(html_dom.style.overflow=='') overflow='hidden'; 
				else overflow=''; 
		html_dom.style.overflow = overflow; 
	});

	function fnLoginCheck(strText1, strText2, strText3) {
	    var strMobileTypeCd = document.getElementById("txtMobileTypeCd").value;
	    var strMobileFrontNo = document.getElementById("txtMobileFrontNo").value;
	    var strMobileRearNo = document.getElementById("txtMobileRearNo").value;
	    var strMemNm = document.getElementById("txtNm").value;
	    var strUserId = document.getElementById("txtUserId").value;

	    document.getElementById("txtMobileTypeCd").value = trim(strMobileTypeCd);
	    document.getElementById("txtMobileFrontNo").value = trim(strMobileFrontNo);
	    document.getElementById("txtMobileRearNo").value = trim(strMobileRearNo);
	    document.getElementById("txtNm").value = trim(strMemNm);
	    document.getElementById("txtUserId").value = trim(strUserId);

	    if (trim(strUserId) == "") {
	        document.getElementById("txtUserId").value = "";
	        alert(strText3);
	        return false;
	    }

	    if (trim(strMobileTypeCd) == "") {
	        document.getElementById("txtMobileTypeCd").value = "";
	        alert(strText1);
	        return false;
	    }

	    if (trim(strMobileFrontNo) == "") {
	        document.getElementById("txtMobileFrontNo").value = "";
	        alert(strText1);
	        return false;
	    }

	    if (trim(strMobileRearNo) == "") {
	        document.getElementById("txtMobileRearNo").value = "";
	        alert(strText1);
	        return false;
	    }

	    if (trim(strMemNm) == "") {
	        alert(strText2);
	        document.getElementById("txtNm").value = "";
	        return false;
	    }
	    return true;
	}
    // 공통성이 짙은 함수이나 페이지마다 알맞게 변형해서 사용할 것.
    function fnKeyEnter() 
    {  	 
	    if(event.keyCode ==13)   
	    {
		    event.keyCode = "";
    		
		    if(fnLoginCheck())
		    { 
                <%= Page.ClientScript.GetPostBackEventReference(imgbtnFindPwd, "") %>
		    }
		    else
		    {
		        return false;
	        }
    	    
	        return true;
	    }
    }
</script>
</head>
<body>
    <form id="frmlogin" runat="server">
		<div id="FIP-wp">
			<div class="Cont">
				<h1><img src="/Common/Images/Common/Index-kn-logo.png" alt="Keangnam"  /></h1>
				<div class="FIP-box">
					<div class="Fip">
						<p class="ftit"><asp:Literal ID="ltFindPwd" runat="server"></asp:Literal></p>
				        <p class="fsel"><asp:RadioButtonList ID="rdoFindPwd" runat="server" OnSelectedIndexChanged="rdoContNo_SelectedIndexChanged" AutoPostBack="true" CssClass="fsel" RepeatLayout="Flow"></asp:RadioButtonList></p>
						<ul class="Fiptx">
							<li>
								<span class="Titx"><asp:Literal ID="ltUserId" runat="server"></asp:Literal></span>
								<asp:TextBox ID="txtUserId" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty"></asp:TextBox> 
							</li>
							<li>
								<span class="Titx"><asp:Literal ID="ltMobile" runat="server"></asp:Literal></span>
								<asp:TextBox ID="txtMobileTypeCd" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty iw30"></asp:TextBox> 
								- <asp:TextBox ID="txtMobileFrontNo" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty iw30"></asp:TextBox> 
								- <asp:TextBox ID="txtMobileRearNo" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty iw30"></asp:TextBox>
							</li>							
							<li>
								<span class="Titx"><asp:Literal ID="ltNm" runat="server"></asp:Literal></span>
						        <asp:TextBox ID="txtNm" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty"></asp:TextBox>
						    </li>
						</ul>
						<div class="FIP-btnw">
							<span><asp:ImageButton ID="imgbtnFindPwd" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/FIPBtn1.gif" onclick="imgbtnFindPwd_Click"/></span>
					        <span><asp:ImageButton ID="imgbtnMoveLogIn" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/FIPBtn2.gif" onclick="imgbtnMoveLogin_Click"/></span>
						</div>
					</div>
				</div><!-- //Lbox -->		
			</div>
		</div>
	</form>
</body>
</html>
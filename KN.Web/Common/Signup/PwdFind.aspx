<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PwdFind.aspx.cs" Inherits="KN.Web.Common.Signup.PwdFind" %>
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
</script>
</head>
<body>
    <form id="frmlogin" runat="server">
		<div id="FIP-wp">
			<div class="Cont">
				<h1><img src="/Common/Images/Common/Index-kn-logo.png" alt="Keangnam"  /></h1>
				<div class="FIP-box">					
					<div class="FIalert2 MrgB10">
						<div class="Txwp">
							<p><asp:Literal ID="ltUserPwd" runat="server"></asp:Literal></p>
						</div>
						<div class="FIP-btnw">
							<span><asp:ImageButton ID="imgbtnMoveLogIn" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/FIPBtn2.gif" onclick="imgbtnMoveLogin_Click"/></span>
						</div>
					</div>					
				</div><!-- //Lbox -->		
			</div>
		</div>
	</form>
</body>
</html>
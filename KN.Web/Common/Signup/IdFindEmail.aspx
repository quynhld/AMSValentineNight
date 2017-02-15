<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="IdFindEmail.aspx.cs" Inherits="KN.Web.Common.Signup.IdFindEmail" %>
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
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script type="text/javascript">
	$(function(){
		//body 스크롤 없애기
		var html_dom = document.getElementsByTagName('html')[0]; 
		var overflow = ''; 
			if(html_dom.style.overflow=='') overflow='hidden'; 
				else overflow=''; 
		html_dom.style.overflow = overflow;
});

function fnLoginCheck(strText1, strText2)
{
    var strEmailId      = document.getElementById("txtEmailId").value;
    var strEmailServer  = document.getElementById("txtEmailServer").value;
    var strMemNm        = document.getElementById("txtNm").value;
   
    document.getElementById("txtEmailId").value      = trim(strEmailId);
    document.getElementById("txtEmailServer").value  = trim(strEmailServer);
    document.getElementById("txtNm").value           = trim(strMemNm);
    
    if (trim(strEmailId) == "")
    {
        document.getElementById("txtEmailId").value = "";
        alert(strText1);
        return false;
    }
    
    if (trim(strEmailServer) == "")
    {
        document.getElementById("txtEmailServer").value = "";
        alert(strText1);
        return false;
    }

    if (trim(strMemNm) == "")
    {
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
            <%= Page.ClientScript.GetPostBackEventReference(imgbtnFindID, "") %>
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
		        <h1><img src="/Common/Images/Common/Index-kn-logo.png" alt="Keangnam"/></h1>
		        <div class="FIP-box">
			        <div class="Fip">
				        <p class="ftit"><asp:Literal ID="ltFindID" runat="server"></asp:Literal></p>
				        <p class="fsel"><asp:RadioButtonList ID="rdoFindID" runat="server" OnSelectedIndexChanged="rdoContNo_SelectedIndexChanged" AutoPostBack="true" CssClass="fsel" RepeatLayout="Flow"></asp:RadioButtonList></p>
				        <ul class="Fiptx">
					        <li>
						        <span class="Titx"><asp:Literal ID="ltEmail" runat="server"></asp:Literal></span>
						        <asp:TextBox ID="txtEmailId" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty iw100"></asp:TextBox> @
							    <asp:TextBox ID="txtEmailServer" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty iw100"></asp:TextBox>
							</li> 					                					
					        <li>
						        <span class="Titx"><asp:Literal ID="ltNm" runat="server"></asp:Literal></span>
						        <asp:TextBox ID="txtNm" runat="server" onKeyPress="javascript:fnKeyEnter();" CssClass="ipty"></asp:TextBox>
						    </li>
				        </ul>
				        <div class="FIP-btnw">
					        <!-- <span><img src="/Common/Images/Btn/FIPBtn1.gif" alt /></span> -->
					        <span><asp:ImageButton ID="imgbtnFindID" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/FIPBtn3.gif" onclick="imgbtnFindID_Click"/></span>
					        <span><asp:ImageButton ID="imgbtnMoveLogIn" runat="server" AlternateText="find id" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Btn/FIPBtn2.gif" onclick="imgbtnMoveLogin_Click"/></span>
				        </div>
			        </div>
		        </div><!-- //Lbox -->

	        </div>
        </div>
    </form>
</body>
</html>
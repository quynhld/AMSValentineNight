<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemoWriteMe.aspx.cs" Inherits="KN.Web.Board.Memo.MemoWriteMe" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnFileTypeCheck(file)
    {
        if (file && file.value.length > 0)
        {
            if (!event.srcElement.value.match(/(.jpg|.gif|.png|.bmp|.pcx|.JPG|.GIF|.PNG|.BMP|.PCX|.zip|.rar|.doc|.pdf|.hwp|.gul|[0-15]{16})/))
            {
                alert("압축파일. 이미지파일, 문서파일만 등록이 가능합니다.");
                file.select();
                document.selection.clear();
            }
        }
    }

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
//-->
</script>
<div class="Tbtop-tit">
    <asp:Literal ID="ltWrite" runat="server"></asp:Literal>
</div>
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
        <tr>
            <th><asp:Literal ID="ltFileAddon" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon" runat="server" Width="450px"/>
            </td>
        </tr>
    </tbody>
</table>
<div class="Btwps FloatR">	    
    <div class="Btn-Type3-wp FloatL">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				    <span> <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
</div>
<asp:TextBox ID="txtHfMemoSeq" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfInsMemNo" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
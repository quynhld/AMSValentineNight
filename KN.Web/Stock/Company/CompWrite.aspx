<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CompWrite.aspx.cs" Inherits="KN.Web.Stock.Company.CompWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

    <script language="javascript" type="text/javascript">
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

    function fnValidateData(strText)
    {
        var strCompNm = document.getElementById("<%=txtCompNm.ClientID%>");
        var strChargerNm = document.getElementById("<%=txtChargerNm.ClientID%>");
        var strCompTelFrontNo = document.getElementById("<%=txtCompTelFrontNo.ClientID%>");
        var strCompTelMidNo = document.getElementById("<%=txtCompTelMidNo.ClientID%>");
        var strCompTelRearNo = document.getElementById("<%=txtCompTelRearNo.ClientID%>");
        var strCompFaxFrontNo = document.getElementById("<%=txtCompFaxFrontNo.ClientID%>");
        var strCompFaxMidNo = document.getElementById("<%=txtCompFaxMidNo.ClientID%>");
        var strCompFaxRearNo = document.getElementById("<%=txtCompFaxRearNo.ClientID%>");
        var strAddr = document.getElementById("<%=txtAddr.ClientID%>");
        var strDetAddr = document.getElementById("<%=txtDetAddr.ClientID%>");

        if (trim(strCompNm.value) == "")
        {
            strCompNm.focus();
            alert(strText);
            return false;
        }

        if (trim(strChargerNm.value) == "")
        {
            strChargerNm.focus();
            alert(strText);
            return false;
        }

        if (trim(strCompTelFrontNo.value) == "")
        {
            strCompTelFrontNo.focus();
            alert(strText);
            return false;
        }

        if (trim(strCompTelMidNo.value) == "")
        {
            strCompTelMidNo.focus();
            alert(strText);
            return false;
        }
        
        if (trim(strCompTelRearNo.value) == "")
        {
            strCompTelRearNo.focus();
            alert(strText);
            return false;
        }
        
        if (trim(strCompFaxFrontNo.value) == "")
        {
            strCompFaxFrontNo.focus();
            alert(strText);
            return false;
        }

        if (trim(strCompFaxMidNo.value) == "")
        {
            strCompFaxMidNo.focus();
            alert(strText);
            return false;
        }
        
        if (trim(strCompFaxRearNo.value) == "")
        {
            strCompFaxRearNo.focus();
            alert(strText);
            return false;
        }

        if (trim(strAddr.value) == "")
        {
            strAddr.focus();
            alert(strText);
            return false;
        }

        if (trim(strDetAddr.value) == "")
        {
            strDetAddr.focus();
            alert(strText);
            return false;
        }

        return true;        
    }    
</script>
    <table class="TbCel-Type2-E">
        <col width="100px"/>
        <col width="300px"/>
        <col width="100px"/>
        <col width="300px"/>
	    <tbody>
		    <tr>
			    <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
			    <td colspan="3"><asp:TextBox ID="txtCompNm" runat="server" Width="550" MaxLength="100"></asp:TextBox></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltRepresentiveNm" runat="server"></asp:Literal></th>
			    <td><asp:TextBox ID="txtRepresentiveNm" runat="server" Width="250" MaxLength="100"></asp:TextBox></td>
			    <th><asp:Literal ID="ltChargerNm" runat="server"></asp:Literal></th>
			    <td><asp:TextBox ID="txtChargerNm" runat="server" Width="250" MaxLength="100"></asp:TextBox></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltIndus" runat="server"></asp:Literal></th>
			    <td colspan="3">
				    <asp:UpdatePanel ID="upIndus" UpdateMode="Conditional" runat="server">
				        <Triggers><asp:AsyncPostBackTrigger ControlID="ddlIndus" EventName="SelectedIndexChanged"/></Triggers>
				        <ContentTemplate>
				            <asp:DropDownList ID="ddlIndus" runat="server" onSelectedIndexChanged="ddlIndus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
				            <asp:TextBox ID="txtEtcIndus" runat="server" Width="200" MaxLength="200"></asp:TextBox>				            
				        </ContentTemplate>
				    </asp:UpdatePanel>				    
			    </td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltTel" runat="server"></asp:Literal></th>
			    <td>
				    <asp:TextBox ID="txtCompTelFrontNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
				    <span>-</span>
				    <asp:TextBox ID="txtCompTelMidNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
				    <span>-</span>
				    <asp:TextBox ID="txtCompTelRearNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
			    </td>
			    <th><asp:Literal ID="ltFax" runat="server"></asp:Literal></th>
			    <td>
				    <asp:TextBox ID="txtCompFaxFrontNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
				    <span>-</span>
				    <asp:TextBox ID="txtCompFaxMidNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
				    <span>-</span>
				    <asp:TextBox ID="txtCompFaxRearNo" runat="server" Width="30" MaxLength="4"></asp:TextBox>
			    </td>
		    </tr>
		    <tr>
			    <th rowspan="2"><asp:Literal ID="ltAddr" runat="server"></asp:Literal></th>
			    <td colspan="3">
			        <asp:TextBox ID="txtAddr" runat="server" Width="550" MaxLength="200"></asp:TextBox>
			    </td>
		    </tr>
		    <tr>
		        <td colspan="3">
		            <asp:TextBox ID="txtDetAddr" runat="server" Width="550" MaxLength="200"></asp:TextBox>
		        </td>
		    </tr>
    	    <tr>
			    <th><asp:Literal ID="ltCompTy" runat="server"></asp:Literal></th>
			    <td colspan="3"><asp:DropDownList ID="ddlCompTyCd" runat="server"></asp:DropDownList></td>
		    </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon1" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:FileUpload ID="fileAddon1" runat="server" Width="450px"/>
                </td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon2" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:FileUpload ID="fileAddon2" runat="server" Width="450px"/>
                </td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltFileAddon3" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:FileUpload ID="fileAddon3" runat="server" Width="450px"/>
                </td>
            </tr>
		    <tr>
			    <th><asp:Literal ID="ltIntro" runat="server"></asp:Literal></th>
			    <td colspan="3">
				    <asp:TextBox ID="txtIntro" runat="server" Columns="20" Rows="10" Height="50" Width="550"></asp:TextBox>
			    </td>
		    </tr>
	    </tbody>
    </table>
    <div class="Btwps FloatR">
	    <div class="Btn-Type2-wp FloatL">
		    <div class="Btn-Tp2-L">
			    <div class="Btn-Tp2-R">
				    <div class="Btn-Tp2-M">
					    <span> <asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp FloatL">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span> <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp FloatL">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span> <asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
</asp:Content>

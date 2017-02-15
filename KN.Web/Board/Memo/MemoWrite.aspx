<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemoWrite.aspx.cs" Inherits="KN.Web.Board.Memo.MemoWrite" ValidateRequest="false"%>
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
<asp:UpdatePanel ID="upLogList" runat="server" UpdateMode="Conditional">
    <Triggers>

    </Triggers>
    <ContentTemplate>
        <div class="OverH">
	        <div class="FloatL iw350 OverH">
		        <table class="TbCel-Type2-E iw350 Mrg0">
			        <tbody>
				        <tr>
                            <th><asp:Literal ID="ltAuthority" runat="server"></asp:Literal></th>
                            <td class="bwe1">
                                <asp:DropDownList ID="ddlAuth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuth_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
					        <th><asp:Literal ID="ltGrpSelect" runat="server"></asp:Literal></th>
					        <td>
						        <div class="Type2-rdo">
							        <asp:DropDownList ID="ddlGrpSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpSelect_SelectedIndexChanged"></asp:DropDownList>
							        <asp:TextBox ID="txtHfGrpMemNo" runat="server" Visible="false"></asp:TextBox>
						        </div>
					        </td>
				        </tr>
			        </tbody>
		        </table>
		        <div class="allsel"><asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"/><asp:Literal ID="ltChkAll" runat="server"></asp:Literal></div>
		        <asp:ListView ID="lvMemberList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" GroupPlaceholderID="groupPlaceHolderID" GroupItemCount="2" OnItemDataBound="lvMemberList_ItemDataBound" OnItemCreated="lvMemberList_ItemCreated">
                    <LayoutTemplate>
                        <table cellspacing="0" class="TypeA">
                            <tr>
                                <td>
                                    <div class="mlist">
									    <ul id="groupPlaceHolderID" runat="server"></ul>
								    </div>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <ul><li id="iphItemPlaceHolderID" runat="server"></li></ul>
                    </GroupTemplate>
                    <ItemTemplate>
                        <li>
                            <span>
                                <asp:CheckBox ID="chkMemberList" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"/><label><asp:Literal ID="ltMemberList" runat="server" ></asp:Literal></label>
                                <asp:TextBox ID="txtHfChkMemNo" runat="server" Visible="false"></asp:TextBox>
                            </span>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
	        </div>
	        <div class="FloatL MrgL70 PT100 OverH">
		        <div class="Btn-Type2-wp ">
			        <div class="Btn-Tp2-L">
				        <div class="Btn-Tp2-R">
					        <div class="Btn-Tp2-M">
						        <span><asp:LinkButton ID="lnkbtnAdd" runat="server" onclick="lnkbtnAdd_Click"></asp:LinkButton>▶</span>
					        </div>
				        </div>
			        </div>
		        </div>
		        <div class="Btn-Type2-wp ">
			        <div class="Btn-Tp2-L">
				        <div class="Btn-Tp2-R">
					        <div class="Btn-Tp2-M">
						        <span>◀<asp:LinkButton ID="lnkbtnDel" runat="server" onclick="lnkbtnDel_Click"></asp:LinkButton></span>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
            <div class="FloatL MrgL70 OverH"><asp:ListBox ID="lbAddMemberList" runat="server" class="selist"></asp:ListBox></div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Indtm Clear MrgT20">
        <table class="TbCel-Type2-E MrgB10">
            <colgroup>
                <col/>
                <col/>
            </colgroup>
            <tbody>
	            <tr>
		            <th class="PLTB"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
		            <td><span class="IdTit"><asp:TextBox ID="txtTitle" runat="server" MaxLength="255" CssClass="iw550 bgType2"></asp:TextBox></span></td>
	            </tr>
	            <tr>
		            <th><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
		            <td><asp:TextBox runat="server" ID="txtContext" Rows="20" Columns="400" TextMode="MultiLine" Width="550px" Height="150px"></asp:TextBox></td>
	            </tr>
                <tr>
                    <th class="Fr-line"><asp:Literal ID="ltFileAddon" runat="server"></asp:Literal></th>
                    <td><span class="Ls-line"><asp:FileUpload ID="fileAddon" runat="server" Width="450px"/></span></td>
                </tr>
            </tbody>
        </table>
        <div class="Btwps FloatR">
            <div class="Btn-Type3-wp">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span><asp:LinkButton ID="lnkbtnSend" runat="server" onclick="lnkbtnSend_Click"></asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>
    </div>
</asp:Content>
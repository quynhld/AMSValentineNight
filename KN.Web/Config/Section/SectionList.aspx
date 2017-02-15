<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="SectionList.aspx.cs" Inherits="KN.Web.Config.Section.SectionList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
//    function fnCheckFileUpload(strText)
//    {
//        var strFileupload = document.getElementById("fuExcelUpload.ClientID");

//        if (trim(strFileupload.value) == "")
//        {
//            alert(strText);
//            return false;
//        }
//    }

//    function fnCheckValidate(strText)
//    {
//        var strFloor = document.getElementById("txtFloor.ClientID");
//        var strSection = document.getElementById("txtSection.ClientID");
//        var strLeasingArea = document.getElementById("txtLeasingArea.ClientID");
//        var strInsDt = document.getElementById("hfInsDt.ClientID");

//        if (trim(strFloor.value) == "")
//        {
//            strFloor.focus();
//            alert(strText);
//            return false;
//        }

//        if (trim(strSection.value) == "")
//        {
//            strSection.focus();
//            alert(strText);
//            return false;
//        }

//        if (trim(strLeasingArea.value) == "")
//        {
//            strLeasingArea.focus();
//            alert(strText);
//            return false;
//        }

//        if (trim(strInsDt.value) == "")
//        {
//            alert(strText);
//            return false;
//        }

//        return true;
//    }
//-->
</script>
	<div class="TpAtit1">
		<div class="TpAw span"><span><asp:Literal ID="ltSearchMenu" runat="server"></asp:Literal></span></div>
		<div class="TpBw span"><span><asp:Literal ID="ltSearchCond" runat="server"></asp:Literal></span></div>
        <span class="shf-sel FloatR2">
            <asp:DropDownList ID="ddlFloor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
        </span>
	</div>
	<div style="overflow-y:scroll;height:400px;" class="iw840">
    <table class="TypeA iw820">
        <col width="50px"/>
        <col width="120px"/>
        <col width="220px"/>
        <col width=""/>
        <col width=""/>
        <col width="100px"/>
	    <thead>
            <tr>
                <th><asp:Literal ID="ltFloor" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltSection" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltLeasingArea" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                <th>&nbsp;</th>
            </tr>
	    </thead>
    </table>
    <asp:ListView ID="lvSectionList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvSectionList_ItemDataBound"
        OnItemCreated="lvSectionList_ItemCreated" OnItemDeleting="lvSectionList_ItemDeleting" OnItemUpdating="lvSectionList_ItemUpdating">
        <LayoutTemplate>
            <table class="TbCel-Type4-A iw820">
               <col width="50px"/>
                <col width="120px"/>
                <col width="220px"/>
                <col width=""/>
                <col width=""/>
                <col width="100px"/>
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                </tbody>                
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter P0"><asp:Literal ID="ltFloor" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter P0"><asp:Literal ID="ltSection" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter P0">
                    <asp:TextBox ID="txtCompNm" runat="server" Width="200" MaxLength="255" Visible="false"></asp:TextBox>
                    <asp:Literal ID="ltCompNm" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hfCompSeq" runat="server"/>
                </td>
                <td class="TbTxtCenter P0">
                    <asp:TextBox ID="txtLeasingArea" runat="server" Width="130" Visible="false"></asp:TextBox>
                    <asp:Literal ID="ltLeasingArea" runat="server"></asp:Literal>
                </td>
                <td class="TbTxtCenter P0">
                    <asp:TextBox ID="txtInsDt" runat="server" Width="70" Visible="false"></asp:TextBox>
                    <asp:Literal ID="ltInsDt" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hfInsDt" runat="server"/>
                    <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="TbTxtCenter P0">
                    <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" Visible="false"/></span>
                    <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" Visible="false" /></span>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA iw820">
			  <col width="50px"/>
                <col width="120px"/>
                <col width="220px"/>
                <col width=""/>
                <col width=""/>
                <col width="100px"/>
                <tbody>
                    <tr><td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
<%--    <table class="TbCel-Type4-A-1 iw820">
		<col width="50px"/>
        <col width="120px"/>
        <col width="220px"/>
        <col width=""/>
        <col width=""/>
        <col width="100px"/>
        <tbody>
            <tr>
                <td class="TbTxtCenter"><asp:TextBox ID="txtFloor" runat="server" Width="40"></asp:TextBox></td>
                <td class="TbTxtCenter"><asp:TextBox ID="txtSection" runat="server" Width="90"></asp:TextBox></td>
                <td class="TbTxtCenter">
                    <asp:TextBox ID="txtCompNm" runat="server" Width="200"></asp:TextBox>
                    <asp:HiddenField ID="hfCompSeq" runat="server"/>
                </td>
                <td class="TbTxtCenter"><asp:TextBox ID="txtLeasingArea" runat="server" Width="130"></asp:TextBox></td>
                <td class="TbTxtCenter">
                    <asp:TextBox ID="txtInsDt" runat="server" ReadOnly="true" Width="70"></asp:TextBox>
                    <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtInsDt.ClientID%>', '<%=hfInsDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                    <asp:HiddenField ID="hfInsDt" runat="server"/>
                    <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="TbTxtCenter"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
            </tr>
        </tbody>
    </table>--%>
<%--	<table class="Type-viewB-PN iw820">
		<col width="15%"/>
		<col width="75%"/>
        <tr>
		    <th class="Fr-line"><asp:Literal ID="ltAddonFile" runat="server"></asp:Literal></th>
			<td>
			    <span class="Ls-line">
			        <asp:FileUpload ID="fuExcelUpload" runat="server" Width="300px" Visible="false"/>
			        <asp:Literal ID="ltSampleFile" runat="server" Visible="false"></asp:Literal>
			        <asp:HyperLink ID="hlExcel" ImageUrl="~/Common/Images/Icon/exell.gif" runat="server" NavigateUrl="~/Config/Section/StandardFiles.xls"></asp:HyperLink>
			    </span>
			</td>
		</tr>
	</table>--%>
    </div>
<%--	<div class="Btwps FloatR">
	    <div class="Btn-Type3-wp ">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnFileUpload" runat="server" OnClick="lnkbtnFileUpload_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnEntireReset" runat="server" OnClick="lnkbtnEntireReset_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnFloorReset" runat="server" OnClick="lnkbtnFloorReset_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
	</div>--%>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>
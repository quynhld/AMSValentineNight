<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ParkingCardList.aspx.cs" Inherits="KN.Web.Park.ParkingCardList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    function fnCheckFileUpload(strText)
    {
        var strFileupload = document.getElementById("<%=fuExcelUpload.ClientID%>");

        if (trim(strFileupload.value) == "")
        {
            alert(strText);
            return false;
        }
    }
    
    function fnMovePage(intPageNo) 
    {
        if (intPageNo == null) 
        {
            intPageNo = 1;
        }
        
        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
        <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
    }
    
    function fnCheckCdEnter() 
    {
        if (event.keyCode==13) 
        {
            <%=Page.GetPostBackEventReference(lnkbtnSearch)%>;
            
        }                        
    }
    
    function fnIssuingCheck(strTxt1, strText2)
    {
        if (confirm(strTxt1))
        {
            var strInsCardNo = document.getElementById("<%=txtInsCardNo.ClientID%>");
            var strInsTagNo = document.getElementById("<%=txtInsTagNo.ClientID%>");
            var strInsCarTyCd = document.getElementById("<%=ddlInsCarTyCd.ClientID%>");

            if (trim(strInsCardNo.value) == "")
            {
                strInsCardNo.focus();
                alert(strText2);
                return false;
            }

            if (trim(strInsTagNo.value) == "")
            {
                strInsTagNo.focus();
                alert(strText2);
                return false;
            }

            if (trim(strInsCarTyCd.value) == "0000")
            {
                strInsCarTyCd.focus();
                alert(strText2);
                return false;
            }
        }
        else
        {
            return false;
        }
    }    
    
    function fnValidateCheck(strTxt)
    {
        if (confirm(strTxt))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    function fnEntireIssuingCheck(strTxt)
    {
        if (confirm(strTxt))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //-->
    </script>
    <fieldset class="sh-field1">
	    <legend></legend>
	    <ul class="sf1-ag MrgL180">
		    <li>
			    <asp:Literal ID="ltSearchCardNo" runat="server"></asp:Literal>
		    </li>
		    <li>
			    <asp:TextBox ID="txtKeyWord" runat="server" CssClass="sh-input" OnKeyPress="javascript:fnCheckCdEnter();"></asp:TextBox>
		    </li>
		    <li>
		        <asp:DropDownList ID="ddlCarTyCd" runat="server"></asp:DropDownList>
		    </li>
             <li>
	                <asp:Literal ID="ltIssYN" runat="server" Text="Use YN"></asp:Literal>
	                <asp:DropDownList ID="ddlIssYN" runat="server" AutoPostBack="true" onselectedindexchanged="ddlIssYN_SelectedIndexChanged" 
                        ></asp:DropDownList>
	     </li> 
		    <li>
			    <div class="Btn-Type4-wp">
				    <div class="Btn-Tp4-L">
					    <div class="Btn-Tp4-R">
						    <div class="Btn-Tp4-M">
							    <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
						    </div>
					    </div>
				    </div>
			    </div>
		    </li>
	    </ul>
    </fieldset>
    <asp:ListView ID="lvTagList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
        OnLayoutCreated="lvTagList_LayoutCreated" OnItemDataBound="lvTagList_ItemDataBound" OnItemCreated="lvTagList_ItemCreated"
        OnItemDeleting="lvTagList_ItemDeleting" OnItemUpdating="lvTagList_ItemUpdating">
        <LayoutTemplate>
            <table class="TbCel-Type4-A iw840">
                <col width="50px"/>
                <col width="140px"/>
                <col width="220px"/>
                <col width="200px"/>
                <col width="130px"/>
                <col width="100px"/>
                <thead>
                    <tr>
                        <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltCardNo" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltTagNo" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltCarTyCd" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltIssuedYn" runat="server"></asp:Literal></th>
                        <th>&nbsp;</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                </tbody>                
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter P0">
                    <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="TbTxtCenter P0"><asp:Literal ID="ltCardNo" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter P0"><asp:Literal ID="ltTagNo" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter P0"><asp:Literal ID="ltCarTyCd" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter P0">
                    <asp:DropDownList ID="ddlIssuedYn" runat="server">
                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="TbTxtCenter P0">
                    <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                    <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA iw840">
                <col width="50px"/>
                <col width="140px"/>
                <col width="220px"/>
                <col width="200px"/>
                <col width="130px"/>
                <col width="100px"/>
                <thead>
                    <tr>
                        <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltCardNo" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltTagNo" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltCarTyCd" runat="server"></asp:Literal></th>
                        <th><asp:Literal ID="ltIssuedYn" runat="server"></asp:Literal></th>
                        <th>&nbsp;</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <table class="TbCel-Type4-A-1 iw840">
        <col width="50px"/>
        <col width="140px"/>
        <col width="220px"/>
        <col width="200px"/>
        <col width="130px"/>
        <col width="100px"/>
        <tbody>
        <tr>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:TextBox ID="txtInsCardNo" runat="server" Width="90" MaxLength="10"></asp:TextBox></td>
            <td class="TbTxtCenter"><asp:TextBox ID="txtInsTagNo" runat="server" Width="200" MaxLength="20"></asp:TextBox></td>
            <td class="TbTxtCenter"><asp:DropDownList ID="ddlInsCarTyCd" runat="server"></asp:DropDownList></td>
            <td class="TbTxtCenter">
                <asp:DropDownList ID="ddlInsIssuedYn" runat="server">
                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="TbTxtCenter"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
        </tr>
        </tbody>
    </table>
    <table class="Type-viewB-PN iw840">
	    <col width="15%"/>
	    <col width="75%"/>
        <tr>
	        <th class="Fr-line"><asp:Literal ID="ltAddonFile" runat="server"></asp:Literal></th>
		    <td>
		        <span class="Ls-line">
		            <asp:FileUpload ID="fuExcelUpload" runat="server" Width="300px"/>
		            <asp:Literal ID="ltSampleFile" runat="server" Visible="false"></asp:Literal>
		            <asp:HyperLink ID="hlExcel" ImageUrl="~/Common/Images/Icon/exell.gif" runat="server" NavigateUrl="~/Park/StandardFiles.xls"></asp:HyperLink>
		        </span>
		    </td>
	    </tr>
    </table>
    <div class="Clear">
	    <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>
	<div class="Btwps FloatR">
	    <div class="Btn-Type3-wp ">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnFileUpload" runat="server" OnClick="lnkbtnFileUpload_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	</div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
</asp:Content>
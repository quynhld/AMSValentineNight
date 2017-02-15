<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="WarehouseMngList.aspx.cs" Inherits="KN.Web.Stock.Warehousing.WarehouseMngList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnMovePage(intPageNo) 
        {
            if (intPageNo == null) 
            {
                intPageNo = 1;
            }
            
            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }
        
        function fnCheckValidate(strTxt)
        {
            var strSvcCd = document.getElementById("<%=txtSvcCd.ClientID%>");
            var strRemark = document.getElementById("<%=txtRemarkTxt.ClientID%>");
            
            if (trim(strSvcCd.value) == "")
            {
                alert(strTxt);
                strSvcCd.focus();
                return false;
            }
            
            if (trim(strRemark.value) == "")
            {
                alert(strTxt);
                strRemark.focus();
                return false;
            }
            
            return true;
        }
        
        function fnModifyConfirm(strClientID, strConfirmTxt, strAlertTxt)
        {
            if (confirm(strConfirmTxt))
            {
                var strClientID = document.getElementById(strClientID);
                
                if (trim(strClientID.value) == "")
                {
                    alert(strAlertTxt);
                    strClientID.focus();
                    
                    return false;
                }
                else
                {
                    return true;                
                }
            }
            
            return false;        
        }
    //-->        
    </script>
    <fieldset class="sh-field1">
	    <legend></legend>
	    <ul class="sf1-ag MrgL50">
		    <li><asp:DropDownList ID="ddlTopRentCd" runat="server"></asp:DropDownList></li>
		    <li><asp:Literal ID="ltSvcCd" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:TextBox ID="txtTopSvcCd" runat="server" CssClass="sh-input" MaxLength="4" Width="50"></asp:TextBox></li>
		    <li><asp:Literal ID="ltRemark" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:TextBox ID="txtTopRemark" runat="server" CssClass="sh-input" Width="200"></asp:TextBox></li>
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
 <div style="overflow-y:scroll;height:500px;" class="iw840">      	

    <asp:ListView ID="lvCargoList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvCargoList_LayoutCreated"
        OnItemDataBound="lvCargoList_ItemDataBound" OnItemCreated="lvCargoList_ItemCreated" OnItemDeleting="lvCargoList_ItemDeleting" OnItemUpdating="lvCargoList_ItemUpdating">
        <LayoutTemplate>
            <table class="TbCel-Type4-A iw820">
                <colgroup>
                    <col width="15%" />
                    <col width="15%" />
                    <col width="60%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <th><asp:Literal ID="ltSection" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltSvcCd" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                    <th></th>
                </tr>
                <tr runat="server" id="iphItemPlaceHolderID"></tr>
            </table>            
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter"><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></td>
                <td class="TbTxtCenter">
                    <asp:TextBox ID="txtSvcCd" runat="server" MaxLength="4" ReadOnly="true" class="iw30"></asp:TextBox>
                    <asp:HiddenField ID="hfSvcCd" runat="server"/>
                </td>
                <td><asp:TextBox ID="txtRemarkTxt" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td  class="TbTxtCenter">
                    <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                    <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TbCel-Type4-A iw820">
                <tr>
                    <th><asp:Literal ID="ltSection" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltSvcCd" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                    <th></th>
                </tr>
                <tr>
                    <td colspan="4"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <table cellspacing="0" class="TbCel-Type4-A-1 iw820">
        <colgroup>
            <col width="15%" />
            <col width="15%" />
            <col width="60%" />
            <col width="10%" />
        </colgroup>
        <tr>
            <td class="TbTxtCenter"><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></td>
            <td class="TbTxtCenter"><asp:TextBox ID="txtSvcCd" runat="server" MaxLength="4" class="iw30"></asp:TextBox></td>
            <td class="TbTxtLeft"><asp:TextBox ID="txtRemarkTxt" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
            <td class="TbTxtCenter"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
        </tr>
    </table>
   
</div>  
 <div class="Clear">
	    <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>  
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
</asp:Content>
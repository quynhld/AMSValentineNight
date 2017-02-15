<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardList.aspx.cs" Inherits="KN.Web.Board.Board.BoardList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="contList" ContentPlaceHolderID="cphContent" runat="server">
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
    
    function fnCheckCdEnter() 
    {
        if (event.keyCode==13) 
        {
            <%=Page.GetPostBackEventReference(lnkbtnSearch)%>;
        }                        
    }
    //-->
    </script>
    <fieldset class="sh-field1">
	    <legend></legend>
	    <ul class="sf1-ag MrgL180">
		    <li>
			    <asp:DropDownList ID="ddlKeyCd" runat="server"></asp:DropDownList>
		    </li>
		    <li>
			    <asp:TextBox ID="txtKeyWord" runat="server" CssClass="sh-input" OnKeyPress="fnCheckCdEnter()"></asp:TextBox>
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
    <asp:ListView ID="lvBoardList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
        OnLayoutCreated="lvBoardList_LayoutCreated" OnItemDataBound="lvBoardList_ItemDataBound" OnItemCreated="lvBoardList_ItemCreated">
        <LayoutTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="77px"/>
                <col/>
                <col width="109px"/>
                <col width="110px"/>
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
	                    <th class="end"><asp:Literal ID="ltViewCnt" runat="server"></asp:Literal></th>
                    <tr>
                </thead>
                <tbody>
                    <tr id="iphItemlPlaceholderId" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td align="center"><asp:Literal ID="ltLSeq" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltLTitle" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltLInsDt" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltLViewCnt" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="77px"/>
                <col/>
                <col width="109px"/>
                <col width="90px"/>
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
	                    <th class="end"><asp:Literal ID="ltViewCnt" runat="server"></asp:Literal></th>
                    <tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="Btn-Type2-wp FloatR">
	    <div class="Btn-Tp2-L">
		    <div class="Btn-Tp2-R">
			    <div class="Btn-Tp2-M">
				    <span><asp:LinkButton ID="lnkbtnWrite" runat="server" OnClick="lnkbtnWrite_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
    <div class="Clear">
	    <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:TextBox ID="txtHfBoardTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfBoardCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="KN.Web.Main" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<div class="OverH">
    <div class="FloatL MrgR05 ">
	    <div class="Tb-Tp-tit OverH iw270">
	        <span class="FloatL"><asp:Literal ID="ltNotice" runat="server"></asp:Literal></span> 
	        <span class="FloatR"> ><asp:LinkButton ID="lnkMore1" runat="server" OnClick="lnkTitleList_Click"></asp:LinkButton></span>
	    </div>
	    <asp:ListView ID="lvBoardList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
            OnLayoutCreated="lvBoardList_LayoutCreated" OnItemDataBound="lvBoardList_ItemDataBound" OnItemCreated="lvBoardList_ItemCreated">
            <LayoutTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>	                  
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="Bd-Lt">
                        <asp:LinkButton ID="lnkTitleList" runat="server" OnClick="lnkTitleList_Click" CommandArgument='<%#Eval("BoardSeq")%>'></asp:LinkButton>
                        <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Bd-Rt  TbTxtCenter P0"><asp:Literal ID="ltInsDtList" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt">
	                            <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
	                        </th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>	                  
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="2" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>                
    </div>
    <div class="FloatL MrgR05">
        <div class="Tb-Tp-tit OverH iw270">
            <span class="FloatL"><asp:Literal ID="ltArchives" runat="server"></asp:Literal></span> 
            <span class="FloatR"> ><asp:LinkButton ID="lnkMore2" runat="server" OnClick="lnkTitleList2_Click"></asp:LinkButton></span>
        </div>
         <asp:ListView ID="lvArchiveList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
            OnLayoutCreated="lvArchiveList_LayoutCreated" OnItemDataBound="lvArchiveList_ItemDataBound" OnItemCreated="lvArchiveList_ItemCreated">
            <LayoutTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>	                  
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="Bd-Lt">
                        <asp:LinkButton ID="lnkTitleList2" runat="server" OnClick="lnkTitleList2_Click" CommandArgument='<%#Eval("BoardSeq")%>'></asp:LinkButton>
                        <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Bd-Rt  TbTxtCenter P0"><asp:Literal ID="ltInsDtList" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt">
	                            <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
	                        </th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>	                  
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="2" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div class="FloatL">
        <div class="Tb-Tp-tit OverH iw270">
            <span class="FloatL"><asp:Literal ID="ltMemo" runat="server"></asp:Literal></span> 
            <span class="FloatR"> ><asp:LinkButton ID="lnkMore3" runat="server" OnClick="lnkTitleList3_Click"></asp:LinkButton></span>
        </div>
        <asp:ListView ID="lvMemoList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
            OnLayoutCreated="lvMemoList_LayoutCreated" OnItemDataBound="lvMemoList_ItemDataBound" OnItemCreated="lvMemoList_ItemCreated">
            <LayoutTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltReceiveDate" runat="server"></asp:Literal></th>	                  
                        <tr>
                    </thead>
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="Bd-Lt">
                        <asp:LinkButton ID="lnkTitleList3" runat="server" OnClick="lnkTitleList3_Click" CommandArgument='<%#Eval("MemoSeq")%>'></asp:LinkButton>
                        <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt Bd-Rt  TbTxtCenter P0"><asp:Literal ID="ltReceiveDateList" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" class="TbCel-Type3-A iw270">
                    <col width="" />
	                <col width="50px" />
                    <thead>
                        <tr>	                        
	                        <th class="Bd-Lt">
	                            <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
	                        </th>
	                        <th class="Bd-Lt Bd-Rt"><asp:Literal ID="ltReceiveDate" runat="server"></asp:Literal></th>	                  
                        <tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="2" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</div>
<div class="Clear MrgT20 Msbd">
    <div class="iw840 OverH">
        <asp:ListView ID="lvSiteMapList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnItemDataBound="lvSiteMapList_ItemDataBound">
            <LayoutTemplate>
                <table class="MainS">               
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <asp:Literal ID="ltContent" runat="server"></asp:Literal>              
                </tr>
            </ItemTemplate>
        </asp:ListView>
   </div>
</div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CounselList_test.aspx.cs" Inherits="KN.Web.Resident.Counsel.CounselList_test" ValidateRequest="false"%>
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

function fnDetailView(strCounselCd, intCounselSeq)
{
    document.getElementById("<%=hfCounselCd.ClientID%>").value = strCounselCd;
    document.getElementById("<%=hfCounselSeq.ClientID%>").value = intCounselSeq;

    <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
}

function fnOrderView(strOrderCd)
{
    document.getElementById("<%=hfOrderCd.ClientID%>").value = strOrderCd;
    <%=Page.GetPostBackEventReference(lnkbtnSearch)%>;
}
//-->
</script>
<asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnInput" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>        
        <asp:AsyncPostBackTrigger ControlID="lnkbtnDetailSearch" EventName="Click"/>  
    </Triggers>
    <ContentTemplate>
    <div class="Btn-Type2-wp ">
		<div class="Btn-Tp2-L">
			<div class="Btn-Tp2-R">
				<div class="Btn-Tp2-M">
					<span> <asp:LinkButton ID="lnkbtnInput" runat="server"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30 ">
            <li><asp:Literal ID="ltInsDt" runat="server"></asp:Literal>
            <li>
		        <asp:TextBox ID="txtStartInsDt" runat="server" Width="70px"></asp:TextBox>
		        <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartInsDt.ClientID%>', '<%=hfStartInsDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
		        <asp:HiddenField ID="hfStartInsDt" runat="server"/>
		    </li>
		    <li><span>~</span></li>
		    <li>
			    <asp:TextBox ID="txtEndInsDt" runat="server" Width="70px"></asp:TextBox>
			    <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndInsDt.ClientID%>', '<%=hfEndInsDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
			    <asp:HiddenField ID="hfEndInsDt" runat="server"/>
		    </li>		    
            <%--
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>                   
                </div>
            </li>
            --%>    
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlIndustry" runat="server"></asp:DropDownList>                   
                </div>
            </li>
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">            
            <li><asp:Literal ID="ltContperiod" runat="server"></asp:Literal></li>
            <li>
		        <asp:TextBox ID="txtStartLeaseDt" runat="server" Width="70px"></asp:TextBox>
		        <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartLeaseDt.ClientID%>', '<%=hfStartLeaseDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer; width: 18px;" value=""/>
		        <asp:HiddenField ID="hfStartLeaseDt" runat="server"/>
		    </li>
		    <li><span>~</span></li>
		    <li>
			    <asp:TextBox ID="txtEndLeaseDt" runat="server" Width="70px"></asp:TextBox>
			    <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndLeaseDt.ClientID%>', '<%=hfEndLeaseDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
			    <asp:HiddenField ID="hfEndLeaseDt" runat="server"/>
		    </li>                       
            <li>
	            <div class="Btn-Type4-wp">
		            <div class="Btn-Tp4-L">
			            <div class="Btn-Tp4-R">
				            <div class="Btn-Tp4-M">
					            <span><asp:LinkButton ID="lnkbtnDetailSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
				            </div>
			            </div>
		            </div>
	            </div>		        
            </li>
        </ul>
    </fieldset>
    <asp:ListView ID="lvCounselList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvCounselList_ItemDataBound"
        OnLayoutCreated="lvCounselList_LayoutCreated" OnItemCreated="lvCounselList_ItemCreated">
        <LayoutTemplate>
            <table cellspacing="0" class="TypeA">
		        <colgroup>
			        <col width="40px"/>
			        <col width="220px"/>
			        <col width="80px"/>
			        <col width="100px"/>
			        <col width="70px"/>
			        <col width="70px"/>
			        <col width="100px"/>
			        <col width="60px"/>
			        <col width="80px"/>
		        </colgroup>
                <thead>
                    <tr>
                        <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>                        
                        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0001" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltCountry" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort1" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0002" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltIndus" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort2" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0003" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltUseArea" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort3" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0004" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltLeaseFee" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort4" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0005" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltExpectedArea" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort5" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0006" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltExpectedPeriod" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort6" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0007" onclick="imgbtnSort_Click"/></th>
                        <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort7" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0008" onclick="imgbtnSort_Click"/></th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="iphItemPlaceHolderId" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("CounselCd")%>","<%#Eval("CounselSeq")%>");'>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsCountry" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsIndus" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsUseArea" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsLeaseFee" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsExpectedArea" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsExpectedPeriod" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsInsDt" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TypeA">
		        <colgroup>
			        <col width="40px"/>
			        <col width="220px"/>
			        <col width="80px"/>
			        <col width="100px"/>
			        <col width="70px"/>
			        <col width="70px"/>
			        <col width="100px"/>
			        <col width="60px"/>
			        <col width="80px"/>
		        </colgroup>
                <thead>
                    <tr>
                        <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>                        
                        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0001" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltCountry" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort1" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0002" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltIndus" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort2" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0003" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltUseArea" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort3" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0004" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltLeaseFee" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort4" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0005" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltExpectedArea" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort5" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0006" onclick="imgbtnSort_Click"/></th>
                        <th><asp:Literal ID="ltExpectedPeriod" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort6" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0007" onclick="imgbtnSort_Click"/></th>
                        <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal><asp:ImageButton ID="imgbtnSort7" runat="server" ImageUrl="~/Common/Images/Icon/sortIcon2.gif" CommandArgument="0008" onclick="imgbtnSort_Click"/></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="9" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="clear MrgB10"><span id="spanPageNavi" runat="server" style="width:100%"></span></div>
   	<fieldset class="sh-field1  ">
		<legend>검색</legend>
			<ul class="sf1-ag MrgL220">
				<li>
					<asp:DropDownList ID="ddlKeyCd" runat="server"></asp:DropDownList>
				</li>
				<li>
					<asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
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
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfCounselCd" runat="server"/>
    <asp:HiddenField ID="hfCounselSeq" runat="server"/>
    <asp:TextBox ID="txtHfCounselCd" runat="server" Visible="false"/>
    <asp:HiddenField ID="hfOrderCd" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
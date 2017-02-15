<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OccupantList.aspx.cs" Inherits="KN.Web.Resident.Residence.OccupantList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnMovePage(intPageNo) 
    {
        if (intPageNo == null) 
        {
            intPageNo = 1;
        }
        
        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
        <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
    }

    function fnDetailView(strUserSeq, intRentSeq)
    {
        document.getElementById("<%=hfUserSeq.ClientID%>").value = strUserSeq;
        document.getElementById("<%=hfRentSeq.ClientID%>").value = intRentSeq;
        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
    }
    
    function fnOccupantList()
    {
        var strData = document.getElementById("<%=hfRentCd.ClientID%>").value;
        
        window.open("/Common/RdPopup/RDPopupOccupantList.aspx?Datum0=" + strData, "OccupantList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        return false;
    }
    
    function fnMakeAptRetailBill()
    {
        var strData = document.getElementById("<%=hfRentCd.ClientID%>").value;
        
        window.open("/Common/RdPopup/RDPopupOccupantList.aspx?Datum0=" + strData, "OccupantList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        return false;
    }

    function fnCheckValidate(strAlert)
    {
        var strNm = document.getElementById("<%=txtNm.ClientID%>");
        var strSearchRoomNo = document.getElementById("<%=txtSearchRoomNo.ClientID%>");
        var strStartDt = document.getElementById("<%=txtStartDt.ClientID%>");
        var strEndDt = document.getElementById("<%=txtEndDt.ClientID%>");
            
        if (trim(strNm.value) == "" && trim(strSearchRoomNo.value) == "" && trim(strStartDt.value) == "" && trim(strEndDt.value) == "")
        {       
            alert(strAlert);
            return false;
        }
        
        return true;
    }
</script>
<fieldset class="sh-field2">
	<legend>검색</legend>
	<ul class="sf2-ag MrgL10 ">
	    <li><asp:Literal ID="ltNm" runat="server"></asp:Literal></li>
	    <li><asp:TextBox ID="txtNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>
	    <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
		<li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
		<li><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></li>
		<li><asp:TextBox ID="txtSearchRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
		<li>
		    <asp:TextBox ID="txtStartDt" runat="server" Width="70px"></asp:TextBox>
		    <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
		    <asp:HiddenField ID="hfStartDt" runat="server"/>
		</li>
		<li><span>~</span></li>
		<li>
			<asp:TextBox ID="txtEndDt" runat="server" Width="70px"></asp:TextBox>
			<img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndDt.ClientID%>', '<%=hfEndDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
			<asp:HiddenField ID="hfEndDt" runat="server"/>
		</li>
		<li>
			<div class="Btn-Type4-wp">
				<div class="Btn-Tp4-L">
					<div class="Btn-Tp4-R">
						<div class="Btn-Tp4-M">
							<span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
						</div>
					</div>
				</div>
			</div>
			<div class="Btn-Type4-wp">
				<div class="Btn-Tp4-L">
					<div class="Btn-Tp4-R">
						<div class="Btn-Tp4-M">
							<span><asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
						</div>
					</div>
				</div>
			</div>
		</li>
	</ul>
</fieldset>
<div class="TpAtit1" id="divSearchCond" runat="server">
	<div class="TpAw">
		<span><asp:Literal ID="ltSearchCond" runat="server"></asp:Literal></span>
	</div>
	<div class="TpBw">
		<span><asp:Literal ID="ltSearchTxt" runat="server"></asp:Literal></span>
	</div>
</div>
<asp:ListView ID="lvUserList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
    OnLayoutCreated="lvUserList_LayoutCreated" OnItemDataBound="lvUserList_ItemDataBound" OnItemCreated="lvUserList_ItemCreated">
    <LayoutTemplate>
        <table class="TypeA MrgT10">
	        <thead>
		        <tr>
			        <th class="Fr-line"><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopNm" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopFloor" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopPhone" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopOccuDt" runat="server"></asp:Literal></th>
			        <th class="Ls-line"><asp:Literal ID="ltTopNoPerson" runat="server"></asp:Literal></th>
		        </tr>
	        </thead>
	        <tbody>
	            <tr id="iphItemPlaceHolderID" runat="server"></tr>
	        </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:return fnDetailView('<%#Eval("UserSeq")%>','<%#Eval("RentSeq")%>')">
            <td class="TbTxtCenter"><span><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsNm" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsFloor" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsPhone" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsOccuDt" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsNoPerson" runat="server"></asp:Literal></span></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table class="TypeA MrgT10">
	        <thead>
		        <tr>
			        <th class="Fr-line"><span><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></span></th>
			        <th><asp:Literal ID="ltTopNm" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopFloor" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopPhone" runat="server"></asp:Literal></th>
			        <th><asp:Literal ID="ltTopOccuDt" runat="server"></asp:Literal></th>
			        <th class="Ls-line"><asp:Literal ID="ltTopNoPerson" runat="server"></asp:Literal></th>
		        </tr>
	        </thead>
	        <tbody>
	            <tr>
	                <td colspan="7" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
	            </tr>
	        </tbody>
        </table>
    </EmptyDataTemplate>
</asp:ListView>
<div class="Clear"><span id="spanPageNavi" runat="server" style="width:100%"></span></div>
<div class="Btwps FloatR">
    <div class="Btn-Type3-wp ">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				    <span><asp:LinkButton ID="lnkbtnMakeBill" runat="server" OnClick="lnkbtnMakeBill_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
    <div class="Btn-Type3-wp ">    
        <div class="Btn-Tp3-L">
	        <div class="Btn-Tp3-R">
		        <div class="Btn-Tp3-M">
			        <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" onclick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
		        </div>
	        </div>
        </div>
    </div>
    <div class="Btn-Type3-wp ">
        <div class="Btn-Tp3-L">
	        <div class="Btn-Tp3-R">
		        <div class="Btn-Tp3-M">
			        <span><asp:LinkButton ID="lnkbtnReport" runat="server" OnClientClick="javascript:return fnOccupantList();"></asp:LinkButton></span>
		        </div>
	        </div>
        </div>
    </div>
</div>
<asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
<asp:TextBox ID="txthfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:HiddenField ID="hfRentCd" runat="server"/>
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
<asp:HiddenField ID="hfUserSeq" runat="server"/>
<asp:HiddenField ID="hfRentSeq" runat="server"/>
</asp:Content>
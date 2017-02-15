<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OccupantModify.aspx.cs" Inherits="KN.Web.Resident.Residence.OccupantModify" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                callCalendar();
            }
        } 
        function fnCheckValidate(strtext, strAlert) 
        {
            if (confirm(strtext)) 
            {            
                var strMobileFrontNo = document.getElementById("<%= txtMobileFrontNo.ClientID %>");
                var strMobileMidNo = document.getElementById("<%= txtMobileMidNo.ClientID %>");
                var strMobileRearNo = document.getElementById("<%= txtMobileRearNo.ClientID %>");
                var strTelFrontNo = document.getElementById("<%= txtTelFrontNo.ClientID %>");
                var strTelMidNo = document.getElementById("<%= txtTelMidNo.ClientID %>");
                var strTelRearNo = document.getElementById("<%= txtTelRearNo.ClientID %>");
                var strBirthDt = document.getElementById("<%= txtBirthDt.ClientID %>");
                var strOccupationDt = document.getElementById("<%= txtOccupationDt.ClientID %>");

//                if (trim(strMobileFrontNo.value) == "") {
//                    strMobileFrontNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strMobileMidNo.value) == "") {
//                    strMobileMidNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strMobileRearNo.value) == "") {
//                    strMobileRearNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strTelFrontNo.value) == "") {
//                    strTelFrontNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strTelMidNo.value) == "") {
//                    strTelMidNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strTelRearNo.value) == "") {
//                    strTelRearNo.focus();
//                    alert(strAlert);
//                    return false;
//                }

//                if (trim(strOccupationDt.value) == "") {
//                    strOccupationDt.focus();
//                    alert(strAlert);
//                    return false;
//                }

                return true;
            }
            else 
            {
                return true;
            }
        }
        
        function fnCheckNameValidate(strText)
        {
            var strAddonUser = document.getElementById("<%= txtAddonUser.ClientID %>"); 
            
            if (trim(strAddonUser.value) == "") 
            {
                strAddonUser.focus();
                alert(strText);
                return false;
            }
            
            else
            {
                return true;
            }
        }
        
        function fnCheckCardValidate(strText)
        {
            var strUserNm = document.getElementById('<%=txtUserNm.ClientID%>');
            var strMngCardNo = document.getElementById('<%=txtMngCardNo.ClientID%>');

            if (trim(strUserNm.value) == "")
            {
                strUserNm.focus();
                alert(strText);
                return false;
            }

            if (trim(strMngCardNo.value) == "")
            {
                strMngCardNo.focus();
                alert(strText);
                return false;
            }

            return true;
        }
        function callCalendar() {
            $("#<%=txtBirthDt.ClientID %>").datepicker({
                altField: "#<%=hfBirthDt.ClientID %>"
            });

            $("#<%=txtOccupationDt.ClientID %>").datepicker({
                altField: "#<%=hfOccupationDt.ClientID %>"
            });
        }

        $(document).ready(function () {
            callCalendar();
        });           

    //-->
    </script>
	<div class="TpAtit1">
	    <div class="FloatL">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate" runat="server" Visible="false"></asp:TextBox></div>
        <span class="shf-sel FloatR2">
            <asp:CheckBox ID="chkSameLessor" runat="server" OnCheckedChanged="chkSameLessor_CheckedChanged" AutoPostBack="true"/>
        </span>
	</div>
    <table class="TbCel-Type2-A">
	    <col width="10%"/>
	    <col width="10%"/>
	    <col width="10%"/>
	    <col width="10%"/>
	    <col width="10%"/>
	    <col width="12%"/>
	    <col width="30%"/>
	    <tr>
		    <th><asp:Literal ID="ltFloor" runat="server"></asp:Literal></th>
		    <td colspan="2"><asp:Literal ID="ltInsFloor" runat="server"></asp:Literal></td>
		    <th class="Bd-Lt ectSt"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
		    <td><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
		    <th class="Bd-Lt"><asp:Literal ID="ltNm" runat="server"></asp:Literal></th>
		    <td><asp:TextBox ID="txtNm" runat="server" CssClass="bgType2" MaxLength="255" Width="150px"></asp:TextBox></td>
	    </tr>
	    <tr>
		    <th colspan="2"><asp:Literal ID="ltMobileNo" runat="server"></asp:Literal></th>
		    <td colspan="3">
		        <asp:TextBox ID="txtMobileFrontNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox><span>&ndash;</span><asp:TextBox ID="txtMobileMidNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox><span>&ndash;</span><asp:TextBox ID="txtMobileRearNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox>
		    </td>
		    <th class="Bd-Lt"><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></th>
		    <td>
		        <asp:TextBox ID="txtTelFrontNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox><span>&ndash;</span><asp:TextBox ID="txtTelMidNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox><span>&ndash;</span><asp:TextBox ID="txtTelRearNo" runat="server" CssClass="bgType2" MaxLength="4" Width="40px"></asp:TextBox>
		    </td>
	    </tr>
	    <tr>
		    <th colspan="2"><asp:Literal ID="ltBirthDt" runat="server"></asp:Literal></th>
		    <td colspan="3">
		        <asp:TextBox ID="txtBirthDt" runat="server" CssClass="bgType2 cssCalendar" Width="80px"></asp:TextBox>
		        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtBirthDt.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
		        <asp:HiddenField ID="hfBirthDt" runat="server"/>
		    </td>
		    <th class="Bd-Lt"><asp:Literal ID="ltGender" runat="server"></asp:Literal></th>
		    <td>
		        <asp:RadioButtonList ID="rdoGender" runat="server" RepeatLayout="Flow" CssClass="Type1-rdo"></asp:RadioButtonList>
		    </td>
	    </tr>
	    <tr>
		    <th colspan="2"><asp:Literal ID="ltOccupationDt" runat="server"></asp:Literal></th>
		    <td colspan="5">
		        <asp:TextBox ID="txtOccupationDt" runat="server" CssClass="bgType2 cssCalendar" Width="80px"></asp:TextBox>
		        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtOccupationDt.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
		        <asp:HiddenField ID="hfOccupationDt" runat="server"/>
		    </td>
	    </tr>
	    <tr>
		    <th colspan="2"><asp:Literal ID="ltTaxCd" runat="server"></asp:Literal></th>
		    <td colspan="5"><asp:TextBox ID="txtTaxCd" runat="server" MaxLength="16" Width="150px"></asp:TextBox></td>
	    </tr>
	    <tr>
		    <th colspan="2" rowspan="2" style="height:52px" class="Bd-bt"><asp:Literal ID="ltTaxAddr" runat="server"></asp:Literal> (For Tax)</th>
		    <td colspan="5" style="height:26px"><asp:TextBox ID="txtTaxAddr" runat="server" MaxLength="255" Width="620px"></asp:TextBox></td>
	    </tr>
	    <tr>
		    <td colspan="5" style="height:26px" class="Bd-bt"><asp:TextBox ID="txtTaxDetAddr" runat="server" MaxLength="255" Width="620px"></asp:TextBox></td>
	    </tr>
		<tr id="KsystemCode" runat="server">
			<th colspan="2"><asp:Literal ID="Literal2" runat="server" Text="Ksystem Code"></asp:Literal></th>
			<td colspan="3" style="height:26px"><asp:TextBox ID="txtKsystemCode" runat="server" MaxLength="255" ></asp:TextBox></td>
			<td class="Bd-bt" colspan="2"><asp:Literal ID="Literal3" runat="server" Text="This code is very important for mapping betweent AMS and K-system(Please ask Ms.Van to input this code!)"></asp:Literal></td>
		</tr>
		
    </table>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltMngAddon" runat="server"></asp:Literal>
    </div>
	<table class="TbCel-Type4-A">
		<col width="29%"/>
        <col width="29%"/>
        <col width="29%"/>
		<col width=""/>
		<tr>
			<th><asp:Literal ID="ltAddonUser" runat="server"></asp:Literal></th>
			<th ><asp:Literal ID="ltAddonRelation" runat="server"></asp:Literal></th>
			<th ><asp:Literal ID="ltAddonoSex" runat="server"></asp:Literal></th>
			<th >&nbsp;</th>
		</tr>
    </table>
    <div style="overflow-y:scroll;height:60px;width:820px;">
        <asp:ListView ID="lvMngAddon" runat="server" 
            ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMngAddon_ItemDataBound" 
            OnItemCreated="lvMngAddon_ItemCreated" 
            OnItemDeleting="lvMngAddon_ItemDeleting" 
            OnItemUpdating="lvMngAddon_ItemUpdating" >
            <LayoutTemplate>
                <table class="TbCel-Type2-A iw800">
		            <col width="250px"/>
		            <col width="250px"/>
		            <col width="250px"/>
		            <col width=""/>
		            <tr runat="server" id="iphItemPlaceHolderID"></tr>
	            </table>
            </LayoutTemplate>
            <ItemTemplate>
	            <tr>
		            <td class="TbTxtCenter Ptb5">
                        <asp:TextBox ID="txtAddonUser" runat="server" CssClass="bgType2" Width="100px" MaxLength="100"></asp:TextBox>
                        <asp:TextBox ID="txtUserDetSeq" runat="server" Visible="false"></asp:TextBox>
		            </td>
		            <td class="TbTxtCenter Ptb5">
                        <asp:DropDownList ID="ddlAddonUser" runat="server"></asp:DropDownList>
		            </td>
		            <td class="TbTxtCenter Ptb5">
		                <asp:RadioButtonList ID="rdoSex" runat="server" RepeatLayout="Flow"></asp:RadioButtonList>
                    </td>
		            <td class="TbTxtCenter Ptb5">
		                <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                        <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
		            </td>
	            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type2-A iw800">
		            <col width="240px;"/>
		            <col width="240px;"/>
		            <col width="220px;"/>
		            <col width=""/>
		            <tr>
		                <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
		            </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
	<table class="TbCel-Type2-A">
        <col width="250px"/>
        <col width="250px"/>
        <col width="250px"/>
        <col width=""/>
        <tr>
	        <td class="TbTxtCenter">
                <asp:TextBox ID="txtAddonUser" runat="server" CssClass="bgType2" Width="100px" MaxLength="100"></asp:TextBox>
            </td>
            <td class="TbTxtCenter">
	            <asp:DropDownList ID="ddlAddonUser" runat="server"></asp:DropDownList>
	        </td>
	        <td class="TbTxtCenter">
	            <asp:RadioButtonList ID="rdoSex" runat="server" RepeatLayout="Flow" CssClass="Type1-rdo"></asp:RadioButtonList>
            </td>
            <td class="TbTxtCenter">
	            <span><asp:ImageButton ID="imgbtnMngAddonInsert" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnMngAddonInsert_Click"/></span>
	        </td>
        </tr>
    </table>
	<div class="Tb-Tp-tit">
	    <asp:Literal ID="ltMngCard" runat="server"></asp:Literal>
	</div>
	<table class="TbCel-Type4-A">
		<col width="29%"/>
        <col width="29%"/>
        <col width="29%"/>
        <col width=""/>
		<tr>
			<th><asp:Literal ID="ltCardUser" runat="server"></asp:Literal></th>
			<th><asp:Literal ID="ltRelation" runat="server"></asp:Literal></th>
			<th><asp:Literal ID="ltMngCardNo" runat="server"></asp:Literal></th>
			<th>&nbsp;</th>
		</tr>
    </table>
    <div style="overflow-y:scroll;height:60px;width:820px;">
        <asp:ListView ID="lvMngCardList" runat="server" 
            ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMngCardList_ItemDataBound" 
            OnItemCreated="lvMngCardList_ItemCreated" 
            OnItemDeleting="lvMngCardList_ItemDeleting" 
            OnItemUpdating="lvMngCardList_ItemUpdating"       >
            <LayoutTemplate>
                <table class="TbCel-Type4-A iw800">
		            <col width="250px;"/>
		            <col width="220px"/>
                      <col width="280px"/>
		            <col width=""/>
		            <tr runat="server" id="iphItemPlaceHolderID"></tr>
	            </table>
            </LayoutTemplate>
            <ItemTemplate>
	            <tr>
		            <td class="TbTxtCenter Ptb5">
                        <asp:TextBox ID="txtUserNm" runat="server" CssClass="bgType2" Width="100px" MaxLength="100"></asp:TextBox>
                        <asp:TextBox ID="txtUserDetSeq" runat="server" Visible="false"></asp:TextBox>
		            </td>
		            <td class="TbTxtCenter Ptb5">
                        <asp:DropDownList ID="ddlUser" runat="server"></asp:DropDownList>
		            </td>
		            <td class="TbTxtCenter Ptb5">
		                <asp:TextBox ID="txtMngCardNo" runat="server" MaxLength="20" Width="100px"></asp:TextBox>
                    </td>
		            <td class="TbTxtCenter Ptb5">
		                <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                        <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
		            </td>
	            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type4-A iw800">
		            <col width="250px"/>
                    <col width="250px"/>
                    <col width="250px"/>
                    <col width=""/>
		            <tr>
		                <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
		            </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
	<table class="TbCel-Type4-A">
        <col width="250px"/>
        <col width="220px"/>
        <col width="280px"/>
        <col width=""/>
        <tr>
	        <td class="TbTxtCenter">
                <asp:TextBox ID="txtUserNm" runat="server" CssClass="bgType2" Width="100px" MaxLength="100"></asp:TextBox>
            </td>
            <td class="TbTxtCenter">
	            <asp:DropDownList ID="ddlUser" runat="server"></asp:DropDownList>
	        </td>
	        <td class="TbTxtCenter">
	            <asp:TextBox ID="txtMngCardNo" runat="server" CssClass="bgType2" MaxLength="20" Width="100px"></asp:TextBox>
            </td>
            <td class="TbTxtCenter">
	            <span><asp:ImageButton ID="imgbtnMngCardListInsert" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnMngCardListInsert_Click"/></span>
	        </td>
        </tr>
    </table>
    <div class="Btwps FloatR">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                        <span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                        <span><asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfTmpSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFloor" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "") 
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="UtilFeeReceiptForTower.aspx.cs" Inherits="KN.Web.Management.Remote.UtilFeeReceiptForTower" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            
            return false;
        }
    }

    function fnSelectCheckValidate(strText)
    {
        var strRentCd = document.getElementById("<%=ddlInsRentCd.ClientID%>");
        var strRoomNo = document.getElementById("<%=txtInsRoomNo.ClientID%>");

//        if (strRentCd.value == "0000")
//        {
//            alert(strText);
//            document.getElementById("<%=ddlInsRentCd.ClientID%>").focus();
//            return false;
//        }

        if (trim(strRoomNo.value) == "")
        {
            alert(strText);
            document.getElementById("<%=txtInsRoomNo.ClientID%>").focus();
            return false;
        }

        return true;
    }
    
//    function fnDetailView(strData2, strData3, strData4)
//    {
//        // Datum1 : 입주자번호
//        // Datum2 : 섹션코드
//        // Datum3 : 방번호
//        // Datum4 : 거주년월(YYYYMM)
//        Page.GetPostBackEventReference(imgbtnDetailview)

//        window.open("/Common/RDPopup/RDPopupEnergyDetail.aspx?Datum1=&Datum2=" + strData2 + "&Datum3=" + strData3 + "&Datum4=" + strData4, "EnergyDetail", "status=no, resizable=no, width=730, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
//        
//        return false;
//    }

    function fnChangedList(intSeq)
    {
        document.getElementById("<%=hfSelectedLine.ClientID%>").value = intSeq-1;
        document.getElementById("<%=imgbtnListChange.ClientID%>").click();

        return false;
    }

    function fnChangedItem()
    {
        document.getElementById("<%=imgbtnItemChange.ClientID%>").click();

        return false;
    }
    
    function  fnCheckValidate(strTxt)
    {
        var strPaymentCd = document.getElementById("<%=ddlPaymentCd.ClientID%>");
        var strTransfer = document.getElementById("<%=ddlTransfer.ClientID%>");
        
        if (strPaymentCd.value == "0000")
        {
            document.getElementById("<%=ddlPaymentCd.ClientID%>").focus();
            alert(strTxt);

            return false;
        }
        else if (strPaymentCd.value == "0003")
        {
            if (strTransfer.value == "")
            {
                document.getElementById("<%=ddlTransfer.ClientID%>").focus();
                alert(strTxt);
                
                return false;
            }
        }

        return true;
    }
    
    function fnCheckProcess(strText)
    {
        if (confirm(strText))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    function fnItemChange()
    {
        document.getElementById("<%=imgbtnChange.ClientID%>").click();
        
        return false;
    }
    //-->
    </script>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport" />
        </Triggers>
        <ContentTemplate>
            <div class="TpAtit1">
                <div class="FloatR">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate" runat="server" Visible="false"></asp:TextBox></div>
            </div>
            <fieldset class="sh-field2 MrgB10">
                <legend>출력물</legend>
                <ul class="sf2-ag MrgL10">
                    <li><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList></li>
                    <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
            <fieldset class="sh-field2 MrgB10">
                <legend>검색</legend>
                <ul class="sf2-ag MrgL30">
                    <li><asp:DropDownList ID="ddlInsRentCd" runat="server"></asp:DropDownList></li>
                    <li><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></li>
                    <li><asp:TextBox ID="txtInsRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
                    <li><asp:Literal ID="ltInsPaidCd" runat="server"></asp:Literal></li>
                    <li><asp:DropDownList ID="ddlPaidCd" runat="server"></asp:DropDownList></li>
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
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upTitle" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnListChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnItemChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />
        </Triggers>
        <ContentTemplate>
            <table class="TypeA" border="0">
                <colgroup>
                    <col width="20" />
                    <col width="70" />
                    <col width="230" />
                    <col width="70" />
                    <col width="150" />
                    <col width="150" />
                    <col width="150" />
                    <thead>
                        <tr>
                            <th align="center" class="Fr-line"><asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" Style="text-align: center" /></th>
                            <th class="TbTxtCenter"><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
                            <th class="TbTxtCenter"><asp:Literal ID="ltTopName" runat="server"></asp:Literal></th>
                            <th class="TbTxtCenter"><asp:Literal ID="ltTopApplyDt" runat="server"></asp:Literal></th>
                            <th class="TbTxtCenter"><asp:Literal ID="ltTopElecFee" runat="server"></asp:Literal></th>
                            <th class="TbTxtCenter"><asp:Literal ID="ltTopWatFee" runat="server"></asp:Literal></th>
                            <th align="center" class="Ls-line"><asp:Literal ID="ltTopGasFee" runat="server"></asp:Literal></th>
                        </tr>
                    </thead>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 200px; width: 840px;">
        <asp:UpdatePanel ID="upResultList" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgbtnListChange" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgbtnItemChange" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgbtnChange" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:ListView ID="lvUtilFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvUtilFeeList_ItemCreated" OnItemDataBound="lvUtilFeeList_ItemDataBound" OnItemDeleting="lvRentItemList_ItemDeleting" OnItemUpdating="lvRentItemList_ItemUpdating" OnItemEditing="lvRentItemList_ItemEditing">
                    <LayoutTemplate>
                        <table class="TypeA" border="0">
                            <col width="20" />
                            <col width="70" />
                            <col width="230" />
                            <col width="70" />
                            <col width="40" />
                            <col width="110" />
                            <col width="40" />
                            <col width="110" />
                            <col width="40" />
                            <col width="110" />
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server"></tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="TbTxtCenter"><asp:CheckBox ID="chkLineList" runat="server"></asp:CheckBox></td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltName" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfName" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltEnergyMonth" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfEnergyMonth" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfEnterMonth" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:CheckBox ID="chkElecItem" runat="server" onclick="javascript:return fnChangedItem();"></asp:CheckBox>
                            </td>
                            <td class="TbTxtLeft">
                                <asp:TextBox ID="txtElecFee" runat="server" Width="70" onchange="javascript:return fnItemChange();"></asp:TextBox>
                                <asp:TextBox ID="txtHfElecFee" runat="server" Visible="false"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnElecCheck" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/check.gif" />
                            </td>
                            <td class="TbTxtCenter">
                                <asp:CheckBox ID="chkWatItem" runat="server" onclick="javascript:return fnChangedItem();"></asp:CheckBox>
                            </td>
                            <td class="TbTxtLeft">
                                <asp:TextBox ID="txtWatFee" runat="server" Width="70" onchange="javascript:return fnItemChange();"></asp:TextBox>
                                <asp:TextBox ID="txtHfWatFee" runat="server" Visible="false"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnWatCheck" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/check.gif" />
                            </td>
                            <td class="TbTxtCenter">
                                <asp:CheckBox ID="chkGasItem" runat="server" onclick="javascript:return fnChangedItem();"></asp:CheckBox>
                            </td>
                            <td class="TbTxtLeft">
                                <asp:TextBox ID="txtGasFee" runat="server" Width="70" onchange="javascript:return fnItemChange();"></asp:TextBox>
                                <asp:TextBox ID="txtHfGasFee" runat="server" Visible="false"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnGasCheck" CommandName="Edit" runat="server" ImageUrl="~/Common/Images/Icon/check.gif" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA">
                            <tbody>
                                <tr>
                                    <td colspan="11" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1 iw840">
                <colgroup>
                    <col width="110" />
                    <col width="170" />
                    <col width="110" />
                    <col width="170" />
                    <col width="110" />
                    <col width="170" />
                    <tr>
                        <th align="center"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
                        <td colspan="2">
                            <asp:Literal ID="ltRegRoomNo" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegRentCd" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfRegRoomNo" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfDongToDollar" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtHfFloorNo" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <th align="center"><asp:Literal ID="ltEnterMonth" runat="server"></asp:Literal></th>
                        <td colspan="2">
                            <asp:Literal ID="ltRegEnterMonth" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegEnterMonth" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="center"><asp:Literal ID="ltElecFee" runat="server"></asp:Literal></th>
                        <td>
                            <asp:Literal ID="ltRegElecFee" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegElecFee" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <th align="center"><asp:Literal ID="ltWatFee" runat="server"></asp:Literal></th>
                        <td>
                            <asp:Literal ID="ltRegWatFee" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegWatFee" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <th align="center"><asp:Literal ID="ltGasFee" runat="server"></asp:Literal></th>
                        <td>
                            <asp:Literal ID="ltRegGasFee" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegGasFee" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTotalFee" runat="server"></asp:Literal></th>
                        <td>
                            <asp:Literal ID="ltRegTotalFee" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRegTotalFee" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <th>
                            <asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal>
                        </th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlPaymentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentCd_SelectedIndexChanged"></asp:DropDownList>
                            <asp:DropDownList ID="ddlTransfer" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </colgroup>
            </table>
            <asp:ImageButton ID="imgbtnListChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnListChange_Click" />
            <asp:ImageButton ID="imgbtnItemChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnItemChange_Click" />
            <asp:HiddenField ID="hfSelectedLine" runat="server" />
            <asp:ImageButton ID="imgbtnChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnChange_Click" />
            <div class="Btn-Type3-wp">
                <div class="Btn-Tp3-L">
                    <div class="Btn-Tp3-R">
                        <div class="Btn-Tp3-M">
                            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
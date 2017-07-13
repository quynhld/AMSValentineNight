<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="TowerSalesView.aspx.cs" Inherits="KN.Web.Resident.Contract.TowerSalesView"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--//
    function fnChangePopup(strNowRate, strReturnBox1, strReturnBox2)
    {
        window.open("<%=Master.PAGE_POPUP2%>?NowRate=" + strNowRate + "&ReturnBox1=" + strReturnBox1 + "&ReturnBox2=" + strReturnBox2, 'TmpExchange', 'status=no, resizable=no, width=320, height=80, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');
        return false;
    }
    
    function fnShowModal(strText1, strData, strText2, strValue)
    {
        <%=Page.GetPostBackEventReference(imgbtnDel)%>
        var strReturn = window.showModalDialog("<%=Master.PAGE_POPUP1%>?" + strText1 + "=" + strData + "&" + strText2 + "=" + strValue + "", "window", "dialogWidth:500px;dialogHeight:320px;scroll=no;menubar=no;toolbar=no;location=no;status=no;resizable=no;");
        
        if (strReturn == "DELETE")
        {
            fnChangeList();
        }
        
        return false;
    }
    
    function fnChangeList()
    {
        <%=Page.GetPostBackEventReference(imgbtnMove)%>
    }
    
    function fnCheckItems(strData1, strData2, strData3, strData4, strData5, strData6, strData7, strText)
    {
        var strDepositExpDt = document.getElementById(strData1);
        var strHfDepositExpDt = document.getElementById(strData2);
        var strDepositExpAmt = document.getElementById(strData3);
        var strDepositPayDt = document.getElementById(strData4);
        var strHfDepositPayDt = document.getElementById(strData5);
        var strOldDepositPayAmt = document.getElementById(strData6);
        var strNewDepositPayAmt = document.getElementById(strData7);

        if (trim(strHfDepositExpDt.value) == "")
        {
            alert(strText);
            strDepositExpDt.focus();
            
            return false;
        }
        
        if (trim(strDepositExpAmt.value) == "")
        {
            alert(strText);
            strDepositExpAmt.focus();
            
            return false;
        }
        else
        {
            if (trim(strOldDepositPayAmt.value) != trim(strNewDepositPayAmt.value))
            {
                if (trim(strNewDepositPayAmt.value) != "")
                {
                    if (Number(trim(strNewDepositPayAmt.value)) > 0)
                    {
                        if (trim(strDepositExpAmt.value) != trim(strNewDepositPayAmt.value))
                        {
                            document.getElementById(strData7).value = strOldDepositPayAmt.value;
                            alert(strText);
                            strNewDepositPayAmt.focus();
                            
                            return false;
                        }
                    }
                }
            }
        }
        
        if (strNewDepositPayAmt.value != "")
        {
            if (strHfDepositPayDt.value == "")
            {
                alert(strText);
                strDepositPayDt.focus();
                
                return false;
            }
        }
            
        return true;
    }

    function fnCheckRentalFee(strData1, strData2, strData3, strData4, strData5, strText)
    {
        var strRentalFeeStartDt = document.getElementById(strData1);
        var strHfRentalFeeStartDt = document.getElementById(strData2);
        var strRentalFeeEndDt = document.getElementById(strData3);
        var strHfRentalFeeEndDt = document.getElementById(strData4);
        var strRentalFeeExpAmt = document.getElementById(strData5);

        if (trim(strHfRentalFeeStartDt.value) == "")
        {
            alert(strText);
            strRentalFeeStartDt.focus();
            
            return false;
        }
        
        if (trim(strHfRentalFeeEndDt.value) == "")
        {
            alert(strText);
            strRentalFeeEndDt.focus();
            
            return false;
        }
        
        if (trim(strRentalFeeExpAmt.value) == "")
        {
            alert(strText);
            strRentalFeeExpAmt.focus();
            
            return false;
        }
            
        return true;
    }
//-->
    </script>
    <style>
        .cont-Mid .cont-wp
        {
            width: 855px;
        }
    </style>
    <asp:UpdatePanel ID="upBasicInfo" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltBasicInfo" runat="server"></asp:Literal>
                (<asp:Literal ID="ltIncharge" runat="server"></asp:Literal>
                :
                <asp:Literal ID="ltContInchage" runat="server"></asp:Literal>)
            </div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                Contract Type
                            </th>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbContractType" runat="server" RepeatDirection="Horizontal"
                                    Enabled="False">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD" Selected="True">USD</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSpecialContract" runat="server" Text="Is Special Contract" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadNm" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlPersonal" runat="server">
                                </asp:DropDownList>
                                <asp:Literal ID="ltContLandloadNm" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltContNo" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlContStep" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTerm" runat="server">
                                </asp:DropDownList>
                                <asp:Literal ID="ltContContNo" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfContContNo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal6" runat="server" Text="Industry"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltIndustry" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="Literal10" runat="server" Text="Nationality"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltNat" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltLandloadAddr" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3" style="height: 20px;">
                                <asp:Literal ID="ltContLandloadAddr" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 20px;">
                                <asp:Literal ID="ltContLandloadDetAddr" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadCorpCert" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadCorpCert" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltIssueDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContIssueDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadTelNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadTel" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadMobileNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadMobile" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadFAX" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadFAX" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadEmail" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadEmail" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltLandloadRepNm" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadRepNm" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltLandloadTaxCd" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContLandloadTaxCd" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th rowspan="2">
                                <asp:Literal ID="ltRentAddr" runat="server"></asp:Literal>&nbsp;(For Tax)
                            </th>
                            <td colspan="3" style="height: 20px;">
                                <asp:Literal ID="ltContRentAddr" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 20px;">
                                <asp:Literal ID="ltContRentDetAddr" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRentTerm" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltOTLAgreeDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContOTLAgreeDt" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltRentAgreeDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContRentAgreeDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRentStartDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContRentStartDt" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltRentEndDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContRentEndDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltFreeRentMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContFeeRentMonth" runat="server"></asp:Literal>
                                <asp:Literal ID="ltMonthUnit" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="ltTermMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContTermMonth" runat="server"></asp:Literal>
                                <asp:Literal ID="ltMonth" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal8" runat="server" Text="Renewal Time"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltRenewDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th class="lebd">
                                <asp:Literal ID="ltHandOverDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContHandOverDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRoomInfo" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltFloor" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContFloor" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContRoomNo" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRentLeasingArea" runat="server"></asp:Literal>
                            </th>
                            <td >
                                <asp:Literal ID="ltContRentLeasingArea" runat="server"></asp:Literal>&nbsp;㎡
                            </td>
                                                        <th>
                                <asp:Literal ID="ltAdditionalRentArea" runat="server" Text="Addition Area"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="ltContAdditionalRentArea" runat="server"></asp:Literal>&nbsp;㎡
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltExchangeRateTitle" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                </colgroup>
                <tbody>
                    <tr>
                        <th>
                            <asp:Literal ID="ltExchangeRate" runat="server"></asp:Literal>
                        </th>
                        <td colspan="2">
                            <asp:TextBox ID="txtFC" runat="server" AutoPostBack="False" CssClass="bgType2" MaxLength="10"
                                ReadOnly="True"></asp:TextBox>
                            <asp:CheckBox ID="chkCC" runat="server" Text="Current Currency" Checked="True" AutoPostBack="False"
                                Enabled="False" />
                        </td>
                        <th>
                            <asp:Label ID="lblFloat" runat="server" Text="Inflation(%)"></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtFloation" runat="server" AutoPostBack="true" CssClass="bgType2"
                                MaxLength="3" Width="67px">0.1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Payment Type
                        </th>
                        <td colspan="2" style="width: 157px">
                            <asp:DropDownList ID="ddlPaymentType" runat="server" Enabled="False">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="VND">VND</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th>
                            CPI(%)
                        </th>
                        <td>
                            <asp:TextBox ID="txtCPI" runat="server" AutoPostBack="true" MaxLength="3" Width="67px">0.1</asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltRetalFee" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr style="display: none">
                            <th>
                            </th>
                            <td colspan="3">
                                1 Dollar :
                                <asp:Literal ID="ltContExchangeRate" runat="server"></asp:Literal>
                                <asp:Literal ID="ltExchangeUnit" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfExchangeRate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPayStartYYYYMM" runat="server" Text="Current Using Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContPayStartYYYYMM" runat="server"></asp:Literal>
                                <asp:Literal ID="ltPayStartYYYYMMUnit" runat="server" Visible="False"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltPayTermMonth" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContPayTermMonth" runat="server"></asp:Literal>
                                <asp:Literal ID="ltPayTermMonthUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>
                                <asp:Literal ID="ltPayDay" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="ltContPayDay" runat="server"></asp:Literal>
                                <asp:Literal ID="ltPayDayUnit" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Current Pay Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltRSPayDt" runat="server"></asp:Literal>
                            </td>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Pay Cycle Type"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlRPaymentCycle" runat="server" AutoPostBack="False" Enabled="False">
                                    <asp:ListItem Text="B" Value="M">By Monthly</asp:ListItem>
                                    <asp:ListItem Text="O" Value="Q">Make Round Monthly</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal4" runat="server" Text="Isue Date Type"></asp:Literal>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlRIsueDateType" runat="server" AutoPostBack="False" Enabled="False">
                                    <asp:ListItem Value="E">End Of month</asp:ListItem>
                                    <asp:ListItem Value="A">By Perior Date</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Literal ID="Literal5" runat="server" Text="Isue Date Adjust"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltRIsueAdjustDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <!--// 임대료 (총임대료) //-->
                        <tr>
                            <th>
                                <asp:Literal ID="ltSREndDate" runat="server" Text="Special End Date"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltSREndDateV" runat="server"></asp:Literal>
                            </td>
                            <td colspan="2">
                                <asp:Literal ID="ltExplain" runat="server" Text="If you apply this date period using will be Current Using Date ~ Special End Date. After make debit this date will auto reset"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--// 임대료 (월간 임대료) //-->
    <asp:UpdatePanel ID="upRentalFee" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="mvRentFee" runat="server" ActiveViewIndex="0">
                <asp:View runat="server" ID="vGeneal">
                    <table class="TbCel-Type4-A">
                        <colgroup>
                            <col width="185px" />
                            <col width="185px" />
                            <col width="185px" />
                            <col width="185px" />
                            <col width="80px" />
                            <tbody>
                                <tr>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeStartDt" runat="server"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeEndDt" runat="server"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeRate" runat="server" Visible="False"></asp:Literal>
                                        <asp:Literal ID="Literal2" runat="server" Text="VND"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                        <asp:Literal ID="ltTopRentalFeeAmt" runat="server"></asp:Literal>
                                    </th>
                                    <th align="center" class="P0">
                                    </th>
                                </tr>
                            </tbody>
                        </colgroup>
                    </table>
                    <asp:ListView ID="lvRentalFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                        OnItemDataBound="lvRentalFeeList_ItemDataBound" OnItemCreated="lvRentalFeeList_ItemCreated"
                        OnItemUpdating="lvRentalFeeList_ItemUpdating" OnItemDeleting="lvRentalFeeList_ItemDeleting">
                        <LayoutTemplate>
                            <table class="TbCel-Type4-A">
                                <col width="185px" />
                                <col width="185px" />
                                <col width="185px" />
                                <col width="185px" />
                                <col width="80px" />
                                <tbody>
                                    <tr id="iphItemPlaceHolderID" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="P0">
                                    <asp:TextBox ID="txtRentalFeeStartDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                        Width="70" ReadOnly="true"></asp:TextBox>
                                    <asp:Literal ID="ltRentStartCalendar" runat="server" Visible="False"></asp:Literal>
                                    <asp:HiddenField ID="hfRentalFeeStartDt" runat="server" />
                                    <asp:TextBox ID="txtContractNo" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtRentFeeSeq" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td align="center" class="P0">
                                    <asp:TextBox ID="txtRentalFeeEndDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                        Width="70" ReadOnly="true"></asp:TextBox>
                                    <asp:Literal ID="ltRentEndCalendar" runat="server" Visible="False"></asp:Literal>
                                    <asp:HiddenField ID="hfRentalFeeEndDt" runat="server" />
                                </td>
                                <td align="center" class="P0">
                                    <asp:TextBox ID="txtRentalFeeExcRate" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                                    <asp:Literal ID="ltRentalFeeExcRateUnit" runat="server"></asp:Literal>
                                </td>
                                <td align="center" class="P0">
                                    <asp:TextBox ID="txtRentalFeeExpAmt" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                                    <asp:Literal ID="ltRentalFeeAmtUnit" runat="server"></asp:Literal>
                                </td>
                                <td align="center" class="P0">
                                    <span style="display: none">
                                        <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                                    <span style="display: none">
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="TbCel-Type4-A">
                                <tbody>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </asp:View>
                <asp:View runat="server" ID="vPodium">
                    <table cellspacing="0" class="TbCel-Type2-A">
                        <colgroup>
                            <col width="147px" />
                            <col width="178px" />
                            <col width="147px" />
                            <col width="178px" />
                            <tbody>
                                <tr>
                                    <th>
                                        <asp:Literal ID="ltMinimumIncome" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:Literal ID="ltContMinimumIncome" runat="server"></asp:Literal>
                                    </td>
                                    <th>
                                        <asp:Literal ID="ltApplyRate" runat="server"></asp:Literal>
                                    </th>
                                    <td>
                                        <asp:Literal ID="ltContApplyRate" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </colgroup>
                    </table>
                </asp:View>
            </asp:MultiView>
            <asp:TextBox ID="txtHfRentFeeTmpSeq" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfRentFeeSeq" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Tb-Tp-tit">
        Fit Out Management Fee
        <div style="float: right" id="chkUsingMnFee" runat="server">
            <asp:CheckBox ID="isApplyFeeMn" runat="server" Enabled="False" Checked="True" />Apply
            Fit Out Management
        </div>
        <asp:HiddenField ID="hfIsApplyFeeMn" runat="server" Value="N" />
    </div>
    <div id="ListFitOutFee" runat="server">
        <table class="TbCel-Type4-A">
            <colgroup>
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="80px" />
                <tbody>
                    <tr>
                        <th align="center" class="P0">
                            Apply Start Date
                        </th>
                        <th align="center" class="P0">
                            Appl End Date
                        </th>
                        <th align="center" class="P0">
                            VND
                        </th>
                        <th align="center" class="P0">
                            USD
                        </th>
                        <th align="center" class="P0">
                        </th>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <table cellspacing="0" class="TbCel-Type2-A" id="tblListFitFee">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
                <tbody>
                    <div id="diplayFitOutFee" runat="server">
                    </div>
                </tbody>
            </colgroup>
        </table>
    </div>
    <div id="lineRow" runat="server" class="lineRow">
    </div>
    <%--<asp:UpdatePanel ID="upMngFee" runat="server">
    <Triggers></Triggers>
    <ContentTemplate>--%>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltMngFee" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="178px" />
            <col width="147px" />
            <col width="178px" />
            <tbody>
                <tr>
                    <th>
                        Current Using Date
                    </th>
                    <td>
                        <asp:Literal ID="ltMCurrentUsingDt" runat="server"></asp:Literal>
                    </td>
                    <th>
                        Payment Cycle
                    </th>
                    <td>
                        <asp:Literal ID="txtMPayCycle" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        Payment Cycle Type
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMPaymentType" runat="server" Enabled="False">
                            <asp:ListItem Text="B" Value="M">By Monthly</asp:ListItem>
                            <asp:ListItem Text="O" Value="Q">Make Round Monthy</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="Literal7" runat="server" Text="Current Pay Date"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltMCurrentPayDt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        Isue Date Type
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMIsueDateType" runat="server" Width="110px" Enabled="False">
                            <asp:ListItem Value="E">End Of month</asp:ListItem>
                            <asp:ListItem Value="A">By Perior Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        Isue Date Adjust
                    </th>
                    <td>
                        <asp:Literal ID="ltMIsueAdjustDt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSMEndDate" runat="server" Text="Special End Date"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltSMEndDateV" runat="server"></asp:Literal>
                    </td>
                    <td colspan="2">
                        <asp:Literal ID="Literal9" runat="server" Text="If you apply this date period using will be Current Using Date ~ Special End Date. After make debit this date will auto reset"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <div id="ListMngFee" runat="server">
        <table class="TbCel-Type4-A">
            <colgroup>
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="80px" />
                <tbody>
                    <tr>
                        <th align="center" class="P0">
                            Apply Start Date
                        </th>
                        <th align="center" class="P0">
                            Appl End Date
                        </th>
                        <th align="center" class="P0">
                            VND
                        </th>
                        <th align="center" class="P0">
                            USD
                        </th>
                        <th align="center" class="P0">
                        </th>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <table cellspacing="0" class="TbCel-Type2-A" id="Table1">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
                <tbody>
                    <div id="displayMngFee" runat="server">
                    </div>
                </tbody>
            </colgroup>
        </table>
    </div>
    <div id="lineRow1" runat="server" class="lineRow">
    <!-- quynhld modify addition area fee -->
    </div>
        <div class="Tb-Tp-tit">
        <asp:Literal ID="Literal12" Text="Addition Area Fee" runat="server"></asp:Literal></div>
        <div id="lstAdditionAreaFee" runat="server">
        <table class="TbCel-Type4-A">
            <colgroup>
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="185px" />
                <col width="80px" />
                <tbody>
                    <tr>
                        <th align="center" class="P0">
                            Apply Start Date
                        </th>
                        <th align="center" class="P0">
                            Appl End Date
                        </th>
                        <th align="center" class="P0">
                            VND
                        </th>
                        <th align="center" class="P0">
                            USD
                        </th>
                        <th align="center" class="P0">
                        </th>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <table cellspacing="0" class="TbCel-Type2-A" id="Table2">
            <colgroup>
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="185px">
                <col width="80px">
                <tbody>
                    <div id="divDisplayAdditionFee" runat="server">
                    </div>
                </tbody>
            </colgroup>
        </table>
    </div>
    <!-- end modify -->
    <div id="Div3" runat="server" class="lineRow">
    </div>
    <table cellspacing="0" class="TbCel-Type2-A" style="display: none">
        <colgroup>
            <col width="147px" />
            <col width="178px" />
            <col width="147px" />
            <col width="178px" />
            <tbody>
                <tr style="display: none">
                    <th>
                        <asp:Literal ID="ltInitMMMngDay" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContInitMMMngDay" runat="server"></asp:Literal>
                        <asp:Literal ID="ltInitMMMngDayUnit" runat="server"></asp:Literal>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltInitMMMngDt" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContInitMMMngDt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltInitPerMMMngVND" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContInitPerMMMngVND" runat="server"></asp:Literal>
                        <asp:Literal ID="ltInitPerMMMngVNDUnit" runat="server"></asp:Literal>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltInitPerMMMngUSD" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContInitPerMMMngUSD" runat="server"></asp:Literal>
                        <asp:Literal ID="ltInitPerMMMngUSDUnit" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltPerMMMngVND" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContPerMMMngVND" runat="server"></asp:Literal>
                        <asp:Literal ID="ltPerMMMngVNDNoUnit" runat="server"></asp:Literal>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltPerMMMngUSD" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltContPerMMMngUSD" runat="server"></asp:Literal>
                        <asp:Literal ID="ltPerMMMngUSDNoUnit" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <%--    </ContentTemplate>
</asp:UpdatePanel>  --%>
    <asp:UpdatePanel ID="upDeposit" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltDeposit" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltSumDepositVNDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContSumDepositVNDNo" runat="server"></asp:Literal>
                                <asp:Literal ID="ltDepositSumVNDNoUnit" runat="server"></asp:Literal>
                                <asp:Literal ID="ltSumDepositVNDEn" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfSumDepositVNDEn" runat="server" />
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltSumDepositUSDNo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContSumDepositUSDNo" runat="server"></asp:Literal>
                                <asp:Literal ID="ltDepositSumUSDNoUnit" runat="server"></asp:Literal>
                                <asp:Literal ID="ltSumDepositUSDEn" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfSumDepositUSDEn" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table class="TbCel-Type4-A">
                <colgroup>
                    <col width="164px" />
                    <col width="164px" />
                    <col width="104px" />
                    <col width="164px" />
                    <col width="164px" />
                    <col width="60px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltDepositExpDt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositExpAmt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositExcRate" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositPayDt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDepositPayAmt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                &nbsp;
                            </th>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvDepositList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemDataBound="lvDepositList_ItemDataBound" OnItemCreated="lvDepositList_ItemCreated"
                OnItemUpdating="lvDepositList_ItemUpdating" OnItemDeleting="lvDepositList_ItemDeleting">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="160px" />
                        <col width="160px" />
                        <col width="100px" />
                        <col width="160px" />
                        <col width="160px" />
                        <col width="80px" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExpDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                Width="70" ReadOnly="true"></asp:TextBox>
                            <asp:Literal ID="ltExpCalendar" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfDepositExpDt" runat="server" />
                            <asp:TextBox ID="txtContractNo" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtDepositSeq" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExpAmt" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                            <asp:Literal ID="ltDepositExpAmtUnit" runat="server"></asp:Literal>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositExcRate" runat="server" MaxLength="18" Width="70"></asp:TextBox>
                            <asp:Literal ID="ltDepositExcRateUnit" runat="server"></asp:Literal>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtDepositPayDt" runat="server" CssClass="grBg bgType1" MaxLength="10"
                                Width="70" ReadOnly="true"></asp:TextBox>
                            <asp:Literal ID="ltPayCalendar" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfDepositPayDt" runat="server" />
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtAftDepositPayAmt" runat="server" MaxLength="10" Width="70"></asp:TextBox>
                            <asp:Literal ID="ltDepositPayAmtUnit" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfOldDepositPayAmt" runat="server" />
                        </td>
                        <td align="center" class="P0">
                            <span>
                                <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                            <span>
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TbCel-Type4-A">
                        <tbody>
                            <tr>
                                <td colspan="6" align="center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upInterior" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltInterior" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltInteriorStartDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContInteriorStartDt" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltInteriorEndDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContInteriorEndDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltConsDeposit" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContConsDeposit" runat="server"></asp:Literal>
                                <asp:Literal ID="ltConsDepositUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltConsDepositDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContConsDepositDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltConsRefund" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContConsRefund" runat="server"></asp:Literal>
                                <asp:Literal ID="ltConsRefundUnit" runat="server"></asp:Literal>
                            </td>
                            <th class="lebd">
                                <asp:Literal ID="ltConsRefundDt" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContConsRefundDt" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltDifferenceReason" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="ltContDifferenceReason" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltOther" runat="server"></asp:Literal></div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltMemo" runat="server"></asp:Literal>
                            </th>
                            <td>
                                <asp:Literal ID="ltContMemo" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltRemark" runat="server" Text="Remark"></asp:Literal>
                            </th>
                            <td>
                                <asp:TextBox ID="txtContRemark" runat="server" MaxLength="18" Width="700" CssClass="bgType3"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal11" runat="server" Text="Contract File"></asp:Literal>
                            </th>
                            <td>
                                <asp:Button runat="server" ID="btnView" Text="View contract" 
                                    onclick="btnView_Click" />
                                <asp:Button runat="server" ID="btnPDF" Text="Download contract" 
                                    onclick="btnPDF_Click1" />
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
                        <div class="Btn-Tp2-R">
                            <div class="Btn-Tp2-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnList" runat="server"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp FloatL">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnMod" runat="server"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp FloatL">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnDel" runat="server"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:TextBox ID="txtHfExchangeRate" runat="server" Visible="false"></asp:TextBox>
            <asp:ImageButton ID="imgbtnDel" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnDel_Click" />
            <asp:ImageButton ID="imgbtnMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnMove_Click" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfRentSeq" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

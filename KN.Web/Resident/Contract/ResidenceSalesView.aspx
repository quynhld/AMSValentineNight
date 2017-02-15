<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ResidenceSalesView.aspx.cs" Inherits="KN.Web.Resident.Contract.ResidenceSalesView"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--//
    function fnShowModal(strText1, strData, strText2, strValue)
    {
        <%=Page.GetPostBackEventReference(imgbtnDel)%>
        var strReturn = window.showModalDialog("<%=Master.PAGE_POPUP1%>?" + strText1 + "=" + strData + "&" + strText2 + "=" + strValue + "", "window", "dialogWidth:500px;dialogHeight:280px;scroll=no;menubar=no;toolbar=no;location=no;status=no;resizable=no;");
        
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
//-->
    </script>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltBasicInfo" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <col width="147px" />
        <col width="178px" />
        <col width="147px" />
        <col width="178px" />
        <tbody>
            <tr>
                <th>
                    <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                </th>
                <td colspan="2">
                    (<asp:Literal ID="ltInsPersonal" runat="server"></asp:Literal>)
                    <asp:Literal ID="ltInsTenantNm" runat="server"></asp:Literal>
                </td>
                <td>
                    (<asp:Literal ID="ltConcYn" runat="server"></asp:Literal>
                    :
                    <asp:Literal ID="ltInsConcYn" runat="server"></asp:Literal>)
                </td>
            </tr>
            <tr>
                <th>
                    Contract Type
                </th>
                <td colspan="2">
                    <asp:RadioButtonList ID="rbContractType" runat="server" RepeatDirection="Horizontal" Enabled="False">
                        <asp:ListItem Value="VND"  Selected="True">VND</asp:ListItem>
                        <asp:ListItem Value="USD">USD</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:CheckBox ID="chkSpecialContract" runat="server" Text="Is Special Contract" Enabled="False" />
                </td>
            </tr>
            <tr runat="server" id="tdSubLessor">
                <th>
                    <asp:Literal ID="Literal1" runat="server" Text="SubLessor Of"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltSubLessorNm" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltContNo" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    (<asp:Literal ID="ltInsContTy" runat="server"></asp:Literal>
                    -
                    <asp:Literal ID="ltInsTerm" runat="server"></asp:Literal>) &nbsp;
                    <asp:Literal ID="ltInsContNo" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hfContNo" runat="server" />
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    <asp:Literal ID="ltAddr" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltInsAddr" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 37px">
                    <asp:Literal ID="ltDetAddr" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltICPN" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsICPN" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltIssueDt" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsIssueDt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltIssuePlace" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltInsIssuePlace" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTel" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsTelFrontNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltInsTelMidNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltIntTelRearNo" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltMobileNo" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsMobileFrontNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltInsMobileMidNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltInsMobileRearNo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltFAX" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsFAXFrontNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltInsFAXMidNo" runat="server"></asp:Literal>
                    -&nbsp;<asp:Literal ID="ltInsFAXRearNo" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsEmailID" runat="server"></asp:Literal>
                    @&nbsp;<asp:Literal ID="ltInsEmailServer" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltRepresent" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsRepresent" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltPosition" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsPosition" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltBank" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsBank" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltTaxCd" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsTaxCd" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <div runat="server" id="divSubLessor">
        <div class="Tbtop-tit">
            <span id="ctl00_cphContent_lblView">Detail SubLessor</span></div>
        <table cellspacing="0" class="TbCel-Type6">
            <colgroup>
                <col width="20%">
                <col width="50%">
                <col width="30%">
            </colgroup>
            <thead>
                <tr>
                    <th>
                        No
                    </th>
                    <th>
                         Name
                    </th>
                    <th>
                         Phone/Mobile
                    </th>                                                              
                </tr>
             </thead>
            <tbody runat="server" id="lstSubLessor">
            </tbody>
        </table>
    </div>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltCoInfo" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <col width="147px" />
        <col width="178px" />
        <col width="147px" />
        <col width="178px" />
        <tbody>
            <tr>
                <th>
                    <asp:Literal ID="ltCoOwnerNm" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsCoOwnerNm" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltRelationShip" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsRelationShip" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltCoRss" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsCoRss" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltCoIssueDt" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsColssueDt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltCoIssuePlace" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltColssuePlace" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th rowspan="2">
                    <asp:Literal ID="ltCoAddr" runat="server"></asp:Literal>
                </th>
                <td colspan="3" style="height: 37px">
                    <asp:Literal ID="ltInsCoAddr" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 37px">
                    <asp:Literal ID="ltInsCoDetAddr" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltRentTerm" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <col width="147px" />
        <col width="178px" />
        <col width="147px" />
        <col width="178px" />
        <tbody>
            <tr>
                <th>
                    <asp:Literal ID="ltCommencingDt" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsCommencingDt" runat="server"></asp:Literal>
                </td>
                <th class="lebd">
                    <asp:Literal ID="ltExpiringDt" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsExpiringDt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltLastKeyDt" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltInsLastKeyDt" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltRoomInfo" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <col width="147px" />
        <col width="178px" />
        <col width="147px" />
        <col width="178px" />
        <tbody>
            <tr>
                <th>
                    <asp:Literal ID="ltUnitNo" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsUnitNo" runat="server"></asp:Literal>
                </td>
                <th class="lebd">
                    <asp:Literal ID="ltFloor" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsFloor" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltLeasingArea" runat="server"></asp:Literal>
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltInsLeasingArea" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
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
                <th>Inflation(%)</th>    
                <td>                    
                    <asp:TextBox ID="txtFloation" runat="server" AutoPostBack="true" CssClass="bgType2"
                        MaxLength="3" Width="67px">0.1</asp:TextBox>
                </td>

            </tr>
            <tr>
                <th>Payment Type</th>

                    <td colspan="2" style="width: 157px">
                     <asp:Literal ID="ltPayMentTy" runat="server"></asp:Literal>
                </td>
                    <th>CPI(%)</th>                                               
                <td>                               
                    <asp:Literal ID="ltCPI" runat="server"></asp:Literal>
                </td>
            </tr>  
        </tbody>
    </table>
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
        <table cellspacing="0" class="TbCel-Type2-A" id="tblListFee">
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
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltRetalFee" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <col width="147px" />
        <col width="178px" />
        <col width="147px" />
        <col width="178px" />
        <tbody>
            <%--        <tr>
            <th><asp:Literal ID="ltExchangeRate" runat="server"></asp:Literal></th>
            <td colspan="3">
                1 Dollar :
                <asp:Literal ID="ltInsExcangeRate" runat="server"></asp:Literal>
                <asp:Literal ID="ltExchangeUnit" runat="server"></asp:Literal>
                <asp:HiddenField ID="hfExchangeRate" runat="server" />
            </td>
        </tr>--%>
            <!--// 매매가(보증금)관련가격 //-->
            <tr id="trSumRent" runat="server" visible="false">
                <th>
                    <asp:Literal ID="ltSumRentVNDNo" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsSumRentVNDNo" runat="server"></asp:Literal>
                    <asp:Literal ID="ltSumRentVNDNoUnit" runat="server"></asp:Literal>
                </td>
                <th class="lebd">
                    <asp:Literal ID="ltSumRentUSDNo" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsSumRentUSDNo" runat="server"></asp:Literal>
                    <asp:Literal ID="ltSumRentUSDNoUnit" runat="server"></asp:Literal>
                </td>
            </tr>
            <!--// 임대관련 가격 //-->
            <tr id="trPerMMRent" runat="server" visible="false">
                <th>
                    <asp:Literal ID="ltPerMMRentVND" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsPerMMRentVND" runat="server"></asp:Literal>
                    <asp:Literal ID="ltPerMMRentVNDUnit" runat="server"></asp:Literal>
                </td>
                <th class="lebd">
                    <asp:Literal ID="ltPerMMRentUSD" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="ltInsPerMMREntUSD" runat="server"></asp:Literal>
                    <asp:Literal ID="ltPerMMRentUSDUnit" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
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
                        <asp:Literal ID="ltInsSumDepositVNDNo" runat="server"></asp:Literal>
                        <asp:Literal ID="ltDepositSumVNDNoUnit" runat="server"></asp:Literal>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltSumDepositUSDNo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsSumDepositUSDNo" runat="server"></asp:Literal>
                        <asp:Literal ID="ltDepositSumUSDNoUnit" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltMngFee" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="178px" />
            <col width="147px" />
            <col width="178px" />
        </colgroup>
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
            <%--        <tr>
            <th><asp:Literal ID="ltInitPerMMMngVND" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltContInitPerMMMngVND" runat="server"></asp:Literal>
                <asp:Literal ID="ltInitPerMMMngVNDUnit" runat="server"></asp:Literal>
            </td>
            <th class="lebd"><asp:Literal ID="ltInitPerMMMngUSD" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltContInitPerMMMngUSD" runat="server"></asp:Literal>
                <asp:Literal ID="ltInitPerMMMngUSDUnit" runat="server"></asp:Literal>
            </td>
        </tr>--%>
            <%--        <tr>
            <th><asp:Literal ID="ltPerMMMngVND" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltInsPerMMMngVND" runat="server"></asp:Literal>
                <asp:Literal ID="ltPerMMMngVNDNoUnit" runat="server"></asp:Literal>
            </td>
            <th class="lebd"><asp:Literal ID="ltPerMMMngUSD" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltInsPerMMMngUSD" runat="server"></asp:Literal>
                <asp:Literal ID="ltPerMMMngUSDNoUnit" runat="server"></asp:Literal>
            </td>
        </tr>--%>
        </tbody>
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
                        Current Using Date
                    </th>
                    <td>
                        <asp:Literal ID="ltM_UsingDt" runat="server" ></asp:Literal>
                    </td>
                    <th>
                        Payment Cycle
                    </th>
                    <td>
                        <asp:Literal ID="ltM_PayCyle" runat="server" ></asp:Literal>
                        <asp:Literal ID="Literal8" runat="server" Text="Month(s)"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        Payment Cycle Type
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMPaymentCycle" runat="server" Enabled="False">
                            <asp:ListItem Text="B" Value="M">By monthly</asp:ListItem>
                            <asp:ListItem Text="O" Value="Q">By round monthly</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="Literal2" runat="server" Text="Current Pay Date"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltM_PayDt" runat="server" ></asp:Literal>
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
                        <asp:Literal ID="ltM_Adjust" runat="server"></asp:Literal>&nbsp;Day(s)
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <div id="Div1" runat="server">
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
                    <div id="diplayMngFee" runat="server">
                    </div>
                </tbody>
            </colgroup>
        </table>
    </div>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltUse" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="178px" />
            <col width="147px" />
            <col width="178px" />
            <tbody>
                <tr>
                    <th>
                        <asp:Literal ID="ltTradeNm" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsTradeNm" runat="server"></asp:Literal>
                    </td>
                    <th class="lebd">
                        <asp:Literal ID="ltPurpose" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsPurpose" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <div class="Tb-Tp-tit">
        <asp:Literal ID="ltOtherCondition" runat="server"></asp:Literal></div>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="503px" />
            <tbody>
                <tr>
                    <th>
                        <asp:Literal ID="ltPlusCondDt" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsPlusCondDt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltPlusCond" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsPlusCond" runat="server"></asp:Literal>
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
            <col width="503px" />
            <tbody>
                <tr>
                    <th>
                        <asp:Literal ID="ltMemo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltInsMemo" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </colgroup>
    </table>
    <table cellspacing="0" class="TbCel-Type2-A">
        <colgroup>
            <col width="147px" />
            <col width="503px" />
            <tbody>
                <tr>
                    <th>
                        <asp:Literal ID="Literal3" runat="server" Text = "Remark"></asp:Literal>
                    </th>
                    <td>
                       <asp:TextBox ID="txtContRemark" runat="server" MaxLength="18" Width="700" CssClass="bgType3" Enabled= "false" ></asp:TextBox>
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
</asp:Content>

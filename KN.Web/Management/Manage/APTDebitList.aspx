<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="APTDebitList.aspx.cs" Inherits="KN.Web.Management.Manage.APTDebitList"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                callCalendar();
                initControl();
            }
        }

        function fnMakingLine(strTxt) {
            var strItems = document.getElementById("<%=ddlItems.ClientID%>");
            var strYear = document.getElementById("<%=txtSearchDt.ClientID%>");

            if (trim(strItems.value) != "") {

                if (trim(strYear.value) != "") {

                }
                else {
                    alert(strTxt);
                    strYear.focus();

                    return false;
                }
            }
            else {
                alert(strTxt);
                strItems.focus();

                return false;
            }
        }


        $(document).ready(function () {
            callCalendar();
            //var now = new Date();
            // $("#<%=txtSearchDt.ClientID %>").val(now.format("yyyy-MM"));
            initControl();
        });

        function callCalendar() {
            $("#<%=txtSearchDt.ClientID %>").monthpicker({
            });
        }

        function initControl() {
            $('#<%=chkAll.ClientID %>').change(function () {
                var $this = $(this);
                var checkboxes = $('table#tblListDebit tr[style!="display: none;"]').find(':checkbox');
                if ($this.is(':checked')) {
                    //alert('ok');
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });
            var options = {
                rowsPerPage: 18
            };
            $('#tblListDebit').tablePagination(options);            
        }

        function LoadPopupDebit(printNo) {
            var isPrinted = $('#<%=rbIsDebit.ClientID %> input:radio:checked').val();
            window.open("/Common/RdPopup/RDPopupMngFeeAPTDetail.aspx?Datum0=" + printNo + "&Datum2="+isPrinted, "ManageFee", "status=yes, resizable=yes, width=840, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }

        window.somefunction = function () {
            var button = document.getElementById('<%=lnkbtnSearch.ClientID %>');
            button.click();
        };
//-->
    </script>
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li>
                <asp:Literal ID="ltItem" runat="server"></asp:Literal></li>
            <li>
                <asp:DropDownList ID="ddlItems" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Literal ID="ltYear" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server" Visible="True"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
            <li>
                <asp:Literal ID="Literal2" runat="server" Text="Room No"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtRoomNoS" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
            <li>
                <asp:Literal ID="Literal3" runat="server" Text="Is Debit"></asp:Literal></li>
            <li>
                <asp:RadioButtonList ID="rbIsDebit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li>
                <asp:Literal ID="Literal1" runat="server" Text="Tennant Name"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtTennantNmS" Width="200px" runat="server"></asp:TextBox></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="Btn-Type4-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnMakeLine" runat="server" OnClick="lnkbtnMakeLine_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel runat="server" ID="upListDebit">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="lnkbtnMakeLine" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlItems" EventName="SelectedIndexChanged" />   
            <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />  
            <asp:AsyncPostBackTrigger ControlID="imgUpdatePrint" EventName="Click" />       
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type4-A">
                            <col width="20px" />
                            <col width="100px" />
                            <col width="80px" />
                            <col width="200px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                <tr>
                    <th class="Fr-line">
                        <asp:CheckBox ID="chkAll" Style="text-align: center"  runat="server"/>
                    </th>
                    <th align="center">
                        <asp:Literal ID="ltTopPayment" runat="server" Text="Fee Type"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="ltTopMngYYYYMM" runat="server" Text="Period"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="ltTopRoomNo" runat="server" Text="Tenant Name"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="Literal5" runat="server" Text="Room"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="ltTopMovieInDt" runat="server" Text="USD Amount"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="ltTopAmt" runat="server" Text="VND Amount"></asp:Literal>
                    </th>
                    <th class="Bd-Lt" align="center">
                        <asp:Literal ID="Literal4" runat="server" Text="ExChange"></asp:Literal>
                    </th>
                </tr>
            </table>
            <div style="width: 840px;">
                <asp:ListView ID="lvMngManualList" runat="server" ItemPlaceholderID="iphd" OnItemCreated="lvMngManualList_ItemCreated"
                    OnItemDataBound="lvMngManualList_ItemDataBound" OnLayoutCreated="lvMngManualList_LayoutCreated">
                    <LayoutTemplate>
                        <table cellspacing="0" class="TypeA" id="tblListDebit">
                            <col width="20px" />
                            <col width="100px" />
                            <col width="80px" />
                            <col width="200px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <tr id="iphd" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                                <asp:TextBox ID="txtHfFeeTy" MaxLength="10" Width="70px" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtHfRentCd" MaxLength="10" Width="70px" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" MaxLength="10" Width="70px" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txthfFeeSeq" MaxLength="10" Width="70px" runat="server" Visible="False"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:Literal ID="ltFeeTy" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltUSDAmt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltVNDAmt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt" align="center">
                                <asp:Literal ID="ltExRate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellspacing="0" width="818px">
                            <col width="20px" />
                            <col width="100px" />
                            <col width="80px" />
                            <col width="200px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="80px" />
                            <tr>
                                <td colspan="7" align="center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtHfRefPrintNo" runat="server" Visible="false"></asp:TextBox>
            <asp:ImageButton ID="imgUpdatePrint" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgUpdatePrint_Click"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

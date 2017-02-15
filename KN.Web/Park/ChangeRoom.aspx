<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ChangeRoom.aspx.cs" Inherits="KN.Web.Park.ChangeRoom"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {


            }
        }

        function fnCheckType() {
            if (event.keyCode == 13) {
                document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
                return false;
            }
        }

        function rowHover(trow) {
            $(trow).addClass('rowHover');
        }
        function rowOut(trow) {
            $(trow).removeClass('rowHover');
        }
        function resetTable() {
            $("table#ListCar tbody").find("tr").each(function () {
                //get all rows in table               
                $(this).removeClass('rowSelected');
            });
        }

        function fnDetailView(trow, CarNo) {
            resetTable();
            $('#<%=upRegist.ClientID %>').hide("slow");
            $(trow).addClass('rowSelected');
            document.getElementById("<%=txtParkingCarNo.ClientID%>").value = CarNo;
            $('#<%=upRegist.ClientID %>').show("slow");
        }

      

        function datePicker() {

        }

        $('#<%=txtInputRoom.ClientID %>').keydown(function (e) {
            var code = e.keyCode || e.which;
            if (code == '9') {
                $('#<%=imgbtnSearchCompNm.ClientID %>').click();

                return false;
            }
            return true;
        });        


        $(document).ready(function () {            
            $('#<%=upRegist.ClientID %>').hide();
        });

        function fnChangePopup(strCompNmId, strRoomNoId, strUserSeqId, strCompNmS, strRentCd) {
            var strRentCd1 = document.getElementById("<%=ddlRegRentCd.ClientID%>").value;
            strCompNmS = $('#<%=txtTitle.ClientID %>').val();
            window.open("/Common/Popup/PopupCompRoomNo.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId + "&UserSeqId=" + strUserSeqId + "&CompNmS=" + strCompNmS + "&RentCdS=" + strRentCd1, 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');

            return false;
        }

        function callBack(compNm, rentCd, roomNo, userSeq) {
        }

        //    function createDebit(isTrue) {
        //        if (isTrue) {
        //            $("#tblRegDebit").show('slow');
        //        } else {
        //            $("#tblRegDebit").hide();
        //        }
        //    }
        //    $(document).ready(function () {
        //        var TypeHere = $("input[id$='TypeHere']");       
        //        var liTypeHere = $("#liTypeHere");
        //        var mydivTextBox = $("#mydivTextBox");

        //        //Once the user clicks on div, set the focus on input box.
        //        mydivTextBox.click(function () {
        //            TypeHere.focus();
        //        });

        //        TypeHere.keypress(function (e) {
        //            switch (e.keyCode) {
        //                case 188: // ','
        //                    // alert('done');
        //                    break;
        //                default:
        //                    TypeHere.width(TypeHere.width() + 10);
        //            }
        //        });
        //        TypeHere.keyup(function (e) {
        //            switch (e.keyCode) {
        //                case 8:  // Backspace
        //                    if (TypeHere.width() > 10) {
        //                        TypeHere.width(TypeHere.width() - 10);
        //                    }
        //                    break;
        //                case 188: // ','
        //                    var myInputLength = TypeHere.val().length;
        //                    var myInputText = TypeHere.val().substring(0, myInputLength - 1); // remove ','
        //                    TypeHere.width(myInputLength * 6 + 15);
        //                    //Check for email validation.
        //                    //You can apply webservices for any type of validation.
        //                    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        //                    if (myInputText.length == 0) {
        //                        TypeHere.val('');
        //                        return false;
        //                    }
        //                    if (!emailReg.test(myInputText)) {
        //                        alert('Email Is Invalid');
        //                        TypeHere.val('');
        //                        return false;
        //                    }
        //                    //Create the list item on fly and apply the css
        //                    CreateLi(myInputText)
        //                    //Save into Textbox or HiddenField
        //                    var strValue =  myInputText + ';';                    
        //                    //Push the textbox to the right
        //                    TypeHere.width(myInputLength * 6 + 15);
        //                    //Make the input width to default and set as blank
        //                    liTypeHere.css('left', TypeHere.position().left + TypeHere.width() + 10);
        //                    TypeHere.val('');
        //                    TypeHere.width(10);
        //                    break;
        //            }
        //        });

        //        function CreateLi(strValue) {
        //            var strHTML = $("<li class='textboxlist-li textboxlist-li-box textboxlist-li-box-deletable'>" + strValue + "<a href='#' class='textboxlist-li-box-deletebutton'></a></li>");
        //            var size = $("#myListbox > li").size();

        //            $("#myListbox li:nth-child(" + size + ")").before($(strHTML));
        //        }
        //    });
        //    //Adding a click event to a delete button.
        //    $("a").live('click', function (e) {
        //        e.preventDefault;
        //        $(this).parent().remove();
        //        //Remove from the textbox of hidden field ...       

        //    });

   

    //-->
    </script>
    <style type="text/css">
        .rowSelected
        {
            background-color: #E4EEF5;
        }
        .rowHover
        {
            background-color: #E4EEF5;
        }
    </style>
    <div class="TpAtit1">
        <div class="FloatR">
            (<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal
                ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate"
                    runat="server" Visible="false"></asp:TextBox></div>
    </div>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL30">
            <li>
                <asp:DropDownList ID="ddlInsRentCd" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtInsRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltInsCardNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtInsCardNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltInsCarNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtInsCarNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 250px; width: 840px;">
        <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table class="TbCel-Type6-A" cellpadding="0" >
                    <colgroup>
                        <col width="40px" />
                        <col width="60px" />
                        <col width="160px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="100px" />
                        <tr>
                            <th class="Fr-line">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged1"
                                    Style="text-align: center" />
                            </th>
                            <th>
                                <asp:Literal ID="ltTopRoomNo" runat="server" Text="Room No"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltTopName" runat="server" Text="Name"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltTopCarNo" runat="server" Text="Car No"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltTopCardNo" runat="server" Text="Card No"></asp:Literal>
                            </th>
                            <th class="Ls-line">
                                <asp:Literal ID="ltTopCarTy" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </colgroup>
                </table>
                <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound">
                    <LayoutTemplate>
                        <table class="TypeA" id="ListCar">
                            <col width="40px" />
                            <col width="60px" />
                            <col width="160px" />
                            <col width="120px" />
                            <col width="120px" />
                            <col width="100px" />
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="cursor: pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)" onclick="javascript:return fnDetailView(this,'<%#Eval("ParkingCarNo")%>')">
                            <td class="Bd-Lt TbTxtCenter" runat="server" id="tdChk">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltName" runat="server"></asp:Literal>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:TextBox ID="txtCarNo" runat="server" MaxLength="13" Width="90"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="16" Width="90"></asp:TextBox>
                                <asp:TextBox ID="txtHfTagNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltCarTyNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfCarTyCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfParkingYYYYMM" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPayDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA">
                            <tbody>
                                <tr>
                                    <td colspan="6" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:HiddenField ID="hfSelectedLine" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRentChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1B iw840">
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegRent" runat="server" Text="Rent Type"></asp:Literal>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlRegRentCd" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="ltBankAcc" runat="server" Text="Room Change"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtInputRoom" runat="server" Enabled="True" Width="80"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif"
                            ImageAlign="AbsMiddle" Width="17px" Height="15px" />
                    </td>
                    <th align="center">
                        <td colspan="">
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="1000" Width="310"></asp:TextBox>
                        </td>
                </tr>
              <%--  <tr>
                    <th align="center">
                        <asp:Literal ID="Literal1" runat="server" Text="Car No"></asp:Literal>
                    </th>
                    <td colspan="5">
                        <div class="textboxlist" id="mydivTextBox">
                            <ul class="textboxlist-ul" id="myListbox">
                                <li class="textboxlist-li textboxlist-li-editable" style="display: block;" id="liTypeHere">
                                    <asp:TextBox ID="TypeHere" CssClass="textboxlist-li-editable-input" Style="width: 20px;"
                                        MaxLength="35" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </div>
                </tr>--%>
                <tr>
                    <th align="center">
                        <asp:Literal ID="Literal2" runat="server" Text="Car No"></asp:Literal>
                    </th>
                    <td colspan="5">
                        <asp:TextBox ID="txtParkingCarNo" CssClass="bgType2" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Btn-Type3-wp ">
                            <div class="Btn-Tp3-L">
                                <div class="Btn-Tp3-R">
                                    <div class="Btn-Tp3-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="imgbtnRentChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnRentChange_Click" />
           <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailView_Click" />
            <asp:HiddenField ID="HfReturnUserSeqId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

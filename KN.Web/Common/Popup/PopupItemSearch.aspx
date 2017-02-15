<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupItemSearch.aspx.cs" Inherits="KN.Web.Common.Popup.PopupItemSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="ko">
    <head>
    <title>ItemSearch</title>	
    <script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
    <link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
    </head>
    <script language="javascript" type="text/javascript">
    <!--//
        function fnPopupConfirm(strCompCd, strCompNm, strRentCd, strRentNm, strSvcZoneCd, ClassiGrpCd, ClassiGrpNm, ClassiMainCd, ClassiMainNm, strClassCd, strClassNm, strQty, strEmergencyYn, strUnitSellingPrice)
        {
            var strReturnValue = document.getElementById("<%=HfReturnValue.ClientID%>").value;
            var strReturnQtyValue = document.getElementById("<%=HfReturnQtyValue.ClientID%>").value;
            var strReturnValueInfo = document.getElementById("<%=HfReturnValueInfo.ClientID%>").value;


            opener.document.getElementById(strReturnValue).value = strClassNm;
            opener.document.getElementById(strReturnQtyValue).value = strQty;

            opener.document.getElementById(strReturnValueInfo).value = strCompCd + "●" + strCompNm + "●" + strRentCd + "●" + strRentNm + "●" + strSvcZoneCd + "●" + ClassiGrpCd + "●" + ClassiGrpNm + "●" + ClassiMainCd + "●" + ClassiMainNm + "●" + strClassCd + "●" + strClassNm + "●" + strQty + "●" + strEmergencyYn + "●" + strUnitSellingPrice;

            self.close();
        }

        function fnClose()
        {
            self.close();
        }

        function fnCheckCd(strText)
        {
            var strCdSearch = document.getElementById("<%=txtCdSearch.ClientID%>");

            if (trim(strCdSearch.value) == "")
            {
                alert(strText);
                return false;
            }

            return true;
        }

        function fnCheckCdNm(strText)
        {
            var strCdNmSearch = document.getElementById("<%=txtCdNmSearch.ClientID%>");

            if (trim(strCdNmSearch.value) == "")
            {
                alert(strText);
                return false;
            }

            return true;
        }

        function fnCheckCdEnter(strText) 
        {
            if (event.keyCode==13) 
            {
                <%=Page.GetPostBackEventReference(lnkbtnSearchCd)%>;
                
            }
            else
            {
                IsNumeric(strText);
            }
            
            
        }
        
        function fnCheckCdNmEnter() 
        {
            if (event.keyCode==13) 
            {
                <%=Page.GetPostBackEventReference(lnkbtnSearchCdNm)%>;
               
            }
        }
    //-->
    </script>
    
    <body style="background:none;">
    
        <form id="frmMaster" runat="server">
            <asp:ScriptManager ID="smManager" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="searchInfo" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlRentCd" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlSvcZoneCd" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlGrpCd" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlMainCd" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlSubCd" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="lnkbtnSearchCd" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="lnkbtnSearchCdNm" EventName="Click"/>
                </Triggers>
                <ContentTemplate>
                
                    <table class="TbCel-Type2-E MrgB10">
                        <colgroup> 
                            <col width="140px" />
                            <col width="300px" />
                            <col width="140px" />
                            <col width="" />
                        </colgroup>
                        
                        <tr>
		                    <th><asp:Literal ID="ltRentCd" runat="server"></asp:Literal></th>
		                    <td><asp:DropDownList ID="ddlRentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRentCd_SelectedIndexChanged"></asp:DropDownList></td>
		                    <th class="Bd-Lt"><asp:Literal ID="ltSvcZoneCd" runat="server"></asp:Literal></th>
		                    <td><asp:DropDownList ID="ddlSvcZoneCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSvcZoneCd_SelectedIndexChanged"></asp:DropDownList></td>
	                    </tr>
	                    <tr>
		                    <th><asp:Literal ID="ltGrpCd" runat="server"></asp:Literal></th>
		                    <td><asp:DropDownList ID="ddlGrpCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpCd_SelectedIndexChanged"></asp:DropDownList></td>
		                    <th class="Bd-Lt"><asp:Literal ID="ltMainCd" runat="server"></asp:Literal></th>
		                    <td><asp:DropDownList ID="ddlMainCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCd_SelectedIndexChanged"></asp:DropDownList></td>
	                    </tr>
	                    <tr>
		                    <th><asp:Literal ID="ltSubCd" runat="server"></asp:Literal></th>
		                    <td colspan="3"><asp:DropDownList ID="ddlSubCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCd_SelectedIndexChanged"></asp:DropDownList></td>
	                    </tr>

	                    <tr>
                            <th><asp:Literal ID="ltCdSearch" runat="server"></asp:Literal></th>
                            <td colspan="3">
                                <span class="FloatL "><asp:TextBox ID="txtCdSearch" runat="server" MaxLength="16" OnKeyPress="fnCheckCdEnter()"></asp:TextBox></span>
			                    <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL">
				                    <div class="Btn-Tp-L">
					                    <div class="Btn-Tp-R">
						                    <div class="Btn-Tp-M">
							                    <span><asp:LinkButton ID="lnkbtnSearchCd" runat="server" OnClick="lnkbtnSearchCd_Click"></asp:LinkButton></span>
						                    </div>
					                    </div>
				                    </div>
			                    </div>
                            </td>
                        </tr>
	                    <tr>
                            <th><asp:Literal ID="ltCdNmSearch" runat="server"></asp:Literal></th>
                            <td colspan="3">
                                <span class="FloatL "><asp:TextBox ID="txtCdNmSearch" runat="server" MaxLength="30" OnKeyPress="fnCheckCdNmEnter()"></asp:TextBox></span>
			                    <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL">
				                    <div class="Btn-Tp-L">
					                    <div class="Btn-Tp-R">
						                    <div class="Btn-Tp-M">
							                    <span><asp:LinkButton ID="lnkbtnSearchCdNm" runat="server" OnClick="lnkbtnSearchCdNm_Click"></asp:LinkButton></span>
							                    <asp:Literal ID="ltText" runat="server"></asp:Literal>
						                    </div>
					                    </div>
				                    </div>
			                    </div>
                            </td>
                        </tr>     
                    </table>
               
                   
                    <asp:ListView ID="lvItemList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvItemList_ItemCreated"
                        OnLayoutCreated="lvItemList_LayoutCreated" OnItemDataBound="lvItemList_ItemDataBound">
                        <LayoutTemplate>
                            <table class="TypeA">
                                <col width="130px" />
		                        <col width="130px" />
		                        <col width="" />
		                        <col width="130px" />
		                        <col width="130px" />
		                        <col width="130px" />
                                <thead>
                                    <tr>
			                            <th class="Fr-line"><asp:Literal ID="ltSection" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltSvcZone" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltItemCd" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			                            <th class="Ls-line"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></th>
		                            </tr>
                                </thead>
                             </table>
                          <div class="iw840" style="height:200px; overflow-y:scroll">
                             <table class="TbCel-Type3-A iw820">
                                <col width="130px" />
		                        <col width="130px" />
		                        <col width="" />
		                        <col width="130px" />
		                        <col width="130px" />
		                        <col width="110px" />
                                <tbody>
                                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                                </tbody>                
                            </table>
                         </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnPopupConfirm("<%#Eval("CompNo")%>", "<%#Eval("CompNm")%>","<%#Eval("RentCd")%>", "<%#Eval("RentNm")%>","<%#Eval("SvcZoneCd")%>","<%#Eval("ClassiGrpCd")%>","<%#Eval("ClassiGrpNm")%>","<%#Eval("ClassiMainCd")%>","<%#Eval("ClassiMainNm")%>","<%#Eval("ClassCd")%>","<%#Eval("ClassNm")%>","<%#Eval("Qty")%>","<%#Eval("EmergencyYn")%>","<%#Eval("UnitSellingPrice")%>");'>
                                <td class="TbTxtCenter"><asp:Literal ID="ltSectionList" runat="server"></asp:Literal></td>
			                    <td class="TbTxtCenter"><asp:Literal ID="ltSvcZoneList" runat="server"></asp:Literal></td>
			                    <td class="TbTxtCenter"><asp:Literal ID="ltItemNmList" runat="server"></asp:Literal></td>
			                    <td class="TbTxtCenter"><asp:Literal ID="ltItemCdList" runat="server"></asp:Literal></td>
			                    <td class="TbTxtCenter"><asp:Literal ID="ltQtyList" runat="server"></asp:Literal></td>
			                    <td class="TbTxtCenter"><asp:Literal ID="ltPriceList" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="TypeA">
                                <col width="130px" />
		                        <col width="130px" />
		                        <col width="" />
		                        <col width="130px" />
		                        <col width="130px" />
		                        <col width="130px" />
                                <thead>
                                    <tr>
			                            <th class="Fr-line"><asp:Literal ID="ltSection" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltSvcZone" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltItemCd" runat="server"></asp:Literal></th>
			                            <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			                            <th class="Ls-line"><asp:Literal ID="ltPrice" runat="server"></asp:Literal></th>
		                            </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                                    </tr>
                                </tbody>
                            </table>
                        </EmptyDataTemplate>        
                    </asp:ListView> 
                     </ContentTemplate>
            </asp:UpdatePanel>    
            <asp:HiddenField ID="HfReturnValue" runat="server"/>
            <asp:HiddenField ID="HfReturnQtyValue" runat="server"/>
            <asp:HiddenField ID="HfReturnValueInfo" runat="server"/>
        </form>
    </body>    
</html>




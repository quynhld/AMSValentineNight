<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MaterialMngList.aspx.cs" Inherits="KN.Web.Stock.Material.MaterialMngList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckFileUpload(strText)
        {
            var strFileupload = document.getElementById("<%=fuExcelUpload.ClientID%>");

            if (trim(strFileupload.value) == "")
            {
                alert(strText);
                return false;
            }
            
            return true;
        }
    
        function fnMovePage(intPageNo) 
        {
            if (intPageNo == null) 
            {
                intPageNo = 1;
            }
            
            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }
        
        function fnDetailView(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd)
        {
            document.getElementById("<%=hfRentCd.ClientID%>").value = strRentCd;
            document.getElementById("<%=hfSvcZoneCd.ClientID%>").value = strSvcZoneCd;            
            document.getElementById("<%=hfGrpCd.ClientID%>").value = strGrpCd;
            document.getElementById("<%=hfMainCd.ClientID%>").value = strMainCd;
            document.getElementById("<%=hfSubCd.ClientID%>").value = strSubCd;
            
            <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
            return false;
        }
    //-->
    </script>
	<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="ddlRentCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="ddlGrpCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="ddlMainCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
            <fieldset class="sh-field5 MrgB10">
	            <legend>검색</legend>
	            <ul class="sf5-ag MrgL30 ">
	                <li><asp:Literal ID="ltSearch" runat="server"></asp:Literal></li>
	                <li><asp:DropDownList ID="ddlRentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRentCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlSvcZoneCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSvcZoneCd_SelectedIndexChanged"></asp:DropDownList></li>
				</ul>
				<ul class="sf5-ag MrgL30 ">
	                <li><asp:DropDownList ID="ddlGrpCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlMainCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlSubCd" runat="server"></asp:DropDownList></li>
				</ul>
				<ul class="sf5-ag MrgL30 bgimgN">
	                <li><asp:DropDownList ID="ddlStatusCd" runat="server"></asp:DropDownList></li>
		            <li><asp:Literal ID="ltCdNm" runat="server"></asp:Literal></li>
		            <li><asp:TextBox ID="txtCdNm" runat="server" Width="60px" MaxLength="255" CssClass="sh-input"></asp:TextBox></li>
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
	    </ContentTemplate>
	</asp:UpdatePanel>
	<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
	        
	    </Triggers>
	    <ContentTemplate>
	        <asp:ListView ID="lvMaterialList" runat="server" ItemPlaceholderID="iphItemPlaceHoderID" 
	            OnItemDataBound="lvMaterialList_ItemDataBound" OnLayoutCreated="lvMaterialList_LayoutCreated" OnItemCreated="lvMaterialList_ItemCreated">
	            <LayoutTemplate>
	                <table class="TbCel-Type3">
		                <tr>
			                <th><asp:Literal runat="server" ID="ltSeq"></asp:Literal></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltGoodsCd"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltGoodsNm"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltQty"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltReleaseRequest"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltOrderRequset"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltEmergency"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><a href="#" class="sortc"><asp:Literal runat="server" ID="ltUse"></asp:Literal><img src="/Common/Images/Icon/sortIcon2.gif" alt="정렬" /></a></th>
			                <th class="Bd-Lt"><asp:CheckBox ID="chkAll" style="text-align:center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged"/></th>
		                </tr>
		                <tr id="iphItemPlaceHoderID" runat="server"></tr>
		            </table>
	            </LayoutTemplate>
	            <ItemTemplate>
                    <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" ondblclick='javascript:fnDetailView("<%#Eval("RentCd")%>","<%#Eval("SvcZoneCd")%>","<%#Eval("ClassiGrpCd")%>","<%#Eval("ClassiMainCd")%>","<%#Eval("ClassCd")%>");'>
		                <td class="TbTxtCenter"><asp:Literal runat="server" ID="ltInsSeq"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsGoodsCd"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsGoodsNm"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsQty"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsReleaseRequest"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsOrderRequset"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsEmergency"></asp:Literal></th>
		                <td class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltInsUse"></asp:Literal></th>
			            <td class="Bd-Lt TbTxtCenter  P0"><asp:CheckBox ID="chkboxList" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"></asp:CheckBox></td>
		            </tr>
	            </ItemTemplate>
	            <EmptyDataTemplate>
                    <table class="TbCel-Type3">
                        <tr>
                            <th><asp:Literal runat="server" ID="ltSeq"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltGoodsCd"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltGoodsNm"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltQty"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltReleaseRequest"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltOrderRequset"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltEmergency"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:Literal runat="server" ID="ltUse"></asp:Literal></th>
                            <th class="Bd-Lt TbTxtCenter"><asp:CheckBox ID="chkAll" style="text-align:center" runat="server"/></th>
                        </tr>
                        <tr>
                            <td colspan="9" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
	            </EmptyDataTemplate>
	        </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="Type-viewB-PN">
        <col width="30%"/>
        <col width="60%"/>
        <tr>
            <th class="Fr-line"><asp:Literal ID="ltAddonFile" runat="server"></asp:Literal></th>
	        <td>
	            <span class="Ls-line">
	                <asp:FileUpload ID="fuExcelUpload" runat="server"/>
	                <asp:Literal ID="ltSampleFile" runat="server"></asp:Literal>
	                <asp:HyperLink ID="hlExcel" ImageUrl="~/Common/Images/Icon/exell.gif" runat="server" NavigateUrl="~/Stock/Material/StandardFiles.xls"></asp:HyperLink>
	            </span>
	        </td>
        </tr>
    </table>
    <div class="Clear">
     <span id="spanPageNavi" runat="server" style="width:100%;display:block;" class="Clear"></span>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                      <span><a href="javascript:newWindow('/Common/PrintOut/StMn.html', 'mgf' , '800', '500')">Print</a></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
	            <div class="Btn-Tp3-R">
		            <div class="Btn-Tp3-M">
			            <span><asp:LinkButton ID="lnkbtnFileUpload" runat="server" OnClick="lnkbtnFileUpload_Click"></asp:LinkButton></span>
		            </div>
	            </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
	        <div class="Btn-Tp3-L">
		        <div class="Btn-Tp3-R">
			        <div class="Btn-Tp3-M">
			            <span><asp:LinkButton ID="lnkbtnReleaseRequest" runat="server" OnClick="lnkbtnReleaseRequest_Click"></asp:LinkButton></span>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="Btn-Type3-wp ">
	        <div class="Btn-Tp3-L">
		        <div class="Btn-Tp3-R">
			        <div class="Btn-Tp3-M">
			            <span><asp:LinkButton ID="lnkbtnOrderRequest" runat="server" OnClick="lnkbtnOrderRequest_Click"></asp:LinkButton></span>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="Btn-Type3-wp ">
	        <div class="Btn-Tp3-L">
		        <div class="Btn-Tp3-R">
			        <div class="Btn-Tp3-M">
				        <span><asp:LinkButton ID="lnkbtnAddItems" runat="server" OnClick="lnkbtnAddItems_Click"></asp:LinkButton></span>
			        </div>
		        </div>
	        </div>
        </div>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
	<asp:UpdatePanel ID="upHfPanel" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>

	    </Triggers>
	    <ContentTemplate>
            <asp:HiddenField ID="hfCurrentPage" runat="server"/>
            <asp:HiddenField ID="hfRentCd" runat="server"/>
            <asp:HiddenField ID="hfSvcZoneCd" runat="server"/>
            <asp:HiddenField ID="hfGrpCd" runat="server"/>
            <asp:HiddenField ID="hfMainCd" runat="server"/>
            <asp:HiddenField ID="hfSubCd" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
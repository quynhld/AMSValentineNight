<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ReleaseChargerList.aspx.cs" Inherits="KN.Web.Stock.Release.ReleaseChargerList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div class="sType2 iw820">
	    <asp:UpdatePanel ID="upDeptList" runat="server" UpdateMode="Conditional">
	        <Triggers>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnModify" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnDelete" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click"/>
	        </Triggers>
	        <ContentTemplate>
	            <dl class="dp1">
		            <dt><asp:Literal ID="ltDeptGrp" runat="server"></asp:Literal></dt>
		            <dd class="setw"><asp:ListBox ID="lbDeptList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lbDeptList_SelectedIndexChanged" Height="330px" Width="168px"></asp:ListBox></dd>
	            </dl>
            </ContentTemplate>
        </asp:UpdatePanel>
	    <asp:UpdatePanel ID="upChagerList" runat="server" UpdateMode="Conditional">
	        <Triggers>
	            <asp:AsyncPostBackTrigger ControlID="lbDeptList" EventName="SelectedIndexChanged"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnModify" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnDelete" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click"/>
	        </Triggers>
	        <ContentTemplate>
	            <dl class="dp1">
		            <dt><asp:Literal ID="ltChargerGrp" runat="server"></asp:Literal></dt>
		            <dd class="setw"><asp:ListBox ID="lbChagerList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lbChagerList_SelectedIndexChanged" Height="330px" Width="168px"></asp:ListBox></dd>
	            </dl>
	        </ContentTemplate>
	    </asp:UpdatePanel>
	    <asp:UpdatePanel ID="upAssignList" runat="server" UpdateMode="Conditional">
	        <Triggers>
	            <asp:AsyncPostBackTrigger ControlID="lbDeptList" EventName="SelectedIndexChanged"/>
	            <asp:AsyncPostBackTrigger ControlID="lbChagerList" EventName="SelectedIndexChanged"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnModify" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnDelete" EventName="Click"/>
	            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click"/>
	        </Triggers>
	        <ContentTemplate>
		        <dl class="dp2">
		            <dt><asp:Literal ID="ltAssignment" runat="server"></asp:Literal></dt>
		            <dd class="setw">
		                <ul class="st3f" style="padding:0 20px;" >
		                    <li>
		                        <asp:Literal ID="ltDept" runat="server"></asp:Literal>&nbsp;/
		                        <asp:Literal ID="ltCharger" runat="server"></asp:Literal>&nbsp;/
		                        <asp:Literal ID="ltException" runat="server"></asp:Literal>
		                    </li>
		                </ul>
		                <asp:ListView ID="lvAssignList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvAssignList_ItemDataBound">
		                    <LayoutTemplate>
			                    <ul class="st3f" style="overflow-y:scroll;height:260px;">
			                        <li runat="server" id="iphItemPlaceHolderId"></li>
				                </ul>
		                    </LayoutTemplate>
		                    <ItemTemplate>
		                        <li>
		                            <span><asp:Literal ID="ltStep" runat="server"></asp:Literal></span>
		                            <span>
		                                <asp:TextBox ID="txtDeptNm" CssClass="bdst1 iw80" runat="server" ReadOnly="true"></asp:TextBox>
		                                <asp:TextBox ID="txtHfDeptCd" runat="server" Visible="false"></asp:TextBox>
		                            </span>
		                            <span>
		                                <asp:TextBox ID="txtMemNm" CssClass="bdst1 iw80" runat="server" ReadOnly="true"></asp:TextBox>
		                                <asp:TextBox ID="txtHfMemNo" runat="server" Visible="false"></asp:TextBox>
		                            </span>
		                            <span><asp:CheckBox ID="chkList" runat="server"/></span>
		                        </li>
		                    </ItemTemplate>
		                </asp:ListView>
		            </dd>
	            </dl>
	            <asp:TextBox ID="txtHfTmpSeq" runat="server" Visible="false"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
	    <div class="Btwps FloatR">
		    <div class="Btn-Type3-wp ">
			    <div class="Btn-Tp3-L">
				    <div class="Btn-Tp3-R">
					    <div class="Btn-Tp3-M">
						    <span><asp:LinkButton ID="lnkbtnModify" runat="server" OnClick="lnkbtnModify_Click"></asp:LinkButton></span>
					    </div>
				    </div>
			    </div>
		    </div>
		    <div class="Btn-Type3-wp ">
			    <div class="Btn-Tp3-L">
				    <div class="Btn-Tp3-R">
					    <div class="Btn-Tp3-M">
						    <span><asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click"></asp:LinkButton></span>
					    </div>
				    </div>
			    </div>
		    </div>
		    <div class="Btn-Type3-wp ">
			    <div class="Btn-Tp3-L">
				    <div class="Btn-Tp3-R">
					    <div class="Btn-Tp3-M">
						    <span><asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>

</asp:Content>
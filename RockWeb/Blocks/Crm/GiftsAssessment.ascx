﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftsAssessment.ascx.cs" Inherits="Rockweb.Blocks.Crm.GiftsAssessment" ViewStateMode="Enabled" EnableViewState="true" %>
<script type="text/javascript">
    ///<summary>
    /// Fade-in effect for the panel.
    ///</summary>
    function fadePanelIn() {
        $("[id$='upAssessment']").rockFadeIn();
    }

   ///<summary>
    /// Returns true if the test is complete.
    ///</summary>
    function isComplete() {

        // provide error indicator if nothing is checked for each radio button group
        $(".js-gift-questions input:radio").each(function(){
            var name = $(this).attr("name");
            if ($("input:radio[name='" + name + "']:checked").length == 0) {
                $("input:radio[name='" + name + "']").first().closest(".form-group").addClass("has-error has-feedback");
            }
        });

        var $completedQuestions = $('.js-gift-questions input[type=radio]:checked');
        if ($completedQuestions.length < parseInt($('#<%=hfQuestionCount.ClientID%>').val())){
            $('[id$="divError"]').fadeIn();
            return false;
        }
        else {
            return true;
        }
    }

    ///<summary>
    /// Unchecks the corresponding item from the other rbl
    /// and moves to next question.
    ///</summary>
    function initQuestionsValidation() {
        $('.js-gift-questions input[type=radio]').change(function () {
            $(this).first().closest(".form-group").removeClass("has-error has-feedback").addClass("text-muted");
        });
    }

    ///<summary>
    /// Standard .Net handler that's called when the page is loaded (including Ajax).
    ///</summary>
    function pageLoad(sender, args) {
        initQuestionsValidation();
    }

    $(document).ready(function () {
        fadePanelIn();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fadePanelIn);
    });
</script>
<asp:UpdatePanel ID="upAssessment" runat="server">
    <ContentTemplate>
        <Rock:NotificationBox ID="nbError" runat="server" Visible="false" NotificationBoxType="Danger">You have to be signed in to take the assessment.</Rock:NotificationBox>
        <asp:HiddenField ID="hfQuestionCount" runat="server" />
        <asp:Panel ID="pnlAssessment" CssClass="panel panel-block" runat="server">
            <div class="panel-heading">
                <h1 class="panel-title"><i runat="server" id="iIcon"></i>
                    <asp:Literal ID="lTitle" runat="server" /></h1>
            </div>
            <div class="panel-body">
                <asp:Panel ID="pnlInstructions" runat="server">
                    <asp:Literal ID="lInstructions" runat="server"></asp:Literal>

                    <div class="actions">
                        <asp:LinkButton ID="btnStart" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnStart_Click">Start <i class="fa fa-chevron-right"></i></asp:LinkButton>
                    </div>
                </asp:Panel>

                <%-- Questions --%>
                <asp:Panel ID="pnlQuestion" runat="server" Visible="false">
                    <asp:HiddenField ID="hfPageNo" runat="server" />
                    <Rock:NotificationBox runat="server" NotificationBoxType="Warning" Text="Respond to these items quickly and don’t overthink them. Usually your first response is your best response." ID="nbMessage" />
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" aria-valuenow="<%=this.PercentComplete%>" aria-valuemin="0" aria-valuemax="100" style="min-width: 2em; width: <%=this.PercentComplete%>%;">
                            <%=this.PercentComplete.ToString("F0") %>%
                        </div>
                    </div>
                    <asp:Repeater ID="rQuestions" runat="server" OnItemDataBound="rQuestions_ItemDataBound">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hfQuestionCode" runat="server" Value='<%# Eval( "Code") %>' />
                                    <Rock:RockRadioButtonList ID="rblQuestion" runat="server" RepeatDirection="Horizontal" Label='<%# Eval( "Question") %>' CssClass="js-gift-questions">
                                        <asp:ListItem Text="Strongly Agree" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Agree" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Somewhat Agree" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Neutral" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Disagree" Value="0"></asp:ListItem>
                                    </Rock:RockRadioButtonList>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                     <div style="display: none" class="alert alert-danger" id="divError">
                         Please answer all questions before continuing.
                     </div>
                    <div class="actions">
                        <asp:LinkButton ID="btnPrevious" runat="server" AccessKey="p" ToolTip="Alt+p" Text="Previous" CssClass="btn btn-default js-wizard-navigation" CausesValidation="false" OnClick="btnPrevious_Click" />
                        <asp:LinkButton ID="btnNext" runat="server" AccessKey="n" Text="Next" OnClientClick="if (!isComplete()) { return false; }" DataLoadingText="Next" CssClass="btn btn-primary pull-right js-wizard-navigation" CausesValidation="true" OnClick="btnNext_Click" />
                    </div>
                </asp:Panel>
                  <asp:Panel ID="pnlResult" runat="server">
                    <asp:Literal ID="lResult" runat="server"></asp:Literal>

                      <div class="actions margin-t-md">
                        <asp:LinkButton ID="btnRetakeTest" runat="server" CssClass="btn btn-primary" OnClick="btnRetakeTest_Click">Retake Test</asp:LinkButton>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
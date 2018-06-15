<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolveQuiz.aspx.cs" Inherits="quizy.SolveQuiz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="style.css" rel="stylesheet" type="text/css" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="title">
            <asp:Label ID="titleLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="">
            <asp:Label ID="questionLabel" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:RadioButtonList ID="answerRadioButtonList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="answerRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
            </div>
        <div>
            <asp:Button ID="BackButton" runat="server" Text="Wstecz" OnClick="BackButton_Click" />
            <asp:Button ID="NextNextButton" runat="server" Text="Dalej" OnClick="NextNextButton_Click" />
            </div>
        <div class="errorText">
            <asp:Label ID="warningLabel1"  runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>

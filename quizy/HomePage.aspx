<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="quizy.MainPage1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="style.css" rel="stylesheet" type="text/css" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="title">
                Strona główna
            </div>
        </div>
        <div class="center">
        <div class="margin larger">
            <asp:Label ID="MenuLabel" runat="server" Text="Menu"></asp:Label>
            </div>
        <div class="margin">
            
        <asp:Button ID="AddQuizButton" runat="server" Text="Dodaj quiz" OnClick="AddQuizButton_Click" />
            </div>
            <div class="margin">
        <asp:Button ID="QuizDBButton" runat="server" Text="Baza quizów" OnClick="QuizDBButton_Click" />
        </div>
        <div class="errorText margin">
            <asp:Label ID="warningLabel1" runat="server" Text=""></asp:Label>
            </div>
        <div>
    </form>
</body>
</html>

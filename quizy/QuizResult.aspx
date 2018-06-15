<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizResult.aspx.cs" Inherits="quizy.QuizResult" %>

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
            Wyniki
        </div>
        <div>
            Twój wynik to: <asp:Label ID="pointsLabel" runat="server" Text=""></asp:Label> / <asp:Label ID="totalLabel" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Button ID="BackButton" runat="server" Text="Powrót" OnClick="BackButton_Click" />
        </div>
    </form>
</body>
</html>

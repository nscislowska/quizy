<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizBase.aspx.cs" Inherits="quizy.QuizBase" %>

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
            Baza quizów
        </div>
       <div class="margin">
        <div class="radiobuttonlist lower">
        <asp:RadioButtonList ID="QuizRadioButtonList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="QuizRadioButtonList_SelectedIndexChanged">
        </asp:RadioButtonList>
            </div>
        <div class="margin sidemenu">
            <div class="margin">
            <asp:Button ID="BackButton" runat="server" Text="Wstecz" OnClick="BackButton_Click" />
                </div>
            <div class="margin">
            <asp:Button ID="SolveButton" runat="server" Text="Rozwiąż" OnClick="SolveButton_Click" />
                </div>
            <div class="margin">
            <asp:Button ID="DeleteButton" runat="server" Text="Usuń" OnClick="DeleteButton_Click" />
                </div>
            <div class="errorText">

            <asp:Label ID="warningLabel1" runat="server" Text=""></asp:Label>

            </div>
        </div>
           </div>
        
       
    </form>
</body>
</html>

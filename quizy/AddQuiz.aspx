<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuiz.aspx.cs" Inherits="quizy.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="style.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="addQuiz" runat="server">
        <div id="title" class="title">
            Dodaj Quiz
        </div>
        <div id="addQuizName" class="margin">
            <asp:Label ID="quizNameLabel" CssClass="question" runat="server" Text="Podaj nazwę quizu"/><br />
            <asp:TextBox ID="quizNameTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="setQuizNameButton" runat="server" Text="Dodaj" OnClick="setQuizNameButton_Click" />
            <br />
             <asp:Label ID="warningLabel0" CssClass="errorText" runat="server" Text=""></asp:Label>
        </div>
        <div id="addQuestion" class="margin">
            <asp:Label ID="plainText" runat="server" Text="Dodaj pytanie"/><br />
            <asp:TextBox ID="questionTextBox" runat="server" Width="475px"></asp:TextBox>
            <asp:Button ID="addButton" runat="server" Text="Dodaj" OnClick="addButton_Click" />
            <br />
            <asp:Label ID="warningLabel1" CssClass="errorText" runat="server" Text=""></asp:Label>
        </div>
        <div id="questions" class="margin">
            <asp:Label ID="listLabel" runat="server" Text="Pytania"/><br/>
            <asp:ListBox ID="questionListBox" runat="server"></asp:ListBox>
                
            
        </div>
        <div class="margin">
            <asp:Label ID="warningLabel2" CssClass="errorText" runat="server" Text=""></asp:Label>
            </div>
        <div class="edit-deleteButtons margin">
                <asp:Button ID="editButton" runat="server" Text="Edytuj" OnClick="editButton_Click" />
                <asp:Button ID="deleteButton" runat="server" Text="Usuń" OnClick="deleteButton_Click" />
            </div>
        
        
        <div class="send">
            <asp:Label ID="label1" runat="server" Text="Zatwierdź pytania i przejdź do dodania odpowiedzi"/><br/>
            <asp:Button ID="BackHomeButton" runat="server" Text="Powrót" OnClick="BackHomeButton_Click" />
            <asp:Button ID="sendButton" runat="server" Text="Przejdź dalej" OnClick="sendButton_Click" /><br //>
            <asp:Label ID="warningLabel3" CssClass="errorText" runat="server" Text=""></asp:Label>
        </div>
     </form>
       
    </body>
</html>

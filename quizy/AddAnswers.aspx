<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAnswers.aspx.cs" Inherits="quizy.AddAnswers" %>

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
          Odpowiedzi
        </div>

        <div>
             <asp:Label ID="QuizNameLabel" runat="server" Text="Nazwa quizu"></asp:Label>
        </div>

        <div>
            <asp:Label ID="QuestionTextLabel" runat="server" Text="Pytanie"></asp:Label>
        </div>

        <div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="Dodaj odpowiedź"></asp:Label>
                <asp:TextBox ID="AnswerTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="addAnswerButton" runat="server" Text="Dodaj" OnClick="addAnswerButton_Click" />
                <div class="errorText">
                    <asp:Label ID="warningLabel0" runat="server" Text=""></asp:Label>
                </div>  
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" Text="Odpowiedzi"></asp:Label>
                <br/>
                <asp:ListBox ID="AnswerListBox" runat="server"></asp:ListBox>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <div class="errorText">
                    <asp:Label ID="warningLabel1" runat="server" Text=""></asp:Label>
                </div> 
            </div>
            <div>
                Poprawna odpowiedź: <asp:Label ID="correctAnswerLabel" runat="server" Text="brak"></asp:Label>
                <div class="errorText">
                    <asp:Label ID="warningLabel3" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="edit-deleteButtons">
                <asp:Button ID="editAnswerButton" runat="server" Text="Edytuj" OnClick="editAnswerButton_Click" />
                <asp:Button ID="deleteAnswerButton" runat="server" Text="Usuń" OnClick="deleteAnswerButton_Click" />
                <asp:Button ID="markCorrectButton" runat="server" Text="Oznacz jako prawidłowa" OnClick="markCorrectButton_Click" />
            </div>
        </div>

        <div class="send">
            <asp:Label ID="Label4" runat="server" Text="Zatwierdź odpowiedi i przejdź do kolejnego pytania"></asp:Label><br/>
            <asp:Button ID="sendAnswersButton" runat="server" Text="Przejdź dalej" OnClick="sendAnswersButton_Click" />
            <div class="errorText">
                <asp:Label ID="warningLabel2" runat="server" Text=""></asp:Label>
            </div> 
        </div>
        
       
    </form>

</body>
</html>

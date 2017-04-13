' Josh Joseph
' This program reads all headlines from US section of New York Times
Public Class Form1

    Dim SAPI = CreateObject("Sapi.spvoice")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sourceString As String = New System.Net.WebClient().DownloadString("http://rss.nytimes.com/services/xml/rss/nyt/US.xml") 'retrieves RSS feed of US section of New York Times and saves it as a String
        Dim cutspread As String() = Split(sourceString, "<item>") 'splits the string at <item>
        Dim cutSpreadAgain As String()
        Dim finalCutSpread As String()
        Dim titles As String = ""

        For index = 1 To cutspread.Length - 1
            cutSpreadAgain = Split(cutspread(index), "<title>") 'splits the string again at <title>
            finalCutSpread = Split(cutSpreadAgain(1), "</title>") 'splits the string again at </title> to get each headline
            titles += finalCutSpread(0) & ". " 'adds period at the end of each headline for better reading
        Next

        Dim titlesBeautify As String = titles.Replace("&#x2019;", "'") 'replaces &#x2019; (unicode) with single quotation mark (right) 
        titlesBeautify = titlesBeautify.Replace("&#x2018;", "'") 'replaces &#x2018; (unicode) with single quotation mark (left) 
        TextBox1.Text = titlesBeautify 'puts the edited headlines in the read only textbox
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SAPI.Speak(TextBox1.Text) 'reads the text from Text Box when button is clicked
    End Sub
End Class
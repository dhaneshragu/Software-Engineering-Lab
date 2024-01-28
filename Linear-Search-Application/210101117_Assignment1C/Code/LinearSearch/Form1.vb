Imports System.IO

' The main form for the application.
Public Class Form1
    ' To normalize float input. For example, 3.500 is converted to 3.5
    Private Function NormalizeFloat(input As String) As String
        Dim value As Double

        ' Try parsing the input as a double
        If Double.TryParse(input, value) Then
            ' If successful, return the normalized string
            Return value.ToString()
        End If

        ' If it's not a float number, return the original input
        Return input
    End Function

    ' Private fields to store file path, target element, case sensitivity, and animation speed
    Private filePath As String
    Private targetElement As String
    Private considerCase As Boolean
    Private speedVal As Integer

    ' Form load event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Default settings on form load
        considerCase = False
        speedVal = 500

        ' Enable horizontal scrolling
        ListView1.Scrollable = True
        ListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable

        ' Adding items to ComboBox
        ComboBox1.Items.Add("Slow")
        ComboBox1.Items.Add("Medium")
        ComboBox1.Items.Add("Fast")

        ' Set default selection
        ComboBox1.SelectedIndex = 1 ' Selects "Medium" as the default option
    End Sub

    ' Load file content into ListView
    Private Sub LoadFileContent(filePath As String)
        ' Clear the ListView
        ListView1.Items.Clear()

        Try
            ' Read file contents and add each non-empty line to the ListView
            Using reader As New StreamReader(filePath)
                Do While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine().Trim()

                    ' Check if the line is not empty before adding it to the ListView
                    If Not String.IsNullOrEmpty(line) Then
                        Dim item As New ListViewItem(NormalizeFloat(line))
                        ListView1.Items.Add(item)
                    End If
                Loop
            End Using

            ' Check if the ListView is still empty after reading the file
            If ListView1.Items.Count = 0 Then
                ' Display an error message if the file is empty
                MessageBox.Show("The file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ' Display an error message if there's an issue reading the file
            MessageBox.Show("Error reading the file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Button click event to initiate search
    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        If ListView1.Items.Count = 0 Then
            ' Display an error message if the file is empty
            MessageBox.Show("The file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ' Get the target element from the TextBox
            targetElement = NormalizeFloat(TextBoxSearch.Text.Trim())

            ' Validate if the target element is empty
            If targetElement.Length = 0 Then
                MessageBox.Show("Please enter the target element.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Highlight items in the ListView
            HighlightItems()
        End If
    End Sub

    ' Custom wait function
    Private Sub wait(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        ' Wait until the interval in milliseconds
        Do While stopW.ElapsedMilliseconds < interval
            Application.DoEvents() ' Allows your UI to remain responsive
        Loop
        stopW.Stop()
    End Sub

    ' Highlight items in the ListView
    Private Sub HighlightItems()
        Dim found As Boolean = False

        ' Iterate through the items
        For i As Integer = 0 To ListView1.Items.Count - 1
            ' Check if the current item matches the target
            If considerCase Then
                If ListView1.Items(i).Text.Equals(targetElement) Then
                    ' Display a message indicating the target is found
                    HighlightItem(i, Color.LightGreen)
                    MessageBox.Show("Element '" & targetElement & "' found at index " & i & ".", "Element Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    found = True
                    HighlightItem(i, ListView1.BackColor)
                    RadioButton1.Checked = False
                    Exit For
                End If
            Else
                If ListView1.Items(i).Text.Equals(targetElement, StringComparison.OrdinalIgnoreCase) Then
                    ' Display a message indicating the target is found
                    HighlightItem(i, Color.LightGreen)
                    MessageBox.Show("Element '" & targetElement & "' found at index " & i & ".", "Element Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    found = True
                    HighlightItem(i, ListView1.BackColor)
                    RadioButton1.Checked = False
                    Exit For
                End If
            End If

            ' Highlight the current item
            HighlightItem(i, Color.Yellow)
            wait(speedVal)
            HighlightItem(i, ListView1.BackColor)
        Next

        ' If the target is not found, display a message
        If Not found Then
            MessageBox.Show("Element '" & targetElement & "' not found.", "Element Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RadioButton1.Checked = False
        End If

        ' Unhighlight the last item
        If ListView1.Items.Count > 0 Then
            HighlightItem(ListView1.Items.Count - 1, ListView1.BackColor)
        End If
    End Sub

    ' Highlight a specific item in the ListView with the given color, used in HighlightItems() function.
    Private Sub HighlightItem(index As Integer, color As Color)
        ListView1.Items(index).BackColor = color
        ListView1.Items(index).EnsureVisible()
    End Sub

    ' File selection dialog event
    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ' Load file content when the file selection dialog is closed
        If OpenFileDialog1.FileName <> "" Then
            LoadFileContent(OpenFileDialog1.FileName)
        End If
    End Sub

    ' Button click event to open file selection dialog
    Private Sub ButtonLoad_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLoad.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    ' RadioButton checked change event
    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        ' Handle RadioButton checked change event
        considerCase = RadioButton1.Checked
    End Sub

    ' ComboBox selected index change event
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Handle ComboBox selected index change event
        Select Case ComboBox1.SelectedItem.ToString()
            Case "Slow"
                speedVal = 1000
            Case "Medium"
                speedVal = 500
            Case "Fast"
                speedVal = 60
            Case Else
                speedVal = 500
        End Select
    End Sub
End Class

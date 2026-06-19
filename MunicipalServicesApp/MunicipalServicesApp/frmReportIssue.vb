Public Class frmReportIssue
    ' Declare data structures
    Private Shared reportedIssues As New List(Of ServiceRequest)
    Private selectedImagePath As String = ""
    Private selectedImageBytes As Byte() = Nothing

    Private Sub frmReportIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupForm()
    End Sub

    Private Sub SetupForm()
        Me.Text = "Report an Issue"
        Me.Size = New Size(800, 650)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Color.White

        ' Header Panel
        Dim pnlHeader As New Panel()
        pnlHeader.BackColor = Color.FromArgb(220, 53, 69)
        pnlHeader.Height = 80
        pnlHeader.Dock = DockStyle.Top

        Dim lblHeader As New Label()
        lblHeader.Text = "Report an Issue or Request Service"
        lblHeader.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        lblHeader.ForeColor = Color.White
        lblHeader.TextAlign = ContentAlignment.MiddleCenter
        lblHeader.Dock = DockStyle.Fill
        pnlHeader.Controls.Add(lblHeader)

        ' Main Panel
        Dim pnlMain As New Panel()
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Padding = New Padding(30)
        pnlMain.AutoScroll = True

        ' Name Field
        Dim lblName As New Label()
        lblName.Text = "Full Name: *"
        lblName.Font = New Font("Segoe UI", 11)
        lblName.Location = New Point(20, 20)
        lblName.Size = New Size(150, 25)

        Dim txtName As New TextBox()
        txtName.Name = "txtName"
        txtName.Font = New Font("Segoe UI", 11)
        txtName.Location = New Point(180, 20)
        txtName.Size = New Size(300, 25)

        ' Location Field
        Dim lblLocation As New Label()
        lblLocation.Text = "Location: *"
        lblLocation.Font = New Font("Segoe UI", 11)
        lblLocation.Location = New Point(20, 60)
        lblLocation.Size = New Size(150, 25)

        Dim txtLocation As New TextBox()
        txtLocation.Name = "txtLocation"
        txtLocation.Font = New Font("Segoe UI", 11)
        txtLocation.Location = New Point(180, 60)
        txtLocation.Size = New Size(300, 25)

        ' Issue Type Dropdown
        Dim lblIssueType As New Label()
        lblIssueType.Text = "Issue Type: *"
        lblIssueType.Font = New Font("Segoe UI", 11)
        lblIssueType.Location = New Point(20, 100)
        lblIssueType.Size = New Size(150, 25)

        Dim cmbIssueType As New ComboBox()
        cmbIssueType.Name = "cmbIssueType"
        cmbIssueType.Font = New Font("Segoe UI", 11)
        cmbIssueType.Location = New Point(180, 100)
        cmbIssueType.Size = New Size(300, 30)
        cmbIssueType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbIssueType.Items.AddRange(New String() {
            "Select Issue Type",
            "🏠 Sanitation/Refuse Collection",
            "🛣️ Roads & Potholes",
            "💡 Utilities (Water/Electricity)",
            "🌳 Parks & Recreation",
            "🚦 Street Lighting",
            "🏢 Municipal Building Issues",
            "🐕 Animal Control",
            "🚰 Water Leakage",
            "📡 Other Services"
        })
        cmbIssueType.SelectedIndex = 0

        ' Description Field
        Dim lblDescription As New Label()
        lblDescription.Text = "Description: *"
        lblDescription.Font = New Font("Segoe UI", 11)
        lblDescription.Location = New Point(20, 150)
        lblDescription.Size = New Size(150, 25)

        Dim txtDescription As New RichTextBox()
        txtDescription.Name = "txtDescription"
        txtDescription.Font = New Font("Segoe UI", 11)
        txtDescription.Location = New Point(180, 150)
        txtDescription.Size = New Size(500, 120)
        txtDescription.BorderStyle = BorderStyle.FixedSingle

        ' ATTACH FILE BUTTON - Complete Code
        Dim btnAttach As New Button()
        btnAttach.Text = "📎 Attach Image/Document"
        btnAttach.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnAttach.Location = New Point(180, 290)
        btnAttach.Size = New Size(180, 35)
        btnAttach.BackColor = Color.FromArgb(108, 117, 125)
        btnAttach.ForeColor = Color.White
        btnAttach.FlatStyle = FlatStyle.Flat
        btnAttach.Cursor = Cursors.Hand

        Dim lblAttachment As New Label()
        lblAttachment.Name = "lblAttachment"
        lblAttachment.Text = "No file attached"
        lblAttachment.Font = New Font("Segoe UI", 9)
        lblAttachment.Location = New Point(370, 295)
        lblAttachment.Size = New Size(300, 25)
        lblAttachment.ForeColor = Color.Gray

        ' Engagement Feature - Progress Bar
        Dim lblEngagement As New Label()
        lblEngagement.Text = "✨ Complete your report to help your community! ✨"
        lblEngagement.Font = New Font("Segoe UI", 10)
        lblEngagement.ForeColor = Color.FromArgb(40, 167, 69)
        lblEngagement.Location = New Point(20, 350)
        lblEngagement.Size = New Size(660, 25)
        lblEngagement.TextAlign = ContentAlignment.MiddleCenter

        Dim progressBar As New ProgressBar()
        progressBar.Name = "progressBar"
        progressBar.Location = New Point(180, 385)
        progressBar.Size = New Size(500, 20)
        progressBar.Minimum = 0
        progressBar.Maximum = 100
        progressBar.Value = 0

        ' SUBMIT BUTTON - Complete Code
        Dim btnSubmit As New Button()
        btnSubmit.Text = "✓ SUBMIT REQUEST"
        btnSubmit.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnSubmit.Location = New Point(250, 430)
        btnSubmit.Size = New Size(200, 45)
        btnSubmit.BackColor = Color.FromArgb(40, 167, 69)
        btnSubmit.ForeColor = Color.White
        btnSubmit.FlatStyle = FlatStyle.Flat
        btnSubmit.Cursor = Cursors.Hand

        ' BACK TO MAIN MENU BUTTON - Complete Code
        Dim btnBack As New Button()
        btnBack.Text = "← Back to Main Menu"
        btnBack.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnBack.Location = New Point(20, 500)
        btnBack.Size = New Size(180, 40)
        btnBack.BackColor = Color.FromArgb(108, 117, 125)
        btnBack.ForeColor = Color.White
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.Cursor = Cursors.Hand

        ' Add controls to panel
        pnlMain.Controls.AddRange(New Control() {
            lblName, txtName, lblLocation, txtLocation,
            lblIssueType, cmbIssueType, lblDescription, txtDescription,
            btnAttach, lblAttachment, lblEngagement, progressBar,
            btnSubmit, btnBack
        })

        Me.Controls.Add(pnlHeader)
        Me.Controls.Add(pnlMain)

        ' ============================================
        ' ATTACH FILE BUTTON CLICK EVENT
        ' ============================================
        AddHandler btnAttach.Click, Sub()
                                        ' Create OpenFileDialog for file selection
                                        Using openFileDialog As New OpenFileDialog()
                                            ' Set dialog properties
                                            openFileDialog.Title = "Select an Image or Document to Attach"
                                            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" &
                                                                   "Document Files|*.pdf;*.doc;*.docx;*.txt|" &
                                                                   "All Files|*.*"
                                            openFileDialog.FilterIndex = 1
                                            openFileDialog.Multiselect = False
                                            openFileDialog.RestoreDirectory = True

                                            ' Show the dialog and check if user clicked OK
                                            If openFileDialog.ShowDialog() = DialogResult.OK Then
                                                ' Get the selected file path
                                                selectedImagePath = openFileDialog.FileName

                                                ' Convert file to byte array for storage
                                                selectedImageBytes = IO.File.ReadAllBytes(selectedImagePath)

                                                ' Display success message with file info
                                                Dim fileInfo As New IO.FileInfo(selectedImagePath)
                                                Dim fileSizeKB As Long = fileInfo.Length \ 1024

                                                lblAttachment.Text = "✓ Attached: " & fileInfo.Name & " (" & fileSizeKB & " KB)"
                                                lblAttachment.ForeColor = Color.Green
                                                lblAttachment.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                                                ' Show confirmation message
                                                MessageBox.Show($"File attached successfully!{vbCrLf}{vbCrLf}" &
                                                               $"File Name: {fileInfo.Name}{vbCrLf}" &
                                                               $"File Size: {fileSizeKB} KB{vbCrLf}" &
                                                               $"Location: {selectedImagePath}",
                                                               "Attachment Successful",
                                                               MessageBoxButtons.OK,
                                                               MessageBoxIcon.Information)
                                            End If
                                        End Using
                                        End AddHandler

                                        ' ============================================
                                        ' SUBMIT BUTTON CLICK EVENT
                                        ' ============================================
                                        AddHandler btnSubmit.Click, Sub()
                                                                        ' Validate all required fields
                                                                        If String.IsNullOrWhiteSpace(txtName.Text) Then
                                                                            MessageBox.Show("Please enter your full name.", "Validation Error",
                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                            txtName.Focus()
                                                                            Return
                                                                        End If

                                                                        If String.IsNullOrWhiteSpace(txtLocation.Text) Then
                                                                            MessageBox.Show("Please enter the location of the issue.", "Validation Error",
                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                            txtLocation.Focus()
                                                                            Return
                                                                        End If

                                                                        If cmbIssueType.SelectedIndex <= 0 Then
                                                                            MessageBox.Show("Please select an issue type from the dropdown.", "Validation Error",
                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                            cmbIssueType.Focus()
                                                                            Return
                                                                        End If

                                                                        If String.IsNullOrWhiteSpace(txtDescription.Text) Then
                                                                            MessageBox.Show("Please provide a detailed description of the issue.", "Validation Error",
                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                            txtDescription.Focus()
                                                                            Return
                                                                        End If

                                                                        ' Create new service request
                                                                        Dim newRequest As New ServiceRequest()
                                                                        newRequest.RequestID = GetNextRequestID()
                                                                        newRequest.CitizenName = txtName.Text.Trim()
                                                                        newRequest.Location = txtLocation.Text.Trim()
                                                                        newRequest.IssueType = cmbIssueType.SelectedItem.ToString()
                                                                        newRequest.Description = txtDescription.Text.Trim()
                                                                        newRequest.AttachmentPath = selectedImagePath
                                                                        newRequest.AttachmentData = selectedImageBytes
                                                                        newRequest.SubmissionDate = DateTime.Now
                                                                        newRequest.Status = "Pending"

                                                                        ' Add to list
                                                                        reportedIssues.Add(newRequest)

                                                                        ' Save to file
                                                                        SaveToFile(newRequest)

                                                                        ' Show success message with engagement feature
                                                                        Dim engagementMessage As String = ""
                                                                        If progressBar.Value = 100 Then
                                                                            engagementMessage = "🌟 Excellent! You've provided all the details needed for quick processing!"
                                                                        ElseIf progressBar.Value >= 75 Then
                                                                            engagementMessage = "👍 Great job! Your report is nearly complete!"
                                                                        Else
                                                                            engagementMessage = "✅ Thank you for reporting! We'll look into this matter."
                                                                        End If

                                                                        MessageBox.Show($"✓ REPORT SUBMITTED SUCCESSFULLY!{vbCrLf}{vbCrLf}" &
                                                                                       $"Reference Number: MSA-{newRequest.RequestID:D6}{vbCrLf}" &
                                                                                       $"Citizen: {newRequest.CitizenName}{vbCrLf}" &
                                                                                       $"Issue Type: {newRequest.IssueType}{vbCrLf}" &
                                                                                       $"Status: {newRequest.Status}{vbCrLf}" &
                                                                                       $"Date: {newRequest.SubmissionDate:dd MMM yyyy HH:mm}{vbCrLf}{vbCrLf}" &
                                                                                       $"{engagementMessage}{vbCrLf}{vbCrLf}" &
                                                                                       $"💡 Tip: You can track this request using the Reference Number in the 'Service Request Status' section.",
                                                                                       "Submission Successful",
                                                                                       MessageBoxButtons.OK,
                                                                                       MessageBoxIcon.Information)

                                                                        ' Clear the form for next submission
                                                                        txtName.Clear()
                                                                        txtLocation.Clear()
                                                                        cmbIssueType.SelectedIndex = 0
                                                                        txtDescription.Clear()
                                                                        lblAttachment.Text = "No file attached"
                                                                        lblAttachment.ForeColor = Color.Gray
                                                                        selectedImagePath = ""
                                                                        selectedImageBytes = Nothing
                                                                        progressBar.Value = 0

                                                                        ' Optional: Focus back to name field
                                                                        txtName.Focus()
                                                                    End Sub

                                        ' ============================================
                                        ' BACK TO MAIN MENU BUTTON CLICK EVENT
                                        ' ============================================
                                        AddHandler btnBack.Click, Sub()
                                                                      ' Ask for confirmation if form has data
                                                                      Dim hasData As Boolean = Not String.IsNullOrWhiteSpace(txtName.Text) OrElse
                                                                                              Not String.IsNullOrWhiteSpace(txtLocation.Text) OrElse
                                                                                              cmbIssueType.SelectedIndex > 0 OrElse
                                                                                              Not String.IsNullOrWhiteSpace(txtDescription.Text) OrElse
                                                                                              Not String.IsNullOrWhiteSpace(selectedImagePath)

                                                                      If hasData Then
                                                                          Dim result As DialogResult = MessageBox.Show(
                                                                              "You have unsaved data in this form." & vbCrLf & vbCrLf &
                                                                              "Are you sure you want to go back to the Main Menu? Any unsaved information will be lost.",
                                                                              "Confirm Navigation",
                                                                              MessageBoxButtons.YesNo,
                                                                              MessageBoxIcon.Question)

                                                                          If result = DialogResult.Yes Then
                                                                              Me.Close() ' Close this form and return to main menu
                                                                          End If
                                                                      Else
                                                                          Me.Close() ' No data, just close
                                                                      End If
                                                                  End Sub

                                        ' Real-time progress update
                                        AddHandler txtName.TextChanged, Sub() UpdateProgress(txtName, txtLocation, cmbIssueType, txtDescription, progressBar)
                                        AddHandler txtLocation.TextChanged, Sub() UpdateProgress(txtName, txtLocation, cmbIssueType, txtDescription, progressBar)
                                        AddHandler cmbIssueType.SelectedIndexChanged, Sub() UpdateProgress(txtName, txtLocation, cmbIssueType, txtDescription, progressBar)
                                        AddHandler txtDescription.TextChanged, Sub() UpdateProgress(txtName, txtLocation, cmbIssueType, txtDescription, progressBar)
                                    End Sub

    Private Sub UpdateProgress(txtName As TextBox, txtLocation As TextBox, cmbIssueType As ComboBox, txtDescription As RichTextBox, progressBar As ProgressBar)
        Dim completed As Integer = 0
        Dim total As Integer = 4

        If Not String.IsNullOrWhiteSpace(txtName.Text) Then completed += 1
        If Not String.IsNullOrWhiteSpace(txtLocation.Text) Then completed += 1
        If cmbIssueType.SelectedIndex > 0 Then completed += 1
        If Not String.IsNullOrWhiteSpace(txtDescription.Text) Then completed += 1

        progressBar.Value = CInt((completed / total) * 100)
    End Sub

    Private Function GetNextRequestID() As Integer
        Dim maxID As Integer = 0
        Dim filePath As String = "ServiceRequests.txt"

        If IO.File.Exists(filePath) Then
            Dim lines As String() = IO.File.ReadAllLines(filePath)
            For Each line In lines
                Dim parts As String() = line.Split("|"c)
                If parts.Length >= 1 Then
                    Dim id As Integer
                    If Integer.TryParse(parts(0), id) AndAlso id > maxID Then
                        maxID = id
                    End If
                End If
            Next
        End If

        For Each req In reportedIssues
            If req.RequestID > maxID Then
                maxID = req.RequestID
            End If
        Next

        Return maxID + 1
    End Function

    Private Sub SaveToFile(request As ServiceRequest)
        Dim filePath As String = "ServiceRequests.txt"
        ' Format: ID|Name|Location|IssueType|Description|AttachmentPath|SubmissionDate|Status
        Dim line As String = $"{request.RequestID}|{request.CitizenName}|{request.Location}|{request.IssueType}|{request.Description}|{request.AttachmentPath}|{request.SubmissionDate:yyyy-MM-dd HH:mm:ss}|{request.Status}"

        Try
            Using writer As New IO.StreamWriter(filePath, True)
                writer.WriteLine(line)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving request: " & ex.Message, "Save Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Public method to get all reported issues
    Public Shared Function GetAllRequests() As List(Of ServiceRequest)
        Return reportedIssues
    End Function

    Public Shared Sub LoadFromFile()
        Dim filePath As String = "ServiceRequests.txt"
        If IO.File.Exists(filePath) Then
            Dim lines As String() = IO.File.ReadAllLines(filePath)
            For Each line In lines
                Dim parts As String() = line.Split("|"c)
                If parts.Length >= 8 Then
                    Dim request As New ServiceRequest()
                    Integer.TryParse(parts(0), request.RequestID)
                    request.CitizenName = parts(1)
                    request.Location = parts(2)
                    request.IssueType = parts(3)
                    request.Description = parts(4)
                    request.AttachmentPath = parts(5)
                    DateTime.TryParse(parts(6), request.SubmissionDate)
                    request.Status = parts(7)
                    reportedIssues.Add(request)
                End If
            Next
        End If
    End Sub
End Class

' Service Request Class
Public Class ServiceRequest
    Public Property RequestID As Integer
    Public Property CitizenName As String
    Public Property Location As String
    Public Property IssueType As String
    Public Property Description As String
    Public Property AttachmentPath As String
    Public Property AttachmentData As Byte()
    Public Property SubmissionDate As DateTime
    Public Property Status As String
End Class
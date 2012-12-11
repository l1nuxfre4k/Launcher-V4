Imports System
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Drawing

Public Class MainForm

    Dim CommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs

    Dim MCDir As String
    Dim LError As String
    Dim LUserFinal As String
    Dim LSessionIDFinal As String

    Dim CurrentLauncherVersion As String = ProductVersion
    Dim NewLauncherVersion As String = ""
    Dim CurrentMOD1Version As String = ""
    Dim NewMOD1Version As String = ""
    Dim CurrentMOD2Version As String = ""
    Dim NewMOD2Version As String = ""

    Dim ServerAdminList As String = ""
    Dim UserIsAdmin As Boolean
    Dim CurrentMOD1IsAdmin As Boolean
    Dim CurrentMOD2IsAdmin As Boolean
    Dim ServerIsReady As Boolean = False

    Dim NumberOfNewsItems As Integer
    Dim NewNewsItems As Array
    Dim OldNewsSt As String = ""
    Dim NewNewsSt As String = ""

    Dim NumberOfPlayers As Integer
    Dim NewPlayerList As Array
    Dim OldPlayersSt As String = ""
    Dim NewPlayersSt As String = ""
    Dim StName As String = "ftb.php"

    Dim NewStatusFirst As String = ""
    Dim OldStatusFirst As String = ""

    Dim WithEvents WC1 As New WebClient
    Dim WithEvents WC2 As New WebClient

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click, RadioButton1.Click, RadioButton2.Click, Logo.Click, BetaTesting.Click, BetaLogo.Click, BetaText.Click, BetaWarning.Click, Button1.Click, Button2.Click
        My.Computer.Audio.Play("click.wav", AudioPlayMode.Background)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.First = True Then
            My.Settings.Upgrade()
            My.Settings.First = False
        End If
        Try
            If CommandLineArgs(0) = "" Then
            Else
                If CommandLineArgs(0) = "auto" Then
                Else
                    If CommandLineArgs(0) = "update" Then
                    Else : My.Settings.User = CommandLineArgs(0)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
        Try
            If CommandLineArgs(1) = "" Then
            Else : My.Settings.Password = CommandLineArgs(1)
            End If
        Catch ex As Exception
        End Try
        VersionLabel.Text = "MineUK Launcher V" & ProductVersion & " Beta"
        TextBox1.Text = My.Settings.User
        TextBox2.Text = My.Settings.Password
        BetaTesting.Show()
        If My.Settings.RAM = 512 Then
        ElseIf My.Settings.RAM = 1024 Then
        ElseIf My.Settings.RAM = 2048 Then
        ElseIf My.Settings.RAM = 4096 Then
        Else : My.Settings.RAM = 1024
        End If
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath.Chars(0) & ":\Program Files\Java\jre7\bin\javaw.exe") Then
        Else
            MsgBox("You need Java 7." & Environment.NewLine & "You cannot continue without it!")
            End
        End If
        My.Settings.Save()
        If My.Settings.ModPack = "FTB" Then
            RadioButton1.Checked = True
            StName = "ftb.php"
        End If
        If My.Settings.ModPack = "Vanilla" Then
            RadioButton2.Checked = True
            StName = "vanilla.php"
        End If
        Try
            If CommandLineArgs(0) = "auto" Then
                Button1.Enabled = False
                Button2.Enabled = False
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                CheckBox1.Enabled = False
                My.Settings.User = TextBox1.Text
                My.Settings.Password = TextBox2.Text
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
            End If
            If CommandLineArgs(2) = "auto" Then
                Button1.Enabled = False
                Button2.Enabled = False
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                CheckBox1.Enabled = False
                My.Settings.User = TextBox1.Text
                My.Settings.Password = TextBox2.Text
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
            End If
            If CommandLineArgs(0) = "update" Then
                CheckBox1.Checked = True
                Button1.Enabled = False
                Button2.Enabled = False
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                CheckBox1.Enabled = False
                My.Settings.User = TextBox1.Text
                My.Settings.Password = TextBox2.Text
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
            End If
            If CommandLineArgs(2) = "update" Then
                CheckBox1.Checked = True
                Button1.Enabled = False
                Button2.Enabled = False
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                CheckBox1.Enabled = False
                My.Settings.User = TextBox1.Text
                My.Settings.Password = TextBox2.Text
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
            End If
        Catch ex As Exception
        End Try
        BackgroundWorkerUpdate1.RunWorkerAsync()
        BackgroundWorkerNews.RunWorkerAsync()
        BackgroundStatus.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerNews_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerNews.DoWork
        Dim ServerStream As Stream
        Dim myWebClient As New WebClient()
        Try
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v3/news.php")
            Dim sr As New StreamReader(ServerStream)
            Dim ServerData = sr.ReadLine.ToString
            ServerStream.Close()
            sr.Close()
            NumberOfNewsItems = (ServerData.Split(":").Length() - 2)
            NewNewsItems = ServerData.Split(":")
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
        End Try
        NewNewsSt = ""
        Try
            For i As Integer = 1 To NumberOfNewsItems
                NewNewsSt = NewNewsSt + NewNewsItems(i)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorkerNews_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerNews.RunWorkerCompleted
        If OldNewsSt = NewNewsSt Then
        Else
            Try
                SplitContainer1.Panel1.Controls.Clear()
                For i As Integer = 1 To NumberOfNewsItems
                    Dim News1 As New Panel
                    News1.AutoSize = False
                    News1.Dock = DockStyle.Top
                    News1.Size = New Size(100, 32)
                    News1.Padding = New Padding(2)
                    If (NumberOfNewsItems - (i - 1)) Mod 2 = 1 Then
                        News1.BackColor = Color.FromKnownColor(KnownColor.ControlDark)
                    Else : News1.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                    End If
                    SplitContainer1.Panel1.Controls.Add(News1)
                    Dim Text1 As New Label()
                    Text1.Text = NewNewsItems(NumberOfNewsItems - (i - 1))
                    Text1.Dock = DockStyle.Fill
                    Text1.AutoSize = False
                    Text1.TextAlign = ContentAlignment.MiddleLeft
                    Text1.Font = New Font("Helvetica", 10, FontStyle.Regular)
                    News1.Controls.Add(Text1)
                Next
                Dim NewsX As New Panel
                NewsX.AutoSize = False
                NewsX.Dock = DockStyle.Top
                NewsX.Size = New Size(100, 35)
                NewsX.Padding = New Padding(2)
                NewsX.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                SplitContainer1.Panel1.Controls.Add(NewsX)
                Dim Text2 As New Label()
                Text2.Text = "Latest News From MineUK"
                Text2.Dock = DockStyle.Fill
                Text2.AutoSize = False
                Text2.TextAlign = ContentAlignment.MiddleCenter
                Text2.Font = New Font("Helvetica", 12, FontStyle.Bold)
                NewsX.Controls.Add(Text2)
            Catch ex As Exception
            End Try
        End If
        OldNewsSt = NewNewsSt
        BackgroundWorkerNewsWait.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerNewsWait_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerNewsWait.DoWork
        Thread.Sleep(30000)
    End Sub

    Private Sub BackgroundWorkerNewsWait_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerNewsWait.RunWorkerCompleted
        BackgroundWorkerNews.RunWorkerAsync()
    End Sub

    Private Sub BackgroundStatus_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundStatus.DoWork
        Dim ServerStream As Stream
        Dim myWebClient As New WebClient()
        Try
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v3/" & StName)
            Dim sr As New StreamReader(ServerStream)
            Dim ServerData = sr.ReadToEnd.ToString
            ServerStream.Close()
            sr.Close()
            NumberOfPlayers = (ServerData.Split(":").Length() - 2)
            NewPlayerList = ServerData.Split(":")
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
        End Try
        NewPlayersSt = ""
        Try
            For i As Integer = 5 To NumberOfPlayers
                NewPlayersSt = NewPlayersSt + NewPlayerList(i)
            Next
        Catch ex As Exception
        End Try
        NewStatusFirst = ""
        Try
            For i As Integer = 1 To 4
                NewStatusFirst = NewStatusFirst + NewPlayerList(i)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundStatus_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundStatus.RunWorkerCompleted
        If OldStatusFirst = NewStatusFirst Then
        Else
            Panel1.Controls.Clear()
            Try
                For i As Integer = 1 To 4
                    Dim News1 As New Panel
                    News1.AutoSize = False
                    News1.Dock = DockStyle.Top
                    News1.Size = New Size(100, 32)
                    News1.Padding = New Padding(2)
                    If (4 - (i - 1)) Mod 2 = 1 Then
                        News1.BackColor = Color.FromKnownColor(KnownColor.ControlDark)
                    Else : News1.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                    End If
                    Panel1.Controls.Add(News1)
                    Dim Text1 As New Label()
                    If (4 - (i - 1)) = 1 Then
                        Text1.Text = "FTB Server: "
                    End If
                    If (4 - (i - 1)) = 2 Then
                        Text1.Text = "Vanilla Server: "
                    End If
                    If (4 - (i - 1)) = 3 Then
                        Text1.Text = "Capes Service: "
                    End If
                    If (4 - (i - 1)) = 4 Then
                        Text1.Text = "Skins Service: "
                    End If
                    If (NewPlayerList(4 - (i - 1))).ToString.Contains("1") Then
                        Text1.Text = Text1.Text + "Online"
                    Else
                        Text1.Text = Text1.Text + "Reported Problems!"
                    End If
                    Text1.Dock = DockStyle.Fill
                    Text1.AutoSize = False
                    Text1.TextAlign = ContentAlignment.MiddleLeft
                    Text1.Font = New Font("Helvetica", 10, FontStyle.Regular)
                    News1.Controls.Add(Text1)
                Next
            Catch ex As Exception
            End Try
            Try
                Dim NewsX As New Panel
                NewsX.AutoSize = False
                NewsX.Dock = DockStyle.Top
                NewsX.Size = New Size(100, 35)
                NewsX.Padding = New Padding(2)
                NewsX.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                Panel1.Controls.Add(NewsX)
                Dim Text2 As New Label()
                Text2.Text = "MineUK Server Status"
                Text2.Dock = DockStyle.Fill
                Text2.AutoSize = False
                Text2.TextAlign = ContentAlignment.MiddleCenter
                Text2.Font = New Font("Helvetica", 12, FontStyle.Bold)
                NewsX.Controls.Add(Text2)
            Catch ex As Exception
            End Try
        End If
        If OldPlayersSt = NewPlayersSt Then
        Else
            Panel2.Controls.Clear()
            Try
                Dim NewsX As New Panel
                NewsX.AutoSize = False
                NewsX.Dock = DockStyle.Top
                NewsX.Size = New Size(100, 12)
                Panel2.Controls.Add(NewsX)
            Catch ex As Exception
            End Try
            Try
                For i As Integer = 5 To NumberOfPlayers
                    Dim News1 As New Panel
                    News1.AutoSize = False
                    News1.Dock = DockStyle.Top
                    News1.Size = New Size(100, 32)
                    News1.Padding = New Padding(2)
                    If (NumberOfPlayers - (i - 5)) Mod 2 = 1 Then
                        News1.BackColor = Color.FromKnownColor(KnownColor.ControlDark)
                    Else : News1.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                    End If
                    Panel2.Controls.Add(News1)
                    Dim Text1 As New Label()
                    Text1.Text = NewPlayerList(NumberOfPlayers - (i - 5))
                    Text1.Dock = DockStyle.Fill
                    Text1.AutoSize = False
                    Text1.TextAlign = ContentAlignment.MiddleLeft
                    Text1.Font = New Font("Helvetica", 10, FontStyle.Regular)
                    News1.Controls.Add(Text1)
                Next
            Catch ex As Exception
            End Try
            Try
                Dim NewsX As New Panel
                NewsX.AutoSize = False
                NewsX.Dock = DockStyle.Top
                NewsX.Size = New Size(100, 35)
                NewsX.Padding = New Padding(2)
                NewsX.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace)
                Panel2.Controls.Add(NewsX)
                Dim Text2 As New Label()
                If RadioButton1.Checked = True Then
                    Text2.Text = "Players On The FTB Server"
                Else : Text2.Text = "Players On The Vanilla Server"
                End If
                Text2.Dock = DockStyle.Fill
                Text2.AutoSize = False
                Text2.TextAlign = ContentAlignment.MiddleCenter
                Text2.Font = New Font("Helvetica", 12, FontStyle.Bold)
                NewsX.Controls.Add(Text2)
            Catch ex As Exception
            End Try
            Try
                Dim NewsX As New Panel
                NewsX.AutoSize = False
                NewsX.Dock = DockStyle.Top
                NewsX.Size = New Size(100, 12)
                Panel2.Controls.Add(NewsX)
            Catch ex As Exception
            End Try
        End If
        OldPlayersSt = NewPlayersSt
        OldStatusFirst = NewStatusFirst
        BackgroundStatusWait.RunWorkerAsync()
    End Sub

    Private Sub BackgroundStatusWait_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundStatusWait.DoWork
        Thread.Sleep(100)
    End Sub

    Private Sub BackgroundStatusWait_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundStatusWait.RunWorkerCompleted
        BackgroundStatus.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerUpdate1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUpdate1.DoWork
        If My.Computer.FileSystem.FileExists("files.7z") Then
            My.Computer.FileSystem.DeleteFile("files.7z")
        End If
        If My.Computer.FileSystem.FileExists("script.bat") Then
            My.Computer.FileSystem.DeleteFile("script.bat")
        End If
        If My.Computer.FileSystem.DirectoryExists("FTB\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("FTB\.minecraft")
        End If
        If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
        End If
        Dim ServerStream As Stream
        Dim myWebClient As New WebClient()
        Try
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v3")
            Dim sr As New StreamReader(ServerStream)
            Dim ServerData = sr.ReadLine.ToString
            ServerStream.Close()
            sr.Close()
            NewLauncherVersion = ServerData.Split(":")(1)
            NewMOD1Version = ServerData.Split(":")(2)
            NewMOD2Version = ServerData.Split(":")(3)
            ServerAdminList = ServerData.Split(":")(4)
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
        End Try
        CurrentMOD1IsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("FTB\.minecraft\version")
                CurrentMOD1Version = Path.GetFileName(i)
            Next
            If CurrentMOD1Version.Substring(CurrentMOD1Version.Length - 1) = "+" Then
                CurrentMOD1IsAdmin = True
                CurrentMOD1Version = CurrentMOD1Version.Remove(CurrentMOD1Version.Length - 1)
            End If
        Catch ex As Exception
        End Try
        CurrentMOD2IsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft\version")
                CurrentMOD2Version = Path.GetFileName(i)
            Next
            If CurrentMOD2Version.Substring(CurrentMOD2Version.Length - 1) = "+" Then
                CurrentMOD2IsAdmin = True
                CurrentMOD2Version = CurrentMOD2Version.Remove(CurrentMOD2Version.Length - 1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorkerUpdate1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdate1.RunWorkerCompleted
        If NewLauncherVersion = "" Then
            MsgBox("MineUK is unreachable!")
            Me.Close()
        End If
        BackgroundWorkerUpdate2.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerUpdate2_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUpdate2.DoWork
        'Start of file check script
        Try
            If My.Computer.FileSystem.DirectoryExists(".minecraft") Then
            Else : My.Computer.FileSystem.DeleteDirectory(".minecraft", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("FTB\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("FTB\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("script.bat") Then
                My.Computer.FileSystem.DeleteFile("script.bat")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("files.7z") Then
                My.Computer.FileSystem.DeleteFile("files.7z")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("FTB\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        'End of file check script
        ServerIsReady = True
    End Sub

    Private Sub BackgroundWorkerUpdate2_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdate2.RunWorkerCompleted
        Try
            If CommandLineArgs(0) = "auto" Then
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "auto" Then
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(0) = "update" Then
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "update" Then
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaTesting.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.ModPack = "FTB"
            StName = "ftb.php"
        End If
        If RadioButton2.Checked = True Then
            My.Settings.ModPack = "Vannila"
            StName = "vanilla.php"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Enabled = False
        My.Settings.User = TextBox1.Text
        My.Settings.Password = TextBox2.Text
        OptionsForm.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Enabled = False
        Button2.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        CheckBox1.Enabled = False
        My.Settings.User = TextBox1.Text
        My.Settings.Password = TextBox2.Text
        StatusLabel.Text = "Checking files..."
        ProgressBar1.Style = ProgressBarStyle.Marquee
        BetaTesting.Hide()
        If ServerIsReady = True Then
            ServerIsReady = False
            BackgroundWorkerUpdate1.RunWorkerAsync()
        End If
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Start of file check script
        Try
            If My.Computer.FileSystem.DirectoryExists(".minecraft") Then
            Else : My.Computer.FileSystem.DeleteDirectory(".minecraft", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("FTB\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("FTB\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("script.bat") Then
                My.Computer.FileSystem.DeleteFile("script.bat")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("files.7z") Then
                My.Computer.FileSystem.DeleteFile("files.7z")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("FTB\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        'End of file check script
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        StatusLabel.Text = "Logging in..."
        BackgroundWorkerMojang.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerMojang_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerMojang.DoWork
        'Set strings
        LError = ""
        MCDir = ""
        Dim ServerStream As Stream
        Dim ServerString
        Dim ServerArray As Array
        Dim myWebClient As New WebClient()
        'Login
        Try
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v3/login.php/?user=" & My.Settings.User & "&password=" & My.Settings.Password)
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                LError = ("3")
            End If
        End Try
        Try
            Dim sr As New StreamReader(ServerStream)
            ServerString = (sr.ReadToEnd())
            ServerStream.Close()
            sr.Close()
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                If LError = ("3") Then
                Else : LError = ("2")
                End If
            End If
        End Try
        Try
            ServerArray = ServerString.Split(":")
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                If LError = ("3") Then
                Else : LError = ("2")
                End If
            End If
        End Try
        LUserFinal = ""
        LSessionIDFinal = ""
        Try
            LUserFinal = ServerArray(3).ToString
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                If LError = ("3") Then
                Else : LError = ("2")
                End If
            End If
        End Try
        Try
            LSessionIDFinal = ServerArray(4).ToString
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                If LError = ("3") Then
                Else : LError = ("2")
                End If
            End If
        End Try
    End Sub

    Private Sub BackgroundWorkerMojang_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerMojang.RunWorkerCompleted
        ProgressBar1.Value = 100
        If LError = "2" Then
            StatusLabel.Text = ("ERROR!")
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            MsgBox("Your login was rejected!")
            BetaTesting.Show()
            Button1.Enabled = True
            Button2.Enabled = True
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            CheckBox1.Enabled = True
        End If
        If LError = "3" Then
            StatusLabel.Text = ("ERROR!")
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            MsgBox("The login servers are unreachable!")
            BetaTesting.Show()
            Button1.Enabled = True
            Button2.Enabled = True
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            CheckBox1.Enabled = True
        End If
        If LError = "" Then
            StatusLabel.Text = "Waiting for server..."
            BackgroundWorker2.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Do While ServerIsReady = False
            Thread.Sleep(200)
        Loop
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        ProgressBar1.Value = 0
        If NewLauncherVersion = "" Then
            GoTo 1
        End If
        If NewMOD1Version = "" Then
            GoTo 1
        End If
        If NewMOD2Version = "" Then
            GoTo 1
        End If
        If ServerAdminList = "" Then
            GoTo 1
        End If
        If LError = "" Then
        Else : GoTo 1
        End If
        StatusLabel.Text = "Checking file versions..."
        If ServerAdminList.Contains("," & LUserFinal & ",") Then
            UserIsAdmin = True
        Else : UserIsAdmin = False
        End If
        If NewLauncherVersion = CurrentLauncherVersion Then
        Else
            StatusLabel.Text = "Downloading new launcher..."
            ProgressBar1.Style = ProgressBarStyle.Marquee
            WC1.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/installer.exe"), Environ("temp") & "\MineUK Installer.exe")
            GoTo 2
        End If
        If RadioButton1.Checked = True Then
            If UserIsAdmin = True Then
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & "+.7z"), "files.7z")
                    GoTo 2
                End If
                If NewMOD1Version = CurrentMOD1Version Then
                    If CurrentMOD1IsAdmin = False Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & "+.7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & "+.7z"), "files.7z")
                    GoTo 2
                End If
            Else
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & ".7z"), "files.7z")
                    GoTo 2
                End If
                If NewMOD1Version = CurrentMOD1Version Then
                    If CurrentMOD1IsAdmin = True Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & ".7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD1Version & ".7z"), "files.7z")
                    GoTo 2
                End If
            End If
        Else
            If UserIsAdmin = True Then
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & "+.7z"), "files.7z")
                    GoTo 2
                End If
                If NewMOD2Version = CurrentMOD2Version Then
                    If CurrentMOD2IsAdmin = False Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & "+.7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & "+.7z"), "files.7z")
                    GoTo 2
                End If
            Else
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & ".7z"), "files.7z")
                    GoTo 2
                End If
                If NewMOD2Version = CurrentMOD2Version Then
                    If CurrentMOD2IsAdmin = True Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & ".7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewMOD2Version & ".7z"), "files.7z")
                    GoTo 2
                End If
            End If
        End If
        StatusLabel.Text = "Launching..."
        BackgroundWorker6.RunWorkerAsync()
        GoTo 2
1:      StatusLabel.Text = ("ERROR!")
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Blocks
        MsgBox("There was an unknown error!")
        BetaTesting.Show()
        Button1.Enabled = True
        Button2.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        CheckBox1.Enabled = True
2:
    End Sub

    Private Sub WC1_DownloadFileCompleted(sender As Object, e As ComponentModel.AsyncCompletedEventArgs) Handles WC1.DownloadFileCompleted
        StatusLabel.Text = "Installing..."
        My.Settings.Save()
        Dim objProcesss As Process
        objProcesss = New Process()
        objProcesss.StartInfo.WorkingDirectory = Environ("temp")
        objProcesss.StartInfo.FileName = "MineUK Installer.exe"
        objProcesss.StartInfo.Arguments = "update"
        objProcesss.Start()
        objProcesss.Close()
        End
    End Sub

    Private Sub WC2_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC2.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub WC2_DownloadFileCompleted(sender As Object, e As ComponentModel.AsyncCompletedEventArgs) Handles WC2.DownloadFileCompleted
        ProgressBar1.Style = ProgressBarStyle.Marquee
        StatusLabel.Text = "Removing old files..."
        BackgroundWorker3.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        For Each i As String In Directory.GetDirectories(".minecraft")
            If Path.GetFileName(i) = "saves" Then
            Else
                If Path.GetFileName(i) = "stats" Then
                Else
                    If Path.GetFileName(i) = "movies" Then
                    Else : My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                End If
            End If
        Next
        For Each i As String In Directory.GetFiles(".minecraft")
            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
        Next
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        StatusLabel.Text = "Generating new files..."
        BackgroundWorker4.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Dim objProcesss As Process
        objProcesss = New Process()
        objProcesss.StartInfo.FileName = "7za.exe"
        objProcesss.StartInfo.Arguments = "x files.7z * -y"
        objProcesss.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        objProcesss.Start()
        objProcesss.WaitForExit()
        objProcesss.Close()
    End Sub

    Private Sub BackgroundWorker4_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker4.RunWorkerCompleted
        StatusLabel.Text = "Checking files..."
        BackgroundWorker5.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker5_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        Thread.Sleep(200)
        'Start of file check script
        Try
            If My.Computer.FileSystem.DirectoryExists(".minecraft") Then
            Else : My.Computer.FileSystem.DeleteDirectory(".minecraft", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("FTB\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("FTB\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("script.bat") Then
                My.Computer.FileSystem.DeleteFile("script.bat")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists("files.7z") Then
                My.Computer.FileSystem.DeleteFile("files.7z")
            End If
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("FTB\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                If Path.GetFileName(i).Contains("log") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("optifog") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("ForgeModLoader") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
                If Path.GetFileName(i).Contains("lck") Then
                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                End If
            Next
        Catch ex As Exception
        End Try
        'End of file check script
    End Sub

    Private Sub BackgroundWorker5_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker5.RunWorkerCompleted
        StatusLabel.Text = "Launching..."
        BackgroundWorker6.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker6_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork
        Dim Writer As StreamWriter
        'Game script
        If My.Settings.Debug = True Then
            MCDir = ("""" & My.Application.Info.DirectoryPath.Chars(0) & ":\Program Files\Java\jre7\bin\java.exe""" & " -Xms" & (My.Settings.RAM / 2) & "M -Xmx" & (My.Settings.RAM) & "M -cp " & """%AppData%\.minecraft\bin\minecraft.jar;%AppData%\.minecraft\bin\jinput.jar;%AppData%\.minecraft\bin\lwjgl.jar;%AppData%\.minecraft\bin\lwjgl_util.jar""" & " -Djava.library.path=" & """%AppData%\.minecraft\bin\natives""" & " net.minecraft.client.Minecraft ")
        Else : MCDir = ("""" & My.Application.Info.DirectoryPath.Chars(0) & ":\Program Files\Java\jre7\bin\javaw.exe""" & " -Xms" & (My.Settings.RAM / 2) & "M -Xmx" & (My.Settings.RAM) & "M -cp " & """%AppData%\.minecraft\bin\minecraft.jar;%AppData%\.minecraft\bin\jinput.jar;%AppData%\.minecraft\bin\lwjgl.jar;%AppData%\.minecraft\bin\lwjgl_util.jar""" & " -Djava.library.path=" & """%AppData%\.minecraft\bin\natives""" & " net.minecraft.client.Minecraft ")
        End If
        MCDir = (MCDir & LUserFinal & " " & LSessionIDFinal)
        'Write launch info
        Writer = My.Computer.FileSystem.OpenTextFileWriter("script.bat", True)
        Writer.WriteLine("@ECHO OFF")
        Writer.WriteLine("@ECHO OFF")
        If RadioButton1.Checked = True Then
            Writer.WriteLine("title MineUK " & NewMOD1Version & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\FTB")
        Else
            Writer.WriteLine("title MineUK " & NewMOD2Version & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\Vanilla")
        End If
        Writer.WriteLine("CLS")
        Writer.WriteLine(MCDir)
        Writer.WriteLine("CLS")
        Writer.Close()
        'Launch
        Dim ClientStarter As Process
        ClientStarter = New Process()
        ClientStarter.StartInfo.FileName = "script.bat"
        If My.Settings.Debug = True Then
        Else : ClientStarter.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        End If
        ClientStarter.Start()
    End Sub

    Private Sub BackgroundWorker6_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker6.RunWorkerCompleted
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Me.Close()
    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Logo.Click
        Process.Start("http://www.mineuk.com/")
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles BetaTesting.Click, BetaLogo.Click, BetaText.Click, BetaWarning.Click
        Process.Start("http://dev.mineuk.com/launcher")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.User = TextBox1.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        My.Settings.Password = TextBox2.Text
    End Sub

    Private Sub MainForm_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Dim SView As Point = SplitContainer1.Panel2.AutoScrollPosition
        SView.X = SView.X + 20
        SplitContainer1.Panel2.AutoScrollPosition = SView
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If TextBox1.Focused = False Then
            If TextBox2.Focused = False Then
                SplitContainer1.Panel2.Select()
            End If
        End If
    End Sub
End Class

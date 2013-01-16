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
    Dim ServerIsReady As Boolean = False

    Dim CurrentLauncherVersion As String = ProductVersion & " Dev"
    Dim NewLauncherVersion As String = ""

    Dim CurrentDirewolf20Version As String = ""
    Dim NewDirewolf20Version As String = ""
    Dim Direwolf20MCVersion As String = ""
    Dim Direwolf20MCWebVersion As String = ""

    Dim CurrentVanillaVersion As String = ""
    Dim NewVanillaVersion As String = ""
    Dim VanillaMCVersion As String = ""
    Dim VanillaMCWebVersion As String = ""

    Dim ServerAdminList As String = "".ToLower
    Dim UserIsAdmin As Boolean
    Dim CurrentDirewolf20IsAdmin As Boolean
    Dim CurrentVanillaIsAdmin As Boolean

    Dim NumberOfNewsItems As Integer
    Dim NewNewsItems As Array
    Dim OldNewsSt As String = ""
    Dim NewNewsSt As String = ""

    Dim NumberOfPlayers As Integer
    Dim NewPlayerList As Array
    Dim OldPlayersSt As String = ""
    Dim NewPlayersSt As String = ""

    Dim StName As String = "direwolf20"
    Dim NewStatusFirst As String = ""
    Dim OldStatusFirst As String = ""

    Dim WithEvents WC1 As New WebClient
    Dim WithEvents WC2 As New WebClient
    Dim WithEvents WC3 As New WebClient
    Dim WithEvents WC4 As New WebClient

    Private Sub Click_Sounds(sender As Object, e As EventArgs) Handles UpdateCheckBox.Click, RadioButton1.Click, RadioButton2.Click, LogoPanel.Click, OptionsButton.Click, LoginButton.Click
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
        VersionLabel.Text = "MineUK Launcher V" & CurrentLauncherVersion
        UserTextBox.Text = My.Settings.User
        PasswordTextBox.Text = My.Settings.Password
        StatusLabel.Hide()
        ProgressBar1.Hide()
        ProgressLabel.Hide()
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
        If My.Settings.ModPack = "Direwolf20" Then
            RadioButton1.Checked = True
            StName = "direwolf20"
            OldPlayersSt = "BAD"
        End If
        If My.Settings.ModPack = "Vanilla" Then
            RadioButton2.Checked = True
            StName = "vanilla"
            OldPlayersSt = "BAD"
        End If
        Try
            If CommandLineArgs(0) = "auto" Then
                OptionsButton.Enabled = False
                LoginButton.Enabled = False
                UserTextBox.Enabled = False
                PasswordTextBox.Enabled = False
                UpdateCheckBox.Enabled = False
                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                My.Settings.User = UserTextBox.Text
                My.Settings.Password = PasswordTextBox.Text
                StatusLabel.Text = "Connecting To MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
            End If
            If CommandLineArgs(2) = "auto" Then
                OptionsButton.Enabled = False
                LoginButton.Enabled = False
                UserTextBox.Enabled = False
                PasswordTextBox.Enabled = False
                UpdateCheckBox.Enabled = False
                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                My.Settings.User = UserTextBox.Text
                My.Settings.Password = PasswordTextBox.Text
                StatusLabel.Text = "Connecting To MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
            End If
            If CommandLineArgs(0) = "update" Then
                UpdateCheckBox.Checked = True
                OptionsButton.Enabled = False
                LoginButton.Enabled = False
                UserTextBox.Enabled = False
                PasswordTextBox.Enabled = False
                UpdateCheckBox.Enabled = False
                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                My.Settings.User = UserTextBox.Text
                My.Settings.Password = PasswordTextBox.Text
                StatusLabel.Text = "Connecting To MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
            End If
            If CommandLineArgs(2) = "update" Then
                UpdateCheckBox.Checked = True
                OptionsButton.Enabled = False
                LoginButton.Enabled = False
                UserTextBox.Enabled = False
                PasswordTextBox.Enabled = False
                UpdateCheckBox.Enabled = False
                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                My.Settings.User = UserTextBox.Text
                My.Settings.Password = PasswordTextBox.Text
                StatusLabel.Text = "Connecting To MineUK..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
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
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v4/news.php")
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
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v4/" & StName)
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
                        Text1.Text = "Direwolf20 Server: "
                    End If
                    If (4 - (i - 1)) = 2 Then
                        Text1.Text = "Vanilla Server: "
                    End If
                    If (4 - (i - 1)) = 3 Then
                        Text1.Text = "Capes Server: "
                    End If
                    If (4 - (i - 1)) = 4 Then
                        Text1.Text = "Skins Server: "
                    End If
                    If (NewPlayerList(4 - (i - 1))) = "1" Then
                        Text1.Text = Text1.Text + "Online"
                    Else
                        Text1.Text = Text1.Text + "Offline"
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
                NewsX.Size = New Size(100, 13)
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
                    Text2.Text = "Players On The Direwolf20 Server"
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
        If My.Computer.FileSystem.DirectoryExists("Direwolf20\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("Direwolf20\.minecraft")
        End If
        If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
        End If
        Dim ServerStream As Stream
        Dim myWebClient As New WebClient()
        Try
            ServerStream = myWebClient.OpenRead("http://launcher.mineuk.com/v4")
            Dim sr As New StreamReader(ServerStream)
            Dim ServerData = sr.ReadLine.ToString
            ServerStream.Close()
            sr.Close()
            NewLauncherVersion = ServerData.Split(":")(1)
            NewDirewolf20Version = ServerData.Split(":")(2)
            Direwolf20MCVersion = NewDirewolf20Version.Split(" ")(1).Split("-")(0)
            Direwolf20MCWebVersion = Direwolf20MCVersion.Replace(".", "_")
            NewVanillaVersion = ServerData.Split(":")(3)
            VanillaMCVersion = NewVanillaVersion.Split(" ")(1).Split("-")(0)
            VanillaMCWebVersion = VanillaMCVersion.Replace(".", "_")
            ServerAdminList = ServerData.Split(":")(4).ToLower
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
        End Try
        CurrentDirewolf20IsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("Direwolf20\.minecraft\version")
                CurrentDirewolf20Version = Path.GetFileName(i)
            Next
            If CurrentDirewolf20Version.Substring(CurrentDirewolf20Version.Length - 1) = "+" Then
                CurrentDirewolf20IsAdmin = True
                CurrentDirewolf20Version = CurrentDirewolf20Version.Remove(CurrentDirewolf20Version.Length - 1)
            End If
        Catch ex As Exception
        End Try
        CurrentVanillaIsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft\version")
                CurrentVanillaVersion = Path.GetFileName(i)
            Next
            If CurrentVanillaVersion.Substring(CurrentVanillaVersion.Length - 1) = "+" Then
                CurrentVanillaIsAdmin = True
                CurrentVanillaVersion = CurrentVanillaVersion.Remove(CurrentVanillaVersion.Length - 1)
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
            For Each i As String In Directory.GetDirectories(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "Direwolf20" Then
                    Else
                        If Path.GetFileName(i) = "Vanilla" Then
                        Else
                            Try
                                My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Direwolf20\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Direwolf20\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Direwolf20")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Vanilla")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "7za.exe" Then
                    Else
                        If Path.GetFileName(i) = "click.wav" Then
                        Else
                            If Path.GetFileName(i) = "MineUK Launcher.exe" Then
                            Else
                                If Path.GetFileName(i) = "MineUK Launcher.exe.config" Then
                                Else
                                    If Path.GetFileName(i) = "MineUK Launcher.xml" Then
                                    Else
                                        Try
                                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                                        Catch ex As Exception
                                        End Try
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                Try
                    My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Direwolf20\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        'End of file check script
        ServerIsReady = True
    End Sub

    Private Sub BackgroundWorkerUpdate2_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdate2.RunWorkerCompleted
        Try
            If CommandLineArgs(0) = "auto" Then
                StatusLabel.Text = "Checking Files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "auto" Then
                StatusLabel.Text = "Checking Files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(0) = "update" Then
                StatusLabel.Text = "Checking Files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "update" Then
                StatusLabel.Text = "Checking Files..."
                ProgressBar1.Style = ProgressBarStyle.Marquee
                StatusLabel.Show()
                ProgressBar1.Show()
                ProgressLabel.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.ModPack = "Direwolf20"
            StName = "direwolf20"
            OldPlayersSt = "BAD"
        End If
        If RadioButton2.Checked = True Then
            My.Settings.ModPack = "Vanilla"
            StName = "vanilla"
            OldPlayersSt = "BAD"
        End If
    End Sub

    Private Sub OptionsButton_Click(sender As Object, e As EventArgs) Handles OptionsButton.Click
        Me.Enabled = False
        My.Settings.User = UserTextBox.Text
        My.Settings.Password = PasswordTextBox.Text
        OptionsForm.Show()
    End Sub

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        OptionsButton.Enabled = False
        LoginButton.Enabled = False
        UserTextBox.Enabled = False
        PasswordTextBox.Enabled = False
        UpdateCheckBox.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        My.Settings.User = UserTextBox.Text
        My.Settings.Password = PasswordTextBox.Text
        StatusLabel.Text = "Checking Files..."
        ProgressBar1.Style = ProgressBarStyle.Marquee
        StatusLabel.Show()
        ProgressBar1.Show()
        ProgressLabel.Hide()
        If ServerIsReady = True Then
            ServerIsReady = False
            BackgroundWorkerUpdate1.RunWorkerAsync()
        End If
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Start of file check script
        Try
            For Each i As String In Directory.GetDirectories(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "Direwolf20" Then
                    Else
                        If Path.GetFileName(i) = "Vanilla" Then
                        Else
                            Try
                                My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Direwolf20\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Direwolf20\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Direwolf20")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Vanilla")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "7za.exe" Then
                    Else
                        If Path.GetFileName(i) = "click.wav" Then
                        Else
                            If Path.GetFileName(i) = "MineUK Launcher.exe" Then
                            Else
                                If Path.GetFileName(i) = "MineUK Launcher.exe.config" Then
                                Else
                                    If Path.GetFileName(i) = "MineUK Launcher.xml" Then
                                    Else
                                        Try
                                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                                        Catch ex As Exception
                                        End Try
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                Try
                    My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Direwolf20\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        'End of file check script
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        StatusLabel.Text = "Logging In..."
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
            ServerStream = myWebClient.OpenRead("http://login.minecraft.net/?user=" & My.Settings.User & "&password=" & My.Settings.Password & "&version=13")
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
        LUserFinal = "".ToLower
        LSessionIDFinal = "".ToLower
        Try
            LUserFinal = ServerArray(2).ToString.ToLower
        Catch ex As Exception
            If ex.Message = "" Then
            Else
                If LError = ("3") Then
                Else : LError = ("2")
                End If
            End If
        End Try
        Try
            LSessionIDFinal = ServerArray(3).ToString.ToLower
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
        ProgressLabel.Text = ProgressBar1.Value & "%"
        If LError = "2" Then
            StatusLabel.Text = ("ERROR!")
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            ProgressLabel.Text = ("0%")
            ProgressLabel.Hide()
            MsgBox("Your login was rejected!")
            StatusLabel.Hide()
            ProgressBar1.Hide()
            OptionsButton.Enabled = True
            LoginButton.Enabled = True
            UserTextBox.Enabled = True
            PasswordTextBox.Enabled = True
            UpdateCheckBox.Enabled = True
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
        End If
        If LError = "3" Then
            StatusLabel.Text = ("ERROR!")
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            ProgressLabel.Text = ("0%")
            ProgressLabel.Hide()
            MsgBox("The login servers are unreachable!")
            StatusLabel.Hide()
            ProgressBar1.Hide()
            OptionsButton.Enabled = True
            LoginButton.Enabled = True
            UserTextBox.Enabled = True
            PasswordTextBox.Enabled = True
            UpdateCheckBox.Enabled = True
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
        End If
        If LError = "" Then
            StatusLabel.Text = "Waiting For Server..."
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
        ProgressLabel.Text = ProgressBar1.Value & "%"
        If NewLauncherVersion = "" Then
            GoTo 1
        End If
        If NewDirewolf20Version = "" Then
            GoTo 1
        End If
        If Direwolf20MCVersion = "" Then
            GoTo 1
        End If
        If Direwolf20MCWebVersion = "" Then
            GoTo 1
        End If
        If NewVanillaVersion = "" Then
            GoTo 1
        End If
        If VanillaMCVersion = "" Then
            GoTo 1
        End If
        If VanillaMCWebVersion = "" Then
            GoTo 1
        End If
        If ServerAdminList = "" Then
            GoTo 1
        End If
        If LError = "" Then
        Else : GoTo 1
        End If
        StatusLabel.Text = "Checking File Versions..."
        If ServerAdminList.Contains("," & LUserFinal & ",") Then
            UserIsAdmin = True
        Else : UserIsAdmin = False
        End If
        If NewLauncherVersion = CurrentLauncherVersion Then
        Else
            StatusLabel.Text = "Downloading The New Launcher..."
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressLabel.Hide()
            WC1.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/installer.exe"), Environ("temp") & "\MineUK Installer.exe")
            GoTo 2
        End If
        If RadioButton1.Checked = True Then
            If UserIsAdmin = True Then
                If UpdateCheckBox.Checked = True Then
                    StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                    GoTo 2
                End If
                If NewDirewolf20Version = CurrentDirewolf20Version Then
                    If CurrentDirewolf20IsAdmin = False Then
                        StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                        If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                        Else
                            Try
                                My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                            Catch ex As Exception
                            End Try
                        End If
                        ProgressBar1.Value = 0
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        ProgressLabel.Text = ("0%")
                        ProgressLabel.Show()
                        WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                    GoTo 2
                End If
            Else
                If UpdateCheckBox.Checked = True Then
                    StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                    GoTo 2
                End If
                If NewDirewolf20Version = CurrentDirewolf20Version Then
                    If CurrentDirewolf20IsAdmin = True Then
                        StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                        If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                        Else
                            Try
                                My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                            Catch ex As Exception
                            End Try
                        End If
                        ProgressBar1.Value = 0
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        ProgressLabel.Text = ("0%")
                        ProgressLabel.Show()
                        WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading Minecraft " & Direwolf20MCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Direwolf20/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Direwolf20/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & Direwolf20MCWebVersion & "/minecraft.jar"), "Direwolf20/Jar/minecraft.jar")
                    GoTo 2
                End If
            End If
        Else
            If UserIsAdmin = True Then
                If UpdateCheckBox.Checked = True Then
                    StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                    GoTo 2
                End If
                If NewVanillaVersion = CurrentVanillaVersion Then
                    If CurrentVanillaIsAdmin = False Then
                        StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                        If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                        Else
                            Try
                                My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                            Catch ex As Exception
                            End Try
                        End If
                        ProgressBar1.Value = 0
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        ProgressLabel.Text = ("0%")
                        ProgressLabel.Show()
                        WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                    GoTo 2
                End If
            Else
                If UpdateCheckBox.Checked = True Then
                    StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                    GoTo 2
                End If
                If NewVanillaVersion = CurrentVanillaVersion Then
                    If CurrentVanillaIsAdmin = True Then
                        StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                        If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                        Else
                            Try
                                My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                            Catch ex As Exception
                            End Try
                        End If
                        ProgressBar1.Value = 0
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        ProgressLabel.Text = ("0%")
                        ProgressLabel.Show()
                        WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading Minecraft " & VanillaMCVersion & "..."
                    If My.Computer.FileSystem.DirectoryExists("Vanilla/Jar") Then
                    Else
                        Try
                            My.Computer.FileSystem.CreateDirectory("Vanilla/Jar")
                        Catch ex As Exception
                        End Try
                    End If
                    ProgressBar1.Value = 0
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    ProgressLabel.Text = ("0%")
                    ProgressLabel.Show()
                    WC2.DownloadFileAsync(New Uri("http://assets.minecraft.net/" & VanillaMCWebVersion & "/minecraft.jar"), "Vanilla/Jar/minecraft.jar")
                    GoTo 2
                End If
            End If
        End If
        StatusLabel.Text = "Cleaning Up..."
        BackgroundWorker6.RunWorkerAsync()
        GoTo 2
1:      StatusLabel.Text = ("ERROR!")
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressLabel.Text = ("0%")
        ProgressLabel.Hide()
        MsgBox("There was an unknown error!")
        StatusLabel.Hide()
        ProgressBar1.Hide()
        OptionsButton.Enabled = True
        LoginButton.Enabled = True
        UserTextBox.Enabled = True
        PasswordTextBox.Enabled = True
        UpdateCheckBox.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
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
        ProgressLabel.Text = ProgressBar1.Value & "%"
    End Sub

    Private Sub WC2_DownloadFileCompleted(sender As Object, e As ComponentModel.AsyncCompletedEventArgs) Handles WC2.DownloadFileCompleted
        If My.Computer.FileSystem.FileExists("files.7z") Then
            Try
                My.Computer.FileSystem.DeleteFile("files.7z")
            Catch ex As Exception
            End Try
        End If
        StatusLabel.Text = "Downloading New Libraries..."
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressLabel.Text = ("0%")
        ProgressLabel.Show()
        If RadioButton1.Checked = True Then
            If UserIsAdmin = True Then
                WC3.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/direwolf20/libs+.7z"), "files.7z")
            Else : WC3.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/direwolf20/libs.7z"), "files.7z")
            End If
            GoTo 2
        Else
            If UserIsAdmin = True Then
                WC3.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/vanilla/libs+.7z"), "files.7z")
            Else : WC3.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/vanilla/libs.7z"), "files.7z")
            End If
            GoTo 2
        End If
1:      StatusLabel.Text = ("ERROR!")
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressLabel.Text = ("0%")
        ProgressLabel.Hide()
        MsgBox("There was an unknown error!")
        StatusLabel.Hide()
        ProgressBar1.Hide()
        OptionsButton.Enabled = True
        LoginButton.Enabled = True
        UserTextBox.Enabled = True
        PasswordTextBox.Enabled = True
        UpdateCheckBox.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
2:
    End Sub

    Private Sub WC3_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC3.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        ProgressLabel.Text = ProgressBar1.Value & "%"
    End Sub

    Private Sub WC3_DownloadFileCompleted(sender As Object, e As ComponentModel.AsyncCompletedEventArgs) Handles WC3.DownloadFileCompleted
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressLabel.Hide()
        StatusLabel.Text = "Removing Old Files..."
        BackgroundWorker3.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        If RadioButton1.Checked = True Then
            For Each i As String In Directory.GetDirectories("Direwolf20\.minecraft")
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
            For Each i As String In Directory.GetFiles("Direwolf20\.minecraft")
                My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
            Next
        Else
            For Each i As String In Directory.GetDirectories("Vanilla\.minecraft")
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
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
            Next
        End If
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        StatusLabel.Text = "Setting Up New Libraries..."
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
        Thread.Sleep(200)
    End Sub

    Private Sub BackgroundWorker4_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker4.RunWorkerCompleted
        If My.Computer.FileSystem.FileExists("files.7z") Then
            Try
                My.Computer.FileSystem.DeleteFile("files.7z")
            Catch ex As Exception
            End Try
        End If
        If RadioButton1.Checked = True Then
            StatusLabel.Text = "Downloading " & NewDirewolf20Version & "..."
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            ProgressLabel.Text = ("0%")
            ProgressLabel.Show()
            WC4.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/direwolf20/" & NewDirewolf20Version & "+.7z"), "files.7z")
            GoTo 2
        Else
            StatusLabel.Text = "Downloading " & NewVanillaVersion & "..."
            ProgressBar1.Value = 0
            ProgressBar1.Style = ProgressBarStyle.Blocks
            ProgressLabel.Text = ("0%")
            ProgressLabel.Show()
            WC4.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v4/vanilla/" & NewVanillaVersion & "+.7z"), "files.7z")
            GoTo 2
        End If
1:      StatusLabel.Text = ("ERROR!")
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressLabel.Text = ("0%")
        ProgressLabel.Hide()
        MsgBox("There was an unknown error!")
        StatusLabel.Hide()
        ProgressBar1.Hide()
        OptionsButton.Enabled = True
        LoginButton.Enabled = True
        UserTextBox.Enabled = True
        PasswordTextBox.Enabled = True
        UpdateCheckBox.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
2:
    End Sub

    Private Sub WC4_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC4.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        ProgressLabel.Text = ProgressBar1.Value & "%"
    End Sub

    Private Sub WC4_DownloadFileCompleted(sender As Object, e As ComponentModel.AsyncCompletedEventArgs) Handles WC4.DownloadFileCompleted
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressLabel.Hide()
        If RadioButton1.Checked = True Then
            StatusLabel.Text = "Setting Up " & NewDirewolf20Version & "..."
        Else
            StatusLabel.Text = "Setting Up " & NewVanillaVersion & "..."
        End If
        BackgroundWorker5.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker5_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        Dim Writer As StreamWriter
        'Write extract info
        Writer = My.Computer.FileSystem.OpenTextFileWriter("script.bat", True)
        Writer.WriteLine("@ECHO OFF")
        Writer.WriteLine("@ECHO OFF")
        If RadioButton1.Checked = True Then
            Writer.WriteLine("title MineUK " & NewDirewolf20Version & " Installer")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("CLS")
            Writer.WriteLine("7za.exe x files.7z * -y")
            Writer.WriteLine("CLS")
            Writer.WriteLine("CD Direwolf20\Jar")
        Else
            Writer.WriteLine("title MineUK " & NewVanillaVersion & " Installer")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("CLS")
            Writer.WriteLine("7za.exe x files.7z * -y")
            Writer.WriteLine("CLS")
            Writer.WriteLine("cd Vanilla\Jar")
        End If
        Writer.WriteLine("CLS")
        Writer.WriteLine("..\..\7za.exe u minecraft.jar * -x!minecraft.jar")
        Writer.WriteLine("CLS")
        Writer.WriteLine("..\..\7za.exe d minecraft.jar META-INF")
        Writer.WriteLine("CLS")
        Writer.WriteLine("DEL script.bat")
        Writer.Close()
        Thread.Sleep(150)
        Writer.Dispose()
        Thread.Sleep(100)
        'Extract
        Dim objProcesss As Process
        objProcesss = New Process()
        objProcesss.StartInfo.FileName = "script.bat"
        objProcesss.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        objProcesss.Start()
        objProcesss.WaitForExit()
        objProcesss.Close()
        Thread.Sleep(250)
        'Put the minecraft.jar in the modpack
        If RadioButton1.Checked = True Then
            If My.Computer.FileSystem.FileExists("Direwolf20\.minecraft\bin\minecraft.jar") Then
                My.Computer.FileSystem.DeleteFile("Direwolf20\.minecraft\bin\minecraft.jar")
            End If
            My.Computer.FileSystem.MoveFile("Direwolf20\Jar\minecraft.jar", "Direwolf20\.minecraft\bin\minecraft.jar")
        Else
            If My.Computer.FileSystem.FileExists("Vanilla\.minecraft\bin\minecraft.jar") Then
                My.Computer.FileSystem.DeleteFile("Vanilla\.minecraft\bin\minecraft.jar")
            End If
            My.Computer.FileSystem.MoveFile("Vanilla\Jar\minecraft.jar", "Vanilla\.minecraft\bin\minecraft.jar")
        End If
        
    End Sub

    Private Sub BackgroundWorker5_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker5.RunWorkerCompleted
        StatusLabel.Text = "Cleaning Up..."
        BackgroundWorker6.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker6_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork
        Thread.Sleep(200)
        'Start of file check script
        Try
            For Each i As String In Directory.GetDirectories(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "Direwolf20" Then
                    Else
                        If Path.GetFileName(i) = "Vanilla" Then
                        Else
                            Try
                                My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Direwolf20\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Direwolf20\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Direwolf20")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists("Vanilla\.minecraft") Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory("Vanilla\.minecraft")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetDirectories("Vanilla")
                Try
                    If Path.GetFileName(i) = ".minecraft" Then
                    Else
                        Try
                            My.Computer.FileSystem.DeleteDirectory(Path.GetFullPath(i), FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles(Application.StartupPath)
                Try
                    If Path.GetFileName(i) = "7za.exe" Then
                    Else
                        If Path.GetFileName(i) = "click.wav" Then
                        Else
                            If Path.GetFileName(i) = "MineUK Launcher.exe" Then
                            Else
                                If Path.GetFileName(i) = "MineUK Launcher.exe.config" Then
                                Else
                                    If Path.GetFileName(i) = "MineUK Launcher.xml" Then
                                    Else
                                        Try
                                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                                        Catch ex As Exception
                                        End Try
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            If My.Computer.FileSystem.FileExists(Environ("temp") & "\MineUK Installer.exe") Then
                Try
                    My.Computer.FileSystem.DeleteFile(Environ("temp") & "\MineUK Installer.exe")
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Direwolf20\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            For Each i As String In Directory.GetFiles("Vanilla\.minecraft")
                Try
                    If Path.GetFileName(i).Contains("log") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("optifog") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("ForgeModLoader") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                    If Path.GetFileName(i).Contains("lck") Then
                        Try
                            My.Computer.FileSystem.DeleteFile(Path.GetFullPath(i))
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        'End of file check script
    End Sub

    Private Sub BackgroundWorker6_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker6.RunWorkerCompleted
        StatusLabel.Text = "Launching..."
        BackgroundWorker7.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker7_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker7.DoWork
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
            Writer.WriteLine("title MineUK " & NewDirewolf20Version & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\Direwolf20")
        Else
            Writer.WriteLine("title MineUK " & NewVanillaVersion & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\Vanilla")
        End If
        Writer.WriteLine("CLS")
        Writer.WriteLine(MCDir)
        Writer.WriteLine("CLS")
        Writer.WriteLine("DEL script.bat")
        Writer.Close()
        Thread.Sleep(150)
        Writer.Dispose()
        Thread.Sleep(100)
        'Launch
        Dim ClientStarter As Process
        ClientStarter = New Process()
        ClientStarter.StartInfo.FileName = "script.bat"
        If My.Settings.Debug = True Then
        Else : ClientStarter.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        End If
        ClientStarter.Start()
    End Sub

    Private Sub BackgroundWorker7_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker7.RunWorkerCompleted
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Me.Close()
    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles LogoPanel.Click
        Process.Start("http://mineuk.com/")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles UserTextBox.TextChanged
        My.Settings.User = UserTextBox.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles PasswordTextBox.TextChanged
        My.Settings.Password = PasswordTextBox.Text
    End Sub

    Private Sub MainForm_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Dim SView As Point = SplitContainer1.Panel2.AutoScrollPosition
        SView.X = SView.X + 20
        SplitContainer1.Panel2.AutoScrollPosition = SView
    End Sub
End Class
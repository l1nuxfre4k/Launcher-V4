Imports System
Imports System.Threading
Imports System.IO
Imports System.Net
Imports Skybound

Public Class MainForm

    Dim CommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs

    Dim MCDir As String
    Dim LError As String
    Dim LUserFinal As String
    Dim LSessionIDFinal As String

    Dim CurrentLauncherVersion As String = ProductVersion
    Dim NewLauncherVersion As String = ""
    Dim CurrentTekkitVersion As String = ""
    Dim NewTekkitVersion As String = ""
    Dim CurrentFTBVersion As String = ""
    Dim NewFTBVersion As String = ""

    Dim ServerAdminList As String = ""
    Dim UserIsAdmin As Boolean
    Dim CurrentTekkitIsAdmin As Boolean
    Dim CurrentFTBIsAdmin As Boolean
    Dim ServerIsReady As Boolean = False

    Dim WithEvents WC1 As New WebClient
    Dim WithEvents WC2 As New WebClient

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click, RadioButton1.Click, RadioButton2.Click, Panel1.Click, Button1.Click, Button2.Click
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
        StatusLabel.Hide()
        ProgressBar1.Hide()
        BetaLogo.Show()
        BetaText.Show()
        BetaWarning.Show()
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
        If My.Settings.ModPack = "Tekkit" Then
            RadioButton1.Checked = True
            WebBrowser1.Navigate(New Uri("http://launcher.mineuk.com/v3/tekkit.php"))
        End If
        If My.Settings.ModPack = "FTB" Then
            RadioButton2.Checked = True
            WebBrowser1.Navigate(New Uri("http://launcher.mineuk.com/v3/ftb.php"))
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
                StatusLabel.Show()
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
            End If
            If CommandLineArgs(2) = "auto" Then
                Button1.Enabled = False
                Button2.Enabled = False
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                CheckBox1.Enabled = False
                My.Settings.User = TextBox1.Text
                My.Settings.Password = TextBox2.Text
                StatusLabel.Show()
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
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
                StatusLabel.Show()
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
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
                StatusLabel.Show()
                StatusLabel.Text = "Connecting to MineUK..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
            End If
        Catch ex As Exception
        End Try
        BackgroundWorkerUpdate1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorkerUpdate1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUpdate1.DoWork
        If My.Computer.FileSystem.FileExists("files.7z") Then
            My.Computer.FileSystem.DeleteFile("files.7z")
        End If
        If My.Computer.FileSystem.FileExists("script.bat") Then
            My.Computer.FileSystem.DeleteFile("script.bat")
        End If
        If My.Computer.FileSystem.DirectoryExists("Tekkit\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("Tekkit\.minecraft")
        End If
        If My.Computer.FileSystem.DirectoryExists("FTB\.minecraft") Then
        Else : My.Computer.FileSystem.CreateDirectory("FTB\.minecraft")
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
            NewTekkitVersion = ServerData.Split(":")(2)
            NewFTBVersion = ServerData.Split(":")(3)
            ServerAdminList = ServerData.Split(":")(4)
            ServerStream.Dispose()
            sr.Dispose()
        Catch ex As Exception
        End Try
        CurrentTekkitIsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("Tekkit\.minecraft\version")
                CurrentTekkitVersion = Path.GetFileName(i)
            Next
            If CurrentTekkitVersion.Substring(CurrentTekkitVersion.Length - 1) = "+" Then
                CurrentTekkitIsAdmin = True
                CurrentTekkitVersion = CurrentTekkitVersion.Remove(CurrentTekkitVersion.Length - 1)
            End If
        Catch ex As Exception
        End Try
        CurrentFTBIsAdmin = False
        Try
            For Each i As String In Directory.GetFiles("FTB\.minecraft\version")
                CurrentFTBVersion = Path.GetFileName(i)
            Next
            If CurrentFTBVersion.Substring(CurrentFTBVersion.Length - 1) = "+" Then
                CurrentFTBIsAdmin = True
                CurrentFTBVersion = CurrentFTBVersion.Remove(CurrentFTBVersion.Length - 1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorkerUpdate1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdate1.RunWorkerCompleted
        If NewLauncherVersion = "" Then
            MsgBox("MineUK is unreachable!")
            Me.Close()
        End If
        StatusLabel.Text = "Checking files..."
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
            If My.Computer.FileSystem.DirectoryExists("Tekkit\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Tekkit\.minecraft")
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
            For Each i As String In Directory.GetFiles("Tekkit\.minecraft")
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
        'End of file check script
    End Sub

    Private Sub BackgroundWorkerUpdate2_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdate2.RunWorkerCompleted
        Try
            If CommandLineArgs(0) = "auto" Then
                StatusLabel.Show()
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "auto" Then
                StatusLabel.Show()
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(0) = "update" Then
                StatusLabel.Show()
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
            If CommandLineArgs(2) = "update" Then
                StatusLabel.Show()
                StatusLabel.Text = "Checking files..."
                ProgressBar1.Show()
                ProgressBar1.Style = ProgressBarStyle.Marquee
                BetaLogo.Hide()
                BetaText.Hide()
                BetaWarning.Hide()
                BackgroundWorker1.RunWorkerAsync()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.ModPack = "Tekkit"
            WebBrowser1.Navigate(New Uri("http://launcher.mineuk.com/v3/tekkit.php"))
        End If
        If RadioButton2.Checked = True Then
            My.Settings.ModPack = "FTB"
            WebBrowser1.Navigate(New Uri("http://launcher.mineuk.com/v3/ftb.php"))
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
        StatusLabel.Show()
        StatusLabel.Text = "Checking files..."
        ProgressBar1.Show()
        ProgressBar1.Style = ProgressBarStyle.Marquee
        BetaLogo.Hide()
        BetaText.Hide()
        BetaWarning.Hide()
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
            If My.Computer.FileSystem.DirectoryExists("Tekkit\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Tekkit\.minecraft")
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
            For Each i As String In Directory.GetFiles("Tekkit\.minecraft")
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
            StatusLabel.Hide()
            ProgressBar1.Hide()
            BetaLogo.Show()
            BetaText.Show()
            BetaWarning.Show()
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
            StatusLabel.Hide()
            ProgressBar1.Hide()
            BetaLogo.Show()
            BetaText.Show()
            BetaWarning.Show()
            Button1.Enabled = True
            Button2.Enabled = True
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            CheckBox1.Enabled = True
        End If
        If LError = "" Then
            StatusLabel.Text = "Waiting for server..."
            ServerIsReady = True
            BackgroundWorker2.RunWorkerAsync()
        End If
        ServerIsReady = True
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
        If NewTekkitVersion = "" Then
            GoTo 1
        End If
        If NewFTBVersion = "" Then
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
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & "+.7z"), "files.7z")
                    GoTo 2
                End If
                If NewTekkitVersion = CurrentTekkitVersion Then
                    If CurrentTekkitIsAdmin = False Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & "+.7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & "+.7z"), "files.7z")
                    GoTo 2
                End If
            Else
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & ".7z"), "files.7z")
                    GoTo 2
                End If
                If NewTekkitVersion = CurrentTekkitVersion Then
                    If CurrentTekkitIsAdmin = True Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & ".7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewTekkitVersion & ".7z"), "files.7z")
                    GoTo 2
                End If
            End If
        Else
            If UserIsAdmin = True Then
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & "+.7z"), "files.7z")
                    GoTo 2
                End If
                If NewFTBVersion = CurrentFTBVersion Then
                    If CurrentFTBIsAdmin = False Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & "+.7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & "+.7z"), "files.7z")
                    GoTo 2
                End If
            Else
                If CheckBox1.Checked = True Then
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & ".7z"), "files.7z")
                    GoTo 2
                End If
                If NewFTBVersion = CurrentFTBVersion Then
                    If CurrentFTBIsAdmin = True Then
                        StatusLabel.Text = "Downloading new modpack..."
                        ProgressBar1.Style = ProgressBarStyle.Blocks
                        WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & ".7z"), "files.7z")
                        GoTo 2
                    End If
                Else
                    StatusLabel.Text = "Downloading new modpack..."
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    WC2.DownloadFileAsync(New Uri("http://launcher.mineuk.com/v3/" & NewFTBVersion & ".7z"), "files.7z")
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
        StatusLabel.Hide()
        ProgressBar1.Hide()
        BetaLogo.Show()
        BetaText.Show()
        BetaWarning.Show()
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
            If My.Computer.FileSystem.DirectoryExists("Tekkit\.minecraft") Then
            Else : My.Computer.FileSystem.CreateDirectory("Tekkit\.minecraft")
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
            For Each i As String In Directory.GetFiles("Tekkit\.minecraft")
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
            Writer.WriteLine("title MineUK " & NewTekkitVersion & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\Tekkit")
        Else
            Writer.WriteLine("title MineUK " & NewFTBVersion & " Launcher")
            Writer.WriteLine("SET BINDIR=%~dp0")
            Writer.WriteLine("CD /D " & """%BINDIR%""")
            Writer.WriteLine("set APPDATA=%CD%\FTB")
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

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        Process.Start("http://status.mineuk.com")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.User = TextBox1.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        My.Settings.Password = TextBox2.Text
    End Sub
End Class

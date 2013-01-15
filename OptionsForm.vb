Imports System.IO
Imports System.Threading

Public Class OptionsForm

    Dim Loaded As Boolean = False

    Private Sub Click_Sounds(sender As Object, e As EventArgs) Handles OKButton.Click, MooButton.Click, RadioButton1.Click, RadioButton2.Click, RadioButton3.Click, RadioButton4.Click, ConsoleCheckBox.Click
        If Loaded = True Then
            My.Computer.Audio.Play("click.wav", AudioPlayMode.Background)
        End If
    End Sub

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.RAM = 512 Then
            RadioButton1.Checked = True
        End If
        If My.Settings.RAM = 1024 Then
            RadioButton2.Checked = True
        End If
        If My.Settings.RAM = 2048 Then
            RadioButton3.Checked = True
        End If
        If My.Settings.RAM = 4096 Then
            RadioButton4.Checked = True
        End If
        ConsoleCheckBox.Checked = My.Settings.Debug
        Loaded = True
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        If RadioButton1.Checked = True Then
            My.Settings.RAM = 512
        End If
        If RadioButton2.Checked = True Then
            My.Settings.RAM = 1024
        End If
        If RadioButton3.Checked = True Then
            My.Settings.RAM = 2048
        End If
        If RadioButton4.Checked = True Then
            My.Settings.RAM = 4096
        End If
        My.Settings.Debug = ConsoleCheckBox.Checked
        MainForm.Enabled = True
        Me.Close()
    End Sub

    Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.RAM = 512
        End If
        If RadioButton2.Checked = True Then
            My.Settings.RAM = 1024
        End If
        If RadioButton3.Checked = True Then
            My.Settings.RAM = 2048
        End If
        If RadioButton4.Checked = True Then
            My.Settings.RAM = 4096
        End If
    End Sub

    Private Sub ConsoleCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ConsoleCheckBox.CheckedChanged
        My.Settings.Debug = ConsoleCheckBox.Checked
    End Sub

    Private Sub MooButton_Click(sender As Object, e As EventArgs) Handles MooButton.Click
        Me.Enabled = False
        Me.Hide()
        MainForm.Hide()
        MooWorker.RunWorkerAsync()
    End Sub

    Private Sub MooWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles MooWorker.DoWork
        'Check if script.bat exist
        If My.Computer.FileSystem.FileExists("script.bat") Then
            Try
                My.Computer.FileSystem.DeleteFile("script.bat")
            Catch ex As Exception
            End Try
        End If
        'Write to script.bat
        Dim Writer As StreamWriter
        Writer = My.Computer.FileSystem.OpenTextFileWriter("script.bat", True)
        Writer.WriteLine("@ECHO OFF")
        Writer.WriteLine("@ECHO OFF")
        Writer.WriteLine("CLS")
        Writer.WriteLine("title apt-get moo")
        Writer.WriteLine("echo (__)")
        Writer.WriteLine("echo (oo)")
        Writer.WriteLine("echo /------\/")
        Writer.WriteLine("echo / / //")
        Writer.WriteLine("echo * /\---/\")
        Writer.WriteLine("echo ~~ ~~")
        Writer.WriteLine("echo ...Have you mooed today?...")
        Writer.WriteLine("ping 127.0.0.1 -n 3 > nul")
        Writer.WriteLine("CLS")
        Writer.WriteLine("DEL script.bat")
        Writer.WriteLine("CLS")
        Writer.Close()
        Thread.Sleep(150)
        Writer.Dispose()
        Thread.Sleep(100)
        'Launch
        Dim Moo As Process
        Moo = New Process()
        Moo.StartInfo.FileName = "script.bat"
        Moo.Start()
        Moo.WaitForExit()
        Moo.Close()
    End Sub

    Private Sub MooWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles MooWorker.RunWorkerCompleted
        MainForm.Show()
        Me.Show()
        Me.Enabled = True
    End Sub
End Class
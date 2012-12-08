Public Class OptionsForm

    Private Sub Click_Sounds(sender As Object, e As EventArgs) Handles Button1.Click, RadioButton1.Click, RadioButton2.Click, RadioButton3.Click, RadioButton4.Click, CheckBox1.Click
        My.Computer.Audio.Play("click.wav", AudioPlayMode.Background)
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
        CheckBox1.Checked = My.Settings.Debug
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
        My.Settings.Debug = CheckBox1.Checked
        MainForm.Enabled = True
        Me.Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.RAM = 512
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.RAM = 1024
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            My.Settings.RAM = 2048
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            My.Settings.RAM = 4096
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.Debug = CheckBox1.Checked
    End Sub
End Class
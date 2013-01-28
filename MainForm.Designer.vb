<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OptionsButton = New System.Windows.Forms.Button()
        Me.LoginButton = New System.Windows.Forms.Button()
        Me.UserLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.UserTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.UpdateCheckBox = New System.Windows.Forms.CheckBox()
        Me.LogoPanel = New System.Windows.Forms.Panel()
        Me.BackgroundWorkerUpdate1 = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker4 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker5 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker7 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorkerMojang = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorkerUpdate2 = New System.ComponentModel.BackgroundWorker()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BackgroundNews = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundNewsWait = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundStatus = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundStatusWait = New System.ComponentModel.BackgroundWorker()
        Me.ProgressLabel = New System.Windows.Forms.Label()
        Me.BackgroundWorker6 = New System.ComponentModel.BackgroundWorker()
        Me.SelectedModpack = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackgroundWorker1
        '
        '
        'OptionsButton
        '
        Me.OptionsButton.Location = New System.Drawing.Point(213, 390)
        Me.OptionsButton.Name = "OptionsButton"
        Me.OptionsButton.Size = New System.Drawing.Size(75, 23)
        Me.OptionsButton.TabIndex = 6
        Me.OptionsButton.Text = "Options"
        Me.OptionsButton.UseVisualStyleBackColor = True
        '
        'LoginButton
        '
        Me.LoginButton.Location = New System.Drawing.Point(213, 421)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(75, 23)
        Me.LoginButton.TabIndex = 7
        Me.LoginButton.Text = "Login"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'UserLabel
        '
        Me.UserLabel.AutoSize = True
        Me.UserLabel.BackColor = System.Drawing.Color.Transparent
        Me.UserLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.UserLabel.Location = New System.Drawing.Point(12, 395)
        Me.UserLabel.Name = "UserLabel"
        Me.UserLabel.Size = New System.Drawing.Size(55, 13)
        Me.UserLabel.TabIndex = 1
        Me.UserLabel.Text = "Username"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.PasswordLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.PasswordLabel.Location = New System.Drawing.Point(12, 426)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(53, 13)
        Me.PasswordLabel.TabIndex = 3
        Me.PasswordLabel.Text = "Password"
        '
        'UserTextBox
        '
        Me.UserTextBox.Location = New System.Drawing.Point(80, 392)
        Me.UserTextBox.Name = "UserTextBox"
        Me.UserTextBox.Size = New System.Drawing.Size(125, 20)
        Me.UserTextBox.TabIndex = 2
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(80, 423)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(125, 20)
        Me.PasswordTextBox.TabIndex = 4
        '
        'UpdateCheckBox
        '
        Me.UpdateCheckBox.AutoSize = True
        Me.UpdateCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.UpdateCheckBox.ForeColor = System.Drawing.Color.Gainsboro
        Me.UpdateCheckBox.Location = New System.Drawing.Point(213, 455)
        Me.UpdateCheckBox.Name = "UpdateCheckBox"
        Me.UpdateCheckBox.Size = New System.Drawing.Size(91, 17)
        Me.UpdateCheckBox.TabIndex = 5
        Me.UpdateCheckBox.Text = "Force Update"
        Me.UpdateCheckBox.UseVisualStyleBackColor = False
        '
        'LogoPanel
        '
        Me.LogoPanel.BackColor = System.Drawing.Color.Transparent
        Me.LogoPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LogoPanel.Location = New System.Drawing.Point(552, 398)
        Me.LogoPanel.Name = "LogoPanel"
        Me.LogoPanel.Size = New System.Drawing.Size(255, 65)
        Me.LogoPanel.TabIndex = 9
        '
        'BackgroundWorkerUpdate1
        '
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(322, 421)
        Me.ProgressBar1.MarqueeAnimationSpeed = 25
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(200, 23)
        Me.ProgressBar1.TabIndex = 13
        '
        'StatusLabel
        '
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.StatusLabel.Location = New System.Drawing.Point(296, 400)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(250, 18)
        Me.StatusLabel.TabIndex = 14
        Me.StatusLabel.Text = "Loading..."
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BackgroundWorker2
        '
        '
        'BackgroundWorker3
        '
        '
        'BackgroundWorker4
        '
        '
        'BackgroundWorker5
        '
        '
        'BackgroundWorker7
        '
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'BackgroundWorkerMojang
        '
        '
        'BackgroundWorkerUpdate2
        '
        '
        'VersionLabel
        '
        Me.VersionLabel.BackColor = System.Drawing.Color.Transparent
        Me.VersionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VersionLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.VersionLabel.Location = New System.Drawing.Point(555, 460)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(249, 18)
        Me.VersionLabel.TabIndex = 19
        Me.VersionLabel.Text = "Loading..."
        Me.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(12, 10, 5, 10)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(5, 10, 12, 10)
        Me.SplitContainer1.Size = New System.Drawing.Size(854, 378)
        Me.SplitContainer1.SplitterDistance = 427
        Me.SplitContainer1.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Loading News Feed..."
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(5, 10)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(406, 0)
        Me.Panel2.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(406, 0)
        Me.Panel1.TabIndex = 0
        '
        'BackgroundNews
        '
        '
        'BackgroundNewsWait
        '
        '
        'BackgroundStatus
        '
        '
        'BackgroundStatusWait
        '
        '
        'ProgressLabel
        '
        Me.ProgressLabel.BackColor = System.Drawing.Color.Transparent
        Me.ProgressLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgressLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.ProgressLabel.Location = New System.Drawing.Point(322, 447)
        Me.ProgressLabel.Name = "ProgressLabel"
        Me.ProgressLabel.Size = New System.Drawing.Size(200, 18)
        Me.ProgressLabel.TabIndex = 24
        Me.ProgressLabel.Text = "0%"
        Me.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BackgroundWorker6
        '
        '
        'SelectedModpack
        '
        Me.SelectedModpack.FormattingEnabled = True
        Me.SelectedModpack.Items.AddRange(New Object() {"Direwolf20", "Vanilla", "FTBLite"})
        Me.SelectedModpack.Location = New System.Drawing.Point(80, 452)
        Me.SelectedModpack.Name = "SelectedModpack"
        Me.SelectedModpack.Size = New System.Drawing.Size(125, 21)
        Me.SelectedModpack.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Gainsboro
        Me.Label1.Location = New System.Drawing.Point(12, 456)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Modpack"
        '
        'MainForm
        '
        Me.AcceptButton = Me.LoginButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.MineUK_Launcher.My.Resources.Resources.Main
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(854, 480)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SelectedModpack)
        Me.Controls.Add(Me.ProgressLabel)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.LogoPanel)
        Me.Controls.Add(Me.UpdateCheckBox)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UserTextBox)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UserLabel)
        Me.Controls.Add(Me.LoginButton)
        Me.Controls.Add(Me.OptionsButton)
        Me.Controls.Add(Me.VersionLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MineUK Launcher"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents OptionsButton As System.Windows.Forms.Button
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents UserLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UpdateCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LogoPanel As System.Windows.Forms.Panel
    Friend WithEvents BackgroundWorkerUpdate1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker4 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker5 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker7 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BackgroundWorkerMojang As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorkerUpdate2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents VersionLabel As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BackgroundNews As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundNewsWait As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundStatus As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundStatusWait As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgressLabel As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker6 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SelectedModpack As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class

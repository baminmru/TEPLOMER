Public NotInheritable Class STKDispatcherSplash

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub STKDispatcherSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  

        'TODO: Customize the application's assembly information in the "Application" pane of the project 
        '  properties dialog (under the "Project" menu).

        'Application title
        'If My.Application.Info.Title <> "" Then
        ApplicationTitle.Text = "ТЕПЛОМЕР-IP. Диспетчер"
        'Else
        '    'If the application title is missing, use the application name, without the extension
        '    ApplicationTitle.Text = "ТЕПЛОМЕР-IP. Диспетчер" ' System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        'End If

        'Format the version information using the text set into the Version control at design time as the
        '  formatting string.  This allows for effective localization if desired.
        '  Build and revision information could be included by using the following code and changing the 
        '  Version control's designtime text to "Version {0}.{1:00}.{2}.{3}" or something similar.  See
        '  String.Format() in Help for more information.
        '
        '    Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        'Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        'Copyright info
        'Copyright.Text = My.Application.Info.Copyright
    End Sub

    Private Sub MainLayoutPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Me.Close()
    End Sub

    Private Sub MainLayoutPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub STKDispatcherSplash_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Me.Close()
    End Sub

    Private Sub ApplicationTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Version_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Copyright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
End Class

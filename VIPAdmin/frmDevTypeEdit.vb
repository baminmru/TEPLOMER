Public Class frmDevTypeEdit

    Public id As Integer
   

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select nvl(max(id_dev),0) id_dev from  devices")
                ii = dt.Rows(0)("id_dev")
                Dim s As String
                s = "insert into devices(id_dev,cdevname,cdevdesc,dllname,VERIFYCOLS) values(" + (ii + 1).ToString + ",'" + txtName.Text + "','" + txtDesc.Text + "','" + txtDLLNAME.Text + "','" + txtVerifyCOLS.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update devices set  cdevname='" + txtName.Text + "' ,cdevdesc='" + txtDesc.Text + "' ,DLLNAME='" + txtDLLNAME.Text + "', verifycols='" + txtVerifyCOLS.Text + "'   where id_dev=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try

        End If
       
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs)
       

    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
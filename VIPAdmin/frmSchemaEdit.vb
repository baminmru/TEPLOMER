Public Class frmSchemaEdit

    Public id As Integer
  

   

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select max(ds_id) ds_id from devschema")
                ii = dt.Rows(0)("ds_id")
                Dim s As String
                s = "insert into devschema(ds_id,name) values(" + (ii + 1).ToString + ",'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update devschema set  name='" + txtName.Text + "'   where ds_id=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If
       

    End Sub

   
    

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
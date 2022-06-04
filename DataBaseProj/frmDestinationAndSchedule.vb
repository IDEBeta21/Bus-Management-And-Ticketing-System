Imports System.Data.OleDb

Public Class frmDestinationAndSchedule

    Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DataBaseProAcc.mdb")

    Private Sub frmDestinationAndSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        displaySchedData()

    End Sub

    Sub displaySchedData()

        con.Open()

        ''variable decalaration
        Dim rowCount As Integer = 0

        ''reading all bus data
        Dim getFromDB As New OleDbCommand("SELECT * FROM RoutesAndSchedule", con)
        Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

        ''displaying data to the column
        lvSchedData.Items.Clear()
        While (readGetFromDB.Read)
            lvSchedData.Items.Add(readGetFromDB.GetValue(0).ToString)
            For b = 1 To 3
                lvSchedData.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub


    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click

        Me.Hide()

    End Sub

    Private Sub btnAddBusRec_Click(sender As Object, e As EventArgs) Handles btnAddBusRec.Click
        con.Open()
        Dim getRouteAndPrice As New OleDbCommand("SELECT Origin , Destination FROM RoutesAndSchedule WHERE Origin = '" & txtOrigin.Text.ToString & "' AND Destination = '" & txtDestination.Text.ToString & "' ", con)
        Dim readRouteAndPrice As OleDbDataReader = getRouteAndPrice.ExecuteReader

        If readRouteAndPrice.Read = True Then

            MessageBox.Show("DATE ALREADY EXIST, PLEASE USE UPDATE")
        Else



        End If
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub lvSchedData_MouseClick(sender As Object, e As MouseEventArgs) Handles lvSchedData.MouseClick

    End Sub
End Class
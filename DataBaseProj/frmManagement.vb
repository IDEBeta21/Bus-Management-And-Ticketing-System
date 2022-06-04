Imports DataBaseProj.frmLogin
Imports System.Data.OleDb

Public Class frmManagement

    Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DataBaseProAcc.mdb")
    Dim managerAct As String

    Private Sub frmManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlManageEmployeeRec.Visible = False
        pnlManageEmployeeRec.Enabled = False

        pnlReports.Visible = False
        pnlReports.Enabled = False
        con.Open()
        Dim getBusOrigin As New OleDbCommand("SELECT Origin FROM RoutesAndSchedule", con)
        Dim readBusOrigin As OleDbDataReader = getBusOrigin.ExecuteReader
        Dim strOrigin As String = ""
        Dim arrayOrigin() As String
        Dim c As Integer = 0

        While readBusOrigin.Read

            strOrigin = strOrigin & "," & readBusOrigin.GetValue(0).ToString

        End While

        arrayOrigin = strOrigin.Split(",")

        Dim originToDisplay As List(Of String) = arrayOrigin.Distinct().ToList

        cmbManageBusOrigin.Items.Clear()

        For Each originElement As String In originToDisplay
            If c >= 1 Then
                cmbManageBusOrigin.Items.Add(originElement)
            End If
            c = c + 1
        Next
        con.Close()


        displayBusData()

    End Sub



    ''lblManagaBuses
    Private Sub lblManageBuses_Click(sender As Object, e As EventArgs) Handles lblManageBuses.Click
        lblManageEmployee.BackColor = Color.FromArgb(240, 240, 240)
        lblReports.BackColor = Color.FromArgb(240, 240, 240)
        lblManageBuses.BackColor = Color.White

        pnlManageBuses.Enabled = True
        pnlManageBuses.Visible = True

        pnlManageEmployeeRec.Visible = False
        pnlManageEmployeeRec.Enabled = False

        pnlReports.Visible = False
        pnlReports.Enabled = False

    End Sub



    ''lblManageEmployees
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles lblManageEmployee.Click
        lblManageBuses.BackColor = Color.FromArgb(240, 240, 240)
        lblReports.BackColor = Color.FromArgb(240, 240, 240)
        lblManageEmployee.BackColor = Color.White

        pnlManageEmployeeRec.Enabled = True
        pnlManageEmployeeRec.Visible = True

        pnlManageBuses.Visible = False
        pnlManageBuses.Enabled = False

        pnlReports.Visible = False
        pnlReports.Enabled = False

        displayEmployeeData()

    End Sub


    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click

        Dim dateStr As String = DateTime.Now.ToString("MM/dd/yyyy")
        Dim timeStr As String = DateTime.Now.ToString("HH:mm:ss tt")

        con.Close()
        con.Open()

        Dim recordLogOut As New OleDbCommand("UPDATE LogInHistory SET DateOut = '" & dateStr.ToString & "' , TimeOut = '" & timeStr.ToString & "' WHERE EmployeeID = '" & lblManageLognID.Text.ToString() & "' AND DateOut = '' AND TimeOut = '' ", con)
        recordLogOut.ExecuteNonQuery()

        con.Close()

        Me.Hide()
        frmLogin.Show()
    End Sub

    Private Sub lblExit_MouseHover(sender As Object, e As EventArgs) Handles lblExit.MouseHover
        lblExit.ForeColor = Color.Gray
    End Sub

    Private Sub lblExit_MouseLeave(sender As Object, e As EventArgs) Handles lblExit.MouseLeave
        lblExit.ForeColor = Color.White
    End Sub



    Private Sub lblReports_Click(sender As Object, e As EventArgs) Handles lblReports.Click

        lblManageEmployee.BackColor = Color.FromArgb(240, 240, 240)
        lblManageBuses.BackColor = Color.FromArgb(240, 240, 240)
        lblReports.BackColor = Color.White

        pnlReports.Visible = True
        pnlReports.Enabled = True

        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand("SELECT * FROM LogInHistory", con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying LogIn History data to the column
        lvLoginHistory.Items.Clear()
        While (readLogInHistory.Read)
            lvLoginHistory.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvLoginHistory.Columns.Count - 1
                lvLoginHistory.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()
        pnlManageBuses.Enabled = False
        pnlManageBuses.Visible = False

        pnlManageEmployeeRec.Visible = False
        pnlManageEmployeeRec.Enabled = False

    End Sub

    Private Sub btnAddBusRec_Click(sender As Object, e As EventArgs) Handles btnAddBusRec.Click

        con.Open()
        Dim addStudentRecoredToDb As New OleDbCommand("INSERT INTO BusData(PlateNumber, Origin, Destination, DriverName, Schedule, BusType, OccupiedSeats, DepartureDate) VALUES('" & _
                                                        txtManageBusPlateNum.Text.ToString & "' , '" & _
                                                        cmbManageBusOrigin.Text.ToString & "' , '" & _
                                                        cmbManageBusDestination.Text.ToString & "' , '" & _
                                                        txtManageBusDriverName.Text.ToString & "' , '" & _
                                                        cmbManageBusSchedule.Text.ToString & "' , '" & _
                                                        cmbManageBusBusType.Text.ToString & "' , '0' , 'EMPTY')", con)
        If txtManageBusPlateNum.Text.ToString = "" Or
            cmbManageBusOrigin.Text.ToString = "" Or
            cmbManageBusDestination.Text.ToString = "" Or
            txtManageBusDriverName.Text.ToString = "" Or
            cmbManageBusSchedule.Text.ToString = "" Or
            cmbManageBusBusType.Text.ToString = "" Then

            MessageBox.Show("COMPLETE RECORD")

        Else

            Try
                addStudentRecoredToDb.ExecuteNonQuery()

            Catch

                Dim getPlateNumber As New OleDbCommand("SELECT PlateNumber FROM BusData Where PlateNumber = '" & txtManageBusPlateNum.Text.ToString & "'", con)
                Dim readPlateNumber As OleDbDataReader = getPlateNumber.ExecuteReader

                If readPlateNumber.Read = True Then
                    MessageBox.Show("Record already exist")
                Else
                    MessageBox.Show("Somtheing went wrong")
                End If

            End Try

        End If
        con.Close()

        displayBusData()
    End Sub

    Private Sub btnDeleteBusRec_Click(sender As Object, e As EventArgs) Handles btnDeleteBusRec.Click

        con.Open()
        Try
            Dim deleteBusRecordInDB As New OleDbCommand("DELETE * FROM BusData WHERE PlateNumber = '" & txtManageBusPlateNum.Text.ToString & "'", con)

            Dim getPlateNumFromDB As New OleDbCommand("SELECT PlateNumber FROM BusData WHERE PlateNumber = '" & txtManageBusPlateNum.Text.ToString & "'", con)
            Dim readPlateNum As OleDbDataReader = getPlateNumFromDB.ExecuteReader

            If readPlateNum.Read = True Then

                Dim confirm As MsgBoxResult = MsgBox("Delete This Record", MsgBoxStyle.YesNo)

                If confirm = MsgBoxResult.Yes Then

                    deleteBusRecordInDB.ExecuteNonQuery()

                End If

            Else

                'MessageBox.Show("Record Does not Exist")

            End If
        Catch

            MessageBox.Show("Something Went Wrong")

        End Try
        con.Close()
        displayBusData()
    End Sub

    Private Sub btnManageBusSearchUpdateBusRec_Click(sender As Object, e As EventArgs) Handles btnManageBusSearchUpdateBusRec.Click

        con.Open()

        Try

            Dim updateBusrecordInDB As New OleDbCommand("UPDATE BusData SET PLateNumber = '" & txtManageBusPlateNum.Text.ToString & _
                                                        "' , Origin = '" & cmbManageBusOrigin.Text.ToString & _
                                                        "' , Destination = '" & cmbManageBusDestination.Text.ToString & _
                                                        "' , DriverName = '" & txtManageBusDriverName.Text.ToString() & _
                                                        "' , Schedule = '" & cmbManageBusSchedule.Text.ToString & _
                                                        "' , BusType = '" & cmbManageBusBusType.Text.ToString & _
                                                        "' , Availability = '" & cmbAvailability.Text.ToString & "' WHERE PlateNumber = '" & txtManageBusPlateNum.Text.ToString & "'", con)

            Dim confirmUpdate As MsgBoxResult = MsgBox("Update Record?", MsgBoxStyle.YesNo)

            If confirmUpdate = MsgBoxResult.Yes Then

                updateBusrecordInDB.ExecuteNonQuery()

            End If

        Catch ex As Exception

        End Try


        con.Close()

        displayBusData()
    End Sub



    Private Sub lvManageBuses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvManageBuses.Click

        txtManageBusPlateNum.Text = lvManageBuses.SelectedItems.Item(0).SubItems(0).Text
        txtManageBusDriverName.Text = lvManageBuses.SelectedItems.Item(0).SubItems(3).Text
        cmbManageBusSchedule.Text = lvManageBuses.SelectedItems.Item(0).SubItems(4).Text
        cmbManageBusOrigin.Text = lvManageBuses.SelectedItems.Item(0).SubItems(1).Text
        cmbManageBusDestination.Text = lvManageBuses.SelectedItems.Item(0).SubItems(2).Text
        cmbManageBusBusType.Text = lvManageBuses.SelectedItems.Item(0).SubItems(5).Text
        cmbAvailability.Text = lvManageBuses.SelectedItems.Item(0).SubItems(6).Text

    End Sub


    ''readBusData
    Sub displayBusData()

        con.Open()

        ''variable decalaration
        Dim rowCount As Integer = 0

        ''reading all bus data
        Dim getFromDB As New OleDbCommand("SELECT * FROM BusData", con)
        Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

        ''displaying data to the column
        lvManageBuses.Items.Clear()
        While (readGetFromDB.Read)
            lvManageBuses.Items.Add(readGetFromDB.GetValue(0).ToString)
            For b = 1 To lvManageBuses.Columns.Count
                lvManageBuses.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub


    ''display employee information
    Sub displayEmployeeData()
        con.Close()
        con.Open()

        ''variable decalaration
        Dim rowCount As Integer = 0

        ''reading all bus data
        Dim getFromDB As New OleDbCommand("SELECT * FROM EmployeeData", con)
        Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

        ''displaying data to the column
        lvManageEmployee.Items.Clear()
        While (readGetFromDB.Read)
            lvManageEmployee.Items.Add("EMP - " & Val(readGetFromDB.GetValue(0).ToString).ToString("00000"))
            For b = 1 To 6
                lvManageEmployee.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub



    Private Sub lblEmployeeNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvManageEmployee.Click

        Dim employeeGender As String

        lblEmployeeNum.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(0).Text
        txtEmployeeName.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(1).Text
        txtEmployeePhoneNum.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(2).Text
        txtEmployeeAge.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(4).Text
        cmbEmployeeJob.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(5).Text
        txtEmployeeAddress.Text = lvManageEmployee.SelectedItems.Item(0).SubItems(6).Text

        employeeGender = lvManageEmployee.SelectedItems.Item(0).SubItems(3).Text

        If employeeGender = "Male" Then
            rdMale.Checked = True
        ElseIf employeeGender = "Female" Then
            rdFemale.Checked = True
        End If

    End Sub



    Private Sub btnAddEmployee_Click(sender As Object, e As EventArgs) Handles btnAddEmployee.Click

       

        con.Open()
        Dim empGender As String = ""

        If rdMale.Checked = True Then
            empGender = "Male"
        ElseIf rdFemale.Checked = True Then
            empGender = "Female"
        End If


        Dim addEmployeeRecToDB As New OleDbCommand("INSERT INTO EmployeeData(EmployeeName, EmployeePhoneNumber, EmployeeGender, EmployeeAge, EmployeeJob, EmployeeAddress) VALUES ('" & _
                                                   txtEmployeeName.Text.ToString & "' , '" & _
                                                   txtEmployeePhoneNum.Text.ToString & "' , '" & _
                                                   empGender.ToString & "' , '" & _
                                                   txtEmployeeAge.Text.ToString & "' , '" & _
                                                   cmbEmployeeJob.Text.ToString & "' , '" & _
                                                   txtEmployeeAddress.Text.ToString & "')", con)

        If txtEmployeeAge.Text = "" Or
            txtEmployeePhoneNum.Text = "" Or
            empGender = "" Or
            txtEmployeeAge.Text = "" Or
            cmbEmployeeJob.Text = "" Or
            txtEmployeeAddress.Text = "" Then

            MessageBox.Show("COMPLETE THE FORM FIRST")

        Else

            addEmployeeRecToDB.ExecuteNonQuery()
            Dim getEmpId As New OleDbCommand("SELECT TOP 1 ID FROM EmployeeData ORDER BY ID DESC", con)
            Dim readEmpId As OleDbDataReader = getEmpId.ExecuteReader

            readEmpId.Read()


            managerAct = "ADD RECORD"
            Dim insertActivityLog As New OleDbCommand("INSERT INTO EmployeeManagementActivityLog(EmployeeID , EmployeeName , Activity , EmployeeEditID, EmployeeEditName, ActivityDate, ActivityTime) VALUES('" & lblManageLognID.Text.ToString & _
                                                      "','" & lblManageLoginName.Text.ToString & _
                                                      "','" & managerAct & _
                                                      "','EMP - " & readEmpId.GetInt32(0).ToString("00000") & _
                                                      "','" & txtEmployeeName.Text.ToString & _
                                                      "','" & DateTime.Now.ToString("MM/dd/yyyy") & _
                                                      "','" & DateTime.Now.ToString("hh:mm:ss tt") & "')", con)
            insertActivityLog.ExecuteNonQuery()

        End If

        con.Close()
        displayEmployeeData()
    End Sub

    Private Sub btnDeleteEmployeRec_Click(sender As Object, e As EventArgs) Handles btnDeleteEmployeRec.Click

        con.Open()
        Try
            Dim deleteEmployeeRec As New OleDbCommand("DELETE * FROM EmployeeData WHERE ID = " & Val(lblEmployeeNum.Text) & "", con)

            Dim confirmDel As MsgBoxResult = MsgBox("Delete Record?", MsgBoxStyle.YesNo)

            If confirmDel = MsgBoxResult.Yes Then

                deleteEmployeeRec.ExecuteNonQuery()
                managerAct = "DELETE RECORD"
                Dim insertActivityLog As New OleDbCommand("INSERT INTO EmployeeManagementActivityLog(EmployeeID , EmployeeName , Activity , EmployeeEditID, EmployeeEditName, ActivityDate, ActivityTime) VALUES('" & lblManageLognID.Text.ToString & _
                                                          "','" & lblManageLoginName.Text.ToString & _
                                                          "','" & managerAct & _
                                                          "','" & lblEmployeeNum.Text.ToString & _
                                                          "','" & txtEmployeeName.Text.ToString & _
                                                          "','" & DateTime.Now.ToString("MM/dd/yyyy") & _
                                                          "','" & DateTime.Now.ToString("hh:mm:ss tt") & "')", con)
                insertActivityLog.ExecuteNonQuery()

            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString)

        End Try
        con.Close()

        displayEmployeeData()
    End Sub

    Private Sub btnUpdateEmployeeRec_Click(sender As Object, e As EventArgs) Handles btnUpdateEmployeeRec.Click

        con.Open()
        Try
            Dim empGender As String = ""

            If rdMale.Checked = True Then
                empGender = "Male"
            ElseIf rdFemale.Checked = True Then
                empGender = "Female"
            End If

            Dim strempID As String = lblEmployeeNum.Text.Replace("EMP - ", "")
            Dim empId As Integer = Val(strempID)

            Dim updateEmployeeData As New OleDbCommand("UPDATE EmployeeData SET EmployeeName = '" & txtEmployeeName.Text.ToString & _
                                                            "' , EmployeePhoneNumber = '" & txtEmployeePhoneNum.Text.ToString & _
                                                            "' , EmployeeGender = '" & empGender.ToString & _
                                                            "' , EmployeeAge = '" & txtEmployeeAge.Text.ToString & _
                                                            "' , EmployeeJob = '" & cmbEmployeeJob.Text.ToString & _
                                                            "' , EmployeeAddress = '" & txtEmployeeAddress.Text.ToString & "' WHERE ID = " & empId & "", con)



            If txtEmployeeAge.Text = "" Or
           txtEmployeePhoneNum.Text = "" Or
           empGender = "" Or
           txtEmployeeAge.Text = "" Or
           cmbEmployeeJob.Text = "" Or
           txtEmployeeAddress.Text = "" Then

                MessageBox.Show("FILL THE BLANK ENTRIES")

            Else

                Dim confirmUpdate As MsgBoxResult = MsgBox("Update Record?", MsgBoxStyle.YesNo)

                If confirmUpdate = MsgBoxResult.Yes Then

                    updateEmployeeData.ExecuteNonQuery()
                    managerAct = "UPDATE RECORD"
                    Dim insertActivityLog As New OleDbCommand("INSERT INTO EmployeeManagementActivityLog(EmployeeID , EmployeeName , Activity , EmployeeEditID, EmployeeEditName, ActivityDate, ActivityTime) VALUES('" & lblManageLognID.Text.ToString & _
                                                              "','" & lblManageLoginName.Text.ToString & _
                                                              "','" & managerAct & _
                                                              "','" & lblEmployeeNum.Text.ToString & _
                                                              "','" & txtEmployeeName.Text.ToString & _
                                                              "','" & DateTime.Now.ToString("MM/dd/yyyy") & _
                                                              "','" & DateTime.Now.ToString("hh:mm:ss tt") & "')", con)
                    insertActivityLog.ExecuteNonQuery()

                End If
            End If
        Catch ex As Exception

            MessageBox.Show("Something Went Wrong" & vbNewLine & ex.ToString)

        End Try


        con.Close()

        displayEmployeeData()
    End Sub

    
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnBusSearch.Click

        ''display employee information

        Dim sqlSearch As String = ""

        If txtBusSearch.Text = "" Or cmbBaseOfSearch.Text = "" Then
            MessageBox.Show("Enter the base of Search 1st")
            sqlSearch = "SELECT * FROM BusData"

        ElseIf cmbBaseOfSearch.Text = "Bus Type" Then
            sqlSearch = "SELECT * FROM BusData WHERE BusType = '" & txtBusSearch.Text.ToString & "'"

        ElseIf cmbBaseOfSearch.Text = "Destination" Then
            sqlSearch = "SELECT * FROM BusData WHERE Destination = '" & txtBusSearch.Text.ToString & "'"

        ElseIf cmbBaseOfSearch.Text = "Driver Name" Then
            sqlSearch = "SELECT * FROM BusData WHERE DriverName = '" & txtBusSearch.Text.ToString & "'"

        ElseIf cmbBaseOfSearch.Text = "Origin" Then
            sqlSearch = "SELECT * FROM BusData WHERE Origin = '" & txtBusSearch.Text.ToString & "'"

        ElseIf cmbBaseOfSearch.Text = "Plate Number" Then
            sqlSearch = "SELECT * FROM BusData WHERE PlateNumber = '" & txtBusSearch.Text.ToString & "'"

        ElseIf cmbBaseOfSearch.Text = "Schedule" Then
            sqlSearch = "SELECT * FROM BusData WHERE Schedule = '" & txtBusSearch.Text.ToString & "'"

        End If

        con.Open()

        ''variable decalaration
        Dim rowCount As Integer = 0

        ''reading all bus data
        Dim getFromDB As New OleDbCommand(sqlSearch, con)
        Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

        ''displaying data to the column
        lvManageBuses.Items.Clear()
        While (readGetFromDB.Read)
            lvManageBuses.Items.Add(readGetFromDB.GetValue(0).ToString)
            For b = 1 To lvManageBuses.Columns.Count
                lvManageBuses.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()



    End Sub

    Private Sub cmbManageBusOrigin_TextChanged(sender As Object, e As EventArgs) Handles cmbManageBusOrigin.TextChanged

        If cmbManageBusOrigin.Text IsNot "" Then
            con.Open()

            Dim getDestinations As New OleDbCommand("SELECT Destination FROM RoutesAndSchedule WHERE Origin = '" & cmbManageBusOrigin.Text.ToString & "'", con)
            Dim readDestination As OleDbDataReader = getDestinations.ExecuteReader

            cmbManageBusDestination.Items.Clear()
            cmbManageBusDestination.Text = ""
            While readDestination.Read

                cmbManageBusDestination.Items.Add(readDestination.GetValue(0).ToString)

            End While
            con.Close()
        End If
    End Sub

    Private Sub cmbManageBusDestination_TextChanged(sender As Object, e As EventArgs) Handles cmbManageBusDestination.TextChanged

        If cmbManageBusDestination.Text IsNot "" Then

            Dim strSched() As String

            con.Open()
            Dim getSchedule As New OleDbCommand("SELECT Schedules FROM RoutesAndSchedule WHERE Origin = '" & cmbManageBusOrigin.Text & "' AND Destination = '" & cmbManageBusDestination.Text.ToString & "'", con)
            Dim readSched As OleDbDataReader = getSchedule.ExecuteReader

            readSched.Read()

            strSched = readSched.GetValue(0).ToString.Split(" ")
            cmbManageBusSchedule.Items.Clear()
            cmbManageBusSchedule.Text = ""
            For Each scheElement In strSched

                cmbManageBusSchedule.Items.Add(scheElement)

            Next

            con.Close()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAddSched.Click

        frmDestinationAndSchedule.Show()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()

        ''variable decalaration
        Dim rowCount As Integer = 0

        ''reading all bus data
        Dim getFromDB As New OleDbCommand("SELECT * FROM BusData", con)
        Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

        ''displaying data to the column
        lvManageBuses.Items.Clear()
        While (readGetFromDB.Read)
            lvManageBuses.Items.Add(readGetFromDB.GetValue(0).ToString)
            For b = 1 To lvManageBuses.Columns.Count
                lvManageBuses.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()
    End Sub

    

    

    Private Sub btnDisplayAllEmpData_Click(sender As Object, e As EventArgs) Handles btnDisplayAllEmpData.Click
        displayEmployeeData()
    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles lblEmployeeManagementHistory.Click

        pnlReservationHistory.Enabled = False
        pnlReservationHistory.Visible = False

        pnlSalesReport.Enabled = False
        pnlSalesReport.Visible = False

        pnlLogInHistorySearchBase.Enabled = False
        pnlLogInHistorySearchBase.Visible = False

        pnlEmployeeManagementHistory.Enabled = True
        pnlEmployeeManagementHistory.Visible = True

        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand("SELECT * FROM EmployeeManagementActivityLog", con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying data to the column
        lvEmployeeManagementList.Items.Clear()
        While (readLogInHistory.Read)
            lvEmployeeManagementList.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvEmployeeManagementList.Columns.Count - 1
                lvEmployeeManagementList.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub


    'Sub displayEmployeeManagementHistory()
    '    con.Open()



    '    ''variable decalaration
    '    Dim rowCount As Integer = 0

    '    ''reading all bus data
    '    Dim getFromDB As New OleDbCommand(sqlSearch, con)
    '    Dim readGetFromDB As OleDbDataReader = getFromDB.ExecuteReader()

    '    ''displaying data to the column
    '    lvManageBuses.Items.Clear()
    '    While (readGetFromDB.Read)
    '        lvManageBuses.Items.Add(readGetFromDB.GetValue(0).ToString)
    '        For b = 1 To lvManageBuses.Columns.Count
    '            lvManageBuses.Items(rowCount).SubItems.Add(readGetFromDB.GetValue(b).ToString)
    '        Next
    '        rowCount = rowCount + 1
    '    End While
    '    rowCount = 0

    '    con.Close()

    'End Sub



    Private Sub lblLogInHistory_Click(sender As Object, e As EventArgs) Handles lblLogInHistory.Click

        pnlEmployeeManagementHistory.Enabled = False
        pnlEmployeeManagementHistory.Visible = False

        pnlReservationHistory.Enabled = False
        pnlReservationHistory.Visible = False

        pnlSalesReport.Enabled = False
        pnlSalesReport.Visible = False

        pnlLogInHistorySearchBase.Enabled = True
        pnlLogInHistorySearchBase.Visible = True

        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand("SELECT * FROM LogInHistory", con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying LogIn History data to the column
        lvLoginHistory.Items.Clear()
        While (readLogInHistory.Read)
            lvLoginHistory.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvLoginHistory.Columns.Count - 1
                lvLoginHistory.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub


    Private Sub btnLoginHistorySearch_Click(sender As Object, e As EventArgs) Handles btnLoginHistorySearch.Click

        Dim sqlStr As String = ""
        If cmbLogInSearch.Text = "Employee ID" Then

            sqlStr = "SELECT * FROM LogInHistory WHERE EmployeeID = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Account Type" Then

            sqlStr = "SELECT * FROM LogInHistory WHERE AccountType = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Date In" Then

            sqlStr = "SELECT * FROM LogInHistory WHERE DateIn = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Date Out" Then

            sqlStr = "SELECT * FROM LogInHistory WHERE DateOut = '" & txtLogInHistorySearch.Text.ToString & "'"

        Else

            sqlStr = "SELECT * FROM LogInHistory"

        End If


        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand(sqlStr, con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying data to the column
        lvLoginHistory.Items.Clear()
        While (readLogInHistory.Read)
            lvLoginHistory.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvLoginHistory.Columns.Count - 1
                lvLoginHistory.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()


    End Sub

    Private Sub btnLoginHistoryAllRecord_Click(sender As Object, e As EventArgs) Handles btnLoginHistoryAllRecord.Click
        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand("SELECT * FROM LogInHistory", con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying data to the column
        lvLoginHistory.Items.Clear()
        While (readLogInHistory.Read)
            lvLoginHistory.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvLoginHistory.Columns.Count - 1
                lvLoginHistory.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()
    End Sub




    Private Sub btnEmployeeManagementHistorySearch_Click(sender As Object, e As EventArgs) Handles btnEmployeeManagementHistorySearch.Click

        Dim sqlStr As String = ""
        If cmbLogInSearch.Text = "Manager ID" Then

            sqlStr = "SELECT * FROM EmployeeManagementActivityLog WHERE EmployeeID = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Activity" Then

            sqlStr = "SELECT * FROM EmployeeManagementActivityLog WHERE Activity = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Employee Name" Then

            sqlStr = "SELECT * FROM EmployeeManagementActivityLog WHERE EmployeeEditName = '" & txtLogInHistorySearch.Text.ToString & "'"

        ElseIf cmbLogInSearch.Text = "Activity Date" Then

            sqlStr = "SELECT * FROM EmployeeManagementActivityLog WHERE ActivityDate = '" & txtLogInHistorySearch.Text.ToString & "'"

        Else

            sqlStr = "SELECT * FROM EmployeeManagementActivityLog"

        End If


        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand(sqlStr, con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying data to the column
        lvEmployeeManagementList.Items.Clear()
        While (readLogInHistory.Read)
            lvEmployeeManagementList.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvEmployeeManagementList.Columns.Count - 1
                lvEmployeeManagementList.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub

    Private Sub btnEmployeeManagementHistoryAllRecord_Click(sender As Object, e As EventArgs) Handles btnEmployeeManagementHistoryAllRecord.Click

        con.Close()
        con.Open()

        Dim getLogInHistory As New OleDbCommand("SELECT * FROM EmployeeManagementActivityLog", con)
        Dim readLogInHistory As OleDbDataReader = getLogInHistory.ExecuteReader
        Dim rowCount As Integer = 0

        ''displaying data to the column
        lvEmployeeManagementList.Items.Clear()
        While (readLogInHistory.Read)
            lvEmployeeManagementList.Items.Add(readLogInHistory.GetValue(0).ToString)
            For b = 1 To lvEmployeeManagementList.Columns.Count - 1
                lvEmployeeManagementList.Items(rowCount).SubItems.Add(readLogInHistory.GetValue(b).ToString)
            Next
            rowCount = rowCount + 1
        End While
        rowCount = 0

        con.Close()

    End Sub

    Private Sub lblReservationHistory_Click(sender As Object, e As EventArgs) Handles lblReservationHistory.Click

        pnlSalesReport.Enabled = False
        pnlSalesReport.Visible = False

        pnlLogInHistorySearchBase.Enabled = False
        pnlLogInHistorySearchBase.Visible = False

        pnlEmployeeManagementHistory.Enabled = False
        pnlEmployeeManagementHistory.Visible = False

        pnlReservationHistory.Enabled = True
        pnlReservationHistory.Visible = True

    End Sub

    Private Sub lblSalesReport_Click(sender As Object, e As EventArgs) Handles lblSalesReport.Click

        pnlLogInHistorySearchBase.Enabled = False
        pnlLogInHistorySearchBase.Visible = False

        pnlEmployeeManagementHistory.Enabled = False
        pnlEmployeeManagementHistory.Visible = False

        pnlReservationHistory.Enabled = False
        pnlReservationHistory.Visible = False

        pnlSalesReport.Enabled = True
        pnlSalesReport.Visible = True


    End Sub

    Private Sub btnPrintBusManagement_Click(sender As Object, e As EventArgs) Handles btnPrintBusManagement.Click

        MessageBox.Show(lvManageBuses.Items.Count)



    End Sub
End Class
Imports System.Data.OleDb
Imports DataBaseProj.My

Public Class frmLogin

    Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DataBaseProAcc.mdb")


    ''frmLogin Method
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        pnlCreateAcc.Enabled = False

    End Sub

    Public Sub login()
        Try
            con.Open()
            'testing if the password is correct or not (by knowing if the record exist or not)
            Dim toDataBase As New OleDbCommand("SELECT LogInUserID,UserName,LogInUserName,UserJob FROM LoginData WHERE UserName ='" & txtUserName.Text.ToString & "' AND UserPassword = '" & txtUserPass.Text.ToString & "'", con)
            Dim letsRead As OleDbDataReader = toDataBase.ExecuteReader 'convert the sending proccess to be readable

            letsRead.Read() 'reads the data that the system get from the database

            Dim timeStr As String = DateTime.Now.ToString("HH:mm:ss tt") 'getting the current date and time
            Dim dateStr As String = DateTime.Now.ToString("MM/dd/yyyy")

            Dim toHistoryRec As New OleDbCommand("INSERT INTO LoginHistory VALUES('" & letsRead.GetValue(0).ToString &
                                                 "','" & letsRead.GetValue(1).ToString &
                                                 "','" & letsRead.GetValue(2).ToString &
                                                 "','" & letsRead.GetValue(3).ToString &
                                                 "','" & dateStr.ToString &
                                                 "','" & timeStr.ToString & "','','')", con)
            toHistoryRec.ExecuteNonQuery()

            'MessageBox.Show("Login Succeed")
            con.Close()

            'determining user's (the person who logs in) job
            con.Open()
            'reading the user job using the password and username
            Dim userJob As New OleDbCommand("SELECT UserJob FROM LoginData WHERE UserName = '" & txtUserName.Text.ToString & "' AND UserPassword = '" & txtUserPass.Text.ToString & "'", con)
            Dim readUserJob As OleDbDataReader = userJob.ExecuteReader

            readUserJob.Read() 'reading the data

            'converting data to string
            Dim userJobString As String = readUserJob.GetValue(0).ToString
            con.Close()

            'test if the user that logs in is employee or manager
            If userJobString = "Manager" Then
                ''displayin the username in manganer window
                con.Open()
                Dim getEmployeeName As New OleDbCommand("SELECT LoginUserID , LoginUserName FROM LoginData WHERE UserName = '" & txtUserName.Text.ToString & "' AND UserPassword = '" & txtUserPass.Text.ToString & "' ", con)
                Dim readEmployeeName As OleDbDataReader = getEmployeeName.ExecuteReader
                readEmployeeName.Read()

                frmManagement.lblManageLognID.Text = readEmployeeName.GetValue(0).ToString
                frmManagement.lblManageLoginName.Text = readEmployeeName.GetValue(1).ToString
                Me.Hide()
                frmManagement.Show()
                con.Close()
            ElseIf userJobString = "Employee" Then
                ''displaying the user name in employee window
                con.Open()
                Dim getEmployeeName As New OleDbCommand("SELECT LoginUserID, LogInUserName FROM LoginData WHERE UserName = '" & txtUserName.Text.ToString & "' AND UserPassword = '" & txtUserPass.Text.ToString & "' ", con)
                Dim readEmployeeName As OleDbDataReader = getEmployeeName.ExecuteReader
                readEmployeeName.Read()

                frmEmployee.lblReservationLoginID.Text = readEmployeeName.GetValue(0).ToString
                frmEmployee.lblReservationLoginName.Text = readEmployeeName.GetValue(1).ToString
                Me.Hide()
                frmEmployee.Show()
                con.Close()
            End If

        Catch ex As Exception

            MessageBox.Show("Log In Not Succeed")
            'MessageBox.Show(ex.ToString)
            Console.WriteLine(ex.ToString)
            con.Close()

        End Try
    End Sub

    ''btnLogin Method
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        login()

    End Sub



    ''btnClear Functions''
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        txtUserName.Text = ""
        txtUserPass.Text = ""
    End Sub



    ''lblExit Functions''
    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click
        Me.Close()
    End Sub

    Private Sub lblExit_MouseHover(sender As Object, e As EventArgs) Handles lblExit.MouseHover
        lblExit.ForeColor = Color.Red
    End Sub

    Private Sub lblExit_MouseLeave(sender As Object, e As EventArgs) Handles lblExit.MouseLeave
        lblExit.ForeColor = Color.Black
    End Sub



    Private Sub lblCreateAcc_Click(sender As Object, e As EventArgs) Handles lblCreateAcc.Click
        pnlCreateAcc.Enabled = True
        pnlCreateAcc.Visible = True
        pnlLogin.Visible = False
        pnlLogin.Enabled = False
        txtCreateEmployeeName.Focus()
    End Sub




    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles lblLogin.Click
        pnlLogin.Enabled = True
        pnlLogin.Visible = True
        pnlCreateAcc.Visible = False
        pnlCreateAcc.Enabled = False
        txtUserName.Focus()
    End Sub


    Private Sub btnCreateAcc_Click(sender As Object, e As EventArgs) Handles btnCreateAcc.Click

        con.Close()
        con.Open()
        Try
            Dim employeeJob, empID As String

            ''getting employeejob form database
            Dim getDataFromEmployeeTable As New OleDbCommand("SELECT EmployeeJob , ID FROM EmployeeData WHERE ID = " & Val(txtCreateUserID.Text) &
                                                             " AND EmployeeName = '" & txtCreateEmployeeName.Text.ToString &
                                                             "' AND EmployeeAddress = '" & txtCreateAddress.Text.ToString &
                                                             "' AND EmployeePhoneNumber = '" & txtCreatePhoneNum.Text.ToString & "'", con)

            Dim readData As OleDbDataReader = getDataFromEmployeeTable.ExecuteReader()

            readData.Read()

            employeeJob = readData.GetValue(0).ToString
            empID = readData.GetValue(1).ToString

            If txtCreateUserName.Text = "" And
                txtCreateUserPass.Text = "" And
                txtCreateUserPassAgain.Text = "" Then

                MessageBox.Show("FILL UP THE USERNAME AND PASSWORD")

            Else

                If txtCreateUserPass.Text.ToString.Equals(txtCreateUserPassAgain.Text.ToString) = True Then

                    Try
                        Dim usrPassAndName As New OleDbCommand("SELECT UserJob FROM LoginData WHERE UserName = '" & txtCreateUserName.Text.ToString & "' AND UserPassword = '" & txtCreateUserPass.Text.ToString & "'", con)
                        readData = usrPassAndName.ExecuteReader

                        readData.Read()

                        If readData.Read = False Then

                            Dim saveNewAcc As New OleDbCommand("INSERT INTO LoginData(UserName, UserPassword, UserJob, LoginUserID, LoginUserName) VALUES('" & txtCreateUserName.Text.ToString &
                                                           "' , '" & txtCreateUserPass.Text.ToString &
                                                           "' , '" & employeeJob &
                                                           "' , 'EMP - " & txtCreateUserID.Text.ToString &
                                                           "' , '" & txtCreateEmployeeName.Text.ToString & "')", con)

                            saveNewAcc.ExecuteNonQuery()

                            MessageBox.Show("ACCOUNT CREATED")

                        Else

                            MessageBox.Show("USERNAME AND PASSWORD ALREADY EXIST")

                        End If

                        con.Close()
                    Catch exf As Exception

                        MessageBox.Show(exf.ToString)

                        con.Close()
                    End Try


                Else

                    MessageBox.Show("PASSWORD IS INCORRECT")
                    con.Close()
                End If

            End If
        Catch ex As Exception

            MessageBox.Show("Cant find datas in data base " & vbNewLine &
                            " - Make sure that the information inputed is correct" & vbNewLine &
                            " - Data might not be in the database")
            con.Close()

        End Try
        con.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles lblExitCreate.Click

        Me.Close()

    End Sub

    Private Sub txtUserInput_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserPass.KeyDown, txtUserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            login()
        End If
    End Sub
End Class

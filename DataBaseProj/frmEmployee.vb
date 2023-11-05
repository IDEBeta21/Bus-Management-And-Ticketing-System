Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmEmployee

    Dim gender As String = "MALE"
    Dim seatNumber As String
    Dim empAct As String
    Dim individualPrice, totalPassengerPrice, calculatedSeatPrice As Double
    Dim seatsClicked As Integer = 0



    Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DataBaseProAcc.mdb")



    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnPay.Enabled = False
        btnPrint.Enabled = False

        Dim b() As Button
        b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

        'disable other button
        For aaa = 0 To 60

            b(aaa).Enabled = False

        Next

    End Sub


    Private Sub rdMale_CheckedChanged(sender As Object, e As EventArgs)
        gender = "MALE"
    End Sub


    Private Sub rdFemale_CheckedChanged(sender As Object, e As EventArgs)
        gender = "FEAMALE"
    End Sub


    Private Sub btnSeatNumber28_MouseClick(sender As Object, e As MouseEventArgs) Handles btnSeatNumber9.MouseClick, btnSeatNumber8.MouseClick, btnSeatNumber7.MouseClick, btnSeatNumber6.MouseClick, btnSeatNumber56.MouseClick, btnSeatNumber55.MouseClick, btnSeatNumber54.MouseClick, btnSeatNumber53.MouseClick, btnSeatNumber52.MouseClick, btnSeatNumber51.MouseClick, btnSeatNumber50.MouseClick, btnSeatNumber5.MouseClick, btnSeatNumber49.MouseClick, btnSeatNumber48.MouseClick, btnSeatNumber47.MouseClick, btnSeatNumber46.MouseClick, btnSeatNumber45.MouseClick, btnSeatNumber44.MouseClick, btnSeatNumber43.MouseClick, btnSeatNumber42.MouseClick, btnSeatNumber41.MouseClick, btnSeatNumber40.MouseClick, btnSeatNumber4.MouseClick, btnSeatNumber39.MouseClick, btnSeatNumber38.MouseClick, btnSeatNumber37.MouseClick, btnSeatNumber36.MouseClick, btnSeatNumber35.MouseClick, btnSeatNumber34.MouseClick, btnSeatNumber32.MouseClick, btnSeatNumber31.MouseClick, btnSeatNumber30.MouseClick, btnSeatNumber3.MouseClick, btnSeatNumber29.MouseClick, btnSeatNumber28.MouseClick, btnSeatNumber27.MouseClick, btnSeatNumber26.MouseClick, btnSeatNumber25.MouseClick, btnSeatNumber24.MouseClick, btnSeatNumber23.MouseClick, btnSeatNumber22.MouseClick, btnSeatNumber21.MouseClick, btnSeatNumber20.MouseClick, btnSeatNumber2.MouseClick, btnSeatNumber19.MouseClick, btnSeatNumber18.MouseClick, btnSeatNumber17.MouseClick, btnSeatNumber16.MouseClick, btnSeatNumber15.MouseClick, btnSeatNumber14.MouseClick, btnSeatNumber13.MouseClick, btnSeatNumber12.MouseClick, btnSeatNumber11.MouseClick, btnSeatNumber10.MouseClick, btnSeatNumber1.MouseClick, btnSeatNumber61.MouseClick, btnSeatNumber60.MouseClick, btnSeatNumber59.MouseClick, btnSeatNumber58.MouseClick, btnSeatNumber57.MouseClick, btnSeatNumber33.MouseClick

        Dim b() As Button
        b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

        Dim btn As Button = sender
        Dim occSeats() As String
        Dim finalOccSeats As String = ""

        Me.seatNumber = btn.Text


        ''testing if the seat is vacant (the backcolor is black)
        If btn.BackColor = Color.Black And btn.ForeColor = Color.White Then

            ''messag prompt for deleting
            Dim deleteSeatPrompt As MsgBoxResult = MsgBox("Make the seat Vacant", MsgBoxStyle.YesNo)

            If deleteSeatPrompt = MsgBoxResult.Yes Then

                con.Open()
                ''getting and reading occupied seats from the database
                Dim getOccupiedSeats As New OleDbCommand("SELECT OccupiedSeats FROM BusData WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
                Dim readOccupiedSeats As OleDbDataReader = getOccupiedSeats.ExecuteReader()

                readOccupiedSeats.Read()

                ''reading the occupuied seats and splitting yung spaces
                occSeats = (readOccupiedSeats.GetValue(0).ToString).Split(" ")

                ''adding new occupied seat in the database
                For a = 0 To occSeats.Length - 1
                    If Val(btn.Text) <> Val(occSeats(a).ToString) Then
                        If a = 0 Then
                            finalOccSeats = finalOccSeats & occSeats(a)
                        Else
                            finalOccSeats = finalOccSeats & " " & occSeats(a)
                        End If
                    End If
                Next

                ''recording employee activity
                Dim updateSeatsCmd As New OleDbCommand("UPDATE BusData SET OccupiedSeats = '" & finalOccSeats.ToString & "' WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
                empAct = "DELETE RESERVATION"
                Dim insertReservation As New OleDbCommand("INSERT INTO BusReservationActivityLog(EmployeeID,EmployeeName,Activity,Route,BusID,SeatNumber,DateOfActivity,TimeOfActivity,Profit) VALUES('" & lblReservationLoginID.Text.ToString & _
                                                          "','" & lblReservationLoginName.Text.ToString & _
                                                          "','" & empAct.ToString & _
                                                          "','" & cmbOrigin.Text.ToString & " - " & cmbDestination.Text.ToString & _
                                                          "','" & cmbBusID.Text.ToString & _
                                                          "','" & seatNumber.ToString &
                                                          "','" & DateTime.Now.ToString("MM/dd/yyyy") & _
                                                          "','" & DateTime.Now.ToString("hh:mm:ss tt") & _
                                                          "','" & txtPrice.Text.ToString & "')", con)


                insertReservation.ExecuteNonQuery()
                updateSeatsCmd.ExecuteNonQuery()

                'enable other button
                'For aaa = 0 To 60

                '    b(aaa).Enabled = True

                'Next

                txtPrice.Text = "00.00"

                con.Close()
                updateSeats()

                'btn.BackColor = Color.White
                'btn.ForeColor = Color.Black

            End If

            ''testing if seat is not occupied
        ElseIf btn.BackColor = Color.FromArgb(240, 240, 240) And btn.ForeColor = Color.Black Then

            btn.BackColor = Color.FromArgb(139, 0, 0)
            btn.ForeColor = Color.White

            seatsClicked = seatsClicked + 1

            'con.Open()
            'Dim getOccupiedSeats As New OleDbCommand("SELECT OccupiedSeats FROM BusData WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
            'Dim readOccupiedSeats As OleDbDataReader = getOccupiedSeats.ExecuteReader
            'readOccupiedSeats.Read()
            'Dim occStr As String = readOccupiedSeats.GetValue(0).ToString & " " & btn.Text
            'Dim updateSeatsCmd As New OleDbCommand("UPDATE BusData SET OccupiedSeats = '" & occStr.ToString & "' WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
            'updateSeatsCmd.ExecuteNonQuery()
            'con.Close()

            ''disabele other button
            'For aaa = 0 To 60
            '    If b(aaa).Text IsNot btn.Text Then
            '        b(aaa).Enabled = False
            '    End If
            'Next

            'updateSeats()

            'btn.BackColor = Color.Black
            'btn.ForeColor = Color.White

        End If

        Dim strGetPrice As String = ""

        If cmbBusType.Text = "AC" Then
            strGetPrice = "SELECT ACPrice FROM RoutesAndSchedule WHERE Origin = '" & cmbOrigin.Text.ToString & "' AND Destination = '" & cmbDestination.Text.ToString & "'"
        ElseIf cmbBusType.Text = "SPECIAL" Then
            strGetPrice = "SELECT SpecialPrice FROM RoutesAndSchedule WHERE Origin = '" & cmbOrigin.Text.ToString & "' AND Destination = '" & cmbDestination.Text.ToString & "'"
        End If

        ''gettig the price
        con.Open()
        Dim getPrice As New OleDbCommand(strGetPrice, con)
        Dim readPrice As OleDbDataReader = getPrice.ExecuteReader
        readPrice.Read()



        txtPrice.Text = Val(txtPrice.Text) + Val(readPrice.GetValue(0).ToString)
        Me.individualPrice = Val(readPrice.GetValue(0))
        con.Close()

        updateTextBox()
        displayInTicket()

    End Sub



    ''lblExit functions
    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click

        Dim dateStr As String = DateTime.Now.ToString("MM/dd/yyyy")
        Dim timeStr As String = DateTime.Now.ToString("HH:mm:ss tt")

        con.Open()

        Dim recordLogOut As New OleDbCommand("UPDATE LogInHistory SET DateOut = '" & dateStr.ToString & "' , TimeOut = '" & timeStr.ToString & "' WHERE EmployeeID = '" & lblReservationLoginID.Text.ToString() & "' AND DateOut = '' AND TimeOut = '' ", con)
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





    Private Sub cmbBehaviourWhenTryToEnter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtbTicketDisplay.KeyPress, cmbTime.KeyPress, cmbPassengerType.KeyPress, cmbOrigin.KeyPress, cmbDestination.KeyPress, cmbDate.KeyPress, cmbBusID.KeyPress
        e.Handled = True
    End Sub


    Private Sub txtAge_TextChanged(sender As Object, e As EventArgs) Handles txtAge.TextChanged


        updateTextBox()

    End Sub



    Private Sub cmbOrigin_TextChanged(sender As Object, e As EventArgs) Handles cmbOrigin.TextChanged

        If cmbOrigin.Text IsNot "" Then
            Try
                con.Open()
                Dim getDestinationByOrigin As New OleDbCommand("SELECT Destination FROM RoutesAndSchedule WHERE Origin = '" & cmbOrigin.Text.ToString & "'", con)
                Dim readDestination As OleDbDataReader = getDestinationByOrigin.ExecuteReader

                cmbDestination.Text = vbNullString
                cmbDestination.Items.Clear()

                'putting available detinations in combo box
                While (readDestination.Read())

                    cmbDestination.Items.Add(readDestination.GetValue(0).ToString)

                End While

                cmbDestination.Text = ""
                cmbTime.Text = ""
                cmbDate.Text = ""
                cmbBusType.Text = ""
                cmbBusID.Text = ""
            Catch

                con.Open()
                Dim getDestinationByOrigin As New OleDbCommand("SELECT Destination FROM RoutesAndSchedule WHERE Origin = '" & cmbOrigin.Text.ToString & "'", con)
                Dim readDestination As OleDbDataReader = getDestinationByOrigin.ExecuteReader

                cmbDestination.Text = vbNullString
                cmbDestination.Items.Clear()

                'putting available detinations in combo box
                While (readDestination.Read())

                    cmbDestination.Items.Add(readDestination.GetValue(0).ToString)

                End While

                cmbDestination.Text = ""
                cmbTime.Text = ""
                cmbDate.Text = ""
                cmbBusType.Text = ""
                cmbBusID.Text = ""

            End Try
            con.Close()
        End If

        updateTextBox()

    End Sub

    Private Sub cmbDestination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDestination.TextChanged

        If cmbDestination.Text IsNot "" Then
            con.Close()
            con.Open()
            Try
                ''display available times
                Dim getDateAndTimeUsinDestination As New OleDbCommand("SELECT Schedule FROM BusData Where Origin = '" & cmbOrigin.Text.ToString & _
                                                                      "' AND Destination = '" & cmbDestination.Text.ToString & "'", con)
                Dim readSchedule As OleDbDataReader = getDateAndTimeUsinDestination.ExecuteReader()

                Dim schedule() As String
                Dim strSched As String = ""
                Dim c As Integer

                While readSchedule.Read

                    strSched = strSched + " " + readSchedule.GetValue(0).ToString

                End While

                schedule = strSched.Split(" ")

                Dim scheduleToDisplay As List(Of String) = schedule.Distinct().ToList

                cmbTime.Items.Clear()
                For Each stringElement As String In scheduleToDisplay
                    If c >= 1 Then
                        cmbTime.Items.Add(stringElement)
                    End If
                    c = c + 1
                Next
                cmbTime.Text = ""

                ''display available dates
                Dim monthNow As Integer = Date.Now.ToString("MM")
                Dim yearNow As Integer = Date.Now.ToString("yyyy")
                Dim displayDays As Integer = Date.Now.ToString("dd")

                For displayDays = Val(Date.Now.ToString("dd")) To Val(System.DateTime.DaysInMonth(yearNow, monthNow))

                    cmbDate.Items.Add(monthNow & "/" & displayDays & "/" & yearNow)

                Next

                cmbTime.Text = ""
                cmbDate.Text = ""
                cmbBusType.Text = ""
                cmbBusID.Text = ""


            Catch exc As Exception

                cmbTime.Text = "NaN"
                MessageBox.Show(exc.ToString & vbNewLine)


            End Try
            con.Close()
        End If

        updateTextBox()

    End Sub

    Private Sub cmbDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDate.TextChanged

        If cmbDate.Text IsNot "" Then
            con.Close()
            con.Open()

            Dim monthNow As Integer = Date.Now.ToString("MM")
            Dim yearNow As Integer = Date.Now.ToString("yyyy")
            Dim displayDays As Integer = Date.Now.ToString("dd")
            Dim strDepartureDate As String = ""
            Dim baseDate As String = monthNow & "/" & displayDays & "/" & yearNow
            Dim nul As String = "EMPTY"

            Dim getDepartureDate As New OleDbCommand("SELECT BusType FROM BusData WHERE Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & baseDate.ToString & _
                                                    "' OR Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & nul.ToString & "'", con)

            Dim readDepartureDate As OleDbDataReader = getDepartureDate.ExecuteReader

            cmbBusType.Items.Clear()
            cmbBusType.Text = ""
            While (readDepartureDate.Read)

                cmbBusType.Items.Add(readDepartureDate.GetValue(0).ToString)

            End While

            'MessageBox.Show(strDepartureDate)

            con.Close()

            cmbBusType.Text = ""
            cmbBusID.Text = ""

            updateTextBox()

        End If
    End Sub

    Private Sub cmbBusType_TextChanged(sender As Object, e As EventArgs) Handles cmbBusType.TextChanged

        If cmbBusType.Text IsNot "" Then
            con.Close()
            con.Open()

            Dim monthNow As Integer = Date.Now.ToString("MM")
            Dim yearNow As Integer = Date.Now.ToString("yyyy")
            Dim displayDays As Integer = Date.Now.ToString("dd")
            Dim strDepartureDate As String = ""
            Dim baseDate As String = monthNow & "/" & displayDays & "/" & yearNow
            Dim nul As String = "EMPTY"

            Dim getPlateNum As New OleDbCommand("SELECT PlateNumber FROM BusData WHERE Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & baseDate.ToString & _
                                                    "' AND BusType = '" & cmbBusType.Text.ToString & _
                                                    "' OR Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & nul.ToString & _
                                                    "' AND BusType = '" & cmbBusType.Text.ToString & "'", con)

            Dim readDepartureDate As OleDbDataReader = getPlateNum.ExecuteReader

            cmbBusID.Items.Clear()
            cmbBusID.Text = ""
            While (readDepartureDate.Read)

                cmbBusID.Items.Add(readDepartureDate.GetValue(0).ToString)

            End While

            cmbBusID.Text = ""

            'MessageBox.Show(strDepartureDate)

            con.Close()
        End If

        updateTextBox()

    End Sub

    Private Sub cmbTime_TextChanged(sender As Object, e As EventArgs) Handles cmbTime.TextChanged

        If cmbTime.Text IsNot "" Then
            con.Close()
            con.Open()

            Dim monthNow As Integer = Date.Now.ToString("MM")
            Dim yearNow As Integer = Date.Now.ToString("yyyy")
            Dim displayDays As Integer = Date.Now.ToString("dd")
            Dim strDepartureDate As String = ""
            Dim baseDate As String = monthNow & "/" & displayDays & "/" & yearNow
            Dim nul As String = "EMPTY"

            Dim getDepartureDate As New OleDbCommand("SELECT BusType FROM BusData WHERE Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & baseDate.ToString & _
                                                    "' OR Origin = '" & cmbOrigin.Text.ToString & _
                                                    "' AND Destination = '" & cmbDestination.Text.ToString & _
                                                    "' AND Schedule = '" & cmbTime.Text.ToString & _
                                                    "' AND DepartureDate = '" & nul.ToString & "'", con)

            Dim readDepartureDate As OleDbDataReader = getDepartureDate.ExecuteReader

            cmbBusType.Items.Clear()
            cmbBusType.Text = ""
            While (readDepartureDate.Read)

                cmbBusType.Items.Add(readDepartureDate.GetValue(0).ToString)

            End While

            cmbBusType.Text = ""
            cmbBusID.Text = ""

            'MessageBox.Show(strDepartureDate)

            con.Close()
        End If

        updateTextBox()

    End Sub

    Private Sub cmbBusID_TextChanged(sender As Object, e As EventArgs) Handles cmbBusID.TextChanged

        If cmbBusID.Text = "" Then

            Dim b() As Button
            b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

            'disable other button
            For aaa = 0 To 60

                b(aaa).BackColor = Color.FromArgb(240, 240, 240)
                b(aaa).ForeColor = Color.Black
                b(aaa).Enabled = False

            Next
        Else

            Dim b() As Button
            b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

            'enable other button
            For aaa = 0 To 60

                b(aaa).Enabled = True

            Next

            updateSeats()
            updateTextBox()

        End If

    End Sub

    Sub updateSeats()

        If cmbBusID.Text IsNot "" Then
            Dim b() As Button
            b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}
            Dim seats() As String

            If cmbBusID.Text IsNot "" Then
                Try


                    con.Close()
                    con.Open()

                    Dim getOccupiedSeats As New OleDbCommand("SELECT OccupiedSeats FROM BusData WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
                    Dim readOccupiedSeats As OleDbDataReader = getOccupiedSeats.ExecuteReader

                    readOccupiedSeats.Read()
                    seats = (readOccupiedSeats.GetValue(0).ToString).Split(" ")

                    con.Close()

                    For aa = 0 To 60

                        b(aa).BackColor = Color.FromArgb(240, 240, 240)
                        b(aa).ForeColor = Color.Black

                    Next

                    For a = 0 To b.Length - 1
                        For s = 0 To seats.Length - 1
                            If b(a).Text = seats(s) Then
                                b(a).BackColor = Color.Black
                                b(a).ForeColor = Color.White
                            End If
                        Next
                    Next

                    txtPrice.ForeColor = Color.Black

                Catch ex As Exception

                    MessageBox.Show(ex.ToString)

                End Try
            Else

                ''to make every btn white or available
                For aa = 0 To 60

                    b(aa).BackColor = Color.FromArgb(240, 240, 240)
                    b(aa).ForeColor = Color.Black

                Next

            End If
        Else



        End If

    End Sub

    Private Sub btnSeatNumber15_Click(sender As Object, e As EventArgs) Handles btnSeatNumber15.Click

    End Sub

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged

        updateTextBox()

    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click

        ''display in richTextBox
        rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        rtbTicketDisplay.AppendText(vbNewLine & _
                                    "PAYMENT" & vbNewLine)
        rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        rtbTicketDisplay.AppendText(("Date Of Reservation: " & DateTime.Now.ToString("MM/dd/yyyy") & vbNewLine & _
                                     "Time Of Reservation: " & DateTime.Now.ToString("hh:mm tt") & vbNewLine & _
                                     "Total Amount: " & txtPrice.Text & vbNewLine & _
                                     "Total Payment: " & txtPayment.Text & vbNewLine & _
                                     "Total Change: " & txtChange.Text & vbNewLine & _
                                     "--------------------------------------------------------------------" & vbNewLine).ToString())

        con.Open()
        Dim getSeat As New OleDbCommand("SELECT OccupiedSeats FROM BusData WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
        Dim readSeats As OleDbDataReader = getSeat.ExecuteReader
        readSeats.Read()
        Dim strSeats As String = readSeats.GetValue(0).ToString
        Dim updateSeatAndDeparture As New OleDbCommand("UPDATE BusData SET OccupiedSeats = '" & strSeats.ToString & " " & seatNumber.ToString & _
                                                       "', DepartureDate = '" & cmbDate.Text.ToString & "' WHERE PlateNumber = '" & cmbBusID.Text.ToString & "'", con)
        updateSeatAndDeparture.ExecuteNonQuery()

        empAct = "ADD RESERVATION"
        Dim insertReservation As New OleDbCommand("INSERT INTO BusReservationActivityLog(EmployeeID,EmployeeName,Activity,Route,BusID,SeatNumber,DateOfActivity,TimeOfActivity,Profit) VALUES('" & lblReservationLoginID.Text.ToString & _
                                                  "','" & lblReservationLoginName.Text.ToString & _
                                                  "','" & empAct.ToString & _
                                                  "','" & cmbOrigin.Text.ToString & " - " & cmbDestination.Text.ToString & _
                                                  "','" & cmbBusID.Text.ToString & _
                                                  "','" & seatNumber.ToString &
                                                  "','" & DateTime.Now.ToString("MM/dd/yyyy") & _
                                                  "','" & DateTime.Now.ToString("hh:mm:ss tt") & _
                                                  "','" & txtPrice.Text.ToString & "')", con)

        Dim insertSalesReport As New OleDbCommand("INSERT INTO salesReport(SalesDate,Profit) VALUES('" & cmbDate.Text.ToString & "','" & txtPrice.Text.ToString & "')", con)

        insertSalesReport.ExecuteNonQuery()
        insertReservation.ExecuteNonQuery()
        con.Close()
        btnPrint.Enabled = True
        updateTextBox()

    End Sub

    Private Sub rdFemale_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdMale.CheckedChanged, rdFemale.CheckedChanged
        updateTextBox()
    End Sub



    Sub displayInTicket()

        rtbTicketDisplay.SelectionFont = New Font("Mirella Personal Use", 26, FontStyle.Regular)
        rtbTicketDisplay.AppendText("   VICTORY LINER" & vbNewLine)
        rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        rtbTicketDisplay.AppendText(vbNewLine & _
                                    "PASSENGER" & vbNewLine)
        rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        rtbTicketDisplay.AppendText(("NAME: " & txtPasseengerName.Text.ToString & vbNewLine & _
                                     "AGE: " & txtAge.Text.ToString & vbNewLine & _
                                     "GENDER: " & gender & vbNewLine & _
                                     "PASSENGER TYPE: " & cmbPassengerType.Text & vbNewLine & _
                                     "--------------------------------------------------------------------" & vbNewLine).ToString())

        rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        rtbTicketDisplay.AppendText(vbNewLine & _
                                    "RESERVATION INFO" & vbNewLine)
        rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        rtbTicketDisplay.AppendText(("Origin: " & cmbOrigin.Text.ToString & vbNewLine & _
                                     "Destination: " & cmbDestination.Text.ToString & vbNewLine & _
                                     "Bus Plate Number: " & cmbBusID.Text & vbNewLine & _
                                     "Seat Number: " & seatNumber & vbNewLine & _
                                     "Bus Type: " & cmbBusType.Text.ToString & vbNewLine & _
                                     "Date Of Departure: " & cmbDate.Text.ToString & vbNewLine & _
                                     "Time Of Departure: " & cmbTime.Text & vbNewLine & _
                                     "--------------------------------------------------------------------" & vbNewLine).ToString())

    End Sub



    Sub updateTextBox()

        'rtbDisplay.Clear()
        'rtbDisplay.SelectionFont = New Font("Ariel", 14, FontStyle.Regular)
        'rtbDisplay.AppendText(("NUMBER: " & txtStudentNumber.Text & vbNewLine).ToString())
        'rtbDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Regular)
        'rtbDisplay.AppendText(("NAME: " & txtStudentName.Text).ToString())



        'Dim gender As String = ""

        'If rdMale.Checked = True Then
        '    gender = "Male"

        'ElseIf rdFemale.Checked = True Then

        '    gender = "Female"

        'End If

        'rtbTicketDisplay.Clear()
        'rtbTicketDisplay.SelectionFont = New Font("Mirella Personal Use", 26, FontStyle.Regular)
        'rtbTicketDisplay.AppendText("   VICTORY LINER" & vbNewLine)
        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        'rtbTicketDisplay.AppendText(vbNewLine & _
        '                            "PASSENGER" & vbNewLine)
        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        'rtbTicketDisplay.AppendText(("NAME: " & txtPasseengerName.Text.ToString & vbNewLine & _
        '                             "AGE: " & txtAge.Text.ToString & vbNewLine & _
        '                             "GENDER: " & gender & vbNewLine & _
        '                             "PASSENGER TYPE: " & cmbPassengerType.Text & vbNewLine & _
        '                             "--------------------------------------------------------------------" & vbNewLine).ToString())

        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        'rtbTicketDisplay.AppendText(vbNewLine & _
        '                            "RESERVATION INFO" & vbNewLine)
        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        'rtbTicketDisplay.AppendText(("Origin: " & cmbOrigin.Text.ToString & vbNewLine & _
        '                             "Destination: " & cmbDestination.Text.ToString & vbNewLine & _
        '                             "Bus Plate Number: " & cmbBusID.Text & vbNewLine & _
        '                             "Seat Number: " & seatNumber & vbNewLine & _
        '                             "Bus Type: " & cmbBusType.Text.ToString & vbNewLine & _
        '                             "Date Of Departure: " & cmbDate.Text.ToString & vbNewLine & _
        '                             "Time Of Departure: " & cmbTime.Text & vbNewLine & _
        '                             "--------------------------------------------------------------------" & vbNewLine).ToString())

        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 12, FontStyle.Bold)
        'rtbTicketDisplay.AppendText(vbNewLine & _
        '                            "PAYMENT" & vbNewLine)
        'rtbTicketDisplay.SelectionFont = New Font("Ariel", 10, FontStyle.Regular)
        'rtbTicketDisplay.AppendText(("Date Of Reservation: " & DateTime.Now.ToString("MM/dd/yyyy") & vbNewLine & _
        '                             "Time Of Reservation: " & DateTime.Now.ToString("hh:mm tt") & vbNewLine & _
        '                             "Total Amount: " & txtPrice.Text & vbNewLine & _
        '                             "Total Payment: " & txtPayment.Text & vbNewLine & _
        '                             "Total Change: " & txtChange.Text & vbNewLine & _
        '                             "--------------------------------------------------------------------" & vbNewLine).ToString())


    End Sub



    'Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

    '    '97 - 122 = Ascii codes for simple letters
    '    '65 - 90  = Ascii codes for capital letters
    '    '48 - 57  = Ascii codes for numbers

    '    If Asc(e.KeyChar) <> 8 Then
    '        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
    '            e.Handled = True
    '        End If
    '    End If

    'End Sub



    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress, txtChange.KeyPress

        e.Handled = True

    End Sub


    Private Sub txtPayment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPayment.KeyPress, txtAge.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If

        End If
    End Sub

    Private Sub txtPasseengerName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasseengerName.KeyPress

        If Not (Asc(e.KeyChar) = 8) Then
            If Not ((Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or Asc(e.KeyChar) = 32) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        rtbTicketDisplay.SaveFile("tmpfile.rtf")

        Dim psi As New ProcessStartInfo

        psi.UseShellExecute = True

        psi.Verb = "print"

        psi.WindowStyle = ProcessWindowStyle.Hidden

        'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()

        psi.FileName = "tmpfile.rtf" ' Here specify a document to be printed

        Process.Start(psi)


        Dim b() As Button
        b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

        For aaa = 0 To 60

            b(aaa).Enabled = True

        Next

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString(rtbTicketDisplay.Text, rtbTicketDisplay.Font, Brushes.Black, 100, 100)
    End Sub

    Private Sub Button2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtChange_TextChanged(sender As Object, e As EventArgs) Handles txtChange.TextChanged
        If txtChange.Text = "00.00" Or txtChange.Text = "Not Enough Cash" Then
            btnPay.Enabled = False
        Else
            btnPay.Enabled = True
        End If
        updateTextBox()
    End Sub

    Private Sub txtPayment_TextChanged(sender As Object, e As EventArgs) Handles txtPayment.TextChanged
        If Val(txtPrice.Text) > Val(txtPayment.Text) Then
            txtChange.Text = "Not Enough Cash"
        Else
            txtChange.Text = Val(txtPayment.Text) - Val(txtPrice.Text)
        End If
        updateTextBox()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        txtPasseengerName.Text = ""
        txtAge.Text = ""
        cmbPassengerType.Text = ""
        cmbOrigin.Text = ""
        cmbDestination.Text = ""
        cmbDate.Text = ""
        cmbTime.Text = ""
        cmbBusType.Text = ""
        cmbBusID.Text = ""
        btnPay.Enabled = False
        btnPrint.Enabled = False
        Me.seatNumber = ""
        rtbTicketDisplay.Clear()

        Dim b() As Button
        b = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

        For aaa = 0 To 60

            If b(aaa).BackColor = Color.FromArgb(139, 0, 0) Then
                b(aaa).BackColor = Color.FromArgb(240, 240, 240)
                b(aaa).ForeColor = Color.Black
            End If

        Next

        updateTextBox()

    End Sub


    Private Sub btnClearPendingSeats_Click(sender As Object, e As EventArgs) Handles btnClearPendingSeats.Click

        Dim buttonList() As Button
        buttonList = {btnSeatNumber1, btnSeatNumber2, btnSeatNumber3, btnSeatNumber4, btnSeatNumber5, btnSeatNumber6, btnSeatNumber7, btnSeatNumber8, btnSeatNumber9, btnSeatNumber10, btnSeatNumber11, btnSeatNumber12, btnSeatNumber13, btnSeatNumber14, btnSeatNumber15, btnSeatNumber16, btnSeatNumber17, btnSeatNumber18, btnSeatNumber19, btnSeatNumber20, btnSeatNumber21, btnSeatNumber22, btnSeatNumber23, btnSeatNumber24, btnSeatNumber25, btnSeatNumber26, btnSeatNumber27, btnSeatNumber28, btnSeatNumber29, btnSeatNumber30, btnSeatNumber31, btnSeatNumber32, btnSeatNumber33, btnSeatNumber34, btnSeatNumber35, btnSeatNumber36, btnSeatNumber37, btnSeatNumber38, btnSeatNumber39, btnSeatNumber40, btnSeatNumber41, btnSeatNumber42, btnSeatNumber43, btnSeatNumber44, btnSeatNumber45, btnSeatNumber46, btnSeatNumber47, btnSeatNumber48, btnSeatNumber49, btnSeatNumber50, btnSeatNumber51, btnSeatNumber52, btnSeatNumber53, btnSeatNumber54, btnSeatNumber55, btnSeatNumber56, btnSeatNumber57, btnSeatNumber58, btnSeatNumber59, btnSeatNumber60, btnSeatNumber61}

        For Each b As Button In buttonList
            If b.BackColor = Color.FromArgb(139, 0, 0) Then
                b.BackColor = Color.FromArgb(240, 240, 240)
                b.ForeColor = Color.Black
            End If
        Next

        txtPrice.Text = ""
        txtPayment.Text = ""
        txtChange.Text = ""

        rtbTicketDisplay.Clear()

    End Sub
End Class
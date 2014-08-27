Imports System.IO
Imports System.Text.RegularExpressions

Public Class FileReaderForm

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            Dim filename As String
            filename = Application.StartupPath + "\Sample.txt"
            Dim iofile As New StreamReader(filename)
            If File.Exists(filename) Then

                'Determine How many lines in the text file
                Dim numlines As Integer = 0
                Using sr As New System.IO.StreamReader(filename)
                    Dim line As String = sr.ReadLine()
                    While line IsNot Nothing
                        numlines = numlines + 1
                        line = sr.ReadLine()
                    End While
                End Using

                'Loop through the text file
                Dim counter As Integer = 0
                Dim index As Integer
                While (counter < numlines - 1)
                    Dim ioline As String = System.IO.File.ReadAllLines(filename)(counter)
                    index = ioline.IndexOf("SPCLB60")
                    Dim mysplit = Regex.Split(ioline, "\s+")
                    Dim EmpNum As Integer
                    If (index = 0) Then
                        counter = counter + 8
                    Else
                        counter = counter + 1
                    End If

                    'Exit While Loop when reaching employee list section
                    If ((String.Compare(mysplit(0), "********") = 0)) Then
                        Exit While
                    End If

                    'Determine the Employee Number
                    If ((String.Compare(mysplit(0), "") <> 0)) And ((String.Compare(mysplit(0), "SPCLB60") <> 0)) Then
                        EmpNum = mysplit(0)
                    End If

                    'Save Each Line 
                    If ((String.Compare(mysplit(0), "SPCLB60") <> 0)) And ((String.Compare(mysplit(1), "T") <> 0)) Then
                        Dim empobj As New Employee
                        empobj.propEmpNum = EmpNum
                        empobj.propEmpAss = mysplit(4)
                        empobj.propPerc = mysplit(16)
                        empobj.propStartDate = mysplit(9)
                        empobj.AddEmployee()
                    End If

                End While
                MsgBox("Your Text File Has been exported successfully")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        

    End Sub
End Class

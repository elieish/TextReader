Public Class Employee
    Private EmpNum As Integer
    Private EmpAss As String
    Private EmpStarDate As String
    Private EmpPerc As String
    Public Sub New()
        EmpNum = 0
        EmpAss = ""
        EmpStarDate = ""
        EmpPerc = 0.0
    End Sub

    Public Sub New(ByVal Num As Integer, _
                   ByVal Ass As String, _
                   ByVal StartDate As String,
                   ByVal Perc As Decimal)
        propEmpNum = Num
        propEmpAss = Ass
        propStartDate = StartDate
        propPerc = Perc
    End Sub
    Public Property propPerc() As String
        Get
            Return EmpPerc
        End Get
        Set(ByVal value As String)
            EmpPerc = value
        End Set
    End Property

    Public Property propEmpNum() As Integer
        Get
            Return EmpNum
        End Get
        Set(ByVal value As Integer)
            EmpNum = value
        End Set
    End Property


    Public Property propStartDate() As String
        Get
            Return EmpStarDate
        End Get
        Set(ByVal value As String)
            EmpStarDate = value
        End Set
    End Property

    Public Property propEmpAss() As String
        Get
            Return EmpAss

        End Get
        Set(ByVal value As String)
            EmpAss = value
        End Set
    End Property


    Public Sub AddEmployee()
        Dim objdal As New CsDAL
        Dim objparlist As New List(Of CsParameterList)
        objparlist.Add(New CsParameterList("@memNumber", SqlDbType.Int, propEmpNum))
        objparlist.Add(New CsParameterList("@memAss", SqlDbType.VarChar, propEmpAss))
        objparlist.Add(New CsParameterList("@memStartDate", SqlDbType.VarChar, propStartDate))
        objparlist.Add(New CsParameterList("@memPerc", SqlDbType.VarChar, propPerc))
        objdal.executespreturnnd("Add_Employee", objparlist)
    End Sub

End Class

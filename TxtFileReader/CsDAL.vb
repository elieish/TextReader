Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient


Public Class CsDAL


    Protected _strconn As String = "Data Source=ELYSEE;Initial Catalog=TextReader;Integrated Security=True;"
    Private conn As SqlConnection
    Private errorstr As String = String.Empty

    Public Sub New()
        conn = New SqlConnection()
    End Sub

    Public Property ConnectionString() As String
        Get
            Return _strconn
        End Get
        Set(ByVal value As String)
            _strconn = value
        End Set
    End Property

    Public Function Open_Connection() As Boolean
        If conn Is Nothing Then
            conn = New SqlConnection()
        End If
        If conn.State = System.Data.ConnectionState.Closed Then
            Try
                conn.ConnectionString = ConnectionString
                conn.Open()
                Return True
            Catch a As Exception
                errorstr += " " & a.Message
                Return False
            End Try
        Else
            Return True
        End If
    End Function

    Public Function executespreturnds(ByVal spname As String, ByVal objlist As List(Of CsParameterList)) As System.Data.DataSet
        Dim cmd As New SqlCommand()
        Dim sda As New SqlDataAdapter()
        Dim ds As New DataSet()
        If Open_Connection() Then
            cmd.Connection = conn
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            cmd.CommandText = spname
            For Each par As CsParameterList In objlist
                cmd.Parameters.Add(add_parameters(par))
            Next
            sda.SelectCommand = cmd
            sda.Fill(ds)
        End If
        close_conn()
        Return ds
    End Function

    Public Function executespreturnds(ByVal spname As String) As System.Data.DataSet
        Dim cmd As New SqlCommand()
        Dim sda As New SqlDataAdapter()
        Dim ds As New DataSet()
        If Open_Connection() Then
            cmd.Connection = conn
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            cmd.CommandText = spname
            sda.SelectCommand = cmd
            sda.Fill(ds)
        End If
        close_conn()
        Return ds
    End Function

    Public Function executespreturndr(ByVal spname As String, ByVal objlist As List(Of CsParameterList)) As IDataReader
        Dim cmd As New SqlCommand()
        Dim dr As IDataReader = Nothing
        Try
            If Open_Connection() Then
                cmd.Connection = conn
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.CommandText = spname
                For Each par As CsParameterList In objlist
                    cmd.Parameters.Add(add_parameters(par))
                Next
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If
            Return dr
        Catch e As Exception
            errorstr += " " & e.Message
            Return Nothing
        End Try
    End Function

    Public Function executespreturndr(ByVal spname As String) As IDataReader
        Dim cmd As New SqlCommand()
        Dim dr As IDataReader = Nothing
        Try
            If Open_Connection() Then
                cmd.Connection = conn
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.CommandText = spname
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If
            Return dr
        Catch e As Exception
            errorstr += " " & e.Message
            Return Nothing
        End Try
    End Function

    Public Sub executespreturnnd(ByVal spname As String)
        Dim cmd As New SqlCommand()
        Try
            If Open_Connection() Then
                cmd.Connection = conn
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.CommandText = spname
                cmd.ExecuteNonQuery()
            End If
            close_conn()
        Catch e As Exception
            errorstr += " " & e.Message
        End Try
    End Sub
    'used to save data to the DB
    'accepts 2 arguments from Class Course: 1. SP; 2. List obj containing the parameters
    Public Sub executespreturnnd(ByVal spname As String, ByVal objlist As List(Of CsParameterList))
        'sql command obj. Need to set the props.
        Dim cmd As New SqlCommand()
        Try
            If Open_Connection() Then
                cmd.Connection = conn
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.CommandText = spname
                'loop to iterate through each instance in List obj
                'each instance will contain the information needed to write to DB
                'use the internal function 'add_parameters' 
                ''add_parameters' will create an sqlParameter for each instance in the List obj
                'each parameter is added to the SqlCommand obj
                For Each par As CsParameterList In objlist
                    cmd.Parameters.Add(add_parameters(par))
                Next
                'fires the SqlCommand obj just set
                cmd.ExecuteNonQuery()
            End If
            close_conn()
        Catch e As Exception
            errorstr += " " & e.Message
        End Try
    End Sub

    'values from the List obj are used to set props of the SQL parameter
    'SQLParameter object is the same as vbParameterListType but is part of the SQLClient Namespace
    Private Function add_parameters(ByVal objpar As CsParameterList) As SqlParameter
        Dim sqlpar As New SqlParameter()
        sqlpar.ParameterName = objpar.Name
        sqlpar.SqlDbType = objpar.SqlType
        sqlpar.SqlValue = objpar.Value
        Return sqlpar
    End Function

    Private Sub close_conn()
        If conn.State = System.Data.ConnectionState.Open Then
            conn.Close()
        End If
        conn = Nothing
    End Sub

End Class


﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Public Class CsParameterList


    Private _Name As String
    Private _SqlType As SqlDbType
    Private _Value As String

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property SqlType() As SqlDbType
        Get
            Return _SqlType
        End Get
        Set(ByVal value As SqlDbType)
            _SqlType = value
        End Set
    End Property

    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property

    Public Sub New(ByVal name__1 As String, ByVal Stype As SqlDbType, ByVal value__2 As String)
        Name = name__1
        SqlType = Stype
        Value = value__2
    End Sub

End Class




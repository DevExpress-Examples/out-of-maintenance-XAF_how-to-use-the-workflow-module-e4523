Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base

Namespace WorkflowExample.Module.BusinessObjects
    <DefaultClassOptions, ImageName("BO_Task"), DefaultProperty("Subject")> _
    Public Class Task
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Subject() As String
            Get
                Return GetPropertyValue(Of String)("Subject")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Subject", value)
            End Set
        End Property
        Public Property Issue() As Issue
            Get
                Return GetPropertyValue(Of Issue)("Issue")
            End Get
            Set(ByVal value As Issue)
                SetPropertyValue(Of Issue)("Issue", value)
            End Set
        End Property
    End Class
End Namespace

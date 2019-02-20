Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base

Namespace WorkflowExample.Module.BusinessObjects
	<DefaultClassOptions, ImageName("BO_Note"), DefaultProperty("Subject")>
	Public Class Issue
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
		Public Property Active() As Boolean
			Get
				Return GetPropertyValue(Of Boolean)("Active")
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue(Of Boolean)("Active", value)
			End Set
		End Property
	End Class
End Namespace

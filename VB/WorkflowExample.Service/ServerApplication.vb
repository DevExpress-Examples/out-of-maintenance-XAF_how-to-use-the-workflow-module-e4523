Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo

Namespace WorkflowExample.Service
	Public Class ServerApplication
		Inherits XafApplication

		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
		End Sub
		Protected Overrides Function CreateLayoutManagerCore(ByVal simple As Boolean) As DevExpress.ExpressApp.Layout.LayoutManager
			Throw New NotImplementedException()
		End Function
		Public Sub Logon()
			MyBase.Logon(Nothing)
		End Sub
	End Class
End Namespace

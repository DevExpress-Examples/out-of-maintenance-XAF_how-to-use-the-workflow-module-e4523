Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ServiceProcess
Imports System.Text

Namespace WorkflowExample.Service
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Sub Main()
			Dim ServicesToRun() As ServiceBase
			ServicesToRun = New ServiceBase() { New Service() }
			ServiceBase.Run(ServicesToRun)
		End Sub
	End Module
End Namespace

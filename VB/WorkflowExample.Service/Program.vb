Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ServiceProcess
Imports System.Text

Namespace WorkflowExample.Service
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Shared Sub Main()
            Dim ServicesToRun() As ServiceBase
            ServicesToRun = New ServiceBase() { New Service() }
            ServiceBase.Run(ServicesToRun)
        End Sub
    End Class
End Namespace

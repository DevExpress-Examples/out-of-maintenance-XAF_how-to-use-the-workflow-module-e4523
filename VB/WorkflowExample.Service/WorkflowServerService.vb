Imports System
Imports System.Configuration
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Linq
Imports System.ServiceProcess
Imports System.Text
Imports DevExpress.ExpressApp.Workflow.Server
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Workflow
Imports DevExpress.ExpressApp.MiddleTier
Imports WorkflowExample.Module
Imports DevExpress.ExpressApp.Security.Strategy

Namespace WorkflowExample.Service
    Partial Public Class Service
        Inherits System.ServiceProcess.ServiceBase

        Private server As WorkflowServer
        Protected Overrides Sub OnStart(ByVal args() As String)
            If server Is Nothing Then
                Dim serverApplication As New ServerApplication()
                serverApplication.ApplicationName = "WorkflowExample"
                ' The service can only manage workflows for those business classes that are contained in Modules specified by the serverApplication.Modules collection.
                ' So, do not forget to add the required Modules to this collection via the serverApplication.Modules.Add method.

                serverApplication.Modules.Add(New WorkflowModule())
                serverApplication.Modules.Add(New WorkflowExampleModule())

                If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                    serverApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
                End If
                serverApplication.Setup()
                serverApplication.Logon()

                Dim objectSpaceProvider As IObjectSpaceProvider = serverApplication.ObjectSpaceProvider

                server = New WorkflowServer("http://localhost:46232", objectSpaceProvider, objectSpaceProvider)

                server.StartWorkflowListenerService.DelayPeriod = TimeSpan.FromSeconds(15)
                server.StartWorkflowByRequestService.DelayPeriod = TimeSpan.FromSeconds(15)
                server.RefreshWorkflowDefinitionsService.DelayPeriod = TimeSpan.FromMinutes(15)
                server.HostManager.CloseHostTimeout = System.TimeSpan.FromSeconds(20)

                AddHandler server.CustomizeHost, Sub(sender As Object, e As CustomizeHostEventArgs)
                    e.WorkflowInstanceStoreBehavior.WorkflowInstanceStore.RunnableInstancesDetectionPeriod = TimeSpan.FromSeconds(15)
                End Sub

                AddHandler server.CustomHandleException, Sub(sender As Object, e As CustomHandleServiceExceptionEventArgs)
                    Tracing.Tracer.LogError(e.Exception)
                    e.Handled = False
                End Sub
            End If
            server.Start()
        End Sub
        Protected Overrides Sub OnStop()
            server.Stop()
        End Sub
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace

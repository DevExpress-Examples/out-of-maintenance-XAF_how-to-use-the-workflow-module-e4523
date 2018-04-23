Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.Linq


Namespace WorkflowExample.Service
    <RunInstaller(True)> _
    Partial Public Class ProjectInstaller
        Inherits System.Configuration.Install.Installer

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace

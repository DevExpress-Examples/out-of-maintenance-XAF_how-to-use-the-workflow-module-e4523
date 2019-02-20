Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports WorkflowExample.Module.BusinessObjects
Imports DevExpress.ExpressApp.Workflow.Xpo
Imports System.IO
Imports System.Reflection
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Security

Namespace WorkflowExample.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater

		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			If ObjectSpace.GetObjects(Of Issue)().Count = 0 Then
				Dim issue1 As Issue = ObjectSpace.CreateObject(Of Issue)()
				issue1.Subject = "Processed issue"
				issue1.Active = False
				Dim issue2 As Issue = ObjectSpace.CreateObject(Of Issue)()
				issue2.Subject = "Active issue"
				issue2.Active = True
			End If

			If ObjectSpace.GetObjects(Of XpoWorkflowDefinition)().Count = 0 Then
				Dim definition As XpoWorkflowDefinition = ObjectSpace.CreateObject(Of XpoWorkflowDefinition)()
				definition.Name = "Create Task for active Issue"
				definition.Xaml = GetXaml("WorkflowExample.Module.DatabaseUpdate.CreateTaskForActiveIssue.xml")
				definition.TargetObjectType = GetType(Issue)
				definition.AutoStartWhenObjectFitsCriteria = True
				definition.Criteria = "[Active] = True"
				definition.IsActive = True
			End If
			Dim adminRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", SecurityStrategy.AdministratorRoleName))
			If adminRole Is Nothing Then
				adminRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				adminRole.Name = SecurityStrategy.AdministratorRoleName
				adminRole.IsAdministrative = True
				adminRole.Save()
			End If
			Dim adminUser As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "Administrator"))
			If adminUser Is Nothing Then
				adminUser = ObjectSpace.CreateObject(Of SecuritySystemUser)()
				adminUser.UserName = "Administrator"
				adminUser.SetPassword("")
				adminUser.Roles.Add(adminRole)
			End If
			Dim workflowServiceUser As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "WorkflowService"))
			If workflowServiceUser Is Nothing Then
				workflowServiceUser = ObjectSpace.CreateObject(Of SecuritySystemUser)()
				workflowServiceUser.UserName = "WorkflowService"
				workflowServiceUser.SetPassword("")
				workflowServiceUser.Roles.Add(adminRole)
			End If
			ObjectSpace.CommitChanges()
		End Sub

		Private Function GetXaml(ByVal resourceName As String) As String
			Using stream As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
			Using reader As New StreamReader(stream)
				Dim version As Version = System.Reflection.Assembly.GetAssembly(GetType(XpoWorkflowDefinition)).GetName().Version
				Return String.Format(reader.ReadToEnd(), version.Major, version.Minor)
			End Using
			End Using
		End Function
	End Class
End Namespace

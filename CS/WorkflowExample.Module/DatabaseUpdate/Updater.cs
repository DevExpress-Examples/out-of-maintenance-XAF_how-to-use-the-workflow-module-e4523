using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using WorkflowExample.Module.BusinessObjects;
using DevExpress.ExpressApp.Workflow.Xpo;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;

namespace WorkflowExample.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            if(ObjectSpace.GetObjects<Issue>().Count == 0) {
                Issue issue1 = ObjectSpace.CreateObject<Issue>();
                issue1.Subject = "Processed issue";
                issue1.Active = false;
                Issue issue2 = ObjectSpace.CreateObject<Issue>();
                issue2.Subject = "Active issue";
                issue2.Active = true;
            }

            if(ObjectSpace.GetObjects<XpoWorkflowDefinition>().Count == 0) {
                XpoWorkflowDefinition definition = ObjectSpace.CreateObject<XpoWorkflowDefinition>();
                definition.Name = "Create Task for active Issue";
                definition.Xaml = GetXaml("WorkflowExample.Module.DatabaseUpdate.CreateTaskForActiveIssue.xml");
                definition.TargetObjectType = typeof(Issue);
                definition.AutoStartWhenObjectFitsCriteria = true;
                definition.Criteria = "[Active] = True";
                definition.IsActive = true;
            }
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(
                new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (adminRole == null) {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
                adminRole.Save();
            }
            SecuritySystemUser adminUser = ObjectSpace.FindObject<SecuritySystemUser>(
                new BinaryOperator("UserName", "Administrator"));
            if (adminUser == null) {
                adminUser = ObjectSpace.CreateObject<SecuritySystemUser>();
                adminUser.UserName = "Administrator";
                adminUser.SetPassword("");
                adminUser.Roles.Add(adminRole);
            }
            SecuritySystemUser workflowServiceUser = ObjectSpace.FindObject<SecuritySystemUser>(
                new BinaryOperator("UserName", "WorkflowService"));
            if (workflowServiceUser == null) {
                workflowServiceUser = ObjectSpace.CreateObject<SecuritySystemUser>();
                workflowServiceUser.UserName = "WorkflowService";
                workflowServiceUser.SetPassword("");
                workflowServiceUser.Roles.Add(adminRole);
            }
            ObjectSpace.CommitChanges();
        }

        string GetXaml(string resourceName) {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                Version version = Assembly.GetAssembly(typeof(XpoWorkflowDefinition)).GetName().Version;
                return string.Format(reader.ReadToEnd(), version.Major, version.Minor);
            }
        }
    }
}

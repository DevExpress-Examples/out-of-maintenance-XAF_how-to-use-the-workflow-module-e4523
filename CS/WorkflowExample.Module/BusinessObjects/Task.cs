using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace WorkflowExample.Module.BusinessObjects {
    [DefaultClassOptions, ImageName("BO_Task"), DefaultProperty("Subject")]
    public class Task : BaseObject {
        public Task(Session session) : base(session) { }
        public string Subject {
            get { return GetPropertyValue<string>("Subject"); }
            set { SetPropertyValue<string>("Subject", value); }
        }
        public Issue Issue {
            get { return GetPropertyValue<Issue>("Issue"); }
            set { SetPropertyValue<Issue>("Issue", value); }
        }
    }
}

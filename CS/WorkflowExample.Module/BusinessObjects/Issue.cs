using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace WorkflowExample.Module.BusinessObjects {
    [DefaultClassOptions, ImageName("BO_Note"), DefaultProperty("Subject")]
    public class Issue : BaseObject {
        public Issue(Session session) : base(session) { }
        public string Subject {
            get { return GetPropertyValue<string>("Subject"); }
            set { SetPropertyValue<string>("Subject", value); }
        }
        public bool Active {
            get { return GetPropertyValue<bool>("Active"); }
            set { SetPropertyValue<bool>("Active", value); }
        }
    }
}

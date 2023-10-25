using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxTestSolution.Module.BusinessObjects;
[DefaultClassOptions]
public class Contact : BaseObject {
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual DateTime BirthDate { get; set; }

    public virtual ObservableCollection<MyTask> MyTasks { get; set; } = new ObservableCollection<MyTask>();
}
[DefaultClassOptions]
public class MyTask : BaseObject {
    public virtual string Subject{get;set;}
    public virtual Contact AssignedTo{ get; set; }
}

   // public DbSet<MyTask> MyTasks { get; set; }
    // public DbSet<Contact> Contacts { get; set; }


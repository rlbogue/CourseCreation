using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookPublishing.Utilities
{
    public class InvokeBindingList<T> : BindingList<T>
    {
        protected delegate void BaseOnListChangedDelegate(InvokeBindingList<T> inThis, ListChangedEventArgs e);
        protected delegate void BaseOnAddingNewDelegate(InvokeBindingList<T> inThis, AddingNewEventArgs e);

        ISynchronizeInvoke syncInvoke;
        public InvokeBindingList(ISynchronizeInvoke _syncInvoke)
        {
            syncInvoke = _syncInvoke;
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (syncInvoke == null) { base.OnListChanged(e); return; }
            if (syncInvoke.InvokeRequired)
            {
                syncInvoke.BeginInvoke(new BaseOnListChangedDelegate(StaticBaseOnListChanged), new object[] { this, e });
            }
            else
            {
                base.OnListChanged(e);
            }

        }

        public void BaseOnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        protected static void StaticBaseOnListChanged(InvokeBindingList<T> inThis, ListChangedEventArgs e)
        {
            inThis.BaseOnListChanged(e);
        }

        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            if (syncInvoke == null) { base.OnAddingNew(e); return; }
            if (syncInvoke.InvokeRequired)
            {
                syncInvoke.BeginInvoke(new BaseOnAddingNewDelegate(StaticBaseOnAddingNew), new object[] { this, e });
            }
            else
            {
                base.OnAddingNew(e);
            }
        }

        protected static void StaticBaseOnAddingNew(InvokeBindingList<T> inThis, AddingNewEventArgs e)
        {
            inThis.BaseOnAddingNew(e);
        }

        protected void BaseOnAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

    }
}

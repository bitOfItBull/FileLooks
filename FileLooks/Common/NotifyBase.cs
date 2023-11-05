using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonLib.Common
{
    public class NotifyBase : INotifyPropertyChanged
    {

        public delegate void OnRusultCallback(NotifyBase notify);


        public event PropertyChangedEventHandler PropertyChanged;

        public void DoNotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

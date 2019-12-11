using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EspejoAnalysis.Helper
{
    [Serializable]
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

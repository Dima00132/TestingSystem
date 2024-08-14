using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Model
{
    public sealed partial class Statistics:ObservableObject
    {
        [ObservableProperty]
        private string _question;
        [ObservableProperty]
        ObservableCollection<string> _answers;
    }
}

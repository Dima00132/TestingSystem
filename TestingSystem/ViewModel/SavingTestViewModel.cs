using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.ViewModel.Base;

namespace TestingSystem.ViewModel
{
    public sealed partial class SavingTestViewModel : ViewModelBase
    {
        private readonly IPopupService _popupService;

        public SavingTestViewModel(IPopupService popupService)
        {
            _popupService = popupService;
        }

    }
}

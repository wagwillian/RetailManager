﻿using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private SimpleContainer _container;
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM,
            SimpleContainer container)
        {
            _events = events;
            _salesVM = salesVM;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);            
        }
    }
}

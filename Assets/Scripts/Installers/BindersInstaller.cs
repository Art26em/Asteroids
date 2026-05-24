using MVVM;
using UI.Binders;
using UI.ViewModels;
using UI.Views;
using Zenject;

namespace Installers
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ReactiveCollectionBinder<HealthItemView, HealthItemViewModel>>();
        }
    }
} 
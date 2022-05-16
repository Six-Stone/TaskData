using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskData.Common;
using TaskData.Services;
using TaskData.ViewModels;
using TaskData.Views;

namespace TaskData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }
        //注销用户操作
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }

        //验证
        protected override void OnInitialized()
        {
            //var dialog = Container.Resolve<IDialogService>();
            //dialog.ShowDialog("LoginView", callback =>
            //{
            //    if (callback.Result != ButtonResult.OK)
            //    {
            //        Environment.Exit(0);
            //        return;
            //    }
            //});
            var service = App.Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
                service.Configure();
            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                 .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://10.84.20.24:5000/", serviceKey: "webUrl");

            containerRegistry.Register<IToDoService, ToDoService>();

            
            containerRegistry.Register<IStuTaskNoService, StuTaskNoService>();
            containerRegistry.Register<ISiXainCheDto, SiXainCheService>();
            containerRegistry.Register<ICarService, CarService>();
            containerRegistry.Register<IGetNodeTypeService, GetNodeTypeService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<SiXainCheView, SiXainCheViewModel>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<SearchSubTasksView, SearchSubTasksViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<NodeView, NodeViewModel>();
            containerRegistry.RegisterForNavigation<SubTaskNoView, SubTaskNoViewModel>();
            containerRegistry.RegisterForNavigation<ConLifeTCHandleView, ConLifeTCHandleViewModel>();
        }
    }
}

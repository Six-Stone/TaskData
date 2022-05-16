using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskData.Common;
using TaskData.Common.Models;
using TaskData.Extensions;

namespace TaskData.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            //CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            //上一步
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            //下一步
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
            LoginOutCommand = new DelegateCommand(() =>
            {
                //注销当前用户
                //App.LoginOut(containerProvider);
            });
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;
            
        }

        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        private IRegionNavigationJournal journal;

        public DelegateCommand GoForwardCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }
        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
        }

        void CreateMenuBar()
        {
            
            //MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "任务查询", NameSpace = "ToDoView" });
           
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "任务管理", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "四向车任务查询", NameSpace = "SearchSubTasksView" });
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "状态查询", NameSpace = "NodeView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }
        public void Configure()
        {
            CreateMenuBar();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }
       
    }
}

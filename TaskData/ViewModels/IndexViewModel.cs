using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;
using TaskData.Common;
using TaskData.Common.Models;
using TaskData.Extensions;
using TaskData.Services;

namespace TaskData.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        
        private readonly IStuTaskNoService memoService;
        private readonly IDialogHostService dialog;
        private readonly IRegionManager regionManager;
      

        public IndexViewModel(IContainerProvider provider,
                   IDialogHostService dialog) : base(provider)
        {
            Title = $"你好，{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
            //添加数据窗口
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.memoService = provider.Resolve<IStuTaskNoService>();
            this.regionManager = provider.Resolve<IRegionManager>();
            this.dialog = dialog;
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }
  
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(null); break;
            }
        }
        async void AddToDo(SubTaskViewDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.ShowDialog("AddToDoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var todo = dialogResult.Parameters.GetValue<SubTaskViewDto>("Value");
                    if (todo!=null)
                    {
                        await memoService.ActionStuTaskNO(todo.subTaskNo);
                       
                    }
                }
                finally
                {
                    UpdateLoading(false);
                }
            }
            var str = await dialog.Question("温馨提示", $"强制完成任务成功");
            if (str.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
        }

        private void Navigate(TaskBar obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Target)) return;

            NavigationParameters param = new NavigationParameters();

            if (obj.Title == "已完成")
            {
                param.Add("Value", 2);
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target, param);
        }
    }
}

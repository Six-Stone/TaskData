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
using Task.NLog;
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
        private readonly ISiXainCheDto siXainCheDto;

        public IndexViewModel(IContainerProvider provider,
                   IDialogHostService dialog) : base(provider)
        {
            Title = $"你好，{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
            //添加数据窗口
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.memoService = provider.Resolve<IStuTaskNoService>();
            this.regionManager = provider.Resolve<IRegionManager>();
            this.siXainCheDto = provider.Resolve<ISiXainCheDto>();
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
                case "强制完成子任务": AddToDo(null); break;
                case "四项车上下线": SiXainChe(null); break;
                case "下发子任务": AddTask(null); break;

                case "强制完成四向车任务": ConLifeTCHandle(null); break;
                case "取消四向车充电任务": CancelCharge(null); break;
                case "呼叫大件库托盘": ConvCallTray(); break;
            }
        }

        async void CancelCharge(CarTaskViewDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.ShowDialog("CancelChargeView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var commStr = dialogResult.Parameters.GetValue<CarTaskViewDto>("Value");
                    if (commStr != null)
                    {
                        //调用接口 取消充电任务
                        await memoService.CancelCharge(commStr.shuttleNo);
                        LogHelp.WriteLog("取消充电任务，取消充电任务车辆" + commStr.shuttleNo);
                        // NLogManager.LogFourWayShuttleMessage.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                        //NLogManager.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                    }

                }

                finally
                {
                    UpdateLoading(false);
                }
            }
        }
        async void ConvCallTray()
        {
                try
                {
                    UpdateLoading(true);
                    
                        //调用接口 取消充电任务
                        await siXainCheDto.ConvCallTray();
                        LogHelp.WriteLog("呼叫大件库托盘");
                        // NLogManager.LogFourWayShuttleMessage.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                        //NLogManager.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                    

                }

                finally
                {
                    UpdateLoading(false);
                }
            
        }

        //强制完成四向车，提升机任务
        async void ConLifeTCHandle(CarTaskViewDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.ShowDialog("ConLifeTCHandleView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var commStr = dialogResult.Parameters.GetValue<CarTaskViewDto>("Value");
                    if (commStr != null)
                    {
                        //调用接口 强制完成四向车，提升机任务
                        await memoService.ConLifeTCHandle(commStr.commandNo);
                        LogHelp.WriteLog("强制四项车，提升机任务，任务号" + commStr.commandNo);
                        // NLogManager.LogFourWayShuttleMessage.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                        //NLogManager.Info("强制四项车，提升机任务，任务号" + commStr.commandNo);
                    }

                }

                finally
                {
                    UpdateLoading(false);
                }
            }
        }

        //下发任务
        async void AddTask(SubTaskViewDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.ShowDialog("SubTaskNoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var todo = dialogResult.Parameters.GetValue<SubTaskViewDto>("Value");
                    if (todo != null)
                    {
                        //调用接口 下发任务
                        await memoService.SendSubTask(todo.subTaskNo);
                        //NLogManager.LogAdminMessage.Info("下发子任务："+todo.subTaskNo);
                        LogHelp.WriteLog("下发子任务：" + todo.subTaskNo);
                        // NLogManager.Info("下发子任务：" + todo.subTaskNo);
                    }

                }

                finally
                {
                    UpdateLoading(false);
                }
            }
        }
        //强制完成子任务
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
                    if (todo != null)
                    {
                        //调用接口 强制完成任务
                        await memoService.ActionStuTaskNO(todo.subTaskNo);
                        LogHelp.WriteLog("强制完成子任务：" + todo.subTaskNo);
                        //NLogManager.LogAdminMessage.Info("强制完成子任务：" + todo.subTaskNo);
                        // NLogManager.Info("强制完成子任务：" + todo.subTaskNo);
                    }

                }

                finally
                {
                    UpdateLoading(false);
                }
            }

        }
        /// <summary>
        /// 四向车上下线
        /// </summary>
        /// <param name="model"></param>
        async void SiXainChe(SubTaskViewDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await dialog.ShowDialog("SiXainCheView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var todo = dialogResult.Parameters.GetValue<SiXainCheDto>("Value");
                    if (todo != null)
                    {
                        //调用接口 完成上下线任务
                        await siXainCheDto.ShuttleOnOffLine(todo);
                        LogHelp.WriteLog("操作四向车上下线，操作车辆" + todo.ShuttleNo + "动作" + todo.IsActive);
                        //NLogManager.LogFourWayShuttleMessage.Info("操作四向车上下线，操作车辆" + todo.ShuttleNo+"动作"+todo.IsActive);
                        //NLogManager.Info("操作四向车上下线，操作车辆" + todo.ShuttleNo + "动作" + todo.IsActive);
                    }
                    //var str = await dialog.Question("温馨提示", $"成功");
                    //if (str.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
                }

                finally
                {
                    UpdateLoading(false);
                }
            }

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

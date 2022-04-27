using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TackNo.Shared.Dtos;
using TaskData.Common;
using TaskData.Extensions;
using TaskData.Services;

namespace TaskData.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost;

        public ToDoViewModel(IToDoService service, IContainerProvider provider)
            : base(provider)
        {
            ToDoDtos = new ObservableCollection<TaskViewDto>();
            SubTask = new ObservableCollection<SubTaskViewDto>();
            SelectedCommand = new DelegateCommand<TaskViewDto>(Selected);
            //强制完成任务
            DeleteCommand = new DelegateCommand<TaskViewDto>(Delete);
            dialogHost = provider.Resolve<IDialogHostService>();
            this.service = service;

            GetDataAsync();
        }

        private async void Delete(TaskViewDto obj)
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认查看任务:{obj.id}");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        private async void Selected(TaskViewDto obj)
        {
            try
            {
                //var dialogResult = await dialogHost.Question("温馨提示", $"确认查看任务:{obj.id}");
                //if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
                UpdateLoading(true);

                var todoResult = await service.GetStuTaskAsync<SubTaskViewDto>(obj.id);
                SubTask.Clear();
                foreach (var item in todoResult)
                {

                    //CurrentDto = item;
                    SubTask.Add(item);
                }

                

                IsRightDrawerOpen = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }
        }

        

        private int selectedIndex;

        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }


        private string search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public DelegateCommand<TaskViewDto> SelectedCommand { get; private set; }
        public DelegateCommand<TaskViewDto> DeleteCommand { get; private set; }

        private ObservableCollection<TaskViewDto> toDoDtos;
        private readonly IToDoService service;

        public ObservableCollection<TaskViewDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<SubTaskViewDto> subTask;

        /// <summary>
        /// 选中的值
        /// </summary>
        public ObservableCollection<SubTaskViewDto> SubTask
        {
            get { return subTask; }
            set { subTask = value; RaisePropertyChanged(); }
        }

        private SubTaskViewDto currentDto;

        /// <summary>
        /// 选中的值
        /// </summary>
        public SubTaskViewDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true);

            var todoResult = await service.GetAllFilterAsync<TaskViewDto>();


            ToDoDtos.Clear();
            foreach (var item in todoResult)
            {
                ToDoDtos.Add(item);
            }

            UpdateLoading(false);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("Value"))
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
            else
                SelectedIndex = 0;
            GetDataAsync();
        }
    }
}

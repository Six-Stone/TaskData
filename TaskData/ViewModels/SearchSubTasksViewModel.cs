using Prism.Commands;
using Prism.Ioc;
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
    public class SearchSubTasksViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost;
        public SearchSubTasksViewModel(ICarService service, IContainerProvider provider)
           : base(provider)
        {
            ToDoDtos = new ObservableCollection<CarTaskViewDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            DeleteCommand = new DelegateCommand<CarTaskViewDto>(Delete);
            dialogHost = provider.Resolve<IDialogHostService>();
            this.service = service;
        }
        private CarTaskViewDto currentDto;

        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public CarTaskViewDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        private async void Delete(CarTaskViewDto obj)
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认补发{obj.shuttleNo} 的任务{obj.commandNo}?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var deleteResult = await service.ActionCaerNO(CurrentDto);
                
                
                    //var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    //if (model != null)
                      //ToDoDtos.Remove(model);
                
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        private readonly ICarService service;
        private ObservableCollection<CarTaskViewDto> toDoDtos;
        public ObservableCollection<CarTaskViewDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
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

        public DelegateCommand<CarTaskViewDto> DeleteCommand { get; private set; }
        private string search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
       
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }
        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            CurrentDto = new CarTaskViewDto();
            IsRightDrawerOpen = true;
        }
        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.commandNo) ||
                string.IsNullOrWhiteSpace(CurrentDto.shuttleNo))
                return;

            UpdateLoading(true);

            try
            {
                if (CurrentDto.shuttleNo!=null)
                {
                    var updateResult = await service.ActionCaerNO(CurrentDto);
                    IsRightDrawerOpen = false;
                }
              
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }
        }
        async void GetDataAsync()
        {
            UpdateLoading(true);

            string Status = Search;

            var todoResult = await service.GetSearchSubTasksCaerNo<CarTaskViewDto>(Status);
            ToDoDtos.Clear();
            foreach (var item in todoResult)
            {
                ToDoDtos.Add(item);
            }
            //IsRightDrawerOpen = true;
            UpdateLoading(false);
        }
    }
}

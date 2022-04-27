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
            //SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            //DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            //dialogHost = provider.Resolve<IDialogHostService>();
            this.service = service;
        }
        private readonly ICarService service;
        private ObservableCollection<CarTaskViewDto> toDoDtos;
        public ObservableCollection<CarTaskViewDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "查询": GetDataAsync(); break;
            }
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
        async void GetDataAsync()
        {
            UpdateLoading(true);


            var str = Search;
            var todoResult = await service.GetSearchSubTasksCaerNo<CarTaskViewDto>(str);



            ToDoDtos.Clear();
            foreach (var item in todoResult)
            {
                ToDoDtos.Add(item);
            }

            UpdateLoading(false);
        }
    }
}

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
using TaskData.Services;

namespace TaskData.ViewModels
{
   public class NodeViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost;
        public NodeViewModel(IGetNodeTypeService service, IContainerProvider provider)
            : base(provider)
        {
            ToDoDtos = new ObservableCollection<NodeTypeViewDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            dialogHost = provider.Resolve<IDialogHostService>();
            this.service = service;
        }
        private readonly IGetNodeTypeService service;
        private ObservableCollection<NodeTypeViewDto> toDoDtos;
        public ObservableCollection<NodeTypeViewDto> ToDoDtos
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

            string Status = Search;

            var todoResult = await service.GetNodeType<NodeTypeViewDto>(Status);
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

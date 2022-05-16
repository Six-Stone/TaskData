using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    public class SiXainCheDto : BaseDto
    {
        //shuttleNo
        private string shuttleNo;
        
        private string isActive;


        public string ShuttleNo
        {
            get { return shuttleNo; }
            set { shuttleNo = value; OnPropertyChanged(); }
        }
        public string IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TackNo.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string userName="HGT";

        public string UserName
        {
            get { return "HGT"; }
            set { userName = value; OnPropertyChanged(); }
        }



        private string passWord="HGT123";

        public string PassWord
        {
            get { return "HGT123"; }
            set { passWord = value; OnPropertyChanged(); }
        }
    }
}

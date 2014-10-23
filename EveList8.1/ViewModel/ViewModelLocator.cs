using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveList8._1.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main { get; private set; }
        public AuthViewModel Auth { get; private set; }

        public ViewModelLocator()
        {
            Main = new MainViewModel();
            Auth = new AuthViewModel();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    interface IBookController
    {
        public BookCollection SearchBook(string searchParam);
    }
}

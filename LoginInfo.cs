using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankDemo
{
    public static class LoginInfo
    {
        public static BlankDemo.Model.UserInfo CurrentUser { get; set; }
    }
}

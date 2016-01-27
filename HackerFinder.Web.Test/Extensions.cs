using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HackerFinder.Web.Test
{
    public static class Extensions
    {
        public static T GetModel<T>(this ViewResult view)
        {
            return ((T)view.Model);
        }

        public static T GetModel<T>(this ActionResult action)
        {
            return GetModel<T>((ViewResult)action);
        }
    }
}

using NUlid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Application.Helper
{
    public class UnityHelper
    {

        public static string GenerateUlid()
        {
            return Ulid.NewUlid().ToString();
        }
    }
}

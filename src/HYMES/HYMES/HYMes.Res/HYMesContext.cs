using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer;

namespace HYMes.Res
{
    class ShopContext 
    {
        /// <summary>
        /// ��ǰ��¼�˺š�
        /// </summary>
        public static IAccount Account
        {
            get
            {
                return EAS.Application.Instance.Session.Client as IAccount;
            }
        }
    }
}

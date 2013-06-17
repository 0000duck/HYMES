using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer.Entities;
using EAS.Services;
using EAS.Explorer;
using System.Linq;

namespace HYMes.Res
{
    class NavigationProxy
    {
        /// <summary>
        /// ������Ϣ��
        /// </summary>
        public IList<INavigateGroup> GroupList
        {
            get;
            set;
        }

        /// <summary>
        /// ģ����Ϣ��
        /// </summary>
        public IList<INavigateModule> ModuleList
        {
            get;
            set;
        }

        /// <summary>
        /// ��һ�����顣
        /// </summary>
        /// <returns></returns>
        public IList<INavigateGroup> GetGroupList()
        {
            Dictionary<string, int> gms = new Dictionary<string, int>();
            foreach (INavigateGroup var in this.GroupList)
                gms.Add(var.ID, 0);

            foreach (INavigateGroup var in this.ModuleList)
            {
                gms[var.ID] += 1;
            }

            IList<INavigateGroup> List = new List<INavigateGroup>();
            foreach (NavigateGroup var in this.GroupList)
            {
                if (gms[var.ID] > 0)
                    List.Add(var);
            }

            return List;
        }

        /// <summary>
        /// �����ط��顣
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IList<INavigateGroup> GetGroupList(string parentID)
        {
            //return this.GroupList.Where(p => p.ParentID == parentID).ToList();
            return this.GroupList.Where(p => p.ParentID == parentID).Distinct().ToList();
        }

        /// <summary>
        /// ��ѯ����ģ�顣
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public IList<INavigateModule> GeModuleList(string groupID)
        {
            return this.ModuleList.Where(p => p.GroupID == groupID).ToList();
        }
    }
}

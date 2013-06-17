using System;
using EAS.Explorer;

namespace HYMes.Res
{
	public class Resources:EAS.Explorer.IResource  
	{
        /// <summary>
        /// ������ͼ�ꡣ
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Icon GetMainIcon()
        {
            return Properties.Resources.Drugbasket;
           
        }

        /// <summary>
        /// Tabҳͼ��
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image GetModuleIcon()
        {
            return Properties.Resources.Module.ToBitmap();
        }

        /// <summary>
        /// ���汳����
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image GetDesktopImage()
        {
            return null;
        }

        /// <summary>
        /// Ӧ�ó������ơ�
        /// </summary>
        /// <returns></returns>
        public string GetApplicationName()
        {
            return "����ִ��ϵͳ";
        }

        /// <summary>
        /// Ӧ�ó�����⡣
        /// </summary>
        /// <returns></returns>
        public string GetApplicationTitle()
        {
            return "HY Manufacturing Execution System";
        }

        public object GetMainShell()
        {
            return null;
        }

        /// <summary>
        /// ���ڴ��ڡ�
        /// </summary>
        /// <returns></returns>
        public object GetAboutForm()
        {
            return new AboutForm();
        }

        /// <summary>
        /// Bottom֪ͨ��
        /// </summary>
        /// <returns></returns>
        public object GetBottomControl()
        {
            return new BottomControl();
        }

        /// <summary>
        /// Top������
        /// </summary>
        /// <returns></returns>
        public object GetBannerControl()
        {
            return new Banner();
           // return null;
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        public object GetNavigationControl()
        {
            return new NavigationControl();
        }

        /// <summary>
        /// ��¼���ڡ�
        /// </summary>
        /// <returns></returns>
        public ILoginForm GetLoginForm()
        {  

            
            return new LoginForm();

           // return new Login();
        }

        /// <summary>
        /// ��ʼҳ��
        /// </summary>
        /// <returns></returns>
        public object GetStartModule()
        {
            return new Welcome();
        }

        /// <summary>
        /// ��ʾ�˵���
        /// </summary>
        public bool DisplayMainMenu
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// ��ʾ������������
        /// </summary>
        public bool DisplayNavigationTool
        {
            get
            {
                return false;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAS.Windows.UI.Controls;
using HYMes.Tools;
using HYMes.WinUI.BaseForm;
namespace HYMes.WinUI.BaseControl
{
    public partial class UcBRM : UserControl
    {
        public UcBRM()
        {
            InitializeComponent();
        }
        #region 基本属性定义
        /// <summary>
        /// DataGridView的分页时候，每页显示的记录数
        /// </summary>
        int pageSize = 20;     //每页显示行数   可以在控件的属性中进行设定

        /// <summary>
        /// DataGridView的分页时候，每页显示的记录数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        int nMax = 0;
        /// <summary>
        /// 页数＝总记录数/每页显示行数
        /// </summary>
        int pageCount = 0;
        /// <summary>
        /// 当前页码
        /// </summary>
        int pageCurrent = 0;
        /// <summary>
        /// 当前记录行 
        /// </summary>
        int nCurrent = 0;
        ///// <summary>
        ///// 获取全部数据，主要用来导出和分页使用
        ///// </summary>
        ////DataTable ddt = new DataTable();  //记录当前GRID的全部数据
        /// <summary>
        /// 用来存放分页的数据
        /// </summary>
        DataTable pagingData = new DataTable();
        /// <summary>
        /// 存放datagridview的数据集对象,非分页信息
        /// </summary>
        private DataTable allGridData = new DataTable();
        /// <summary>
        /// datagridview的数据集信息
        /// </summary>
        public DataTable AllGridData
        {
            get { return allGridData; }
            set
            {
                allGridData = value;
                //EventHandlerDg();
                // ddt = this.allGridData;
                InitDataSet();
            }
        }


        private DataGridView sourceDataGridView = new DataGridView();
        /// <summary>
        /// sourceDataGridView,需要操作的datagridview对象,分页与增删改共用的datagridview
        /// </summary>
        public DataGridView SourceDataGridView
        {
            get { return sourceDataGridView; }
            set
            {
                sourceDataGridView = value;
                if (sourceDataGridView.DataSource != null)
                {  

                    AllGridData = (DataTable)sourceDataGridView.DataSource; ///同时进行数据记录集的赋值
                }

            }
        }
        ///// <summary>
        ///// DataGridView控件实例
        ///// </summary>
        //public DataGridView DgvInstanceName
        //{
        //    get { return dgvInstanceName; }
        //    set { dgvInstanceName = value;
        //    if (dgvInstanceName.DataSource != null)
        //    {
        //        DtGridData = (DataTable)dgvInstanceName.DataSource; ///同时进行数据记录集的赋值
        //    }
        //    }
        //}

        #endregion
        #region 初始化控件信息，算出及获取每页显示的记录数及总页数、当前页数等信息
        /// <summary>
        /// 初始化控件信息，算出及获取每页显示的记录数及总页数、当前页数等信息
        /// </summary>
        public void InitDataSet()
        {
            if (sourceDataGridView == null || sourceDataGridView.Rows.Count == 0)
            {
                return;
            }
            this.tstOnePageShowCount.Text = this.PageSize.ToString();//将目前设置的单页显示的记录数显示在文本框中
            pageSize = PageSize;      //设置页面行数,可以设置
            nMax = sourceDataGridView.Rows.Count; //总记录数

            pageCount = (nMax / pageSize);    //计算出总页数

            if ((nMax % pageSize) > 0) pageCount++;
            ///当前页数从1开始
            pageCurrent = 1;
            //当前记录数从0开始
            nCurrent = 0;
            lblsumPageCount.Text = pageCount.ToString();//总页数
            txtTotalRowCount.Text = AllGridData.Rows.Count.ToString(); //总记录数
            LoadData();
        }
        #endregion
        #region 根据页码显示出当前页的记录数
        /// <summary>
        /// 根据页码显示出当前页的记录数
        /// </summary>
        public void LoadData()
        {
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行
            pagingData = AllGridData.Clone();   //克隆DataTable结构框架
            if (pageCurrent == pageCount)
                nEndPos = nMax;
            else
                nEndPos = pageSize * pageCurrent;

            nStartPos = nCurrent;

            // lblPageCount.Text = pageCount.ToString();
            txtCurrPageCnt.Text = Convert.ToString(pageCurrent);

            //从元数据源复制记录行
            for (int i = nStartPos; i < nEndPos; i++)
            {
                pagingData.ImportRow(AllGridData.Rows[i]);
                nCurrent++;
            }
            //bdsInfo.DataSource = dtTemp;
            //bdnInfo.BindingSource = bdsInfo;
            //dataGridViewExt1.DataSource = bdsInfo;
            // dgvE.DataSource = dtTemp;
            sourceDataGridView.DataSource = pagingData;
        }
        #endregion
        #region toolstrip的单击事件
        /// <summary>
        /// toolstrip的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "上一页")
            {
                pageCurrent--;
                if (pageCurrent <= 0)
                {
                    pageCurrent++;//add by cs 20120712解决此情况下单击'上一页'时候还是显示当前页。
                    MessageBox.Show("已经是第一页，请点击“下一页”查看！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    nCurrent = pageSize * (pageCurrent - 1);
                }

                LoadData();
            }
            if (e.ClickedItem.Text == "下一页")  //移至首页
            {
                pageCurrent++;
                if (pageCurrent > pageCount)
                {
                    pageCurrent--;//add by cs 20120712解决此情况下单击'下一页'时候还是显示当前页。
                    MessageBox.Show("已经是最后一页，请点击“上一页”查看！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    nCurrent = pageSize * (pageCurrent - 1);
                }
                LoadData();
            }
            if (e.ClickedItem.Text == "移至末页")  //
            {
                pageCurrent++;
                if (pageCurrent > pageCount)
                {
                    pageCurrent--;//add by cs 20120712解决此情况下单击'下一页'时候还是显示当前页。
                    MessageBox.Show("已经是末页!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    nCurrent = pageSize * (pageCount - 1);
                    pageCurrent = pageCount;//末页
                    //this.txtCurrPageCnt.Text = pageCount.ToString();

                }
                LoadData();
            }
            if (e.ClickedItem.Text == "移至首页")  //
            {
                pageCurrent--;
                if (pageCurrent <= 0)
                {
                    pageCurrent++;//add by cs 20120712解决此情况下单击'上一页'时候还是显示当前页。
                    MessageBox.Show("已经是首页!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //nCurrent = pageSize * (pageCurrent - 1);
                    nCurrent = 0;
                    pageCurrent = 1;
                    //this.txtCurrPageCnt.Text = "1";
                }
                LoadData();
            } //移至末页
            if (e.ClickedItem.Name == "tstExport") //资料汇出功能Excel
            {

                if (sourceDataGridView.Rows.Count == 0 || sourceDataGridView == null)
                {
                    MessageBox.Show("无资料可汇出", "警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                SaveFileDialog save = new SaveFileDialog();
                // save.Filter = "excel文件(*.xlsx)|*.xlsx"; //OFFICE 2007
                save.Filter = "excel文件(*.xls)|*.xls";//OFFICE 2003
                string fileNamePath = "";
                //  DialogResult re = save.ShowDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    fileNamePath = save.FileName;
                    if (string.IsNullOrEmpty(fileNamePath))
                    {
                        fileNamePath = @"C:\Temp\demos.xls";
                    }
                    DataTable dt = (DataTable)this.sourceDataGridView.DataSource;
                    //ds.Tables.Add(dt.Copy());
                    //  Cs.Sys.Tools.ExportToExcelProvider.ExportToExcel(dt, fileNamePath); //导出当前页面的数据
                    HYMes.Tools.ExportToExcelProvider.ExportToExcel(AllGridData, fileNamePath); //导出所有页面的数据
                    MessageBox.Show("导出成功");
                    this.Cursor = Cursors.Default;
                }
                //else if (save.ShowDialog() == DialogResult.Cancel)
                else
                {
                    return;
                }

            }
        }

        #endregion
        private void bdnInfo_RefreshItems(object sender, EventArgs e)
        {

        }

        #region 输入每页显示记录集数的确认事件
        private void tstOnePageShowCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (AllGridData.Rows.Count == 0)
                {
                    MessageBox.Show("当前记录记录数位0!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.tstOnePageShowCount.Text.Trim().Length == 0)
                {
                    this.tstOnePageShowCount.Text = this.PageSize.ToString();
                }
                else
                {
                    this.PageSize = Convert.ToInt32(this.tstOnePageShowCount.Text);
                    this.sourceDataGridView.DataSource = AllGridData; //初始化Grid中的数据记录
                    this.InitDataSet();
                }

            }
        }
        #endregion



        private void txtCurrPageCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                try
                {
                    pageCurrent = Convert.ToInt16(txtCurrPageCnt.Text);
                }
                catch
                {
                    MessageBox.Show("请输入正确的页码!\n页码必须为整数", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    this.txtCurrPageCnt.Focus();
                    return;
                }
                if (pageCurrent > pageCount)
                {

                    MessageBox.Show("输入的页码超出最大页码数,\n请重新输入!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    this.txtCurrPageCnt.Focus();
                    return;

                }
                if (pageCurrent < 1)
                {

                    MessageBox.Show("输入的页码不能小于1!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    this.txtCurrPageCnt.Focus();
                    return;

                }
                nCurrent = pageSize * (pageCurrent - 1);

                LoadData();
            }
        }

        //功能按钮事件
        /// <summary>
        /// 单击编辑、添加、修改按钮时弹出的子窗体
        /// </summary>
        // private FrmBaseEdit frmEdit;
        private FrmBaseEdit frmEdit;
        /// <summary>
        /// 按钮类型
        /// </summary>
        private Bparmaters.FormMode currentFormMode;

        /// <summary>
        /// 
        /// </summary>
        private DataSet dsMaster;

        /// <summary>
        /// 
        /// </summary>
        public DataSet DsMaster
        {
            get { return dsMaster; }
            set { dsMaster = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Bparmaters.FormMode CurrentFormMode
        {
            get { return currentFormMode; }
            set { currentFormMode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public FrmBaseEdit FrmEdit
        {
            get { return frmEdit; }
            set
            {
                frmEdit = value;
            }
        }
        //private DataGridView dgvInstance;
        ///// <summary>
        ///// 
        ///// </summary>
        //public DataGridView DgvInstance
        //{
        //    get { return dgvInstance; }
        //    set { dgvInstance = value; }
        //}
        /// <summary>
        /// 刷新按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void AfterClickRefreshEventHandler(object sender, EventArgs e);
       // public delegate void AfterClickAddEventHandler(object sender, EventArgs e);
        //public delegate void AfterClickModifyEventHandler(object sender, EventArgs e);
        //public delegate void AfterLoadEventHandler(object sender, EventArgs e);
        //public delegate void BeforUpdateDataEventHandler(object sender, EventArgs e);
        public delegate void AfterClickDeleteEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 刷新事件
        /// </summary>
        public event AfterClickRefreshEventHandler RefreshEventHandler;
        public event AfterClickDeleteEventHandler DeleteEventHandler;
       // public event AfterClickAddEventHandler AddEventHandler;
       // public event AfterClickModifyEventHandler ModifyEventHandler;
        //public event AfterLoadEventHandler AfterLoadEvent;
        //public event BeforUpdateDataEventHandler BeforUpdateEvent;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //DataTable dt = ((DataTable)this.dgvInstance.DataSource).Clone() ;
            //dt.Rows.Add(this.DgvInstance.CurrentRow as DataRow);
            this.CurrentFormMode = Bparmaters.FormMode.AddNew;

            // DsMaster.Tables[0].Rows.Add(this.DgvInstance.SelectedRows);
            if (FrmEdit != null)
            {
                FrmEdit.EditFormEventArgs.CurrentFormMode = Bparmaters.FormMode.AddNew;
                //   FrmEdit.EditFormEventArgs.DgvRow = this.DgvInstance.CurrentRow;
                //  //FrmEdit.EditFormEventArgs.DsMaster.Tables.Add();
                ////FrmEdit.EditFormEventArgs.DsMaster = this.DgvInstance.CurrentRow;
                // //  FrmEdit.EditFormEventArgs.DsMaster = DsMaster;
                FrmEdit.ShowDialog();
                btnRefresh_Click(sender, e);//在退出编辑窗体后，刷新界面

            }
            else
            {
                MessageBox.Show("没有指定编辑界面" + this.CurrentFormMode);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            this.CurrentFormMode = Bparmaters.FormMode.Modify;
            if (this.SourceDataGridView.CurrentRow == null)
            {
                MessageBox.Show("没有资料可供编辑");
                return;
            }

            if (FrmEdit != null)
            {
                FrmEdit.EditFormEventArgs.CurrentFormMode = Bparmaters.FormMode.Modify;
                FrmEdit.EditFormEventArgs.SelectedRow = this.SourceDataGridView.CurrentRow;
                FrmEdit.ShowDialog();
                btnRefresh_Click(sender, e);//在退出编辑窗体后，刷新界面
            }
            else
            {
                MessageBox.Show("没有指定编辑界面" + this.CurrentFormMode);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (RefreshEventHandler != null)
            {
                RefreshEventHandler(sender, new EventArgs());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.CurrentFormMode = Bparmaters.FormMode.delete;
            if (this.SourceDataGridView.CurrentRow == null)
            {
                MessageBox.Show("没有资料可供删除");
                return;
            }
            if (DeleteEventHandler != null)
            {
                DeleteEventHandler(sender, e);
            }
            else
            {
                MessageBox.Show("没有定义删除事件，请确认!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (RefreshEventHandler != null)
            {
                btnRefresh_Click(sender, e); //在退出编辑窗体后，刷新界面
            }
           
        }

    }
}

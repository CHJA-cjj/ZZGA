using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HighResolutionApps.Interfaces;
using HrvVisualControlStyles;
using VisualContorlBase;
using System.Windows.Threading;
using System.Data;
using Visifire.Charts;

namespace HighResolutionApps.VisualControls.HRVZZCityMapControl_ZZGA
{
    /// <summary>
    /// HRVZZCityMapControl_ZZGA.xaml 的交互逻辑
    /// </summary>
    public partial class HRVZZCityMapControl_ZZGA : UserControl, IVisualControl, IPageEvent
    {


        DispatcherTimer timer;
        int sum = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HRVZZCityMapControl_ZZGA()
        {
            InitializeComponent();
            // 必要 样式使用
            StyleManager.GetResourceDictionnary(this, m_WindowStyleName);
            // 区分当前运行环境
            if (CurrentRunderUnitEnvironment.Mode == EnvironmentMode.Designer)
            {
                // 运行环境是设计端
                // 界面渲染  在设计端调用  在其他运行环境无需调用
                LoadContent();
            }

            //定义地图高中低的三种色值
            LowNum = 20;
            MiddleNum = 40;
            HighNum = 60;
        }

        #region 自定义方法

        #region 定时器
        private void TimerCall(object sender, EventArgs e)
        {
            SystemHelper.logger.LogDebug("TimerCall  sender" + sender.ToString());
            try
            {
                if (sum == 5)
                {
                    sum = 0;
                }

                // showChart(sum);
                ShowWindow(m_InputDataTable, sum);
                SystemHelper.logger.LogDebug("TimerCall  currentRegion=" + sum.ToString());

                ShowPie(m_InputDataTable, sum);
                SystemHelper.logger.LogDebug("TimerCall  currentRegion=" + sum.ToString());

                ShowMap(m_InputDataTable, sum);

                sum++;
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region  封装的方法
        private void showChart(int currentRegion)
        {
            SystemHelper.logger.LogDebug("showChart  currentRegion=" + currentRegion.ToString());

            ShowMap(m_InputDataTable, currentRegion);


            SystemHelper.logger.LogDebug("ShowMap  currentRegion=" + currentRegion.ToString());

            ShowWindow(m_InputDataTable, currentRegion);
            SystemHelper.logger.LogDebug("ShowWindow  currentRegion=" + currentRegion.ToString());

            ShowPie(m_InputDataTable, currentRegion);
            SystemHelper.logger.LogDebug("ShowPie  currentRegion=" + currentRegion.ToString());

        }
        #endregion

        #region 地图

        public void ShowMap(DataTable dt, int sum)
        {
            SystemHelper.logger.LogDebug("ShowMap  dt=" + dt.ToString());
            try
            {
                if (dt == null || dt.Rows.Count < 1)
                {
                    //dt = CreateChartData();
                    return;
                }
                var color = (Color)ColorConverter.ConvertFromString("#596CFFE6");//普通状态点击颜色 
                for (int i = 0; i < 5; i++)
                {

                    int dataRow = getDataRowByRegionID(i, dt);

                    var total = Convert.ToDouble(dt.Rows[dataRow][0].ToString());

                    var linearGradientBrush = new LinearGradientBrush();
                    GradientStop stop = new GradientStop();
                    if (sum == (i))
                    {
                        if (total >= LowNum && total <= MiddleNum)
                        {
                            color = (Color)ColorConverter.ConvertFromString("#4CFFFA5A");//低等状态颜色
                        }
                        if (total >= MiddleNum && total <= HighNum)
                        {
                            color = (Color)ColorConverter.ConvertFromString("#59FF934A");//中等状态颜色
                        }
                        if (total > HighNum)
                        {
                            color = (Color)ColorConverter.ConvertFromString("#59FC4C4C");//高等状态颜色
                        }
                        stop.Color = color;
                        linearGradientBrush.GradientStops.Add(stop);

                        ((Path)CityMap.Children[i]).Fill = linearGradientBrush;
                        SystemHelper.logger.LogDebug("ShowMap  linearGradientBrush=" + linearGradientBrush.ToString());
                    }
                    else
                    {
                        linearGradientBrush = GetColor(Convert.ToDouble(total));//原本颜色
                        ((Path)CityMap.Children[i]).Fill = linearGradientBrush;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region 地图 颜色渐变
        private LinearGradientBrush GetColor(double value)
        {
            SystemHelper.logger.LogDebug("LinearGradientBrush  value=" + sum.ToString());
            var linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0.5, 0);
            linearGradientBrush.EndPoint = new Point(0.5, 1);
            linearGradientBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;
            GradientStop stop;
            stop = new GradientStop();
            stop.Offset = 0;
            stop.Color = (Color)ColorConverter.ConvertFromString("#00000000");//普通状态颜色
            GradientStop stop1 = new GradientStop();
            stop1.Offset = 1;
            stop1.Color = (Color)ColorConverter.ConvertFromString("#7C38D6E0");
            if (value >= LowNum && value <= MiddleNum)//低级到中级
            {
                stop1.Color = (Color)ColorConverter.ConvertFromString("#7CEBE85E");
            }
            if (value >= MiddleNum && value <= HighNum)//中级到高级
            {
                stop1.Color = (Color)ColorConverter.ConvertFromString("#7CFA8F52");
            }
            if (value > HighNum)//高级
            {
                stop1.Color = (Color)ColorConverter.ConvertFromString("#7CF03B30");
            }
            linearGradientBrush.GradientStops.Add(stop);
            linearGradientBrush.GradientStops.Add(stop1);
            return linearGradientBrush;
        }
        #endregion

        #region 显示文本框
        public void ShowWindow(DataTable dt, int sum)
        {
            SystemHelper.logger.LogDebug("ShowWindow  dt=" + dt.ToString());
            SystemHelper.logger.LogDebug("ShowWindow  sum=" + sum.ToString());
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    //dt = CreateChartData();
                    return;
                }

                for (int i = 0; i < 5; i++)
                {
                    ((Grid)GroupRoot.Children[i]).Visibility = (sum == i ? Visibility.Visible : Visibility.Hidden);

                    int dataRow = getDataRowByRegionID(sum, dt);

                    ((TextBlock)((Grid)GroupRoot.Children[i]).FindName("PoliceCase" + (sum))).Text = dt.Rows[dataRow][0].ToString();
                    ((TextBlock)((Grid)GroupRoot.Children[i]).FindName("Persons" + (sum))).Text = dt.Rows[dataRow][1].ToString();
                    ((TextBlock)((Grid)GroupRoot.Children[i]).FindName("PolicePower" + (sum))).Text = dt.Rows[dataRow][2].ToString();
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion
        public int getDataRowByRegionID(int RegionID, DataTable dt)
        {


            string[] Regions = { "株洲县", "醴陵市", "攸县", "茶陵县", "炎陵县" };
            //string Region = ((Path)CityMap.Children[RegionID]).DataContext.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string b = Regions[RegionID].ToString();
                string a = dt.Rows[i][5].ToString();
                if (b == a)
                {
                    return i;
                }
            }
            return 0;
        }

        #region  显示饼图
        public void ShowPie(DataTable dt, int currentRegion)
        {
            SystemHelper.logger.LogDebug("ShowPie  dt=" + dt.ToString());
            try
            {
                if (dt == null || dt.Rows.Count < 1)
                {
                    //dt = CreateChartData();
                    return;
                }

                int dataRow = getDataRowByRegionID(currentRegion, dt);

                var datarow = dt.Rows[dataRow];
                DataSeries ds = new DataSeries();
                ds.RenderAs = RenderAs.Doughnut;


                DataPoint dp = new DataPoint();
                dp.ShowInLegend = false;
                dp.LabelStyle = LabelStyles.Inside;
                dp.LightingEnabled = false;

                dp.LabelEnabled = false; ;
                dp.YValue = 100;
                dp.Color = Brushes.Blue;
                ds.DataPoints.Add(dp);

                chart.Series.Add(ds);

                for (int i = 3; i < dt.Columns.Count; i++)
                {
                    SystemHelper.logger.LogDebug("ShowPie  i" + i.ToString());
                    dp = new DataPoint();
                    dp.ShowInLegend = false;
                    dp.LabelStyle = LabelStyles.Inside;
                    dp.LightingEnabled = false;
                    dp.YValue = Convert.ToDouble(datarow[i].ToString());

                    dp.Color = Brushes.Green;
                    if (i == 3)
                    {
                        dp.Color = Brushes.Red;
                    }

                    SystemHelper.logger.LogDebug("ShowPie  YValue" + dp.YValue.ToString());
                    dp.LabelText = datarow[i].ToString();
                    ds.DataPoints.Add(dp);
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region 鼠标移上事件

        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            SystemHelper.logger.LogDebug("Path_MouseEnter  sender" + sender.ToString());
            try
            {
                var path = (Path)sender;
                sum = Convert.ToInt32(path.Name.Remove(0, 4)) - 1;
                TimerCall(sender, e);
                timer.Tick -= TimerCall;
                timer.Stop();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region 鼠标移出事件

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            SystemHelper.logger.LogDebug("Path_MouseLeave  sender" + sender.ToString());
            try
            {
                timer.Tick += TimerCall;
                timer.Start();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        public DataTable CreateChartData()
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("PoliceCase", typeof(string));
                DataColumn dc1 = new DataColumn("PeopleCount", typeof(string));
                DataColumn dc2 = new DataColumn("PolicePower", typeof(string));
                DataColumn dc3 = new DataColumn("Translation", typeof(string));
                DataColumn dc4 = new DataColumn("Criminal", typeof(string));
                DataColumn dc5 = new DataColumn("Criminals", typeof(string));

                dt.Columns.Add(dc);
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);

                dt.Rows.Add("10", "30", "60", "23", "11", "攸县");
                dt.Rows.Add("30", "30", "60", "34", "22", "茶陵县");
                dt.Rows.Add("50", "30", "60", "54", "33", "炎陵县");
                dt.Rows.Add("90", "30", "60", "26", "44", "醴陵市");
                dt.Rows.Add("10", "30", "60", "23", "11", "株洲县");
                return dt;
                //LowNum = 20;
                //MiddleNum = 40;
                //HighNum = 60;
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
                return null;
            }
        }
        public void InputDataTable2Map(int currentRegion)
        {
            SystemHelper.logger.LogDebug("showChart  currentRegion=" + currentRegion.ToString());

            ShowMap(m_InputDataTable, currentRegion);


            SystemHelper.logger.LogDebug("ShowMap  currentRegion=" + currentRegion.ToString());

            ShowWindow(m_InputDataTable, currentRegion);
            SystemHelper.logger.LogDebug("ShowWindow  currentRegion=" + currentRegion.ToString());

            ShowPie(m_InputDataTable, currentRegion);
            SystemHelper.logger.LogDebug("ShowPie  currentRegion=" + currentRegion.ToString());

            //showChart(region);
        }

        #endregion

        #region 定义外接属性

        #region 高
        public static readonly DependencyProperty HighNumProperty =
            DependencyProperty.Register("HighNum", typeof(int), typeof(HRVZZCityMapControl_ZZGA), new FrameworkPropertyMetadata(60, OnInputDataTableChange));
        public int HighNum
        {
            get { return (int)GetValue(HighNumProperty); }
            set { SetValue(HighNumProperty, value); }
        }
        #endregion

        #region 中
        public static readonly DependencyProperty MiddleNumProperty =
           DependencyProperty.Register("MiddleNum", typeof(int), typeof(HRVZZCityMapControl_ZZGA), new FrameworkPropertyMetadata(40, OnInputDataTableChange));
        public int MiddleNum
        {
            get { return (int)GetValue(MiddleNumProperty); }
            set { SetValue(MiddleNumProperty, value); }
        }
        #endregion

        #region 低
        public static readonly DependencyProperty LowNumProperty =
            DependencyProperty.Register("LowNum", typeof(int), typeof(HRVZZCityMapControl_ZZGA), new FrameworkPropertyMetadata(20, OnInputDataTableChange));
        public int LowNum
        {
            get { return (int)GetValue(LowNumProperty); }
            set { SetValue(LowNumProperty, value); }
        }





        #endregion



        #region 绑定数据
        private DataTable m_InputDataTable = new DataTable();

        public DataTable InputDataTable
        {
            get { return (DataTable)GetValue(InputDataTableProperty); }
            set { SetValue(InputDataTableProperty, value); }
        }

        /// <summary>
        /// datatable
        /// </summary>
        public static readonly DependencyProperty InputDataTableProperty = DependencyProperty.Register(
            "InputDataTable", typeof(DataTable), typeof(HRVZZCityMapControl_ZZGA), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnInputDataTableChange)));


        /// <summary>
        /// 改变输入的datatable
        /// </summary> 
        private static void OnInputDataTableChange(DependencyObject o, DependencyPropertyChangedEventArgs ea)
        {
            try
            {
                //绑定数据
                HRVZZCityMapControl_ZZGA gridControl = o as HRVZZCityMapControl_ZZGA;
                gridControl.ReRenderData();

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        void ReRenderData()
        {
            try
            {
                //调用绘制画面方法

                m_InputDataTable = InputDataTable;
                InputDataTable2Map(sum);
                timer = new DispatcherTimer();

                sum = 0;
                timer.Tick += TimerCall;
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.Stop();
                timer.Start();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        #endregion

        #endregion

        #region 接口IVisualControl成员

        private Label m_lb_ControlInstanceMainTitle;
        private Label m_lb_ControlInstanceSubTitle;
        public override void OnApplyTemplate()
        {
            try
            {
                base.OnApplyTemplate();
                m_lb_ControlInstanceMainTitle = GetTemplateChild("lb_ControlInstanceMainTitle") as Label;
                if (m_lb_ControlInstanceMainTitle != null)
                {
                    m_lb_ControlInstanceMainTitle.Content = m_ControlInstanceMainTitle;
                }
                m_lb_ControlInstanceSubTitle = GetTemplateChild("lb_ControlInstanceSubTitle") as Label;
                if (m_lb_ControlInstanceSubTitle != null)
                {
                    m_lb_ControlInstanceSubTitle.Content = m_ControlInstanceSubTitle;
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }

        }

        /// <summary>
        /// 重要
        /// </summary>
        /// <returns></returns>
        public override bool ShouldSerializeContent()
        {
            return false;
        }


        /// <summary>
        /// 组件背景窗口名称
        /// </summary>
        private string m_WindowStyleName;
        public string WindowStyleName
        {
            get { return m_WindowStyleName; }
            set { m_WindowStyleName = value; StyleManager.SetStyle(this, m_WindowStyleName); }
        }

        /// <summary>
        /// 加载组件中的显示内容 加载成功 isContentLoaded = true;
        /// </summary>
        /// <returns>加载完成并无错误返回true,有错误返回false</returns>
        public bool LoadContent()
        {
            try
            {
                if (CurrentRunderUnitEnvironment.Mode == EnvironmentMode.Designer)
                {

                    // InputDataTable = CreateChartData();

                    m_InputDataTable = CreateChartData();
                    InputDataTable2Map(0);

                    timer = new DispatcherTimer();
                    sum = 0;
                    timer.Tick += TimerCall;
                    timer.Interval = TimeSpan.FromSeconds(10);
                    timer.Stop();
                    timer.Start();

                }

                // 渲染成功后
                isContentLoaded = true;
                return true;

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                              System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
                isContentLoaded = false;
                return false;
            }
        }




        /// <summary>
        /// 标识此组件的内容是否已加载，即LoadContent()方法已成功调用
        /// </summary>
        private bool m_isContentLoaded = false;

        public bool isContentLoaded
        {
            get { return m_isContentLoaded; }
            private set { m_isContentLoaded = value; }
        }


        /// <summary>
        /// 加载顺序标识，数字越大越后加载，建议默认值为0，
        /// *此属性应注册为可在设计端修改的属性
        /// </summary>
        private int m_renderOrder = 0;
        public int RenderOrder
        {
            get { return m_renderOrder; }
            set { m_renderOrder = value; }
        }


        /// <summary>
        /// 本可视化组件类型的唯一标识
        /// 需要修改 guid
        /// </summary>
        public string ControlGUID
        {
            get { return "B524698A-C269-4faa-AE66-BF9193F40791"; }
        }


        /// <summary>
        /// 本可视化组件类型名称
        /// 需要修改 组件名称
        /// </summary>
        public string ControlName
        {
            get { return "株洲市区地图"; }
        }


        /// <summary>
        /// 实例名称即窗口默认主标题
        /// *此属性应注册为可在设计端修改的属性
        /// 此属性默认值建议与ControlName相同
        /// 在设计过程中允许设计人员给此实例命名，便于人工查找区分
        /// 在可视化组件选取列表中将展示此名称
        /// 如：组件名称为：表格组件，实例名称为：2015年度北京市人口统计表
        /// 提示：组件名称可由组件开发者动态生成，如上个例子中，可将dataTable/dataView的名称作为此项为空时的默认名称
        /// 需要修改 默认主标题名称
        /// </summary>
        private string m_ControlInstanceMainTitle = "默认主标题名称";
        public string ControlInstanceMainTitle
        {
            get { return m_ControlInstanceMainTitle; }
            set
            {
                m_ControlInstanceMainTitle = value;
                if (m_lb_ControlInstanceMainTitle != null)
                {
                    m_lb_ControlInstanceMainTitle.Content = m_ControlInstanceMainTitle;
                }
            }
        }
        /// <summary>
        /// 实例名称即窗口默认副标题
        /// *此属性应注册为可在设计端修改的属性
        /// 在设计过程中允许设计人员给此实例命名，便于人工查找区分
        /// 在可视化组件选取列表中将展示此名称
        /// 如：组件名称为：表格组件，实例名称为：2015年度北京市人口统计表
        /// 提示：组件名称可由组件开发者动态生成，如上个例子中，可将dataTable/dataView的名称作为此项为空时的默认名称
        /// 需要修改 默认副标题名称
        /// </summary>
        private string m_ControlInstanceSubTitle = "默认副标题名称";
        public string ControlInstanceSubTitle
        {
            get { return m_ControlInstanceSubTitle; }
            set
            {
                m_ControlInstanceSubTitle = value;
                if (m_lb_ControlInstanceSubTitle != null)
                {
                    m_lb_ControlInstanceSubTitle.Content = m_ControlInstanceSubTitle;
                }
            }
        }

        /// <summary>
        /// 组件实例描述信息
        /// 用于设计人员描述组件所呈现的相关业务信息
        /// 通过此信息，交互控制人员可分辨组件内显示的业务内容
        /// 需要修改 组件示例
        /// </summary>
        private string m_ControlInstanceDes = "组件示例";
        public string ControlInstanceDes
        {
            get { return m_ControlInstanceDes; }
            set { m_ControlInstanceDes = value; }
        }



        /// <summary>
        /// 可视化组件的描述信息
        /// 包括呈现说明，应用描述
        /// 需要修改  组件描述
        /// </summary>
        public string ControlDescription
        {
            get { return "组件描述"; }
        }


        /// <summary>
        /// 可视化组件配置应用说明
        /// 此属性在页面设计人员引用此组件时提示时使用，告诉设计人员如何配置数据源之用
        /// 必须配置哪几项，属性间相互关系说明等
        /// 需要修改  配置说明
        /// </summary>
        public string ControlUseDescription
        {
            get { return "配置说明"; }
        }

        /// <summary>
        /// 可视化组件版本信息
        /// 需要修改  组件版本
        /// </summary>
        public Version Version { get { return new Version("1.0.0.1"); } }


        /// <summary>
        /// 依赖的可视化开发、运行环境版本号
        /// 开发时使用的开发、运行环境版本号        
        /// </summary>
        public Version HRVRuntimeVersion
        {
            get { return new Version("2.0.0.0"); }
        }


        /// <summary>
        /// 可视化组件所有权公司
        /// </summary>
        public string CompanyName
        {
            get { return "北京威创视讯信息系统有限公司"; }
        }



        /// <summary>
        /// 依赖动态库列表属性（高分平台dll,.net framework系统dll不用描述）
        /// 要求格式：版本号=动态库名称
        /// 样例    ：1.0.0.1=AForge.Imaging.Formats.dll
        /// </summary>
        public string[] DependentDllList
        {
            get
            {
                return new string[] { "" };
            }
        }


        /// <summary>
        /// 依赖文件资源列表属性
        /// 要求资源放置位置为启动程序(可视化组件dll)目录为起点的
        /// ../Files_Resource/xxxx.dll/文件夹内，xxxx为本可视化组件所在的dll名称
        /// 资源文件夹内可创建子文件夹
        /// </summary
        public string[] DependentResourceFileList
        {
            get { return null; }
        }

        /// <summary>
        /// 依赖字体名称列表
        /// </summary>
        public string[] DependentFontList
        {
            get
            {
                string[] str = new string[1];
                str[0] = "宋体";
                return str;
            }
        }


        /// <summary>
        /// 获取复合数据源样例数据
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public System.Data.DataTable getDataTableSample(string propertyName)
        {
            return null;
        }


        /// <summary>
        /// 通用控制触发窗口调用方法
        /// 通过此方法，可给最终用户提供对本组件的细节控制交互界面 预留
        /// 注意：如果需要与大屏幕上的渲染形成互动，则配置窗口改变的属性应都为同步属性
        /// </summary>
        public void ShowConfigurationWindow()
        {

        }

        /// <summary>
        /// 设置网络授权的ip地址
        /// </summary>
        /// <param name="ip">网络授权的IP地址</param>
        bool IVisualControl.setNetLicenseIP(string ip)
        {
            return false;
        }

        /// <summary>
        /// 独立线程、进程加载及运行标识属性
        ///  *此属性应注册为可在设计端修改的属性
        /// 一般组件请设置InMainProcess为默认值
        /// StandaloneThread：独立线程运行。StandaloneProcess：独立进程运行
        /// </summary>
        EnumRenderMethodType m_RenderMethod = EnumRenderMethodType.InMainProcess;
        public EnumRenderMethodType RenderMethod
        {
            get
            {
                return m_RenderMethod;
            }
            set
            {
                m_RenderMethod = value;
            }
        }

        /// <summary>
        /// 内存要求
        /// </summary>
        int m_MemoryRequest = 0;
        public int MemoryRequest
        {
            get
            {
                return m_MemoryRequest;
            }
            set
            {
                m_MemoryRequest = value;
            }
        }

        /// <summary>
        /// 标识此可视化组件的实例化限制方式
        /// 大型可视化组件，如：3D，GIS等作为所有页面的公用底图使用的组件，可考虑多页面间共用一个实例。设置为：OnlyOneInstance
        /// 普通组件可创建多个实例。设置为：MultiInstance
        /// </summary>
        public EnumInstanceType InstanceType
        {
            get { return EnumInstanceType.MultiInstance; }
        }

        /// <summary>
        /// 标识此可视化组件可支持的windows操作系统版本情况
        /// 系统默认按照x86模式进行渲染，如果组件仅支持x64操作系统，则必须在设计时标识使用独立进程渲染
        /// 仅当此可视化组件被设置为独立进程渲染，同时此项标识为x64的情况下
        /// 渲染引擎可考虑采用x64模式渲染，以增加内存的支持和x64特性的保障。
        /// </summary>
        public EnumOperateSystemSupportType OperateSystemSupportType
        {
            get { return EnumOperateSystemSupportType.X86andX64; }
        }

        /// <summary>
        /// 可视化组件释放资源接口
        /// </summary>
        public void DisposeVisualControl()
        {

        }

        ///此组件在显示时是否可以任意移动、改变大小、角度改变
        ///避免有些组件被错误的移动到其他地方，比如标题，底图，背景图片等
        /// *此属性应注册为可在设计端修改的属性
        /// 默认为可移动,true
        private bool m_IsEditableInOperater = true;

        public bool IsEditableInOperater
        {
            get { return m_IsEditableInOperater; }
            set { m_IsEditableInOperater = value; }
        }

        /// <summary>
        /// 是否可以跨屏，跨渲染节点显示
        ///  *此属性应注册为可在设计端修改的属性
        /// 默认为false
        /// 此属性为处理那些高分化改造不充分或无法改造的组件而设计
        /// </summary>
        private bool m_IsMustInOnlyOneMachine = false;

        public bool IsMustInOnlyOneMachine
        {
            get { return m_IsMustInOnlyOneMachine; }
            private set { m_IsMustInOnlyOneMachine = value; }
        }

        #endregion

        #region 接口IPageEvent成员
        /// <summary>
        /// 页面隐藏后
        /// </summary>
        public void AfterPageHide()
        {

        }
        /// <summary>
        /// 页面初始化后
        /// </summary>
        public void AfterPageInit()
        {

        }
        /// <summary>
        /// 页面展示之后
        /// </summary>
        public void AfterPageShow()
        {

        }
        /// <summary>
        /// 页面隐藏前
        /// </summary>
        public void BeforePageHide()
        {

        }
        /// <summary>
        /// 页面显示前
        /// </summary>
        public void BeforePageShow()
        {

        }
        /// <summary>
        /// 页面销毁前 调用释放资源接口
        /// </summary>
        public void BeforePageDispose()
        {
            DisposeVisualControl();
        }
        #endregion
    }
}
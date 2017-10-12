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
using System.Data;
using Visifire.Charts;

namespace HighResolutionApps.VisualControls.HRVTodayControl_ZZGA
{
    /// <summary>
    /// HRVTodayControl_ZZGA.xaml 的交互逻辑
    /// </summary>
    public partial class HRVTodayControl_ZZGA : UserControl, IVisualControl, IPageEvent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HRVTodayControl_ZZGA()
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
        }
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dt"></param>
        void InitInputDataTable()
        {
            try
            {
                SystemHelper.logger.LogDebug("HRVTodayControl_ZZGA ==============InitInputDataTable.end============= ");  
                    DataTable dt = new DataTable();

                    DataColumn dc1 = new DataColumn("name", Type.GetType("System.String"));
                    DataColumn dc2 = new DataColumn("count", Type.GetType("System.Int32"));
                    dt.Columns.Add(dc1);
                    dt.Columns.Add(dc2);

                    dt.Rows.Add("茶陵县公安局", 10);
                    dt.Rows.Add("董家段分局", 15);
                    dt.Rows.Add("荷塘分局", 20);
                    dt.Rows.Add("芦松分局", 25);
                    dt.Rows.Add("石峰分局", 30);
                    dt.Rows.Add("天元分局", 35);
                    dt.Rows.Add("田心分局", 40);
                    dt.Rows.Add("炎陵县公安局", 45);
                    dt.Rows.Add("株洲县公安局", 50);
                    dt.Rows.Add("攸县公安局", 55);
                    dt.Rows.Add("醴陵市公安局", 60);
                    m_InputDataTable = dt;

                SystemHelper.logger.LogDebug("HRVTodayControl_ZZGA ==============InitInputDataTable.end============= ");
                

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        /// <summary>
        /// 绑定数到柱状图
        /// </summary>
        /// <param name="dtChart"></param>
        void InputDataTableToChart(DataTable dtChart)
        {
            try
            {
                SystemHelper.logger.LogDebug("HRVTodayControl_ZZGA ==============InputDataTableToChart.start============= ");
                //判空
                if ((dtChart == null) || (dtChart.Rows.Count == 0))
                {
                    SystemHelper.logger.LogDebug("InputDataTableToChart  dtChart 数据为空");
                    return;
                }
                this.chart.Series.Clear();

                DataSeries dataSeries = new DataSeries();
                dataSeries.RenderAs = RenderAs.Bar;

                DataPoint datapoint = null;       
                for (int i = 0; i < dtChart.Rows.Count; i++)
                {
                    string name = "";
                    int value = 0;

                    if (i < dtChart.Rows.Count)
                    {
                        if (dtChart.Rows[i][0] != DBNull.Value)
                        {
                            name = dtChart.Rows[i][0].ToString();
                        }
                        else
                        {
                            SystemHelper.logger.LogDebug("InputDataTableToChart  name[" + i.ToString() + "][0]数据为空");
                        }

                        try
                        {
                            if (dtChart.Rows[i][1] != DBNull.Value)
                            {
                                value = Convert.ToInt32(dtChart.Rows[i][1]);
                            }
                            else
                            {
                                SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
                            }
                        }
                        catch (Exception)
                        {
                            value = 0;
                        }
                    }
                    datapoint = new DataPoint();
                    datapoint.AxisXLabel = name;
                    datapoint.YValue = Convert.ToDouble(value);
                    datapoint.Tag = value;

                    // double value = Convert.ToDouble(dtChart.Rows[i][1].ToString());
                    LinearGradientBrush pointbrush = new LinearGradientBrush();
                    string[] colorArr = { "#a7e4ff", "#007ee6", "#FAFAD2", "#FFFF00", "#FFE4C4", "#FF8C00" };
                    int stage = getStage(value) - 1;
                    pointbrush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString(colorArr[stage * 2]), offset: 0));
                    pointbrush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString(colorArr[stage * 2 + 1]), offset: 1));
                  
                    datapoint.Color = pointbrush;
                    dataSeries.DataPoints.Add(datapoint);
                }

                #region 设置char
                chart.LightingEnabled = false;
                chart.SmartLabelEnabled = true;
                dataSeries.LabelAngle = 0;//字体角度
                this.chart.AnimationEnabled = true;
                this.chart.ThemeEnabled = true;
                this.chart.Series.Add(dataSeries);
                this.chart.FontSize = 18;
                this.chart.ShadowEnabled = false;
                #endregion

                #region 设置x轴
                AxisLabels xLabel = new AxisLabels();
                xLabel.FontColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99cccc")); //x轴刻度文本信息颜色
                ChartGrid xGrid = new ChartGrid();//设置x轴的纵向刻度虚线
                xGrid.Enabled = false;
                Axis xAxis = new Axis();
                xAxis.Enabled = true; //是否显示X轴刻度、文本
                xAxis.AxisLabels = xLabel;
                xAxis.AxisLabels.FontSize = 18;//字体大小
                xAxis.FontFamily = new System.Windows.Media.FontFamily("宋体");
                xAxis.Grids.Add(xGrid);
                chart.AxesX.Add(xAxis);
                #endregion

                #region 设置y轴
                AxisLabels yLabel = new AxisLabels();
                yLabel.FontColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99cccc")); //y轴刻度文本信息颜色
                ChartGrid yGrid = new ChartGrid();// 设置y轴的横向刻度虚线
                yGrid.Enabled = true;
                Axis yAxis = new Axis();
                yAxis.Enabled = true; //是否显示Y轴刻度、文本
                yAxis.Grids.Add(yGrid);
                yAxis.AxisMinimum = 0;  //y轴刻度最小值
                dataSeries.LabelEnabled = true;
                yAxis.Interval = 10;    //设置y轴刻度的增量 -- 即2个刻度值之间的的间隔
                yAxis.IntervalType = IntervalTypes.Number;
                yAxis.LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003333"));
                xAxis.FontFamily = new System.Windows.Media.FontFamily("微软雅黑");
                yAxis.AxisLabels = yLabel;
                chart.AxesY.Add(yAxis);
                #endregion

                SystemHelper.logger.LogDebug("HRVTodayControl_ZZGA ==============InputDataTableToChart.end============= ");

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        #region  所需属性

        #region 0.绑定数据
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
            "InputDataTable", typeof(DataTable), typeof(HRVTodayControl_ZZGA), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnInputDataTableChange)));


        /// <summary>
        /// 改变输入的datatable
        /// </summary> 
        private static void OnInputDataTableChange(DependencyObject o, DependencyPropertyChangedEventArgs ea)
        {
            try
            {
                //绑定数据
                HRVTodayControl_ZZGA gridControl = o as HRVTodayControl_ZZGA;
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
                //m_InputDataTable = InputDataTable;
                InputDataTableToChart(InputDataTable);

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region 1.置标题字体样式
        public static readonly DependencyProperty TitleFontFamilyProperty =
      DependencyProperty.Register("TitleFontFamily", typeof(enum_MyFontFamily), typeof(HRVTodayControl_ZZGA), new PropertyMetadata(enum_MyFontFamily.微软雅黑, OnTitleFontFamilyChanged));
        public enum_MyFontFamily TitleFontFamily
        {
            set { this.SetValue(TitleFontFamilyProperty, value); }
            get { return (enum_MyFontFamily)this.GetValue(TitleFontFamilyProperty); }
        }
        public static void OnTitleFontFamilyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVTodayControl_ZZGA);
            control.ReRender(1);
            System.Drawing.Text.InstalledFontCollection font = new System.Drawing.Text.InstalledFontCollection();
            System.Drawing.FontFamily[] array = font.Families;


        }

        #endregion

        #region 2.设置标题字体颜色

        public static readonly DependencyProperty TitleForegroundColorProperty =
      DependencyProperty.Register("TitleForegroundColor", typeof(Brush), typeof(HRVTodayControl_ZZGA), new PropertyMetadata(Brushes.Black, OnTitleForegroundColorChanged));
        public Brush TitleForegroundColor
        {
            set { this.SetValue(TitleForegroundColorProperty, value); }
            get { return (Brush)this.GetValue(TitleForegroundColorProperty); }
        }

        public static void OnTitleForegroundColorChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVTodayControl_ZZGA);
            control.ReRender(2);
        }

        #endregion

        #region 3.设置标题内容

        public static readonly DependencyProperty TextTitleProperty =
      DependencyProperty.Register("TextTitle", typeof(string), typeof(HRVTodayControl_ZZGA), new PropertyMetadata("今日警情状态窗口", OnTextTitleChanged));
        public string TextTitle
        {
            set { this.SetValue(TextTitleProperty, value); }
            get { return (string)this.GetValue(TextTitleProperty); }
        }

        public static void OnTextTitleChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVTodayControl_ZZGA);
            control.ReRender(3);
        }

        #endregion

        #region 4.标题字体大小

        public static readonly DependencyProperty TitleSizeProperty =
 DependencyProperty.Register("TitleSize", typeof(double), typeof(HRVTodayControl_ZZGA), new PropertyMetadata(30d, OnTitleSizeChanged));
        public double TitleSize
        {
            set { this.SetValue(TitleSizeProperty, value); }
            get { return (double)this.GetValue(TitleSizeProperty); }
        }

        public static void OnTitleSizeChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVTodayControl_ZZGA);
            control.ReRender(4);
        }

        #endregion

        /// <summary>
        /// 字体
        /// </summary>
        public enum enum_MyFontFamily
        {
            微软雅黑,
            宋体,
            黑体,
            楷体,
        };
        /// <summary>
        /// 属性返回值
        /// </summary>
        /// <param name="x"></param>
        void ReRender(int x)
        {
            switch (x)
            {
                case 1:
                    t1.FontFamily = new FontFamily(TitleFontFamily.ToString());
                    break;
                case 2:
                    t1.Foreground = TitleForegroundColor;
                    break;

                case 3:
                    t1.Text = TextTitle;
                    break;


                case 4:
                    t1.FontSize = TitleSize;
                    break;

            }
        }
        /// <summary>
        /// 判断颜色
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        int getStage(double v)
        {
            int stage1 = 30;
            int stage2 = 40;

            if (v <= stage1) return 1;
            if ((v > stage1) && (v <= stage2)) return 2;
            return 3;
        }

        #endregion

        #region 标题修改 序列化控制
        /// <summary>
        /// 主标题
        /// </summary>
        private Label m_lb_ControlInstanceMainTitle;
        /// <summary>
        /// 副标题
        /// </summary>
        private Label m_lb_ControlInstanceSubTitle;
        /// <summary>
        /// 修改组件框中的标题
        /// </summary>
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
        /// 重要 是否序列化内容  
        /// </summary>
        /// <returns></returns>
        public override bool ShouldSerializeContent()
        {
            return false;
        }
        #endregion


        #region 接口IVisualControl成员
        /// <summary>
        /// 组件背景窗口名称
        /// </summary>
        private string m_WindowStyleName;
        /// <summary>
        /// 组件背景窗口名称
        /// </summary>
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
                    InitInputDataTable();
                    InputDataTableToChart(m_InputDataTable);
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
        /// <summary>
        /// 是否加载完成
        /// </summary>
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
        /// <summary>
        /// 加载顺序
        /// </summary>
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
            get { return "9FE1E500-039E-41F7-8338-123D9D52CD9C"; }
        }


        /// <summary>
        /// 本可视化组件类型名称
        /// 需要修改 组件名称
        /// </summary>
        public string ControlName
        {
            get { return "组件名称：请同步修改wrapper中的组件名称保持一致"; }
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
        /// <summary>
        /// 窗口默认主标题
        /// </summary>
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
        /// <summary>
        /// 窗口默认副标题
        /// </summary>
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
        /// <summary>
        /// 组件描述信息
        /// </summary>
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
            get { return new Version("1.1.0.0"); }
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
        /// </summary>
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
                str[0] = "微软雅黑";
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
        /// <summary>
        /// 渲染模式
        /// </summary>
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
        /// <summary>
        /// 内存需求
        /// </summary>
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
        /// <summary>
        /// 是否可编辑
        /// </summary>
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
        /// <summary>
        /// 是否可以跨屏
        /// </summary>
        public bool IsMustInOnlyOneMachine
        {
            get { return m_IsMustInOnlyOneMachine; }
            private set { m_IsMustInOnlyOneMachine = value; }
        }

        #endregion


        #region IPageEvent接口
        /// <summary>
        /// 整个页面隐藏之后的通知方法
        /// </summary>
        public void AfterPageHide()
        {

        }
        /// <summary>
        /// 整个页面初始化之后的通知方法，区分于组件loaded，
        /// 此方法为所有组件均loaded完成之后触发
        /// </summary>
        public void AfterPageInit()
        {

        }
        /// <summary>
        /// 整个页面显示到大屏之后的通知
        /// </summary>
        public void AfterPageShow()
        {

        }

        /// <summary>
        /// 整个页面隐藏显示之前的通知方法
        /// </summary>
        public void BeforePageHide()
        {

        }

        /// <summary>
        /// 整个页面显示之前的通知方法
        /// </summary>
        public void BeforePageShow()
        {

        }

        /// <summary>
        /// 整个页面释放之前的通知方法，要求组件停止timer，处理逻辑，保存状态等
        /// </summary>
        public void BeforePageDispose()
        {

        }
        #endregion
    }
}

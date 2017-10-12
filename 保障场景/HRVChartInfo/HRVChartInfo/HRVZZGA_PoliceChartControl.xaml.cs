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
using Visifire.Charts;
using System.Data;

namespace HighResolutionApps.VisualControls.HRVChartInfo
{
    /// <summary>
    /// HRVChartInfo.xaml 的交互逻辑
    /// </summary>
    public partial class HRVChartInfo : UserControl, IVisualControl, IPageEvent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HRVChartInfo()
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
        /// 绑定Table表数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ChartType"></param>
        public void InitInputDataTable()
        {
            try
            {
                SystemHelper.logger.LogDebug("HRVChartInfo ==============InitInputDataTable.start============= ");
                DataTable dt = new DataTable();

                DataColumn dc1 = new DataColumn("name", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("PoliceNum", Type.GetType("System.Int32"));
                DataColumn dc3 = new DataColumn("CarNum", Type.GetType("System.Int32"));
                DataColumn dc4 = new DataColumn("OnlinePoliceNum", Type.GetType("System.Int32"));
                DataColumn dc5 = new DataColumn("OnlineCarNum", Type.GetType("System.Int32"));

                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);

                dt.Rows.Add("市公安局", 80, 43, 44, 38);
                dt.Rows.Add("茶陵县公安局", 61, 42, 33, 23);
                dt.Rows.Add("董家段分局", 55, 26, 45, 20);
                dt.Rows.Add("荷塘分局", 63, 42, 33, 23);
                dt.Rows.Add("芦淞分局", 75, 43, 44, 38);
                dt.Rows.Add("石峰分局", 60, 43, 44, 38);
                dt.Rows.Add("天元分局", 55, 43, 44, 38);
                dt.Rows.Add("田心分局", 29, 22, 21, 13);
                dt.Rows.Add("炎陵县公安局", 55, 43, 44, 38);
                dt.Rows.Add("株洲县公安局 ", 45, 43, 44, 38);
                dt.Rows.Add("攸县公安局", 61, 43, 44, 38);
                dt.Rows.Add("醴陵市公安局", 29, 22, 21, 13);

                m_InputDataTable = dt;

                SystemHelper.logger.LogDebug("HRVChartInfo ==============InitInputDataTable.end============= ");
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        /// <summary>
        /// 绑定柱状图数据
        /// </summary>
        /// <param name="dtChart"></param>
        /// <param name="ChartType"></param>
        public void InputTableToChart(DataTable dtChart, int ChartType)
        {
            try
            {
                SystemHelper.logger.LogDebug("HRVChartInfo ==============InputTableToChart.start============= ");
                //判空
                if ((dtChart == null) || (dtChart.Rows.Count == 0))
                {
                    SystemHelper.logger.LogDebug("InputDataTableToChart  dtChart 数据为空");
                    return;
                }
                //绑定Chart前先清空
                chart.Series.Clear();
                chart.Titles.Clear();

                chart.AnimationEnabled = true;
                chart.ThemeEnabled = true;
                chart.DataPointWidth = 5;   //柱体宽度

                #region 设置柱状图的柱体颜色
                ColorSet cs = new ColorSet();
                cs.Id = "colorset1";
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString("#a7e4ff"), offset: 0));//使用渐变色作为主体颜色
                brush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString("#007ee6"), offset: 1));
                cs.Brushes.Add(brush);
                chart.ColorSets.Add(cs);
                chart.ColorSet = "colorset1";
                #endregion

                chart.LightingEnabled = false;
                chart.ShadowEnabled = false;
                chart.View3D = false;   //使Chart显示三维

                DataSeries dataSeries = new DataSeries();
                DataPoint datapoint = null;
                dataSeries.LabelFontStyle = FontStyles.Normal;
                dataSeries.RenderAs = RenderAs.Column;
                dataSeries.LabelStyle = LabelStyles.OutSide;
                dataSeries.LabelEnabled = true;

                #region 设置x轴信息
                AxisLabels xLabel = new AxisLabels();
                xLabel.FontColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5e9ff")); //x轴刻度文本信息颜色

                ChartGrid xGrid = new ChartGrid();//设置x轴的纵向刻度虚线
                xGrid.Enabled = false;

                Axis xAxis = new Axis();
                xAxis.Enabled = true; //是否显示X轴刻度、文本
                xAxis.AxisLabels = xLabel;
                xAxis.AxisLabels.FontSize = 7;
                xAxis.FontFamily = new System.Windows.Media.FontFamily("宋体");
                xAxis.Grids.Add(xGrid);
                chart.AxesX.Add(xAxis);
                #endregion

                #region 设置y轴信息
                AxisLabels yLabel = new AxisLabels();
                yLabel.FontColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5e9ff")); //y轴刻度文本信息颜色

                ChartGrid yGrid = new ChartGrid();// 设置y轴的横向刻度虚线
                yGrid.Enabled = true;

                Axis yAxis = new Axis();
                yAxis.Enabled = true;       //是否显示Y轴刻度、文本
                yAxis.Grids.Add(yGrid);
                yAxis.AxisMinimum = 0;      //y轴刻度最小值
                //yAxis.AxisMaximum = 100;  //y轴刻度最大值
                //yAxis.Interval = 20;      //设置y轴刻度的增量 -- 即2个刻度值之间的的间隔
                yAxis.IntervalType = IntervalTypes.Number;
                yAxis.AxisLabels = yLabel;
                chart.AxesY.Add(yAxis);
                #endregion

                //各项警力数量
                int policeNum = 0;
                int carNum = 0;
                int onlineCar = 0;
                int onLinePolice = 0;
              
                for (int i = 0; i < dtChart.Rows.Count; i++)
                {
                    if (dtChart.Rows[i][1] != DBNull.Value)
                    {
                        policeNum += Convert.ToInt32(dtChart.Rows[i][1]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
                    }
                    if (dtChart.Rows[i][2] != DBNull.Value)
                    {
                        carNum += Convert.ToInt32(dtChart.Rows[i][2]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][2]数据为空");
                    }
                    if (dtChart.Rows[i][3] != DBNull.Value)
                    {
                        onlineCar += Convert.ToInt32(dtChart.Rows[i][3]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][3]数据为空");
                    }
                    if (dtChart.Rows[i][4] != DBNull.Value)
                    {
                        onLinePolice += Convert.ToInt32(dtChart.Rows[i][4]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][4]数据为空");
                    }            
                    datapoint = new DataPoint();
                    datapoint.AxisXLabel = dtChart.Rows[i][0].ToString();
                    double value;
                    string tag;
                    int val1 = 0;
                    int val2 = 0;
                    int val3 = 0;
                    int val4 = 0;
                    if (dtChart.Rows[i][1] != DBNull.Value)
                    {
                        val1 = Convert.ToInt32(dtChart.Rows[i][1]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
                    }
                    if (dtChart.Rows[i][2] != DBNull.Value)
                    {
                        val2 = Convert.ToInt32(dtChart.Rows[i][2]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][2]数据为空");
                    }
                    if (dtChart.Rows[i][3] != DBNull.Value)
                    {
                        val3 = Convert.ToInt32(dtChart.Rows[i][3]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][3]数据为空");
                    }
                    if (dtChart.Rows[i][4] != DBNull.Value)
                    {
                        val4 = Convert.ToInt32(dtChart.Rows[i][4]);
                    }
                    else
                    {
                        SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][4]数据为空");
                    }        
                    switch (ChartType)
                    {
                        case 2:
                            value = Convert.ToDouble(val2);
                            tag =   val2.ToString();
                            break;
                        case 3:
                            value = Convert.ToDouble(val3);
                            tag =   val3.ToString();
                            break;
                        case 4:
                            value = Convert.ToDouble(val4);
                            tag =   val4.ToString();
                            break;
                        default:
                            value =Convert.ToDouble(val1);
                            tag =  val1.ToString();
                            break;
                    }
                    datapoint.YValue = value;
                    datapoint.Tag = tag;

                    dataSeries.DataPoints.Add(datapoint);
                }
                Run runPolice = (grid_PoliceNum.FindName("PoliceNum") as Run);
                Run runCar = (grid_PoliceNum.FindName("CarNum") as Run);
                Run runOnlinePolice = (grid_PoliceNum.FindName("OnlinePoliceNum") as Run);
                Run runOnlineCar = (grid_PoliceNum.FindName("OnlineCarNum") as Run);

                runPolice.Text = policeNum.ToString();
                runCar.Text = carNum.ToString();
                runOnlinePolice.Text = onLinePolice.ToString();
                runOnlineCar.Text = onlineCar.ToString();

                chart.Series.Add(dataSeries);

                Title title = new Title();
                title.Text = "各区县警力配置详情";
                title.FontSize = 15;
                title.FontColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81d5ff"));
                chart.Titles.Add(title);
                SystemHelper.logger.LogDebug("HRVChartInfo ==============InputTableToChart.end============= ");
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name,
                    ex.Message.ToString(), ex.ToString());
                SystemHelper.logger.LogDebug("加载柱状图出错--- 柱状图数据长度：" + dtChart.Rows.Count.ToString() + ",错误信息：" + ex.Message);
            }
        }
        void ReRenderdata()
        {
            //m_InputDataTable = InputDataTable;
            InputTableToChart(InputDataTable, 1);
        }
        /// <summary>
        /// 警员数量点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPoliceNum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //m_InputDataTable = InputDataTable;
            InputTableToChart(m_InputDataTable, 1);
        }
        /// <summary>
        /// 警车数量点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_InputDataTable = InputDataTable;
            InputTableToChart(m_InputDataTable, 2);
        }
        /// <summary>
        /// 在线警员点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOnlinePoliceNum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_InputDataTable = InputDataTable;
            InputTableToChart(m_InputDataTable, 3);
        }
        /// <summary>
        /// 在线警车点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOnlineCarNum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_InputDataTable = InputDataTable;
            InputTableToChart(InputDataTable, 4);
        }

        #region 依赖属性

        #region 标题文字大小
        public static readonly DependencyProperty TitleSizeProperty =
 DependencyProperty.Register("TitleSize", typeof(double), typeof(HRVChartInfo), new PropertyMetadata(28d, OnTitleSizeChanged));
        /// <summary>
        /// 标题文字大小
        /// </summary>
        public double TitleSize
        {
            set { this.SetValue(TitleSizeProperty, value); }
            get { return (double)this.GetValue(TitleSizeProperty); }
        }

        public static void OnTitleSizeChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            HRVChartInfo hrv = dp as HRVChartInfo;
            double text = (double)e.NewValue;
            hrv.headerText.FontSize = text;

        }
        #endregion

        #region 柱状图X轴字体大小
        public static readonly DependencyProperty ChartXSizeProperty =
DependencyProperty.Register("ChartXSize", typeof(double), typeof(HRVChartInfo), new PropertyMetadata(7d, OnChartXSizeChanged));

        /// <summary>
        /// 柱状图X轴字体大小
        /// </summary>
        public double ChartXSize
        {
            set { this.SetValue(ChartXSizeProperty, value); }
            get { return (double)this.GetValue(ChartXSizeProperty); }
        }

        public static void OnChartXSizeChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            HRVChartInfo hrv = dp as HRVChartInfo;
            double text = (double)e.NewValue;
            for (int i = 0; i < hrv.chart.AxesX.Count; i++)
            {
                hrv.chart.AxesX[i].AxisLabels.FontSize = text;
            }


        }
        #endregion

        #region 绑定数据
        private DataTable m_InputDataTable = new DataTable();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public DataTable InputDataTable
        {
            get { return (DataTable)GetValue(InputDataTableProperty); }
            set { SetValue(InputDataTableProperty, value); }
        }

        public static readonly DependencyProperty InputDataTableProperty =
            DependencyProperty.Register("InputDataTable", typeof(DataTable), typeof(HRVChartInfo), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnInputDataTableChange)));
        private static void OnInputDataTableChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                HRVChartInfo hrv = d as HRVChartInfo;
                hrv.ReRenderdata();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

        #region 表格标题文字
        /// <summary>
        /// 表格标题文字
        /// </summary>
        public string TextHeaderContent
        {
            get { return (string)GetValue(TextHeaderContentProperty); }
            set { SetValue(TextHeaderContentProperty, value); }
        }

        public static readonly DependencyProperty TextHeaderContentProperty =
            DependencyProperty.Register("TextHeaderContent", typeof(string), typeof(HRVChartInfo), new PropertyMetadata("今日警力状态", TextHeaderContentCallBack));
        private static void TextHeaderContentCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue != null)
                {
                    HRVChartInfo hrv = d as HRVChartInfo;
                    string text = (string)e.NewValue;
                    hrv.headerText.Text = text;
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        #endregion

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
                    InputTableToChart(m_InputDataTable, 1);
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
            get { return "今日警力状态"; }
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
//for (int i = 0; i < dtChart.Rows.Count; i++)
//{
//    string name = "";
//    int value1 = 0;
//     int value2 = 0;
//     int value3 = 0;
//     int value4 = 0;

//    if (i < dtChart.Rows.Count)
//    {
//        if (dtChart.Rows[i][0] != DBNull.Value)
//        {
//            name = dtChart.Rows[i][0].ToString();
//        }
//        else
//        {
//            SystemHelper.logger.LogDebug("InputDataTableToChart  name[" + i.ToString() + "][0]数据为空");
//        }

//        try
//        {
//            if (dtChart.Rows[i][1] != DBNull.Value)
//            {
//                value1 = Convert.ToInt32(dtChart.Rows[i][1]);
//            }
//            else
//            {
//                SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
//            }
//            if (dtChart.Rows[i][2] != DBNull.Value)
//            {
//                value2 = Convert.ToInt32(dtChart.Rows[i][2]);
//            }
//            else
//            {
//                SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
//            }
//            if (dtChart.Rows[i][3] != DBNull.Value)
//            {
//                value3 = Convert.ToInt32(dtChart.Rows[i][3]);
//            }
//            else
//            {
//                SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
//            }
//            if (dtChart.Rows[i][4] != DBNull.Value)
//            {
//                value4 = Convert.ToInt32(dtChart.Rows[i][4]);
//            }
//            else
//            {
//                SystemHelper.logger.LogDebug("InputDataTableToChart  value[" + i.ToString() + "][1]数据为空");
//            }
//        }
//        catch (Exception)
//        {   
//            value1 = 0;
//            value2 = 0;
//            value3 = 0;
//             value4 = 0;
//        }
//    }
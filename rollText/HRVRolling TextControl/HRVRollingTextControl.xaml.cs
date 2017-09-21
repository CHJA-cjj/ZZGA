using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HighResolutionApps.Interfaces;
using HrvVisualControlStyles;
using VisualContorlBase;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HighResolutionApps.VisualControls.HRVRollingTextControl_ZZGA
{
    /// <summary>
    /// HRVRolling_TextControl.xaml 的交互逻辑
    /// </summary>
    public partial class HRVRollingTextControl_ZZGA : UserControl, IVisualControl, IPageEvent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HRVRollingTextControl_ZZGA()
        {
            InitializeComponent();
            // 必要 样式使用
            StyleManager.GetResourceDictionnary(this, m_WindowStyleName);
            // 区分当前运行环境
            //if (CurrentRunderUnitEnvironment.Mode == EnvironmentMode.Designer)
            {
                // 运行环境是设计端
                // 界面渲染  在设计端调用  在其他运行环境无需调用
                LoadContent();      
            }

            InitScollText();
        }


        #region 所需属性
        #region define rolling param

        int step = 30;
        int length = 7000;
        int textlength = 0;

        #endregion

        #region 设置滚动TextPostion
        public static readonly DependencyProperty TextPostionProperty =
            DependencyProperty.Register("TextPostion", typeof(int), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(7000, OnTextPostionChanged));

        public int TextPosition
        {
            set { this.SetValue(TextPostionProperty, value); }
            get { return (int)this.GetValue(TextPostionProperty); }
        }
        public static void OnTextPostionChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            try{
                if (e.NewValue != null)
                {
                    var control = (dp as HRVRollingTextControl_ZZGA);
                    control.SetTextPostion();
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }

        }
        public void SetTextPostion()
        {
            try
            {
                int leftMargin = length - TextPosition;
                int rightMargin = TextPosition - textlength;
                t1.Margin = new Thickness(leftMargin, 0, rightMargin, 0);
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }


        #endregion

        #region 设置文本滚动步长

        public static readonly DependencyProperty TextStepProperty =
            DependencyProperty.Register("TextStep", typeof(int), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(30, OnTextStepChanged));
        public int TextStep
        {
            set { this.SetValue(TextStepProperty, value); }
            get { return (int)this.GetValue(TextStepProperty); }
        }

        public static void OnTextStepChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue != null)
                {
                    var control = (dp as HRVRollingTextControl_ZZGA);
                    control.SetTextStep();
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        public void SetTextStep()
        {
            try
            {
                step = TextStep;
                StartTimer();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        #endregion

        #region 设置文本框长度

        public static readonly DependencyProperty TextBlockLengthProperty =
            DependencyProperty.Register("TextBlockLength", typeof(int), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(7000, OnTextBlockLengthChanged));
        public int TextBlockLength
        {
            set { this.SetValue(TextBlockLengthProperty, value); }
            get { return (int)this.GetValue(TextBlockLengthProperty); }
        }

        public static void OnTextBlockLengthChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue != null)
                {
                    var control = (dp as HRVRollingTextControl_ZZGA);
                    control.SetTextBlockLength();
                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        public void SetTextBlockLength()
        {
            try
            {
                length = TextBlockLength;
                StartTimer();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        #endregion

        #region 设置文本长度

        public static readonly DependencyProperty TextLengthProperty =
            DependencyProperty.Register("TextLength", typeof(int), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(2000, OnTextLengthChanged));
        public int TextLength
        {
            set { this.SetValue(TextBlockLengthProperty, value); }
            get { return (int)this.GetValue(TextBlockLengthProperty); }
        }

        public static void OnTextLengthChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue != null)
                {
                    var control = (dp as HRVRollingTextControl_ZZGA);
                    control.SetTextLength();

                }
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }
        public void SetTextLength()
        {
            try
            {

                textlength = TextLength;
                StartTimer();
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
        }

        #endregion

        #region 滚动定时设置

        //int fristRoll = 1;
        public void ScollText(object o, EventArgs e)
        {
            try
            {
                // 实时更新 文本框 的实际大小
                t1.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                t1.Arrange(new Rect(t1.DesiredSize));
                textlength = Convert.ToInt32(t1.ActualWidth);
                //SystemHelper.logger.LogDebug("textlength = " + textlength);

                // TextPosition记录 文本框在当前区域走过的路径的长度
                TextPosition = (TextPosition + step) % (length + textlength);
                
                // 对命令系统进行一次刷新。
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {

                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }
            return;
        }

        DispatcherTimer timer;
        
        public void InitScollText()
        {
            try
            {
                //TextBlock txt = new TextBlock();
                t1.Text = "对 党 忠 诚   服 务 人 民   执 法 公 正   纪 律 严 明";

                // 重新测量 文本框 的实际大小
                t1.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                t1.Arrange(new Rect(t1.DesiredSize));
                textlength = Convert.ToInt32(t1.ActualWidth);
                //SystemHelper.logger.LogDebug("textlength = " + textlength);

                LinearGradientBrush brush = new LinearGradientBrush();
                brush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString("#fffdf6"), offset: 0));
                brush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString("#ff9222"), offset: 1));
                t1.Foreground = brush;

                TextPosition = 0 ;

                if (CurrentRunderUnitEnvironment.Mode != EnvironmentMode.Runtime)
                {
                    timer = new DispatcherTimer();
                    timer.Tick += new EventHandler(ScollText);
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    timer.Start();
                }

            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }

        }

        public void StartTimer()
        {
            try
            {
                timer.Stop();
                timer.Tick -= ScollText;
                //timer = new DispatcherTimer();
                timer.Tick += ScollText;
                timer.Interval = TimeSpan.FromSeconds(0.1);
                timer.Start();
            }
            catch (Exception ex)
            {

                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }

        }
        #endregion

        #region 设置标题字体样式
        public static readonly DependencyProperty TitleFontFamilyProperty =
            DependencyProperty.Register("TitleFontFamily", typeof(Enum_MyFontFamily), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(Enum_MyFontFamily.微软雅黑, OnTitleFontFamilyChanged));
        public Enum_MyFontFamily TitleFontFamily
        {
            set { this.SetValue(TitleFontFamilyProperty, value); }
            get { return (Enum_MyFontFamily)this.GetValue(TitleFontFamilyProperty); }
        }
        public static void OnTitleFontFamilyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVRollingTextControl_ZZGA);
            control.ReRender(1);
            System.Drawing.Text.InstalledFontCollection font = new System.Drawing.Text.InstalledFontCollection();
            System.Drawing.FontFamily[] array = font.Families;
        }
        #endregion

        #region 设置标题字体颜色

        public static readonly DependencyProperty TitleForegroundColorProperty =
      DependencyProperty.Register("TitleForegroundColor", typeof(Brush), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(Brushes.YellowGreen, OnTitleForegroundColorChanged));
        public Brush TitleForegroundColor
        {
            set { this.SetValue(TitleForegroundColorProperty, value); }
            get { return (Brush)this.GetValue(TitleForegroundColorProperty); }
        }

        public static void OnTitleForegroundColorChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVRollingTextControl_ZZGA);
            control.ReRender(2);
        }

        #endregion

        #region 设置标题内容

        public static readonly DependencyProperty TextTitleProperty =
      DependencyProperty.Register("TextTitle", typeof(string), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata("对 党 忠 诚    服 务 人 民    执 法 公 正    纪 律 严 明", OnTextTitleChanged));
        public string TextTitle
        {
            set { this.SetValue(TextTitleProperty, value); }
            get { return (string)this.GetValue(TextTitleProperty); }
        }
        public static void OnTextTitleChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVRollingTextControl_ZZGA);
            control.ReRender(3);
        }
        #endregion

        #region 标题字体大小

        public static readonly DependencyProperty TitleSizeProperty =
            DependencyProperty.Register("TitleSize", typeof(double), typeof(HRVRollingTextControl_ZZGA), new PropertyMetadata(90d, OnTitleSizeChanged));
        public double TitleSize
        {
            set { this.SetValue(TitleSizeProperty, value); }
            get { return (double)this.GetValue(TitleSizeProperty); }
        }
        public static void OnTitleSizeChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var control = (dp as HRVRollingTextControl_ZZGA);
            control.ReRender(4);
        }
        #endregion
        public enum Enum_MyFontFamily
        {
            微软雅黑,
            华文细黑,
            粗体,
            小纂, // 小篆
            Arial, 
            Helvetica,
            宋体, 
            Times,
            华文仿宋,
            Georgia,            
            serif黑体,
            Verdana,
            方正姚体,
            Geneva,      
            幼圆,
            幼圆斜体,
            方正舒体,    
            华文彩云,
        };

        void ReRender(int x)
        {
            DoubleAnimation control = (gun.FindName("info") as DoubleAnimation);
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
                //if (CurrentRunderUnitEnvironment.Mode == EnvironmentMode.Designer)
                {
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
            get { return "557200E1-A031-4DB4-94B1-00535E063E37"; }
        }


        /// <summary>
        /// 本可视化组件类型名称
        /// 需要修改 组件名称
        /// </summary>
        public string ControlName
        {
            get { return "1.滚动文本"; }
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

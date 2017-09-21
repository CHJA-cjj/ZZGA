using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HighResolutionApps.Common;
using HighResolutionApps.Interfaces;
using HighResolutionApps.Interfaces.Plugins;

namespace HighResolutionApps.VisualControls.HRVZZCityMapControl_ZZGA
{
    public class Plugin : IVisualControlsPlug
    {
        /// <summary>
        /// 环境变量，用来判断当前组件运行环境
        /// </summary>
        private IEnvironment environment;

        public IEnvironment Environment
        {
            get { return environment; }
            set
            {
                this.environment = value;
                Initialize();
            }
        }

        /// <summary>
        /// 当前插件内包含的所有组件
        /// </summary>
        List<IVisualControlDescriptor> controls = new List<IVisualControlDescriptor>();

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Plugin()
        {
        }

        #region IVisualControlsPlug Members

        /// <summary>
        /// 插件名称，理解为分类名称
        /// </summary>
        public string Name
        {
            get { return "株洲公安"; }
        }

        /// <summary>
        /// 接口实现
        /// </summary>
        public IVisualControlDescriptor[] Controls
        {
            get { return controls.ToArray(); }
        }

        /// <summary>
        /// ID，必须为唯一而且命名要有一定意义，便于脚本编写
        /// 需要修改
        /// </summary>
        public string PluginId
        {
            get { return "ZZCMap_controls_plug"; }
        }

        /// <summary>
        /// 插件初始化 
        /// 需要修改
        /// </summary>
        public void Initialize()
        {
            //自定义组件增加
            HRVUserControlWrappers.Plugin = this;
            this.controls.Add(new HRVUserControlWrappers());

        }
        #endregion


        /// <summary>
        /// 插件配置保存
        /// </summary>
        public void SaveSettings()
        {

        }

        /// <summary>
        /// 插件配置载入
        /// </summary>
        void LoadSettings()
        {

        }

        /// <summary>
        /// 处理项目载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnProjectLoad(object sender, System.EventArgs e)
        {
            LoadSettings();
        }

        public void DeInitialize()
        { }

    }
}

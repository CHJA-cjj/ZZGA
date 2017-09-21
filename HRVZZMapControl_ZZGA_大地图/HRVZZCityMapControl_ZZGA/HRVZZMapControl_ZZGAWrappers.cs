using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using HighResolutionApps.Common.Schema;
using HighResolutionApps.Common.UI.Editor;
using HighResolutionApps.Interfaces;
using HrvVisualControlStyles;

namespace HighResolutionApps.VisualControls.HRVZZCityMapControl_ZZGA
{
    /// <summary>
    /// 组件封装类 实现IVisualControlDescriptor接口
    /// </summary>
    class HRVUserControlWrappers : IVisualControlDescriptor
    {
        public static Plugin Plugin { get; set; }

        #region interface IVisualControlDescriptor

        /// <summary>
        /// 组件名称
        /// </summary>
        public string Name
        {
            get { return "1.株洲地图"; }
        }

        public string PluginId
        {
            get { return Plugin.PluginId; }
        }

        public Type Type
        {
            get { return typeof(HRVZZCityMapControl_ZZGA); }
        }

        public ManipulatorKind ManipulatorKind
        {
            get { return ManipulatorKind.DragResizeRotateManipulator; }
        }

        public UIElement CreateControl()
        {
            return new HRVZZCityMapControl_ZZGA();
        }

        public object Tag
        {
            get;
            set;
        }

        public ICustomTypeDescriptor getPropProxy(object o)
        {
            return (ICustomTypeDescriptor)new HRVUserControlPropProxy(o);
        }
        #endregion interface IVisualControlDescriptor
    }

    public class HRVUserControlPropProxy : HighResolutionApps.Common.Schema.BasePropProxy
    {

        public HRVUserControlPropProxy(object controlledObject)
            : base(controlledObject)
        {
        }

        /// <summary>
        /// 控件属性注册
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<PropertyWrapper> result = new List<PropertyWrapper>();
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "InputDataTable", null, result, "数据库数据输入", "数据");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "HighNum", null, result, "定义地图中颜色范围的数据中”数量高”的最大值", "调试");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "LowNum", null, result, "定义地图中颜色范围的数据中”数量中等”的最小值", "调试");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "MiddleNum", null, result, "定义地图中颜色范围的数据中”数量中等”的最小值", "调试");

            // 组别：基础
            RegisterProperty(typeof(FrameworkElement), "Name", null, result, "组件名称", "基础");
            RegisterProperty(typeof(FrameworkElement), "Cursor", null, result, "光标", "基础");
            RegisterProperty(typeof(FrameworkElement), "IsEnabled", null, result, "使能", "基础");
            RegisterProperty(typeof(FrameworkElement), "Background", typeof(BrushEditor), result, "背景", "基础");
            RegisterProperty(typeof(FrameworkElement), "Opacity", null, result, "透明度", "基础");
            RegisterProperty(typeof(UIElement), "Visibility", null, result, "是否可见", "基础");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "IsEditableInOperater", null, result, "是否可以任意移动、改变大小、角度改变", "基础");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "IsMustInOnlyOneMachine", null, result, "是否可以跨屏，跨渲染节点显示", "基础");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "MemoryRequest", null, result, "内存需求", "基础");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "ControlInstanceDes", null, result, "组件示例描述", "基础");
            // 位置尺寸
            RegisterProperty(typeof(FrameworkElement), "Canvas.Top", null, result, "上侧位置", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Canvas.Left", null, result, "左侧位置", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Width", null, result, "宽度", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Height", null, result, "高度", "位置尺寸");

            // 组别: 外观
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "ControlInstanceMainTitle", null, result, "窗口默认主标题", "外观");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "ControlInstanceSubTitle", null, result, "窗口默认副标题", "外观");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "WindowStyleName", typeof(SelectStylesEditor), result, "样式名称", "外观");
            // 组别：渲染
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "RenderMethod", null, result, "加载方式", "渲染");
            RegisterProperty(typeof(HRVZZCityMapControl_ZZGA), "RenderOrder", null, result, "渲染优先级", "渲染");

            return new PropertyDescriptorCollection(result.ToArray());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="sourceProperty"></param>
        /// <param name="editor"></param>
        /// <param name="result"></param>
        /// <param name="description">描述</param>
        /// <param name="group">组别</param>
        void RegisterProperty(Type objectType, string sourceProperty, Type editor, List<PropertyWrapper> result, string description, string group)
        {
            try
            {
                PropertyInfo info = new PropertyInfo();
                info.SourceProperty = sourceProperty;
                info.Editor = editor;
                info.Description = description;
                info.Group = group;
                result.Add(new PropertyWrapper(ControlledObject, info));
            }
            catch (Exception ex)
            {
                SystemHelper.logger.LogError(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName,
                System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.ToString());
            }

        }

    }
}

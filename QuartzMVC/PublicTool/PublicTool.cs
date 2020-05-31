using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using QuartzMVC.Job.Tool;
using QuartzMVC.Models;

namespace QuartzMVC.PublicTool
{
    public static class PublicTool
    {
        /// <summary>
        /// 自定义下拉框(把枚举封装成下拉框)
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">下拉框id名</param>
        /// <param name="obj">定义的样式</param>
        /// <param name="selectedVal">选中值</param>
        /// <param name="needDefault">是否有请选择</param>
        /// <returns></returns>
        public static MvcHtmlString IndustryDropDownList(
           this  HtmlHelper htmlHelper,
            string name,
            object obj,
            dynamic selectedVal = null,
             bool needDefault = true)
        {

            var industry = EnumToList<QuartzEnum>();
            var list = new List<SelectListItem>();
            if (needDefault)
            {
                list.Add(new SelectListItem()
                {
                    Value = "",
                    Text = @"===请选择==="
                });
            }
            list.AddRange(industry.Select(o => new SelectListItem { Text = o.EnumDisplay, Value = o.EnumName.ToString() }));
            if (selectedVal != null)
            {
                var item = list.Find(o => o.Value == selectedVal.ToString());
                if (item != null)
                    item.Selected = true;
            }
            return htmlHelper.DropDownList(name, list, obj);
        }

        /// <summary>
        /// 把枚举转换list 并获取对应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumberEntity> EnumToList<T>()
        {
            List<EnumberEntity> list = new List<EnumberEntity>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumberEntity m = new EnumberEntity();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    if (da != null) m.Desction = da.Description;
                }

                object[] objArr1 = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), true);
                if (objArr1.Length > 0)
                {
                    DisplayAttribute da = objArr1[0] as DisplayAttribute;
                    if (da != null) m.EnumDisplay = da.Name;
                }
                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }
            return list;
        }
    }
}
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;//数据转换库
using static FlatUI_TestPlatform.PubCls.MyDevice;

namespace FlatUI_TestPlatform.PubCls
{
    public static class MySys
    {
        //Structs
        //定义10种颜色盘结构体，用于颜色风格统一
        public struct RGBColors
        {
            public static Color color0 = Color.BlueViolet;//紫色
            public static Color color1 = Color.DodgerBlue;//蓝色
            public static Color color2 = Color.Pink;//粉丝
            public static Color color3 = Color.Chocolate;//浅红
            public static Color color4 = Color.CornflowerBlue;//蓝色
            public static Color color5 = Color.DarkOrange;//粉丝
            public static Color color6 = Color.LightSkyBlue;//淡蓝
            public static Color color7 = Color.SpringGreen;//绿色
            public static Color color8 = Color.Yellow;//黄色
            public static Color color9 = Color.Red;//红色
            public static Color color10 = Color.Aquamarine;//淡绿色
            public static Color color11 = Color.LightSalmon;//土粉色
            public static Color color12 = Color.DarkViolet;//紫色
            public static Color color13 = Color.SlateGray;//灰色
            public static Color color14 = Color.LavenderBlush;//乳白
            public static Color color15 = Color.Tomato;//西红柿
        }
        /// <summary>
        /// 模拟器
        /// </summary>
        public static Random rnd = new Random();
        public static double NextDouble(this Random ran, double minValue, double maxValue)
        {
            return ran.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static double NextDouble(this Random ran, double minValue, double maxValue, int decimalPlace)
        {
            double randNum = ran.NextDouble() * (maxValue - minValue) + minValue;
            return Convert.ToDouble(randNum.ToString("f" + decimalPlace));
        }
    }
}

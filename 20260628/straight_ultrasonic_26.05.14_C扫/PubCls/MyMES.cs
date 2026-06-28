using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatUI_TestPlatform.PubCls
{
    public  static class MyMES
    {
        public static MesInfoModel MesInfo;//MES交互结构体实例
        public static string MesJSON;//MES交互字符串实例
        public struct MesInfoModel
        {
            public string id;
            public string name;
            public int number;
            public bool xingbie;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.Profinet.Melsec;

namespace FlatUI_TestPlatform.PubCls
{
    public  class ClsFX3U
    {

        public static List<PLCItems> ItTable = new List<PLCItems>();

        /// <summary>
        /// 初始化交互内容列表,需要交互的数据要列在items中，更新Socket 状态
        /// </summary>
        /// <returns>bool类型</returns>
        public static bool ItTable_Ini()
        {
            try
            {
                /*
                 * 急停状态         D100 0    指示灯  0时灰色，1时红色，1时PC要关闭启动
                 * 启动状态         D100.1    指示灯  0时灰色，1时绿色，软件只有检测到此位为1时，才可以向控制器发出启动信号，否则伺服驱动器会报警。
                 * 自动控制         D100.5    指示灯  0时灰色，1时绿色
                 * 气压报警         D100.6    指示灯  0时绿色，1时红色
                 * 系统错误         D111.0    指示灯  0时绿色，1时红色
                 * PLC 心跳         M8012     指示灯  0时灰色，1时绿色，用来监控设备PLC处于工作状态
                 *     
                 * 自动流程步     D50     文本显示，PLC自动工作流程号
                 * PC准备好       D51     软件准备完毕后写1
                 * 对话PLC->PC    D52     PLC对PC的对话码 1：预压申请 3：申请开始试验 5：归零
                 * 对话PC->PLC    D53     PC对PLC的对话码 1：收到预压申请 2：预压完毕 3：收到开始试验 4：试验完毕 5：收到归零指令 6：归零完毕
                 * 批次号         D54     软件根据批次号+试验累加序号来存储试验数据

                 * 
                 * 以下内容为气缸手动控制部分，单独放置一个区域
                 * 上料气缸手动工作     D201.4  按钮控制    手动按钮控制去工作位，注意此位置1时，要把回原位置0
                 * 上料气缸手动回原位   D201.5  按钮控制    手动按钮控制回原位，注意此位置1时，要把去工作位置0
                 * 上料气缸工作超时     D203.3  警告提示
                 * 上料气缸回原位超时   D203.4  警告提示
                 * 上料气缸传感器错误   D203.5  警告提示
                 * 上料气缸工作位       D203.6  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 上料气缸原位         D203.7  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 
                 * 清扫气缸手动工作     D206.4  按钮控制    手动按钮控制去工作位，注意此位置1时，要把回原位置0
                 * 清扫气缸手动回原位   D206.5  按钮控制    手动按钮控制回原位，注意此位置1时，要把去工作位置0
                 * 清扫气缸工作超时     D208.3  警告提示
                 * 清扫气缸回原位超时   D208.4  警告提示
                 * 清扫气缸传感器错误   D208.5  警告提示
                 * 清扫气缸工作位       D208.6  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 清扫气缸原位         D208.7  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 
                 * 定位气缸手动工作     D211.4  按钮控制    手动按钮控制去工作位，注意此位置1时，要把回原位置0
                 * 定位气缸手动回原位   D211.5  按钮控制    手动按钮控制回原位，注意此位置1时，要把去工作位置0
                 * 定位气缸工作超时     D213.3  警告提示
                 * 定位气缸回原位超时   D213.4  警告提示
                 * 定位气缸传感器错误   D213.5  警告提示
                 * 定位气缸工作位       D213.6  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 定位气缸原位         D213.7  指示灯  用图片颜色显示状态，1时绿色，0时灰色
                 * 
                */
                ItTable.Add(new PLCItems { ItemName = "系统控制",           ItemAddress = "D100",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//0
                ItTable.Add(new PLCItems { ItemName = "系统错误",           ItemAddress = "D111",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//1
                ItTable.Add(new PLCItems { ItemName = "上料气缸_01",        ItemAddress = "D201",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//2
                ItTable.Add(new PLCItems { ItemName = "上料气缸_02",        ItemAddress = "D203",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//3
                ItTable.Add(new PLCItems { ItemName = "清扫气缸_01",        ItemAddress = "D206",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//4
                ItTable.Add(new PLCItems { ItemName = "清扫气缸_02",        ItemAddress = "D208",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//5
                ItTable.Add(new PLCItems { ItemName = "定位气缸_01",        ItemAddress = "D211",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//6 
                ItTable.Add(new PLCItems { ItemName = "定位气缸_02",        ItemAddress = "D213",   ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//7
                ItTable.Add(new PLCItems { ItemName = "自动流程步",         ItemAddress = "D50",    ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) }); //8
                ItTable.Add(new PLCItems { ItemName = "PC准备好",           ItemAddress = "D51",    ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//9
                ItTable.Add(new PLCItems { ItemName = "对话PLC->PC",        ItemAddress = "D52",    ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//10
                ItTable.Add(new PLCItems { ItemName = "对话PC->PLC",        ItemAddress = "D53",    ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//11
                ItTable.Add(new PLCItems { ItemName = "批次号",             ItemAddress = "D54",    ItemType = PLCItemType.INT16, ItemValue = Convert.ToString(0) });//12

                ItTable.Add(new PLCItems { ItemName = "PLC心跳",            ItemAddress = "M8012",  ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "上料气缸手动工作",   ItemAddress = "M410",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "上料气缸手动回原位", ItemAddress = "M411",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "清扫气缸手动工作",   ItemAddress = "M412",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "清扫气缸手动回原位", ItemAddress = "M413",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "定位气缸手动工作",   ItemAddress = "M414",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13
                ItTable.Add(new PLCItems { ItemName = "定位气缸手动回原位", ItemAddress = "M415",   ItemType = PLCItemType.BOOL,  ItemValue = Convert.ToString(0) });//13

                SocketState = "Loaded";//接口状态：加载列表完毕
                return true;
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("与PLC对话列表初始化错误！", ex);
                return false;
            }
        }

        #region 功能函数
        //FX3U 波特率：9600 7位数据位，1位停止位
        public static bool Connect(string PortName, int BaudRate,int DataBits)
        {
            try
            {
                if (Socket == null)
                {
                    Socket = new MelsecFxSerial();
                }
                Socket.SerialPortInni(PortName, BaudRate, DataBits, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                if (!Connected && SocketState != "Connecting")
                {
                    SocketState = "Connecting";
                    Connected = false;
                    Socket.Open();
                    if (Socket.IsOpen())
                    {
                        SocketState = "Connected";
                        Connected = true;
                        EnableMonitor = true;
                    }
                    else
                    {
                        SocketState = "ConnectFailed";
                        Connected = false;
                        EnableMonitor = false;
                    }
                }
                return _Socket.IsOpen();
            }
            catch (Exception ex)
            {
                SocketState = "ConnectFailed";
                Connected = false;
                EnableMonitor = false;
                MyLogNetFile.logNet.WriteException("链接PLC时出现异常！",ex);
                return _Socket.IsOpen();
            }
        }

        /// <summary>
        /// 根据列表项名称来读取PLC单个数据
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static object Read(string itemName)
        {
            try
            {
                if (!Connected)
                {
                    return null;
                }
                var Item = GetItems(itemName);
                switch (Item.ItemType)
                {
                    case PLCItemType.BOOL:
                        {
                            HslCommunication.OperateResult<bool> Result = Socket.ReadBool(Item.ItemAddress);
                            if (Result.IsSuccess)
                            {
                                return Result.Content;
                            }
                            else
                            {
                                MyLogNetFile.logNet.WriteError(string.Format("读取PLC中BOOL类型变量失败！失败原因{0}", Result.Message), "错误信息");
                                return false;
                            }
                        }
                    case PLCItemType.FLOAT:
                        {
                            HslCommunication.OperateResult<float> Result = Socket.ReadFloat(Item.ItemAddress);
                            if (Result.IsSuccess)
                            {
                                return Result.Content;
                            }
                            else
                            {
                                MyLogNetFile.logNet.WriteError(string.Format("读取PLC中Float类型变量失败！失败原因{0}", Result.Message), "错误信息");
                                return false;
                            }
                        }
                    case PLCItemType.INT16:
                        {
                            HslCommunication.OperateResult<UInt16> Result = Socket.ReadUInt16(Item.ItemAddress);
                            if (Result.IsSuccess)
                            {
                                return Result.Content;
                            }
                            else
                            {
                                MyLogNetFile.logNet.WriteError(string.Format("读取PLC中INT16类型变量失败！失败原因{0}", Result.Message), "错误信息");
                                return false;
                            }
                        }
                    case PLCItemType.INT32:
                        {
                            HslCommunication.OperateResult<Int32> Result = Socket.ReadInt32(Item.ItemAddress);
                            if (Result.IsSuccess)
                            {
                                return Result.Content;
                            }
                            else
                            {
                                MyLogNetFile.logNet.WriteError(string.Format("读取PLC中INT32类型变量失败！失败原因{0}", Result.Message), "错误信息");
                                return false;
                            }
                        }
                    default:
                        {
                            MyLogNetFile.logNet.WriteError("读取PLC中不存在的数据类型！", "错误信息");
                            return null;
                        }
                }
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("根据名称从PLC读取单个数据时出现异常！",ex );
                return null;
            }
        }

        /// <summary>
        /// 批量读字节数据,必须定义为静态，未链接时，返回值为NULL
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">要读取数据长度</param>
        /// <returns>Byte类型字节数组</returns>
        public static byte[] MutiRead(string startAddress, ushort length )
        {
            try
            {
                OperateResult<byte[]> read = Socket.Read(startAddress, length);
                if (read.IsSuccess)
                {
                    return read.Content;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("从PLC批量读取数据时异常！",ex);
                return null;
            }
        }

        /// <summary>
        /// 根据列表中名字写入数据
        /// </summary>
        /// <param name="itemName">列表中名字</param>
        /// <param name="value">写入的值</param>
        /// <returns>bool类型，是否成功</returns>
        public async static void Write(string itemName, object value)
        {
            try
            {
                OperateResult Result = new OperateResult();
                var Item = GetItems(itemName);//从列表中获取对应内容
                if (Item == null)
                {
                    //return false;
                }
                switch (Item.ItemType)
                {
                    case PLCItemType.BOOL:
                        {
                            var tempWriteValue = bool.Parse(value.ToString());
                            Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                    case PLCItemType.FLOAT:
                        {
                            var tempWriteValue = float.Parse(value.ToString());
                            Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                    case PLCItemType.INT16:
                        {
                            var tempWriteValue = Int16.Parse(value.ToString());
                            Result = await Socket.WriteAsync(Item.ItemAddress, tempWriteValue);
                            //Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                    case PLCItemType.INT32:
                        {
                            var tempWriteValue = Int32.Parse(value.ToString());
                            Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                    case PLCItemType.STRING:
                        {
                            var tempWriteValue = value.ToString();
                            Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                    case PLCItemType.BYTE:
                        {
                            var tempWriteValue = byte.Parse(value.ToString());
                            Result = Socket.Write(Item.ItemAddress, tempWriteValue);
                            break;
                        }
                }
                //转换位字节BCB编码
                //byte[] valuebybyte = HslCommunication.BasicFramework.SoftBasic.HexStringToBytes(value.ToString());
                if (!Result.IsSuccess)
                {
                    MyLogNetFile.logNet.WriteError(string.Format("写入PLC数据失败！失败原因{0}", Result.Message), "错误信息");
                    //return false;
                }
                else
                {
                    //return true;
                }
            }
            catch (Exception ex)
            {
                MyLogNetFile.logNet.WriteException("向PLC写入操作异常！",ex);
                //return false;
            }
        }

        //释放端口
        public static void Dispose()
        {
            EnableMonitor = false;
            Connected = false;
            if (Socket != null)
            {
                Socket.Close();
                //Socket = null;
            }
        }

        //连接复位
        public static void Reset(string PortName, int BaudRate, int DataBits)
        {
            if (Socket != null)
            {
                Socket.Close();
                Connected = false;
                EnableMonitor = false;
                System.Threading.Thread.Sleep(100);
                Connect(PortName, BaudRate, DataBits);
            }
        }

        //根据名称获取一条内容
        static PLCItems GetItems(string itemName)
        {
            return ItTable.FirstOrDefault(p => p.ItemName == itemName);
        }
        #endregion

        #region 属性&&字段
        public static MelsecFxSerial _Socket;
        /// <summary>
        /// PLC实例
        /// </summary>
        public static MelsecFxSerial Socket
        {
            get { return _Socket; }
            set { _Socket = value; }
        }

        static List<PLCItems> _Items;
        public static List<PLCItems> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        //链接成功，可以进行监控
        static bool _EnableMonitor;
        public static bool EnableMonitor
        {
            get { return _EnableMonitor; }
            set { _EnableMonitor = value; }
        }
        public static string _State;
        public static string SocketState
        {
            get { return _State; }
            set{_State = value;}
        }
        public static bool _Connected;
        public static bool Connected
        {
            get { return _Connected; }
            set { _Connected = value;}
        }
        #endregion

        //读取内容结构类
        public class PLCItems
        {
            string _itemvalue;
            public string ItemName//名字
            {
                get;
                set;
            }
            public string ItemAddress//地址
            {
                get;
                set;
            }
            public PLCItemType ItemType//数据类型
            {
                get;
                set;
            }
            public string ItemValue
            {
                get { return _itemvalue; }
                set { _itemvalue = value; }
            }
        }

        //读写内容数据类型
        public enum PLCItemType
        {
            INT16,
            INT32,
            INT64,
            BOOL,
            STRING,
            FLOAT,
            BYTE
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SmartStudio.Cls
{
    public static class ClsData
    {
        public static PLC_DB_r plc_db_r;//实例读PLC通讯数据区
        public static PLC_DB_w plc_db_w;//写
        public static Data_Result_UpDown data_result_UpDown;//升降结果数据
        public static Data_Result_Sliding data_result_Sliding;//滑动结果数据
        public static Current_Max_Min current_max_min_p, current_max_min_r;//电流峰谷值
        public static string StrMessageBox;//主窗体下信息框

        public struct PLC_DB_r//PLC->PC
        {
            public int PLCtP_Cheartbeat;//PC心跳
            public int PLCtPC_Mode;//0：手动 1：自动
            public int PLCtPC_Dialogue;//自动时对话
            public int PLCtPC_AutoFlowStep;//自动时流程步

            //MES：deviceRunningInfo
            public int deviceNo;//设备编号
            public int deciveStatus;//0：待机 1、工作中 2：异常 3:关机
            public int details;//异常信息描述编号
            public int deviceRecipe;//当前配方编号
            public int result;//0：默认 1：合格 2：不合格
            public float resultValue;//结果值
            public int numOK;//OK
            public int numNG;//NG
            public int numSum;//合计
        }

        public struct PLC_DB_w//PC->PLC
        {
            public int PCtPLC_heartbeat { get; set; }//PC心跳
            public int PCtPLC_reciptNo { get; set; }//配方编号，如每套配方动作不同 
            public int PCtPLC_reciptP1;//参数描述
            public int PCtPLC_reciptP2;//参数描述
            public int PCtPLC_reciptP3;//参数描述
            public int PCtPLC_reciptP4;//参数描述
            public int PCtPLC_reciptP5;//参数描述
            public int PCtPLC_reciptP6;//参数描述
            public int PCtPLC_reciptP7;//参数描述
            public int PCtPLC_reciptP8;//参数描述
            public int PCtPLC_reciptP9;//参数描述
            public int PCtPLC_reciptP10;//参数描述
            public int PCtPLC_Dialogue;//自动时对话
        }
        public struct Data_Result_UpDown//升降试验结果
        {
            public string TestDataTime;
            public string TestName;
            public string TestNumber;
            public string TestModel;
            public string TesterNumber;
            public string WorkingVoltage;
            public string WorkingCurrent;
            public string WorkingPower;
            public string WorkingPowerFactor;
            public string MaxPoint;
            public string MinPoint;
            public string Distance;
            public string Time;//调节时间
            public string Speed;
            public string Load;
            public string ClutchForce;
            public string WorkingNCurrent;
            public string WorkingDCCurrent;
            public string WorkingDCVoltage;
            public string MaxCurrentA;
            public string MinCurrentA;
            public string MaxCurrentB;
            public string MinCurrentB;
            public string MaxCurrentC;
            public string MinCurrentC;
            public string MaxCurrentN;
            public string MinCurrentN;
            public string MaxCurrentDC;
            public string MinCurrentDC;
            public string MaxLoad_hold3;//3s保持结果
            public string MaxLoad_hold30;//30s保持结果

        }

        public struct Data_Result_Sliding//滑动试验结果
        {
            public string TestDataTime;
            public string TestName;
            public string TestNumber;
            public string TestModel;
            public string TesterNumber;
            public string WorkingVoltage;
            public string WorkingCurrent;
            public string WorkingPower;
            public string WorkingPowerFactor;
            public string Speed;
            public string Torque;
            public string Time;
            public string ClutchForce;
            public string WorkingNCurrent;
            public string WorkingDCCurrent;
            public string WorkingDCVoltage;
            public string MaxCurrentA;
            public string MinCurrentA;
            public string MaxCurrentB;
            public string MinCurrentB;
            public string MaxCurrentC;
            public string MinCurrentC;
            public string MaxCurrentN;
            public string MinCurrentN;
            public string MaxCurrentDC;
            public string MinCurrentDC;
            public string MaxLoad_hold3;//3s保持结果
            public string MaxLoad_hold30;//30s保持结果

        }

        public struct Current_Max_Min//电流峰谷值
        {
            public double MaxCurrentA;
            public double MinCurrentA;
            public double MaxCurrentB;
            public double MinCurrentB;
            public double MaxCurrentC;
            public double MinCurrentC;
            public double MaxCurrentN;
            public double MinCurrentN;
            public double MaxCurrentDC;
            public double MinCurrentDC;
        }
    }
}

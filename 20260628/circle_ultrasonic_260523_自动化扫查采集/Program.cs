using FlatUI_TestPlatform.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WizardOEMPA;

namespace FlatUI_TestPlatform
{
    internal static class Program
    {
        public static bool HslCommunicationIsActive { get; private set; }
        public static bool HslControlIsActive { get; private set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 通过线程互斥锁来防止程序被多次执行
            bool isAppRunning = false;
            Mutex mutex = new Mutex(true, System.Diagnostics.Process.GetCurrentProcess().ProcessName, out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("程序已运行，不能再次打开！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(1);
            }
            #endregion

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 授权HslCommunication
            if (!HslCommunication.Authorization.SetAuthorizationCode("d1c914f5-2159-49f0-9c7e-2a6c5cd29e55"))
            {
                MessageBox.Show("'HslCommunication'授权失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HslCommunicationIsActive = false;
                return;
            }
            else
            {
                HslCommunicationIsActive = true;
            }

            // 授权HslControls
            if (!HslControls.Authorization.SetAuthorizationCode("40e2fd08-7df9-4892-9700-09cf1a0d3d7c"))
            {
                MessageBox.Show("'HslControl'授权失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HslControlIsActive = false;
                return;
            }
            else
            {
                HslControlIsActive = true;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.ThreadPool.SetMaxThreads(2000, 800);
            Application.Run(new FormMain());
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            System.IO.File.WriteAllText("ExceptionMessage.txt", HslCommunication.BasicFramework.SoftBasic.GetExceptionMessage(ex));
        }
    }

    class IniFile
    {
        public string Path;
        static string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath != null ? IniPath : EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            StringBuilder RetVal = new StringBuilder(255);
            string value;
            GetPrivateProfileString(Section != null ? Section : EXE, Key, "!@#$%^&*()", RetVal, 255, Path);
            value = RetVal.ToString();
            if (value == "!@#$%^&*()")
                return null;
            return value;
        }

        public void Write(string Key, string Value)
        {
            WritePrivateProfileString(EXE, Key, Value, Path);
        }

        public void Write(string Key, string Section, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        public void WriteDepthMode(string Key, string Section, csEnumDepthMode DepthMode)
        {
            string Value;

            if (DepthMode == csEnumDepthMode.csTrueDepth)
            {
                Value = "TrueDepth";
                WritePrivateProfileString(Section, Key, Value, Path);
            }
            if (DepthMode == csEnumDepthMode.csSoundPath)
            {
                Value = "SoundPath";
                WritePrivateProfileString(Section, Key, Value, Path);
            }
            if (DepthMode == csEnumDepthMode.csTrueDepthBigBar)
            {
                Value = "TrueDepthBigBar";
                WritePrivateProfileString(Section, Key, Value, Path);
            }
        }

        public void Write(string Key, string Section, bool Value)
        {
            string strValue;

            if (Value)
                strValue = "1";
            else
                strValue = "0";
            WritePrivateProfileString(Section, Key, strValue, Path);
        }

        public void Write(string Key, string Section, int Value)
        {
            string strValue;
            bool bRet;

            try
            {
                strValue = Value.ToString(CultureInfo.InvariantCulture);
                bRet = true;
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                strValue = "";
                bRet = false;
            }
            if (bRet)
                WritePrivateProfileString(Section, Key, strValue, Path);
        }

        public void Write(string Key, string Section, double Value, double factor = 1.0, string Unit = null)
        {
            string strValue;
            double data = Value * factor;
            bool bRet;

            try
            {
                strValue = data.ToString(CultureInfo.InvariantCulture);
                bRet = true;
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                strValue = "";
                bRet = false;
            }
            if (bRet)
            {
                if (Unit != null)
                    strValue = strValue + " " + Unit;
                WritePrivateProfileString(Section, Key, strValue, Path);
            }
        }
        public void Write(string Key, string Section, double Value, string Unit = null)
        {
            Write(Key, Section, Value, 1.0, Unit);
        }

        public void aWrite(string Key, string Section, double[] Value, double factor = 1.0, string Unit = null)
        {
            string strValue;
            bool bRet;
            double data;
            int count = Value.Length;
            int iIndex = 0;

            if (Value == null)
            {
                WritePrivateProfileString(Section, Key, "", Path);
                return;
            }
            bRet = true;
            strValue = "";
            count = Value.Length;
            foreach (double value in Value)
            {
                data = value * factor;
                try
                {
                    strValue = strValue + data.ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Unreferenced.Parameter(ex);
                    strValue = "";
                    bRet = false;
                }
                iIndex++;
                if (iIndex < count)
                    strValue = strValue + ";";
            }
            if (bRet)
            {
                if (Unit != null)
                    strValue = strValue + " " + Unit;
                WritePrivateProfileString(Section, Key, strValue, Path);
            }
        }
        public void aWrite(string Key, string Section, double[] Value, string Unit = null)
        {
            aWrite(Key, Section, Value, 1.0, Unit);
        }

        public void Write(string Key, string Section, int Value, string[] Values)
        {
            if (Value < 0)
                return;
            if (Value >= Values.Length)
                return;
            WritePrivateProfileString(Section, Key, Values[Value], Path);
        }

        public bool Read(string Key, string Section, out bool Value)
        {
            string strValue;

            Value = false;
            strValue = Read(Key, Section);
            if (strValue == null)
                return false;
            if (strValue == "true")
                Value = true;
            else if (strValue == "1")
                Value = true;
            else if (strValue == "false")
                Value = false;
            else if (strValue == "0")
                Value = false;
            else
                return false;
            return true;
        }

        public bool Read(string Key, string Section, out int Value)
        {
            string strValue;
            bool bRet;

            Value = 0;
            strValue = Read(Key, Section);
            if (strValue == null)
                return false;
            try
            {
                Value = Convert.ToInt32(strValue, CultureInfo.InvariantCulture);
                bRet = true;
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                bRet = false;
            }
            return bRet;
        }

        public bool ReadDepthMode(double dSpecimenRadius, string Key, string Section, out csEnumDepthMode Value)
        {
            string strValue;

            strValue = Read(Key, Section);
            if (strValue == null)
            {
                Value = csEnumDepthMode.csTrueDepth;
                return false;
            }
            Value = csEnumDepthMode.csTrueDepth;
            if (strValue == "TrueDepth")
                return true;
            if (strValue == "SoundPath")
            {
                Value = csEnumDepthMode.csSoundPath;
                return true;
            }
            if (strValue == "TrueDepthBigBar")
            {
                if (dSpecimenRadius > 0.0)
                    Value = csEnumDepthMode.csTrueDepthBigBar;
                return true;
            }
            return false;
        }

        public bool Read(string Key, string Section, out double Value, double factor)
        {
            string strValue;
            string[] list;
            bool bRet;

            Value = 0.0;
            strValue = Read(Key, Section);
            if (strValue == null)
                return false;
            list = strValue.Split(' ');
            if (list.Length > 0)
                strValue = list[0];
            try
            {
                Value = Convert.ToDouble(strValue, CultureInfo.InvariantCulture);
                Value = Value * factor;
                bRet = true;
            }
            catch (Exception ex)
            {
                Unreferenced.Parameter(ex);
                bRet = false;
            }
            return bRet;
        }
        public bool Read(string Key, string Section, out double Value)
        {
            return Read(Key, Section, out Value, 1.0);
        }

        public bool aRead(string Key, string Section, out double[] Value, double factor = 1.0)
        {
            string strValue, value;
            string[] list, list2;
            List<double> dataList = new List<double>();
            bool bRet;
            double data;
            int count;
            int iIndex = 0;

            bRet = true;
            Value = null;
            strValue = Read(Key, Section);
            if (strValue == null)
                return false;
            list = strValue.Split(';');
            count = list.Length;
            foreach (string value2 in list)
            {
                value = value2;
                if (iIndex == count - 1)
                {
                    list2 = value.Split(' ');
                    if (list2.Length > 0)
                        value = list2[0];
                }
                try
                {
                    data = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                    data = data * factor;
                    dataList.Add(data);
                }
                catch (Exception ex)
                {
                    Unreferenced.Parameter(ex);
                    bRet = false;
                }
                iIndex++;
            }
            if (!bRet)
                return false;
            iIndex = 0;
            Value = new double[dataList.Count];
            foreach (double data2 in dataList)
            {
                Value[iIndex] = data2;
                iIndex++;
            }
            return true;
        }

        public bool Read(string Key, string Section, out int Value, string[] Values)
        {
            string strValue;
            int iIndex = 0;

            Value = 0;
            strValue = Read(Key, Section);
            if (strValue == null)
                return false;
            foreach (string value in Values)
            {
                if (strValue == value)
                {
                    Value = iIndex;
                    return true;
                }
                iIndex++;
            }
            return false;
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section != null ? Section : EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section != null ? Section : EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0 ? true : false;
        }

        private void TemplateWriteCscan(int index, csWizardGateCscan wizardGateCscan)
        {
            string enable, start, range, threshold, rectification, modeAmp, modeTof;

            enable = "Enable[" + index.ToString() + "]";
            start = "Start[" + index.ToString() + "]";
            range = "Range[" + index.ToString() + "]";
            threshold = "Threshold[" + index.ToString() + "]";
            rectification = "Rectification[" + index.ToString() + "]";
            modeAmp = "ModeAmp[" + index.ToString() + "]";
            modeTof = "ModeTof[" + index.ToString() + "]";
            if ((wizardGateCscan != null) && wizardGateCscan.Enable)
            {
                //Write(enable, "Cscan", wizardGateCscan.Enable);
                Write(start, "Cscan", wizardGateCscan.Start, 1.0e6, "us");
                Write(range, "Cscan", wizardGateCscan.Range, 1.0e6, "us");
                Write(threshold, "Cscan", wizardGateCscan.Threshold, "%");
                Write(rectification, "Cscan", (int)wizardGateCscan.Rectification, csWizardGateCscan.Rectifications);
                Write(modeAmp, "Cscan", (int)wizardGateCscan.ModeAmplitude, csWizardGateCscan.GateModeAmps);
                Write(modeTof, "Cscan", (int)wizardGateCscan.ModeTimeOfFlight, csWizardGateCscan.GateModeTofs);
            }
            else
            {
                Write(enable, "Cscan", wizardGateCscan.Enable);
                Write(start, "Cscan", 0.0e-6, 1.0e6, "us");
                Write(range, "Cscan", 1.0e-6, 1.0e6, "us");
                Write(threshold, "Cscan", 50.0, "%");
                Write(rectification, "Cscan", 0, csWizardGateCscan.Rectifications);
                Write(modeAmp, "Cscan", 0, csWizardGateCscan.GateModeAmps);
                Write(modeTof, "Cscan", 0, csWizardGateCscan.GateModeTofs);
            }
        }
        private bool TemplateReadCscan(int index, ref csWizardGateCscan wizardGateCscan)
        {
            double data;
            int idata;
            bool bRet = true;
            string enable, start, range, threshold, rectification, modeAmp, modeTof;

            enable = "Enable[" + index.ToString() + "]";
            start = "Start[" + index.ToString() + "]";
            range = "Range[" + index.ToString() + "]";
            threshold = "Threshold[" + index.ToString() + "]";
            rectification = "Rectification[" + index.ToString() + "]";
            modeAmp = "ModeAmp[" + index.ToString() + "]";
            modeTof = "ModeTof[" + index.ToString() + "]";
            wizardGateCscan.Init();
            if (Read(start, "Cscan", out data, 1.0e-6))
                wizardGateCscan.Start = data;
            else
                bRet = false;
            if (Read(range, "Cscan", out data, 1.0e-6))
                wizardGateCscan.Stop = wizardGateCscan.Start + data;
            else
                bRet = false;
            if (Read(threshold, "Cscan", out data))
                wizardGateCscan.Threshold = data;
            else
                bRet = false;
            if (Read(rectification, "Cscan", out idata, csWizardGateCscan.Rectifications))
                wizardGateCscan.Rectification = (csRectification)idata;
            else
                bRet = false;
            if (Read(modeAmp, "Cscan", out idata, csWizardGateCscan.GateModeAmps))
                wizardGateCscan.ModeAmplitude = (csGateModeAmp)idata;
            else
                bRet = false;
            if (Read(modeTof, "Cscan", out idata, csWizardGateCscan.GateModeTofs))
                wizardGateCscan.ModeTimeOfFlight = (csGateModeTof)idata;
            else
                bRet = false;
            return bRet;
        }
        public bool TemplateWrite(csWizardTemplate wizardTemplate)
        {
            double data;
            data = wizardTemplate.Specimen.Velocity;
            Write("Velocity", "Specimen", wizardTemplate.Specimen.Velocity, "m/s");
            if (wizardTemplate.Specimen.Wave == csWave.csLongitudinal)
                Write("Wave", "Specimen", "L");
            else
                Write("Wave", "Specimen", "T");
            Write("Radius", "Specimen", wizardTemplate.Specimen.Radius, 1.0e3, "mm");
            if (wizardTemplate.Wedge.Enable)
                Write("Enable", "Wedge", "1");
            else
                Write("Enable", "Wedge", "0");
            Write("Velocity", "Wedge", wizardTemplate.Wedge.Velocity, "m/s");
            Write("Angle", "Wedge", wizardTemplate.Wedge.Angle, 180.0 / Math.PI, "deg");
            Write("Height", "Wedge", wizardTemplate.Wedge.Height, 1.0e3, "mm");//1.1.3.2c "deg");
            Write("ElementOffset", "Probe", wizardTemplate.Probe.ElementOffset);
            Write("ElementCount", "Probe", wizardTemplate.Probe.ElementCount);
            Write("Pitch", "Probe", wizardTemplate.Probe.Pitch, 1.0e3, "mm");
            Write("Frequency", "Probe", wizardTemplate.Probe.Frequency, 1.0e-6, "MHz");
            Write("Radius", "Probe", wizardTemplate.Probe.Radius, 1.0e3, "mm");
            if (wizardTemplate.Scan.Linear)
            {
                Write("Count", "Scan\\Linear", wizardTemplate.Scan.ElementCount);
                Write("DepthEmission", "Scan\\Linear", wizardTemplate.Scan.DepthEmission, 1.0e3, "mm");
                aWrite("DepthReception", "Scan\\Linear", wizardTemplate.Scan.DepthReception, 1.0e3, "mm");
                Write("Angle", "Scan\\Linear", wizardTemplate.Scan.AngleStart, 180.0 / Math.PI, "deg");
                Write("ElementStart", "Scan\\Linear", wizardTemplate.Scan.ElementStart + 1);
                Write("ElementStop", "Scan\\Linear", wizardTemplate.Scan.ElementStop + 1);
                Write("ElementStep", "Scan\\Linear", wizardTemplate.Scan.ElementStep);
                WriteDepthMode("DepthMode", "Scan\\Linear", wizardTemplate.Scan.DepthMode);
            }
            else
            {
                Write("Count", "Scan\\Sector", wizardTemplate.Scan.ElementCount);
                Write("DepthEmission", "Scan\\Sector", wizardTemplate.Scan.DepthEmission, 1.0e3, "mm");
                aWrite("DepthReception", "Scan\\Sector", wizardTemplate.Scan.DepthReception, 1.0e3, "mm");
                Write("AngleStart", "Scan\\Sector", wizardTemplate.Scan.AngleStart, 180.0 / Math.PI, "deg");
                Write("AngleStop", "Scan\\Sector", wizardTemplate.Scan.AngleStop, 180.0 / Math.PI, "deg");
                Write("AngleStep", "Scan\\Sector", wizardTemplate.Scan.AngleStep, 180.0 / Math.PI, "deg");
                Write("ElementStart", "Scan\\Sector", wizardTemplate.Scan.ElementStart + 1);
                WriteDepthMode("DepthMode", "Scan\\Sector", wizardTemplate.Scan.DepthMode);
            }
            Write("Start", "Ascan", wizardTemplate.GateAscan.Start, 1.0e6, "us");
            Write("Range", "Ascan", wizardTemplate.GateAscan.Range, 1.0e6, "us");
            Write("TimeSlot", "Ascan", wizardTemplate.GateAscan.TimeSlot, 1.0e6, "us");
            Write("Count", "Cscan", 1);//wizardTemplate.aGateCscan.Length);
            Write("Start[0]", "Cscan", 0.0, 1e6, "us");
            Write("Range[0]", "Cscan", 1.0e-6, 1e6, "us");
            Write("Threshold[0]", "Cscan", 50.0, 1.0, "%");
            Write("Rectification[0]", "Cscan", "Signed");
            Write("ModeAmp[0]", "Cscan", "Absolute");
            Write("ModeTof[0]", "Cscan", "AmplitudeDetection");
            for (int index = 0; index < 4; index++)
            {
                if (wizardTemplate.aGateCscan.Length > 1)
                    TemplateWriteCscan(index, wizardTemplate.aGateCscan[index]);
                else
                    TemplateWriteCscan(index, null);
            }
            return true;
        }

        public bool TemplateWrite(csWizardPitchCatchTemplate wizardTemplate)
        {
            bool bCycleByCycle = false;
            Write("Velocity", "Specimen", wizardTemplate.Specimen.Velocity, "m/s");
            if (wizardTemplate.Specimen.Wave == csWave.csLongitudinal)
                Write("Wave", "Specimen", "L");
            else
                Write("Wave", "Specimen", "T");
            Write("Radius", "Specimen", wizardTemplate.Specimen.Radius, 1.0e3, "mm");
            if (wizardTemplate.Wedge.Enable)
                Write("Enable", "Wedge", "1");
            else
                Write("Enable", "Wedge", "0");
            Write("Velocity", "Wedge", wizardTemplate.Wedge.Velocity, "m/s");
            Write("Angle", "Wedge", wizardTemplate.Wedge.Angle, 180.0 / Math.PI, "deg");
            Write("Height", "Wedge", wizardTemplate.Wedge.Height, 1.0e3, "mm");//1.1.3.2c "deg");
            Write("ElementOffset", "Probe", wizardTemplate.Probe.ElementOffset);
            Write("ElementCount", "Probe", wizardTemplate.Probe.ElementCount);
            Write("Pitch", "Probe", wizardTemplate.Probe.Pitch, 1.0e3, "mm");
            Write("Frequency", "Probe", wizardTemplate.Probe.Frequency, 1.0e-6, "MHz");
            Write("Radius", "Probe", wizardTemplate.Probe.Radius, 1.0e3, "mm");
            switch (wizardTemplate.Scan.PitchCatchDefinition)
            {
                case csEnumPitchCatchDefinition.csLinear:
                    Write("CountEmission", "Scan\\Linear", wizardTemplate.Scan.ElementCountEmission);
                    Write("CountReception", "Scan\\Linear", wizardTemplate.Scan.ElementCountReception);
                    Write("DepthEmission", "Scan\\Linear", wizardTemplate.Scan.DepthEmission, 1.0e3, "mm");
                    aWrite("DepthReception", "Scan\\Linear", wizardTemplate.Scan.DepthReception, 1.0e3, "mm");
                    Write("AngleEmission", "Scan\\Linear", wizardTemplate.Scan.AngleStartEmission, 180.0 / Math.PI, "deg");
                    Write("AngleReception", "Scan\\Linear", wizardTemplate.Scan.AngleStartReception, 180.0 / Math.PI, "deg");
                    Write("ElementStartEmission", "Scan\\Linear", wizardTemplate.Scan.ElementStartEmission + 1);
                    Write("ElementStartReception", "Scan\\Linear", wizardTemplate.Scan.ElementStartEmission + 1);
                    Write("ElementStopEmission", "Scan\\Linear", wizardTemplate.Scan.ElementStopEmission + 1);
                    Write("ElementStopReception", "Scan\\Linear", wizardTemplate.Scan.ElementStopReception + 1);
                    Write("ElementStepEmission", "Scan\\Linear", wizardTemplate.Scan.ElementStepEmission);
                    Write("ElementStepReception", "Scan\\Linear", wizardTemplate.Scan.ElementStepReception);
                    WriteDepthMode("DepthModeEmission", "Scan\\Linear", wizardTemplate.Scan.DepthModeEmission);
                    WriteDepthMode("DepthModeReception", "Scan\\Linear", wizardTemplate.Scan.DepthModeReception);
                    break;

                case csEnumPitchCatchDefinition.csSector:
                    Write("CountEmission", "Scan\\Sector", wizardTemplate.Scan.ElementCountEmission);
                    Write("CountReception", "Scan\\Sector", wizardTemplate.Scan.ElementCountReception);
                    Write("DepthEmission", "Scan\\Sector", wizardTemplate.Scan.DepthEmission, 1.0e3, "mm");
                    aWrite("DepthReception", "Scan\\Sector", wizardTemplate.Scan.DepthReception, 1.0e3, "mm");
                    Write("AngleStartEmission", "Scan\\Sector", wizardTemplate.Scan.AngleStartEmission, 180.0 / Math.PI, "deg");
                    Write("AngleStartReception", "Scan\\Sector", wizardTemplate.Scan.AngleStartReception, 180.0 / Math.PI, "deg");
                    Write("AngleStopEmission", "Scan\\Sector", wizardTemplate.Scan.AngleStopEmission, 180.0 / Math.PI, "deg");
                    Write("AngleStopReception", "Scan\\Sector", wizardTemplate.Scan.AngleStopReception, 180.0 / Math.PI, "deg");
                    Write("AngleStepEmission", "Scan\\Sector", wizardTemplate.Scan.AngleStepEmission, 180.0 / Math.PI, "deg");
                    Write("AngleStepReception", "Scan\\Sector", wizardTemplate.Scan.AngleStepReception, 180.0 / Math.PI, "deg");
                    Write("ElementStartEmission", "Scan\\Sector", wizardTemplate.Scan.ElementStartEmission + 1);
                    Write("ElementStartReception", "Scan\\Sector", wizardTemplate.Scan.ElementStartReception + 1);
                    WriteDepthMode("DepthModeEmission", "Scan\\Sector", wizardTemplate.Scan.DepthModeEmission);
                    WriteDepthMode("DepthModeReception", "Scan\\Sector", wizardTemplate.Scan.DepthModeReception);
                    break;

                case csEnumPitchCatchDefinition.csCycleByCycle:
                    bCycleByCycle = true;
                    break;

            }
            Write("Start", "Ascan", wizardTemplate.GateAscan.Start, 1.0e6, "us");
            Write("Range", "Ascan", wizardTemplate.GateAscan.Range, 1.0e6, "us");
            Write("TimeSlot", "Ascan", wizardTemplate.GateAscan.TimeSlot, 1.0e6, "us");
            Write("Count", "Cscan", 1);//wizardTemplate.aGateCscan.Length);
            Write("Start[0]", "Cscan", 0.0, 1e6, "us");
            Write("Range[0]", "Cscan", 1.0e-6, 1e6, "us");
            Write("Threshold[0]", "Cscan", 50.0, 1.0, "%");
            Write("Rectification[0]", "Cscan", "Signed");
            Write("ModeAmp[0]", "Cscan", "Absolute");
            Write("ModeTof[0]", "Cscan", "AmplitudeDetection");
            if (bCycleByCycle)
            {
                if (wizardTemplate.Scan.Cycles == null)
                {
                    wizardTemplate.Scan.SetScanCount(1);
                }
                Write("CycleCount", "Cycles", wizardTemplate.Scan.Cycles.Length);
                for (int i = 0; i < wizardTemplate.Scan.Cycles.Length; i++)
                {
                    Write("Angle", "Cycle:" + i + "\\Pulser", wizardTemplate.Scan.Cycles[i].AngleEmission, 180.0 / Math.PI, "deg");
                    Write("Depth", "Cycle:" + i + "\\Pulser", wizardTemplate.Scan.Cycles[i].DepthEmission, 1.0e3, "mm");
                    Write("Count", "Cycle:" + i + "\\Pulser", wizardTemplate.Scan.Cycles[i].ElementCountEmission);
                    Write("ElementStart", "Cycle:" + i + "\\Pulser", wizardTemplate.Scan.Cycles[i].ElementStartEmission + 1);
                    WriteDepthMode("DepthMode", "Cycle:" + i + "\\Pulser", wizardTemplate.Scan.Cycles[i].DepthModeEmission);
                    Write("Angle", "Cycle:" + i + "\\Reception", wizardTemplate.Scan.Cycles[i].AngleReception, 180.0 / Math.PI, "deg");
                    aWrite("Depth", "Cycle:" + i + "\\Reception", wizardTemplate.Scan.Cycles[i].DepthReception, 1.0e3, "mm");
                    Write("Count", "Cycle:" + i + "\\Reception", wizardTemplate.Scan.Cycles[i].ElementCountReception);
                    Write("ElementStart", "Cycle:" + i + "\\Reception", wizardTemplate.Scan.Cycles[i].ElementStartReception + 1);
                    WriteDepthMode("DepthMode", "Cycle:" + i + "\\Reception", wizardTemplate.Scan.Cycles[i].DepthModeReception);
                }
            }
            return true;
        }
        public bool TemplateRead(ref csWizardTemplate wizardTemplate)
        {
            double data;
            double[] adata;
            int idata;
            string sdata;
            csEnumDepthMode csDepthMode;

            if (wizardTemplate == null)
                return false;
            if (!Read("Velocity", "Specimen", out data))
                return false;
            wizardTemplate.Specimen.Velocity = data;
            sdata = Read("Wave", "Specimen");
            if (sdata == "T")
                wizardTemplate.Specimen.Wave = csWave.csTransverse;
            else
                wizardTemplate.Specimen.Wave = csWave.csLongitudinal;
            if (!Read("Radius", "Specimen", out data, 1.0e-3))
                return false;
            wizardTemplate.Specimen.Radius = data;
            sdata = Read("Enable", "Wedge");
            if (sdata == "1")
                wizardTemplate.Wedge.Enable = true;
            else
                wizardTemplate.Wedge.Enable = false;
            if (!Read("Velocity", "Wedge", out data))
                return false;
            wizardTemplate.Wedge.Velocity = data;
            if (!Read("Angle", "Wedge", out data, Math.PI / 180.0))
                return false;
            wizardTemplate.Wedge.Angle = data;
            if (!Read("Height", "Wedge", out data, 1.0e-3))
                return false;
            wizardTemplate.Wedge.Height = data;
            if (!Read("ElementOffset", "Probe", out idata))
                return false;
            wizardTemplate.Probe.ElementOffset = idata;
            if (!Read("ElementCount", "Probe", out idata))
                return false;
            wizardTemplate.Probe.ElementCount = idata;
            if (!Read("Pitch", "Probe", out data, 1.0e-3))
                return false;
            wizardTemplate.Probe.Pitch = data;
            if (!Read("Frequency", "Probe", out data, 1.0e6))
                return false;
            wizardTemplate.Probe.Frequency = data;
            if (!Read("Radius", "Probe", out data, 1.0e-3))
                return false;
            wizardTemplate.Probe.Radius = data;
            if (wizardTemplate.Scan.Linear)
            {
                wizardTemplate.Scan.Linear = true;
                if (!Read("Count", "Scan\\Linear", out idata))
                    return false;
                wizardTemplate.Scan.ElementCount = idata;
                if (!Read("DepthEmission", "Scan\\Linear", out data, 1.0e-3))
                    return false;
                wizardTemplate.Scan.DepthEmission = data;
                if (!aRead("DepthReception", "Scan\\Linear", out adata, 1.0e-3))
                    return false;
                wizardTemplate.Scan.DepthReception = adata;
                if (!Read("Angle", "Scan\\Linear", out data, Math.PI / 180.0))
                    return false;
                wizardTemplate.Scan.AngleStart = data;
                if (!Read("ElementStart", "Scan\\Linear", out idata))
                    return false;
                wizardTemplate.Scan.ElementStart = idata - 1;
                if (!Read("ElementStop", "Scan\\Linear", out idata))
                    return false;
                wizardTemplate.Scan.ElementStop = idata - 1;
                if (!Read("ElementStep", "Scan\\Linear", out idata))
                    return false;
                wizardTemplate.Scan.ElementStep = idata;
                if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthMode", "Scan\\Linear", out csDepthMode))
                    return false;
                wizardTemplate.Scan.DepthMode = csDepthMode;
            }
            else
            {
                wizardTemplate.Scan.Linear = false;
                if (!Read("Count", "Scan\\Sector", out idata))
                    return false;
                wizardTemplate.Scan.ElementCount = idata;
                if (!Read("DepthEmission", "Scan\\Sector", out data, 1.0e-3))
                    return false;
                wizardTemplate.Scan.DepthEmission = data;
                if (!aRead("DepthReception", "Scan\\Sector", out adata, 1.0e-3))
                    return false;
                wizardTemplate.Scan.DepthReception = adata;
                if (!Read("AngleStart", "Scan\\Sector", out data, Math.PI / 180.0))
                    return false;
                wizardTemplate.Scan.AngleStart = data;
                if (!Read("AngleStop", "Scan\\Sector", out data, Math.PI / 180.0))
                    return false;
                wizardTemplate.Scan.AngleStop = data;
                if (!Read("AngleStep", "Scan\\Sector", out data, Math.PI / 180.0))
                    return false;
                wizardTemplate.Scan.AngleStep = data;
                if (!Read("ElementStart", "Scan\\Sector", out idata))
                    return false;
                wizardTemplate.Scan.ElementStart = idata - 1;
                if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthMode", "Scan\\Sector", out csDepthMode))
                    return false;
                wizardTemplate.Scan.DepthMode = csDepthMode;
            }
            if (!Read("Start", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.Start = data;
            if (!Read("Range", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.Stop = wizardTemplate.GateAscan.Start + data;
            if (!Read("TimeSlot", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.TimeSlot = data;
            wizardTemplate.ReallocGateCscan();
            if (!Read("Count", "Cscan", out idata))
                return false;
            for (int index = 0; index < csWizardTemplate.GateCsanCountMax(); index++)
            {
                if (index < idata)
                {
                    if (!TemplateReadCscan(index, ref wizardTemplate.aGateCscan[index]))
                        return false;
                }
                else
                    wizardTemplate.aGateCscan[index].Enable = false;
            }
            return true;
        }

        public bool TemplateRead(ref csWizardPitchCatchTemplate wizardTemplate)
        {
            double data;
            double[] adata;
            int idata;
            string sdata;
            csEnumDepthMode csDepthMode;

            if (wizardTemplate == null)
                return false;
            if (!Read("Velocity", "Specimen", out data))
                return false;
            wizardTemplate.Specimen.Velocity = data;
            sdata = Read("Wave", "Specimen");
            if (sdata == "T")
                wizardTemplate.Specimen.Wave = csWave.csTransverse;
            else
                wizardTemplate.Specimen.Wave = csWave.csLongitudinal;
            if (!Read("Radius", "Specimen", out data, 1.0e-3))
                return false;
            wizardTemplate.Specimen.Radius = data;
            sdata = Read("Enable", "Wedge");
            if (sdata == "1")
                wizardTemplate.Wedge.Enable = true;
            else
                wizardTemplate.Wedge.Enable = false;
            if (!Read("Velocity", "Wedge", out data))
                return false;
            wizardTemplate.Wedge.Velocity = data;
            if (!Read("Angle", "Wedge", out data, Math.PI / 180.0))
                return false;
            wizardTemplate.Wedge.Angle = data;
            if (!Read("Height", "Wedge", out data, 1.0e-3))
                return false;
            wizardTemplate.Wedge.Height = data;
            if (!Read("ElementOffset", "Probe", out idata))
                return false;
            wizardTemplate.Probe.ElementOffset = idata;
            if (!Read("ElementCount", "Probe", out idata))
                return false;
            wizardTemplate.Probe.ElementCount = idata;
            if (!Read("Pitch", "Probe", out data, 1.0e-3))
                return false;
            wizardTemplate.Probe.Pitch = data;
            if (!Read("Frequency", "Probe", out data, 1.0e6))
                return false;
            wizardTemplate.Probe.Frequency = data;
            if (!Read("Radius", "Probe", out data, 1.0e-3))
                return false;
            wizardTemplate.Probe.Radius = data;
            if (Read("CountEmission", "Scan\\Linear", out idata))
                wizardTemplate.Scan.PitchCatchDefinition = csEnumPitchCatchDefinition.csLinear;
            else
            if (Read("CountEmission", "Scan\\Sector", out idata))
                wizardTemplate.Scan.PitchCatchDefinition = csEnumPitchCatchDefinition.csSector;
            else
                wizardTemplate.Scan.PitchCatchDefinition = csEnumPitchCatchDefinition.csCycleByCycle;
            switch (wizardTemplate.Scan.PitchCatchDefinition)
            {
                case csEnumPitchCatchDefinition.csLinear:
                    if (!Read("CountEmission", "Scan\\Linear", out idata))
                        return false;
                    wizardTemplate.Scan.ElementCountEmission = idata;
                    if (!Read("CountReception", "Scan\\Linear", out idata))
                        return false;
                    wizardTemplate.Scan.ElementCountReception = idata;
                    if (!Read("DepthEmission", "Scan\\Linear", out data, 1.0e-3))
                        return false;
                    wizardTemplate.Scan.DepthEmission = data;
                    if (!aRead("DepthReception", "Scan\\Linear", out adata, 1.0e-3))
                        return false;
                    wizardTemplate.Scan.DepthReception = adata;
                    if (!Read("AngleEmission", "Scan\\Linear", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStartEmission = data;
                    if (!Read("AngleReception", "Scan\\Linear", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStartReception = data;
                    if (!Read("ElementStartEmission", "Scan\\Linear", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStartEmission = idata - 1;
                    if (!Read("ElementStartReception", "Scan\\Linear", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStartReception = idata - 1;
                    if (!Read("ElementStopEmission", "Scan\\Linear", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStopEmission = idata - 1;
                    if (!Read("ElementStopReception", "Scan\\Linear", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStopReception = idata - 1;
                    if (!Read("ElementStepEmission", "Scan\\Linear", out idata))
                        return false;
                    wizardTemplate.Scan.ElementStepEmission = idata;
                    if (!Read("ElementStepReception", "Scan\\Linear", out idata))
                        return false;
                    wizardTemplate.Scan.ElementStepReception = idata;
                    if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthModeEmission", "Scan\\Linear", out csDepthMode))
                        return false;
                    wizardTemplate.Scan.DepthModeEmission = csDepthMode;
                    if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthModeReception", "Scan\\Linear", out csDepthMode))
                        return false;
                    wizardTemplate.Scan.DepthModeReception = csDepthMode;
                    break;

                case csEnumPitchCatchDefinition.csSector:
                    if (!Read("CountEmission", "Scan\\Sector", out idata))
                        return false;
                    wizardTemplate.Scan.ElementCountEmission = idata;
                    if (!Read("CountReception", "Scan\\Sector", out idata))
                        return false;
                    wizardTemplate.Scan.ElementCountReception = idata;
                    if (!Read("DepthEmission", "Scan\\Sector", out data, 1.0e-3))
                        return false;
                    wizardTemplate.Scan.DepthEmission = data;
                    if (!aRead("DepthReception", "Scan\\Sector", out adata, 1.0e-3))
                        return false;
                    wizardTemplate.Scan.DepthReception = adata;
                    if (!Read("AngleStartEmission", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStartEmission = data;
                    if (!Read("AngleStartReception", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStartReception = data;
                    if (!Read("AngleStopEmission", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStopEmission = data;
                    if (!Read("AngleStopReception", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStopReception = data;
                    if (!Read("AngleStepEmission", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStepEmission = data;
                    if (!Read("AngleStepReception", "Scan\\Sector", out data, Math.PI / 180.0))
                        return false;
                    wizardTemplate.Scan.AngleStepReception = data;
                    if (!Read("ElementStartEmission", "Scan\\Sector", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStartEmission = idata - 1;
                    if (!Read("ElementStartReception", "Scan\\Sector", out idata))
                        return false;
                    if (idata < 1)
                        return false;
                    wizardTemplate.Scan.ElementStartReception = idata - 1;
                    if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthModeEmission", "Scan\\Sector", out csDepthMode))
                        return false;
                    wizardTemplate.Scan.DepthModeEmission = csDepthMode;
                    if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthModeReception", "Scan\\Sector", out csDepthMode))
                        return false;
                    wizardTemplate.Scan.DepthModeReception = csDepthMode;
                    break;

                case csEnumPitchCatchDefinition.csCycleByCycle:
                    if (!Read("CycleCount", "Cycles", out idata))
                        return false;
                    wizardTemplate.Scan.SetScanCount(idata);
                    for (int i = 0; i < wizardTemplate.Scan.Cycles.Length; i++)
                    {
                        if (!Read("Angle", "Cycle:" + i + "\\Pulser", out data, Math.PI / 180.0))
                            return false;
                        wizardTemplate.Scan.Cycles[i].AngleEmission = data;
                        if (!Read("Depth", "Cycle:" + i + "\\Pulser", out data, 1.0e-3))
                            return false;
                        wizardTemplate.Scan.Cycles[i].DepthEmission = data;
                        if (!Read("Count", "Cycle:" + i + "\\Pulser", out idata))
                            return false;
                        wizardTemplate.Scan.Cycles[i].ElementCountEmission = idata;
                        if (!Read("ElementStart", "Cycle:" + i + "\\Pulser", out idata))
                            return false;
                        if (idata < 1)
                            return false;
                        wizardTemplate.Scan.Cycles[i].ElementStartEmission = idata - 1;
                        if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthMode", "Cycle:" + i + "\\Pulser", out csDepthMode))
                            return false;
                        wizardTemplate.Scan.Cycles[i].DepthModeEmission = csDepthMode;
                        if (!Read("Angle", "Cycle:" + i + "\\Reception", out data, Math.PI / 180.0))
                            return false;
                        wizardTemplate.Scan.Cycles[i].AngleReception = data;
                        if (!aRead("Depth", "Cycle:" + i + "\\Reception", out adata, 1.0e-3))
                            return false;
                        wizardTemplate.Scan.Cycles[i].DepthReception = adata;
                        if (!Read("Count", "Cycle:" + i + "\\Reception", out idata))
                            return false;
                        if (idata < 1)
                            return false;
                        wizardTemplate.Scan.Cycles[i].ElementCountReception = idata - 1;
                        if (!Read("ElementStart", "Cycle:" + i + "\\Reception", out idata))
                            return false;
                        wizardTemplate.Scan.Cycles[i].ElementStartReception = idata;
                        if (!ReadDepthMode(wizardTemplate.Specimen.Radius, "DepthMode", "Cycle:" + i + "\\Reception", out csDepthMode))
                            return false;
                        wizardTemplate.Scan.Cycles[i].DepthModeReception = csDepthMode;
                    }
                    break;

            }
            if (!Read("Start", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.Start = data;
            if (!Read("Range", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.Stop = wizardTemplate.GateAscan.Start + data;
            if (!Read("TimeSlot", "Ascan", out data, 1.0e-6))
                return false;
            wizardTemplate.GateAscan.TimeSlot = data;
            wizardTemplate.ReallocGateCscan();
            if (!Read("Count", "Cscan", out idata))
                return false;
            for (int index = 0; index < csWizardTemplate.GateCsanCountMax(); index++)
            {
                wizardTemplate.aGateCscan[index].Enable = false;
                if (index < idata)
                {
                    if (TemplateReadCscan(index, ref wizardTemplate.aGateCscan[index]))
                        wizardTemplate.aGateCscan[index].Enable = true;
                }
            }
            return true;
        }
        public bool IsTemplateLinear()
        {
            double data;
            int idata;

            if (!Read("Velocity", "Specimen", out data))
                return false;
            return Read("Count", "Scan\\Sector", out idata);
        }
        public bool IsTemplateSector()
        {
            double data;
            int idata;

            if (!Read("Velocity", "Specimen", out data))
                return false;
            return !Read("Count", "Scan\\Linear", out idata);
        }

    }
}

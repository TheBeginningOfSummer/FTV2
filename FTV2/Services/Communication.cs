using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CIPCommunication;

namespace Services
{
    public class Communication
    {
        public static Communication Singleton = new Communication();
        public NJCompoletLibrary Compolet;

        #region 要读取的变量名
        readonly string[] plcOutIOName;
        readonly string[] plcOutPOSName;
        readonly string[] plcOutAlarmName;
        readonly string[] plcInPmtName;
        readonly string[] plcOutFlagName = new string[] { "PLC标志位[0]", "PLC标志位[1]", "PLC标志位[2]" };
        readonly string[] plcTestInfoName;
        
        #endregion

        #region 读取到的键值对
        public ConcurrentDictionary<string, bool> PLCOutput { get; private set; }
        public ConcurrentDictionary<string, double> Location { get; private set; }
        public ConcurrentDictionary<string, bool> Alarm { get; private set; }
        public ConcurrentDictionary<string, double> PLCPmt { get; private set; }
        public ConcurrentDictionary<string, bool> FlagBits { get; private set; }
        public ConcurrentDictionary<string, string> TestInformation { get; private set; }
        #endregion

        private Communication()
        {
            Compolet = new NJCompoletLibrary();

            #region 初始化变量名
            //PLC Out IO
            plcOutIOName = InitializeNameArray("PlcOutIO", 0, 299);
            //位置信息
            plcOutPOSName = InitializeNameArray("POS", 0, 199);
            //报警信息
            plcOutAlarmName = InitializeNameArray("PlcOutAlarm", 0, 249);
            //参数信息
            plcInPmtName = InitializeNameArray("PlcInPmt", 0, 149);
            //标志位
            plcOutFlagName = InitializeNameArray("PLC标志位", 0, 2);
            //字符串信息
            plcTestInfoName = InitializeNameArray("PLC测试信息", 0, 59);
            #endregion
        }

        /// <summary>
        /// 初始化一组连续的字符串数组
        /// </summary>
        /// <param name="mainValue">字符串值</param>
        /// <param name="start">开始的索引</param>
        /// <param name="end">结束的索引</param>
        /// <returns></returns>
        public static string[] InitializeNameArray(string mainValue, int start, int end)
        {
            List<string> arrayList = new List<string>();
            for (int i = start; i <= end; i++)
                arrayList.Add($"{mainValue}[{i}]");
            return arrayList.ToArray();
        }
        
        public void RefreshData()
        {
            try
            {
                #region 更新数据
                UpdateValue<bool>(Compolet.GetHashtable(plcOutIOName), PLCOutput);
                UpdateValue<double>(Compolet.GetHashtable(plcOutPOSName), Location);
                //UpdateValue<bool>(Compolet.GetHashtable(plcOutAlarmName), Alarm);
                //UpdateValue(Compolet.GetHashtable(plcInPmtName), PLCPmt);
                //UpdateValue(Compolet.GetHashtable(plcOutFlagName), FlagBits);
                //UpdateValue<string>(Compolet.GetHashtable(plcTestInfoName), TestInformation);//测试信息
                #endregion

                #region 从PLC读取标志位
                ////托盘扫码完成
                //ReadFlagBits[0] = Compolet.ReadVariable<bool>("PLC标志位[0]");
                ////探测器测试完成
                //ReadFlagBits[1] = Compolet.ReadVariable<bool>("PLC标志位[1]");
                ////20个托盘已摆好
                //ReadFlagBits[2] = Compolet.ReadVariable<bool>("PLC标志位[2]");
                #endregion

                #region 读取测试信息（字符串变量）
                ////产品编码
                //ReadTestInformation[0] = Compolet.ReadVariable<string>("PLC测试信息[0]");
                ////类型
                //ReadTestInformation[1] = Compolet.ReadVariable<string>("PLC测试信息[1]");
                ////测试工位
                //ReadTestInformation[2] = Compolet.ReadVariable<string>("PLC测试信息[2]");
                ////结果
                //ReadTestInformation[3] = Compolet.ReadVariable<string>("PLC测试信息[3]");
                ////托盘编号
                //ReadTestInformation[4] = Compolet.ReadVariable<string>("PLC测试信息[4]");
                ////托盘位置
                //ReadTestInformation[5] = Compolet.ReadVariable<string>("PLC测试信息[5]");
                ////外观
                //ReadTestInformation[6] = Compolet.ReadVariable<string>("PLC测试信息[6]");
                ////开始时间
                //ReadTestInformation[7] = Compolet.ReadVariable<string>("PLC测试信息[7]");
                ////完成时间
                //ReadTestInformation[8] = Compolet.ReadVariable<string>("PLC测试信息[8]");
                ////当前托盘索引
                //ReadTestInformation[20] = Compolet.ReadVariable<string>("PLC测试信息[20]");
                ////良率
                //ReadTestInformation[21] = Compolet.ReadVariable<string>("PLC测试信息[21]");
                ////探针次数
                //ReadTestInformation[22] = Compolet.ReadVariable<string>("PLC测试信息[22]");
                ////当前控制器时间
                //ReadTestInformation[29] = Compolet.ReadVariable<string>("PLC测试信息[29]");
                ////上视觉1扫码信息
                //ReadTestInformation[30] = Compolet.ReadVariable<string>("PLC测试信息[30]");
                ////下视觉对位X
                //ReadTestInformation[32] = Compolet.ReadVariable<string>("PLC测试信息[32]");
                ////下视觉对位Y
                //ReadTestInformation[33] = Compolet.ReadVariable<string>("PLC测试信息[33]");
                ////下视觉对位θ
                //ReadTestInformation[34] = Compolet.ReadVariable<string>("PLC测试信息[34]");
                ////上视觉2对位X
                //ReadTestInformation[36] = Compolet.ReadVariable<string>("PLC测试信息[36]");
                ////上视觉2对位Y
                //ReadTestInformation[37] = Compolet.ReadVariable<string>("PLC测试信息[37]");
                ////上视觉2对位θ
                //ReadTestInformation[38] = Compolet.ReadVariable<string>("PLC测试信息[38]");
                ////计算对位X
                //ReadTestInformation[39] = Compolet.ReadVariable<string>("PLC测试信息[39]");
                ////计算对位Y
                //ReadTestInformation[40] = Compolet.ReadVariable<string>("PLC测试信息[40]");
                ////计算对位θ
                //ReadTestInformation[41] = Compolet.ReadVariable<string>("PLC测试信息[41]");
                ////下视觉2对位X
                //ReadTestInformation[42] = Compolet.ReadVariable<string>("PLC测试信息[42]");
                ////下视觉2对位Y
                //ReadTestInformation[43] = Compolet.ReadVariable<string>("PLC测试信息[43]");
                ////下视觉2对位θ
                //ReadTestInformation[44] = Compolet.ReadVariable<string>("PLC测试信息[44]");
                ////钧舵1反馈字节
                //ReadTestInformation[45] = Compolet.ReadVariable<string>("PLC测试信息[45]");
                ////钧舵2反馈字节
                //ReadTestInformation[46] = Compolet.ReadVariable<string>("PLC测试信息[46]");
                ////钧舵3反馈字节
                //ReadTestInformation[47] = Compolet.ReadVariable<string>("PLC测试信息[47]");
                ////钧舵4反馈字节
                //ReadTestInformation[48] = Compolet.ReadVariable<string>("PLC测试信息[48]");
                ////上视觉1偏移X
                //ReadTestInformation[49] = Compolet.ReadVariable<string>("PLC测试信息[49]");
                ////上视觉1偏移Y
                //ReadTestInformation[50] = Compolet.ReadVariable<string>("PLC测试信息[50]");
                ////上视觉1偏移θ
                //ReadTestInformation[51] = Compolet.ReadVariable<string>("PLC测试信息[51]");
                #endregion

            }
            catch (Exception)
            {
                //LogManager.WriteLog($"读取PLC数据。{e.Message}", LogType.Error);
            }
        }

        public void UpdateValue<T>(Hashtable sourceTable, ConcurrentDictionary<string, T> targetTable)
        {
            foreach (DictionaryEntry keyValue in sourceTable)
            {
                T v = (T)Convert.ChangeType(keyValue.Value, typeof(T));
                targetTable.AddOrUpdate((string)keyValue.Key, v, (oldKey, oldValue) => v);
            }
        }

        public bool WriteVariable<T>(T variable, string variableName)
        {
            try
            {
                bool result = Compolet.WriteVariable(variableName, variable);
                if (!result) LogManager.WriteLog($"参数{variableName}写入失败，检查连接状态。", LogType.Error);
                return result;
            }
            catch (Exception e)
            {
                LogManager.WriteLog($"参数{variableName}写入失败。{e.Message}", LogType.Error);
                return false;
            }
        }


    }

}

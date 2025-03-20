using System;
using System.Collections;
using OMRON.Compolet.CIP;

namespace CIPCommunication
{
    public class NJCompoletLibrary
    {
        #region 端口
        private CIPPortCompolet portCompolet;
        private NJCompolet compolet;
        #endregion

        #region 参数
        public DateTime CurrentTime;
        public string PeerAddress;
        public int LocalPort;
        #endregion

        readonly IComparer stringValueComparer = new StringValueComparer();

        public NJCompoletLibrary(string peerAddress = "192.168.250.6", int localPort = 2)
        {
            PeerAddress = peerAddress;
            LocalPort = localPort;
        }

        private void NjCompolet1_OnHeartBeatTimer(object sender, System.EventArgs e)
        {
            if (!portCompolet.IsOpened(LocalPort)) return;
            try
            {
                DateTime date = (DateTime)this.compolet.ReadVariable("_CurrentTime");
                this.CurrentTime = date;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 连接
        /// <summary>
        /// 开启
        /// </summary>
        public void Open()
        {
            this.portCompolet = new CIPPortCompolet();
            this.compolet = new NJCompolet();

            if (!portCompolet.IsOpened(LocalPort))
            {
                try
                {
                    portCompolet.Open(LocalPort);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (portCompolet.IsOpened(LocalPort))
            {
                this.compolet.ConnectionType = ConnectionType.UCMM;
                this.compolet.DontFragment = false;
                this.compolet.HeartBeatTimer = 0;
                this.compolet.LocalPort = LocalPort;
                this.compolet.PeerAddress = PeerAddress;
                this.compolet.ReceiveTimeLimit = ((long)(750));
                this.compolet.OnHeartBeatTimer += new System.EventHandler(this.NjCompolet1_OnHeartBeatTimer);

                this.compolet.Active = true;

                if (!this.compolet.IsConnected)
                {
                    throw new Exception("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress." + this.compolet.PeerAddress);
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            this.compolet.Active = false;

            if (!portCompolet.IsOpened(LocalPort))
                return;

            try
            {
                portCompolet.Close(LocalPort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 读数据
        /// <summary>
        /// 读参数
        /// </summary>
        /// <param name="variableName">变量名</param>
        /// <returns></returns>
        public T ReadVariable<T>(string variableName)
        {
            if (!portCompolet.IsOpened(LocalPort))
                return default;
            else
                return (T)Convert.ChangeType(compolet.ReadVariable(variableName), typeof(T));
        }
        /// <summary>
        /// 直接读取哈希表
        /// </summary>
        /// <param name="variableNames">变量名数组</param>
        /// <returns></returns>
        public Hashtable GetHashtable(string[] variableNames)
        {
            if (!portCompolet.IsOpened(LocalPort))
                return null;
            else
                return compolet.ReadVariableMultiple(variableNames);
        }

        /// <summary>
        /// 读多个数据的变量名(按变量名中的数字排序)
        /// </summary>
        /// <param name="variableNames">变量名数组</param>
        /// <returns></returns>
        public string[] ReadVariablesKeyArray(string[] variableNames)
        {
            if (!portCompolet.IsOpened(LocalPort)) return new string[] { "null" };
            Hashtable hashtable = this.compolet.ReadVariableMultiple(variableNames);
            string[] keys = new string[hashtable.Count];
            hashtable.Keys.CopyTo(keys, 0);
            Array.Sort(keys, stringValueComparer);
            return keys;
        }
        /// <summary>
        /// 读多个数据的变量值(按变量名中的数字排序)
        /// </summary>
        /// <param name="variableNames">变量名数组</param>
        /// <returns></returns>
        public object[] ReadVariablesValueArray(string[] variableNames)
        {
            if (!portCompolet.IsOpened(LocalPort)) return null;
            Hashtable hashtable = this.compolet.ReadVariableMultiple(variableNames);
            string[] keys = new string[hashtable.Count];
            object[] values = new object[hashtable.Count];
            hashtable.Keys.CopyTo(keys, 0);
            hashtable.Values.CopyTo(values, 0);
            Array.Sort(keys, values, stringValueComparer);
            return values;
        }
        /// <summary>
        /// 读多个数据的变量值(按变量名中的数字排序)
        /// </summary>
        /// <typeparam name="T">给定的类型</typeparam>
        /// <param name="variableNames">变量名数组</param>
        /// <returns></returns>
        public T[] ReadVariablesValueArray<T>(string[] variableNames)
        {
            if (!portCompolet.IsOpened(LocalPort)) return null;
            Hashtable hashtable = this.compolet.ReadVariableMultiple(variableNames);
            string[] keys = new string[hashtable.Count];
            T[] values = new T[hashtable.Count];
            hashtable.Keys.CopyTo(keys, 0);
            hashtable.Values.CopyTo(values, 0);
            Array.Sort(keys, values, stringValueComparer);
            return values;
        }
        #endregion

        #region 写数据
        public void WriteVariable(string variableName, object writeData)
        {
            if (!portCompolet.IsOpened(LocalPort)) return;
            this.compolet.WriteVariable(variableName, writeData);
        }

        public bool WriteVariable<T>(string variableName, T variable)
        {
            if (!portCompolet.IsOpened(LocalPort)) return false;
            this.compolet.WriteVariable(variableName, variable);
            return true;
        }
        #endregion

    }
}


using Services;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FTV2.Services
{
    public class Config
    {
        #region 单例模式
        private static Config _instance;
        private static readonly object _instanceLock = new object();
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                        _instance = new Config();
                }
                return _instance;
            }
        }
        #endregion

        public string ConfigPath { get; private set; } = "Config";

        public Config()
        {
            
        }


    }
}

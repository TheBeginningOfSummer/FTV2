using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CIPCommunication
{
    public class CompoletSingleton
    {
        private static CompoletSingleton instance;//1.需要提供一个静态实例
        private NJCompoletLibrary compolet;

        private CompoletSingleton()//2.私有化构造函数，不让外部随便创建对象
        {
            compolet = new NJCompoletLibrary();
        }

        public static NJCompoletLibrary GetCompolet()//3.提供获取的实例接口
        {
            //if (instance == null)//如果实例为空，就创建一个静态的实例，在整个程序过程中都不会发生变化
            //{
            instance = new CompoletSingleton();
            //}
            return instance.compolet;
        }
    }
}

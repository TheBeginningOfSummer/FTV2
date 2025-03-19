using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class JsonManager
    {
        public static void SaveJsonString(string path, string fileName, object data)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += "/" + fileName;
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            FileStream file = new FileStream(path, FileMode.Create);
            file.Write(jsonBytes, 0, jsonBytes.Length);//整块写入
            file.Flush();
            file.Close();
        }

        public static T ReadJsonString<T>(string path, string fileName)
        {
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += "\\" + fileName;
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader stream = new StreamReader(file);
                T jsonData = JsonSerializer.Deserialize<T>(stream.ReadToEnd());
                file.Flush();
                file.Close();
                //T jsonData = JsonMapper.ToObject<T>(File.ReadAllText(path));
                return jsonData;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static bool Save<T>(string path, string fileName, T parameter)
        {
            if (parameter == null) return false;
            try
            {
                SaveJsonString(path, fileName, parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T Load<T>(string path, string fileName) where T : new()
        {
            var result = ReadJsonString<T>(path, fileName);
            if (result == null)
            {
                result = new T();
                Save(path, fileName, result);
            }
            return result;
        }
    }
}

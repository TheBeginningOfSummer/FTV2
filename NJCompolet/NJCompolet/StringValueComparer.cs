using System;
using System.Collections;

namespace CIPCommunication
{
    public class StringValueComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null || y == null) throw new ArgumentException("参数不能为空");
            string a = x as string;
            string b = y as string;
            char[] array1 = a.ToCharArray();
            char[] array2 = b.ToCharArray();
            int i = 0, j = 0;
            while(i < array1.Length && j < array2.Length)
            {
                if(char.IsDigit(array1[i]) && char.IsDigit(array2[j]))
                {
                    string s1 = "", s2 = "";
                    while (i < array1.Length && char.IsDigit(array1[i]))
                    {
                        s1 += array1[i];
                        i++;
                    }
                    while(j < array2.Length && char.IsDigit(array2[j]))
                    {
                        s2 += array2[j];
                        j++;
                    }
                    if (int.Parse(s1) > int.Parse(s2)) return 1;
                    if (int.Parse(s1) < int.Parse(s2)) return -1;

                }
                else
                {
                    if (array1[i] > array2[j]) return 1;
                    if (array1[i] < array2[j]) return -1;
                    i++;
                    j++;
                }
            }
            if (array1.Length == array2.Length) return 0;
            else return array1.Length > array2.Length ? 1 : -1;
        }
    }
}

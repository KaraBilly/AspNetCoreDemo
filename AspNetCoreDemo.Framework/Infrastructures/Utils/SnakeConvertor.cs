using System;
using System.Collections.Generic;
using System.Text;

namespace ND.ManagementSvcs.Framework.Infrastructures.Utils
{
    internal static class SnakeConvertor
    {
        internal static string HumpToSnake(string para)
        {
            StringBuilder sb = new StringBuilder(para);
            var s = para.ToCharArray();
            int temp = 0;
            if (!para.Contains("_"))
            {
                for (int i = 1; i < para.Length; i++)
                {
                    if (s[i] >= 65 && s[i] <= 90)
                    {
                        sb.Insert(i + temp, "_");
                        temp += 1;
                    }
                }
            }

            return sb.ToString().ToLower();
        }
    }
}

using AX.Core.Extension;
using System.IO;

namespace AX.Core.Config
{
    public class TJsonConfig<T> where T : new()
    {
        public static T Current { get; set; }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, Current.ToJson());
        }
    }
}
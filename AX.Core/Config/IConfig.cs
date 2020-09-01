using System;

namespace AX.Core.Config
{
    public interface IConfig<T> where T : class
    {
        string Name { get; }

        string FilePath { get; }

        DateTime? LoadTime { get; }

        DateTime? LastSaveTime { get; }

        String CaCheValueTypeName { get; }

        bool Load();

        bool Save();
    }
}
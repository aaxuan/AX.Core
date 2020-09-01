using System;

namespace AX.Core.Config
{
    public interface IConfig
    {
        string CaCheValueTypeName { get; }

        string FilePath { get; }

        DateTime? LastSaveTime { get; }

        DateTime? LoadTime { get; }

        string Name { get; }

        bool Load();

        bool Save();
    }

    public interface IConfigT<T> : IConfig where T : class
    {
    }
}
using System;

namespace AX.Core.Cache
{
    public interface ICaChe
    {
        string Name { get; }

        int Count { get; }

        DateTime CreateTime { get; }

        String CaCheValueTypeName { get; }
    }
}
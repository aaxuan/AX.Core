using System;

namespace AX.Core.Cache
{
    public interface ICaChe
    {
        string Name { get; }

        int Count { get; }

        String CaCheValueTypeName { get; }
    }
}
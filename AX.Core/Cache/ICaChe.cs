using System;

namespace AX.Core.Cache
{
    public interface ICaChe
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 缓存数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 缓存值类型全名
        /// </summary>
        String CaCheValueTypeName { get; }
    }
}
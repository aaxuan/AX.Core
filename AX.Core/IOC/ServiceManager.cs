using Microsoft.Extensions.DependencyInjection;
using System;

namespace AX.Core.IOC
{
    public static class ServiceManager
    {
        public static IServiceCollection ServiceCollection { get; set; }

        public static IServiceProvider ServiceProvider { get; set; }
    }
}
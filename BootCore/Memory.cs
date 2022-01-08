using System;
using Sys = Cosmos.System;

/*
 * The original version taken from https://github.com/corgifist/quantum/blob/main/Utils/KernelMemoryMonitor.cs
 */

namespace ComobiOS.BootCore
{
    class Memory
    {
        public static uint GetUsedMemory()
        {
            uint UsedRAM = Cosmos.Core. CPU.GetEndOfKernel() + 1024;
            return UsedRAM / div;
        }
        public static uint TotalMemory = Cosmos.Core.CPU.GetAmountOfRAM();
        public uint FreePercentage;
        public uint UsedPercentage = (GetUsedMemory() * 100) / TotalMemory;
        public uint FreeMemory = TotalMemory - GetUsedMemory();
        private const uint div = 1048576;

        public static void GetTotalMemory()
        {
            TotalMemory = Cosmos.Core.CPU.GetAmountOfRAM() + 1;
        }
        public void Monitor()
        {
            GetTotalMemory();
            FreeMemory = TotalMemory - GetUsedMemory();
            UsedPercentage = (GetUsedMemory() * 100) / TotalMemory;
            FreePercentage = 100 - UsedPercentage;
        }
        public Memory()
        {
            Monitor();
        }

        public static uint GetFreeMemory()
        {
            return TotalMemory - GetUsedMemory();
        }
    }
}

﻿using System.Threading;
using System.Threading.Tasks;
using FileMonitor.Utilities;

namespace FileMonitor
{
    internal static class Program
    {
        private static Task Main(string[] args)
        {
            var monitor = new FileDeletionService(new Logger(), new FileSystemValidator());
            while (true)
            {
                monitor.Run();
                Thread.Sleep(900000);
            }
        }
    }
}
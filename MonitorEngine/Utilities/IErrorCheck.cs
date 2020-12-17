﻿namespace MonitorEngine.Utilities
{
    public interface IErrorCheck
    {
        bool CheckIfDirectoryIsEmpty(string directory);
        bool CheckIfDirectoryIsCorrectFormat(string directory);
        bool CheckIfFileIsCorrectFormat(string file);
        bool CheckIfFileDateIsLessThanCurrentDate(string file);
    }
}
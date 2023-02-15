using System;

namespace Interfaces
{
    public interface IDataReader
    {
        void ReadData(int timeoutMs, Action<string[]> dataRead);
        event EventHandler Timeout;
    }
}
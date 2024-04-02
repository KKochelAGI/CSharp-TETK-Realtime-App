using System;

namespace Interfaces
{
    public interface IDataWriter
    {
        void WriteData(int dataRefreshSpeed,int dataRate);
        event EventHandler WriterFinished;
    }
}
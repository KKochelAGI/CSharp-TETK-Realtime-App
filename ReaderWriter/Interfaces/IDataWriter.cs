using System;

namespace Interfaces
{
    public interface IDataWriter
    {
        void WriteData();
        event EventHandler WriterFinished;
    }
}
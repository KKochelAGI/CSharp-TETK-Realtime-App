using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;

namespace Implementations
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class FileDataReader : IDataReader, IDisposable
    {
        private readonly Mutex _mut;
        private long _filePeek = -1;
        private readonly string _inputFile;
        public event EventHandler Timeout;

        public FileDataReader(string inputFile, string mutexName)
        {
            _mut = new Mutex(false, mutexName);
            _inputFile = inputFile;
        }

        public void ReadData(int timeoutMs, Action<string[]> dataRead)
        {
            if (_mut.WaitOne(timeoutMs)) {
                using (var file = new FileStream(_inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var read = new StreamReader(file))
                    {
                        // Seek to where we last read the file
                        if (_filePeek > 0)
                        {
                            file.Seek(_filePeek, SeekOrigin.Begin);
                        }

                        var lines = read.ReadToEnd().Split('\n');
                        Task.Run(() => dataRead(lines));

                        _filePeek = file.Position;
                    }
                }

                _mut.ReleaseMutex();
            }
            else
            {
                OnTimeout();
            }
        }

        private void OnTimeout()
        {
            Timeout?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _mut.Dispose();
        }
    }
}

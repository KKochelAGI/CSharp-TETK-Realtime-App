using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Interfaces;

namespace Implementations
{
    public class FileDataWriter : IDataWriter, IDisposable
    {
        public event EventHandler WriterFinished;

        private readonly string _outputFile;
        private readonly StreamReader _read;
        private readonly Mutex _mut;
        private bool _firstTime = true;

        public FileDataWriter(string inputFile, string outputFile)
        {
            _read = new StreamReader(inputFile);
            _outputFile = outputFile;
            _mut = new Mutex(false, "TE_Mutex");
        }

        public void WriteData()
        {
            int numberOfLines = 5000;

            try
            {
                if (!_mut.WaitOne(10000)) return;

                if (_firstTime)
                {
                    if (File.Exists(_outputFile))
                        File.Delete(_outputFile);
                    _firstTime = false;
                }

                using (var fileStream = new FileStream(_outputFile, FileMode.Append, FileAccess.Write))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        int cnt = 0;
                        while (_read.Peek() >= 0)
                        {
                            streamWriter.WriteLine(_read.ReadLine());
                            if (++cnt > numberOfLines + 1) // Value here sets how many lines of data are read per time interval set on line 17 in WriterForm.cs
                                break;
                        }

                        streamWriter.Flush();
                        fileStream.Flush(true); // make sure everything is written
                    }
                }

                if (_read.Peek() < 0)
                {
                    _read.Close();
                    OnWriterFinished(this, EventArgs.Empty);
                }

                _mut.ReleaseMutex();

                //if (_read.Peek() < 0)
                //{
                //    OnWriterFinished(this, EventArgs.Empty);
                //}
            }
            catch (AbandonedMutexException ex)
            {
                _read.Close();
                throw new ApplicationException("Reader process exited and released the mutex.  Stopping...", ex);
            }
            catch (Exception ex)
            {
                _read.Close();
                MessageBox.Show(ex.ToString());
                throw new ApplicationException("Exception occurred in FileDataWriter.WriteData", ex);
            }
        }

        protected virtual void OnWriterFinished(object sender, EventArgs e)
        {
            WriterFinished?.Invoke(sender, e);
        }

    #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                   _mut.Dispose();
                   _read.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}

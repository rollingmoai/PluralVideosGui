using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace PluralVideosGui.Encryption
{
    internal class VirtualFileStream : IStream, IDisposable
    {
        private long _position;
        private readonly object _lock;
        private readonly VirtualFileCache _cache;

        private VirtualFileStream(VirtualFileCache cache)
        {
            this._lock = new object();
            this._cache = cache;
        }

        public VirtualFileStream(string encryptedVideoFilePath)
        {
            this._lock = new object();
            this._cache = new VirtualFileCache(encryptedVideoFilePath);
        }

        public void Clone(out IStream ppstm)
        {
            ppstm = new VirtualFileStream(this._cache);
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this._cache.Dispose();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            if ((this._position < 0L) || (this._position > this._cache.Length))
            {
                Marshal.WriteIntPtr(pcbRead, new IntPtr(0));
            }
            else
            {
                object obj2 = this._lock;
                lock (obj2)
                {
                    this._cache.Read(pv, (int)this._position, cb, pcbRead);
                    this._position += pcbRead.ToInt64();
                }
            }
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            SeekOrigin origin = (SeekOrigin)dwOrigin;
            object obj2 = this._lock;
            lock (obj2)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        this._position = dlibMove;
                        break;

                    case SeekOrigin.Current:
                        this._position += dlibMove;
                        break;

                    case SeekOrigin.End:
                        this._position = this._cache.Length + dlibMove;
                        break;
                }
                if (IntPtr.Zero != plibNewPosition)
                {
                    Marshal.WriteInt64(plibNewPosition, this._position);
                }
            }
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        public void Stat(out STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new STATSTG { cbSize = this._cache.Length };
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }
    }
}

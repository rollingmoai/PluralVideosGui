using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PluralVideosGui.Encryption
{
    public class VirtualFileCache : IDisposable
    {
        private readonly IPsStream _encryptedVideoFile;

        public VirtualFileCache(IPsStream stream)
        {
            this._encryptedVideoFile = stream;
        }

        public VirtualFileCache(string encryptedVideoFilePath)
        {
            this._encryptedVideoFile = new PsStream(encryptedVideoFilePath);
        }

        public void Dispose()
        {
            this._encryptedVideoFile.Dispose();
        }

        public void Read(byte[] pv, int offset, int count, IntPtr pcbRead)
        {
            if (this.Length != 0)
            {
                this._encryptedVideoFile.Seek(offset, SeekOrigin.Begin);
                int length = this._encryptedVideoFile.Read(pv, 0, count);
                VideoEncryption.DecryptBuffer(pv, length, (long)offset);
                if (IntPtr.Zero != pcbRead)
                {
                    Marshal.WriteIntPtr(pcbRead, new IntPtr(length));
                }
            }
        }

        public long Length => this._encryptedVideoFile.Length;
    }
}

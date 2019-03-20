namespace PluralVideosGui.Encryption
{
    public class VideoEncryption
    {
        //private const string String1 = "pluralsight";
        //private const string String2 = "\x0006?zY¢\x00B2\x0085\x009FL\x00BEî0Ö.ì\x0017#©>Å£Q\x0005¤°\x00018Þ^\x008Eú\x0019Lqß'\x009D\x0003ßE\x009EM\x0080'x:\0~\x00B9\x0001ÿ 4\x00B3õ\x0003Ã§Ê\x000EAË\x00BC\x0090è\x009Eî~\x008B\x009Aâ\x001B¸UD<\x007FKç*\x001Döæ7H\v\x0015Arý*v÷%Âþ\x00BEä;pü";
        private static string _string1V2 = "\0¿{U9\x0001®`ë\x0013Ñ[\x001BÏ";
        private static string _string2V2 = "\x0002\x008D\a\x0099\x0089\x009A%\x0084K°súÁ48äcz@\x009F,í>ö 2\vß\n@*í\vz\x008C\x0004\x00BD\x0093\0ÜeË\x0086\x001F\bÖ\x009E ADÓg&ì¶\x0017\x008DÀ\x0014{µìß\x0088Ø\x009FòÕÄ\x0081pªªtC\x008A@\x009C2:Åf\\\\\x00ADè\x009Eý\x0002g\x0003|ØBf\x0092 ";
        internal static readonly string[][] CryptoKeys = {
              new[]
              {
                "pluralsight",
                "\x0006?zY¢\x00B2\x0085\x009FL\x00BEî0Ö.ì\x0017#©>Å£Q\x0005¤°\x00018Þ^\x008Eú\x0019Lqß'\x009D\x0003ßE\x009EM\x0080'x:\0~\x00B9\x0001ÿ 4\x00B3õ\x0003Ã§Ê\x000EAË\x00BC\x0090è\x009Eî~\x008B\x009Aâ\x001B¸UD<\x007FKç*\x001Döæ7H\v\x0015Arý*v÷%Âþ\x00BEä;pü"
              },
              new[]
              {
                String1V2,
                String2V2
              },
              new[]
              {
                "os22$!sKHyy9jnGlgHB&vP21CK96tx!l2uhK1K%Fbubree9%o0wT44zwvJ446iAdA%M!@RopKCmOWMOqTt1*BIw@lF68x3itctw"
              }
        };
        private static int _currentClipReadCrypto;

        public static string String1V2
        {
            get => _string1V2;
            set
            {
                CryptoKeys[1][0] = value;
                _string1V2 = value;
            }
        }

        public static string String2V2
        {
            get => _string2V2;
            set
            {
                CryptoKeys[1][1] = value;
                _string2V2 = value;
            }
        }

        private static void XorBuffer(byte[] buff, int length, long position)
        {
            XorBuffer(_currentClipReadCrypto, buff, length, position);
        }

        internal static void XorBuffer(int key, byte[] buff, int length, long position)
        {
            for (int index1 = 0; index1 < length; ++index1)
            {
                string[] cryptoKey = CryptoKeys[key];
                string str1 = cryptoKey[0];
                int num1 = (int)position + index1;
                char ch = str1[num1 % str1.Length];
                for (int index2 = 1; index2 < cryptoKey.Length; ++index2)
                {
                    string str2 = cryptoKey[index2];
                    ch ^= str2[num1 % str2.Length];
                }
                int num2 = ch ^ num1 % 251;
                buff[index1] = (byte)(buff[index1] ^ (uint)num2);
            }
        }

        public static void EncryptBuffer(byte[] buff, int length, long position)
        {
            XorBuffer(CryptoKeys.Length - 1, buff, length, position);
        }

        public static void DecryptBuffer(byte[] buff, int length, long position)
        {
            if (position == 0L)
            {
                for (int index1 = CryptoKeys.Length - 1; index1 >= 0; --index1)
                {
                    _currentClipReadCrypto = index1;
                    XorBuffer(buff, length, position);
                    bool flag = (uint)buff.Length > 0U;
                    for (int index2 = 0; index2 < buff.Length && index2 < 3; ++index2)
                        flag = flag && buff[index2] == 0;
                    if (flag)
                        return;
                    XorBuffer(buff, length, position);
                }
            }
            XorBuffer(buff, length, position);
        }
    }
}


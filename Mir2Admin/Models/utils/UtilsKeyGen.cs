using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mir2Admin.Models
{
    public class UtilsKeyGen
    {
        public UtilsKeyGen()
        {
        }

        private UInt32 GenerateKey(int seed)
        {
            Random random = new Random(seed);
            
            UInt32 uKeyValue = (UInt32)((random.Next(0xFFF) % 0x1000) * DateTime.UtcNow.Ticks);
            return uKeyValue;
        }

        private UInt32 EncryptKey(UInt32 dwKey)
        {

            byte[] destKey = new byte[4];

            destKey[0] = (byte)((dwKey >> 16) & 0xFF);  //2
            destKey[1] = (byte)(dwKey & 0xFF);          //0
            destKey[2] = (byte)((dwKey >> 24)& 0xFF);   //3
            destKey[3] = (byte)((dwKey >> 8) & 0xFF);   //1

            UInt32 resKey = BitConverter.ToUInt32(destKey, 0);

            return resKey;
        }

        private UInt32 DecryptKey(UInt32 dwKey)
        {
            byte[] destKey = new byte[4];

            destKey[0] = (byte)((dwKey >> 8) & 0xFF);   //1
            destKey[1] = (byte)((dwKey >> 24) & 0xFF);  //3
            destKey[2] = (byte)(dwKey & 0xFF);          //0
            destKey[3] = (byte)((dwKey >> 16) & 0xFF);  //2

            UInt32 resKey = BitConverter.ToUInt32(destKey, 0); 

            return resKey;
        }

        public string GenerateId(int seed, int year, int month, int day)
        {
            string strCardNo = "T";
            // generate key (4byte)
            UInt32 dwKey = GenerateKey(seed);
            UInt32 dwKeyEnc = EncryptKey(dwKey);

            byte[] btArr1= BitConverter.GetBytes(dwKeyEnc);

            byte[] btArr2 = BitConverter.GetBytes((UInt16)month);
            byte[] btArr3 = BitConverter.GetBytes((UInt16)year);
            byte[] btArr4 = BitConverter.GetBytes((UInt16)day);
            byte[] btArr234 = new byte[btArr2.Length + btArr3.Length + btArr4.Length];
            Array.Copy(btArr2, 0, btArr234, 0, btArr2.Length);
            Array.Copy(btArr3, 0, btArr234, btArr2.Length, btArr3.Length);
            Array.Copy(btArr4, 0, btArr234, btArr2.Length+btArr3.Length, btArr4.Length);

            Encrypt(ref btArr234, 6, dwKey);

            byte[] btCardNo = new byte[btArr1.Length + btArr234.Length];
            Array.Copy(btArr1, 0, btCardNo, 0, btArr1.Length);
            Array.Copy(btArr234, 0, btCardNo, btArr1.Length, btArr234.Length);
            
            strCardNo += BinToStr(ref btCardNo, 10);

            return strCardNo;
        }

        public string GeneratePwd(int seed, int len)
        {
            string strCardNo = "";

            Random random = new Random(seed);

            byte[] btArr = new byte[len];
            for(int i = 0; i < len; i++)
            {
                btArr[i] = (byte)(random.Next() % 0x100);
            }

            strCardNo += BinToStr(ref btArr, len);
            
            return strCardNo;
        }

        private void Encrypt(ref byte[] btData, int len, UInt32 dwKey)
        {
            byte[] szKey = BitConverter.GetBytes(dwKey);
            
            for (int i = 0; i < len; i++)
            {
                btData[i] += 0xE8;
                if (i > 0)
                    btData[i] ^= btData[i - 1];
                btData[i] ^= szKey[(i + 2) % 4];
            }
        }

        private string BinToStr(ref byte[] btData, int len)
        {
            
            string strCardNo = "";
                        
            for (int i = 0; i < len; i++)
            {
                strCardNo += ByteToChar((byte)(btData[i] & 0xF));
                strCardNo += ByteToChar((byte)(btData[i] >> 4));
                
            }
            
            return strCardNo;
        }

        private char ByteToChar(byte btByte)
        {
            if (btByte >= 0 && btByte < 0xA)
                return (char)('0' + btByte);

            if (btByte == 0xA)
                return 'b';

            if (btByte == 0xB)
                return 'f';

            if (btByte == 0xC)
                return 'd';

            if (btByte == 0xD)
                return 'a';

            if (btByte == 0xE)
                return 'c';

            if (btByte == 0xF)
                return 'k';

            return 't';
        }

    }
}
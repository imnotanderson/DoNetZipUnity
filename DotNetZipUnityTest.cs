using UnityEngine;
using System.Collections;
using System.IO;

public class DotNetZipUnityTest : MonoBehaviour
{
    [ContextMenu("Test")]
    public void Test() {
        
        string str = "aaaaaaaaaaaaaaaaaaaaaaaaa";
        var data = System.Text.UTF8Encoding.UTF8.GetBytes(str);
        
        //zip
        MemoryStream ms = new MemoryStream(data, 0, data.Length);
        Ionic.Zlib.ZlibStream zip = new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Compress);
        var zipData = new byte[ms.Length];
        var len = zip.Read(zipData, 0, zipData.Length);
        zipData = GetBytes(zipData, len);
        ms.Close();
        zip.Close();
        Debug.Log(string.Format("zip:{0}->{1}", data.Length, zipData.Length));
        
        //unzip
        ms = new MemoryStream(zipData, 0, zipData.Length);
        Ionic.Zlib.ZlibStream unzip = new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Decompress);
        var unZipData = new byte[ms.Length];
        len = unzip.Read(unZipData, 0, unZipData.Length);
        unZipData = GetBytes(unZipData, len);
        var result = System.Text.UTF8Encoding.UTF8.GetString(unZipData);
        Debug.Log(result);
    }

    public static byte[] GetBytes(byte[] bytes, int len)
    {
        if (len >= bytes.Length)
        {
            return bytes;
        }
        var data = new byte[len];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = bytes[i];
        }
        return data;
    }
}

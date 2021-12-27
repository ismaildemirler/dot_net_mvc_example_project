using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Domain.Infrastructure.Utilities.Common
{
    public static class BarcodeUtility
    {
        public static byte[] GenerateBarcodeByteArray(string barcodeString)
        {
            //BarcodeControl b = new BarcodeControl();
            //b.Init();
            //b.Unlock("LB78CF-59ED0", "WSFCX-0100-36304982");
            //b.CurrentCode = 1006;
            //b.DataToEncode = barcodeString;
            //Bitmap barcode = b.GetCode(150f);
            //MemoryStream ms = new MemoryStream();
            //barcode.Save(ms, ImageFormat.Bmp);
            byte[] bitmapData = new byte[100]; //ms.ToArray();
            return bitmapData;
        }
    }
    public static class QrCodeUtility
    {
        public static byte[] GenerateQrCodeByteArray(string data)
        {
            QRCodeEncoder qrCodeEncode = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L,
                QRCodeVersion = 1
            };
            Bitmap qrCodeBitmap = qrCodeEncode.Encode(data);
            MemoryStream ms = new MemoryStream();
            qrCodeBitmap.Save(ms, ImageFormat.Bmp);
            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }
        public static byte[] GenerateQrCodeByteArrayByString(string data)
        {
            QRCodeEncoder qrCodeEncode = new QRCodeEncoder();
            Bitmap qrCodeBitmap = qrCodeEncode.Encode(data);
            MemoryStream ms = new MemoryStream();
            qrCodeBitmap.Save(ms, ImageFormat.Bmp);
            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }
    }
}

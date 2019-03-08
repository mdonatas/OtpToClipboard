using System;
using System.Linq;
using OtpNet;

namespace OtpToClipboard
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string otpKey = args.FirstOrDefault();
#if DEBUG
            if (string.IsNullOrEmpty(otpKey))
            {
                otpKey = "ASD";
            }
#endif

            var bytes = Base32Encoding.ToBytes(otpKey);

            var totp = new Totp(bytes, mode: OtpHashMode.Sha1);

            string result = totp.ComputeTotp();

            System.Windows.Forms.Clipboard.SetText(result);
        }
    }
}

using OtpNet;

namespace OtpToClipboard;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        string? otpKey = args.FirstOrDefault();
#if DEBUG
        if (string.IsNullOrEmpty(otpKey))
        {
            otpKey = "ASD";
        }
#endif

        var bytes = Base32Encoding.ToBytes(otpKey);

        var totp = new Totp(bytes, mode: OtpHashMode.Sha1);

        string result = totp.ComputeTotp();

        Clipboard.SetText(result);
    }
}
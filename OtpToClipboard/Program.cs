using OtpNet;

namespace OtpToClipboard;

internal static class Program
{
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

        var result = totp.ComputeTotp();

        try
        {
            Clipboard.Clear();
        }
        catch
        {
        }

        for (int i = 0; i < 3; i++)
        {
            try
            {
                Clipboard.SetText(result, TextDataFormat.Text);
                return;
            }
            catch
            {
                Thread.Sleep(200);
            }
        }
    }
}

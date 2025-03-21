using System;
using System.Windows;

namespace OpenKh.Tools.Kh2MapStudio
{
    internal class ClipboardService
    {
        internal static void GetText(Action<string> consumer, Action<Exception> onFailure = null)
        {
            try
            {
                consumer(Clipboard.GetText());
            }
            catch (Exception ex)
            {
                onFailure?.Invoke(ex);
            }
        }

        internal static void SetText(string text, Action onSuccess = null, Action<Exception> onFailure = null)
        {
            try
            {
                Clipboard.SetText(text);
                onSuccess?.Invoke();
            }
            catch (Exception ex)
            {
                onFailure?.Invoke(ex);
            }
        }
    }
}

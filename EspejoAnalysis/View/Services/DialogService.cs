using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace EspejoAnalysis.View.Services
{
    public class DialogService : IDialogService
    {
        private ProgressDialogController _controller;
        private MetroWindow MetroWindow => (MetroWindow)App.Current.MainWindow;
        public async Task<DialogResult> ShowOkCancelDialogAsync(string text, string title)
        {
            var result =
              await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);

            return result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
              ? DialogResult.OK
              : DialogResult.Cancel;
        }
        public async Task ShowInfoDialogAsync(string title, string text)
        {
            await MetroWindow.ShowMessageAsync(title, text);
        }

        public async Task ShowProgress(string title, string message)
        {
            _controller = await MetroWindow.ShowProgressAsync(title, message);
            _controller.Minimum = 0;
            _controller.Maximum = 100;
        }

        public void UpdateProgress(double progress)
        {
            if (_controller != null)
                _controller.SetProgress(progress);
        }

        public void UpdateMessageProgress(string message)
        {
            if (_controller != null)
                _controller.SetMessage(message);
        }

        public async Task CloseProgress()
        {
            if (_controller != null)
            {
                await _controller.CloseAsync();
                _controller = null;
            }
        }
    }
}

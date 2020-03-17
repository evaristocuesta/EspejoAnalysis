using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspejoAnalysis.View.Services
{
    public interface IDialogService
    {
        Task<DialogResult> ShowOkCancelDialogAsync(string text, string title);
        Task ShowInfoDialogAsync(string title, string text);
        Task ShowProgress(string title, string message);
        void UpdateProgress(double progress);
        void UpdateMessageProgress(string message);
        Task CloseProgress();
    }
}

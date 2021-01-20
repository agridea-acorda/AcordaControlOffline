using System;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using MediatR;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class Autosave : IDisposable
    {
        public int SaveIntervalInMsDefault = 3 * 1000; // three seconds
        public int SavedMessageDisplayLengthInMsDefault = 5 * 1000; // five seconds
        private Timer savingTimer_;
        private Timer savedTimer_;
        private SavingState state;
        private Task saveAction_;

        public SavingState State => state;

        public Autosave(Task saveAction)
        {
            saveAction_ = saveAction;
            savingTimer_ = new Timer(SaveIntervalInMsDefault) {AutoReset = false};
            savingTimer_.Elapsed += async (sender, e) =>
            {
                state = SavingState.Saving;
                await WaitForActionToComplete();
            };
            savedTimer_ = new Timer(SavedMessageDisplayLengthInMsDefault) {AutoReset = false};
            savedTimer_.Elapsed += (sender, e) => state = SavingState.Idle;
            state = SavingState.Idle;
        }

        private async Task WaitForActionToComplete()
        {
            await saveAction_;
            savedTimer_.Stop();
            savedTimer_.Start();
            state = SavingState.Displaying;
        }

        public void Save()
        {
            savingTimer_.Stop();
            savingTimer_.Start();
        }

        public void Dispose()
        {
            savingTimer_?.Dispose();
            savedTimer_?.Dispose();
        }

        public enum SavingState
        {
            Idle,
            Saving,
            Displaying
        }
    }
}

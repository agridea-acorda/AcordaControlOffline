using System;
using System.Threading.Tasks;
using System.Timers;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public class Autosave : IDisposable
    {
        public int SaveIntervalInMsDefault = 3 * 1000; // three seconds
        public int SavedMessageDisplayLengthInMsDefault = 5 * 1000; // five seconds
        private readonly Timer savingTimer_;
        private readonly Timer savedTimer_;
        private SavingState state;
        private readonly Func<ValueTask> saveAction_;

        public SavingState State => state;

        public Autosave(Func<ValueTask> saveAction)
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
            Console.WriteLine("Performing save action...");
            await saveAction_();
            Console.WriteLine("...Save action finished.");
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

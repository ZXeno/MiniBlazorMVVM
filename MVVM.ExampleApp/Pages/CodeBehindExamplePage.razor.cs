namespace MVVM.ExampleApp
{
    using MBMVVM;
    using System.Threading.Tasks;

    //
    // This example shows how to use the code-behind pattern with the ViewModelBase class
    // and implements a couple of button click methods to show the interaction between
    // the ViewModel and the ViewComponentBase objects.
    //
    public class CodeBehindExamplePageBase : ViewComponentBase<CodeBehindExampleViewModel>
    {
        /// <summary>
        /// Called when clicked by the "Cycle Value" button.
        /// </summary>
        public void OnCycleButtonClick()
        {
            this.ViewModel.CycleStringValues();
        }

        /// <summary>
        /// Called when clicked by the "Fire Async Call" button.
        /// </summary>
        /// <returns></returns>
        public async Task ExampleAsyncCall()
        {
            await this.ViewModel.ExmpleLongRunningTask();
        }
    }
}

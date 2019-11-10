namespace MVVM.ExampleApp
{
    using MBMVVM;
    using System.Threading;
    using System.Threading.Tasks;

    public class CodeBehindExampleViewModel : ViewModelBase
    {
        private int index = 0;
        private string myStringProperty;
        private string[] stringValues = new string[]
        {
            "Value 0",
            "Value 1",
            "Value 2",
            "Value 3"
        };
        private string currentCycleStringValue;

        public CodeBehindExampleViewModel()
        {
            this.myStringProperty = "My String Property! Change me to see what happens!";
            this.currentCycleStringValue = stringValues[0];
        }

        public string MyStringProperty
        {
            get => this.myStringProperty;
            set
            {
                this.OnPropertyChanging();
                this.myStringProperty = value;
                this.OnPropertyChanged();
            }
        }

        public string CurrentCycleStringValue
        {
            get => this.currentCycleStringValue;
            set
            {
                this.currentCycleStringValue = value;
                this.OnPropertyChanged();
            }
        }

        public void CycleStringValues()
        {
            if (this.index < this.stringValues.Length - 1)
            {
                this.index++;
            }
            else
            {
                this.index = 0;
            }

            this.CurrentCycleStringValue = stringValues[index];
        }

        public async Task ExmpleLongRunningTask()
        {
            await Task.Run(
                () =>
                {
                    // Sleep 2 seconds to fake a "long" process.
                    Thread.Sleep(2000);
                    lock (this.MyStringProperty)
                    {
                        this.MyStringProperty = "We made an example asynchronous call! This updates asynchronously without breaking SignalR connection when calling StateHasChanged.";
                    }
                });
        }
    }
}

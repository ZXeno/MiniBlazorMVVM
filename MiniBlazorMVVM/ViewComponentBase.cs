/*
 * MIT License
 *
 * Copyright(c) 2019 Jonathan Cain
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

namespace MBMVVM
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The MVVM extension for <see cref="ComponentBase"/> that handles <see cref="ViewModelBase"/> injection and passes <see cref="System.ComponentModel.INotifyPropertyChanged"/> events to the UI.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewComponentBase<T> : ComponentBase
        where T : notnull, ViewModelBase
    {
        /// <summary>
        /// The ViewModel that is injected into this <see cref="ViewComponentBase{T}"/>.
        /// </summary>
        [Inject]
        public T ViewModel { get; set;  }

        /// <summary>
        /// Override for base OnInitializeAsync method. If this method is overridden, be sure to call "await base.OnInitializeAsync()" first in your override method.
        /// Subscribes the <see cref="NotifyStateChanged"/> method to the <see cref="ViewModelBase"/>'s <see cref="ViewModelBase.PropertyChanged"/> event.
        /// </summary>
        /// <returns>The completed task.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.ViewModel.PropertyChanged += NotifyStateChanged;
        }

        /// <summary>
        /// Notifies the UI of state changes. Should be subscribed to the <see cref="ViewModelBase"/>'s <see cref="ViewModelBase.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender"><see cref="EventHandler"/> sender object</param>
        /// <param name="e"><see cref="EventArgs"/> of the event.</param>
        protected virtual void NotifyStateChanged(object sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}

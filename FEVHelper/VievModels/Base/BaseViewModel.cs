using System.ComponentModel;
using PropertyChanged;

namespace FEVHelper.VievModels
{
    /// <summary>
    /// A base view model that fires Property Changed exents as needed
    /// </summary>
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}

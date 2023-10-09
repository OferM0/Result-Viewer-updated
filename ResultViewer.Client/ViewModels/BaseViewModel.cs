using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ResultViewer.Client.ViewModels
{
    // Define the abstract class 'BaseViewModel' implementing 'INotifyPropertyChanged'
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Private field to store the 'IsBusy' flag (for showing loading/busy state)
        private bool isBusy = false;

        // Public property for 'IsBusy', which triggers property change notification
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                // Use the SetValue method to set the value and notify property changes
                SetValue(ref isBusy, value);
            }
        }

        // Event for property change notification, from the 'INotifyPropertyChanged' interface
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to invoke the property changed event for a specific property
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Invoke the PropertyChanged event, passing the property name
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Generic method to set a property value and raise the property changed event if necessary
        public void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            // Compare the old value (backingField) with the new value (value)
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;

            // If the value has changed, update the backing field and raise the property changed event
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        // Method to store data in application storage (e.g., preferences, settings, etc.)
        public void SetDataStorageValue<T>(string key, T value)
        {
            // Store the value in application storage (e.g., using Xamarin.Essentials.Preferences)
            Preferences.Default.Set(key, value);
        }

        // Method to retrieve data from application storage with a default value if not found
        public T GetDataStorageValue<T>(string key, T defaultValue)
        {
            // Retrieve the value from application storage (e.g., using Xamarin.Essentials.Preferences)
            return Preferences.Default.Get<T>(key, defaultValue);
        }
    }
}

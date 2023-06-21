using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AsgardCore.MVVM
{
    /// <summary>
    /// Abstract base class for ViewModels.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with the given property names.
        /// </summary>
        /// <param name="propertyNames">The names of the changed properties.</param>
        public void RaisePropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
                for (int i = 0; i < propertyNames.Length; i++)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyNames[i]));
        }

        /// <summary>
        /// Updates the variable's value by reference, and raises <see cref="PropertyChanged"/> event.
        /// If the new value equals with the old value, does nothing.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        /// <param name="memberName">Optional: the name of the property. Filled by compiler.</param>
        /// <returns>True, if there was change, otherwise false.</returns>
        public bool SetValue<T>(ref T variable, T value, [CallerMemberName] string memberName = "")
        {
            if (Equals(variable, value))
                return false;

            variable = value;
            RaisePropertyChanged(memberName);
            return true;
        }
    }
}

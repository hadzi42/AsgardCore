using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

namespace AsgardCore.Test
{
    public sealed class PropertyChangedListener : IDisposable
    {
        internal readonly Dictionary<string, bool> _PropertyCalls = new Dictionary<string, bool>();
        internal INotifyPropertyChanged _Model;

        public PropertyChangedListener(INotifyPropertyChanged model)
        {
            _Model = model;
            _Model.PropertyChanged += OnPropertyChanged;
        }

        public void Dispose()
        {
            if (_Model != null)
            {
                _Model.PropertyChanged -= OnPropertyChanged;
                _Model = null;
            }
        }

        /// <summary>
        /// Registers properties of the ViewModel to listen to <see cref="INotifyPropertyChanged.PropertyChanged"/> events.
        /// </summary>
        /// <param name="properties">The property names to listen to.</param>
        public void AddListener(params string[] properties)
        {
            foreach (string name in properties)
                _PropertyCalls[name] = false;
        }

        /// <summary>
        /// Checks whether the <see cref="INotifyPropertyChanged.PropertyChanged"/> event was raised for the given property.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        /// <param name="wasRaised">Should the event be raised.</param>
        public void Verify(string property, bool wasRaised)
        {
            if (_PropertyCalls.TryGetValue(property, out bool b))
            {
                if (b != wasRaised)
                    Assert.Fail("PropertyChanged event was {0}raised for: '{1}'", wasRaised ? "not " : "", property);
            }
            else
                throw new InvalidOperationException("Listener was not set for this property: " + property);
        }

        /// <summary>
        /// Checks whether the <see cref="INotifyPropertyChanged.PropertyChanged"/> event was raised for all of the registered properties.
        /// </summary>
        public void VerifyAllWereRaised()
        {
            foreach (KeyValuePair<string, bool> kvp in _PropertyCalls)
                if (!kvp.Value)
                    Assert.Fail("PropertyChanged event was not raised for: " + kvp.Key);
        }

        /// <summary>
        /// Checks whether the <see cref="INotifyPropertyChanged.PropertyChanged"/> event was not raised for any of the registered properties.
        /// </summary>
        public void VerifyNoneWereRaised()
        {
            foreach (KeyValuePair<string, bool> kvp in _PropertyCalls)
                if (kvp.Value)
                    Assert.Fail("PropertyChanged event was raised for: " + kvp.Key);
        }

        /// <summary>
        /// Sets all registered properties' internal "was event raised" states to false.
        /// </summary>
        public void ResetListeners()
        {
            foreach (string key in _PropertyCalls.Keys.ToList())
                _PropertyCalls[key] = false;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_PropertyCalls.ContainsKey(e.PropertyName))
                _PropertyCalls[e.PropertyName] = true;
        }
    }
}

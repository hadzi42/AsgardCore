using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace AsgardCore.MVVM
{
    /// <summary>
    /// Extension of <see cref="ObservableCollection{T}"/> with range-operations and natural sorting.
    /// </summary>
    public sealed class RangeObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// A static <see cref="NotifyCollectionChangedAction.Reset"/> parameter instance for <see cref="ObservableCollection{T}.CollectionChanged"/> events.
        /// </summary>
        internal static readonly NotifyCollectionChangedEventArgs ResetEventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

        private bool _IsFireEventEnabled = true;

        /// <summary>
        /// Sets if <see cref="ObservableCollection{T}.CollectionChanged"/> event should be raised after operations.
        /// If set to true from false, raises a <see cref="NotifyCollectionChangedAction.Reset"/> event.
        /// </summary>
        public bool IsFireEventEnabled
        {
            get { return _IsFireEventEnabled; }
            set
            {
                if (_IsFireEventEnabled == value)
                    return;

                _IsFireEventEnabled = value;
                OnCollectionChanged(ResetEventArgs);
            }
        }

        /// <summary>
        /// Creates a new <see cref="RangeObservableCollection{T}"/> instance.
        /// </summary>
        public RangeObservableCollection()
        { }

        /// <summary>
        /// Creates a new <see cref="RangeObservableCollection{T}"/> instance from the given list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        public RangeObservableCollection(List<T> list)
            : base(list)
        { }

        /// <summary>
        /// Creates a new <see cref="RangeObservableCollection{T}"/> instance from the given collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        public RangeObservableCollection(IEnumerable<T> collection)
            : base(collection)
        { }

        /// <summary>
        /// Adds the item to the collection using stable, natural sorting.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="selector">The selector to be used for natural sorting.</param>
        public void AddAndSort(T item, Func<T, string> selector)//todo optimize with insert
        {
            Items.Add(item);
            SortNatural(selector);
        }

        /// <summary>
        /// Adds the item to the collection using stable sorting.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="selector">The selector to be used for sorting.</param>
        public void AddAndSort<TKey>(T item, Func<T, TKey> selector)//todo optimize with insert
        {
            Items.Add(item);
            List<T> sorted = new List<T>(Items.OrderBy(selector));
            Items.Clear();
            AddRange(sorted);
        }

        /// <summary>
        /// Adds the given items to the collection and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Items.Add(item);
            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Adds the given items to the collection and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRange(IReadOnlyList<T> items)
        {
            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Adds the given items to the collection and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRange(T[] items)
        {
            int count = items.Length;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Adds the given items to the collection and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRange(List<T> items)
        {
            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Adds the given items to the collection using a stable, natural sorting and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRangeSorted(IEnumerable<T> items, Func<T, string> selector)
        {
            foreach (T item in items)
                Items.Add(item);
            SortNatural(selector);
        }

        /// <summary>
        /// Adds the given items to the collection using a stable, natural sorting and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void AddRangeSorted(List<T> items, Func<T, string> selector)
        {
            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);
            SortNatural(selector);
        }

        /// <summary>
        /// Removes all the items matching the given predicate.
        /// If there was at least one removed item, raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        /// <param name="predicate">The condition to remove by.</param>
        public void RemoveAll(Func<T, bool> predicate)
        {
            List<T> copy = new List<T>(Items);
            bool isAnyDeleted = false;

            for (int i = 0; i < copy.Count; i++)
                if (predicate(copy[i]))
                {
                    Items.Remove(copy[i]);
                    isAnyDeleted = true;
                }

            if (isAnyDeleted)
                OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes the given items from the collection and raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Items.Remove(item);
            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it.
        /// </summary>
        public void ReplaceAll(IEnumerable<T> items)
        {
            Items.Clear();
            foreach (T item in items)
                Items.Add(item);
            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it.
        /// </summary>
        public void ReplaceAll(IReadOnlyList<T> items)
        {
            Items.Clear();

            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it.
        /// </summary>
        public void ReplaceAll(T[] items)
        {
            Items.Clear();

            int count = items.Length;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it.
        /// </summary>
        public void ReplaceAll(List<T> items)
        {
            Items.Clear();

            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it using a stable, natural sorting.
        /// </summary>
        public void ReplaceAllSorted(IEnumerable<T> items, Func<T, string> selector)
        {
            Items.Clear();
            foreach (T item in items)
                Items.Add(item);
            SortNatural(selector);
        }

        /// <summary>
        /// Removes all items from the collection, then adds the given items to it using a stable, natural sorting.
        /// </summary>
        public void ReplaceAllSorted(List<T> items, Func<T, string> selector)
        {
            Items.Clear();

            int count = items.Count;
            for (int i = 0; i < count; i++)
                Items.Add(items[i]);

            SortNatural(selector);
        }

        /// <summary>
        /// Sorts the collection with the given <see cref="Comparison{T}"/> using a non-stable sorting.
        /// </summary>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for sorting.</param>
        public void Sort(Comparison<T> comparison)
        {
            List<T> sorted = new List<T>(Items);
            sorted.Sort(comparison);
            Items.Clear();
            AddRange(sorted);
        }

        /// <summary>
        /// Sorts the collection using stable, natural sorting.
        /// </summary>
        /// <param name="selector">The string-selector used for natural sorting.</param>
        public void SortNatural(Func<T, string> selector)
        {
            List<T> sorted = new List<T>(Items.OrderBy(selector, NaturalComparer.Instance));
            Items.Clear();
            AddRange(sorted);
        }

        /// <summary>
        /// Sorts the collection with the default comparer using a stable sorting.
        /// </summary>
        public void SortStable()
        {
            // If the Comparer is null, the DefaultComparer will be used.
            List<T> sorted = new List<T>(Items.OrderBy(Selectors.SelfSelector, null));
            Items.Clear();
            AddRange(sorted);
        }

        /// <summary>
        /// Sorts the collection with the given string-selector using a stable sorting.
        /// The null or <see cref="string.Empty"/> values are sorted to the end of the collection.
        /// </summary>
        public void SortStable(Func<T, string> selector)
        {
            List<T> sorted = new List<T>(Items.OrderBy(x => string.IsNullOrEmpty(selector(x))).ThenBy(selector));
            Items.Clear();
            AddRange(sorted);
        }

        /// <summary>
        /// Raises a <see cref="ObservableCollection{T}.CollectionChanged"/> event with <see cref="NotifyCollectionChangedAction.Reset"/> parameter to indicate, the collection has to be rebuilt.
        /// Does not consider the value of <see cref="IsFireEventEnabled"/>, the event is always raised.
        /// </summary>
        public void RebuildCollection()
        {
            base.OnCollectionChanged(ResetEventArgs);
        }

        /// <summary>
        /// Updates the collection according to the given parameters.
        /// Currently only adding and removing are supported.
        /// </summary>
        public void UpdateList(NotifyCollectionChangedEventArgs e)
        {
            bool original = _IsFireEventEnabled;
            _IsFireEventEnabled = false;
            bool wasUpdate = false;

            if (e.NewItems != null)
            {
                AddRange(e.NewItems.Cast<T>());
                wasUpdate = true;
            }

            if (e.OldItems != null)
            {
                RemoveRange(e.OldItems.Cast<T>());
                wasUpdate = true;
            }

            // Turn raising event back on only if it was active originally.
            if (original)
            {
                // Raise changed event only if there was a change.
                //todo optimize
                if (wasUpdate)
                    IsFireEventEnabled = true;
                else
                    _IsFireEventEnabled = true;
            }
        }

        /// <summary>
        /// If <see cref="IsFireEventEnabled"/> is true, raises the <see cref="ObservableCollection{T}.CollectionChanged"/> event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_IsFireEventEnabled)
                base.OnCollectionChanged(e);
        }
    }
}

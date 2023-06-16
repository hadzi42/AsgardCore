using System;
using System.Collections.Generic;
using System.Threading;

namespace AsgardCore
{
    /// <summary>
    /// Manages object instances.
    /// </summary>
    public static class ComponentProvider
    {
        internal static readonly Dictionary<string, object> _ComponentsWithName = new Dictionary<string, object>();
        internal static readonly Dictionary<Type, object> _Components = new Dictionary<Type, object>();

        private static readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Number of instances stored.
        /// </summary>
        public static int Count
        {
            get
            {
                using (new ReadLock(_Lock))
                {
                    return _Components.Count + _ComponentsWithName.Count;
                }
            }
        }

        internal static bool IsComponentRegisteredNull
        {
            get { return ComponentRegistered == null; }
        }

        /// <summary>
        /// Raised when a new instance is registered.
        /// The parameters: the ID (or null) and the new instance.
        /// </summary>
        public static event Action<string, object> ComponentRegistered;

        /// <summary>
        /// Adds an object by its type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="fireEvent">Optional: should it raise the <see cref="ComponentRegistered"/> event.</param>
        public static void Register<T>(T instance, bool fireEvent = true)
            where T : class
        {
            using (new WriteLock(_Lock))
            {
                _Components[typeof(T)] = instance;
            }
            if (fireEvent && ComponentRegistered != null)
                ComponentRegistered(null, instance);
        }

        /// <summary>
        /// Adds an object by its ID (name).
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The ID of the instance.</param>
        /// <param name="fireEvent">Optional: should it raise the <see cref="ComponentRegistered"/> event.</param>
        public static void Register<T>(T instance, string id, bool fireEvent = true)
            where T : class
        {
            using (new WriteLock(_Lock))
            {
                _ComponentsWithName[id] = instance;
            }
            if (fireEvent && ComponentRegistered != null)
                ComponentRegistered(id, instance);
        }

        /// <summary>
        /// Removes an object by its ID (name).
        /// </summary>
        /// <param name="id">The ID of the instance.</param>
        public static void Unregister(string id)
        {
            using (new WriteLock(_Lock))
            {
                _ComponentsWithName.Remove(id);
            }
        }

        /// <summary>
        /// Removes an object by its type.
        /// </summary>
        /// <param name="type">The type of the instance.</param>
        public static void Unregister(Type type)
        {
            using (new WriteLock(_Lock))
            {
                _Components.Remove(type);
            }
        }

        /// <summary>
        /// Removes an object instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void Unregister<T>(T instance)
            where T : class
        {
            using (new WriteLock(_Lock))
            {
                Type key = null;
                foreach (KeyValuePair<Type, object> kvp in _Components)
                    if (ReferenceEquals(kvp.Value, instance))
                    {
                        key = kvp.Key;
                        break;
                    }
                if (key != null)
                {
                    _Components.Remove(key);
                    return;
                }

                string key2 = null;
                foreach (KeyValuePair<string, object> kvp in _ComponentsWithName)
                    if (ReferenceEquals(kvp.Value, instance))
                    {
                        key2 = kvp.Key;
                        break;
                    }
                if (key2 != null)
                    _ComponentsWithName.Remove(key2);
            }
        }

        /// <summary>
        /// Removes all elements and clears <see cref="ComponentRegistered"/> registrations.
        /// </summary>
        public static void Clear()
        {
            using (new WriteLock(_Lock))
            {
                _Components.Clear();
                _ComponentsWithName.Clear();
                ComponentRegistered = null;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ComponentProvider"/> contains the item with given ID.
        /// </summary>
        public static bool Contains(string id)
        {
            using (new ReadLock(_Lock))
            {
                return _ComponentsWithName.ContainsKey(id);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ComponentProvider"/> contains the item with given type.
        /// </summary>
        public static bool Contains(Type type)
        {
            using (new ReadLock(_Lock))
            {
                if (_Components.ContainsKey(type))
                    return true;
                foreach (object o in _Components.Values)
                    if (type.IsInstanceOfType(o))
                        return true;
                foreach (object o in _ComponentsWithName.Values)
                    if (type.IsInstanceOfType(o))
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Finds the instance of the given type.
        /// If not found, returns null.
        /// </summary>
        public static T TryGet<T>()
            where T : class
        {
            using (new ReadLock(_Lock))
            {
                Type type = typeof(T);
                object c;
                if (_Components.TryGetValue(type, out c))
                    return (T)c;

                T t;
                foreach (object o in _ComponentsWithName.Values)
                {
                    t = o as T;
                    if (t != null)
                        return t;
                }

                foreach (object o in _Components.Values)
                {
                    t = o as T;
                    if (t != null)
                        return t;
                }

                return null;
            }
        }

        /// <summary>
        /// Finds the instance of the given ID.
        /// If not found, returns null.
        /// </summary>
        public static T TryGet<T>(string id)
            where T : class
        {
            using (new ReadLock(_Lock))
            {
                object o;
                _ComponentsWithName.TryGetValue(id, out o);
                return (T)o;
            }
        }
    }
}

# AsgardCore
Lightweight MVVM and other helpers for .NET projects, primarily on Windows platform.

Contains the following assemblies:
- **AsgardCore.dll:** common helpers and base classes.
- **AsgardCore.Test.dll:** test helpers.

## AsgardCore.dll
- Read/Write locks using `ReaderWriterLockSlim` as the common synchronizer.
- Selectors: they come handy in performance-critical scenarios instead of "lambdas".
- NaturalComparer: *Windows-only*, wraps the sorting used in "File Explorer" to managed code. Uses native shlwapi.dll under the hood.
- Binary serializer methods: helps to serialize collections in a very fast and efficient way.
- ComponentProvider: a common place to get/store object instances.
- IdManager: handles int32 IDs in a deterministic way.

### AsgardCore.MVVM
- ViewModelBase: minimalistic abstract View-Model base class.
- CommandBase: minimalistic abstract `ICommand` base class.
- DelegateCommand: for simple command implementations, without the need for separate command-classes.
- RangeObservableCollection: `ObservableCollection` with efficient "range" operations. Some sorting methods are for Windows only.
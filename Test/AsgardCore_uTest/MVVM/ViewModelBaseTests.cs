using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using AsgardCore.MVVM;
using AsgardCore.Test;
using NUnit.Framework;

namespace AsgardCore_uTest.MVVM
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class ViewModelBaseTests : IDisposable
    {
        private readonly TestViewModel _ViewModel = new TestViewModel();
        private PropertyChangedListener _Listener;

        [SetUp]
        public void Setup()
        {
            _Listener = new PropertyChangedListener(_ViewModel);
        }

        [TearDown]
        public void Dispose()
        {
            _Listener?.Dispose();
            _Listener = null;
        }

        [Test]
        public void ViewModelBase_Type_TypeImplementsINotifyPropertyChanged()
        {
            Assert.IsInstanceOf<INotifyPropertyChanged>(_ViewModel);
            Assert.IsFalse(typeof(ViewModelBase).IsSealed);
        }

        [Test]
        public void RaisePropertyChanged_NoEventHandler_DoesNothing()
        {
            // Remove event subscription.
            _Listener.Dispose();

            Assert.DoesNotThrow(() => _ViewModel.RaisePropertyChanged("TestProperty"));
        }

        [Test]
        public void RaisePropertyChanged_WithEventHandler_DoesNothing()
        {
            const string testProperty = "TestProperty";
            _Listener.AddListener(testProperty);

            _ViewModel.RaisePropertyChanged(testProperty);

            _Listener.VerifyAllWereRaised();
        }

        [Test]
        public void RaisePropertyChanged_ZeroProperty_DoesNothing()
        {
            bool wasRaised = false;
            PropertyChangedEventHandler handler = (o, e) => wasRaised = true;
            _ViewModel.PropertyChanged += handler;

            try
            {
                _ViewModel.RaisePropertyChanged();
                Assert.IsFalse(wasRaised);
            }
            finally
            {
                _ViewModel.PropertyChanged -= handler;
            }
        }

        [Test]
        public void RaisePropertyChanged_TwoProperties_RaisesTwoEvents()
        {
            const string testProperty1 = "TestProperty1";
            const string testProperty2 = "TestProperty2";
            _Listener.AddListener(testProperty1, testProperty2);

            _ViewModel.RaisePropertyChanged(testProperty1, testProperty2);

            _Listener.VerifyAllWereRaised();
        }

        [Test]
        public void SetValue_ToSameValue_DoesNothingAndReturnsFalse()
        {
            _Listener.AddListener("a", "b");

            // With value-type.
            int a = 42;
            bool isChanged = _ViewModel.SetValue(ref a, 42, "a");
            Assert.IsFalse(isChanged);
            Assert.AreEqual(42, a);
            _Listener.VerifyNoneWereRaised();

            // With reference-type.
            string b = "b";
            isChanged = _ViewModel.SetValue(ref b, "b", "b");
            Assert.IsFalse(isChanged);
            Assert.AreEqual("b", b);
            _Listener.VerifyNoneWereRaised();
        }

        [Test]
        public void SetValue_ToNewValue_RaisesPropertyChangedAndReturnsTrue()
        {
            _Listener.AddListener("a", "b");

            // With value-type.
            int a = 42;
            bool isChanged = _ViewModel.SetValue(ref a, 43, "a");
            Assert.IsTrue(isChanged);
            Assert.AreEqual(43, a);
            _Listener.Verify("a", true);
            _Listener.Verify("b", false);

            // With reference-type.
            _Listener.ResetListeners();
            string b = "b";
            isChanged = _ViewModel.SetValue(ref b, "c", "b");
            Assert.IsTrue(isChanged);
            Assert.AreEqual("c", b);
            _Listener.Verify("a", false);
            _Listener.Verify("b", true);
        }

        private sealed class TestViewModel : ViewModelBase
        { }
    }
}

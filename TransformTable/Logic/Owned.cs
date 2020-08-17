using System;

namespace Kladzey.TransformTable.Logic
{
    public readonly struct Owned<T> : IDisposable
    {
        private readonly Action? onDispose;

        public Owned(T value, Action? onDispose)
        {
            this.onDispose = onDispose;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public T Value { get; }

        public void Dispose()
        {
            onDispose?.Invoke();
        }
    }

    public static class Owned
    {
        public static Owned<T> From<T>(T disposable) where T : IDisposable
        {
            if (disposable == null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }

            return new Owned<T>(disposable, disposable.Dispose);
        }
    }
}
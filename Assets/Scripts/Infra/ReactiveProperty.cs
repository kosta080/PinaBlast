using System;

namespace Kor.Infra
{
    public class ReactiveProperty<T>
    {
        private T _value;
        public event Action<T> ValueChanged;
    
        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    ValueChanged?.Invoke(_value);
                }
            }
        }

        public ReactiveProperty() { }

        public ReactiveProperty(T initialValue)
        {
            _value = initialValue;
        }
    }
}
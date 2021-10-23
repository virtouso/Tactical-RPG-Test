using System;
public class Model<T>
{
    private T _data;
    public Action<T> Action;

    public T Data
    {
        get => _data;
        set
        {
            _data = value;
            Action?.Invoke(_data);
        }
    }


}

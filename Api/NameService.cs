namespace Api;

public interface INameService<T>
{
    string GetName(T obj);
}

public class NameService<T> : INameService<T>
{
    public string GetName(T obj) => obj?.GetType().Name ?? "null";
}


public class DetailedNameService<T> : INameService<T>
{
    public string GetName(T obj) => $"* {(obj?.GetType().Name ?? "null")} [{obj?.GetType().Assembly}]";
}

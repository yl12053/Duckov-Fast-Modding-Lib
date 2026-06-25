namespace FastModdingLib.Registry
{
    public interface IRegistry<T, U> where U: class
    {
        public U? Get(T key);
    }
}
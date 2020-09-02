namespace AX.Core.RunLog
{
    public interface ILoger
    {
        string Name { get; }

        void Info(string msg);

        void Error(string msg);

        void Waring(string msg);

        void Debug(string msg);
    }
}
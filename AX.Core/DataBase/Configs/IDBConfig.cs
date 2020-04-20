namespace AX.Core.DataBase.Configs
{
    public interface IDBConfig
    {
        string LeftEscapeChar { get; }

        string RightEscapeChar { get; }

        string DbParmChar { get; }
    }
}
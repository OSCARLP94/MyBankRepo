
namespace CommonDataModels.Enums
{

    /// <summary>
    /// Enum de tipos de transacciones asociados a id en tabla generica
    /// </summary>
    public enum EnumTypeTransactions
    {
        None = 0,

        /// <summary>
        /// Retiro
        /// </summary>
        WithDrawal = 1,

        /// <summary>
        /// Consignacion
        /// </summary>
        Deposit =2,

        /// <summary>
        /// Transferencia fondos
        /// </summary>
        FundsTransfer =3
    }

    public record TypeTransactionRecord(string Code, EnumTypeTransactions IdEnum);

    /// <summary>
    /// records para hecer uso de inmutabilidad
    /// </summary>
    public static class RecordsTypeTransactions
    {
        public static TypeTransactionRecord WithDrawalRecord = new TypeTransactionRecord("RetiroCod", EnumTypeTransactions.WithDrawal);
        public static TypeTransactionRecord DepositRecord = new TypeTransactionRecord("ConsignaCod", EnumTypeTransactions.Deposit);
        public static TypeTransactionRecord FundsTransferRecord = new TypeTransactionRecord("TransferenciaCodInt", EnumTypeTransactions.FundsTransfer);
    }
}

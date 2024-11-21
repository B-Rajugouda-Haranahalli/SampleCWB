namespace CWB.Masters.ViewModels.OperationList
{
    public class OperationVM
    {
        public long OperationId { get; set; }
        public string Operation { get; set; }
        public bool IsMultiplePartsOfBOMUsed { get; set; }
        public long TenantId { get; set; }
    }
}

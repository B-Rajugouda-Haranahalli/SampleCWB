namespace CWB.Masters.ViewModels.OperationList
{
    public class OperationListVM
    {
        public long OperationId { get; set; }

        public bool IsMultiplePartsOfBOMUsed { get; set; }
        public int Inhouse { get; set; }
        public int Subcon { get; set; }
        public string Operation { get; set; }

        public int TenantId {  get; set; }  
    }
}

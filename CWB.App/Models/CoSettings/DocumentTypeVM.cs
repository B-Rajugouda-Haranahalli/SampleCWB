namespace CWB.App.Models.CoSettings
{
    public class DocumentTypeVM
    {
        public long DocumentTypeId { get; set; }
        public string Name { get; set; }
        public long TenantId { get; set; }
        public bool IsUploadedByUser { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
    }
}

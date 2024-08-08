namespace DRF.ViewModels
{
    public class EmailAttachmentModel
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}

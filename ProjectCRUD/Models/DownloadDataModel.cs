namespace ProjectCRUD.Models
{
    public class DownloadDataModel
    {
        public int Id { get; set; }
        public int Book_Id { get; set; }
        public int Mem_Id { get; set; }
        public int DownloadCount { get; set; }

        public DownloadDataModel()
        {
            Id = 0;
            Book_Id = 0;
            Mem_Id = 0;
            DownloadCount = 0;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA.Model
{


    public class ImageApplication
    {
        //out byte[] ImageApp, out List<byte[]> ImageAppAdditional

        public DateTime? DocumentDate { get; set; }
        public string GuIdKey { get; set; }

        public long? ImageId { get; set; }

        public int Pages { get; set; }

        public List<DataFileInfo> DataFiles { get; set; }

    }

    public class ImageAddtionalApplication
    {
        public int Pages { get; set; }

        //out byte[] ImageApp, out List<byte[]> ImageAppAdditional

        public DateTime? DocumentDate { get; set; }

        public string ConntentIdKey { get; set; }

        public long? ImageId { get; set; }

        public DataFileInfo DataFile { get; set; }

        public string StatusType { get; set; }

        public string SubStatusType { get; set; }

    }

    public class ImageApplicationDocument
    {

        public ImageApplication ImageApp { get; set; }

        public List<ImageAddtionalApplication> ImageAppAditonal { get; set; }

    }

    public class DataFileInfo
    {
        public string ContentType { get; set; }
        public byte[] FileByte { get; set; }
        public string RefID { get; set; }

    }




    //public class ImageApplication
    //{
    //    //out byte[] ImageApp, out List<byte[]> ImageAppAdditional

    //    public DateTime? DocumentDate { get; set; }
    //    public string GuIdKey { get; set; }

    //    public long? ImageId { get; set; }

    //    public List<DataFileInfo> DataFiles { get; set; }

    //}


    //public class ImageAddtionalApplication
    //{
    //    //out byte[] ImageApp, out List<byte[]> ImageAppAdditional

    //    public DateTime? DocumentDate { get; set; }

    //    public string ConntentIdKey { get; set; }

    //    public long? ImageId { get; set; }

    //    public DataFileInfo DataFile { get; set; }

    //    public string StatusType { get; set; }

    //    public string SubStatusType { get; set; }


    //}


    //public class ImageApplicationDocument
    //{

    //    public ImageApplication ImageApp { get; set; }

    //    public List<ImageAddtionalApplication> ImageAppAditonal { get; set; }

    //}

    //public class DataFileInfo
    //{
    //    public string ContentType { get; set; }
    //    public byte[] FileByte { get; set; }

    //}
}

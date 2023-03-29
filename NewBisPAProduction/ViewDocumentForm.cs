using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ITUtility;
using Microsoft.Win32;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Configuration;
using NewBisPA.NewBISSvcRef;
using NewBisPA.IsisAppTransportService;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using NewBisPA.Library;
//using NewBIS.DmsExtCallService;

namespace NewBisPA
{
    public partial class ViewDocumentForm : Form
    {
        private static Image[] arrimg;
        private int _pageCount;
        private PictureBox[] oPictureBox;
        private Panel[] oPanel;
        private Label[] oLabel;
        private int numberPicbox;
        private Image myImage;
        private Image myImagesave;
        private string checkZoom = "normal";
        NewBISSvcRef.GET_DATA_SCAN_COLLECTION getDataScanColl = new NewBISSvcRef.GET_DATA_SCAN_COLLECTION();
        NewBISSvcRef.U_CUSTNAME uCustnameObj = new NewBISSvcRef.U_CUSTNAME();
        CustomClass.DOC_ISISSHOW_Collection docIsisColls = new CustomClass.DOC_ISISSHOW_Collection();
        private int glbScrollBarValue = 0;//หน้าที่กำลังแสดงผลของ hscollbar
        private int glbMaximumPicture = 0;//จำนวนที่แสดงทั้งหมดต่อ 1 หน้า
        private int glbstartPicture = 0;//รูปเริ่มต้นของหน้านั้นใน array
        private int ArryIndex = 0;//รูปภาพที่กำลังเลือก picture selected ตำแหน่งที่กำลังเลือกรูปแสดงในArray 0-...
        private string fileNameTifSelected = ""; //
        private bool firstTimeRotrate = true;//ครั้งแรกที่คลิกRotate

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


        private string _AppNo;

        public string AppNo
        {
            get { return _AppNo; }
            set { _AppNo = value; }
        }




        private string _ChannelType;

        public string ChannelType
        {
            get { return _ChannelType; }
            set { _ChannelType = value; }
        }


        public long _appId;

        public ViewDocumentForm(long appId, string appNo, string channelType, string userId)//ใช้อันนี้
        {
            InitializeComponent();
            _appId = appId;
            _AppNo = appNo;
            _ChannelType = channelType;
            _UserID = "000000";// userId;
        }

        public ViewDocumentForm(string appId)//ใช้อันนี้
        {
            InitializeComponent();
            _appId = Convert.ToInt64(appId);
        }

        public ViewDocumentForm()
        {
            InitializeComponent();
        }

        private int pageIndex;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        void AddForm_MouseClick(object sender, MouseEventArgs e)
        {
            ArryIndex = Array.IndexOf(oPictureBox, (PictureBox)sender);
            ShowPicture();
        }

        private void ShowPicture()
        {
            numberPicbox = ArryIndex + (hScrollBar1.Value * glbMaximumPicture);

            System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension(myImagesave.FrameDimensionsList[0]);
            myImagesave.SelectActiveFrame(frameDimensions, numberPicbox);
            Image _picture = (Bitmap)myImagesave.Clone();
            pictureBox1.Image = _picture;

            PageIndex = numberPicbox;
            int iMaxArry = 0;
            iMaxArry = (glbstartPicture + glbMaximumPicture) > _pageCount ? glbMaximumPicture - ((glbstartPicture + glbMaximumPicture) - _pageCount) : glbMaximumPicture;

            for (int i = 0; i < iMaxArry; i = i + 1)
            {
                oPanel[i].BackColor = Color.Transparent;
                oLabel[i].ForeColor = Color.Black;
            }
            try
            {
                oPanel[ArryIndex].BackColor = SystemColors.Highlight;
                oLabel[ArryIndex].ForeColor = Color.White;
            }
            catch
            {
            }

            float docHeight = 0;
            float docWidth = 0;

            if ((_picture.Width / _picture.Height) > ((tableLayoutPanel1.Width - 15) / (tableLayoutPanel1.Height - 264)))
            {
                docHeight = _picture.Height * (tableLayoutPanel1.Width - 15) / _picture.Width;
                docWidth = _picture.Width * (tableLayoutPanel1.Width - 15) / _picture.Width;
            }
            else
            {
                docHeight = _picture.Height * (tableLayoutPanel1.Height - 264) / _picture.Height;
                docWidth = _picture.Width * (tableLayoutPanel1.Height - 264) / _picture.Height;
            }

            pictureBox1.Width = (int)Math.Ceiling(docWidth);
            pictureBox1.Height = (int)Math.Ceiling(docHeight);
            groupBox1.Text = "แผ่น " + (numberPicbox + 1) + " จาก " + _pageCount + " แผ่น";
            firstTimeRotrate = true;
        }

        private void clearData()
        {
            myImage = null;
            myImagesave = null;
            myImage.Dispose();
            myImagesave.Dispose();
            pictureBox1.Image = null;
            panelPicture.Controls.Clear();
            int i = 0;
            for (i = 0; i < _pageCount; i = i + 1)
            {
                oPictureBox[i].Image = null;
            }
            _pageCount = 0;
            //j = 0; 
            getDataScanColl = null;
            uCustnameObj = null;
            docIsisColls = null;
            groupBox1.Text = "รายการภาพเอกสาร";
            arrimg = null;

            glbScrollBarValue = 0;
            glbMaximumPicture = 0;
            glbstartPicture = 0;

        }

        private void picLoad(Image myImage)
        {
            System.Drawing.Size _size = new Size();
            _size.Width = 100;
            _size.Height = 100;

            this.Cursor = Cursors.WaitCursor;

            _pageCount = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);

            glbMaximumPicture = Convert.ToInt16(Math.Floor(Convert.ToDouble(panelPicture.Width / 133)));

            if (_pageCount < glbMaximumPicture)
            {
                glbMaximumPicture = _pageCount;
            }

            if (_pageCount == glbMaximumPicture)
            {
                hScrollBar1.Maximum = 0;
            }
            else
            {
                if (_pageCount % glbMaximumPicture == 0)
                {
                    hScrollBar1.Maximum = (_pageCount / glbMaximumPicture) - 1;
                }
                else
                {
                    hScrollBar1.Maximum = Convert.ToInt16(Math.Floor(Convert.ToDouble(_pageCount / glbMaximumPicture)));
                }
            }


            progressBar1.Maximum = _pageCount;
            progressBar1.Minimum = 0;
            progressBar1.Width = 293;
            progressBar1.Visible = true;

            oPictureBox = new PictureBox[glbMaximumPicture];
            oPanel = new Panel[glbMaximumPicture];
            oLabel = new Label[glbMaximumPicture];
            arrimg = new Image[_pageCount];
            panelPicture.Controls.Clear();

            for (int i = 0; i < _pageCount; i = i + 1)//เก็บรูปภาพเล็กทั้งหมด
            {
                progressBar1.Value = i;
                System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension(myImage.FrameDimensionsList[0]);
                myImage.SelectActiveFrame(frameDimensions, i);
                arrimg[i] = (resizeImage((Bitmap)myImage.Clone(), _size));

            }
            progressBar1.Visible = false;
            progressBar1.Maximum = glbMaximumPicture;

            for (int i = 0; i < glbMaximumPicture; i = i + 1)
            {
                progressBar1.Value = i;

                oPictureBox[i] = new PictureBox();
                oPanel[i] = new Panel();
                oLabel[i] = new Label();

                panelPicture.Controls.Add(oPictureBox[i]);
                panelPicture.Controls.Add(oPanel[i]);
                oPanel[i].Controls.Add(oLabel[i]);

                oPictureBox[i].Left = 13 + ((112 + 21) * i);
                oPictureBox[i].Top = 10;
                oPictureBox[i].Height = 138;
                oPictureBox[i].Width = 112;

                oPanel[i].Left = 7 + ((112 + 21) * i);
                oPanel[i].Top = 6;
                oPanel[i].Height = 162;
                oPanel[i].Width = 124;
                oPanel[i].BackColor = Color.Transparent;

                oLabel[i].Left = 38;
                oLabel[i].Top = 144;
                oLabel[i].Height = 13;
                oLabel[i].AutoEllipsis = true;
                oLabel[i].Text = "Page " + (i + 1).ToString();

                //System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension(myImage.FrameDimensionsList[0]);

                //myImage.SelectActiveFrame(frameDimensions, i);
                //oPictureBox[i].Image = (resizeImage((Bitmap)myImage.Clone(), _size));
                //oPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                //oPictureBox[i].Refresh();
                //oPictureBox[i].MouseClick += new MouseEventHandler(AddForm_MouseClick);
                //progressBar1.Visible = false; 

                oPictureBox[i].Image = arrimg[i];
                oPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                oPictureBox[i].Refresh();
                oPictureBox[i].MouseClick += new MouseEventHandler(AddForm_MouseClick);

            }
            progressBar1.Visible = false;
            ArryIndex = 0;
            oPanel[ArryIndex].BackColor = SystemColors.Highlight;
            oLabel[ArryIndex].ForeColor = Color.White;

            panel1.Height = tableLayoutPanel1.Height - 264;
            panel1.Width = tableLayoutPanel1.Width;

            numberPicbox = 0;
            System.Drawing.Imaging.FrameDimension frameDimensionsShow = new System.Drawing.Imaging.FrameDimension(myImage.FrameDimensionsList[0]);

            myImage.SelectActiveFrame(frameDimensionsShow, numberPicbox);
            pictureBox1.Image = (Bitmap)myImage.Clone();

            float docHeight = 0;
            float docWidth = 0;

            if ((pictureBox1.Image.Width / pictureBox1.Image.Height) > ((tableLayoutPanel1.Width - 15) / (tableLayoutPanel1.Height - 264)))
            {
                docHeight = pictureBox1.Image.Height * (tableLayoutPanel1.Width - 15) / pictureBox1.Image.Width;
                docWidth = pictureBox1.Image.Width * (tableLayoutPanel1.Width - 15) / pictureBox1.Image.Width;
            }
            else
            {
                docHeight = pictureBox1.Image.Height * (tableLayoutPanel1.Height - 264) / pictureBox1.Image.Height;
                docWidth = pictureBox1.Image.Width * (tableLayoutPanel1.Height - 264) / pictureBox1.Image.Height;
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();
            pictureBox1.Width = (int)Math.Ceiling(docWidth);
            pictureBox1.Height = (int)Math.Ceiling(docHeight);


            groupBox1.Text = "แผ่น 1 จาก " + _pageCount + " แผ่น";
            firstTimeRotrate = true;
            this.Cursor = Cursors.Default;
        }

        private void picLoadFromPosition(Image myImage)
        {
            System.Drawing.Size _size = new Size();
            _size.Width = 100;
            _size.Height = 100;

            this.Cursor = Cursors.WaitCursor;

            //Clear picture===============================
            for (int i = 0; i < _pageCount; i = i + 1)
            {
                try
                {
                    oPictureBox[i].Image = null;
                    oLabel[i].Text = "";
                }
                catch
                {
                    break;
                }
            }


            oPictureBox = new PictureBox[glbMaximumPicture];
            oPanel = new Panel[glbMaximumPicture];
            oLabel = new Label[glbMaximumPicture];
            panelPicture.Controls.Clear();

            int iPosition = 0;
            int iMaxArry = 0;
            iPosition = glbstartPicture;
            iMaxArry = (glbstartPicture + glbMaximumPicture) > _pageCount ? glbMaximumPicture - ((glbstartPicture + glbMaximumPicture) - _pageCount) : glbMaximumPicture;

            progressBar1.Maximum = iMaxArry;
            progressBar1.Visible = true;

            for (int i = 0; i < iMaxArry; i = i + 1)
            {
                progressBar1.Value = i;
                oPictureBox[i] = new PictureBox();
                oPanel[i] = new Panel();
                oLabel[i] = new Label();

                panelPicture.Controls.Add(oPictureBox[i]);
                panelPicture.Controls.Add(oPanel[i]);
                oPanel[i].Controls.Add(oLabel[i]);

                oPictureBox[i].Left = 13 + ((112 + 21) * (i));
                oPictureBox[i].Top = 10;
                oPictureBox[i].Height = 138;
                oPictureBox[i].Width = 112;

                oPanel[i].Left = 7 + ((112 + 21) * (i));
                oPanel[i].Top = 6;
                oPanel[i].Height = 162;
                oPanel[i].Width = 124;
                oPanel[i].BackColor = Color.Transparent;

                oLabel[i].Left = 38;
                oLabel[i].Top = 144;
                oLabel[i].Height = 13;
                oLabel[i].AutoEllipsis = true;
                oLabel[i].Text = "Page " + (iPosition + 1).ToString();

                oPictureBox[i].Image = arrimg[iPosition];
                oPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                oPictureBox[i].Refresh();
                oPictureBox[i].MouseClick += new MouseEventHandler(AddForm_MouseClick);

                iPosition = iPosition + 1;
            }

            try
            {
                oPanel[ArryIndex].BackColor = SystemColors.Highlight;
                oLabel[ArryIndex].ForeColor = Color.White;
                numberPicbox = ArryIndex + (hScrollBar1.Value * glbMaximumPicture);
            }
            catch
            {
                ArryIndex = 0;
                numberPicbox = ArryIndex + (hScrollBar1.Value * glbMaximumPicture);
                oPanel[ArryIndex].BackColor = SystemColors.Highlight;
                oLabel[ArryIndex].ForeColor = Color.White;
            }

            progressBar1.Visible = false;

            panel1.Height = tableLayoutPanel1.Height - 264;
            panel1.Width = tableLayoutPanel1.Width;
            firstTimeRotrate = true;
            this.Cursor = Cursors.Default;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        //private static Image resizeImage(Image imgToResize, Size size)
        //{
        //    int sourceWidth = imgToResize.Width;
        //    int sourceHeight = imgToResize.Height;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)size.Width / (float)sourceWidth);
        //    nPercentH = ((float)size.Height / (float)sourceHeight);

        //    if (nPercentH < nPercentW)
        //        nPercent = nPercentH;
        //    else
        //        nPercent = nPercentW;

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap b = new Bitmap(destWidth, destHeight);
        //    Graphics g = Graphics.FromImage((Image)b);
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        //    g.Dispose();

        //    return (Image)b;
        //}

        private void btnRotradeRigth_Click(object sender, EventArgs e)
        {
            Image image1;
            image1 = pictureBox1.Image;
            image1.RotateFlip(RotateFlipType.Rotate90FlipNone);

            float docHeight = (image1.Height / image1.VerticalResolution);
            float docWidth = (image1.Width / image1.HorizontalResolution);
            pictureBox1.Width = (int)Math.Ceiling(docWidth) * 55;
            pictureBox1.Height = (int)Math.Ceiling(docHeight) * 55;

            pictureBox1.Image = image1;
            oPictureBox[numberPicbox].Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();
        }

        private void btnRotradeLeft_Click(object sender, EventArgs e)
        {
            Image image1;
            image1 = pictureBox1.Image;
            image1.RotateFlip(RotateFlipType.Rotate270FlipNone);

            float docHeight = (image1.Height / image1.VerticalResolution);
            float docWidth = (image1.Width / image1.HorizontalResolution);
            pictureBox1.Width = (int)Math.Ceiling(docWidth) * 55;
            pictureBox1.Height = (int)Math.Ceiling(docHeight) * 55;

            pictureBox1.Image = image1;
            oPictureBox[numberPicbox].Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();
        }

        private void btnZoomin_Click(object sender, EventArgs e)
        {
            pictureBox1.Height += 40;
            pictureBox1.Width += 40;

            //zoomWidth = zoomWidth + 50;
            //zoomHeight = zoomHeight + 50;
            //pictureBox1.Width = zoomWidth;
            //pictureBox1.Height = zoomHeight;
        }

        private void btnSlideUp_Click(object sender, EventArgs e)
        {
            Image Upimage;
            Image Downimage;
            if (numberPicbox != 0) //move else don't move
            {
                Upimage = oPictureBox[numberPicbox].Image;
                Downimage = oPictureBox[numberPicbox - 1].Image;
                oPictureBox[numberPicbox - 1].Image = Upimage;
                oPictureBox[numberPicbox].Image = Downimage;
                numberPicbox = numberPicbox - 1;

                for (int i = 0; i < _pageCount; i = i + 1)
                {
                    oPanel[i].BackColor = Color.Transparent;
                    oLabel[i].ForeColor = Color.Black;
                }

                oPanel[numberPicbox].BackColor = SystemColors.Highlight;
                oLabel[numberPicbox].ForeColor = Color.White;
            }
        }

        private void btnSlideDown_Click(object sender, EventArgs e)
        {
            Image Upimage;
            Image Downimage;
            if ((numberPicbox != (_pageCount - 1)) && (_pageCount != 0)) //move else don't move
            {
                Downimage = oPictureBox[numberPicbox].Image;
                Upimage = oPictureBox[numberPicbox + 1].Image;
                oPictureBox[numberPicbox].Image = Upimage;
                oPictureBox[numberPicbox + 1].Image = Downimage;
                numberPicbox = numberPicbox + 1;

                for (int i = 0; i < _pageCount; i = i + 1)
                {
                    oPanel[i].BackColor = Color.Transparent;
                    oLabel[i].ForeColor = Color.Black;
                }

                oPanel[numberPicbox].BackColor = SystemColors.Highlight;
                oLabel[numberPicbox].ForeColor = Color.White;
            }
        }

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void rotateRight90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1;
            if (firstTimeRotrate == true)
            {
                byte[] imgDataNow = System.IO.File.ReadAllBytes(fileNameTifSelected);
                MemoryStream ms = new MemoryStream(imgDataNow);
                Image myImagenow = Image.FromStream(ms);
                System.Drawing.Imaging.FrameDimension frameDimensionsNow = new System.Drawing.Imaging.FrameDimension(myImagenow.FrameDimensionsList[0]);
                myImagenow.SelectActiveFrame(frameDimensionsNow, numberPicbox);
                image1 = (Bitmap)myImagenow;
                image1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                firstTimeRotrate = false;
            }
            else
            {
                image1 = pictureBox1.Image;
                image1.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            float docHeight = (image1.Height / image1.VerticalResolution);
            float docWidth = (image1.Width / image1.HorizontalResolution);
            pictureBox1.Width = (int)Math.Ceiling(docWidth) * 55;
            pictureBox1.Height = (int)Math.Ceiling(docHeight) * 55;

            pictureBox1.Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();

        }

        private void rotateLeft90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1;
            if (firstTimeRotrate == true)
            {
                byte[] imgDataNow = System.IO.File.ReadAllBytes(fileNameTifSelected);
                MemoryStream ms = new MemoryStream(imgDataNow);
                Image myImagenow = Image.FromStream(ms);
                System.Drawing.Imaging.FrameDimension frameDimensionsNow = new System.Drawing.Imaging.FrameDimension(myImagenow.FrameDimensionsList[0]);
                myImagenow.SelectActiveFrame(frameDimensionsNow, numberPicbox);
                image1 = (Bitmap)myImagenow;
                image1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                firstTimeRotrate = false;
            }
            else
            {
                image1 = pictureBox1.Image;
                image1.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            float docHeight = (image1.Height / image1.VerticalResolution);
            float docWidth = (image1.Width / image1.HorizontalResolution);
            pictureBox1.Width = (int)Math.Ceiling(docWidth) * 55;
            pictureBox1.Height = (int)Math.Ceiling(docHeight) * 55;

            pictureBox1.Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();
        }

        private void rotate180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image1;
            if (firstTimeRotrate == true)
            {
                byte[] imgDataNow = System.IO.File.ReadAllBytes(fileNameTifSelected);
                MemoryStream ms = new MemoryStream(imgDataNow);
                Image myImagenow = Image.FromStream(ms);
                System.Drawing.Imaging.FrameDimension frameDimensionsNow = new System.Drawing.Imaging.FrameDimension(myImagenow.FrameDimensionsList[0]);
                myImagenow.SelectActiveFrame(frameDimensionsNow, numberPicbox);
                image1 = (Bitmap)myImagenow;
                image1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                firstTimeRotrate = false;
            }
            else
            {
                image1 = pictureBox1.Image;
                image1.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            float docHeight = (image1.Height / image1.VerticalResolution);
            float docWidth = (image1.Width / image1.HorizontalResolution);
            pictureBox1.Width = (int)Math.Ceiling(docWidth) * 55;
            pictureBox1.Height = (int)Math.Ceiling(docHeight) * 55;

            pictureBox1.Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Refresh();
        }

        private void zoominToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            checkZoom = "zoomin";
            this.Cursor = Cursors.SizeAll;
        }

        private void zoomoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkZoom = "zoomout";
            this.Cursor = Cursors.Cross;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            pictureBox1.Height -= 40;
            pictureBox1.Width -= 40;
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkZoom = "normal";
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (checkZoom == "zoomout")
            {
                pictureBox1.Height -= 100;
                pictureBox1.Width -= 100;
            }
            else if (checkZoom == "zoomin")
            {
                pictureBox1.Height += 100;
                pictureBox1.Width += 100;
            }
        }

        private string searchGuid()
        {
            string guid = "";

            if (_appId.ToString() != "")
            {
                NewBISSvcRef.ProcessResult pr3 = new NewBISSvcRef.ProcessResult();
                using (NewBISSvcClient client = new NewBISSvcClient())
                {
                    try
                    {
                        guid = client.GetGUID(out pr3, _appId);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (client.State != System.ServiceModel.CommunicationState.Closed)
                        {
                            client.Close();
                        }
                    }
                }
            }
            return guid;
        }

        private void ViewDocumentForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            using (NewBISSvcClient client = new NewBISSvcClient())
            {
                try
                {
                    // "ค้นหาใบคำขอ IMS"
                    var clientNewBis = new NewBisPASvcRef.NewBisPASvcClient();
                    NewBisPASvcRef.U_NEWBIS_SCAN[] dataObjects;
                    var prNewbis = clientNewBis.GetImageIdsOfApplicationAllDocument(out dataObjects, AppNo, ChannelType);
                    if (!prNewbis.Successed)
                    {
                        throw new Exception(prNewbis.ErrorMessage);
                    }

                    if (dataObjects != null && dataObjects.Any())
                    {
                        foreach (var item in dataObjects)
                        {

                            var imageId = item.IMAGE_ID?.ToString();
                            ITUtility.DataFile fileItem;
                            var applicationId = ITUtility.Utility.AppSettings("ApplicationId");
                            var clientImg = new ImageManagementSvcRef.ImagemanagementSvcClient();
                            var prImg = clientImg.DOWNLOAD_FILE_BY_TAG(out fileItem, UserID, imageId, applicationId, ImageManagementSvcRef.EnumContractDownloadType.forPrint);
                            if (!prImg.Successed)
                            {
                                throw new Exception(prImg.ErrorMessage);
                            }

                            if (fileItem.ContentType == MediaTypeNames.Application.Pdf)
                            {
                                var path = ConvertPDFToTiff(imageId, fileItem.FileData);
                                CustomClass.DOC_ISISSHOW docisis = new CustomClass.DOC_ISISSHOW();
                                docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
                                docisis.IDDOC = imageId;
                                docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(item.SCAN_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                                docIsisColls.Add(docisis);
                            }
                            else if (fileItem.ContentType == MediaTypeNames.Image.Tiff)
                            {
                                MemoryStream ms = new MemoryStream(fileItem.FileData);
                                myImage = Image.FromStream(ms);
                                ms.Close();
                                ms.Dispose();
                                CustomClass.DOC_ISISSHOW docisis = new CustomClass.DOC_ISISSHOW();
                                docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
                                docisis.IDDOC = imageId;
                                docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(item.SCAN_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                                docIsisColls.Add(docisis);

                                using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\WINDOWS\Temp\" + imageId + ".tif", FileMode.Create)))
                                {
                                    binWriter.Write(fileItem.FileData);
                                }
                            }
                        }
                    }
                    else
                    {
                        #region "ค้นหาใบคำขอ ISIS" 
                        U_APP_ISIS[] uAppISIS;
                        long[] app_ids = { _appId };
                        var pr = client.GetU_APP_ISIS(out uAppISIS, app_ids);
                        if (uAppISIS != null)
                        {
                            foreach (U_APP_ISIS uapp in uAppISIS)
                            {
                                if (uapp.GUID != null)
                                {
                                    #region Main Document
                                    using (AppTransportServiceContractClient clientISIS = new AppTransportServiceContractClient())
                                    {
                                        try
                                        {
                                            byte[] imageByte = clientISIS.GetAppFormContentByID(uapp.GUID);

                                            if (imageByte != null)
                                            {
                                                MemoryStream ms = new MemoryStream(imageByte);
                                                myImage = Image.FromStream(ms);
                                                ms.Close();
                                                ms.Dispose();
                                                CustomClass.DOC_ISISSHOW docisis = new CustomClass.DOC_ISISSHOW();
                                                docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
                                                docisis.IDDOC = uapp.GUID;
                                                docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(uapp.IMPORT_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                                                docIsisColls.Add(docisis);

                                                using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\WINDOWS\Temp\" + uapp.GUID + ".tif", FileMode.Create)))
                                                {
                                                    binWriter.Write(imageByte);
                                                }
                                            }
                                            imageByte = null;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                        finally
                                        {
                                            if (clientISIS.State != System.ServiceModel.CommunicationState.Closed)
                                            {
                                                clientISIS.Close();
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        try
                        {
                            pr = new NewBISSvcRef.ProcessResult();
                            U_APP_ISIS_DOCUMENT[] uAppISISDocuments;
                            app_ids = new long[] { _appId };
                            pr = client.GetU_APP_ISIS_DOCUMENT(out uAppISISDocuments, null, app_ids);
                            if (uAppISISDocuments != null)
                            {
                                foreach (U_APP_ISIS_DOCUMENT uapp in uAppISISDocuments)
                                {

                                    #region "ค้นหาเอกสารเพิ่มเติม" 

                                    if (uapp.CONTENT_ID != null)
                                    {
                                        using (AppTransportServiceContractClient clientISIS = new AppTransportServiceContractClient())
                                        {
                                            try
                                            {
                                                byte[] imageByte = clientISIS.GetMoreContentByID(uapp.CONTENT_ID);
                                                if (imageByte != null)
                                                {
                                                    MemoryStream ms = new MemoryStream(imageByte);
                                                    myImage = Image.FromStream(ms);
                                                    ms.Close();
                                                    ms.Dispose();
                                                    CustomClass.DOC_ISISSHOW docisis = new CustomClass.DOC_ISISSHOW();
                                                    docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
                                                    docisis.IDDOC = uapp.CONTENT_ID;
                                                    docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(uapp.RECEIVE_ISIS_DT.Value, "dd/MM/yyyy hh:mi:ss", "BU");
                                                    docIsisColls.Add(docisis);

                                                    using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\WINDOWS\Temp\" + uapp.CONTENT_ID + ".tif", FileMode.Create)))
                                                    {
                                                        binWriter.Write(imageByte);
                                                    }
                                                }
                                                imageByte = null;
                                            }
                                            catch (Exception ex)
                                            {
                                                throw ex;
                                            }
                                            finally
                                            {
                                                if (clientISIS.State != System.ServiceModel.CommunicationState.Closed)
                                                {
                                                    clientISIS.Close();
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (client.State != System.ServiceModel.CommunicationState.Closed)
                            {
                                client.Close();
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }
            }



            //var repo = new NewBisRepository();
            //var appInfo = repo.GetU_APPLICATION_ID(_appId);
            //if (appInfo == null)
            //{
            //    throw new Exception("ไม่พบข้อมูลใบคำขอ");
            //}
            //var imgeDoc = repo.GetApplicationDocument(_appId, appInfo.APP_NO, appInfo.CHANNEL_TYPE, "000000");
            //if (imgeDoc != null && imgeDoc.ImageApp != null)
            //{
            //    var tempImageApp = imgeDoc.ImageApp;
            //    if (tempImageApp != null && tempImageApp.DataFiles != null && tempImageApp.DataFiles.Any())
            //    {
            //        foreach (var item in tempImageApp.DataFiles)
            //        {

            //            MemoryStream ms = new MemoryStream(item.FileByte);
            //            myImage = Image.FromStream(ms);
            //            ms.Close();
            //            ms.Dispose();
            //            var docisis = new CustomClass.DOC_ISISSHOW();
            //            docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
            //            docisis.IDDOC = item.RefID + _appId;
            //            docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(tempImageApp.DocumentDate.Value, "dd/MM/yyyy hh:mm:ss", "BU");
            //            docIsisColls.Add(docisis);

            //            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\WINDOWS\Temp\" + docisis.IDDOC + ".tif", FileMode.Create)))
            //            {
            //                binWriter.Write(item.FileByte);
            //            }
            //        }
            //    }

            //    var tempImageAppAditonal = imgeDoc.ImageAppAditonal;
            //    if (tempImageAppAditonal != null && tempImageAppAditonal.Any())
            //    {
            //        foreach (var item in tempImageAppAditonal)
            //        {
            //            var docisis = new CustomClass.DOC_ISISSHOW();
            //            docisis.RECEIVE_ISIS_DT = Utility.dateTimeToString(item.DocumentDate.Value, "dd/MM/yyyy hh:mm:ss", "BU");
            //            if (item.DataFile != null && item.DataFile.FileByte != null && item.DataFile.FileByte.Any())
            //            {
            //                MemoryStream ms = new MemoryStream(item.DataFile.FileByte);
            //                myImage = Image.FromStream(ms);
            //                ms.Close();
            //                ms.Dispose();
            //                docisis.PAGES = myImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page).ToString();
            //                docisis.IDDOC = item.DataFile.RefID + _appId;
            //                using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:\WINDOWS\Temp\" + docisis.IDDOC + ".tif", FileMode.Create)))
            //                {
            //                    binWriter.Write(item.DataFile.FileByte);
            //                }
            //            }
            //            docIsisColls.Add(docisis);
            //        }
            //    }
            //}

            #region "Set dtgDocument"

            dtgDocument.DataSource = null;
            dtgDocument.ColumnHeadersVisible = false;
            dtgDocument.ColumnHeadersVisible = true;
            dtgDocument.AutoGenerateColumns = false;
            dtgDocument.RowHeadersVisible = false;
            dtgDocument.DataSource = docIsisColls;
            int i = 0;
            foreach (DataGridViewRow dr in dtgDocument.Rows)
            {
                i = i + 1;
                if (dr.DataBoundItem is CustomClass.DOC_ISISSHOW)
                {
                    CustomClass.DOC_ISISSHOW obj = (CustomClass.DOC_ISISSHOW)dr.DataBoundItem;
                    dr.Cells["clmNo"].Value = i;
                    dr.Cells["clmPages"].Value = obj.PAGES;
                    dr.Cells["clmISISDT"].Value = obj.RECEIVE_ISIS_DT;
                    dr.Cells["clmId"].Value = obj.IDDOC;
                }
            }
            #endregion


        }

        private string ConvertPDFToTiff(string fileName, byte[] inputMS)
        {
            var listImageBase64String = new List<byte[]>();
            var mem = new MemoryStream(inputMS);
            var document = PdfiumViewer.PdfDocument.Load(mem);
            try
            {
                var pageCount = document.PageCount;
                for (int pageNumber = 0; pageNumber < pageCount; pageNumber += 1)
                {
                    if (pageNumber == 100)
                    {
                        break;
                    }
                    try
                    {
                        var image = document.Render(pageNumber, 150, 150, PdfiumViewer.PdfRenderFlags.CorrectFromDpi);
                        using (var stream = new MemoryStream())
                        {
                            try
                            {
                                image.Save(stream, ImageFormat.Jpeg);
                                listImageBase64String.Add(stream.ToArray());
                                return ConvertJpegToTiff(fileName, listImageBase64String);
                            }
                            finally
                            {
                                stream.Close();
                                stream.Dispose();
                                image.Dispose();
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        var image = document.Render(pageNumber, 300, 300, true);
                        using (var stream = new MemoryStream())
                        {
                            try
                            {
                                image.Save(stream, ImageFormat.Jpeg);
                                listImageBase64String.Add(stream.ToArray());
                                return ConvertJpegToTiff(fileName, listImageBase64String);
                            }
                            finally
                            {
                                stream.Close();
                                stream.Dispose();
                                image.Dispose();
                            }

                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw new Exception("Can't Convert PDFToImageByte :" + e.Message);
            }
            finally
            {
                document.Dispose();
                mem.Close();
                mem.Dispose();
            }
        }

        public string ConvertJpegToTiff(string fileName, List<byte[]> fileBytes)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            ImageCodecInfo tiffCodecInfo = ImageCodecInfo.GetImageEncoders()
                .First(ie => ie.MimeType == "image/tiff");

            byte[] test = null;
            string tiffPaths = @"C:\WINDOWS\Temp\" + fileName + ".tif"; ;

            Image tiffImg = null;
            try
            {
                for (int i = 0; i < fileBytes.Count; i++)
                {
                    if (i == 0)
                    {
                        // Initialize the first frame of multipage tiff. 
                        // tiffImg = Image.Fr(test)  // Image.FromFile(fileNames[i]);

                        using (var ms = new MemoryStream(fileBytes[i]))
                        {
                            tiffImg = Image.FromStream(ms);
                        }

                        encoderParams.Param[0] = new EncoderParameter(
                            Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                        tiffImg.Save(tiffPaths, tiffCodecInfo, encoderParams);
                    }
                    else
                    {
                        // Add additional frames. 
                        encoderParams.Param[0] = new EncoderParameter(
                            Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                        using (var ms = new MemoryStream(fileBytes[i]))
                        {
                            var temp = Image.FromStream(ms);
                            tiffImg.SaveAdd(temp, encoderParams);
                        }
                    }

                    if (i == fileBytes.Count - 1)
                    {
                        // When it is the last frame, flush the resources and closing. 
                        encoderParams.Param[0] = new EncoderParameter(
                            Encoder.SaveFlag, (long)EncoderValue.Flush);
                        tiffImg.SaveAdd(encoderParams);
                    }
                }
            }
            finally
            {
                if (tiffImg != null)
                {
                    tiffImg.Dispose();
                    tiffImg = null;
                }
            }

            //else
            //{
            //    tiffPaths = new string[fileNames.Length];

            //    for (int i = 0; i < fileNames.Length; i++)
            //    {
            //        tiffPaths[i] = String.Format("{0}\\{1}.tif",
            //            Path.GetDirectoryName(fileNames[i]),
            //            Path.GetFileNameWithoutExtension(fileNames[i]));

            //        // Save as individual tiff files. 
            //        using (Image tiffImg = Image.FromFile(fileNames[i]))
            //        {
            //            tiffImg.Save(tiffPaths[i], ImageFormat.Tiff);
            //        }
            //    }
            //}

            return tiffPaths;
        }

        private void ViewDocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (CustomClass.DOC_ISISSHOW docIsis in docIsisColls)
            {
                TryToDelete(@"C:\WINDOWS\Temp\" + docIsis.IDDOC + ".tif");
            }
            for (int i = 0; i < _pageCount; i = i + 1)
            {
                try
                {
                    oPanel[i].Dispose();
                    oLabel[i].Dispose();
                    oPictureBox[i].Image.Dispose();
                    oPictureBox[i].Dispose();
                }
                catch
                {
                    break;
                }
            }
            this.Dispose();
            this.Close();
        }

        private bool TryToDelete(string f)
        {
            try
            {
                File.Delete(f);
                return true;
            }
            catch (IOException)
            {
                return false;
            }

        }

        private void dtgDocument_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex > -1) && (e.ColumnIndex == dtgDocument.Rows[e.RowIndex].Cells["clmBtnShow"].ColumnIndex) && (dtgDocument.Rows[e.RowIndex].IsNewRow == false))
                {
                    if (dtgDocument.Rows[e.RowIndex].Cells["clmId"].Value != null)
                    {
                        for (int i = 0; i < _pageCount; i = i + 1)
                        {
                            try
                            {
                                oPanel[i].Dispose();
                                oLabel[i].Dispose();
                                oPictureBox[i].Image.Dispose();
                                oPictureBox[i].Dispose();
                                arrimg[i].Dispose();
                            }
                            catch
                            {
                                break;
                            }
                        }

                        //ค้นหารูปภาพ 
                        fileNameTifSelected = @"C:\WINDOWS\Temp\" + dtgDocument.Rows[e.RowIndex].Cells["clmId"].Value + ".tif";
                        byte[] imgData = System.IO.File.ReadAllBytes(@"C:\WINDOWS\Temp\" + dtgDocument.Rows[e.RowIndex].Cells["clmId"].Value + ".tif");
                        MemoryStream ms2 = new MemoryStream(imgData);
                        myImagesave = Image.FromStream(ms2);
                        glbScrollBarValue = 0;//หน้าที่กำลังแสดงผลของ hscollbar
                        glbMaximumPicture = 0;//จำนวนที่แสดงทั้งหมดต่อ 1 ครั้ง
                        glbstartPicture = 0;//รูปเริ่มต้นของหน้านั้นใน array
                        hScrollBar1.Value = 0;

                        ArryIndex = 0;//รูปภาพที่กำลังเลือก picture selected ตำแหน่งที่กำลังเลือกรูปแสดง
                        picLoad(myImagesave);
                        //ms2.Close();
                        //ms2.Dispose(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ViewDocumentForm_KeyDown(object sender, KeyEventArgs e)
        {
            totalKeyDown(sender, e);
        }

        private void menuStrip1_KeyDown(object sender, KeyEventArgs e)
        {
            totalKeyDown(sender, e);
        }

        private void dtgDocument_KeyDown(object sender, KeyEventArgs e)
        {
            totalKeyDown(sender, e);
        }

        private void totalKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F12)
            //{
            //    if (PageIndex < oPictureBox.Length - 1)
            //    {
            //        AddForm_MouseClick(oPictureBox[PageIndex + 1], null); 
            //    }
            //}
            //else if (e.KeyCode == Keys.F11)
            //{
            //    if (PageIndex != 0)
            //    {
            //        AddForm_MouseClick(oPictureBox[PageIndex - 1], null); 
            //    }
            //}

            if (e.KeyCode == Keys.F12)
            {
                if ((numberPicbox + 1) < _pageCount)
                {
                    if (ArryIndex == (glbMaximumPicture - 1))
                    {
                        hScrollBar1.Value = hScrollBar1.Value + 1;
                        ArryIndex = 0;
                        ShowPicture();

                    }
                    else
                    {
                        ArryIndex = ArryIndex + 1;
                        ShowPicture();
                    }
                }
            }
            else if (e.KeyCode == Keys.F11)
            {
                if ((numberPicbox - 1) >= 0)
                {
                    if (ArryIndex == 0)
                    {
                        hScrollBar1.Value = hScrollBar1.Value - 1;
                        ArryIndex = glbMaximumPicture - 1;

                        ShowPicture();
                    }
                    else
                    {

                        ArryIndex = ArryIndex - 1;
                        ShowPicture();
                    }
                }
            }
        }

        private void ViewDocumentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < _pageCount; i = i + 1)
            {
                try
                {
                    oPanel[i].Dispose();
                    oLabel[i].Dispose();
                    oPictureBox[i].Dispose();
                }
                catch
                {
                    break;
                }

            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {

            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (Math.Abs(hScrollBar1.Value - glbScrollBarValue) >= 1)
            {
                glbstartPicture = (hScrollBar1.Value * glbMaximumPicture);
                glbScrollBarValue = hScrollBar1.Value;
                picLoadFromPosition(myImagesave);
                ShowPicture();
            }
        }

    }
}

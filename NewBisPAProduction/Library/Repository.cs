using NewBisPA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace NewBisPA.Library
{
    public class Repository
    {
        public bool IsRiskOfOccupation( string ocpType, string ocpClass, string plblock, string pl_code, string pl_code2)
        {

            var client = new NewBisPASvcRef.NewBisPASvcClient();
            bool isRiskOfOccupation;
            var pr = client.IsRiskOfOccupation(out isRiskOfOccupation, ocpType, ocpClass, plblock, pl_code, pl_code2);
            if (pr.Successed)
            {

                return isRiskOfOccupation;

            }
            throw new Exception(pr.ErrorMessage);

        }

        public ImageApplicationDocument GetApplicationDocument(long appId, string appNo, string channelType, string userId)
        {

            ImageApplicationDocument imgeDoc = null;
            var client = new NewBisPASvcRef.NewBisPASvcClient();
            NewBisPASvcRef.U_NEWBIS_SCAN[] dataObjects;
            var pr = client.GetImageIdsOfApplicationAllDocument(out dataObjects, appNo, channelType);
            if (pr.Successed)
            {
                if (dataObjects != null && dataObjects.Any())
                {
                    imgeDoc = new ImageApplicationDocument();
                    var imageClient = new ImageManagementSvcRef.ImagemanagementSvcClient();
                    foreach (var item in dataObjects)
                    {
                        ITUtility.DataFile returnFiles;
                        var applicationId = ITUtility.Utility.AppSettings("ApplicationId");
                        var pr2 = imageClient.DOWNLOAD_FILE_BY_TAG(out returnFiles, "003062", item.IMAGE_ID?.ToString(), applicationId, ImageManagementSvcRef.EnumContractDownloadType.forPrint);
                        if (pr2.Successed)
                        {

                            if (returnFiles != null)
                            {
                                if (item.STATUS == "SC" && item.SUBSTATUS == "SC" && item.DOC_TYPE == "A") // "A"  = main docuemnt of aaplication
                                {

                                    if (imgeDoc.ImageApp == null)
                                    {
                                        imgeDoc.ImageApp = new ImageApplication()
                                        {
                                            DocumentDate = item.SCAN_DT,
                                            GuIdKey = "",
                                            ImageId = item.IMAGE_ID
                                            // ImageByte = returnFiles.FileData,
                                            // ContentType = returnFiles.ContentType
                                        };
                                    }
                                    if (imgeDoc.ImageApp.DataFiles == null)
                                    {
                                        imgeDoc.ImageApp.DataFiles = new List<DataFileInfo>();
                                    }
                                    imgeDoc.ImageApp.DataFiles.Add(new DataFileInfo()
                                    {
                                        ContentType = returnFiles.ContentType,
                                        FileByte = returnFiles.FileData
                                    });


                                }
                                else
                                {

                                    imgeDoc.ImageAppAditonal = imgeDoc.ImageAppAditonal ?? new List<ImageAddtionalApplication>();
                                    imgeDoc.ImageAppAditonal.Add(new ImageAddtionalApplication()
                                    {
                                        ConntentIdKey = "",
                                        DocumentDate = item.SCAN_DT,
                                        StatusType = item.STATUS,
                                        SubStatusType = item.SUBSTATUS,
                                        ImageId = item.IMAGE_ID,
                                        DataFile = new DataFileInfo()
                                        {
                                            ContentType = returnFiles.ContentType,
                                            FileByte = returnFiles.FileData
                                        }
                                    });

                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Can't  DOWNLOAD_FILE_BY_IMAGE_ID : " + pr.ErrorMessage);

                        }
                    }
                }
                else
                {
                    NewBisPASvcRef.U_APP_ISIS uappIsis = null;
                    pr  = client.GetU_APP_ISIS(out uappIsis, appId);
                    if (!pr.Successed)
                    {
                        throw new Exception(pr.ErrorMessage);
                    }
                    if (uappIsis != null)
                    {
                        imgeDoc = new ImageApplicationDocument();


                        #region "ค้นหาใบคำขอชุดแรก"
                        using (var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient())
                        {
                            try
                            {
                                var byteData = clientISIS.GetAppFormContentByID(uappIsis.GUID);
                                imgeDoc.ImageApp = new ImageApplication()
                                {
                                    DocumentDate = uappIsis.IMPORT_DT,
                                    GuIdKey = uappIsis.GUID,
                                    DataFiles = byteData != null && byteData.Any() ? new List<DataFileInfo>() { new DataFileInfo() { ContentType = MediaTypeNames.Image.Tiff, FileByte = byteData } } : null
                                };
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

                        NewBisPASvcRef.U_APP_ISIS_DOCUMENT[] uappIsisDoc;
                        pr = client.GetU_APP_ISIS_DOCUMENT(out uappIsisDoc , appId);
                        if (!pr.Successed)
                        {
                            throw new Exception(pr.ErrorMessage);
                        }

                        if (uappIsisDoc != null && uappIsisDoc.Any())
                        {
                            imgeDoc.ImageAppAditonal = new List<ImageAddtionalApplication>();

                            #region "ค้นหาเอกสารเพิ่มเติม"
                            var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient();
                            foreach (var item in uappIsisDoc)
                            {
                                try
                                {
                                    var byteData = clientISIS.GetMoreContentByID(item.CONTENT_ID);
                                    var addtional = new ImageAddtionalApplication() { ConntentIdKey = item.CONTENT_ID, DocumentDate = uappIsis.IMPORT_DT, };
                                    addtional.StatusType = item.STATUS;
                                    addtional.SubStatusType = item.SUBSTATUS;
                                    addtional.DataFile = byteData != null && byteData.Any() ? new DataFileInfo()
                                    {
                                        ContentType = MediaTypeNames.Image.Tiff,
                                        FileByte = byteData
                                    } : null;
                                    imgeDoc.ImageAppAditonal.Add(addtional);

                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }

                            }
                            clientISIS.Close();
                            #endregion

                        }

                    }
                }
            }
            else
            {
                throw new Exception("Can't  GetImageIdsOfApplicationAllDocument : " + pr.ErrorMessage);
            }

            return imgeDoc;
        }


        public long? GetAUTB_APPNAME_BY_PLAN(string plBlock, string plCode, string plCode2, bool isAdult, bool hasBuyRider)
        {

            NewBisPA.NewBisPASvcRef.AUTB_APPNAME_BY_PLAN dataObject;
            var client = new NewBisPASvcRef.NewBisPASvcClient();
            bool isRiskOfOccupation;
            var pr = client.GetAUTB_APPNAME_BY_PLAN(out dataObject, plBlock, plCode, plCode2, isAdult, hasBuyRider);
            if (pr.Successed)
            {
                if (dataObject == null)
                {
                    return null;
                }
                return dataObject.APPNAME_ID.Value;
            }
            throw new Exception(pr.ErrorMessage);
        }


    }
}


using NewBisPA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace NewBisPA.Library
{
    public class NewBisRepository
    {
        public NewBisOnlineWcfRef.U_APP_ISIS GetU_APP_ISIS(long appId)
        {
            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            NewBisOnlineWcfRef.U_APP_ISIS dataObjects;
            var pr = client.GetU_APP_ISIS(out dataObjects, appId);
            if (pr.Successed)
            {
                return dataObjects;
            }
            throw new Exception("Can't  GetU_APP_ISIS : " + pr.ErrorMessage);
        }

        public NewBisOnlineWcfRef.U_APPLICATION_ID GetU_APPLICATION_ID(long appId)
        {
            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            NewBisOnlineWcfRef.U_APPLICATION_ID dataObject;
            var pr = client.GetU_APPLICATION_ID(out dataObject, appId);
            if (pr.Successed)
            {
                return dataObject;
            }
            throw new Exception("Can't  GetU_APP_ISIS : " + pr.ErrorMessage);
        }



        public NewBisOnlineWcfRef.U_APP_ISIS_DOCUMENT[] GetGetU_APP_ISIS_DOCUMENT(long appId)
        {

            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            NewBisOnlineWcfRef.U_APP_ISIS_DOCUMENT[] dataObjects;
            var pr = client.GetU_APP_ISIS_DOCUMENT(out dataObjects, appId);
            if (pr.Successed)
            {
                return dataObjects;
            }
            throw new Exception("Can't  GetU_APP_ISIS_DOCUMENT : " + pr.ErrorMessage);
        }

        public bool HasApplicationDocument(string appNo, string channelType, long? appId)
        {

            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            bool hasDocument;
            var pr = client.HaveApplicationMainDocument(out hasDocument, appNo, channelType);
            try
            {
                if (pr.Successed)
                {
                    if (hasDocument)
                    {
                        return true;
                    }

                    if (appId != null)
                    {
                        var uappIsis = GetU_APP_ISIS(appId.Value);
                        if (uappIsis != null)
                        {
                            var imgeDoc = new ImageApplicationDocument();

                            #region "ค้นหาใบคำขอชุดแรก"
                            using (var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient())
                            {
                                try
                                {
                                    var imageByte = clientISIS.GetAppFormContentByID(uappIsis.GUID);
                                    if (imageByte != null && imageByte.Any())
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error Function : ตรวจสอบข้อมูลสแกนใบคำขอ");
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
                    return false;
                }
                else
                {
                    throw new Exception("Can't Check HaveApplicationMainDocument : " + pr.ErrorMessage);
                }
            }
            finally
            {
                client.Close();
            }

        }


        public void WriteErrorLogToDMSNB(NewBisOnlineWcfRef.U_DMSNB_LOG obj)
        {
            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            var pr = client.AddU_DMSNB_LOG(obj);
            if (!pr.Successed)
            {

                throw new Exception("Can't  WriteErrorLogToDMSNB : " + pr.ErrorMessage);
            }
        }
        public ImageApplicationDocument GetApplicationDocument(long appId, string appNo, string channelType, string userId)
        {

            ImageApplicationDocument imgeDoc = null;
            var client = new NewBisOnlineWcfRef.NewBisSvcClient();
            NewBisOnlineWcfRef.U_NEWBIS_SCAN[] dataObjects;
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
                        var applicationId = "1";//ITUtility.Utility.AppSettings(SystemConstant.ApplicationIdOfAppSetting);
                        var imageId = item.IMAGE_ID == null ? "" : item.IMAGE_ID.ToString();

                        try
                        {
                            var pr2 = imageClient.DOWNLOAD_FILE_BY_TAG(out returnFiles, userId, imageId, applicationId, ImageManagementSvcRef.EnumContractDownloadType.forPrint);
                            if (pr2.Successed)
                            {

                                if (returnFiles != null)
                                {
                                    var mainApplicationDocument = "A";
                                    //item.STATUS == ApplicationStatusConst.WaitScan && item.SUBSTATUS == ApplicationSubStatusConst.WaitScan && 
                                    if (item.DOC_TYPE == mainApplicationDocument) // main document
                                    {

                                        if (imgeDoc.ImageApp == null)
                                        {
                                            imgeDoc.ImageApp = new ImageApplication()
                                            {
                                                DocumentDate = item.SCAN_DT,
                                                GuIdKey = "",
                                                ImageId = item.IMAGE_ID,
                                                Pages = item.PAGE ?? 0
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
                                            FileByte = returnFiles.FileData,
                                            RefID = item.IMAGE_ID == null ? "" : item.IMAGE_ID.ToString(),
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
                                            Pages = item.PAGE ?? 0,
                                            DataFile = new DataFileInfo()
                                            {
                                                ContentType = returnFiles.ContentType,
                                                FileByte = returnFiles.FileData,
                                                RefID = item.IMAGE_ID == null ? "" : item.IMAGE_ID.ToString(),
                                            }
                                        });

                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Can't  DOWNLOAD_FILE_BY_IMAGE_ID : " + pr2.ErrorMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Can't  DOWNLOAD_FILE_BY_IMAGE_ID : " + ex.Message);

                        }
                    }
                }
                else
                {
                    var uappIsis = GetU_APP_ISIS(appId);
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
                                    DataFiles = byteData != null && byteData.Any() ? new List<DataFileInfo>() { new DataFileInfo() { ContentType = MediaTypeNames.Image.Tiff, FileByte = byteData, RefID = uappIsis.GUID } } : null
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

                        var uappIsisDoc = GetGetU_APP_ISIS_DOCUMENT(appId);
                        if (uappIsisDoc != null && uappIsisDoc.Any())
                        {
                            imgeDoc.ImageAppAditonal = new List<ImageAddtionalApplication>();

                            #region "ค้นหาเอกสารเพิ่มเติม"
                            var clientISIS = new IsisAppTransportService.AppTransportServiceContractClient();
                            foreach (var item in uappIsisDoc)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(item.CONTENT_ID))
                                    {
                                        var byteData = clientISIS.GetMoreContentByID(item.CONTENT_ID);
                                        var addtional = new ImageAddtionalApplication() { ConntentIdKey = item.CONTENT_ID, DocumentDate = item.RECEIVE_ISIS_DT, };
                                        addtional.StatusType = item.STATUS;
                                        addtional.SubStatusType = item.SUBSTATUS;
                                        addtional.DataFile = byteData != null && byteData.Any() ? new DataFileInfo()
                                        {
                                            ContentType = MediaTypeNames.Image.Tiff,
                                            FileByte = byteData,
                                            RefID = item.CONTENT_ID
                                        } : null;
                                        imgeDoc.ImageAppAditonal.Add(addtional);
                                    }

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

        public void UploadFlagEDM(long imageId)
        {


            var client = new ImageManagementSvcRef.ImagemanagementSvcClient();
            var pr = client.UPDATE_FLAG_EDM(imageId);
            if (!pr.Successed)
            {
                throw new Exception("UPDATE_FLAG_EDM Error : " + pr.ErrorMessage);
            }

        }

        public long UploadImageToDMSNB(out long? imageEasy, byte[] imageApp, string contentType, string appNo, string channelType, string policyId, string policy, string policyHolding, long uappId, long appId, long seqNo, string documentStatus, bool isUploadToEDM, string userId)
        {
            var client = new ImageManagementSvcRef.ImagemanagementSvcClient();


            var fileName = "";

            if (contentType == MediaTypeNames.Application.Pdf)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.PDF";
            }
            else if (contentType == MediaTypeNames.Image.Tiff)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.TIFF";
            }

            var dataFile = new ITUtility.DataFile()
            {
                ContentType = contentType,
                FileData = imageApp,
                FileName = fileName
            };
            var tag = new List<ImageManagementSvcRef.TAG_INFO>();
            var infoCollection = new ImageManagementSvcRef.TAG_INFO_Collection();

            ImageManagementSvcRef.TAG_DETAIL tagInfo;
            var pr = client.GET_TAG_DETAIL_WITH_APP_NEWBIS(out tagInfo, channelType, documentStatus);
            if (!pr.Successed)
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS Error :" + pr.ErrorMessage);
            }

            if (tagInfo == null)
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS tagInfo is null :");

            }
            //     var tagTypeId = GetTagTypeId(channelType, documentStatus);
            pr = client.GET_TAG(out infoCollection, tagInfo.APP_IMG_TYPE_ID.Value);
            if (!pr.Successed)
            {
                throw new Exception("GET_TAG error :" + pr.ErrorMessage);

            }
            if (infoCollection != null && infoCollection.Any())
            {
                foreach (var item in infoCollection)
                {
                    if (item.TagName.Equals("POLICY_ID"))
                    {
                        item.TagData = policyId;
                    }
                    if (item.TagName.Equals("APP_ID"))
                    {
                        item.TagData = appId.ToString();
                    }
                    if (item.TagName.Equals("SEQ_NO"))
                    {
                        item.TagData = seqNo.ToString();
                    }
                    if (item.TagName.Equals("PARENT_TYPE"))
                    {
                        item.TagData = "APP";
                    }
                }

                long? imageIms = 0;
                imageEasy = 0;
                var applicationId = "180";///ITUtility.Utility.AppSettings(SystemConstant.ApplicationIdOfAppSetting);
                var pathId = "1";//; ITUtility.Utility.AppSettings(SystemConstant.ImsPathIdOfFromAppSetting);
                //  var pr = client.UPLOAD_FILE_BY_TAG(out image, dataFile, userId, null, infoCollection.ToArray(), pathId, applicationId);
                string certNo = "", policyNo = "";
                if (!string.IsNullOrEmpty(policyHolding))
                {
                    certNo = policy;
                    policyNo = policyHolding;
                }
                else
                {
                    policyNo = policy;
                }

                ImageManagementSvcRef.UploadeFiles objectEDM = new ImageManagementSvcRef.UploadeFiles
                {
                    APP_NO = appNo,
                    ChannelType = channelType,
                    policy_id = policyId,
                    UserID = "000000",// userId,
                    cert_no = certNo,
                    policy_no = policyNo,
                    UAPP_ID = uappId
                };
                var uploadTo = isUploadToEDM ? ImageManagementSvcRef.EnumContractTo.EASY : ImageManagementSvcRef.EnumContractTo.IMS;
                pr = client.UPLOAD_FILE_BY_TAG__NEWBIS(out imageIms, out imageEasy, dataFile, infoCollection.ToArray(), objectEDM, pathId, applicationId, uploadTo);
                if (pr.Successed)
                {
                    imageIms = imageIms ?? 0;
                    return imageIms.Value;
                }
                throw new Exception("ImageSystem ->" + pr.ErrorMessage);
            }
            throw new Exception("ImageSystem -> Tag-Info not found from channeltype  " + channelType);
        }


        public long UploadImageToIMS_EDM(out long? imageEasy, byte[] imageApp, string contentType, string appNo, string channelType, string policyId, string policy, string policyHolding, long uappId, long appId, bool isUploadToEDM, string userId)
        {
            var client = new ImageManagementSvcRef.ImagemanagementSvcClient();


            var fileName = "";

            if (contentType == MediaTypeNames.Application.Pdf)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.PDF";
            }
            else if (contentType == MediaTypeNames.Image.Tiff)
            {
                fileName = (!string.IsNullOrEmpty(policyId) ? "P" + policyId : "A" + appId.ToString()) + "-NEWBIS.TIFF";
            }

            var dataFile = new ITUtility.DataFile()
            {
                ContentType = contentType,
                FileData = imageApp,
                FileName = fileName
            };
            var tag = new List<ImageManagementSvcRef.TAG_INFO>();
            var infoCollection = new ImageManagementSvcRef.TAG_INFO_Collection();

            ImageManagementSvcRef.TAG_DETAIL_Collection tagInfo;
            var pr = client.GET_TAG_DETAIL_WITH_APP(out tagInfo, "NewBisOnline", "APPLICATION");
            if (!pr.Successed)
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS Error :" + pr.ErrorMessage);
            }

            if (tagInfo == null || !tagInfo.Any())
            {
                throw new Exception("GET_TAG_DETAIL_WITH_APP_NEWBIS tagInfo is null :");

            }

            if (isUploadToEDM)
            {
                var info = tagInfo.Where(item => item.DESCRIPTION.Trim() == "เอกสารใบคำขอที่ส่งไปEasy").FirstOrDefault();
                if (info == null)
                {
                    throw new Exception("tagInfo Description  of edm is empty.");
                }
                pr = client.GET_TAG(out infoCollection, info.APP_IMG_TYPE_ID);
            }
            else
            {
                var info = tagInfo.Where(item => item.DESCRIPTION.Trim() == "ใบคำขอ").FirstOrDefault();
                if (info == null)
                {
                    throw new Exception("tagInfo Description of app is empty.");
                }
                pr = client.GET_TAG(out infoCollection, info.APP_IMG_TYPE_ID);
            }


            if (!pr.Successed)
            {
                throw new Exception("GET_TAG error :" + pr.ErrorMessage);

            }
            if (infoCollection != null && infoCollection.Any())
            {
                foreach (var item in infoCollection)
                {
                    if (item.TagName.Equals("APP_NO"))
                    {
                        item.TagData = appNo;
                    }
                }

                long? imageIms = 0;
                imageEasy = 0;
                var applicationId = "180"; //ITUtility.Utility.AppSettings(SystemConstant.ApplicationIdOfAppSetting);
                var pathId = "1";// ITUtility.Utility.AppSettings(SystemConstant.ImsPathIdOfFromAppSetting);
                //  var pr = client.UPLOAD_FILE_BY_TAG(out image, dataFile, userId, null, infoCollection.ToArray(), pathId, applicationId);
                string certNo = "", policyNo = "";
                if (!string.IsNullOrEmpty(policyHolding))
                {
                    certNo = policy;
                    policyNo = policyHolding;
                }
                else
                {
                    policyNo = policy;
                }
                var uploadTo = isUploadToEDM ? ImageManagementSvcRef.EnumContractTo.EASY : ImageManagementSvcRef.EnumContractTo.IMS;
                ImageManagementSvcRef.UploadeFiles objectEDM = new ImageManagementSvcRef.UploadeFiles
                {
                    APP_NO = appNo,
                    ChannelType = channelType,
                    policy_id = policyId,
                    UserID = "000000",// userId,
                    cert_no = certNo,
                    policy_no = policyNo,
                    UAPP_ID = uappId
                };
                var tagInfoItem = tagInfo.First();
                var tempPathId = tagInfoItem.PATH_ID == null ? pathId : tagInfoItem.PATH_ID.ToString();
                var tempApplicationId = tagInfoItem.APPLICATION_ID == null ? applicationId : tagInfoItem.APPLICATION_ID.ToString();
                pr = client.UPLOAD_FILE_BY_TAG__NEWBIS(out imageIms, out imageEasy, dataFile, infoCollection.ToArray(), objectEDM, tempPathId, tempApplicationId, uploadTo);
                if (pr.Successed)
                {
                    imageIms = imageIms ?? 0;
                    return imageIms.Value;
                }
                throw new Exception("ImageSystem ->" + pr.ErrorMessage);
            }
            throw new Exception("ImageSystem -> Tag-Info not found from channeltype  " + channelType);
        }


    }
}

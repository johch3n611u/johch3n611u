using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ECAPI.Models;
using ECAPI.Models.Amazon;
using Microsoft.AspNetCore.Mvc;
using static EC.ENUMS;

namespace ECAPI.Controllers.Amazon
{
    /// <summary>
    /// Bucket相關API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Bucket")]
    public class BucketController : Controller
    {
        private readonly Dictionary<RegionEndpointsList, RegionEndpoint> dicRegionEndpoint = new Dictionary<RegionEndpointsList, RegionEndpoint>
        {
            {RegionEndpointsList.USEast1, RegionEndpoint.USEast1 },
            {RegionEndpointsList.MESouth1, RegionEndpoint.MESouth1 },
            {RegionEndpointsList.CACentral1, RegionEndpoint.CACentral1 },
            {RegionEndpointsList.CNNorthWest1, RegionEndpoint.CNNorthWest1 },
            {RegionEndpointsList.CNNorth1, RegionEndpoint.CNNorth1 },
            {RegionEndpointsList.USGovCloudWest1, RegionEndpoint.USGovCloudWest1 },
            {RegionEndpointsList.USGovCloudEast1, RegionEndpoint.USGovCloudEast1 },
            {RegionEndpointsList.SAEast1, RegionEndpoint.SAEast1 },
            {RegionEndpointsList.APSoutheast1, RegionEndpoint.APSoutheast1 },
            {RegionEndpointsList.APSouth1, RegionEndpoint.APSouth1 },
            {RegionEndpointsList.APNortheast3, RegionEndpoint.APNortheast3 },
            {RegionEndpointsList.APSoutheast2, RegionEndpoint.APSoutheast2 },
            {RegionEndpointsList.APNortheast1, RegionEndpoint.APNortheast1 },
            {RegionEndpointsList.USEast2, RegionEndpoint.USEast2 },
            {RegionEndpointsList.APNortheast2, RegionEndpoint.APNortheast2 },
            {RegionEndpointsList.USWest2, RegionEndpoint.USWest2 },
            {RegionEndpointsList.EUNorth1, RegionEndpoint.EUNorth1 },
            {RegionEndpointsList.EUWest1, RegionEndpoint.EUWest1 },
            {RegionEndpointsList.USWest1, RegionEndpoint.USWest1 },
            {RegionEndpointsList.EUWest3, RegionEndpoint.EUWest3 },
            {RegionEndpointsList.EUCentral1, RegionEndpoint.EUCentral1 },
            {RegionEndpointsList.APEast1, RegionEndpoint.APEast1 },
            {RegionEndpointsList.EUWest2, RegionEndpoint.EUWest2 },
        };

        private readonly Dictionary<ServerSideEncryptionList, ServerSideEncryptionMethod> dicSideEncryption = new Dictionary<ServerSideEncryptionList, ServerSideEncryptionMethod>
        {
            {ServerSideEncryptionList.None, ServerSideEncryptionMethod.None },
            {ServerSideEncryptionList.AES256, ServerSideEncryptionMethod.AES256 },
            {ServerSideEncryptionList.AWSKMS, ServerSideEncryptionMethod.AWSKMS },
        };

        /// <summary>
        /// 上傳檔案
        /// </summary>
        [HttpPost("PutObject")]
        public async Task<JsonResult> PutObjectAsync([FromBody] PutObjectModel putObject)
        {
            try
            {
                using (IAmazonS3 s3Client = GetAmazonS3Client(putObject))
                {
                    using (WebClient wc = new WebClient())
                    {
                        //用網路連結取得檔案
                        Stream fileStream = wc.OpenRead(putObject.FileUrl);
                        byte[] fileBytes = StreamToArrayBytes(fileStream);
                        //設定上傳加密格式
                        dicSideEncryption.TryGetValue(putObject.SideEncryption, out ServerSideEncryptionMethod serverSideEnc);
                        var putRequest = new PutObjectRequest
                        {
                            BucketName = putObject.BucketName,
                            Key = GetUploadPathExtension(putObject),
                            InputStream = new MemoryStream(fileBytes),
                            ServerSideEncryptionMethod = serverSideEnc
                        };
                        //Add metadata
                        putObject.Metadata?.ForEach((k, v) => { putRequest.Metadata.Add(k, v); });
                        PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);
                        var data = new { response.ETag };
                        apiResult resp = new apiResult(ApiResultStatus.ok, "執行成功", data);
                        return Json(resp);
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                apiResult resp = new apiResult(ApiResultStatus.error, e.Message);
                return Json(resp);
            }
            catch (Exception e)
            {
                apiResult resp = new apiResult(ApiResultStatus.error, e.Message);
                return Json(resp);
            }
        }

        /// <summary>
        /// 根據資料狀態決定連亞馬遜的方式
        /// </summary>
        /// <returns></returns>
        private AmazonS3Client GetAmazonS3Client(PutObjectModel putObject)
        {
            bool reg = dicRegionEndpoint.TryGetValue(putObject.RegionEndpoint, out RegionEndpoint bucketRegion);
            if (!reg)
            {
                throw new Exception("伺服器位址不存在");
            }
            if (!string.IsNullOrWhiteSpace(putObject.AwsSessionToken))
            {
                return new AmazonS3Client(putObject.AwsAccessKeyId, putObject.AwsSecretAccessKey, putObject.AwsSessionToken, bucketRegion);
            }
            else
            {
                return new AmazonS3Client(putObject.AwsAccessKeyId, putObject.AwsSecretAccessKey, bucketRegion);
            }
        }

        /// <summary>
        /// 產生檔案名稱
        /// </summary>
        /// <param name="putObject"></param>
        /// <returns></returns>
        private string GetUploadPathExtension(PutObjectModel putObject)
        {
            var ext = Path.GetExtension(putObject.FileUploadPath);
            var urlExt = Path.GetExtension(putObject.FileUrl);
            if (string.IsNullOrWhiteSpace(ext))
            {
                return putObject.FileUploadPath + urlExt;
            }
            return putObject.FileUploadPath;
        }

        /// <summary>
        /// Stream 轉 byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private byte[] StreamToArrayBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
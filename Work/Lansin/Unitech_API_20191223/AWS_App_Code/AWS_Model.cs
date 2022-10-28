using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EC.ENUMS;

namespace ECAPI.Models.Amazon
{
    /// <summary>
    /// 上傳物件至Amazon
    /// </summary>
    public class PutObjectModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string AwsAccessKeyId { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string AwsSecretAccessKey { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string AwsSessionToken { get; set; }
        /// <summary>
        /// bucketname
        /// </summary>
        public string BucketName { get; set; }
        /// <summary>
        /// 檔案位址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 上傳資料Bucket位址
        /// </summary>
        public string FileUploadPath { get; set; }
        /// <summary>
        /// bucket 需加的Metadata
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; }
        /// <summary>
        /// Bucket伺服器加密方法
        /// </summary>
        public ServerSideEncryptionList SideEncryption { get; set; }
        /// <summary>
        /// Bucket伺服器位址
        /// </summary>
        public RegionEndpointsList RegionEndpoint { get; set; }
    }
}
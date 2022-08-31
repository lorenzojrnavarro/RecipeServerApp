using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Configuration;
using System.Collections.Specialized;

public static class AmazonUploader
{
    public static readonly string bucketPath = "https://morgothscookbookbucket.s3.us-west-1.amazonaws.com/";

    public static void SendMyFileToS3(string imageBytes, string bucketName, string fileNameInS3)
    {
        // input explained :
        // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
        // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
        // subDirectoryInBucket : if this string is not empty the file will be uploaded to
        // a subdirectory with this name
        // fileNameInS3 = the file name in the S3

        IAmazonS3 client = new AmazonS3Client("key", "secret_key",  RegionEndpoint.USWest1);        

        // create a TransferUtility instance passing it the IAmazonS3 created in the first step
        TransferUtility utility = new TransferUtility(client);
        // making a TransferUtilityUploadRequest instance
        //TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

        //request.BucketName = bucketName;

        //request.Key = fileNameInS3; //file name up in S3
        //request.FilePath = localFilePath; //local file name

        byte[] bytes = Convert.FromBase64String(imageBytes);

        MemoryStream memStream = new MemoryStream(bytes.Length);
        memStream.Write(bytes, 0, bytes.Length);

        utility.Upload(memStream, bucketName, fileNameInS3); //commensing the transfer
    }
}

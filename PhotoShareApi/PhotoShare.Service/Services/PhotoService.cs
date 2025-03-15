using Amazon.S3.Transfer;
using Amazon.S3;
using AutoMapper;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Service.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PhotoService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public async Task UploadPhoto(PhotoDto photoDto)
        {
            // בדוק אם הקובץ המקומי קיים
            if (!File.Exists(photoDto.Url))
            {
                throw new FileNotFoundException("הקובץ המקומי לא קיים. לא ניתן להעלות."); 
            }

            var bucketName = "your-bucket-name"; // שם ה-Bucket שלך
            var keyName = $"{photoDto.Id}/{Path.GetFileName(photoDto.Url)}"; // שם הקובץ ב-S3

            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1)) // בחר את האזור המתאים
            {
                try
                {
                    // בדוק אם התמונה קיימת ב-S3
                    var response = await client.GetObjectMetadataAsync(bucketName, keyName);
                    if (response != null)
                    {
                        Console.WriteLine("התמונה כבר קיימת ב-S3.");
                        return; // אם התמונה קיימת, יוצאים מהפונקציה
                    }
                }
                catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // אם קיבלנו שגיאת NotFound, זה אומר שהתמונה לא קיימת
                }
                catch (Exception e)
                {
                    Console.WriteLine($"שגיאה בעת בדיקת קיום התמונה ב-S3: {e.Message}");
                    return;
                }

                try
                {
                    var fileTransferUtility = new TransferUtility(client);

                    // העלאת הקובץ ל-S3
                    await fileTransferUtility.UploadAsync(photoDto.Url, bucketName, keyName);

                    // עדכון ה-URL של התמונה ב-Database
                    photoDto.Url = $"https://{bucketName}.s3.amazonaws.com/{keyName}";

                    // שמירה של התמונה ב-Database
                    var photo = _mapper.Map<Photo>(photoDto);
                    await _repositoryManager.Photo.AddAsync(photo);
                }
                catch (AmazonS3Exception e)
                {
                    // טיפול בשגיאות
                    Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
                }
                catch (Exception e)
                {
                    // טיפול בשגיאות כלליות
                    Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
                }
            }
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosByAlbumId(int albumId)
        {
            var photos = (await _repositoryManager.Album.GetAlbumIncludePhotosAsync(albumId)).Photos;
            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync()
        {
            var photos = await _repositoryManager.Photo.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<PhotoDto> GetByIdAsync(int id)
        {
            var photo = await _repositoryManager.Photo.GetByIdAsync(id);
            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> CreateAsync(PhotoDto photoDto)
        {
            var photo= _mapper.Map<Photo>(photoDto);
          var res=  await _repositoryManager.Photo.AddAsync(photo);
            return _mapper.Map<PhotoDto>(res);
        }

        public async Task UpdateAsync(PhotoDto photoDto)
        {
            var photo = _mapper.Map<Photo>(photoDto);
            await _repositoryManager.Photo.UpdateAsync(photo);
        }

        public async Task DeleteAsync(int id)
        {
            await _repositoryManager.Photo.DeleteAsync(id);
        }

    }

}

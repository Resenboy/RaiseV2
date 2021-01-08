using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using Raise.Model.Models;
using Raise.Utils;
using System;
using System.IO;

namespace Raise.DataBase.FireStorage
{
    public abstract class FireStorageClient<T> where T : BaseModel
    {
        public static IConfiguration Configuration { get; set; }
        public FirebaseStorage Storage;

        T Obj;

        /// <summary>
        /// Método que recebe as configurações dos repositorios firebase
        /// </summary>
        public void GetFirebaseConnection()
        {
            // se for realizar um debug pelo console app alterar para config.json (config azure appsettings.json)
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            Storage = new FirebaseStorage(Configuration["fireStoragePath"]);
        }

        /// <summary>
        /// Método que realiza o upload de images
        /// </summary>
        /// <param name="obj">Path pricipal do objeto</param>
        /// <param name="fileName">Nome do arquivo que será inserido</param>
        /// <param name="fileStream">Arquivo em stream</param>
        /// <returns></returns>
        public ApiResponse<T> Upload(T obj, Guid fileName, Stream fileStream)
        {
            try
            {
                var imageUrl = Storage.Child($"{Obj.GetType().Name}").Child(fileName.ToString()).PutAsync(fileStream);
                if (imageUrl != null)
                    return new ApiResponse<T>(obj, null, true, System.Net.HttpStatusCode.OK);
                else
                    return new ApiResponse<T>(null, "Falha ao realizar upload da imagem", false, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception exc)
            {
                return new ApiResponse<T>(null, exc.InnerException.Message, false, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Método que realiza o upload de images de posts
        /// </summary>
        /// <param name="obj">Path princial do objeto</param>
        /// <param name="userDirectory">Path do usuário específico</param>
        /// <param name="fileName">Nome do arquivo que será inserido</param>
        /// <param name="fileStream">Arquivo em stream</param>
        /// <returns></returns>
        public ApiResponse<T> UploadPost(T obj, Guid fileName, Stream fileStream)
        {
            try
            {
                var imageUrl = Storage.Child($"{Obj.GetType().Name}").Child((obj as BaseModel).GuidKey.ToString()).Child(fileName.ToString()).PutAsync(fileStream);
                if (imageUrl.TargetUrl != null)
                    return new ApiResponse<T>(obj, null, true, System.Net.HttpStatusCode.OK);
                else
                    return new ApiResponse<T>(null, "Falha ao realizar upload da imagem do post", false, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception exc)
            {
                return new ApiResponse<T>(null, exc.InnerException.Message, false, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

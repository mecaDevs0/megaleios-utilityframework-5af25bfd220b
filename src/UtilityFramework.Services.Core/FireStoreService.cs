using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UtilityFramework.Application.Core;
using UtilityFramework.Services.Core.Interface;
using UtilityFramework.Services.Core.Models;

namespace UtilityFramework.Services.Core
{
    public class FireStoreService : IFireStoreService
    {

        private readonly FirestoreDb _db;
        private readonly string _directory;

        public FireStoreService()
        {
            var fireStoreSettings = Utilities.GetConfigurationRoot().GetSection("FireStoreSettings")
                .Get<FireStoreSettings>();

            var projectId = fireStoreSettings.ProjectId;
            _directory = Directory.GetCurrentDirectory();

            var credentialPath = $"{_directory}\\{fireStoreSettings.FileCredentials}";

            if (File.Exists(credentialPath) == false)
                throw new ArgumentNullException(fireStoreSettings.Environment, "Arquivo de credenciais não encontrado");

            //vefica a existencia da variavel de credenciais do google
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(fireStoreSettings.Environment)))
                Environment.SetEnvironmentVariable(fireStoreSettings.Environment, credentialPath);

            _db = FirestoreDb.Create(projectId);


        }

        public void SavePost<T>(T entity, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(collectionName))
                collectionName = Utilities.GetValueByProperty(entity, "CollectionName")?.ToString();

            var collection = _db.Collection(collectionName);

            var data = MapFireStore(entity);

            collection.AddAsync(data).Wait();
        }

        public void SavePatch<T>(T entity, string id = null, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {

            if (string.IsNullOrEmpty(collectionName))
                collectionName = Utilities.GetValueByProperty(entity, "CollectionName")?.ToString();

            if (string.IsNullOrEmpty(id))
                id = Utilities.GetValueByProperty(entity, "Id")?.ToString();

            var collection = _db.Collection(collectionName);

            var data = MapFireStore(entity);

            collection.Document(id).UpdateAsync(data, Precondition.None).Wait();
        }

        public void Delete(string collectionName, string id, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var collection = _db.Collection(collectionName);

            collection.Document(id).DeleteAsync().Wait();
        }

        public void SavePatchMultiple(string collectionName, List<string> refDocuments, string property, object data, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {

            var collection = _db.Collection(collectionName);
            var batch = collection.Database.StartBatch();

            if (string.IsNullOrEmpty(property))
            {

                var updates = MapFireStore(data);

                foreach (var item in refDocuments)
                {
                    try
                    {
                        var docRef = collection.Document(item);

                        batch.Update(docRef, updates, Precondition.None);
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogFile(new { MethodName = memberName, RefDocument = item }, $"{_directory}/LogFireStore/{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.txt", ex);
                    }
                }

            }
            else
            {

                var dataValid = ToDynamicFirestore(data);

                var updates = new Dictionary<FieldPath, object> { { new FieldPath(property), dataValid } };

                foreach (var item in refDocuments)
                {
                    try
                    {
                        var docRef = collection.Document(item);

                        batch.Update(docRef, updates, Precondition.None);
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogFile(new { MethodName = memberName, RefDocument = item }, $"{_directory}/LogFireStore/{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.txt", ex);
                    }
                }
            }
            batch.CommitAsync().Wait();

        }
        public async Task SavePatchMultipleAsync(string collectionName, List<string> refDocuments, string property, object data, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {

            var collection = _db.Collection(collectionName);
            var batch = collection.Database.StartBatch();

            if (string.IsNullOrEmpty(property))
            {
                var updates = MapFireStore(data);

                for (int i = 0; i < refDocuments.Count; i++)
                {
                    try
                    {
                        var docRef = collection.Document(refDocuments[i]);

                        batch.Update(docRef, updates, Precondition.None);
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogFile(new { MethodName = memberName, RefDocument = refDocuments[i] }, $"{_directory}/LogFireStore/{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.txt", ex);
                    }
                }
            }
            else
            {
                var dataValid = ToDynamicFirestore(data);

                var updates = new Dictionary<FieldPath, object> { { new FieldPath(property), dataValid } };


                for (int i = 0; i < refDocuments.Count; i++)
                {
                    try
                    {
                        var docRef = collection.Document(refDocuments[i]);

                        batch.Update(docRef, updates, Precondition.None);
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogFile(new { MethodName = memberName, RefDocument = refDocuments[i] }, $"{_directory}/LogFireStore/{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.txt", ex);
                    }
                }
            }
            await batch.CommitAsync().ConfigureAwait(false);

        }

        public async Task SavePostAsync<T>(T entity, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(collectionName))
                collectionName = Utilities.GetValueByProperty(entity, "CollectionName")?.ToString();

            var collection = _db.Collection(collectionName);

            var data = MapFireStore(entity);

            await collection.AddAsync(data);
        }
        public async Task SavePatchAsync<T>(T entity, string id = null, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {

            if (string.IsNullOrEmpty(collectionName))
                collectionName = Utilities.GetValueByProperty(entity, "CollectionName")?.ToString();

            if (string.IsNullOrEmpty(id))
                id = Utilities.GetValueByProperty(entity, "Id")?.ToString();

            var collection = _db.Collection(collectionName);

            var data = MapFireStore(entity);

            await collection.Document(id).UpdateAsync(data, Precondition.None);
        }

        public async Task DeleteAsync(string collectionName, string id, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var collection = _db.Collection(collectionName);

            await collection.Document(id).DeleteAsync();
        }

        private Dictionary<FieldPath, object> MapFireStore<T>(T entity, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var updates = new Dictionary<FieldPath, object>();

            var properties = entity.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];

                var custonTargetPropertie = (JsonPropertyAttribute)propertyInfo.GetCustomAttributes(typeof(JsonPropertyAttribute), false).FirstOrDefault();

                if (propertyInfo.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).Any())
                    continue;

                var propValue = propertyInfo.GetValue(entity, null);
                var propName = custonTargetPropertie?.PropertyName ?? propertyInfo.Name.LowercaseFirst();

                updates.Add(new FieldPath(propName),
                    propertyInfo.GetCustomAttributes(typeof(IsCustomClass), false).Count() > 0
                        ? ToDynamicFirestore(propValue)
                        : propValue);
            }

            return updates;
        }

        private static dynamic ToDynamicFirestore<T>(T model)
        {
            if (model == null)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var custonData = JsonConvert.DeserializeObject(json);

            var dictionary = custonData.ToDictionary<object>();
            var dataAnonymous = new ExpandoObject();
            var valuePairs = (ICollection<KeyValuePair<string, object>>)dataAnonymous;

            foreach (var kvp in dictionary)
            {
                valuePairs.Add(kvp);
            }

            dynamic anonymous = dataAnonymous;

            return anonymous;
        }

    }
}
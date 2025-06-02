using System.Collections.Generic;
using System.Threading.Tasks;

namespace UtilityFramework.Services.Core3.Interface
{
    public interface IFireStoreService
    {
        /// <summary>
        /// REGISTRAR OBJ NO FIRESTORE
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="collectionName"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        void SavePost<T>(T entity, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// ATUALIZAR OBJETO (TODAS PROPS DIFERENTE SÃO ATUALIZADAS)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="collectionName"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        void SavePatch<T>(T entity, string id = null, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// DELETAR OBJ DO FIRESTORE    
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="memberName"></param>
        void Delete(string collectionName, string id, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// ATUALIZAR VARIOS OBJETOS NO FIREBASE
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="refDocuments"></param>
        /// <param name="property"></param>
        /// <param name="data"></param>
        /// <param name="memberName"></param>
        void SavePatchMultiple(string collectionName, List<string> refDocuments, string property, object data, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// SALVAR NO FIREBASE ASYNC
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="collectionName"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task SavePostAsync<T>(T entity, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// ATUALIZAR NO FIREBASE ASYNC
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="collectionName"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task SavePatchAsync<T>(T entity, string id = null, string collectionName = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");
        /// <summary>
        /// REMOVER ASYNC
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        Task DeleteAsync(string collectionName, string id, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "");

    }
}
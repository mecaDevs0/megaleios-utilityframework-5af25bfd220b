using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UtilityFramework.Application.Core;
using UtilityFramework.Services.Core.Enum;
using UtilityFramework.Services.Core.Interface;
using UtilityFramework.Services.Core.Models;
using System;

namespace UtilityFramework.Services.Core
{
    public class FirebaseServices : IFirebaseServices
    {
        public static FirebaseClient ClientFirebase;
        public static FirebaseConfigViewModel configFirebase;
        public static FirebaseAuthProvider authProvider;
        private string _json;
        public static FirebaseAuthLink auth = null;

        public FirebaseServices()
        {

            configFirebase = Utilities.GetConfigurationRoot().GetSection("FirebaseSettings").Get<FirebaseConfigViewModel>();

            if (configFirebase == null)
                throw new ArgumentNullException("FirebaseSettings", "Informe os dados de configuração do firebase em Config.json na prop \"FirebaseSettings\"");

            if (string.IsNullOrEmpty(configFirebase.ApiKey))
                throw new ArgumentNullException(nameof(FirebaseConfigViewModel.ApiKey), "Informe a APIKEY do firebase");

            authProvider = new FirebaseAuthProvider(new FirebaseConfig(configFirebase.ApiKey));

            GetToken();

        }

        public T Get<T>(string child)
        {
            RenewToken();
            return ClientFirebase.Child(child).OnceSingleAsync<T>().Result;
        }

        public void SavePost(string child, object data, JsonSerializerSettings configJson = null)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            ClientFirebase.Child(child).PostAsync(_json).Wait();
        }

        public void SavePut(string child, object data, JsonSerializerSettings configJson = null)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            ClientFirebase.Child(child).PutAsync(_json).Wait();
        }

        public void SavePatch(string child, object data, JsonSerializerSettings configJson = null)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            ClientFirebase.Child(child).PatchAsync(_json).Wait();
        }

        public void Delete(string child)
        {
            RenewToken();

            ClientFirebase.Child(child).DeleteAsync().Wait();
        }


        public IEnumerable<T> List<T>(string child)
        {
            RenewToken();

            return ClientFirebase.Child(child).OnceAsync<T>().Result.Select(x => x.Object).ToList();
        }

        public async Task<T> GetAsync<T>(string child, bool configureAwait = false)
        {
            RenewToken();

            return await ClientFirebase.Child(child).OnceSingleAsync<T>().ConfigureAwait(configureAwait);
        }

        public async Task SavePostAsync(string child, object data, JsonSerializerSettings configJson = null, bool configureAwait = false)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            await ClientFirebase.Child(child).PostAsync(_json).ConfigureAwait(configureAwait);

        }

        public async Task SavePutAsync(string child, object data, JsonSerializerSettings configJson = null, bool configureAwait = false)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            await ClientFirebase.Child(child).PutAsync(_json).ConfigureAwait(configureAwait);
        }

        public async Task SavePatchAsync(string child, object data, JsonSerializerSettings configJson = null, bool configureAwait = false)
        {
            RenewToken();

            _json = configJson != null ? JsonConvert.SerializeObject(data, configJson) : JsonConvert.SerializeObject(data);

            await ClientFirebase.Child(child).PatchAsync(_json).ConfigureAwait(configureAwait);


        }

        public async Task DeleteAsync(string child, bool configureAwait = false)
        {
            RenewToken();

            await ClientFirebase.Child(child).DeleteAsync().ConfigureAwait(configureAwait);
        }

        public async Task<IEnumerable<T>> ListAsync<T>(string child, bool configureAwait = false)
        {
            RenewToken();

            var list = await ClientFirebase.Child(child).OnceAsync<T>().ConfigureAwait(configureAwait);

            return list.Select(x => x.Object);
        }

        private void RenewToken()
        {
            try
            {
                if (auth == null && configFirebase.UseAuth == false)
                {
                    GetToken();
                    return;
                }

                if (auth.IsExpired() && authProvider != null)
                    auth = authProvider.RefreshAuthAsync(auth).Result;

                if (ClientFirebase == null && configFirebase != null)
                    ClientFirebase = new FirebaseClient(configFirebase.UrlDataBase);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetToken()
        {

            if (configFirebase.UseAuth)
            {
                if (auth == null)
                {
                    switch (configFirebase.TypeAuth)
                    {
                        case TypeAuthFirebase.Social:
                            if (string.IsNullOrEmpty(configFirebase.SocialKey))
                                throw new ArgumentNullException(nameof(FirebaseConfigViewModel.SocialKey), "Informe a SOCIALKEY do firebase");

                            auth = authProvider.SignInWithOAuthAsync(configFirebase.FirebaseAuthType, configFirebase.SocialKey).Result;

                            break;
                        case TypeAuthFirebase.EmailPassword:

                            if (string.IsNullOrEmpty(configFirebase.Email))
                                throw new ArgumentNullException(nameof(FirebaseConfigViewModel.Email), "Informe a Email de acesso ao firebase");

                            if (string.IsNullOrEmpty(configFirebase.Password))
                                throw new ArgumentNullException(nameof(FirebaseConfigViewModel.Password), "Informe a Password de acesso ao firebase");

                            auth = authProvider.SignInWithEmailAndPasswordAsync(configFirebase.Email, configFirebase.Password).Result;
                            break;

                        default:
                            auth = authProvider.SignInAnonymouslyAsync().Result;
                            break;
                    }

                    ClientFirebase = new FirebaseClient(configFirebase.UrlDataBase, new FirebaseOptions()
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                    });
                }
                else
                {
                    RenewToken();
                }
            }
            else
            {
                ClientFirebase = new FirebaseClient(configFirebase.UrlDataBase);
            }

        }


    }
}
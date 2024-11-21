using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.CommonUtils.Common
{
    public static class RestHelper<T>
    {
        /// <summary>
        /// To Get resource over a web api using GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync(Uri url, Dictionary<string, string> headers = null)
        {
            using (var client = new RestClient())
            {
                var request = new RestRequest(url);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                var response = await client.ExecuteGetAsync<T>(request);
                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }
                return response.Data;
            }
        }
        /// <summary>
        /// Post - to create a new resource over a web api using POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync(Uri url, object objData, Dictionary<string, string> headers = null)
        {
            using (var client = new RestClient())
            {
                var request = new RestRequest(url);
                request.AddJsonBody(objData);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                var response = await client.ExecutePostAsync<T>(request);
               if (!response.IsSuccessful)
                {
                   // response.ErrorException.Message
                    throw response.ErrorException;
                }

                return response.Data;
            }
        }

        /// <summary>
        /// Put - to update an existing item over a web api using PUT
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> PutAsync(Uri url, object objData, Dictionary<string, string> headers = null)
        {
            using (var client = new RestClient())
            {
                var request = new RestRequest(url);
                request.AddJsonBody(objData);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                var response = await client.ExecutePutAsync<T>(request);
                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }

                return response.Data;
            }
        }


        /// <summary>
        /// Delete - to delete exisiting item over a web api using DELETE 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteAsync(Uri url, Dictionary<string, string> headers = null)
        {
            using (var client = new RestClient())
            {
                var request = new RestRequest(url, Method.Delete);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                var response = await client.ExecuteAsync<T>(request);
                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }

                return response.IsSuccessful;
            }
        }
    }
}

using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using iTextSharp.text.pdf;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.recencementAutomation
{


    public class AcordaRecensementWebUIAutomation
        {
            public const string AcordaRecensementWebUI = "AcordaRecensementWebUI";
            private HttpClient client;

        #region Properties

            public string BaseUrl { get; }
            public CookieContainer cookies { get; private set; }
            public string Canton { get; }
            public string UserName { get; }
            public string Password { get; }
            public int FarmId { get; }

            #endregion

            #region Initialization

            public AcordaRecensementWebUIAutomation(HttpClient httpClient, string canton, string username,
                string password, int farmId)
            {

            client = httpClient;
            client.BaseAddress = new Uri("http://localhost:8221");
            Canton = canton;
            UserName = username;
            Password = password;
            FarmId = farmId;
            cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                //CookieContainer = cookies,
                //UseDefaultCredentials = true,
            };
            client = new HttpClient(handler);

            }


            /*public static AcordaRecensementWebUIAutomation FromConfig(AcordaControlConfiguration configuration,
                                                                      Func<AcordaControlConfiguration, WebServiceBase> getConfigSection,
                                                                      string canton,
                                                                      int farmId)
            {
                var config = getConfigSection(configuration);
                Requires<ConfigurationException>.IsNotNull(config, $"Cannot initialize instance of {nameof(AcordaRecensementWebUIAutomation)}: no api configuration found.");

                string userName = Environment.GetEnvironmentVariable(config.UserNameEnvironmentVariable);
                Requires<ConfigurationException>.IsNotEmpty(userName, $"Cannot initialize instance of {nameof(AcordaRecensementWebUIAutomation)}: no environment variable {config.UserNameEnvironmentVariable} found.");

                string password = Environment.GetEnvironmentVariable(config.PasswordEnvironmentVariable);
                Requires<ConfigurationException>.IsNotEmpty(userName, $"Cannot initialize instance of {nameof(AcordaRecensementWebUIAutomation)}: no environment variable {config.PasswordEnvironmentVariable} found.");
                
                return new AcordaRecensementWebUIAutomation(config.Url, canton, userName, password, farmId);
            }*/

            #endregion

            #region Services

            public async Task LoginAsync(string username, string password)
            {
                string url = BaseUrl + "/Account/LogOn";
                var postParams = new Dictionary<string, string>();
                postParams.Add("UserName", username);
                postParams.Add("Password", password);
                var postContent = new FormUrlEncodedContent(postParams);
                HttpResponseMessage response = await client.PostAsync(url, postContent);
            }

        public async Task SelectCanton(string canton)
            {

                string url = $"/Account/SelectCanton?cantonCode={canton}";
                HttpResponseMessage response = await client.GetAsync(url);
                var tmp = response.StatusCode;
                IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            }

        public async Task ChooseFarmAsync(int farmId)
            {
                string url = BaseUrl + "/Home/ChooseFarm";
                var postParams = new Dictionary<string, string>();
                postParams.Add("farmId", farmId.ToString("D"));
                var postContent = new FormUrlEncodedContent(postParams);
                HttpResponseMessage response = await client.PostAsync(url, postContent);
            }

            public async Task DownloadFormCAsync(string filename)
            {
                await SelectCanton(Canton);
                await LoginAsync(UserName, Password);
                await ChooseFarmAsync(FarmId);
                //DownloadFile(BaseUrl + $"/Consultation/PdfFormC/farmId{FarmId}", filename);
            }

            public Task<byte[]> DownloadFormCAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormC/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormSAsync()
            {
                return DownloadBinaryAsync($"/Pdf/FormStatCurrent/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormInscriptionsAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfInscriptions/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormlandscapeAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfPaysage/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormLowPesticideAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfLowPesticide/farmId{FarmId}");
            }
            public Task<byte[]> DownloadFormLowPesticideNextYearAsync()
            {
                return DownloadBinaryAsync($"/CerLowPesticideNextYear/Pdf/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormCbeAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormCbeOther/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormMildSoilTreatmentAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfMildSoil/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormMildSoilTreatmentNextYearAsync()
            {
                return DownloadBinaryAsync($"/MildSoilTreatmentNextYear/Pdf/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB2YearlyAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfYearlyBees/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB2Async()
            {
                return DownloadBinaryAsync($"/FormB2/PdfFormB2/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB2InscriptionAsync()
            {
                return DownloadBinaryAsync($"/FormB2/PdfFormB2Inscriptions/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB3Async()
            {
                return DownloadBinaryAsync($"/FormB3/PdfFormB3/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB3InscriptionAsync()
            {
                return DownloadBinaryAsync($"/FormB3/PdfFormB3Inscriptions/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFichePer1Async()
            {
                return DownloadBinaryAsync($"/Consultation/FichePer1/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFichePer2Async()
            {
                return DownloadBinaryAsync($"/Consultation/FichePer2/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormAnimalSummeringAsync()
            {
                return DownloadBinaryAsync($"/ConsultationEstivage/PdfSummeringAnimal/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormQualitySummeringAsync()
            {
                return DownloadBinaryAsync($"/ConsultationEstivage/PdfFormQualitySummering/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormLandscapeSummeringAsync()
            {
                return DownloadBinaryAsync($"/ConsultationEstivage/PdfPaysage/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormCbeSummeringAsync()
            {
                return DownloadBinaryAsync($"/ConsultationEstivage/PdfFormCbeOtherSummering/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormQualityAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormQuality/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormNetworkAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormNetwork/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormWaterProtectionAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfWaterProtection/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormAAsync()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormA/farmId{FarmId}");
            }

            public Task<byte[]> DownloadFormB1Async()
            {
                return DownloadBinaryAsync($"/Consultation/PdfFormB1/farmId{FarmId}");
            }

            #endregion

            #region Helpers

            private async Task<byte[]> DownloadBinaryAsync(string relativeUrl)
            {
                await SelectCanton(Canton);
                await LoginAsync(UserName, Password);
                await ChooseFarmAsync(FarmId);
                HttpResponseMessage response = await client.GetAsync(relativeUrl);
                return null;//await response.Content.ReadAsByteArrayAsync();
                
            }

            #endregion

        }
    
}

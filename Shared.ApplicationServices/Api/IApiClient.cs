﻿using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.IndexedDb;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using CSharpFunctionalExtensions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        //void SetAuthToken(string basicAuthToken);
        Task<Result<string>> FetchInspectorNameAsync(string uri);
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchAllMandatesAsync(string uri);
        Task<Result<ViewModel.Town.Town[]>> FetchAllTownsAsync(string uri);
        Task<Result<Mandate>> FetchMandateDetailAsync(string uri);
        Task<Result<string>> FetchRawJsonAsync(string uri);
        Task<Result<FormInscription>> FetchFileAsync(string uri);
        Task<Result<ViewModel.Farm.Farm>> FetchFarmDetailAsync(string uri);
        Task<Result<ChecklistSample>> FetchChecklistSampleAsync(string uri);
        Task<Result<MergeResult>> SendMergePackage(string uri, MergePackage mergePackage);
        Task<Result<MergeResult>> SendMergeFile(string uri, FileChecklist mergePackage);
        Task<Result<string>> CancelMergePackage(string uri, int id, string state);
        Task<Result<string>> AcknowledgeMerge(string uri, int id);
    }
}
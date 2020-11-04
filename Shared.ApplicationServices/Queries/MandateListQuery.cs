using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Decorators;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Queries
{
    //[AuditLog]
    public sealed class MandateListQuery : IQuery<ValueTask<Mandate[]>>
    {
        public sealed class MandateListQueryHandler : IQueryHandler<MandateListQuery, ValueTask<Mandate[]>>
        {
            private readonly RepositoryFactory repositoryFactory_;
            public MandateListQueryHandler(RepositoryFactory repositoryFactory)
            {
                repositoryFactory_ = repositoryFactory;
            }

            public async ValueTask<Mandate[]> Handle(MandateListQuery query)
            {
                var repository = repositoryFactory_.CreateRepository();
                return await repository.ReadAllMandatesAsync();
            }
        }
    }
}

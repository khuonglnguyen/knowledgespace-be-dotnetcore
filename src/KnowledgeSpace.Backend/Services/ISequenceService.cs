using System.Threading.Tasks;

namespace KnowledgeSpace.Backend.Services
{
    public interface ISequenceService
    {
        Task<int> GetKnowledgeBaseNewId();
    }
}

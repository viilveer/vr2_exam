

namespace API.DAL.Interfaces
{
    public interface IUOW

    {
        T GetRepository<T>() where T : class;


        //UOW Methods, that dont fit into specific repo
    }
}
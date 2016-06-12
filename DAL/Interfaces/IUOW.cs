

namespace DAL.Interfaces
{
    public interface IUOW

    {
        void Commit();

        void RefreshAllEntities();

        T GetRepository<T>() where T : class;


        //UOW Methods, that dont fit into specific repo
    }
}
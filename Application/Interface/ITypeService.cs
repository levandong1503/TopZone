namespace Application.Interface;

public interface ITypeService
{
    public void Add(Type type);
    public Type GetById(int id);
}

namespace Core.Player
{
    public interface IServerClientModelQuery
    {
        public int Id { get; }
    }
    
    public interface IServerClientModelCommand
    {
        
    }
    
    public interface IServerClient: IServerClientModelQuery ,IServerClientModelCommand
    {
        
    }
    
    public interface IServerClientModelMutable: IServerClient 
    
    {
        public void SetId(int id);
    }
}
namespace ObligatoriskOpgave1
{
    public interface ITrophiesRepository
    {
        IEnumerable<Trophy> Get(int? year = null, string? sortby = null);
        Trophy? GetById(int id);
        Trophy Add(Trophy trophy);
        Trophy? Remove(int id);
        Trophy? Update(int id, Trophy trophy);
    }
}

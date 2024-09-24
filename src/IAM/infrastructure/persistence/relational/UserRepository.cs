using Microsoft.EntityFrameworkCore;
using tecnosor.cleanarchitecture.common.domain.match;
using iam.domain;

namespace iam.infrastructure.persistence.relational;

public class UserRepository : IUserRepository
{
    private UserRelationalPersistenceContext _context;
    private IMatch<ApplicationUser> _match;

    public void CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public User GetUserByUserName(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUserNameAsync(string id)
    {
        throw new NotImplementedException();
    }

    public List<User> Match(ISet<Filter<User>> filterList)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> MatchQueryable(ISet<Filter<User>> filterList)
    {
        throw new NotImplementedException();
    }

    /*
    public UserRepository(UserRelationalPersistenceContext context, IMatch<Publication> match)
    {
        _context = context;
        _match = match;
    }

    public Publication CreatePublication(Publication publication)
    {
        var lpublication = _context.Publications.Where(p => p.Id == publication.Id).First();
        if (lpublication != null)
        {
            throw new PublicationExistException("Create publication cannot be applied because publication already exist");
        }
        _context.Publications.Add(publication);
        _context.SaveChanges();
        return publication;
    }

    public async Task<Publication> CreatePublicationAsync(Publication publication)
    {
        var lpublication = await _context.Publications.Where(p => p.Id == publication.Id).FirstAsync();
        if (lpublication != null)
        {
            throw new PublicationExistException("Create publication cannot be applied because publication already exist");
        }
        _context.Publications.Add(publication);
        await _context.SaveChangesAsync();
        return publication;
    }

    public void DeletePublicationById(string id)
    {
        var publication = _context.Publications.Where(p => p.Id == id).First();
        if (publication is null)
        {
            throw new PublicationNotFoundException("Delete publication cannot be applied because publication does not exist");
        }
        _context.Publications.Remove(publication);
        _context.SaveChanges();
    }

    public async Task DeletePublicationByIdAsync(string id)
    {
        var publication = await _context.Publications.Where(p => p.Id == id).FirstAsync();
        if (publication is null)
        {
            throw new PublicationNotFoundException("Delete publication cannot be applied because publication does not exist");
        }
        _context.Publications.Remove(publication);
        await _context.SaveChangesAsync();
    }

    public Publication GetPublicationById(string id)
    {
        var publication = _context.Publications.Where(p => p.Id == id).First();
        if (publication is null)
        {
            throw new PublicationNotFoundException("Get publication by Id cannot be applied because publication does not exist");
        }
        return publication;
    }

    public async Task<Publication> GetPublicationByIdAsync(string id)
    {
        var publication = await _context.Publications.Where(p => p.Id == id).FirstAsync();
        if (publication is null)
        {
            throw new PublicationNotFoundException("Get publication by Id cannot be applied because publication does not exist");
        }
        return publication;
    }

    public List<Publication> Match(ISet<Filter<Publication>> filterList) => _match.Match(filterList);

    public IQueryable<Publication> MatchQueryable(ISet<Filter<Publication>> filterList) => _match.MatchQueryable(filterList);

    public Publication UpdatePublication(Publication publication)
    {
        _context.Publications.Update(publication);
        _context.SaveChanges();
        return publication;
    }

    public async Task<Publication> UpdatePublicationAsync(Publication publication)
    {
        _context.Publications.Update(publication);
        await _context.SaveChangesAsync();
        return publication;
    }
    */
}

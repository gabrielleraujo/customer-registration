using CustomerRegistration.Domain.Models.Exceptions;

namespace CustomerRegistration.Domain.Models.Abstracts;

public abstract class BaseDomainModel
{
    public IReadOnlyCollection<string> Erros => _erros.ToList();
    private IList<string> _erros;

    public bool IsValid => _erros.Count == 0;

    public BaseDomainModel()
    {
        _erros = new List<string>();
    }

    /// <summary>
    /// Add the validation rules here.
    /// </summary>
    protected abstract void ApplyValidation();

    private void FinalizeValidation()
    {
        if (!IsValid)
        {
            throw new DomainException("\nErrors:\n" + string.Join("\n", _erros));
        }
    }

    public void AddError(string error)
    {
        _erros.Add(error);
    }

    public void Validate()
    {
        ApplyValidation();
        FinalizeValidation();
    }
}

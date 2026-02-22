namespace ClassLibrary
{
    public class ValidationService
    {
        // params before C# 13 (limited to arrays)
        public bool ValidateAll( params string[] values )
        {
            if (values ==  null || values.Length == 0) return false;

            return values.All( v => !string.IsNullOrEmpty( v ) );
        }

        // params collections in C# 13 
        // https://learn.microsoft.com/en-us/dotnet/api/system.readonlyspan-1
        public bool ValidateAll_CS13( params ReadOnlySpan<string> values )
        {
            if (values.Length == 0) return false;

            foreach (var v in values)
            {
                if (!string.IsNullOrEmpty(v))
                {
                    return false;
                }
            }
            return true;
        }

        // params collections in C# 13 
        // supported types for params collections are Array, List<T>, Span, ReadOnlySpan
        // IEnumerable<T>, ICollection<T>, IReadOnlyCollection<T>, IList<T>, IReadOnlyList<T>
        public ValidationResult ValidateEmails( params  List<string> emails )
        {
            var result = new ValidationResult();
            foreach (var email in emails)
            {
                if (!email.Contains('@')) result.Errors.Add($"{email} is an invalid email");
            }
            result.IsValid = result.Errors.Count == 0;
            return result;
        }
    }
}

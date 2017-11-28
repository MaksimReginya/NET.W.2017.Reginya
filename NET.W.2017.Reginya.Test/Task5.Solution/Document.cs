using System;
using System.Collections.Generic;
using Task5.Solution.DocumentPartVisitors;

namespace Task5.Solution
{
    public class Document
    {
        private readonly IEnumerable<DocumentPart> _parts;

        public Document(IEnumerable<DocumentPart> parts)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            this._parts = new List<DocumentPart>(parts);
        }

        public string Convert(DocumentConverter converter)
            => converter.ConvertDocument(_parts);        
    }
}

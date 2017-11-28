using System;
using System.Collections.Generic;

namespace Task5.Solution
{
    public class Document
    {
        private IEnumerable<DocumentPart> _parts;

        public Document(IEnumerable<DocumentPart> parts)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            Parts = parts;
        }

        public IEnumerable<DocumentPart> Parts
        {
            get => new List<DocumentPart>(_parts);

            set
            {
                _parts = new List<DocumentPart>(value);
            }
        }
    }
}

using Grs.BioRestock.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Domain.Entities
{
    public class BonDeRetourArticle : AuditableEntity<int>
    {
        public int ArticleId { get; set; }
        public int BonDeretourId { get; set; }
        public BonDeRetour BonDeRetour { get; set; }
        public List<Article> Articles { get; set; }
        public int Quantity { get; set; }
    }
}
